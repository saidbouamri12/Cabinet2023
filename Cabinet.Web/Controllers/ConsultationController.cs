using Cabinet.Core.Models;
using Cabinet.EF.DbContext;
using Microsoft.AspNetCore.Mvc;

namespace Cabinet.Web.Controllers
{
	public class ConsultationController : Controller
	{
        private readonly ApplicationDbContext _context;
        public ConsultationController(ApplicationDbContext context)
		{
			_context = context;
		}
		public IActionResult IndexCosultationbypaitent(int id)
		{

			return View();
		}

		public IActionResult Create(int id)
		{

			Consultation con = _context.Consultation.Where(x=>x.AppointmentID == id).FirstOrDefault();
			if (con == null)
			{
				ViewBag.action = "Create";
                    return View();
                
                
            }
            ViewBag.action = "Edit";
            return View(con);
		}
		[HttpPost]
		public IActionResult Create(int id,Consultation con)
		{
            Consultation conn = _context.Consultation.Where(x => x.AppointmentID == id).FirstOrDefault();
            ModelState.Remove("Appointment");
            if (conn == null)
            {
				ModelState.Remove("Appointment");
				con.ConsultationDate = DateTime.Now;
				con.AppointmentID = id;
                if (ModelState.IsValid)
                {
					con.AppointmentID = id;
                    _context.Consultation.Add(con);
					_context.SaveChanges();
                    return RedirectToAction("Index", "WaitingRoom");
                }
				else
				{
					return View(con);
				}
               // _context.Consultation.Add(con);
				
            }
			else
			{
				if (ModelState.IsValid)
				{
					conn.ConsultationNotes = con.ConsultationNotes;
					conn.Title = con.Title;
					_context.SaveChanges();
                    return RedirectToAction("index", "WaitingRoom");
                }
				else
				{
                    return View(con);
                }
            }
            
		}
		public IActionResult Detail(int id,Consultation con)
		{
            Consultation conn = _context.Consultation.Where(x => x.AppointmentID == id).FirstOrDefault();
            return View(conn);
		}


	}
}
