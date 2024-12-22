using Microsoft.EntityFrameworkCore;
//using kb_back.Entities;

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
            public decimal Salary { get; set; }

            public List<string> GetValues()
            {
                return new List<string> { Id.ToString(), Surname, FirstName, LastName, DateOfBirth.ToString(), Position, Department, YearsOfExperience.ToString(), Salary.ToString() };
            }            
        }

        public static List<EmployeeViewModel> LoadTable(KbDbContext db)
        {
            db.Employees.Load();

            return db.Employees.Select(e => new EmployeeViewModel { Id = e.Id, Surname = e.Surname, FirstName = e.FirstName, LastName = e.LastName, DateOfBirth = e.DateOfBirth, Position = e.Position, Department = e.Department, YearsOfExperience = e.YearsOfExperience, Salary = e.Salary }).ToList();
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
            db.Departments.Load();
            db.Projects.Load();

            Employee employee = db.Employees.Find(id);

            if (db.Departments.Where(d => d.Director == employee.Id).ToList().Count() != 0 || db.Projects.Where(p => p.ChiefDesigner == employee.Id).ToList().Count() != 0)
            {
                throw new Exception("Employee assigned to be a ChiefDesigner or Director. Reassign first, then delete");
            }

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

            var itemsSource = db.Employees.Local.ToBindingList().Select(e => new EmployeeViewModel { Id = e.Id, Surname = e.Surname, FirstName = e.FirstName, LastName = e.LastName, DateOfBirth = e.DateOfBirth, Position = e.Position, Department = e.Department, YearsOfExperience = e.YearsOfExperience, Salary = e.Salary });

            if (Input[0] != "" && Input[0] != "Id (int)")
            {
                try
                {
                    (int, int) ends = Extensions.GetEnds<int>(Input[0]);
                    itemsSource = itemsSource.Where(e => (ends.Item1 <= e.Id) && (e.Id <= ends.Item2)); 
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "Surname (string)")
            {
                try
                {
                    string surname = Input[1];
                    itemsSource = itemsSource.Where(e => e.Surname.Contains(surname));
                }
                catch { }
            }

            if (Input[2] != "" && Input[2] != "FirstName (string)")
            {
                try
                {
                    string firstName = Input[2];
                    itemsSource = itemsSource.Where(e => e.FirstName.Contains(firstName));
                }
                catch { }
            }
            
            if (Input[3] != "" && Input[3] != "LastName (string)")
            {
                try
                {
                    string lastName = Input[3];
                    itemsSource = itemsSource.Where(e => e.LastName != null).Where(e => e.LastName.Contains(lastName));
                }
                catch { }
            }

            if (Input[4] != "" && Input[4] != "DateOfBirth (DateOnly)")
            {
                try
                {
                    (DateOnly, DateOnly) ends = Extensions.GetEnds<DateOnly>(Input[4]);
                    itemsSource = itemsSource.Where(p => (ends.Item1 <= p.DateOfBirth) && (p.DateOfBirth <= ends.Item2));
                }
                catch { }
            }

            if (Input[5] != "" && Input[5] != "Position (string)")
            {
                try
                {
                    string position = Input[5];
                    itemsSource = itemsSource.Where(e => e.Position.Contains(position));
                }
                catch { }
            }

            if (Input[6] != "" && Input[6] != "Department (string)")
            {
                try
                {
                    string department = Input[6];
                    itemsSource = itemsSource.Where(e => e.Department != null).Where(e => e.Department.Contains(department));
                }
                catch { }
            }

            if (Input[7] != "" && Input[7] != "YearsOfExperience (byte)")
            {
                try
                {
                    (byte, byte) ends = Extensions.GetEnds<byte>(Input[7]);
                    itemsSource = itemsSource.Where(e => (ends.Item1 <= e.YearsOfExperience) && (e.YearsOfExperience <= ends.Item2));
                }
                catch { }
            }

            if (Input[8] != "" && Input[8] != "Salary (decimal)")
            {
                try
                {
                    (decimal, decimal) ends = Extensions.GetEnds<decimal>(Input[8]);
                    itemsSource = itemsSource.Where(e => (ends.Item1 <= e.Salary) && (e.Salary <= ends.Item2));
                }
                catch { }
            }

            return itemsSource.ToList();
        }
    }
}