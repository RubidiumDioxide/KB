using kb_back.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb_back.Tools
{
    public static class ArmamentTools
    {
        public class ArmamentViewModel
        {
            public string Name { get; set; }
            public double Caliber {  get; set; }
            public double FiringRate {  get; set; }
            public double Weight {  get; set; } 

            public List<string> GetValues()
            {
                List<string> l = new List<string> {Name, Caliber.ToString(), FiringRate.ToString(), Weight.ToString()};
                return l;
            }
        }

        public static List<ArmamentViewModel> LoadTable(KbDbContext db)
        {
            db.Armaments.Load();

            return db.Armaments.Local.ToBindingList().Select(a => new ArmamentViewModel { Name = a.Name, Caliber = a.Caliber, FiringRate = a.FiringRate, Weight = a.Weight }).ToList();
        }

        public static void Add(KbDbContext db, List<string> Input)
        {
            db.Armaments.Load();

            db.Armaments.Add(new Armament
                {
                    Name = Input[0],
                    Caliber = double.Parse(Input[1]),
                    FiringRate = double.Parse(Input[2]),
                    Weight = double.Parse(Input[3])
                });

            try
            {
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void Edit(KbDbContext db, string name,List<string> Input)
        {
            db.Armaments.Load();

            Armament armament = db.Armaments.Find(name);

            if (armament != null)
            {
                armament.Caliber = double.Parse(Input[1]);
                armament.FiringRate = double.Parse(Input[2]);
                armament.Weight = double.Parse(Input[3]);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static void Delete(KbDbContext db, string name)
        {
            db.Armaments.Load();

            Armament armament = db.Armaments.Find(name);

            if (armament != null)
            {
                db.Armaments.Remove(armament);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        public static List<ArmamentViewModel> Search(KbDbContext db, List <string> Input)
        {
            db.Armaments.Load();

            var itemsSource = db.Armaments.Local.ToBindingList().Select(a => new ArmamentViewModel { Name = a.Name, Caliber = a.Caliber, FiringRate = a.FiringRate, Weight = a.Weight });

             try
             {
                string name = Input[0];
                itemsSource = itemsSource.Where(a => a.Name == name);
             }
             catch { }

            try
            {
                double caliber = double.Parse(Input[1]);
                itemsSource = itemsSource.Where(a => a.Caliber == caliber);
            }
            catch { }

            try
            {
                double firingRate = double.Parse(Input[2]);
                itemsSource = itemsSource.Where(a => a.FiringRate == firingRate);
            }
            catch { }

            try
            {
                double weight = double.Parse(Input[3]);
                itemsSource = itemsSource.Where(a => a.Weight == weight);
            }
            catch { }

            return itemsSource.ToList();
        }
    }
}