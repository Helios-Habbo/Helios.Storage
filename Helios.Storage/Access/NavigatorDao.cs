using System.Collections.Generic;
using System.Linq;
using Helios.Storage.Models.Navigator;
using Microsoft.EntityFrameworkCore;

namespace Helios.Storage.Access
{
    public static class NavigatorDao
    {
        /// <summary>
        /// Get list of public items
        /// </summary>
        public static List<PublicItemData> GetPublicItems(this StorageContext context)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<PublicItemData>().List() as List<PublicItemData>;//.Where(x => x.Room != null).ToList();
            //}

            var publicItems = context.PublicItemData
                .Include(x => x.Room).ThenInclude(x => x.Category)
                .Include(x => x.Room).ThenInclude(x => x.OwnerData)
                .ToList();

            foreach (var publicItem in publicItems)
            {
                publicItem.OrderId = publicItem.ParentId > 0 ? publicItems.FirstOrDefault(x => x.ParentId == publicItem.ParentId).OrderId + 1 : publicItem.OrderId;
            }

            return publicItems.OrderBy(x => x.OrderId).ToList();
        }

        /// <summary>
        /// Get list of room categories
        /// </summary>
        public static List<NavigatorCategoryData> GetCategories(this StorageContext context)
        {
            //using (var session = SessionFactoryBuilder.Instance.SessionFactory.OpenSession())
            //{
            //    return session.QueryOver<NavigatorCategoryData>().List() as List<NavigatorCategoryData>;
            //}

            return context.NavigatorCategoryData.ToList();
        }
    }
}