using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static kb_back.Tools.ArmamentTools;

namespace kb_back.Tools
{
    public static class EmployeeTools
    {
        public class EmployeeViewModel
        {
            public int Id { get; set; }
            public string Surname { get; set; } = null!;
            public string FirstName { get; set; } = null!;
            public string? LastName { get; set; }
            public DateOnly DateOfBirth { get; set; }
            public string Position { get; set; } = null!;
            public string? Department { get; set; }
            public byte YearsOfExperience { get; set; }
            public int? CurrentProject { get; set; }
            public decimal Salary { get; set; }

            public List<string> GetValues()
            {
                List<string> l = new List<string> { Id.ToString(), Surname, FirstName, LastName, DateOfBirth.ToString(), Position, Department, YearsOfExperience.ToString(), CurrentProject.ToString(), Salary.ToString() };
                return l;
            }            
        }

        public static List<EmployeeViewModel> LoadTable(KbDbContext db)
        {
            db.Employees.Load();

            return db.Employees.Local.ToBindingList().Select(e => new EmployeeViewModel { Id = e.Id, Surname = e.Surname, FirstName = e.FirstName, LastName = e.LastName, DateOfBirth = e.DateOfBirth, Position = e.Position, Department = e.Department, YearsOfExperience = e.YearsOfExperience, CurrentProject = e.CurrentProject, Salary = e.Salary }).ToList();
        }

        public static void Add(KbDbContext db, List<string> Input)
        {
            db.Employees.Load();

            Employee employee = new Employee(Input);

            try
            {
                db.Employees.Add(employee);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Employees.Remove(employee);
                throw new Exception(ex.Message);
            }
        }

        public static void Edit(KbDbContext db, int id, List<string> Input)
        {
            db.Employees.Load();

            Employee employee = db.Employees.Find(id);
            Employee employee_reserve = new Employee(employee);

            if (employee != null)
            {
                employee.Set(Input);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    employee.Set(employee_reserve);
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("null reference");
            }
        }

        public static void Delete(KbDbContext db, int id)
        {
            db.Employees.Load();

            Employee employee = db.Employees.Find(id);

            if (employee != null)
            {
                try
                {
                    db.Employees.Remove(employee);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Employees.Add(employee);
                    throw new Exception(ex.Message);
                }
            }
        }

        public static List<EmployeeViewModel> Search(KbDbContext db, List<string> Input)
        {
            db.Employees.Load();

            var itemsSource = db.Employees.Local.ToBindingList().Select(e => new EmployeeViewModel { Id = e.Id, Surname = e.Surname, FirstName = e.FirstName, LastName = e.LastName, DateOfBirth = e.DateOfBirth, Position = e.Position, Department = e.Department, YearsOfExperience = e.YearsOfExperience, CurrentProject = e.CurrentProject, Salary = e.Salary });

            if (Input[0] != "" && Input[0] != "Id (int)")
            {
                try
                {
                    int id = int.Parse(Input[0]);
                    itemsSource = itemsSource.Where(e => e.Id == id);
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "Surname (string)")
            {
                try
                {
                    string surname = Input[1];
                    itemsSource = itemsSource.Where(e => e.Surname == surname);
                }
                catch { }
            }

            if (Input[2] != "" && Input[2] != "FirstName (string)")
            {
                try
                {
                    string firstName = Input[2];
                    itemsSource = itemsSource.Where(e => e.FirstName == firstName);
                }
                catch { }
            }
            
            if (Input[3] != "" && Input[3] != "LastName (string)")
            {
                try
                {
                    string lastName = Input[3];
                    itemsSource = itemsSource.Where(e => e.LastName == lastName);
                }
                catch { }
            }

            if (Input[4] != "" && Input[4] != "DateOfBirth (DateOnly)")
            {
                try
                {
                    DateOnly dateOfBirth = DateOnly.Parse(Input[1]);
                    itemsSource = itemsSource.Where(e => e.DateOfBirth == dateOfBirth);
                }
                catch { }
            }

            if (Input[5] != "" && Input[5] != "Position (string)")
            {
                try
                {
                    string position = Input[5];
                    itemsSource = itemsSource.Where(e => e.Position == position);
                }
                catch { }
            }

            if (Input[6] != "" && Input[6] != "Department (string)")
            {
                try
                {
                    string department = Input[6];
                    itemsSource = itemsSource.Where(e => e.Department == department);
                }
                catch { }
            }

            if (Input[7] != "" && Input[7] != "YearsOfExperience (byte)")
            {
                try
                {
                    byte yearsOfExperience = byte.Parse(Input[7]);
                    itemsSource = itemsSource.Where(e => e.YearsOfExperience == yearsOfExperience);
                }
                catch { }
            }

            if (Input[8] != "" && Input[8] != "CurrentProject (int)")
            {
                try
                {
                    int currentProject = int.Parse(Input[8]);
                    itemsSource = itemsSource.Where(e => e.CurrentProject == currentProject);
                }
                catch { }
            }

            if (Input[9] != "" && Input[9] != "Salary (decimal)")
            {
                try
                {
                    decimal salary = decimal.Parse(Input[9]);
                    itemsSource = itemsSource.Where(e => e.Salary == salary);
                }
                catch { }
            }

            return itemsSource.ToList();
        }
    }
}