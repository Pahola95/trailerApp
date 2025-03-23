using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrailerAPPv3.DTO
{
    public class TrackingDTO
    {
        public System.Guid id { get; set; }
        public Nullable<System.Guid> trailer_id { get; set; }

        public System.String trailer_plate { get; set; }
        public System.String trailer_model { get; set; }

        public System.String trailer_status { get; set; }
        public System.String trailer_linea { get; set; }
        public System.String trailer_color {  get; set; }
        public Nullable<decimal> latitude { get; set; }
        public Nullable<decimal> longitude { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.DateTime)]
        public Nullable<System.DateTime> datetime { get; set; }
        public System.Int32 contador {  get; set; }
    }
}