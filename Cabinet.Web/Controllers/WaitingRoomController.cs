using Cabinet.Core.Models;
using Cabinet.Core.modelview;
using Cabinet.EF.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Runtime.CompilerServices;

namespace Cabinet.Web.Controllers
{
	public class WaitingRoomController : Controller
	{
		private readonly ApplicationDbContext _context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public WaitingRoomController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<ApplicationUser> userManager)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task<IActionResult> Index()
		{
			//var indexWaitRoomData = await (from wr in _context.WaitingRoom
			//						 join a in _context.Appointment on wr.AppointmentID equals a.AppointmentID
			//						 join p in _context.patients on a.packid equals packid		 
			//							   where/* a.AppointmentDate == DateTime.Today &&*/ wr.CheckInStatus == "checked-in"
			//							   select new indexwaitroom
			//						 {
			//							 WaitingRoomID = wr.WaitingRoomID,
			//							 AppointmentID = wr.AppointmentID,
			//							 PatientID = p.PatientID,
			//							 FirstName = p.FirstName,
			//							 LastName = p.LastName,
			//							 CheckInStatus = wr.CheckInStatus,
			//							 AppointmentDate = a.AppointmentDate,
			//							 AppointmentTime = a.AppointmentTime,
			//							 AppointmentStatus = a.AppointmentStatus,
			//							 AppointmentType = a.AppointmentType,
			//							 Statuspaitent = wr.Statuspaitent
			//						 }).ToListAsync();
			//         //var waitingRooms = await _context.WaitingRoom.Where(x=>x.AppointmentTime==DateTime.Today && x.CheckInStatus == "checked-in").ToListAsync();
			//         ViewBag.paitent = await _context.patients.ToListAsync();

			var index = await (from wr in _context.WaitingRoom
							   join a in _context.Appointment on wr.AppointmentID equals a.AppointmentID
							   join packt in _context.Packs on a.packid equals packt.Id
							   join pat in _context.patients on packt.idpatient equals pat.PatientID
							   join ty in _context.typePacks on packt.idtypepack equals ty.id
							   //where a.AppointmentDate.Date == DateTime.Today.Date
							   select new indexwaitroom
							   {
								   WaitingRoomID = wr.WaitingRoomID,
								   AppointmentID = wr.AppointmentID,
								   PatientID = pat.PatientID,
								   FirstName = pat.FirstName,
								   LastName = pat.LastName,
								   namepack = ty.label,
								   CheckInStatus = wr.CheckInStatus,
								   AppointmentDate = a.AppointmentDate,
								   AppointmentTime = a.AppointmentTime,
								   AppointmentStatus = a.AppointmentStatus,
                                   // AppointmentType = a.AppointmentType,
                                   doctorid = packt.doctorid,
                                   Statuspaitent = wr.Statuspaitent
							   }).ToListAsync();



			return View(index);
		}

		[HttpPost]
		public  IActionResult Createwithapoiment(int  id)
		{
			var find = _context.Appointment.Find(id);
			if(find==null)
			{
				return NotFound();
			}
			try
			{
				WaitingRoom model = new WaitingRoom();
				model.Statuspaitent = "have appointment";
				model.AppointmentID = id;
				model.AppointmentTime = DateTime.Today;
				model.CheckInStatus = "checked-in";
				model.AppointmentTime = find.AppointmentDate;
				model.Notes = "test";
				_context.WaitingRoom.Add(model);
				_context.SaveChanges();
				TempData["success"] = "paitent ajouter on waitroom";
				return RedirectToAction("index", "WaitingRoom");
			}
			catch(Exception ex)
			{
				return BadRequest();
			}
		}

		

