using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Createx.Core.Enums;

namespace Createx.Core.Entities
{
    public  class Customer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name {  get; set; }


        [StringLength(20)]
        public string PhoneNumber { get; set; } = null!;


        [StringLength(50)]
        public string ConnectAuthority {  get; set; } = null!;

        public Activation Activation {  get; set; }

        public string AccountGroup {  get; set; } = null!;


        [StringLength(100)]
        public string Region {  get; set; } = null!;

        public Gender Gender {  get; set; }

        public int CityID {  get; set; }

        public City City { get; set; } = null!;
    }
}
