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
     [ServiceFilter(typeof(LogUserActivity))]
   //  [Authorize]
     [Route("api/donations/[controller]")]
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

          [HttpGet("{id}", Name = "GetDonation")]
         public async Task<IActionResult> GetDonation( int id)
         {
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

            //messageParams.UserId = userId;

            var messagesFromRepo = await _repo.GetDonations(donationParams);

            var messages = _mapper.Map<IEnumerable<MessageToReturnDto>>(messagesFromRepo);

            Response.AddPagination(messagesFromRepo.CurrentPage, messagesFromRepo.PageSize,
               messagesFromRepo.TotalCount, messagesFromRepo.TotalPages);

            return Ok(messages);
        }



        // [HttpPost]
        //public async Task<IActionResult> CreateDonation(int userId, DonationForCreationDto donationForCreationDto)
        //{


        //     var donation = _mapper.Map<Donation>(DonationForCreationDto);

        //     _repo.Add(donation);

        //     if (await _repo.SaveAll())
        //    {
        //        var messageToReturn = _mapper.Map<MessageToReturnDto>(donation);
        //        return CreatedAtRoute("GetMessage", 
        //            new {userId, id = message.Id}, messageToReturn);
        //    }

        //     throw new Exception("Creating the message failed on save");
        //}


    }
 } 