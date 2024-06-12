using Microsoft.AspNetCore.Mvc;
using SatisYonetimSistemi.Models;
using System.Data;
using System.Data.SqlClient;

namespace SatisYonetimSistemi.Controllers
{
    public class OfferController : Controller
    {
        SqlConnection sqlConnection = new SqlConnection();
        public OfferController(IConfiguration configuration)
        {
            sqlConnection.ConnectionString = configuration.GetConnectionString("SatisYonetimSistemi");
        }
        public IActionResult Index()
        {
            SqlDataAdapter sqlAdapter = new SqlDataAdapter("select * from dbo.Offer as o inner join dbo.Status as s on s.StatusId=o.StatusId order by OfferTitle", sqlConnection);
            DataTable dataTable = new DataTable();
            sqlAdapter.Fill(dataTable);

            List<OfferViewModel> offerslist = new List<OfferViewModel>();

            foreach (DataRow offer in dataTable.Rows)
            {
                OfferViewModel offer1 = new OfferViewModel
                {
                    OfferId = Convert.ToInt32(offer["OfferId"]),
                    OfferTitle = (offer["OfferTitle"]).ToString(),
                    CustomerName = (offer["CustomerName"]).ToString(),
                    Price = Convert.ToDecimal(offer["Price"]),
                    StatusId = Convert.ToInt32(offer["StatusId"]),
                    StatusName = (offer["StatusName"]).ToString(),
                };
                offerslist.Add(offer1);
            }


            return View(offerslist);
        }



        public IActionResult Create()
        {
            OfferCreateModel model1 = new OfferCreateModel
            {
                Offer = new Offer(),
                Statuses = GetStatusList(),
            };

            return View(model1);
        }


        [HttpPost]
        public IActionResult Create(OfferCreateModel model)
        {
            if (ModelState.IsValid)
            {
                SqlCommand command = new SqlCommand("insert into dbo.Offer values (@offerTitle, @customerName, @price, @statusId )", sqlConnection);
                command.Parameters.AddWithValue("offerTitle", model.Offer.OfferTitle);
                command.Parameters.AddWithValue("customerName", model.Offer.CustomerName);
                command.Parameters.AddWithValue("price", model.Offer.Price);
                command.Parameters.AddWithValue("statusId", model.Offer.StatusId);

                sqlConnection.Open();
                command.ExecuteNonQuery();
                sqlConnection.Close();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                OfferCreateModel createModel = new OfferCreateModel
                {
                    Offer = model.Offer,
                    Statuses = GetStatusList(),

                };
                    return View(createModel);

            }
        }




