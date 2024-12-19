using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static kb_back.Tools.ArmamentTools;

namespace kb_back.Tools
{
    public static class EngineTools
    {
        public class EngineViewModel 
        {
            public string Name { get; set; } = null!;
            public string Type { get; set; } = null!;
            public double Power { get; set; }
            public double Weight { get; set; }

            public List<string> GetValues()
            {
                List<string> l = new List<string>() { Name, Type, Power.ToString(), Weight.ToString() };
                return l;
            }
        }

        public static List<EngineViewModel> LoadTable(KbDbContext db)
        {
            db.Engines.Load();

            return db.Engines.Local.ToBindingList().Select(e => new EngineViewModel { Name = e.Name, Type = e.Type, Power = e.Power, Weight = e.Weight }).ToList(); 
        }

        public static void Add(KbDbContext db, List<string> Input)
        {
            db.Engines.Load();

            Engine engine = new Engine(Input);

            try
            {
                db.Engines.Add(engine);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Engines.Remove(engine);
                throw new Exception(ex.Message);
            }
        }

        public static void Edit(KbDbContext db, string name, List<string> Input)
        {
            db.Armaments.Load();

            Engine engine = db.Engines.Find(name);
            Engine engine_reserve = engine;

            if (engine != null)
            {
                engine.Set(Input);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    engine = engine_reserve;
                    throw new Exception(ex.Message);
                }
            }
        }

        public static void Delete(KbDbContext db, string name)
        {
            db.Engines.Load();

            Engine engine = db.Engines.Find(name);

            if (engine != null)
            {
                try
                {
                    db.Engines.Remove(engine);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Engines.Add(engine);
                    throw new Exception(ex.Message);
                }
            }
        }

        public static List<EngineViewModel> Search(KbDbContext db, List< string > Input){
            db.Engines.Load();

            var itemsSource = db.Engines.Local.ToBindingList().Select(e => new EngineViewModel { Name = e.Name, Type = e.Type, Power = e.Power, Weight = e.Weight });

            if (Input[0] != "" && Input[0] != "Name (string)")
            {
                try 
                {
                    string name = Input[0];
                    itemsSource = itemsSource.Where(e => e.Name == name);
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "Type (string)")
            {
                try
                {
                    string type = Input[1];
                    itemsSource = itemsSource.Where(e => e.Type == type);
                }
                catch { }
            }


            if (Input[2] != "" && Input[2] != "Power (float)")
            {
                try
                {
                    double power = double.Parse(Input[2]);
                    itemsSource = itemsSource.Where(e => e.Power == power);
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

            return itemsSource.ToList();
        }

        public static string GetEngineString(KbDbContext db, string aircraftName)
        {
            db.Engines.Load();
            db.Aircraft.Load();

            Aircraft aircraft = db.Aircraft.Find(aircraftName);
            Engine engine = db.Engines.Find(aircraft.Engine);

            if (aircraft != null && engine != null)
            { 
                return engine.Name + engine.Type + engine.Power.ToString() + engine.Weight.ToString();   
            }
            else{
                return "";
            }
        }
    }
}
