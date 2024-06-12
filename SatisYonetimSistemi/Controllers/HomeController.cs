using Microsoft.AspNetCore.Mvc;
using SatisYonetimSistemi.Models;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;

namespace SatisYonetimSistemi.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection _connection = new SqlConnection();

        public HomeController(IConfiguration configuration)
        {
            _connection.ConnectionString = configuration.GetConnectionString("SatisYonetimSistemi");
        }
        public IActionResult Index(int id)
        {
            //ViewBag.GetStatusList = GetStatusList();

            HomeIndexModel homeIndex = new HomeIndexModel();
            {
                homeIndex.StatusList = GetStatusList();
                homeIndex.GetOffers = GetOfferList();
                homeIndex.OfferViewModels = GetOfferViewModel(id);

                return View(homeIndex);
            }
        }

        
        public List<Status> GetStatusList()
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status", _connection);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);

            List<Status> list = new List<Status>();

            foreach (DataRow item in dataTable.Rows)
            {
                list.Add(new Status
                {
                    StatusId = Convert.ToInt32(item["StatusId"]),
                    StatusName = (item["StatusName"].ToString())
                });
            }
            return list;
        }

        public List<Offer> GetOfferList()
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("select * from dbo.Offer", _connection);
            DataTable dataTable = new DataTable();
            sqlAdapter.Fill(dataTable);

            List<Offer> offerslist = new List<Offer>();

            foreach (DataRow offer in dataTable.Rows)
            {
                Offer offer1 = new Offer
                {
                    OfferId = Convert.ToInt32(offer["OfferId"]),
                    OfferTitle = (offer["OfferTitle"]).ToString(),
                    CustomerName = (offer["CustomerName"]).ToString(),
                    Price = Convert.ToDecimal(offer["Price"]),
                    StatusId = Convert.ToInt32(offer["StatusId"]),
                    
                };
                offerslist.Add(offer1);
            }
            return offerslist;
        }

        public Status GetStatus(int statusId)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status where StatusId=@StatusId", _connection);
            da.SelectCommand.Parameters.AddWithValue("StatusId", statusId);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);

            Status status = new Status
            {
                StatusId = Convert.ToInt32(dataTable.Rows[0]["StatusId"]),
                StatusName = dataTable.Rows[0]["StatusName"].ToString()
            };

            return status;
        }

        public List<OfferViewModel> GetOfferViewModel(int offerId)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from dbo.Status as s inner join dbo.Offer as o on o.StatusId=s.StatusId", _connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            List<OfferViewModel> indexModels = new List<OfferViewModel>();
            foreach (DataRow row in dataTable.Rows)
            {
                indexModels.Add(new OfferViewModel
                {
                    OfferId = Convert.ToInt32(row["OfferId"]),
                    OfferTitle = (row["OfferTitle"]).ToString(),
                    CustomerName = (row["CustomerName"]).ToString(),
                    Price = Convert.ToDecimal(row["Price"]),
                    StatusId = Convert.ToInt32(row["StatusId"]),
                    StatusName = (row["StatusName"]).ToString(),
                });
            }
            return indexModels;
        }

    }
}
