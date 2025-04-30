using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Drawing;
using System.Text;
using VRS.Global;
using VRS.Models;

namespace VRS.Controllers
{
    public class OrderBooking : Controller
    {
        private readonly IConfiguration _configuration;
        public OrderBooking(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            var usersession = HttpContext.Session.GetString("Userid");
            if (usersession == null)
            {                
                if (!Request.Path.StartsWithSegments("/images") && !Request.Path.StartsWithSegments("/css") && !Request.Path.StartsWithSegments("/js"))
                {
                    return RedirectToAction("Index", "Login");
                }
            }
            var dbop = new Models.db(_configuration);

            //var ItemSubGrp = dbop.GetFilteredItemSubGrp();
            //var Brands = dbop.GetBrands();
            //ViewData["ItemSubGrp"] = ItemSubGrp;
            //ViewData["Brand"] = Brands;
            //var Salesman = dbop.GetSalesPerson("S","");
            //var conditionValue = GlobalVariables.User_Type_ForSOApp == 'C' ? GlobalVariables.Led_Key : "";

            //var Salesman = dbop.GetSalesPerson("S", conditionValue);
            //var Customer = dbop.Getledger("W");
            //var transpoter = dbop.Getledger("T");
            //var broker = dbop.Getledger("B");
            //ViewData["Customer"] = Customer;
            //ViewData["transpoter"] = transpoter;
            //ViewData["Salesman"] = Salesman;
            //ViewData["broker"] = broker;
            //var payterms = dbop.GetPayTerms();
            //ViewData["payterms"] = payterms;
            return View();
        }
        [HttpPost]
        public IActionResult GetItems(string CoBr_Id)
        {
            List<ItemSubGrp> ItemSubGrp = new List<ItemSubGrp>();
            var dbop = new Models.db(_configuration);
            ItemSubGrp = dbop.GetFilteredItemSubGrp(CoBr_Id);

            ViewData["ItemSubGrp"] = ItemSubGrp;
            return Json(ItemSubGrp);
        }
        [HttpPost]
        public IActionResult Getbrand(string CoBr_Id)
        {
            List<Brand> ItemSubGrp = new List<Brand>();
            var dbop = new Models.db(_configuration);

            var Brand = dbop.GetBrands(CoBr_Id);
            ViewData["Brand"] = Brand;

            return Json(Brand);
        }
        [HttpPost]
        public IActionResult GetSalesPerson(string led_cat, string custkey,string CoBr_Id)
        {            
            var dbop = new Models.db(_configuration);
            var salesman = dbop.GetSalesPerson(led_cat,custkey,CoBr_Id);
            ViewData["Salesman"] = salesman;
            return Json(salesman);
        }
        [HttpPost]
        public IActionResult GetOrderDetails(string itemSubGrpKey, string ItemKey, string brandKey, string styleKey, string shadeKey, string sizeKey, string fromMRP, string toMRP, bool stockWise,string userid,string cobr,string fcyr)
        {
            var dbop = new Models.db(_configuration);            
            var items = dbop.GetSalesOrderDetails(itemSubGrpKey, ItemKey, brandKey, styleKey, shadeKey, sizeKey, userid, cobr, fcyr, fromMRP, toMRP, stockWise);
            return Json(items);
        }
        [HttpPost]
        public IActionResult GetOrderDetailsHeader(string itemSubGrpKey, string ItemKey, string brandKey, string styleKey, string shadeKey, string sizeKey,
                                             string fromMRP, string toMRP, bool stockWise, string userid, string cobr, string fcyr)
        {
            var dbop = new Models.db(_configuration);
            var items = dbop.GetOrderDetailsHeader(itemSubGrpKey, ItemKey, brandKey, styleKey, shadeKey, sizeKey, userid, cobr, fcyr, fromMRP, toMRP, stockWise);

            // Return the view and pass the data
            return Json(items);  // Return the view and model
        }


        [HttpPost]
        public IActionResult GetBarcodeDetails(string barcode, string userid, string cobr, string fcyr)
        {
            var dbop = new Models.db(_configuration);
            var items = dbop.GetBarcodeDetails(barcode,userid,cobr,fcyr);
            return Json(items);
        }

