using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class GameSession
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int Order { get; set; }
    }
}
