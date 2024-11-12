using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb_back
{
    class Program
    {
        static void Main(string[] args)
        {
            using (KbDbContext db = new KbDbContext())
            {
                //db.Engines.Add(new Engine { EngineName = "cool engine 2", EngineType = "1-cylinder opposite", Power = 213.324, EngineWeight = 6832.32 });
                db.Engines.Add(new Engine { EngineName = "cool engine 3", EngineType = "1-cylinder opposite", Power = 2324, EngineWeight = 6832.32 });

                db.SaveChanges();
       
                var engines = db.Engines.ToList();
                Console.WriteLine("Таблица Двигателей: ");
                foreach (Engine e in engines)
                {
                    Console.WriteLine(e.EngineName+" "  +e.EngineType+" "+e.Power+" "+e.EngineWeight);
                }
            }
        }
    }
}
