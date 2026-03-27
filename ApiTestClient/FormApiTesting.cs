using APIServiceDoklado;
using DataAccess;
using DataAccess.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ApiTestClient
{
    public partial class FormApiTesting : Form
    {
        private readonly DokladoService _dokladoService;
        private readonly AppDbContext _dbContext;
        private readonly string connectionString;
        private bool _comboInitialized = false;
        private List<CompanyLookup> companies;
        private string defaultFilename; 
        //var companies;

        public FormApiTesting(AppDbContext dbContext)
        {
            InitializeComponent();
            toDatePicker.Value = DateTime.Now;
            btnFetchData.Select();
            _dokladoService = new DokladoService();
            _dokladoService.ApiKey = textBoxApiKey.Text;
            _dokladoService.CompanyId = comboBoxCompany.Text;
            _dbContext = dbContext;

            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Fetch the connection string
            connectionString = configuration.GetConnectionString("DefaultConnection");
            PopulateCompaniescomboBox();

            fromDatePicker.Value = DateTime.Parse("01." + DateTime.Today.Month.ToString() + "." + DateTime.Today.Year.ToString());

            comboBoxCompany.SelectedIndex = -1;  // clear selection
            _comboInitialized = true;
        }

        private void PopulateCompaniescomboBox()
        {
            // Load companies into the combo box
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            var context = new AppDbContext(options);
            var repository = new DataAccess.CompaniesRepository(context);
            companies = (List<CompanyLookup>)repository.GetCompanies();
            comboBoxCompany.DataSource = companies;
            comboBoxCompany.DisplayMember = "Name";
            comboBoxCompany.ValueMember = "ICO";
            comboBoxCompany.SelectedIndex = -1; // No selection by default
        }

        private async void BtnFetchData_Click(object sender, EventArgs e)
        {
            try
            {
                // Fetch input values
                var fromDate = fromDatePicker.Value;
                var toDate = toDatePicker.Value;
                _dokladoService.ApiKey = textBoxApiKey.Text;
                // CompanyId text box missing

                // Fetch documents from API
                var documents = await _dokladoService.GetReceiptsAsync(fromDate);
                /*
                                // Load configuration from appsettings.json
                                var configuration = new ConfigurationBuilder()
                                    .SetBasePath(AppContext.BaseDirectory)
                                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                    .Build();

                                // Fetch the connection string
                                var connectionString = configuration.GetConnectionString("DefaultConnection");
                */
                // Configure DbContextOptions with the connection string
                var options = new DbContextOptionsBuilder<AppDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

                // Save documents to database
                using (var context = new AppDbContext(options))
                {
                    var repository = new DocumentRepository(context);
                    var mappedDocuments = MapDocuments(documents);
                    //await repository.SaveDocumentsAsync(mappedDocuments);

                    int logId = await repository.SaveBatchAndCreateLogAsync(
                        _dokladoService.CompanyId,
                        fromDate,
                        mappedDocuments);

                    var pLogId = new SqlParameter("@LogId", logId);

                    // (optional) one transaction for the 3 data-fix procs
                    await using (var tx = await context.Database.BeginTransactionAsync())
                    {
                        //await context.Database.ExecuteSqlRawAsync("EXEC [dbo].[DeleteDuplicates] @LogId", pLogId);
                        await context.Database.ExecuteSqlRawAsync("EXEC [MkSoft].[PopulateAdresar] @LogId", pLogId);
                        await context.Database.ExecuteSqlRawAsync("EXEC [MkSoft].[ConvertToDoklady2025] @LogId", pLogId);

                        await tx.CommitAsync();
                    }

                    using var conn = context.Database.GetDbConnection();
                    if (conn.State != ConnectionState.Open) await conn.OpenAsync();

                    await using SqlCommand cmd = new SqlCommand("[MkSoft].[ExportToXML2]", (SqlConnection)conn)
                    {
                        CommandType = CommandType.StoredProcedure,
                        CommandTimeout = 120
                    };
                    cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.NVarChar, 32) { Value = _dokladoService.CompanyId });
                    cmd.Parameters.Add(new SqlParameter("@exportFrom", SqlDbType.Date) { Value = fromDate.Date });

                    using var xr = await cmd.ExecuteXmlReaderAsync();
                    string xml;
                    {
                        // Load entire xml safely (no truncation)
                        var xdoc = XDocument.Load(xr);                      // or XmlDocument if you prefer
                        xml = xdoc.ToString(SaveOptions.DisableFormatting); // keep compact
                    }

                    // Save if any XML returned
                    if (!string.IsNullOrEmpty(xml))
                    {
/*
                        using var sfd = new SaveFileDialog
                        {
                            Filter = "XML files (*.xml)|*.xml|All files (*.*)|*.*",
                            FileName = $"Export_{_dokladoService.CompayId}_{fromDate:yyyyMMdd}.xml"
                        };
                        if (sfd.ShowDialog() == DialogResult.OK)
                            File.WriteAllText(sfd.FileName, xml, Encoding.UTF8);
*/
                        var fullPath = Path.Combine(txtPath.Text, $"Export_{defaultFilename}.xml");
                        await File.WriteAllTextAsync(fullPath, xml, Encoding.UTF8);
                    }

                    ShowImportLogAsync(logId);

                    //                    await repository.SaveDocumentsAsync((IEnumerable<DataAccess.Entities.Document>)documents);
                }

                // Display in DataGridView
                dataGridViewResult.DataSource = documents;
                lblDocumentsCount.Text = $"Documents fetched: {documents.Count()}";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DoJobs()
        {
            // This method is not used in the current implementation
            // It can be removed or repurposed as needed
        }

        private IEnumerable<DataAccess.Entities.Document> MapDocuments(IEnumerable<APIServiceDoklado.Document> apiDocuments)
        {
            return apiDocuments.Select(apiDoc => new DataAccess.Entities.Document
            {
                ScanedForCompany = _dokladoService.CompanyId,
                DocumentErpId = apiDoc.DocumentId,
                Type = apiDoc.Type,
                ReceiptUID = apiDoc.ReceiptUID,
                IssuedAt = apiDoc.IssuedAt,
                CreatedAt = apiDoc.CreatedAt,
                Currency = apiDoc.Currency,
                VatRateBasic = apiDoc.VatRateBasic,
                TaxBaseBasic = apiDoc.TaxBaseBasic,
                VatAmountBasic = apiDoc.VatAmountBasic,
                VatRateReduced = apiDoc.VatRateReduced,
                TaxBaseReduced = apiDoc.TaxBaseReduced,
                VatAmountReduced = apiDoc.VatAmountReduced,
                VatRateFree = apiDoc.VatRateTaxFree,
                TaxBaseFree = apiDoc.FreeTaxAmount,
                VatAmountFree = apiDoc.VatAmountTaxFree,
                TotalPrice = apiDoc.TotalPrice,
                AccountingCode = apiDoc.AccountingCode,
                OrganizationId = apiDoc.OrganizationId,
                //                organizationVAT = apiDoc.OrganizationVat,
                OrganizationVatId = apiDoc.OrganizationVatId,
                OrganizationTaxId = apiDoc.OrganizationTaxId,
                OrganizationName = apiDoc.OrganizationName,
                StreetName = apiDoc.StreetName,
                Municipality = apiDoc.Municipality,
                PostalCode = apiDoc.PostalCode,
                Email = apiDoc.Email,
                PaymentType = apiDoc.PaymentType,
                InvoiceNumber = apiDoc.InvoiceNumber,

                SubType = apiDoc.SubType,
                TaxPointDate = apiDoc.TaxPointDate,
                VatCategory = apiDoc.VatCategory,
                Note = apiDoc.Note,
                CustomText = apiDoc.CustomText,
                OtherCurrency = apiDoc.OtherCurrency,
                OtherTotalPrice = apiDoc.OtherTotalPrice,




                Address = apiDoc.Address == null ? null : new DataAccess.Entities.Address
                {
                    StreetName = apiDoc.Address.StreetName,
                    PropertyRegistrationNumber = apiDoc.Address.PropertyRegistrationNumber,
                    BuildingNumber = apiDoc.Address.BuildingNumber,
                    PostalCode = apiDoc.Address.PostalCode,
                    Municipality = apiDoc.Address.Municipality,
                    Country = apiDoc.Address.Country
                },

                AccountingSettings = apiDoc.AccountingSettings == null ? null : new DataAccess.Entities.AccountingSettingsElement
                {

                    AccountingItem_AccountingSoftwareId = apiDoc.AccountingSettings.AccountingItem_AccountingSoftwareId,
                    AccountingItem_Value = apiDoc.AccountingSettings.AccountingItem_Value,
                    AccountingItem_Code = apiDoc.AccountingSettings.AccountingItem_Code,
                    AccountingItem_ResourceType = apiDoc.AccountingSettings.AccountingItem_ResourceType,
                    NumericCode_AccountingSoftwareId = apiDoc.AccountingSettings.NumericCode_AccountingSoftwareId,
                    NumericCode_Value = apiDoc.AccountingSettings.NumericCode_Value,
                    NumericCode_Name = apiDoc.AccountingSettings.NumericCode_Name,
                    NumericCode_PaymentType = apiDoc.AccountingSettings.NumericCode_PaymentType,
                    CashRegister_AccountingSoftwareId = apiDoc.AccountingSettings.CashRegister_AccountingSoftwareId,
                    CashRegister_Value = apiDoc.AccountingSettings.CashRegister_Value,
                    CashRegister_Name = apiDoc.AccountingSettings.CashRegister_Name,
                    Project_AccountingSoftwareId = apiDoc.AccountingSettings.Project_AccountingSoftwareId,
                    Project_Value = apiDoc.AccountingSettings.Project_Value,
                    Project_Code = apiDoc.AccountingSettings.Project_Note,
                    Order_AccountingSoftwareId = apiDoc.AccountingSettings.Order_AccountingSoftwareId,
                    Order_Value = apiDoc.AccountingSettings.Order_Value,
                    Order_Code = apiDoc.AccountingSettings.Order_Code,
                    Order_Name = apiDoc.AccountingSettings.Order_Name,
                    Activity_AccountingSoftwareId = apiDoc.AccountingSettings.Activity_AccountingSoftwareId,
                    Activity_Value = apiDoc.AccountingSettings.Activity_Value,
                    Activity_Code = apiDoc.AccountingSettings.Activity_Code,
                    ExpenditureCenter_AccountingSoftwareId = apiDoc.AccountingSettings.ExpenditureCenter_AccountingSoftwareId,
                    ExpenditureCenter_Value = apiDoc.AccountingSettings.ExpenditureCenter_Value,
                    ExpenditureCenter_Code = apiDoc.AccountingSettings.ExpenditureCenter_Code,
                    GeneralDocumentAgenda_AccountingSoftwareId = apiDoc.AccountingSettings.GeneralDocumentAgenda_AccountingSoftwareId,
                    GeneralDocumentAgenda_Name = apiDoc.AccountingSettings.GeneralDocumentAgenda_Name,
                    GeneralDocumentAgenda_Code = apiDoc.AccountingSettings.GeneralDocumentAgenda_Code,
                    AccountingSoftwareAgenda_AccountingSoftwareId = apiDoc.AccountingSettings.AcountingSoftwareAgenda_AccountingSoftwareId,
                    AccountingSoftwareAgenda_Name = apiDoc.AccountingSettings.AccountingSoftwareAgenda_Name,
                    AccountingSoftwareAgenda_Code = apiDoc.AccountingSettings.AccountingSoftwareAgenda_Code,
                    PredefinedNote_AccountingSoftwareId = apiDoc.AccountingSettings.PredefinedNote_AccountingSoftwareId,
                    PredefinedNote_Value = apiDoc.AccountingSettings.PredefinedNote_Value,
                    ClassificationVat_AccountingSoftwareId = apiDoc.AccountingSettings.ClassificationVat_AccountingSoftwareId,
                    ClassificationVat_Name = apiDoc.AccountingSettings.ClassificationVat_Name,
                    ClassificationVat_Code = apiDoc.AccountingSettings.ClassificationVat_Code,
                    ClassificationVat_ValidFrom = apiDoc.AccountingSettings.ClassificationVat_ValidFrom,
                    ClassificationVat_ValidTo = apiDoc.AccountingSettings.ClassificationVat_ValidTo,
                    ClassificationVat_isReverseCharge = apiDoc.AccountingSettings.ClassificationVat_isReverseCharge
                },

                VatSummary = apiDoc.VatSummary?.Select(vat => new DataAccess.Entities.CustomVatInterface
                {
                    TaxBase = vat.TaxBase,
                    VatAmount = vat.VatAmount,
                    //                    isTaxExempt = vat.IsTaxExempt
                    VatRate = vat.VatRate,
                }).ToList(),
                Items = apiDoc.Items?.Select(item => new DataAccess.Entities.ExportItem
                {
                    Name = item.Name,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    VatRate = item.VatRate,
                    ItemType = item.ItemType,
                    Unit = item.Unit,
                    VatAmount = item.VatAmount
                    /*                    
                                        ,
                                        AccountingSettings = item.AccountingSettings == null ? null : new DataAccess.Entities.AccountingSettingsElement
                                        {
                                            Value = item.AccountingSettings.Value,
                                            Code = item.AccountingSettings.Code,
                                            ResourceType = item.AccountingSettings.ResourceType
                                        }
                    */
                }).ToList()
            });
        }

        private IEnumerable<DataAccess.Entities.Attachment> MapAttachments(IEnumerable<APIServiceDoklado.GetAttachmentsV2SuccesfullResponse> apiAttachments, string documentErpId)
        {

            if (apiAttachments == null)
            {
                var attachment = new Attachment
                {
                    DocumentId = documentErpId,
                    DownloadUrl = null,
                    FileName = "DocumentDoesNotExists",
                    FileType = null
                };
                // Add the attachment to the context
                _dbContext.Attachments.Add(attachment);

                // Return a collection containing the attachment
                return new List<Attachment> { attachment };
            }
            else

            if (apiAttachments.Count() == 0)
            {
                var attachment = new Attachment
                {
                    DocumentId = documentErpId,
                    DownloadUrl = null,
                    FileName = null,
                    FileType = null
                };
                // Add the attachment to the context
                _dbContext.Attachments.Add(attachment);

                // Return a collection containing the attachment
                return new List<Attachment> { attachment };
            }
            else
            {
                return apiAttachments.Select(apiAttachment => new DataAccess.Entities.Attachment
                {
                    DocumentId = apiAttachment.DocumentId,
                    FileName = apiAttachment.FileName,
                    FileType = apiAttachment.FileType,
                    DownloadUrl = apiAttachment.DownloadUrl
                });
            }

        }

        private void comboBoxCompany_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxCompany.SelectedIndex > -1 && _comboInitialized)
            {
                btnFetchData.Enabled = true;
                _dokladoService.CompanyId = (string?)comboBoxCompany.SelectedValue;
                defaultFilename = companies[comboBoxCompany.SelectedIndex].DefaultFilename;
            }
        }

        private async void btnGetFilenames_Click(object sender, EventArgs e)
        {
            _dokladoService.ApiKey = textBoxApiKey.Text;
            // Configure DbContextOptions with the connection string
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // Create an instance of DocumentRepository
            using (var context = new AppDbContext(options))
            {
                var repository = new DataAccess.DocumentRepository(context);
                List<string> documentErpIds = repository.GetDocumentErpIdsFromDatabase();
                ICollection<GetAttachmentsV2SuccesfullResponse> attachmentPaths;
                foreach (string documentErpId in documentErpIds)
                {
                    attachmentPaths = await _dokladoService.GetFilePath(documentErpId);
                    var mappedAttachments = MapAttachments(attachmentPaths, documentErpId);
                    await repository.SaveAttachmentsAsync(mappedAttachments);
                }
            }
        }

        private async Task getAttachmentData(string documentId)
        {
            var attachmentPaths = await _dokladoService.GetFilePath(documentId);

            // Configure DbContextOptions with the connection string
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // Save attachments to database
            using (var context = new AppDbContext(options))
            {
                var repository = new DocumentRepository(context);
                var mappedAttachments = MapAttachments(attachmentPaths, documentId);
                await repository.SaveAttachmentsAsync(mappedAttachments);
                //                    await repository.SaveDocumentsAsync((IEnumerable<DataAccess.Entities.Document>)documents);
            }
        }

        private void btnPath_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                fbd.InitialDirectory = txtPath.Text;
                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                {
                    txtPath.Text = fbd.SelectedPath;
                }
            }
        }

        private static string FormatImportLog(ImportLog log, string? filePath = null)
        {
            int Z(int? x) => x ?? 0; // null-safe

            var sb = new StringBuilder();
            void L(string label, string? value = null)
                => sb.AppendLine(value is null ? label : $"{label}\t{value}");

            L($"Import:\t{log.ExecutionDate:yyyy-MM-dd HH:mm:ss}");
            sb.AppendLine();

            L("Company ICO:", log.CompanyCin);
            L("Loaded from date:", log.ExportFrom?.ToString("yyyy-MM-dd") ?? "-");
            sb.AppendLine();

            L("Imported from Doklado:");
            L("Documents:", Z(log.DokladoDocumentsCount).ToString());
            L("Items:", Z(log.DokladoItemsCount).ToString());
            L("DPH Summaries:", Z(log.DokladoVatSummariesCount).ToString());
            L("Deleted duplicates:", Z(log.DeletedDuplicates).ToString());
            sb.AppendLine();

            L("Adresár:", Z(log.MkSoftAdresarCount).ToString());
            L("Doklady:", Z(log.MkSoftDokladyCount).ToString());
            L("Pohyby:", Z(log.MkSoftPohybyCount).ToString());
            sb.AppendLine();

            L("File:");
            L(filePath ?? "(not saved)");
            return sb.ToString();
        }

        private static string SanitizeFileName(string s)
        {
            var bad = Path.GetInvalidFileNameChars();
            return new string(s.Select(ch => bad.Contains(ch) ? '_' : ch).ToArray());
        }

        private static string BuildSuggestedFileName(CompanyLookup c, DateTime? exportFrom)
        {
            var baseName = string.IsNullOrWhiteSpace(c.DefaultFilename)
                ? $"{c.ICO}-{SanitizeFileName(c.Name)}"
                : c.DefaultFilename;

            // If you don’t want date in the name, drop the _{...} part.
            return exportFrom.HasValue
                ? $"Export_{baseName}_{exportFrom:yyyyMMdd}.xml"
                : $"Export_{baseName}.xml";
        }

        private async Task ShowImportLogAsync(int logId)
        {
            // read the log row
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;
            using var ctx = new AppDbContext(options);
            var log = await ctx.ImportLogs.AsNoTracking()
                         .SingleAsync(x => x.Id == logId);

            // find the company for filename
            // (use your bound list; or query by ICO if needed)
            var company = companies.FirstOrDefault(c => c.ICO == log.CompanyCin)
                       ?? new CompanyLookup { ICO = log.CompanyCin, Name = log.CompanyCin, DefaultFilename = null };

            var fileName = BuildSuggestedFileName(company, log.ExportFrom);
            var folder = txtPath.Text; // your “folder textbox” from earlier
            var fullPath = Path.Combine(folder, fileName);

            txtSummary.Text = FormatImportLog(log, fullPath);
        }

    }
}
