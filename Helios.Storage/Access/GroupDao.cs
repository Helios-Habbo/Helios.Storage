using Helios.Storage.Models.Item;
using System.Collections.Generic;
using System.Linq;

namespace Helios.Storage.Access
{
    public class GroupDao
    {
        /// <summary>
        /// Get list of all definition data
        /// </summary>
        public static List<ItemDefinitionData> GetDefinitions()
        {
            using (var context = new StorageContext())
            {
                return context.ItemDefinitionData.ToList();
            }

        }
    }
}
