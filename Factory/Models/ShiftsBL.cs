﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Factory.Models
{
    public class ShiftsBL
    {
        FactoryDBEntitie db = new FactoryDBEntitie();

        public List<ShiftsWithEmployeesModel> getShifts()
        {
            List<ShiftsWithEmployeesModel> shfts = new List<ShiftsWithEmployeesModel>();

            foreach (var item in db.shifts)
            {
                ShiftsWithEmployeesModel newShiftModel = new ShiftsWithEmployeesModel();

                newShiftModel.ID = item.ID;
                newShiftModel.Date = item.Date;
                newShiftModel.Start_time = item.Start_time;
                newShiftModel.End_time = item.End_time;
                newShiftModel.Employees = new List<Employee>();

                var EmployeeRegistered = db.EmployeeShifts.Where(x => x.ShiftID == item.ID);

                foreach (var emp in EmployeeRegistered)
                {
                    var empID = emp.EmployeeID;
                    var employee = db.Employees.Where(x => x.ID == empID).First();
                    newShiftModel.Employees.Add(employee);
                }
                shfts.Add(newShiftModel);
            }
            return shfts;
        }

        public ShiftsWithEmployeesModel getShift(int id)
        {
            return getShifts().Where(x => x.ID == id).First();
        }


        public string AddShift(ShiftsWithEmployeesModel shft)
        {
            shift newShift = new shift();

            newShift.Date = shft.Date;
            newShift.Start_time = shft.Start_time;
            newShift.End_time = shft.End_time;

            db.shifts.Add(newShift);

            db.SaveChanges();

            return "Created";

        }
    }
}