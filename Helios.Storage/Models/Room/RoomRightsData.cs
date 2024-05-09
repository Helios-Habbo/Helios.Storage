using Helios.Storage.Models.Avatar;

namespace Helios.Storage.Models.Room
{
    public class RoomRightsData
    {
        public virtual int AvatarId { get; set; }
        public virtual int RoomId { get; set; }


        #region Constraints

        public virtual RoomData RoomData { get; set; }
        public virtual AvatarData AvatarData { get; set; }

        #endregion
    }
}
