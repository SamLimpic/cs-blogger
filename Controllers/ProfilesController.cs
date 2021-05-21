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
    public class ProfilesController : ControllerBase
    {
        private readonly ProfilesService _service;



        private readonly AccountsService _acctService;



        public ProfilesController(ProfilesService service, AccountsService acctsService)
        {
            _service = service;
            _acctService = acctsService;
        }



        [HttpGet("{id}")]
        public ActionResult<Profile> GetById(string id)
        {
            try
            {
                Profile found = _service.GetById(id);
                return Ok(found);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("{id}/blogs")]
        public ActionResult<IEnumerable<Blog>> GetBlogsByProfileId(string id)
        {
            try
            {
                IEnumerable<Blog> blogs = _service.GetBlogsByProfileId(id);
                return Ok(blogs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("{id}/comments")]
        public ActionResult<IEnumerable<Comment>> GetCommentsByProfileId(string id)
        {
            try
            {
                IEnumerable<Comment> comments = _service.GetCommentsByProfileId(id);
                return Ok(comments);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}