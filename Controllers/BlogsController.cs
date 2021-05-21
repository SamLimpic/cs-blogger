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
    public class BlogsController : ControllerBase
    {
        private readonly BlogsService _service;



        private readonly AccountsService _acctService;



        public BlogsController(BlogsService service, AccountsService acctsService)
        {
            _service = service;
            _acctService = acctsService;
        }



        [HttpGet]
        public ActionResult<IEnumerable<Blog>> GetAll()
        {
            try
            {
                IEnumerable<Blog> blogs = _service.GetAll();
                return Ok(blogs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("{id}")]
        public ActionResult<Blog> GetById(int id)
        {
            try
            {
                Blog found = _service.GetById(id);
                return Ok(found);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("{id}/comments")]
        public ActionResult<IEnumerable<Comment>> GetCommentsByBlogId(int id)
        {
            try
            {
                IEnumerable<Comment> comments = _service.GetCommentsByBlogId(id);
                return Ok(comments);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Blog>> Create([FromBody] Blog newBlog)
        {
            try
            {
                // TODO[epic=Auth] Get the user info to set the creatorID
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                // safety to make sure an account exists for that user before CREATE-ing stuff.
                Account fullAccount = _acctService.GetOrCreateAccount(userInfo);
                newBlog.CreatorId = userInfo.Id;

                Blog blog = _service.Create(newBlog);
                //TODO[epic=Populate] adds the account to the new object as the creator
                blog.Creator = fullAccount;
                return Ok(blog);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<Blog>> Update([FromBody] Blog edit, int id)
        {
            try
            {
                edit.Id = id;
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Blog update = _service.Update(edit, userInfo.Id);
                return Ok(update);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Blog>> Delete(int id)
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