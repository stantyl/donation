using System;
 using System.Collections.Generic;
 using System.Security.Claims;
 using System.Threading.Tasks;
 using AutoMapper;
 using DatingApp.API.Data;
 using DatingApp.API.Dtos;
 using DatingApp.API.Helpers;
 using DatingApp.API.Models;


 using Microsoft.AspNetCore.Authorization;
 using Microsoft.AspNetCore.Mvc;


namespace DatingApp.API.Controllers
{
    //[ServiceFilter(typeof(LogUserActivity))]
    //  [Authorize]
    [Route("api/[controller]")]
    [ApiController]


    public class DonationsController : ControllerBase
    {

        private readonly IDatingRepository _repo;
        private readonly IMapper _mapper;
        public DonationsController(IDatingRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        //[HttpGet("{id}", Name = "GetDonation")]
        //public async Task<IActionResult> GetDonation(int id)
        // [HttpGet("{id}", Name = "GetDonation")]


        // https://localhost:44366/api/donations
        public async Task<IActionResult> Get()
        {

            int id = 1;
            //if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //    return Unauthorized();

            var donationFromRepo = await _repo.GetDonation(id);

            if (donationFromRepo == null)
                return NotFound();

            return Ok(donationFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetDonations([FromQuery]DonationParams donationParams)
        {
            //if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            //    return Unauthorized();

            var donationsFromRepo = await _repo.GetDonations(donationParams);

            return Ok(donationsFromRepo);

        }



        [HttpPost]
        public async Task<IActionResult> CreateDonation(int userId, DonationForCreationDto donationForCreationDto)
        {
            var donation = _mapper.Map<Donation>(donationForCreationDto);
            try
            {

                _repo.Add(donation);
         
                if (await _repo.SaveAll())
                {
                    return Ok(donation);
                }

                throw new Exception("Creating the donation failed on save");
            }
            catch (Exception E)
            {

                return BadRequest("error " + E.InnerException.ToString());
            }



        }
    }
}