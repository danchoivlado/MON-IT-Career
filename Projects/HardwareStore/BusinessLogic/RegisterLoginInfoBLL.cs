﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HardwareStore.View;
using HardwareStore.Models;

namespace HardwareStore.BusinessLogic
{
     public class RegisterLoginInfoBLL
    {
        public OfficeStoreContext officestoreContext;

        public RegisterLoginInfoBLL(OfficeStoreContext context)
        {
            this.officestoreContext = context;
        }

        public RegisterLoginInfoBLL()
        {
            this.officestoreContext = new OfficeStoreContext();
        }

        public bool Login(string LoginName, string password)
        {
            //Return bool if there is employee with this name and password
            var HasEmployeeWithName = officestoreContext.Employees.FirstOrDefault(a => a.FirstName == LoginName && a.Password == password);
            if (HasEmployeeWithName != null)
            {
                return true;
            }
            return false;
        }

        public void Register(string FirstName, string LastName, string TownName, string Password)
        {
            //Registe employee
            var Town = this.officestoreContext.Towns.FirstOrDefault(a => a.Name == TownName);
            if (Town == null)//Checks if Town exists
            {
                //Creates new town
                Town = new Towns()
                {
                    Name = TownName
                };
                this.officestoreContext.Towns.Add(Town);
            }
            Employees employee = new Employees()
            {
                FirstName = FirstName,
                LastName = LastName,
                TownId = Town.Id,
                Password = Password,
                DutyId = 1
            };

            this.officestoreContext.Employees.Add(employee);
            this.officestoreContext.SaveChanges();
        }

        public string[] GetNameAddressIfExists()
        {
            //Check and returns info if exists
            var StoreInfo = officestoreContext.StoreInfo.First(a => a.Id ==1);

            return new string[2] { StoreInfo.Name, StoreInfo.Address };
        }

        public void SaveData(string StoreName, string StoreAddress)
        {
            //Updates data 
            var StoreInfo = officestoreContext.StoreInfo.ToList().First(a => a.Id == 1);
            StoreInfo.Name = StoreName;
            StoreInfo.Address = StoreAddress;

            this.officestoreContext.SaveChanges();
        }

        public void SaveLogined(string EmployeeName)
        {
            //Save last logined
            if (this.officestoreContext.LastLogin.Count() > 35)
            {
                this.officestoreContext.LastLogin.Remove(this.officestoreContext.LastLogin.First());
            }
            var Employee = this.officestoreContext.Employees.First(a => a.FirstName == EmployeeName);
            this.officestoreContext.LastLogin.Add(new LastLogin() { EmployeeId = Employee.Id, DateLimeLogined = DateTime.Now });
            this.officestoreContext.SaveChanges();
        }

       
    }
}
    