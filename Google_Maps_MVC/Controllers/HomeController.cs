using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


using System.Configuration;
using System.Data.SqlClient;

namespace Google_Maps_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            string markers = "[";
            string conString = ConfigurationManager.ConnectionStrings["ConString"].ConnectionString;
            SqlCommand cmd = new SqlCommand("select * from db_entrevista..TB_ENTREVISTA where vr_lat is not null and vr_lng is not null and vr_lat <> '' and vr_lng <> '' and vr_latfim <> '' and vr_lngfim <> ''");
            using (SqlConnection con = new SqlConnection(conString))
            {
                cmd.Connection = con;
                con.Open();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        markers += "{";
                        //markers += string.Format("'title': '{0}',", sdr["cd_eva"]);
                        markers += string.Format("'lat': {0},", sdr["vr_latfim"]);
                        markers += string.Format("'lng': {0}", sdr["vr_lngfim"]);
                       // markers += string.Format("'description': '{0}'", sdr["fl_parsrt"]);
                        markers += "},";
                    }
                }
                con.Close();
            }

            markers = markers.Remove(markers.Length - 1);
            markers += "];";
            ViewBag.Markers = markers;
            return View();
        }
    }
}