		[HttpPost]
        public async Task<IActionResult> Createwithnoapoiment(IFormCollection form)
		{
			try
			{
                

                string PatientID = form["PatientID"];
                bool isNumber = int.TryParse(PatientID, out int patientId);
                if (!isNumber)
                {
                    return NotFound();
                }
                var paitent = _context.patients.Find(patientId);
                if (paitent == null)
                {
                    return NotFound();

                }

                string AppointmentTime = form["AppointmentTime"];

                bool isValidTimeSpan = TimeSpan.TryParse(AppointmentTime, out TimeSpan appointmentTime);
                if (!isValidTimeSpan)
                {
                    return NotFound();
                }

                string AppointmentType = form["AppointmentType"];

                string Statuspaitent = form["Status paitent"];
                if (AppointmentType == null && Statuspaitent == null)
                {
                    return NotFound();
                }
				//var appo = _context.Appointment.Where(x => x.AppointmentDate >= DateTime.Now && x.PatientID == patientId).FirstOrDefault();
				//if(appo==null)
				//{
				//	return NotFound();
				//}
				var doctorRole = await _roleManager.FindByNameAsync("doctor");
				if (doctorRole == null)
				{
					return NotFound();
				}
				var i = form["doctorid"];
				var user = await _userManager.FindByIdAsync(i);
				//ViewBag.doctorUser = await _userManager.GetUsersInRoleAsync("Doctor");
				var isInRole = await _userManager.IsInRoleAsync(user, "Doctor");


				Pack p = new Pack();
				p.doctorid = i;
				p.idpatient = patientId;
				p.idtypepack = 1;
				
				_context.Packs.Add(p);
			    _context.SaveChanges();
				Appointment appointment = new Appointment();
				appointment.packid = p.Id;
                appointment.AppointmentDate = DateTime.Now;
                appointment.AppointmentStatus = "confirmed";
                appointment.AppointmentTime = appointmentTime;
                _context.Appointment.Add(appointment);
                _context.SaveChanges();

                WaitingRoom waiting = new WaitingRoom();

                waiting.Statuspaitent = Statuspaitent;
                waiting.AppointmentTime = DateTime.Now;
                waiting.AppointmentID = appointment.AppointmentID;
                waiting.CheckInStatus = "checked-in";
				waiting.Notes = "on oppoimentmt";
                _context.WaitingRoom.Add(waiting);
                _context.SaveChanges();
				TempData["success"] = "add paitent in waitroom";

				return RedirectToAction("index", "WaitingRoom");
            }
			catch(Exception ex)
			{
				return NotFound();
			}
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var waitroom = _context.WaitingRoom.Find(id);
			if(id==0 || waitroom == null)
			{
				return NotFound();
			}

			_context.WaitingRoom.Remove(waitroom);
			_context.SaveChanges();
			TempData["success"] = "patient remove in   Waitingroom ";
			return RedirectToAction("index", "WaitingRoom");
        }

		public IActionResult Edit(int id)
		{
			var waitroom = _context.WaitingRoom.Find(id);
			return View(waitroom);
		}
		[HttpPost]
		public IActionResult Edit(int id,WaitingRoom wat)
		{
            WaitingRoom wat1 = _context.WaitingRoom.Find(id);
			if(wat1==null)
			{
				return NotFound();
			}
			else
			{
                wat1.Statuspaitent = wat.Statuspaitent;
                wat1.Notes = wat.Notes;
                _context.SaveChanges();
				TempData["success"] = "Data updated";
				return RedirectToAction("index");
            }
               
			
		}

        public async Task<IActionResult> Detail(int id)
		{
			 //var indexWaitRoomData =                     await(from wr in _context.WaitingRoom
    //                                                     join a in _context.Appointment on wr.AppointmentID equals a.AppointmentID
    //                                                     join p in _context.patients on a.PatientID equals p.PatientID
    //                                                     where wr.WaitingRoomID == id
    //                                                     select new indexwaitroom
    //                                                     {
    //                                                         WaitingRoomID = wr.WaitingRoomID,
    //                                                         AppointmentID = wr.AppointmentID,
    //                                                         PatientID = p.PatientID,
    //                                                         FirstName = p.FirstName,
    //                                                         LastName = p.LastName,
    //                                                         CheckInStatus = wr.CheckInStatus,
    //                                                         AppointmentDate = a.AppointmentDate,
    //                                                         AppointmentTime = a.AppointmentTime,
    //                                                         AppointmentStatus = a.AppointmentStatus,
    //                                                         AppointmentType = a.AppointmentType,
    //                                                         Statuspaitent = wr.Statuspaitent
    //                                                     }).ToListAsync();
            return View(/*indexWaitRoomData*/);
		}


    }
}
