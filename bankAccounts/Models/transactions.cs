	using System;
	using System.ComponentModel.DataAnnotations;

	namespace bankAccounts.Models
    //variable name must be exact match to table column
	{
        public class transactions : BaseEntity
    	{	
        [Key]
        public int transactionsid { get; set; }

		
		[Required]
        public int transaction { get; set; }

				
        [Required]
        public DateTime createTime { get; set; }
        public int usersid {get; set;}

        // public List<user> transactions { get; set; }

        // //---------------constructor below to not throw error of db retreval is blank; created one-to-many relationship

        //     public transactions()
        //     {
        //         transactions = new List<transactions>();
        //     }

        // public class LoginRegViewModel
        // {
        //     public LoginViewModel loginVM {get; set;}
        //     public RegisterViewModel registerVM {get; set;}
        // }

    	}
    }