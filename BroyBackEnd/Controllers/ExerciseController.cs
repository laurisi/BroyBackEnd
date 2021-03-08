using BroyBackEnd.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace BroyBackEnd.Controllers
{
    [System.Web.Http.Authorize]
    public class ExerciseController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public IQueryable<ExerciseViewModel> GetDiary()
        {
            return db.Exercises.ToList().Select(i => new ExerciseViewModel
            {
                Id = i.Id,
                NameOfExercise = i.NameOfExercise,
                Number = i.Number,
            }).AsQueryable();
        }

        public async Task<IHttpActionResult> PostExercise([FromBody] Exercise _exercise)
        {
            string userId = User.Identity.GetUserId();
            _exercise.UserId = userId;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var exercise = new Exercise()
            {
                NameOfExercise = _exercise.NameOfExercise,
                Number = _exercise.Number,
            };
            db.Exercises.Add(exercise);
            await db.SaveChangesAsync();
            return StatusCode(HttpStatusCode.Created);
        }
    }
}