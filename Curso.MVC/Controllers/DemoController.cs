using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Curso.MVC.Controllers {
    public class DemoController : Controller {
        public IActionResult Index() {
            return View();
        }
        public IActionResult ConId(int? id) {
            if (id.HasValue) {
                var lst = new List<string>();
                for (var i = 0; i < id.Value; i++) {
                    lst.Add($"Elemento {i}");
                    ViewData["listado"] = lst;
                }
            } else
                ViewData["listado"] = new List<string> { "uno", "dos", "tres" };
            ViewData["hayValor"] = id.HasValue;
            return View();
    }
}
}
