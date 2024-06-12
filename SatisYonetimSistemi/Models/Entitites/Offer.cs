using System.ComponentModel;

namespace SatisYonetimSistemi.Models
{
    public class Offer
    {
        [DisplayName("Teklif No")]
        public int OfferId { get; set; }

        [DisplayName("Teklif Başlığı")]
        public string OfferTitle { get; set; }

        [DisplayName("Müşteri Adı")]
        public string CustomerName { get; set; }

        [DisplayName("Fiyat")]
        public decimal Price { get; set; }

        [DisplayName("Durum No")]
        public int StatusId { get; set; }
    }
}
