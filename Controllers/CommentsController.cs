using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using cs_blogger.Models;
using cs_blogger.Services;
using CodeWorks.Auth0Provider;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cs_blogger.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentsService _service;



        private readonly AccountsService _acctService;



        public CommentsController(CommentsService service, AccountsService acctsService)
        {
            _service = service;
            _acctService = acctsService;
        }



        [HttpGet("{id}")]
        public ActionResult<Comment> GetById(int id)
        {
            try
            {
                Comment found = _service.GetById(id);
                return Ok(found);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Comment>> Create([FromBody] Comment newComment, int blogId)
        {
            try
            {
                // TODO[epic=Auth] Get the user info to set the creatorID
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                // safety to make sure an account exists for that user before CREATE-ing stuff.
                Account fullAccount = _acctService.GetOrCreateAccount(userInfo);
                newComment.CreatorId = userInfo.Id;
                newComment.BlogId = blogId;

                Comment comment = _service.Create(newComment);
                //TODO[epic=Populate] adds the account to the new object as the creator
                comment.Creator = fullAccount;
                return Ok(comment);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Comment>> Update([FromBody] Comment edit, int id)
        {
            try
            {
                edit.Id = id;
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Comment update = _service.Update(edit, userInfo.Id);
                return Ok(update);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Comment>> Delete(int id)
        {
            try
            {
                // TODO[epic=Auth] Get the user info to set the creatorID
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                // safety to make sure an account exists for that user before DELETE-ing stuff.
                _service.Delete(id, userInfo.Id);
                return Ok("Successfulyl Deleted!");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}