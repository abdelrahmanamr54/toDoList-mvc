using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Xml.Linq;
using ToDoList.Data;
using ToDoList.Models;

namespace ToDoList.Controllers
{
    public class TodoitemsController : Controller
    {
        ApplicationDbContext context = new ApplicationDbContext();

        public IActionResult add()
        {

            return View();
        }
        public IActionResult add_user(string name)
        {

            CookieOptions cookieOptions = new CookieOptions();
            cookieOptions.Expires = DateTimeOffset.Now.AddHours(1);
            Response.Cookies.Append("name", name);
            ViewData["user"]=name;
            //var existinguser = context.person.Single(x => x.Name == name);
           
            

           var newuser = context.person.Add(new Models.Person
                {
                    Name = name
                });
            context.SaveChanges();

            var list = context.items.Where(e=>e.person.Name==name).ToList();
            return View("items", list);

        }


        public IActionResult items()


        {
            ViewData["user"] = Request.Cookies["name"];

            var item = context.items.Where(e => e.person.Name == ViewData["user"]).ToList();
            return View(item);
            //string? note = "ahmed";

            //Response.Cookies.Append("name", name);
            //HttpContext.Session.SetString("name", name);
            //TempData["note"] = note;
            string? note = "taha";

            Response.Cookies.Append("note", note);
        }


        public IActionResult createnew()
        {

            ViewData["msg"] = Request.Cookies["note"];
            ViewData["user"] = Request.Cookies["name"];

            return View();
        }
        public IActionResult test()
        {
            ViewData["msg"] = Request.Cookies["note"];
            return View();
        }


        public IActionResult savenew(string input_title, string input_description, DateTime input_deadline)
        {
            ViewData["user"] = Request.Cookies["name"];
            var username = ViewData["user"];
            Person thatperson = context.person.Single(e => e.Name == ViewData["user"]);
           

            context.items.Add(new Models.Item
            {
                Title = input_title,
                Description = input_description,
                Deadline = input_deadline,
              personId=thatperson.Id

            });
            context.SaveChanges();
            return View("createnew");
        }
        public IActionResult edit(int id)

        {
            var selectedtask = context.items.Single(e => e.Id == id);
            ViewData["msg"] = Request.Cookies["note"];

            return View(selectedtask);

        }
        [HttpPost]
        public ActionResult edit(Models.Item item)
        {
            var selectedtask = context.items.Find(item.Id);
            if (selectedtask is not null)

            {
                selectedtask.Title = item.Title;

                selectedtask.Description= item.Description;
                selectedtask.Deadline = item.Deadline;
                context.SaveChanges();
            }
        
            return RedirectToAction("items");

        }
        public ActionResult delete(Models.Item delitem)
        {
            var selectedtask = context.items.Find(delitem.Id);
            if (selectedtask is not null)

            {
                context.items.Remove(selectedtask);
                context.SaveChanges();
            }

            return RedirectToAction("items");

            //if (ModelState.IsValid)
            //{

            //    context.Entry(product).State = EntityState.Modified;
            //    context.SaveChanges();

            //    return RedirectToAction("items"); 
            //}

            //return View(product);

        }

    }



    }