        [HttpPost]
        public IActionResult GetItemsBySubGroup(string itemSubGrpKey, string cobr)
        {
            List<Item> items = new List<Item>();
            if (itemSubGrpKey != null)
            {
                var dbop = new Models.db(_configuration);
                items = dbop.GetItemsByItemSubGrpKey(itemSubGrpKey, cobr);
            }
            return Json(items);
        }

        [HttpPost]
        public IActionResult Getbroker(string customerid,string cobr)
        {
            var dbop = new Models.db(_configuration);
            var items = dbop.Getbroker(customerid,cobr);
            return Json(items);
        }

        [HttpPost]
        public IActionResult Getconsignee(string customerid,string cobr)
        {
            var dbop = new Models.db(_configuration);
            var consignee = dbop.Getconsignee(customerid,cobr);
            return Json(consignee);
        }
        [HttpPost]
        public IActionResult GetCustInfo(string customerid, string cobr)
        {

            var dbop = new Models.db(_configuration);
            CustInfo city = dbop.GetCustInfo(customerid, cobr);
            return Json(city);
        }
        [HttpPost]
        public IActionResult getConsineeCity(string consigneekey, string cobr)
        {
            var dbop = new Models.db(_configuration);
            string city = dbop.getConsineeCity(consigneekey, cobr);
            return Json(city);
        }

        [HttpPost]
        public IActionResult GetComm(string brokerid, string cobr)
        {
            var dbop = new Models.db(_configuration);
            string comm = dbop.GetComm(brokerid, cobr);
            return Json(comm);
        }
        [HttpPost]
        public IActionResult GetStylesByItem(string itemKey, string cobr)
        {
            List<Style> styles = new List<Style>();
            if (itemKey != null)
            {
                var dbop = new Models.db(_configuration);
                styles = dbop.GetStyles(itemKey,cobr );
            }

            return Json(styles);
        }
        [HttpPost]
        public IActionResult GetShadeByStyle(string styleKey, string cobr)
        {
            List<Shade> shade = new List<Shade>();
            if (styleKey != null)
            {
                var dbop = new Models.db(_configuration);
                shade = dbop.GetDistinctShadesFromStyleShade(styleKey,cobr);
            }
            return Json(shade);
        }
        [HttpPost]
        public IActionResult GetShadeByItem(string itemKey, string cobr)
        {
            List<Shade> shade = new List<Shade>();
            if (itemKey != null)
            {
                var dbop = new Models.db(_configuration);
                shade = dbop.GetDistinctShadesFromItemShade(itemKey,cobr);
            }
            return Json(shade);
        }
        [HttpPost]
        public IActionResult GetStylesSizeByStyle(string styleKey, string cobr)
        {
            List<StyleSize> size = new List<StyleSize>();
            if (styleKey != null)
            {
                var dbop = new Models.db(_configuration);
                size = dbop.GetStyleSizesWithStyleSizes(styleKey, cobr);
            }
            return Json(size);
        }
        [HttpPost]
        public IActionResult GetStylesSizeByItem(string itemKey,string cobr)
        {
            List<StyleSize> size = new List<StyleSize>();
            if (itemKey != null)
            {
                var dbop = new Models.db(_configuration);
                size = dbop.GetStyleSizesWithItemSizes(itemKey,cobr);
            }
            return Json(size);
        }
        [HttpPost]
        public IActionResult Getsalesorderno(bool barcodewise,string userid,string cobr,string fcyr)
        {
            
            var dbop = new Models.db(_configuration);
            var size = dbop.Getsalesorderno(userid, cobr, fcyr, barcodewise);
            return Json(size);

        }
        [HttpPost]
        public IActionResult Addtocard(string userid, string data, string datajson, int isactive)
        {
            // Instantiate the db class
            var dbop = new Models.db(_configuration);
            userid = GlobalVariables.LoginName;
            // Get items based on the selected item subgroup key
            var items = dbop.InsertAddcart(userid, data, datajson, isactive);//hardcoded
            //var items = dbop.GetSalesOrderDetails(itemSubGrpKey, ItemKey, brandKey, styleKey, shadeKey, sizeKey);
            // Return the items as JSON
            return Json(items);
        }
        [HttpPost]
        public IActionResult Insertsalesorderdetails(string userid,string cobr,string fcyr, string data, int typ)
        {

            var dbop = new Models.db(_configuration);
          
            var items = dbop.Insertsalesordertemp(userid, data, typ, cobr, fcyr);
            return Json(items);


        }
        [HttpPost]
        public IActionResult InsertFinalSalesOrder(string userid, string cobr, string fcyr, string data, bool barcodewise)
        {
            var dbop = new Models.db(_configuration);
            
            var items = dbop.InsertFinalSalesOrder(userid, cobr, fcyr, data, barcodewise);
            return Json(items);

        }
        [HttpPost]
        public IActionResult Getcartdetails(string userid)
        {
            // Instantiate the db class
            var dbop = new Models.db(_configuration);
            userid = GlobalVariables.LoginName;
            // Get items based on the selected item subgroup key
            DataSet items = dbop.Getvieworderdetails(userid);//hardcoded
                                                             //var items = dbop.GetSalesOrderDetails(itemSubGrpKey, ItemKey, brandKey, styleKey, shadeKey, sizeKey);

            // Return the items as JSON
            String x = items.Tables[0].Rows[0]["data"].ToString();
            return Json(x);
        }
        [HttpPost]
        public IActionResult GetViewOrder(string userid,string cobr,string fcyr, bool barcodewise)
        {
            var dbop = new Models.db(_configuration);            
            var items = dbop.GetViewOrder(userid, cobr, fcyr, barcodewise);
            return Json(items);
        }
        [HttpPost]
        public IActionResult GetBookingType(string cobr)
        {
            List<BookingType> bookingType = new List<BookingType>();
            var dbop = new Models.db(_configuration);
            bookingType = dbop.GetBookingType(cobr);
            return Json(bookingType);
        }
        [HttpPost]
        public String UpdateSession(string user, string coBrId, string fcYrId, string UserTypeForSOApp, string CompanyName, string MobileNo, string Led_Key, string LoginName)
        {
            string userid = "";
            GlobalVariables.User_Name = user;
            GlobalVariables.CoBr_Id = coBrId;
            GlobalVariables.FcYr_Id = fcYrId;
            GlobalVariables.User_Type_ForSOApp = Convert.ToChar(UserTypeForSOApp);
            GlobalVariables.CompanyName = CompanyName;
            GlobalVariables.MobileNo = MobileNo;
            GlobalVariables.Led_Key = Led_Key;
            GlobalVariables.LoginName = LoginName;
            return userid;
        }

