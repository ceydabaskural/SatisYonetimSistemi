using System.ComponentModel;

namespace SatisYonetimSistemi.Models
{
    public class Status
    {
        [DisplayName("Durum No")]
        public int StatusId { get; set; }


        [DisplayName("Durumlar")]
        public string StatusName { get; set; }


        [DisplayName("Sıra")]
        public int RowNo { get; set; }

    }
}
