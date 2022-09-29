using ArmyAccountSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ArmyAccountSystem.Controllers
{
    public class InvoiceController : Controller
    {
        // Instance from the context entity of database

        ArmyTechTaskEntities InvoiceModel = new ArmyTechTaskEntities();

        #region Display the basic data of Invoices

        [HttpGet]
        public ActionResult Index()
        {
            List<InvoiceDetail> _InvoiceDetails = InvoiceModel.InvoiceDetails.OrderBy(x => x.ID).ToList();
            return View(_InvoiceDetails);
        }

        #endregion

        #region Get Main data of Invoices that have other basic data from relations of the tables - Data from View is Vw_Invoice_Info in database
        
        [HttpGet]
        public ActionResult GetAllInvoiceData()
        {
            IQueryable<Vw_Invoice_Info> _InvoiceAllData = InvoiceModel.Vw_Invoice_Info.OrderBy(x => x.ID);
            return View(_InvoiceAllData);
        }

        #endregion

        #region Get invoice element by it's id to use it in edit

        [HttpGet]
        public ActionResult EditInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            InvoiceDetail _edit = InvoiceModel.InvoiceDetails.Where(x => x.ID == id).SingleOrDefault();
            return View(_edit);
        }

        #endregion

        #region Edit the invoice data by passing id to this method

        [HttpPost]
        public ActionResult EditInvoice(int? id, InvoiceDetail _invoiceDetail)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            InvoiceDetail _editInvoice = InvoiceModel.InvoiceDetails.Where(x => x.ID == id).SingleOrDefault();
            _editInvoice.ItemName = _invoiceDetail.ItemName;
            _editInvoice.ItemCount = _invoiceDetail.ItemCount;
            _editInvoice.ItemPrice = _invoiceDetail.ItemPrice;
            InvoiceModel.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Save invoice data in the InvoiceDetails table in DB

        [HttpPost]
        public ActionResult SaveInvoice(InvoiceDetail _invoiceDetail)
        {
            var invoiceItem = new InvoiceDetail
            {
                ItemName = _invoiceDetail.ItemName,
                ItemCount = _invoiceDetail.ItemCount,
                ItemPrice = _invoiceDetail.ItemPrice,
                InvoiceHeaderID = _invoiceDetail.InvoiceHeader.ID
            };
            InvoiceModel.InvoiceDetails.Add(invoiceItem);
            InvoiceModel.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

        #region Get the Id of invoice header FK by ViewBag to select it from DropDown List

        [HttpGet]
        public ActionResult CreateInvoice()
        {
            List<string> _list = InvoiceModel.InvoiceHeaders.Select(x => x.ID.ToString()).ToList();
            ViewBag.CustomerID = new SelectList(_list);
            return View();
        }

        #endregion

        #region Get invoice element by Id to delete

        [HttpGet]
        public ActionResult DeleteInvoice(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            InvoiceDetail _Delete = InvoiceModel.InvoiceDetails.Where(x => x.ID == id).SingleOrDefault();
            return View(_Delete);
        }

        #endregion

        #region Delete the selected element from Invoice table in DB

        [HttpPost]
        public ActionResult DeleteInvoice(int? id, InvoiceDetail _InvoiceDetail)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            }
            InvoiceModel.InvoiceDetails.Remove(InvoiceModel.InvoiceDetails.Where(x => x.ID == id).SingleOrDefault());
            InvoiceModel.SaveChanges();
            return RedirectToAction("Index");
        }

        #endregion

    }
}