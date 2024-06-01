using System.ComponentModel;

namespace SatisYonetimSistemi.Models
{
    public class OfferViewModel : Offer
    {
        [DisplayName("Durumu")]
        public string StatusName { get; set; }
    }
}
