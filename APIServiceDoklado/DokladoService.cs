using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using APIServiceDoklado;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace APIServiceDoklado
{
    public class DokladoService
    {
        private readonly HttpClient _httpClient;
        private readonly DokladoApiSchemaClient _apiClient;
        public string? ApiKey { get; set; }
        public string? CompanyId { get; set; }

        public DokladoService()
        {
            // Create HttpClient instance
            //            var httpClient = new HttpClient();

            // Configure JSON settings globally
            JsonSerializationConfig.ConfigureGlobalSettings();

            // Create HttpClient with LoggingHandler
            _httpClient = new HttpClient(new LoggingHandler(new HttpClientHandler()));

            // Retrieve API key from environment variable
            if (ApiKey == null) ApiKey = Environment.GetEnvironmentVariable("DOKLADO_API_KEY");
            //if (string.IsNullOrEmpty(ApiKey))
            //{
            if (ApiKey == null) ApiKey = "ebt5bhbh98c-2a4ta3-4ucq83-9ovrb4-fb99l4aqbr-6bbqbdb";
                //ApiKey = "1951a88eaa9-m9jgag-4s6m9n-9b5c8m-vbm85o81as-0687b6b";

            //}

            // Add the Authorization header with the API key
            if (!string.IsNullOrEmpty(ApiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {ApiKey}");
                _httpClient.DefaultRequestHeaders.Add("api_key", ApiKey);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            }

            // Initialize API client with HttpClient
            _apiClient = new DokladoApiSchemaClient(_httpClient);
        }

        public async Task<List<Document>> GetReceiptsAsync(DateTime fromDate)
        {
            var allReceipts = new List<Document>();
            string? continuationToken = null;

            do
            {
                // Fetch receipts with pagination

                // Fetch the result once and use it for both Receipts and ContinuationToken
                var result = await FetchReceiptsPageAsync(fromDate, continuationToken);

                // Append to the list
                allReceipts.AddRange(result.Receipts);

                // Update continuationToken for next page
                continuationToken = result.ContinuationToken;
                // continuationToken = null;

            } while (!string.IsNullOrEmpty(continuationToken));



            return allReceipts;
        }

        public async Task<ICollection<GetAttachmentsV2SuccesfullResponse>> GetFilePath(string documentId)
        {
            // Make the API call with the necessary headers and parameters
            // Construct the request payload

            InputDataGetAttachmentsV2 request = new InputDataGetAttachmentsV2
            {
                Data = new InputParametersGetAttachmentsV2
                {

                    RequestDocumentsAttachments = new List<PublicRequestDocumentAttachments>
                    {
                        new PublicRequestDocumentAttachments
                        {
                            DocumentId = documentId,
                            DocumentType = RequestDocumentType.Expense
                        }
                    }
                }
                
            };

            var response = await _apiClient.GetAsync(request);


            return response.Data;
        }

        private async Task<(IEnumerable<Document> Receipts, string ContinuationToken)> FetchReceiptsPageAsync(DateTime fromDate, string? continuationToken)
        {
            // Make the API call with the necessary headers and parameters

            /*
                        // Construct the request payload
                        var request = new InputData
                        {
                            Data = new InputParameters
                            {
                                //OrganizationId = "36206075",
                                OrganizationId = CompanyId,
                                IsExported = false,
                                DateFrom = fromDate, // Force UTC
                                DateType = InputParametersDateType.Create,   // Optional sorting
                                ContinuationToken = continuationToken
                            }
                        };
            */

            var request = new InputData
            {
                Data = new InputParameters
                {
                    OrganizationId = CompanyId,
                    IsExported = false,
                    DateFrom = fromDate,
                    DateType = InputParametersDateType.Create,

                    // New API format: send the token inside searchAfter array
                    SearchAfter = string.IsNullOrWhiteSpace(continuationToken)
            ? null
            : new List<string> { continuationToken }
                }
            };

            // Serialize the request object to JSON and log it
            string serializedRequest = JsonConvert.SerializeObject(request, Formatting.Indented);
            Console.WriteLine("Request Body:");
            Console.WriteLine(serializedRequest);

            var response = await _apiClient.DocumentsAsync(request);

            // Assuming the API client returns data and a continuation token
            var responseData = response.Data.ToList();
            var documents = response.Data.Select(MapToDocument).ToList();
            return (documents, response.ContinuationToken);
        }

        public static Document MapToDocument(Data2 data)
        {
            Document document = new()
            {
/*
                DocumentId = data.AdditionalProperties.TryGetValue("documentId", out var documentId) ? documentId.ToString() : null,
                Type = data.AdditionalProperties.TryGetValue("type", out var type) ? type.ToString() : null,
                ReceiptUID = data.AdditionalProperties.TryGetValue("receiptUID", out var receiptUID) ? receiptUID.ToString() : null,
                IssuedAt = data.AdditionalProperties.TryGetValue("issuedAt", out var issuedAt) && DateTime.TryParse(issuedAt.ToString(), out var issuedAtDate) ? issuedAtDate : default,
                CreatedAt = data.AdditionalProperties.TryGetValue("createdAt", out var createdAt) && DateTime.TryParse(createdAt.ToString(), out var createdAtDate) ? createdAtDate : default,
                Currency = data.AdditionalProperties.TryGetValue("currency", out var currency) ? currency.ToString() : null,
                VatRateBasic = data.AdditionalProperties.TryGetValue("vatRateBasic", out var vatRateBasic) && decimal.TryParse(vatRateBasic.ToString(), out var vatRateBasicDecimal) ? vatRateBasicDecimal : 0,
                TaxBaseBasic = data.AdditionalProperties.TryGetValue("taxBaseBasic", out var taxBaseBasic) && decimal.TryParse(taxBaseBasic.ToString(), out var taxBaseBasicDecimal) ? taxBaseBasicDecimal : 0,
                VatAmountBasic = data.AdditionalProperties.TryGetValue("vatAmountBasic", out var vatAmountBasic) && decimal.TryParse(vatAmountBasic.ToString(), out var vatAmountBasicDecimal) ? vatAmountBasicDecimal : 0,
                VatRateReduced = data.AdditionalProperties.TryGetValue("vatRateReduced", out var vatRateReduced) && decimal.TryParse(vatRateReduced.ToString(), out var vatRateReducedDecimal) ? vatRateReducedDecimal : 0,
                TaxBaseReduced = data.AdditionalProperties.TryGetValue("taxBaseReduced", out var taxBaseReduced) && decimal.TryParse(taxBaseReduced.ToString(), out var taxBaseReducedDecimal) ? taxBaseReducedDecimal : 0,
                VatAmountReduced = data.AdditionalProperties.TryGetValue("vatAmountReduced", out var vatAmountReduced) && decimal.TryParse(vatAmountReduced.ToString(), out var vatAmountReducedDecimal) ? vatAmountReducedDecimal : 0,
                VatRateTaxFree = data.AdditionalProperties.TryGetValue("vatRateTaxFree", out var vatRateTaxFree) && decimal.TryParse(vatRateTaxFree.ToString(), out var vatRateTaxFreeDecimal) ? vatRateTaxFreeDecimal : 0,
                FreeTaxAmount = data.AdditionalProperties.TryGetValue("freeTaxAmount", out var freeTaxAmount) && decimal.TryParse(freeTaxAmount.ToString(), out var freeTaxAmountDecimal) ? freeTaxAmountDecimal : 0,
                VatAmountTaxFree = data.AdditionalProperties.TryGetValue("vatAmountTaxFree", out var vatAmountTaxFree) && decimal.TryParse(vatAmountTaxFree.ToString(), out var vatAmountTaxFreeDecimal) ? vatAmountTaxFreeDecimal : 0,
                TotalPrice = data.AdditionalProperties.TryGetValue("totalPrice", out var totalPrice) && decimal.TryParse(totalPrice.ToString(), out var totalPriceDecimal) ? totalPriceDecimal : 0,
                Value = data.AdditionalProperties.TryGetValue("value", out var value) ? value.ToString() : null,
                AccountingCode = data.AdditionalProperties.TryGetValue("accountingCode", out var accountingCode) ? accountingCode.ToString() : null,
                OrganizationId = data.AdditionalProperties.TryGetValue("organizationId", out var organizationId) ? organizationId.ToString() : null,
                OrganizationVAT = data.AdditionalProperties.TryGetValue("organizationVAT", out var organizationVAT) ? organizationVAT.ToString() : null,
                OrganizationVatId = data.AdditionalProperties.TryGetValue("organizationVatId", out var organizationVatId) ? organizationVatId.ToString() : null,
                OrganizationTaxId = data.AdditionalProperties.TryGetValue("organizationTaxId", out var organizationTaxId) ? organizationTaxId.ToString() : null,
                OrganizationName = data.AdditionalProperties.TryGetValue("organizationName", out var organizationName) ? organizationName.ToString() : null,
                StreetName = data.AdditionalProperties.TryGetValue("streetName", out var streetName) ? streetName.ToString() : null,
                Municipality = data.AdditionalProperties.TryGetValue("municipality", out var municipality) ? municipality.ToString() : null,
                PostalCode = data.AdditionalProperties.TryGetValue("postalCode", out var postalCode) ? postalCode.ToString() : null,
                Email = data.AdditionalProperties.TryGetValue("email", out var email) ? email.ToString() : null,
                PaymentType = data.AdditionalProperties.TryGetValue("paymentType", out var paymentType) ? paymentType.ToString() : null,
                InvoiceNumber = data.AdditionalProperties.TryGetValue("invoiceNumber", out var invoiceNumber) ? invoiceNumber.ToString() : null,
*/
                AccountingCode = data.AdditionalProperties.TryGetValue("accountingCode", out var accountingCode) ? accountingCode.ToString() : null,
                CreatedAt = data.AdditionalProperties.TryGetValue("createdAt", out var createdAt) && DateTime.TryParse(createdAt.ToString(), out var createdAtDate) ? createdAtDate : default,
                Currency = data.AdditionalProperties.TryGetValue("currency", out var currency) ? currency.ToString() : null,
                DocumentId = data.AdditionalProperties.TryGetValue("documentId", out var documentId) ? documentId.ToString() : null,
                Email = data.AdditionalProperties.TryGetValue("email", out var email) ? email.ToString() : null,
                FreeTaxAmount = data.AdditionalProperties.TryGetValue("freeTaxAmount", out var freeTaxAmount) && decimal.TryParse(freeTaxAmount.ToString(), out var freeTaxAmountDecimal) ? freeTaxAmountDecimal : 0,
                InvoiceNumber = data.AdditionalProperties.TryGetValue("invoiceNumber", out var invoiceNumber) ? invoiceNumber.ToString() : null,
                IssuedAt = data.AdditionalProperties.TryGetValue("issuedAt", out var issuedAt) && DateTime.TryParse(issuedAt.ToString(), out var issuedAtDate) ? issuedAtDate : default,
                Municipality = data.AdditionalProperties.TryGetValue("municipality", out var municipality) ? municipality.ToString() : null,
                OrganizationId = data.AdditionalProperties.TryGetValue("organizationId", out var organizationId) ? organizationId.ToString() : null,
                OrganizationName = data.AdditionalProperties.TryGetValue("organizationName", out var organizationName) ? organizationName.ToString() : null,
                OrganizationTaxId = data.AdditionalProperties.TryGetValue("organizationTaxId", out var organizationTaxId) ? organizationTaxId.ToString() : null,
                OrganizationVAT = data.AdditionalProperties.TryGetValue("organizationVAT", out var organizationVAT) ? organizationVAT.ToString() : null,
                OrganizationVatId = data.AdditionalProperties.TryGetValue("organizationVatId", out var organizationVatId) ? organizationVatId.ToString() : null,
                PaymentType = data.AdditionalProperties.TryGetValue("paymentType", out var paymentType) ? paymentType.ToString() : null,
                PostalCode = data.AdditionalProperties.TryGetValue("postalCode", out var postalCode) ? postalCode.ToString() : null,
                ReceiptUID = data.AdditionalProperties.TryGetValue("receiptUID", out var receiptUID) ? receiptUID.ToString() : null,
                StreetName = data.AdditionalProperties.TryGetValue("streetName", out var streetName) ? streetName.ToString() : null,
                TaxBaseBasic = data.AdditionalProperties.TryGetValue("taxBaseBasic", out var taxBaseBasic) && decimal.TryParse(taxBaseBasic.ToString(), out var taxBaseBasicDecimal) ? taxBaseBasicDecimal : 0,
                TaxBaseReduced = data.AdditionalProperties.TryGetValue("taxBaseReduced", out var taxBaseReduced) && decimal.TryParse(taxBaseReduced.ToString(), out var taxBaseReducedDecimal) ? taxBaseReducedDecimal : 0,
                TotalPrice = data.AdditionalProperties.TryGetValue("totalPrice", out var totalPrice) && decimal.TryParse(totalPrice.ToString(), out var totalPriceDecimal) ? totalPriceDecimal : 0,
                Type = data.AdditionalProperties.TryGetValue("type", out var type) ? type.ToString() : null,
                Value = data.AdditionalProperties.TryGetValue("value", out var value) ? value.ToString() : null,
                VatAmountBasic = data.AdditionalProperties.TryGetValue("vatAmountBasic", out var vatAmountBasic) && decimal.TryParse(vatAmountBasic.ToString(), out var vatAmountBasicDecimal) ? vatAmountBasicDecimal : 0,
                VatAmountReduced = data.AdditionalProperties.TryGetValue("vatAmountReduced", out var vatAmountReduced) && decimal.TryParse(vatAmountReduced.ToString(), out var vatAmountReducedDecimal) ? vatAmountReducedDecimal : 0,
                VatAmountTaxFree = data.AdditionalProperties.TryGetValue("vatAmountTaxFree", out var vatAmountTaxFree) && decimal.TryParse(vatAmountTaxFree.ToString(), out var vatAmountTaxFreeDecimal) ? vatAmountTaxFreeDecimal : 0,
                VatRateBasic = data.AdditionalProperties.TryGetValue("vatRateBasic", out var vatRateBasic) && decimal.TryParse(vatRateBasic.ToString(), out var vatRateBasicDecimal) ? vatRateBasicDecimal : 0,
                VatRateReduced = data.AdditionalProperties.TryGetValue("vatRateReduced", out var vatRateReduced) && decimal.TryParse(vatRateReduced.ToString(), out var vatRateReducedDecimal) ? vatRateReducedDecimal : 0,
                VatRateTaxFree = data.AdditionalProperties.TryGetValue("vatRateTaxFree", out var vatRateTaxFree) && decimal.TryParse(vatRateTaxFree.ToString(), out var vatRateTaxFreeDecimal) ? vatRateTaxFreeDecimal : 0,

                SubType = data.AdditionalProperties.TryGetValue("subType", out var subType) ? subType.ToString() : null,
                TaxPointDate = data.AdditionalProperties.TryGetValue("taxPointDate", out var taxPointDate) && DateTime.TryParse(taxPointDate.ToString(), out var TaxPointDateD) ? TaxPointDateD : default,
                VatCategory = data.AdditionalProperties.TryGetValue("vatCategory", out var vatCategory) ? vatCategory.ToString() : null,
                Note = data.AdditionalProperties.TryGetValue("note", out var note) ? note.ToString() : null,
                CustomText = data.AdditionalProperties.TryGetValue("customText", out var customText) ? customText.ToString() : null,
                OtherCurrency = data.AdditionalProperties.TryGetValue("otherCurrency", out var otherCurrency) ? otherCurrency.ToString() : null,
                OtherTotalPrice = data.AdditionalProperties.TryGetValue("otherTotalPrice", out var otherTotalPrice) && decimal.TryParse(otherTotalPrice.ToString(), out var otherTotalPriceDecimal) ? otherTotalPriceDecimal : 0

            };

            // Extract the `address` object
            if (data.AdditionalProperties.TryGetValue("address", out var addressObj) && addressObj is Newtonsoft.Json.Linq.JObject addressJObject)
            {
                document.Address = MapToAddress(addressJObject);
            }

            // Map AccountingSettings
            if (data.AdditionalProperties.TryGetValue("accountingSettings", out var accountingSettingsObj) && accountingSettingsObj is Newtonsoft.Json.Linq.JObject accountingSettingsJObject)
            {
                document.AccountingSettings = MapAccountingSettings(accountingSettingsJObject);
            }
/*
            // First, extract the `accountingSettings` object
            if (data.AdditionalProperties.TryGetValue("accountingSettings", out var accountingSettingsObj)
                && accountingSettingsObj is Newtonsoft.Json.Linq.JObject accountingSettingsJObject)
            {
                // Ensure AccountingSettings is initialized
                document.AccountingSettings ??= new AccountingSettings();

                // Iterate over each key in accountingSettingsKeys
                foreach (var key in accountingSettingsKeys)
                {
                    if (accountingSettingsJObject.TryGetValue(key.Key, out var accountingObj)
                        && accountingObj is Newtonsoft.Json.Linq.JObject accountingJObject)
                    {
                        var mappedElement = MapAccountingSettingsElement(accountingJObject, key.Value);

                        // Assign the mapped element to the correct property of `document.AccountingSettings`
                        switch (key.Value)
                        {
                            case AccountingSettingsElementType.AccountingItem:
                                document.AccountingSettings.AccountingItem = mappedElement;
                                break;
                            case AccountingSettingsElementType.NumericCode:
                                document.AccountingSettings.NumericCode = mappedElement;
                                break;
                            case AccountingSettingsElementType.CashRegister:
                                document.AccountingSettings.CashRegister = mappedElement;
                                break;
                            case AccountingSettingsElementType.ExpenditureCenter:
                                document.AccountingSettings.ExpenditureCenter = mappedElement;
                                break;
                            case AccountingSettingsElementType.Project:
                                document.AccountingSettings.Projects = mappedElement;
                                break;
                            case AccountingSettingsElementType.Order:
                                document.AccountingSettings.Order = mappedElement;
                                break;
                            case AccountingSettingsElementType.Activity:
                                document.AccountingSettings.Activity = mappedElement;
                                break;
                            case AccountingSettingsElementType.AccountingSoftwareAgenda:
                                document.AccountingSettings.AccountingSoftwareAgenda = mappedElement;
                                break;
                            case AccountingSettingsElementType.ClassificationVat:
                                document.AccountingSettings.ClassificationVat = mappedElement;
                                break;
                            default:
                                // Handle unexpected cases or ignore
                                break;
                        }
                    }
                }
            }
*/
            // Extract and map items array
            if (data.AdditionalProperties.TryGetValue("items", out var itemsObj) && itemsObj is Newtonsoft.Json.Linq.JArray itemsArray)
            {
                document.Items = itemsArray
                    .Select(item => item is Newtonsoft.Json.Linq.JObject itemObject ? MapToExportItem(itemObject) : null)
                    .Where(item => item != null)
                    .ToList();
            }

            // Map vatSummary array
            if (data.AdditionalProperties.TryGetValue("vatSummary", out var vatSummaryObj) && vatSummaryObj is Newtonsoft.Json.Linq.JArray vatSummaryArray)
            {
                document.VatSummary = vatSummaryArray
                    .Select(vat => vat.ToObject<CustomVatInterface>())
                    .Where(vat => vat != null)
                    .ToList();
            }

            return document;
        }

        public static ExportItem MapToExportItem(Newtonsoft.Json.Linq.JObject data)
        {
            return new ExportItem
            {
                Name = data["name"]?.ToString(),
                Price = decimal.TryParse(data["price"]?.ToString(), out var price) ? price : 0,
                Quantity = decimal.TryParse(data["quantity"]?.ToString(), out var quantity) ? quantity : 0,
                VatRate = decimal.TryParse(data["vatRate"]?.ToString(), out var vatRate) ? vatRate : 0,
                ItemType = data["itemType"]?.ToString(),
                Unit = data["unit"]?.ToString(),
                VatAmount = decimal.TryParse(data["vatAmount"]?.ToString(), out var vatAmount) ? vatAmount : (decimal?)null,
                AccountingSettings = data["accountingSettings"] is Newtonsoft.Json.Linq.JObject accountingSettingsObject
                    ? MapAccountingSettings(accountingSettingsObject)
                    : null
            };
        }

        public static Address MapToAddress(Newtonsoft.Json.Linq.JObject data)
        {
            return new Address
            {
                BuildingNumber = data["buildingNumber"]?.ToString(),
                Country = data["country"]?.ToString(),
                Municipality = data["municipality"]?.ToString(),
                PostalCode = data["postalCode"]?.ToString(),
                PropertyRegistrationNumber = data["propertyRegistrationNumber"]?.ToString(),
                StreetName = data["streetName"]?.ToString()
            };
        }

        public static AccountingSettings MapAccountingSettings(Newtonsoft.Json.Linq.JObject data)
        {
            AccountingSettings ret = new()
            {
                AccountingItem_AccountingSoftwareId = int.TryParse(data["accountingItem"]?["accountingSoftwareId"]?.ToString(), out var accountingItem_AccountingSoftwareId) ? accountingItem_AccountingSoftwareId : default,
                AccountingItem_Value = data["accountingItem"]?["value"]?.ToString(),
                AccountingItem_Code = data["accountingItem"]?["code"]?.ToString(),
                AccountingItem_ResourceType = data["accountingItem"]?["resourceType"]?.ToString(),

                NumericCode_AccountingSoftwareId = int.TryParse(data["numericCode"]?["accountingSoftwareId"]?.ToString(), out var numericCode_AccountingSoftwareId) ? numericCode_AccountingSoftwareId : default,
                NumericCode_Value = data["numericCode"]?["value"]?.ToString(),
                NumericCode_Name = data["numericCode"]?["name"]?.ToString(),
                NumericCode_PaymentType = data["numericCode"]?["paymentType"]?.ToString(),

                CashRegister_AccountingSoftwareId = int.TryParse(data["cashRegister"]?["accountingSoftwareId"]?.ToString(), out var cashRegister_AccountingSoftwareId) ? cashRegister_AccountingSoftwareId : default,
                CashRegister_Value = data["cashRegister"]?["value"]?.ToString(),
                CashRegister_Name = data["cashRegister"]?["name"]?.ToString(),

                Project_AccountingSoftwareId = int.TryParse(data["project"]?["accountingSoftwareId"]?.ToString(), out var project_AccountingSoftwareId) ? project_AccountingSoftwareId : default,
                Project_Value = data["project"]?["value"]?.ToString(),
                Project_Note = data["project"]?["note"]?.ToString(),

                Order_AccountingSoftwareId = int.TryParse(data["order"]?["accountingSoftwareId"]?.ToString(), out var order_AccountingSoftwareId) ? order_AccountingSoftwareId : default,
                Order_Value = data["order"]?["value"]?.ToString(),
                Order_Code = data["order"]?["code"]?.ToString(),

                Activity_AccountingSoftwareId = int.TryParse(data["activity"]?["accountingSoftwareId"]?.ToString(), out var activity_AccountingSoftwareId) ? activity_AccountingSoftwareId : default,
                Activity_Value = data["activity"]?["value"]?.ToString(),
                Activity_Code = data["activity"]?["code"]?.ToString(),

                ExpenditureCenter_AccountingSoftwareId = int.TryParse(data["expenditureCenter"]?["accountingSoftwareId"]?.ToString(), out var expenditureCenter_AccountingSoftwareId) ? expenditureCenter_AccountingSoftwareId : default,
                ExpenditureCenter_Value = data["expenditureCenter"]?["value"]?.ToString(),
                ExpenditureCenter_Code = data["expenditureCenter"]?["code"]?.ToString(),

                GeneralDocumentAgenda_AccountingSoftwareId = int.TryParse(data["generalDocumentAgenda"]?["accountingSoftwareId"]?.ToString(), out var generalDocumentAgenda_AccountingSoftwareId) ? generalDocumentAgenda_AccountingSoftwareId : default,
                GeneralDocumentAgenda_Name = data["generalDocumentAgenda"]?["name"]?.ToString(),
                GeneralDocumentAgenda_Code = data["generalDocumentAgenda"]?["code"]?.ToString(),

                AcountingSoftwareAgenda_AccountingSoftwareId = int.TryParse(data["accountingSoftwareAgenda"]?["accountingSoftwareId"]?.ToString(), out var acountingSoftwareAgenda_AccountingSoftwareId) ? acountingSoftwareAgenda_AccountingSoftwareId : default,
                AccountingSoftwareAgenda_Name = data["accountingSoftwareAgenda"]?["name"]?.ToString(),
                AccountingSoftwareAgenda_Code = data["accountingSoftwareAgenda"]?["code"]?.ToString(),

                PredefinedNote_AccountingSoftwareId = int.TryParse(data["predefinedNote"]?["accountingSoftwareId"]?.ToString(), out var predefinedNote_AccountingSoftwareId) ? predefinedNote_AccountingSoftwareId : default,
                PredefinedNote_Value = data["predefinedNote"]?["value"]?.ToString(),

                ClassificationVat_AccountingSoftwareId = int.TryParse(data["classificationVat"]?["accountingSoftwareId"]?.ToString(), out var classificationVat_AccountingSoftwareId) ? classificationVat_AccountingSoftwareId : default,
                ClassificationVat_Name = data["classificationVat"]?["name"]?.ToString(),
                ClassificationVat_Code = data["classificationVat"]?["code"]?.ToString(),
                ClassificationVat_ValidFrom = data["classificationVat"]?["validFrom"]?.ToString(),
                ClassificationVat_ValidTo = data["classificationVat"]?["validTo"]?.ToString()
            };
            return ret;
        }
        public static AccountingSettingsElement MapAccountingSettingsElement(Newtonsoft.Json.Linq.JObject data, string elementType)
        {
            return new AccountingSettingsElement
            {
                AccountingSoftwareId = int.TryParse(data["accountingSoftwareId"]?.ToString(), out var accountingSoftwareId) ? accountingSoftwareId : default,
                Code = data["code"]?.ToString(),
                Value = data["value"]?.ToString(),
                ElementType = elementType
            };
        }

        public static AccountingSettingsElement MapAccountingSettingsElement(Newtonsoft.Json.Linq.JObject data, AccountingSettingsElementType elementType)
        {
            return new AccountingSettingsElement
            {
                AccountingSoftwareId = int.TryParse(data["accountingSoftwareId"]?.ToString(), out var accountingSoftwareId) ? accountingSoftwareId : default,
                Code = data["code"]?.ToString(),
                Value = data["value"]?.ToString(),
                ElementType = elementType.ToString()
            };
        }


    }

}
