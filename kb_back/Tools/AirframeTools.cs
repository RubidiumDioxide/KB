using Microsoft.EntityFrameworkCore;
//using kb_back.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static kb_back.Tools.ArmamentTools;
using static kb_back.Tools.EngineTools;

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
                    itemsSource = itemsSource.Where(a => a.Name.Contains(name));
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "WingProfile (string)")
            {
                try
                {
                    string wingProfile = Input[1];
                    itemsSource = itemsSource.Where(a => a.WingProfile != null).Where(a => a.WingProfile.Contains(wingProfile));
                }
                catch { }
            }

            if (Input[2] != "" && Input[2] != "Length (double)")
            {
                try
                {
                    (double, double) ends = Extensions.GetEnds<double>(Input[2]);
                    itemsSource = itemsSource.Where(a => (ends.Item1 <= a.Length) && (a.Length <= ends.Item2));
                }
                catch { }
            }

            if (Input[3] != "" && Input[3] != "Wingspan (double)")
            {
                try
                {
                    (double, double) ends = Extensions.GetEnds<double>(Input[3]);
                    itemsSource = itemsSource.Where(a => (ends.Item1 <= a.Wingspan) && (a.Wingspan <= ends.Item2));
                }
                catch { }
            }

            return itemsSource.ToList();
        }
        
        public static List<AirframeViewModel> GetAirframeString(KbDbContext db, string aircraftName)
        {
            db.Airframes.Load();
            db.Aircraft.Load();

            Aircraft aircraft = db.Aircraft.Find(aircraftName);
            Airframe airframe = db.Airframes.Find(aircraftName);

            if (aircraft != null && airframe != null)
            {
                AirframeViewModel a = new AirframeViewModel { Name = airframe.Name, WingProfile = airframe.WingProfile, Length = airframe.Length, Wingspan = airframe.Wingspan };
                List<AirframeViewModel> list = new List<AirframeViewModel>();
                list.Add(a);
                return list;
            }
            else
            {
                return null;
            }
        }
    }
}
