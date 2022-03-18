namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;

    public class Inventory : IHolder
    {

        private List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }

        public int Capacity => this.weapons.Count();

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.weapons.Contains(weapon);
        }

        public void EmptyArsenal(Category category)
        {
            var weaponsToEmpty = this.weapons.Where(x => x.Category == category).ToList();

            foreach (var item in weaponsToEmpty)
            {
                item.Ammunition = 0;
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            if (!this.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            var currentweapon = this.GetById(weapon.Id);

            if (ammunition <= currentweapon.Ammunition)
            {
                currentweapon.Ammunition -= ammunition;

                return true;
            }

            return false;
        }

        public IWeapon GetById(int id)
        {
            foreach (var item in weapons)
            {
                if (item.Id == id)
                {
                    return item;
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this.weapons.GetEnumerator();
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            if (!this.Contains(weapon))
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            var currentweapon = this.GetById(weapon.Id);

            if ((currentweapon.Ammunition + ammunition) <= currentweapon.MaxCapacity)
            {
                currentweapon.Ammunition += ammunition;
            }

            return currentweapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            IWeapon searched = null;

            for (int i = 0; i < this.Capacity; i++)
            {
                if (this.weapons[i].Id == id)
                {
                    searched = this.weapons[i];
                    this.weapons.RemoveAt(i);
                    break;
                }
            }

            if (searched == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            return searched;
        }

        public int RemoveHeavy()
        {
            return this.weapons.RemoveAll(w => w.Category == Category.Heavy);
        }

        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this.weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            var weapons =  this.weapons.Where(x => (int)x.Category >= (int)lower && (int)x.Category <= (int)upper).ToList();

            if (weapons == null)
            {
                return new List<IWeapon>();
            }

            return weapons;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int indexOfFirst = this.weapons.IndexOf(firstWeapon);            
            int indexOfSecond = this.weapons.IndexOf(secondWeapon);

            if (indexOfFirst < 0 || indexOfSecond < 0)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            if (firstWeapon.Category == secondWeapon.Category)
            {
                this.weapons[indexOfFirst] = secondWeapon;
                this.weapons[indexOfSecond] = firstWeapon;
            }
        }
    }
}
