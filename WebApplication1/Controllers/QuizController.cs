using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;


namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        ApplicationContext db;
        //Начальная проверка на наличие вопросов, если их нет, создаются 5 вопросов и по 4 ответа к ним.
        public QuizController(ApplicationContext context)
        {
            db = context;
            if (!db.Questions.Any())
            {
                db.Questions.Add(new Question { Title = "Сколько будет 4 + 4" });
                db.SaveChanges();
                db.Answers.Add(new Answer { Title = "8", QuestionId = 1, IsRight = true });
                db.Answers.Add(new Answer { Title = "6", QuestionId = 1, IsRight = false });
                db.Answers.Add(new Answer { Title = "7", QuestionId = 1, IsRight = false });
                db.Answers.Add(new Answer { Title = "9", QuestionId = 1, IsRight = false });
                db.SaveChanges();
                db.Questions.Add(new Question { Title = "Сколько будет 6 * 8" });
                db.SaveChanges();
                db.Answers.Add(new Answer { Title = "38", QuestionId = 2, IsRight = false });
                db.Answers.Add(new Answer { Title = "48", QuestionId = 2, IsRight = true });
                db.Answers.Add(new Answer { Title = "59", QuestionId = 2, IsRight = false });
                db.Answers.Add(new Answer { Title = "92", QuestionId = 2, IsRight = false });
                db.SaveChanges();
                db.Questions.Add(new Question { Title = "Сколько будет 2 + 8" });
                db.SaveChanges();
                db.Answers.Add(new Answer { Title = "11", QuestionId = 3, IsRight = false });
                db.Answers.Add(new Answer { Title = "16", QuestionId = 3, IsRight = false });
                db.Answers.Add(new Answer { Title = "10", QuestionId = 3, IsRight = true });
                db.Answers.Add(new Answer { Title = "9", QuestionId = 3, IsRight = false });
                db.SaveChanges();
                db.Questions.Add(new Question { Title = "Сколько будет 5 - 3" });
                db.SaveChanges();
                db.Answers.Add(new Answer { Title = "8", QuestionId = 4, IsRight = false });
                db.Answers.Add(new Answer { Title = "6", QuestionId = 4, IsRight = false });
                db.Answers.Add(new Answer { Title = "7", QuestionId = 4, IsRight = false });
                db.Answers.Add(new Answer { Title = "2", QuestionId = 4, IsRight = true });
                db.SaveChanges();
                db.Questions.Add(new Question { Title = "Сколько будет (1 + 7) / 2" });
                db.SaveChanges();
                db.Answers.Add(new Answer { Title = "4", QuestionId = 5, IsRight = true });
                db.Answers.Add(new Answer { Title = "3", QuestionId = 5, IsRight = false });
                db.Answers.Add(new Answer { Title = "5", QuestionId = 5, IsRight = false });
                db.Answers.Add(new Answer { Title = "12", QuestionId = 5, IsRight = false });
                db.SaveChanges();
            }
        }

        //Нахождение игровой сессии по номеру идентификатора
        [HttpGet("{GameId}")]
        public async Task<ActionResult<GameSession>> Get(int GameId)
        {
            Game game = await db.Games.FirstOrDefaultAsync(x => x.Id == GameId);
            if (game == null)
            {
                return NotFound();
            }
            else if (game.Completed == true)
            {
                return BadRequest();
            }
            else
            {
                var gameS = db.GameSessions.FromSqlRaw($"SELECT * FROM GameSessions WHERE GameId={GameId} AND AnswerId=null").ToList();
                GameSession gameSession = gameS[0];
                return Ok(gameSession);
            }
        }

    }
}
