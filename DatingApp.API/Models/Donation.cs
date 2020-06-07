using System;

namespace DatingApp.API.Models
{
    public class Donation
    {
        public int DonationId { get; set; }
     //   public DateTime DonationSent { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        //public DonationForCreationDto()
        //{
        //    DonationSent = DateTime.Now;
        //}
    }
}