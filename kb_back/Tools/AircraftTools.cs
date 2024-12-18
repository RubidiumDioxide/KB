using kb_back.Entities;
using Microsoft.EntityFrameworkCore;

namespace kb_back.Tools
{
    public static class AircraftTools
    {
        public class AircraftViewModel
        {
            public string Name { get; set; } = null!;
            public string? Type { get; set; }
            public byte? Crew { get; set; }
            public double? Weight { get; set; }
            public string? Engine { get; set; }
            public List<string> GetValues()
            {
                List<string> l = new List<string> { Name, Type, Crew.ToString(), Weight.ToString(), Engine.ToString() };
                return l;
            }
        }

        public static List<AircraftViewModel> LoadTable(KbDbContext db)
        {
            db.Aircrafts.Load();

            return db.Aircrafts.Local.ToBindingList().Select(a => new AircraftViewModel { Name = a.Name, Type = a.Type, Crew = a.Crew, Weight = a.Weight, Engine = a.Engine }).ToList();
        }

        public static void Add(KbDbContext db, List<string> Input)
        {
            db.Aircrafts.Load();
            db.Airframes.Load();

            Aircraft aircraft = new Aircraft(Input);
            List<string> Input_1 = new List<string> { aircraft.Name };
            Airframe airframe = new Airframe(Input_1);

            try
            {
                db.Aircrafts.Add(aircraft);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Aircrafts.Remove(aircraft);
                throw new Exception(ex.Message);
            }

            db.Airframes.Add(airframe);
            db.SaveChanges();

            /*try
            {
                db.Airframes.Add(airframe);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Aircrafts.Remove(aircraft);
                db.Airframes.Remove(airframe);
                throw new Exception(ex.Message);
            }*/
        }

        public static void Edit(KbDbContext db, string name, List<string> Input)
        {
            db.Aircrafts.Load();

            Aircraft aircraft = db.Aircrafts.Find(name);
            Aircraft aircraft_reserve = aircraft;

            if (aircraft != null)
            {
                aircraft.Set(Input);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    aircraft = aircraft_reserve;
                    throw new Exception(ex.Message);
                }
            }
        }

        public static void Delete(KbDbContext db, string name)
        {
            db.Aircrafts.Load();
            db.Airframes.Load();

            Aircraft aircraft = db.Aircrafts.Find(name);

            if (aircraft != null)
            {
                try
                {
                    db.Aircrafts.Remove(aircraft);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Aircrafts.Add(aircraft);
                    throw new Exception(ex.Message);
                }
            }
        }

        public static List<AircraftViewModel> Search(KbDbContext db, List<string> Input)
        {
            db.Aircrafts.Load();

            var itemsSource = db.Aircrafts.Local.ToBindingList().Select(a => new AircraftViewModel { Name = a.Name, Type = a.Type, Crew = a.Crew, Weight = a.Weight, Engine = a.Engine });

            if (Input[0] != "" && Input[0] != "Name (string)")
            {
                try
                {
                    string name = Input[0];
                    itemsSource = itemsSource.Where(a => a.Name == name);
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "Type (string)")
            {
                try
                {
                    string type = Input[1];
                    itemsSource = itemsSource.Where(a => a.Type == type);
                }
                catch { }
            }

            if (Input[2] != "" && Input[2] != "Crew (byte)")
            {
                try
                {
                    byte crew = byte.Parse(Input[2]);
                    itemsSource = itemsSource.Where(a => a.Crew == crew);
                }
                catch { }
            }

            if (Input[3] != "" && Input[3] != "Weight (float)")
            {
                try
                {
                    double weight = double.Parse(Input[3]);
                    itemsSource = itemsSource.Where(a => a.Weight == weight);
                }
                catch { }
            }

            if (Input[4] != "" && Input[4] != "Engine (string)")
            {
                try
                {
                    string engine = Input[4];
                    itemsSource = itemsSource.Where(a => a.Engine == engine);
                }
                catch { }
            }

            return itemsSource.ToList();
        }
    }
}