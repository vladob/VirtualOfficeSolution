using APIServiceDoklado;
using DataAccess;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ApiTestClient
{
    public partial class FormApiTesting : Form
    {
        private readonly DokladoService _dokladoService;
        private readonly AppDbContext _dbContext;
        private readonly String connectionString;
        private bool _comboInitialized = false;

        public FormApiTesting(AppDbContext dbContext)
        {
            InitializeComponent();
            toDatePicker.Value = DateTime.Now;
            btnFetchData.Select();
            _dokladoService = new DokladoService();
            _dokladoService.CompayId = comboBoxCompany.Text;
            _dbContext = dbContext;

            // Load configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Fetch the connection string
            connectionString = configuration.GetConnectionString("DefaultConnection");
            PopulateCompaniescomboBox();

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
            var companies = repository.GetCompanies();
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
                    await repository.SaveDocumentsAsync(mappedDocuments);
                    //                    await repository.SaveDocumentsAsync((IEnumerable<DataAccess.Entities.Document>)documents);
                }

                // Display in DataGridView
                dataGridViewResult.DataSource = documents;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private IEnumerable<DataAccess.Entities.Document> MapDocuments(IEnumerable<APIServiceDoklado.Document> apiDocuments)
        {
            return apiDocuments.Select(apiDoc => new DataAccess.Entities.Document
            {
                ScanedForCompany = _dokladoService.CompayId,
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
            } else

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
                _dokladoService.CompayId = (string?)comboBoxCompany.SelectedValue;
            }
        }

        private async void btnGetFilenames_Click(object sender, EventArgs e)
        {
            // Configure DbContextOptions with the connection string
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(connectionString)
                .Options;

            // Create an instance of DocumentRepository
            using (var context = new AppDbContext(options))
            {
                var repository = new DataAccess.DocumentRepository(context);
                List<string> documentErpIds = repository.GetDocumentErpIdsFromDatabase();
                ICollection <GetAttachmentsV2SuccesfullResponse> attachmentPaths;
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
    }
}
