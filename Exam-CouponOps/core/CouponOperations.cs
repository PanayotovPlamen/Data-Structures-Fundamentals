using System;
using System.Collections.Generic;
using System.Linq;

namespace CouponOps
{
    public class CouponOperations : ICouponOperations
    {
        private List<Coupon> coupons;
        private List<Website> websites;

        public CouponOperations()
        {
            this.coupons = new List<Coupon>();
            this.websites = new List<Website>();
        }

        public void RegisterSite(Website w)
        {
            if (this.websites.Contains(w))
            {
                throw new ArgumentException();
            }

            this.websites.Add(w);
        }

        public void AddCoupon(Website w, Coupon c)
        {
            if (!this.websites.Contains(w))
            {
                throw new ArgumentException();
            }
           
            c.Website = w;

            w.Coupons.Add(c);

            this.coupons.Add(c);
        }

        public Website RemoveWebsite(string domain)
        {
            var removed = this.websites.FirstOrDefault(x => x.Domain == domain);

            if (removed != null)
            {
                foreach (var item in removed.Coupons)
                {
                    this.coupons.Remove(item);
                }

                this.websites.Remove(removed);

                return removed;
            }

            throw new ArgumentException();
        }

        public Coupon RemoveCoupon(string code)
        {
            var couponToRemove = this.coupons.FirstOrDefault(x => x.Code == code);

            if (couponToRemove != null)
            {
                this.coupons.Remove(couponToRemove);

                foreach (var item in this.websites)
                {
                    if (item.Coupons.Contains(couponToRemove))
                    {
                        item.Coupons.Remove(couponToRemove);
                    }
                }

                return couponToRemove;
            }

            throw new ArgumentException();
        }

        public bool Exist(Website w)
        {
            return this.websites.Contains(w);
        }

        public bool Exist(Coupon c)
        {
            return this.coupons.Contains(c);
        }

        public IEnumerable<Website> GetSites()
        {
            return new List<Website>(this.websites);
        }

        public IEnumerable<Coupon> GetCouponsForWebsite(Website w)
        {
            if (!this.websites.Contains(w))
            {
                throw new ArgumentException();
            }

            var result = this.coupons.Where(x => x.Website.Equals(w)).ToList();

            return new List<Coupon>(result);
        }

        public void UseCoupon(Website w, Coupon c)
        {
            if (!this.websites.Contains(w) || !this.coupons.Contains(c))
            {
                throw new ArgumentException();
            }            

            if (!w.Coupons.Contains(c))
            {
                throw new ArgumentException();
            }

            w.Coupons.Remove(c);

            this.coupons.Remove(c);
        }

        public IEnumerable<Coupon> GetCouponsOrderedByValidityDescAndDiscountPercentageDesc()
        {
            var result = this.coupons.OrderByDescending(x => x.Validity).ThenByDescending(x => x.DiscountPercentage);

            return new List<Coupon>(result);
        }

        public IEnumerable<Website> GetWebsitesOrderedByUserCountAndCouponsCountDesc()
        {
            var result = this.websites.OrderByDescending(x => x.UsersCount).ThenByDescending(x => x.Coupons.Count);

            return new List<Website>(result);
        }
    }
}
