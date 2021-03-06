﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ReactAndDotNet.Models;

namespace ReactAndDotNet.Controllers
{
    [Route("api/highscores")]
    public class ValuesController : Controller
    {

        public static List<string> Names = new List<string>{
                    "Rafiki",
                    "Mufusa",
                    "Nala",
                    "Simba",
                    "Timon",
                    "Pumbaa",
                    "Zazu"
                };
       

        [HttpGet]
        public ScoreDetails Get()
        {
            // Mock High Scores 
            var scores = Names
                .Select(s => new Result { Name = s, Score = new Random(s.GetHashCode() * DateTime.Now.Millisecond).Next(0, 100) })
                .OrderByDescending(o => o.Score);
            return new ScoreDetails { Scores = scores };
        }

        [HttpPost]
        public ScoreDetails Post([FromBody] Result data)
        {
            // Mock High Scores 
            // get "current scores"
            var scores = Names
                .Select(s => new Result { Name = s, Score = new Random(s.GetHashCode() * DateTime.Now.Millisecond).Next(0, 100) })
                .Where(w => w.Name != data.Name)
                .ToList();

            if (!(Names.Any(a => a == data.Name)))  {
                 Names.Add(data.Name);
            }

            scores.Add(new Result{ Name = data.Name, Score = data.Score});
            return new ScoreDetails { Scores = scores.OrderByDescending(o => o.Score) };
        }
}