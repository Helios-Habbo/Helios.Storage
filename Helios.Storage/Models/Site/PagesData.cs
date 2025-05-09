﻿namespace Helios.Storage.Models.Site
{
    public class PagesData
    {
        public virtual int Id { get; set; }
        public virtual int ParentId { get; set; }
        public virtual int OrderId { get; set; }
        public virtual string Label { get; set; }
        public virtual string Link { get; set; }
        public virtual string Page { get; set; }
        public virtual PageColor Colour { get; set; }
        public virtual int MinimumRank { get; set; }
        public virtual bool RequiresLogin { get; set; }
        public virtual bool RequiresLogout { get; set; }
    }

    public enum PageColor
    {
        GREEN,
        BLUE
    }
}
