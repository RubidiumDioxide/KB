﻿using Microsoft.EntityFrameworkCore;
//using kb_back.Entities;

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kb_back.Tools
{
    public static class ArmamentTools
    {
        public class ArmamentViewModel
        {
            public string Name { get; set; } = null!;
            public double Caliber {  get; set; }
            public double FiringRate {  get; set; }
            public double Weight {  get; set; } 

            public List<string> GetValues()
            {
                List<string> l = new List<string> {Name, Caliber.ToString(), FiringRate.ToString(), Weight.ToString()};
                return l;
            }
        }

        public class AircraftsArmamentViewModel
        {
            public string Name { get; set; } = null!;
            public double Caliber { get; set; }
            public double FiringRate { get; set; }
            public double Weight { get; set; }
            public byte Quantity { get; set; }
        }


        public static List<ArmamentViewModel> LoadTable(KbDbContext db)
        {
            db.Armaments.Load();

            return db.Armaments.Local.ToBindingList().Select(a => new ArmamentViewModel { Name = a.Name, Caliber = a.Caliber, FiringRate = a.FiringRate, Weight = a.Weight }).ToList();
        }

        public static void Add(KbDbContext db, List<string> Input)
        {
            db.Armaments.Load();

            Armament armament = new Armament(Input);

            try
            {
                db.Armaments.Add(armament);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                db.Armaments.Remove(armament);
                throw new Exception(ex.Message);
            }
        }

        public static void Edit(KbDbContext db, string name,List<string> Input)
        {
            db.Armaments.Load(); 

            Armament armament = db.Armaments.Find(name);
            Armament armament_reserve = armament;

            if (armament != null)
            {
                armament.Set(Input);

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    armament = armament_reserve;
                    throw new Exception(ex.Message);
                }
            }
        }

        public static void Delete(KbDbContext db, string name)
        {
            db.Armaments.Load();

            Armament armament = db.Armaments.Find(name);

            if (armament != null)
            {
                try
                {
                    db.Armaments.Remove(armament);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    db.Armaments.Add(armament);
                    throw new Exception(ex.Message);
                }
            }
        }

        public static List<ArmamentViewModel> Search(KbDbContext db, List <string> Input) 
        {
            db.Armaments.Load();

            var itemsSource = db.Armaments.Local.ToBindingList().Select(a => new ArmamentViewModel { Name = a.Name, Caliber = a.Caliber, FiringRate = a.FiringRate, Weight = a.Weight });

            if (Input[0] != "" && Input[0] != "Name (string)")
            {
                try
                {
                    string name = Input[0];
                    itemsSource = itemsSource.Where(a => a.Name.Contains(name));
                }
                catch { }
            }

            if (Input[1] != "" && Input[1] != "Caliber (double)")
            {
                try
                {
                    (double, double) ends = Extensions.GetEnds<double>(Input[1]);
                    itemsSource = itemsSource.Where(a => (ends.Item1 <= a.Caliber) && (a.Caliber <= ends.Item2));
                }
                catch { }
            }

            if (Input[2] != "" && Input[2] != "FiringRate (double)")
            {
                try
                {
                    (double, double) ends = Extensions.GetEnds<double>(Input[2]);
                    itemsSource = itemsSource.Where(a => (ends.Item1 <= a.FiringRate) && (a.FiringRate <= ends.Item2)); 
                }
                catch { }
            }

            if (Input[3] != "" && Input[3] != "Weight (double)")
            {
                try
                {
                    (double, double) ends = Extensions.GetEnds<double>(Input[3]);
                    itemsSource = itemsSource.Where(a => (ends.Item1 <= a.Weight) && (a.Weight <= ends.Item2));
                }
                catch { }
            }

            return itemsSource.ToList();
        }

        public static List<AircraftsArmamentViewModel> GetArmamentsList(KbDbContext db, string aircraftName)
        {
            db.Armaments.Load();
            db.AircraftArmaments.Load();

            return db.AircraftArmaments.Local.ToBindingList().Where(aa => aa.Aircraft == aircraftName).Join(db.Armaments.Local,
                aa => aa.Armament,
                a => a.Name, 
                (aa, a) => new AircraftsArmamentViewModel
                {
                    Name = a.Name,
                    Caliber = a.Caliber,
                    FiringRate = a.FiringRate,
                    Weight = a.Weight,
                    Quantity = aa.Quantity
                }).ToList();
        }
    }
}