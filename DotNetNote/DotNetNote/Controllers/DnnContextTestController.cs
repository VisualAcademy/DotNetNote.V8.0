﻿using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DotNetNote.Controllers;

public class DnnContextTestController(DotNetNoteContext context) : Controller
{
    public IActionResult Index()
    {
        var ideas = context.Ideas.ToList();
        return View(ideas);
    }
}
