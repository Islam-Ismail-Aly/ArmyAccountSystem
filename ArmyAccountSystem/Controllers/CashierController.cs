using ArmyAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArmyAccountSystem.Controllers
{
    public class CashierController : Controller
    {
        // Instance from the context entity of database

        ArmyTechTaskEntities InvoiceModel = new ArmyTechTaskEntities();

        #region Display the data of Cashier
        // GET: Cashier
        [HttpGet]
        public ActionResult GetCashierData()
        {
            List<Cashier> _CashierDetails = InvoiceModel.Cashiers.OrderBy(x => x.ID).ToList();
            return View(_CashierDetails);
        }
        #endregion

        #region Get the Id of Branch FK by ViewBag to select it from DropDown List in the view

        [HttpGet]
        public ActionResult AddCashierData()
        {
            List<string> _list = InvoiceModel.Branches.Select(x => x.ID.ToString()).ToList();
            ViewBag.BranchID = new SelectList(_list);
            return View();
        }

        #endregion

        #region save cashier data in Cashier table in DB

        [HttpPost]
        public ActionResult SaveCashierData(Cashier _cashierDetails)
        {
            var cashierUser = new Cashier
            {
                CashierName = _cashierDetails.CashierName,
                BranchID = _cashierDetails.Branch.ID
            };
            InvoiceModel.Cashiers.Add(cashierUser);
            InvoiceModel.SaveChanges();
            return RedirectToAction("GetCashierData");
        }

        #endregion

        #region Get Cashier element by it's Id to delete

        [HttpGet]
        public ActionResult DeleteCashier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Cashier _Delete = InvoiceModel.Cashiers.Where(x => x.ID == id).SingleOrDefault();
            return View(_Delete);
        }

        #endregion

        #region Delete the selected element from Cashier table in DB

        [HttpPost]
        public ActionResult DeleteCashier(int? id, Cashier _CashierDetail)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            InvoiceModel.Cashiers.Remove(InvoiceModel.Cashiers.Where(x => x.ID == id).SingleOrDefault());
            InvoiceModel.SaveChanges();
            return RedirectToAction("GetCashierData");

        }

        #endregion

        #region Get Cashier element by it's id to use it in edit

        [HttpGet]
        public ActionResult EditCashier(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Cashier _edit = InvoiceModel.Cashiers.Where(x => x.ID == id).SingleOrDefault();
            return View(_edit);
        }

        #endregion

        #region Edit the invoice data by passing id to this Action method

        [HttpPost]
        public ActionResult EditCashier(int? id, Cashier _cashierDetail)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            Cashier _editcashier = InvoiceModel.Cashiers.Where(x => x.ID == id).SingleOrDefault();
            _editcashier.CashierName = _cashierDetail.CashierName;
            InvoiceModel.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion
    }
}