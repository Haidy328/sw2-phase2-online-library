using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibrary.Models
{
    [MetadataType(typeof(BookMetaData))]
    public partial class Book
    {
        //Add properties or methods
    }
    public class BookMetaData
    {
        //edit properties
        [Display(Name = "ID")]
        public int BId { get; set; }

        [Display(Name = "Name")]
        public string Bname { get; set; }

        [Display(Name = "Author")]
        public string Bauthor { get; set; }

        [Display(Name = "price")]
        public Nullable<int> Bprice { get; set; }

        [Display(Name = "Number Of Books")]
        public Nullable<int> no_of_books { get; set; }

        [Display(Name = "Category ID")]
        public int catagry_Id { get; set; }
    }
}