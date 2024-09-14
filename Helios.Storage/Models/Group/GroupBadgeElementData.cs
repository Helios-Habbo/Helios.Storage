using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.Design;

namespace Helios.Storage.Models.Group
{
    public class GroupBadgeElementData
    {
        public int Id { get; set; }
        public string FirstValue { get; set; }
        public string SecondValue { get; set; }
        public string Type { get; set; }
        public bool Enabled { get; set; }

    }
}
