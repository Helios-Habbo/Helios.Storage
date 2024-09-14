using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Helios.Storage.Models.Subscription
{
    public class SubscriptionData
    {
        public virtual int AvatarId { get; set; }
        public virtual DateTime SubscribedDate { get; set; }
        public virtual DateTime ExpireDate { get; set; }
        public virtual int GiftsRedeemable { get; set; }
        public virtual DateTime GiftDate { get; set; }
        public virtual long SubscriptionAge { get; set; }
        public virtual DateTime SubscriptionAgeLastUpdated { get; set; }

        public virtual int MonthsLeft
        {
            get
            {
                if (DateTime.Now > ExpireDate)
                    return 0;

                DateTime now = DateTime.Now;
                DateTime expireDate = ExpireDate;

                int months = (expireDate.Year - now.Year) * 12 + expireDate.Month - now.Month;

                if (expireDate.Day < now.Day)
                {
                    months--;
                }

                return months;
            }
        }

        public virtual int DaysLeft
        {
            get
            {
                if (DateTime.Now > ExpireDate)
                    return 0;

                DateTime now = DateTime.Now;
                DateTime expireDate = ExpireDate;

                // Calculate total days between now and expireDate
                int totalDays = (expireDate - now).Days;

                // Calculate the number of months remaining
                int months = (expireDate.Year - now.Year) * 12 + expireDate.Month - now.Month;

                if (expireDate.Day < now.Day)
                {
                    months--;
                }

                // Calculate the number of days left after accounting for full months
                DateTime firstOfNextMonth = now.AddMonths(months + 1).Date;
                int daysInNextMonth = (expireDate - firstOfNextMonth).Days;

                return totalDays - (months * 30) + daysInNextMonth; // Adjust the days calculation
            }
        }
    }
}
