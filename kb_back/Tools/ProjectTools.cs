using Microsoft.EntityFrameworkCore;
//using kb_back.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace kb_back.Tools
{
    public static class ProjectTools
    {
        public class ProjectViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string Aircraft { get; set; } = null!;
            public string Status { get; set; } = null!;
            public DateOnly DateBegan { get; set; }
            public DateOnly? DateFinished { get; set; }
            public int? ChiefDesigner { get; set; }

            public List<string> GetValues()
            {
                return new List<string> { Id.ToString(), Name, Aircraft, Status, DateBegan.ToString(), DateFinished.ToString().ToString(), ChiefDesigner.ToString() };
            }
        }

        public static List<ProjectViewModel> LoadTable(KbDbContext db)
        {
            db.Projects.Load();

            return db.Projects.Select(p => new ProjectViewModel { Id = p.Id, Name = p.Name, Aircraft = p.Aircraft, Status = p.Status, DateBegan = p.DateBegan, DateFinished = p.DateFinished, ChiefDesigner = p.ChiefDesigner }).ToList();
        }

        public static void Add(KbDbContext db, List<string> Input)
        {
            db.Projects.Load();
            db.Employees.Load();

            Project project = new Project(Input);

            try
            {
                db.Projects.Add(project);
                db.SaveChanges();
            }
            catch(Exception ex)
            {
                db.Projects.Remove(project);
                throw new Exception(ex.Message); 
            }
        }

        public static void Edit(KbDbContext db, int id, List<string> Input)
        {
            db.Projects.Load();

            Project project = db.Projects.Find(id);
            Project project_reserve = new Project(project);

            if (project == null) { throw new Exception("null reference"); }

            if (project.Status == "завершен") { throw new Exception("Нельзя изменить данные о завершенном проекте"); }

            project.Set(Input);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    project.Set(project_reserve);
                    throw new Exception(ex.Message);
                }
        }

        public static void Delete(KbDbContext db, int id)
        {
            db.Projects.Load();

            Project project = db.Projects.Find(id);

            if(project != null)
            {
                try
                {
                    db.Projects.Remove(project);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Projects.Add(project);
                    throw new Exception(ex.Message); 
                }
            }
            else
            {
                throw new Exception("null reference");
            }
        }

        public static void EndProject(KbDbContext db, int id)
        {
            db.Projects.Load();

            Project project = db.Projects.Find(id);
            Project project_reserve = new Project(project);

            if (project != null)
            {
                try
                {
                    if (project.Status != "завершен")
                    {
                        project.Status = "завершен";
                        project.DateFinished = DateOnly.FromDateTime(DateTime.Now);
                        db.SaveChanges();
                    }
                }
                catch (Exception ex)
                {
                    project.DateFinished = null; 
                    project.Status = project_reserve.Status;
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("null reference"); 
            }
        } 

        public static List<ProjectViewModel> Search(KbDbContext db, List<string> Input)
        {
            db.Projects.Load();

            var itemsSource = db.Projects.Select(p => new ProjectViewModel { Id = p.Id, Name = p.Name, Aircraft = p.Aircraft, Status = p.Status, DateBegan = p.DateBegan, DateFinished = p.DateFinished, ChiefDesigner = p.ChiefDesigner });

            if (Input[0] != "" && Input[0] != "Id (int)")
            {
                try
                {
                    (int, int) ends = Extensions.GetEnds<int>(Input[0]);
                    itemsSource = itemsSource.Where(p => (ends.Item1 <= p.Id) && (p.Id <= ends.Item2));
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "Name (string)")
            {
                try
                {
                    string name = Input[1];
                    itemsSource = itemsSource.Where(p => p.Name.Contains(name));
                }
                catch { }
            }

            if (Input[2] != "" && Input[2] != "Aircraft (string)")
            {
                try
                {
                    string aircraft = Input[2];
                    itemsSource = itemsSource.Where(p => p.Aircraft.Contains(aircraft));
                }
                catch { }
            }

            if (Input[3] != "" && Input[3] != "Status (string)")
            {
                try
                {
                    string status = Input[3];
                    itemsSource = itemsSource.Where(p => p.Status.Contains(status));
                }
                catch { }
            }

            if (Input[4] != "" && Input[4] != "DateBegan (DateOnly)")
            {
                try
                {
                    (DateOnly, DateOnly) ends = Extensions.GetEnds<DateOnly>(Input[4]);
                    itemsSource = itemsSource.Where(p => (ends.Item1 <= p.DateBegan) && (p.DateBegan <= ends.Item2));
                }
                catch { }
            }

            if (Input[5] != "" && Input[5] != "DateFinished (DateOnly)")
            {
                try
                {
                    (DateOnly, DateOnly) ends = Extensions.GetEnds<DateOnly>(Input[5]);
                    itemsSource = itemsSource.Where(p => (ends.Item1 <= p.DateFinished) && (p.DateFinished <= ends.Item2));
                }
                catch { }
            }

            if (Input[6] != "" && Input[6] != "ChiefDesigner (int)")
            {
                try
                {
                    (int, int) ends = Extensions.GetEnds<int>(Input[6]);
                    itemsSource = itemsSource.Where(p => (ends.Item1 <= p.ChiefDesigner) && (p.ChiefDesigner <= ends.Item2));
                }
                catch { }
            }

            return itemsSource.ToList(); 
        }
    }
}