        public List<Status> GetStatusList()
        {
            List<Status> list = new List<Status>();

            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status", sqlConnection);
            DataTable dataTable = new DataTable();
            da.Fill(dataTable);

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


        public Offer GetOffer(int id)
        {
            SqlDataAdapter dataAdapter = new SqlDataAdapter("select * from dbo.Offer where OfferId=@OfferId", sqlConnection);
            dataAdapter.SelectCommand.Parameters.AddWithValue("OfferId", id);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            Offer offer = new Offer
            {
                OfferId = Convert.ToInt32(dt.Rows[0]["OfferId"]),
                OfferTitle = Convert.ToString(dt.Rows[0]["OfferTitle"]),
                CustomerName = Convert.ToString(dt.Rows[0]["CustomerName"]),
                StatusId = Convert.ToInt32(dt.Rows[0]["StatusId"]),
                Price = Convert.ToDecimal(dt.Rows[0]["Price"]),
            };
            return offer;
        }


        public OfferViewModel GetOfferViewModel(int id)
        {
            SqlDataAdapter adapter = new SqlDataAdapter("select * from dbo.Offer as o inner join dbo.Status as s on s.StatusId=o.StatusId where o.OfferId=@OfferId", sqlConnection);
            adapter.SelectCommand.Parameters.AddWithValue("OfferId", id);
            DataTable data = new DataTable();
            adapter.Fill(data);

            OfferViewModel offerView = new OfferViewModel
            {
                OfferId = Convert.ToInt32(data.Rows[0]["OfferId"]),
                OfferTitle = Convert.ToString(data.Rows[0]["OfferTitle"]),
                CustomerName = Convert.ToString(data.Rows[0]["CustomerName"]),
                StatusId = Convert.ToInt32(data.Rows[0]["StatusId"]),
                Price = Convert.ToDecimal(data.Rows[0]["Price"]),
                StatusName = Convert.ToString(data.Rows[0]["StatusName"]),
            };
            return offerView;
        }



        public IActionResult Edit(int id)
        {
            ViewBag.DurumListesi = GetStatusList();

            return View(GetOffer(id));

            //SqlDataAdapter adapter = new SqlDataAdapter("select * from dbo.Offer where OfferId=@OfferId", sqlConnection);
            //adapter.SelectCommand.Parameters.AddWithValue("OfferId", id);

            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            //Offer offer = new Offer();
            //offer.OfferTitle = (dataTable.Rows[0]["OfferTitle"]).ToString();
            //offer.OfferId = Convert.ToInt32(dataTable.Rows[0]["OfferId"]);
            //offer.StatusId = Convert.ToInt32(dataTable.Rows[0]["StatusId"]);
            //offer.CustomerName = (dataTable.Rows[0]["CustomerName"]).ToString();
            //offer.Price = Convert.ToDecimal(dataTable.Rows[0]["Price"]);

            //return View(offer);
        }


        [HttpPost]
        public IActionResult Edit(Offer model)
        {

            if (ModelState.IsValid)
            {
                SqlCommand sqlCommand = new SqlCommand("update dbo.Offer set OfferTitle=@OfferTitle, CustomerName=@CustomerName, Price=@Price, StatusId=@StatusId where OfferId=@OfferId", sqlConnection);
                sqlCommand.Parameters.AddWithValue("OfferId", model.OfferId);
                sqlCommand.Parameters.AddWithValue("OfferTitle", model.OfferTitle);
                sqlCommand.Parameters.AddWithValue("Price", model.Price);
                sqlCommand.Parameters.AddWithValue("CustomerName", model.CustomerName);
                sqlCommand.Parameters.AddWithValue("StatusId", model.StatusId);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.DurumListesi = GetStatusList();

                return View(model);
            }
        }




        public IActionResult Delete(int id)
        {

            return View(GetOfferViewModel(id));
            //SqlDataAdapter adapter = new SqlDataAdapter("select * from dbo.Offer where OfferId=@OfferId", sqlConnection);
            //adapter.SelectCommand.Parameters.AddWithValue("OfferId", id);

            //DataTable dataTable = new DataTable();
            //adapter.Fill(dataTable);

            //Offer offer = new Offer();
            //offer.OfferTitle = (dataTable.Rows[0]["OfferTitle"]).ToString();
            //offer.OfferId = Convert.ToInt32(dataTable.Rows[0]["OfferId"]);
            //offer.StatusId = Convert.ToInt32(dataTable.Rows[0]["StatusId"]);
            //offer.CustomerName = (dataTable.Rows[0]["CustomerName"]).ToString();
            //offer.Price = Convert.ToDecimal(dataTable.Rows[0]["Price"]);

            //return View(offer);
        }


        [HttpPost]
        public IActionResult Delete(Offer model)
        {
            OfferViewModel viewModel = GetOfferViewModel(model.OfferId);

            //SqlDataAdapter sqlData = new SqlDataAdapter("select * from dbo.Offer where OfferId=@OfferId ", sqlConnection);
            //sqlData.SelectCommand.Parameters.AddWithValue("OfferId", model.OfferId);

            //DataTable table = new DataTable();
            //sqlData.Fill(table);

            if (viewModel == null)
            {
                return View(viewModel);
            }
            else
            {
                SqlCommand sqlCommand = new SqlCommand("delete from dbo.Offer where OfferId=@OfferId", sqlConnection);
                sqlCommand.Parameters.AddWithValue("OfferId", model.OfferId);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();

                return RedirectToAction(nameof(Index));
            }
        }
    }
}
