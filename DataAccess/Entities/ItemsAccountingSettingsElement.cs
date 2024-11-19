using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Entities
{
    public class ItemsAccountingSettingsElement
    {
        public int Id { get; set; } // Primary Key
        public int ItemId { get; set; }

        public int? AccountingItem_AccountingSoftwareId { get; set; }
        public string? AccountingItem_Value { get; set; }
        public string? AccountingItem_Code { get; set; }
        public string? AccountingItem_ResourceType { get; set; }

        public int? NumericCode_AccountingSoftwareId { get; set; }
        public string? NumericCode_Value { get; set; }
        public string? NumericCode_Name { get; set; }
        public string? NumericCode_PaymentType { get; set; }

        public int? CashRegister_AccountingSoftwareId { get; set; }
        public string? CashRegister_Value { get; set; }
        public string? CashRegister_Name { get; set; }

        public int? Project_AccountingSoftwareId { get; set; }
        public string? Project_Value { get; set; }
        public string? Project_Code { get; set; }

        public int? Order_AccountingSoftwareId { get; set; }
        public string? Order_Value { get; set; }
        public string? Order_Code { get; set; }
        public string? Order_Name { get; set; }

        public int? Activity_AccountingSoftwareId { get; set; }
        public string? Activity_Value { get; set; }
        public string? Activity_Code { get; set; }

        public int? ExpenditureCenter_AccountingSoftwareId { get; set; }
        public string? ExpenditureCenter_Value { get; set; }
        public string? ExpenditureCenter_Code { get; set; }

        public int? GeneralDocumentAgenda_AccountingSoftwareId { get; set; }
        public string? GeneralDocumentAgenda_Name { get; set; }
        public string? GeneralDocumentAgenda_Code { get; set; }

        public int? AccountingSoftwareAgenda_AccountingSoftwareId { get; set; }
        public string? AccountingSoftwareAgenda_Name { get; set; }
        public string? AccountingSoftwareAgenda_Code { get; set; }

        public int? PredefinedNote_AccountingSoftwareId { get; set; }
        public string? PredefinedNote_Value { get; set; }

        public int? ClassificationVat_AccountingSoftwareId { get; set; }
        public string? ClassificationVat_Name { get; set; }
        public string? ClassificationVat_Code { get; set; }
        public string? ClassificationVat_ValidFrom { get; set; }
        public string? ClassificationVat_ValidTo { get; set; }
        public bool? ClassificationVat_isReverseCharge { get; set; }
    }
}
