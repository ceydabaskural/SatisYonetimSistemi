using Microsoft.AspNetCore.Mvc;
using SatisYonetimSistemi.Models;
using System.Data;
using System.Data.SqlClient;

namespace SatisYonetimSistemi.Controllers
{
    public class StatusController : Controller
    {
        SqlConnection connection = new SqlConnection();
        public StatusController(IConfiguration configuration)
        {
            connection.ConnectionString = configuration.GetConnectionString("SatisYonetimSistemi");
        }
        public IActionResult Index()
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status order by RowNo",connection);
            DataTable dt = new DataTable();
            da.Fill(dt);

            List<Status> list = new List<Status>();

            foreach (DataRow item in dt.Rows)
            {
                Status status = new Status();
                {
                    status.StatusId = Convert.ToInt32(item["StatusId"]);
                    status.StatusName = item["StatusName"].ToString();
                    status.RowNo = Convert.ToInt32(item["RowNo"]);
                };

                list.Add(status);
            }
            return View(list);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Status model)
        {
            if (ModelState.IsValid)
            {
                SqlCommand command = new SqlCommand("insert into dbo.Status values (@StatusName, @RowNo)", connection);
                command.Parameters.AddWithValue("StatusName", model.StatusName);
                command.Parameters.AddWithValue("RowNo", model.RowNo);

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }


        }



        public IActionResult Edit(int StatusId)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status where StatusId=@StatusId", connection);
            da.SelectCommand.Parameters.AddWithValue("StatusId", StatusId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Status status = new Status
                {
                    StatusId = Convert.ToInt32(dt.Rows[0]["StatusId"]),
                    StatusName = dt.Rows[0]["StatusName"].ToString(),
                    RowNo = Convert.ToInt32(dt.Rows[0]["RowNo"])
                };

                return View(status);
            }
        }


        [HttpPost]
        public IActionResult Edit(Status model) 
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status where StatusId=@StatusId", connection);
            da.SelectCommand.Parameters.AddWithValue("StatusId", model.StatusId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) 
            {
                ModelState.AddModelError(string.Empty, model.StatusId + "numaralı kayıt bulunamadı.");
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("update dbo.Status set StatusName=@StatusName, RowNo=@RowNo where StatusId=@StatusId", connection);
                cmd.Parameters.AddWithValue("StatusId", model.StatusId);
                cmd.Parameters.AddWithValue("StatusName", model.StatusName);
                cmd.Parameters.AddWithValue("RowNo", model.RowNo);


                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }

        }


        public IActionResult Delete(int StatusId)
        {
            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status where StatusId=@StatusId", connection);
            da.SelectCommand.Parameters.AddWithValue("StatusId", StatusId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Status status = new Status
                {
                    StatusId = Convert.ToInt32(dt.Rows[0]["StatusId"]),
                    StatusName = dt.Rows[0]["StatusName"].ToString(),
                    RowNo = Convert.ToInt32(dt.Rows[0]["RowNo"]),
                };

                return View(status);
            }
        }


        [HttpPost]
        public IActionResult Delete(Status model)
        {

            SqlDataAdapter da = new SqlDataAdapter("select * from dbo.Status where StatusId=@StatusId", connection);
            da.SelectCommand.Parameters.AddWithValue("StatusId", model.StatusId);

            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0)
            {
                ModelState.AddModelError(string.Empty, model.StatusId + "numaralı kayıt bulunamadı.");
                return View(model);
            }
            else
            {
                SqlCommand cmd = new SqlCommand("delete from dbo.Status where StatusId=@StatusId", connection);
                cmd.Parameters.AddWithValue("StatusId", model.StatusId);

                connection.Open();
                cmd.ExecuteNonQuery();
                connection.Close();

                return RedirectToAction(nameof(Index));
            }

        }
    }
}
