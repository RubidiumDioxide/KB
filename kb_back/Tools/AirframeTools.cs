using kb_back.Entities;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static kb_back.Tools.ArmamentTools;

namespace kb_back.Tools
{
    public static class AirframeTools
    {
        public class AirframeViewModel
        {
            public string Name { get; set; } = null!;
            public string? WingProfile { get; set; }
            public double? Length { get; set; }
            public double? Wingspan { get; set; }

            public List<string> GetValues()
            {
                List<string> l = new List<string> { Name, WingProfile, Length.ToString(), Wingspan.ToString() };
                return l;
            }
        }

        public static List<AirframeViewModel> LoadTable(KbDbContext db)
        {
            db.Airframes.Load();

            return db.Airframes.Local.ToBindingList().Select(a => new AirframeViewModel { Name = a.Name, WingProfile = a.WingProfile, Length = a.Length, Wingspan = a.Wingspan }).ToList();
        }

        /*public static void Add(KbDbContext db, List<string> Input)
        {
            db.Airframes.Load();

            Airframe airframe = new Airframe(Input);

            try
            {
                db.Airframes.Add(airframe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Airframes.Remove(airframe);
                throw new Exception(ex.Message);
            }
        }*/

        public static void Edit(KbDbContext db, string name, List<string> Input)
        {
            db.Airframes.Load();

            Airframe airframe = db.Airframes.Find(name);
            Airframe airframe_reserve = airframe; 

            if (airframe != null)
            {
                try
                {
                    airframe.Set(Input);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    airframe = airframe_reserve;
                    throw new Exception(ex.Message);
                }
            }
        }

        /*public static void Delete(KbDbContext db, string name)
        {
            db.Airframes.Load();

            Airframe airframe = db.Airframes.Find(name);

            if (airframe != null)
            {
                try
                {
                    db.Airframes.Remove(airframe);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Airframes.Add(airframe); 
                    throw new Exception(ex.Message);
                }
            }
        }*/

        public static List<AirframeViewModel> Search(KbDbContext db, List<string> Input)
        {
            db.Airframes.Load();

            var itemsSource = db.Airframes.Local.ToBindingList().Select(a => new AirframeViewModel { Name = a.Name, WingProfile = a.WingProfile, Length = a.Length, Wingspan = a.Wingspan });

            if (Input[0] != "" && Input[0] != "Name (string)")
            {
                try
                {
                    string name = Input[0];
                    itemsSource = itemsSource.Where(a => a.Name == name);
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "WingProfile (string)")
            {
                try
                {
                    string wingProfile = Input[1];
                    itemsSource = itemsSource.Where(a => a.WingProfile == wingProfile);
                }
                catch { }
            }

            if (Input[2] != "" && Input[2] != "Length (float)")
            {
                try
                {
                    double length = double.Parse(Input[2]);
                    itemsSource = itemsSource.Where(a => a.Length == length);
                }
                catch { }
            }

            if (Input[3] != "" && Input[3] != "Wingspan (float)")
            {
                try
                {
                    double wingspan = double.Parse(Input[3]);
                    itemsSource = itemsSource.Where(a => a.Wingspan == wingspan);
                }
                catch { }
            }

            return itemsSource.ToList();
        }
    }
}
