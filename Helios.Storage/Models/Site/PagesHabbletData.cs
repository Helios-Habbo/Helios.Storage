namespace Helios.Storage.Models.Site
{
    public class PagesHabbletData
    {
        public virtual string Page { get; set; }
        public virtual int OrderId { get; set; }
        public virtual string Widget { get; set; }
        public virtual string Column { get; set; }
        public virtual bool Visible { get; set; }
    }

}
