using System.Configuration;
using System.Linq;
using System.Web.Mvc;
using KShannonInnes;
using KShannonInnes.Models;

public class ProductController : Controller
{
    [HttpGet]
    public JsonResult GetAll()
    {
        var connectionString =
            ConfigurationManager.ConnectionStrings["BitXifySampleTestDBConnectionString"].ConnectionString;

        using (var db = new BitXifyDataContext(connectionString))
        {
            var products = db.tblProducts
                .Select(product => new Product { Name = product.Name, Price = product.Price })
                .ToList();

            // Apparently Json wasn't allowed for GET requests due to an exploit in early browsers
            // called Json Hijacking. This is fixed in modern browsers so is largely a non-issue.
            // JsonREquestBehaviour tells asp.net to allow the json return.
            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}
