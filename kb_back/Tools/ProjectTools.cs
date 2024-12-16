using kb_back;
using kb_back.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb_back.Tools
{
    public static class ProjectTools
    {
        public static dynamic LoadTable(KbDbContext db)
        {
            db.Projects.Load();

            //return db.Projects.Local.ToBindingList().Select(c => new { c.Id, c.Name, c.Aircraft, c.Department, c.Status, c.DateBegan, c.DateFinished, c.ChiefDesigner });
            return db.Projects.Local.ToBindingList();
        }
    }
}
