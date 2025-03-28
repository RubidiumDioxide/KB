﻿using Microsoft.EntityFrameworkCore;
//using kb_back.Entities;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;

namespace kb_back.Tools
{
    public static class DepartmentTools
    {
        public class DepartmentViewModel {
            public string Name { get; set; } = null!;
            public string Adress { get; set; } = null!;
            public int? Director { get; set; }
            public string? DirectorSurname { get; set; }
            public string? DirectorName { get; set; }

            public List<string> GetValues()
            {
                return new List<string>{ Name, Adress, Director.ToString(), DirectorSurname, DirectorName }; 
            }
        }

        public static List<DepartmentViewModel> LoadTable(KbDbContext db)
        {
            db.Departments.Load();
            db.Employees.Load();

            return db.Departments.Join(
                db.Employees,
                d => d.Director, 
                e => e.Id, 
                (d, e) => new DepartmentViewModel { 
                    Name = d.Name, 
                    Adress = d.Adress,
                    Director = d.Director,
                    DirectorSurname = e.Surname, 
                    DirectorName = e.FirstName
                }).ToList();
        }

        public static void Add(KbDbContext db, List<string> Input) 
        {
            db.Departments.Load();
            db.Employees.Load();

            Department department = new Department(Input);
            
            if (db.Departments.Where(d => d.Director == department.Director).ToList().Count() != 0)
            {
                throw new Exception("Director has to be unique");
            }

            try
            {
                db.Departments.Add(department);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Departments.Remove(department);
                throw new Exception(ex.Message); 
            }

            Employee director = db.Employees.Find(department.Director);
            director.Department = department.Name; 
            db.SaveChanges();
        }

        public static void Edit(KbDbContext db, string name, List<string> Input)
        {
            db.Departments.Load();
            db.Employees.Load();

            Department department = db.Departments.Find(name);
            Department department_reserve = new Department(department);

            if(department != null)
            {
                if (db.Departments.Where(d => d.Director == department.Director && d != department).ToList().Count() != 0 )
                {
                    throw new Exception("Director has to be unique");
                }

                department.Set(Input);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    department.Set(department_reserve);
                    throw new Exception(ex.Message); 
                }

                Employee director = db.Employees.Find(department.Director);
                director.Department = department.Name;
                db.SaveChanges();
            }
            else
            {
                throw new Exception("null reference");
            }
        }

        public static void Delete(KbDbContext db, string name)
        {
            db.Departments.Load();

            Department department = db.Departments.Find(name);

            if (department != null)
            {
                try
                {
                    db.Departments.Remove(department);
                    db.SaveChanges();
                }
                catch(Exception ex)
                {
                    db.Departments.Add(department);
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("null reference");
            }
        }

        public static List<DepartmentViewModel> Search(KbDbContext db, List<string> Input)
        {
            db.Departments.Load();
            db.Employees.Load();

            var itemsSource = db.Departments.Local.ToBindingList().Join(
                db.Employees.Local.ToBindingList(),
                d => d.Director,
                e => e.Id,
                (d, e) => new DepartmentViewModel
                {
                    Name = d.Name,
                    Adress = d.Adress,
                    Director = d.Director,
                    DirectorSurname = e.Surname,
                    DirectorName = e.FirstName
                });

            if (Input[0] != "" && Input[0] != "Name (string)")
            {
                try
                {
                    string name = Input[0];
                    itemsSource = itemsSource.Where(d => d.Name.Contains(name));
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "Adress (string)")
            {
                try
                {
                    string adress = Input[1];
                    itemsSource = itemsSource.Where(d => d.Adress.Contains(adress));
                }
                catch { }
            }


            if (Input[2] != "" && Input[2] != "Director (int)")
            {
                try
                {
                    (int, int) ends = Extensions.GetEnds<int>(Input[2]);
                    itemsSource = itemsSource.Where(e => (ends.Item1 <= e.Director) && (e.Director <= ends.Item2)); 
                }
                catch { }
            }

            return itemsSource.ToList(); 
        }
    }
}
