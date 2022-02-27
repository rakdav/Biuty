namespace Biuty.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ClientService")]
    public partial class ClientService
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientService()
        {
            DocumentByService = new HashSet<DocumentByService>();
            ProductSale = new HashSet<ProductSale>();
        }

        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string ClientID { get; set; }

        [Required]
        [StringLength(255)]
        public string ServiceID { get; set; }

        public DateTime StartTime { get; set; }

        public string Comment { get; set; }

        public virtual Client Client { get; set; }

        public virtual Service Service { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DocumentByService> DocumentByService { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProductSale> ProductSale { get; set; }
    }
}
