using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Models
{
    public class Game
    {
        public int Id { get; set; }
        public bool Completed { get; set; }
    }
}
