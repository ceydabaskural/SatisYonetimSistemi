using System.Data.SqlClient;
using System.Data;

namespace SatisYonetimSistemi.Models
{
    public class HomeIndexModel
    {
        public Status GetStatus {  get; set; }
        public List<Status> StatusList {  get; set; }

        public List<Offer> OfferList { get; set; }
    }
}
