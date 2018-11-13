using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using StackUnderflow.Business;
using StackUnderflow.Data;
using StackUnderflow.Entities;

namespace StackUnderflow.Web.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
	    private readonly CommentService _cs;

	    public CommentsController(ApplicationDbContext context, CommentService cs)
	    {
            _context = context;
	        _cs = cs;
        }

        [HttpPost]
        public IActionResult CommentOnAnswer([FromBody] Comment comment)
        {
	        if (!ModelState.IsValid) return BadRequest();
	        comment.CreatedBy = "123";

	        _cs.Create(comment);
	        return Ok(comment);
        }
    }
}
