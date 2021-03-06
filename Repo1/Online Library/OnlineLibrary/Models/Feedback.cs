//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnlineLibrary.Models
{
    using System;
    using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel;

	public partial class Feedback
    {
        public int Id { get; set; }
		[Required(ErrorMessage = "Please description is required")]
		public string description { get; set; }
		[Display( Name ="Book_name")]
		[Required(ErrorMessage = "Please Book_ID is required")]
		public Nullable<int> Book_ID { get; set; }
		[Display( Name="User_name")]
		[Required(ErrorMessage = "Please User_ID is required")]
		public Nullable<int> User_ID { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual UsersTable UsersTable { get; set; }
    }
}
