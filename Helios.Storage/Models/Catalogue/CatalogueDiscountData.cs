using System;

namespace Helios.Storage.Models.Catalogue
{
    public class CatalogueDiscountData
    {
        public virtual int PageId { get; set; }
        public virtual int PurchaseLimit { get; set; }
        public virtual int DiscountBatchSize { get; set; }
        public virtual int DiscountAmountPerBatch { get; set; }
        public virtual int MinimumDiscountForBonus { get; set; }
        public virtual DateTime? ExpireDate { get; set; }
    }
}
