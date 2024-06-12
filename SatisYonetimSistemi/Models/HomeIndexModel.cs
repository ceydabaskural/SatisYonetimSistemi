using System.Data.SqlClient;
using System.Data;

namespace SatisYonetimSistemi.Models
{
    public class HomeIndexModel 
    {
        public List<OfferViewModel> OfferViewModels {  get; set; }
        public List<Status> StatusList {  get; set; }

        public List<Offer> GetOffers { get; set; }

        
    }
}
