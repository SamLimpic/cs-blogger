using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CodeWorks.Auth0Provider;
using cs_blogger.Models;
using cs_blogger.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace cs_blogger.Controllers
{
    [ApiController]
    [Route("[controller]")]

    [Authorize]
    // STUB[epic=Auth] Adds Authguard to all routes on the whole controller

    public class AccountController : ControllerBase
    {
        private readonly AccountsService _service;
        private readonly BlogsService _blogService;
        private readonly CommentsService _commentService;


        public AccountController(AccountsService service, BlogsService blogService, CommentsService commentService)
        {
            _service = service;
            _blogService = blogService;
            _commentService = commentService;
        }



        [HttpGet]
        public async Task<ActionResult<Account>> Get()
        // NOTE asyncronous actions must include "Task" before ActionResult
        {
            try
            {
                // STUB[epic=Auth] Replaces req.userinfo
                // NOTE HOW TO GET ACTIVE USERS
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account currentUser = _service.GetOrCreateAccount(userInfo);
                return Ok(currentUser);
            }
            catch (Exception error)
            {
                return BadRequest(error.Message);
            }
        }



        [HttpGet("blogs")]
        public async Task<ActionResult<IEnumerable<Blog>>> GetBlogsByCreatorId()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account currentUser = _service.GetOrCreateAccount(userInfo);
                IEnumerable<Blog> blogs = _blogService.GetBlogsByCreatorId(currentUser.Id);
                return Ok(blogs);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpGet("comments")]
        public async Task<ActionResult<IEnumerable<Comment>>> GetCommentsByCreatorId()
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                Account currentUser = _service.GetOrCreateAccount(userInfo);
                IEnumerable<Comment> comments = _commentService.GetCommentsByCreatorId(currentUser.Id);
                return Ok(comments);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }



        [HttpPut]
        public async Task<ActionResult<Account>> Update([FromBody] Account edit)
        {
            try
            {
                Account userInfo = await HttpContext.GetUserInfoAsync<Account>();
                edit.Id = userInfo.Id;
                Account update = _service.Update(edit, userInfo);
                return Ok(update);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}