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
    public class CommentsController : Controller
    {
        private readonly ApplicationDbContext _context;
	    private readonly CommentService _cs;
	    private readonly UserManager<IdentityUser> _um;

	    public CommentsController(UserManager<IdentityUser> um, ApplicationDbContext context, CommentService cs)
	    {
		    _um = um;
            _context = context;
	        _cs = cs;
        }

        // POST: Comments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CommentOnAnswer([Bind("Id,AnswerId,Body,CreatedBy,CreatedDate")] Comment comment)
        {
	        if (!ModelState.IsValid) return BadRequest();
	        var userId = _um.GetUserId(HttpContext.User);
	        comment.CreatedBy = userId;

	        _cs.Create(comment);
	        return Ok(comment);
        }
    }
}
