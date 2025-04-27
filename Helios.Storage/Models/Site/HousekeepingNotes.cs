using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;
using Helios.Storage.Models.Avatar;

namespace Helios.Storage.Models.Site
{
    public class HousekeepingNotes
    {
        public Guid Id { get; set; }
        public int AvatarId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;

        public AvatarData AvatarData { get; set; }
    }

}
