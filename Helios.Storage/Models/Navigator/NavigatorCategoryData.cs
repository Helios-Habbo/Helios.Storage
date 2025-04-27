using Helios.Storage.Models.Room;
using System.Collections.Generic;

namespace Helios.Storage.Models.Navigator
{
    public class NavigatorCategoryData
    {
        public virtual int Id { get; set; }
        public virtual string Caption { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual int MinimumRank { get; set; }

        #region Constraints

        public virtual List<RoomData> Rooms { get; set; }

        #endregion
    }
}