        [HttpPost]
        public IActionResult GetLedger(string ledcat, string CoBr_Id, string SalesPerson_key=null)
        {
            List<Customer> ledger = new List<Customer>();
            if (ledcat != null)
            {
                var dbop = new Models.db(_configuration);
                ledger = dbop.Getledger(ledcat, CoBr_Id, SalesPerson_key);
            }
            return Json(ledger);
        }        
        [HttpPost]
        public IActionResult GetPayTerms(string CoBr_Id)
        {
            List<paymentterms> paymentterms = new List<paymentterms>();

            var dbop = new Models.db(_configuration);
            paymentterms = dbop.GetPayTerms(CoBr_Id);
            return Json(paymentterms);
        }
        [HttpPost]
        public IActionResult GetStation(string cobr)
        {
            List<city> city = new List<city>();
            var dbop = new Models.db(_configuration);
            city = dbop.GetCity(cobr);
            return Json(city);
        }
        [HttpPost]
        public IActionResult InsertCust( string data, string cobr, string fcyr,string user)
        {

            var dbop = new Models.db(_configuration);
            var items = dbop.InsertCust(data,cobr,fcyr,user);
            return Json(items);

        }
        [HttpPost]
        public IActionResult GetDiscount(string CoBr_Id)
        {
            var dbop = new Models.db(_configuration);
            var discountList = dbop.GetDiscounts(CoBr_Id);
            return Json(discountList);
        }
        
    }
}
