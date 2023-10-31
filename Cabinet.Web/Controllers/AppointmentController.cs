using Cabinet.Core.Models;
using Cabinet.Core.modelview;
using Cabinet.EF.DbContext;
using Cabinet.EF.Migrations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cabinet.Web.Controllers
{
	public class AppointmentController : Controller
	{

		public readonly ApplicationDbContext _Context;
		private readonly ILogger<AppointmentController> _logger;


		public AppointmentController(ApplicationDbContext Context, ILogger<AppointmentController> logger)
		{
			_Context = Context;
			_logger = logger;
		}
		public IActionResult Index(string? daterange)
		{
			bool success = false;
			bool success1 = false;
			string startDate = null;
			string endDate = null;
			DateTime dateTime;
			string dateRange = daterange;
			if (dateRange != null)
			{
				string[] dates = dateRange.Split(new[] { " - " }, StringSplitOptions.None);

				startDate = dates[0]; // "06/07/2023"
				
				endDate = dates[1]; // "08/25/2023"
			}
			

			 success = DateTime.TryParse(startDate, out dateTime);
			 success1 = DateTime.TryParse(endDate, out dateTime);

			if (success && success1)
			{
				var appointments = _Context.Appointment
					.Where(k => k.AppointmentDate >= DateTime.Parse(startDate) && k.AppointmentDate <= DateTime.Parse(endDate))
					.ToList();

				var appointmentViews = appointments.Select(c => new Appointment
				{
					AppointmentID = c.AppointmentID,
					AppointmentDate = c.AppointmentDate,
					//AppointmentType = c.AppointmentType,
					AppointmentStatus = c.AppointmentStatus,
					AppointmentTime = c.AppointmentTime,
                    patient = (from patient in _Context.patients join pack in _Context.Packs
							   on patient.PatientID equals pack.idpatient join appoi in _Context.Appointment
							   on pack.Id equals appoi.packid select patient
							   ).FirstOrDefault(),
				}).ToList();
				return View("index", appointmentViews);
			}
			var list = _Context.Appointment.Where(k=> k.AppointmentDate >= DateTime.Today).ToList();
			var appointmentto = list.Select(c => new Appointment
			{
				AppointmentID = c.AppointmentID,
				AppointmentDate = c.AppointmentDate,
				//AppointmentType = c.AppointmentType,
				AppointmentStatus = c.AppointmentStatus,
				AppointmentTime = c.AppointmentTime,
                patient = (from patient in _Context.patients join pack in _Context.Packs
                on patient.PatientID equals pack.idpatient join appoi in _Context.Appointment

                               on pack.Id equals appoi.packid select patient

                               ).FirstOrDefault(),
            }).ToList();
			return View(appointmentto);
		}
		public IActionResult Create()
		{
			var appointments = _Context.Appointment.ToList();
			Appointment t = new Appointment();
			var app = appointments.Select(c => new Appointment
			{
				AppointmentID = c.AppointmentID,
				AppointmentDate = c.AppointmentDate,
				//AppointmentType = c.AppointmentType,
				AppointmentStatus = c.AppointmentStatus,
				AppointmentTime = c.AppointmentTime,
				//Patient = _Context.patients.FirstOrDefault(t => t.PatientID == c.PatientID)
			}).ToList();
			ViewBag.Appointment = app;
			ViewBag.paitent = _Context.patients.ToList();
			return View(t);
		}
		[HttpPost]
		public IActionResult Create(Appointment appointment)
		{
			ModelState.Remove("Patient");
			ModelState.Remove("Pack");
			ModelState.Remove("Consultation");
			ModelState.Remove("WaitingRoom");

			//ModelState.Remove("PatientID");
			if(!ModelState.IsValid)
			{
				var appointments = _Context.Appointment.ToList();
				var app = appointments.Select(c => new Appointment
				{
					AppointmentID = c.AppointmentID,
					AppointmentDate = c.AppointmentDate,
					//AppointmentType = c.AppointmentType,
					AppointmentStatus = c.AppointmentStatus,
					AppointmentTime = c.AppointmentTime,
					patient = (from patient in _Context.patients join pack in _Context.Packs
					on patient.PatientID equals pack.idpatient join appoi in _Context.Appointment


							   on pack.Id equals appoi.packid select patient


							   ).FirstOrDefault(),
				}).ToList();
				ViewBag.Appointment = app;
				TempData["error"] = "check input";

				return View(appointment);

				
			}
			else
			{
				_Context.Appointment.Add(appointment);
				_Context.SaveChanges();
				TempData["success"] = "Add appointnment";
				return RedirectToAction("index", "Appointment");
			}
			
		}

		public IActionResult Edit(int id)
		{
			var appointments = _Context.Appointment.Where(x => x.AppointmentID == id).FirstOrDefault();
            if (appointments == null)
            {
                // Handle the case when the appointment is not found
                return NotFound();
            }

            return View(appointments);
		}
		[HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Appointment appt)
		{
            if (id != appt.AppointmentID)
            {
                // Handle the case when the id in the URL doesn't match the appointment id
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var appointments = _Context.Appointment.Where(x => x.AppointmentID == id).FirstOrDefault();

                appointments.AppointmentStatus = appt.AppointmentStatus;
				appointments.AppointmentDate = appt.AppointmentDate;
				appointments.AppointmentTime = appt.AppointmentTime;
				//appointments.AppointmentType	= appt.AppointmentType;
				TempData["success"] = "appointment updated";
				_Context.SaveChanges();
				return RedirectToAction("index", "Appointment");
			}

            // If the model state is invalid, return the view with the validation errors
            return View(appt);
        }
		
		public IActionResult Details(int id)
		{
			var appointments = _Context.Appointment.Where(x => x.AppointmentID == id).FirstOrDefault();
			if (appointments == null)
			{
				// Handle the case when the appointment is not found
				return NotFound();
			}

			return View(appointments);
			
		}

		[HttpPost]
		public IActionResult Delete(int id)
		{
			var appointments = _Context.Appointment.Where(x => x.AppointmentID == id).FirstOrDefault();
			if (appointments == null)
			{
				// Handle the case when the appointment is not found
				return NotFound();
			}
			else
			{
				_Context.Appointment.Remove(appointments);
				_Context.SaveChanges();
				TempData["success"] = "appoi deleted";
			}
			return RedirectToAction("Index");	
		}

		public IActionResult rendezvous_historique(int id)
		{
			var appointment = (from appoi in _Context.Appointment
							  join pack in _Context.Packs on appoi.packid equals pack.Id
							  where pack.idpatient == id
							  select new Appointmentview
							  {
								  AppointmentID = appoi.AppointmentID,
								  PatientID = pack.idpatient,
								  AppointmentDate = appoi.AppointmentDate,
								  AppointmentTime = appoi.AppointmentTime,
								  AppointmentStatus = appoi.AppointmentStatus,
								  //AppointmentType = appoi.AppointmentType,
								  Patients = _Context.patients.Where(x => x.PatientID == id).FirstOrDefault(),
								  payment = _Context.Payments.Where(x=> x.packid == pack.Id).FirstOrDefault(),
								  TypePack = _Context.typePacks.Where(x=>x.id==pack.idtypepack).FirstOrDefault(),
								  consultation = _Context.Consultation.Where(x=>x.AppointmentID == appoi.AppointmentID).FirstOrDefault()
							  }).ToList();
				              
			return View(appointment);
		}

		public IActionResult Listappoi(int id)
		{
			var appointment = (from appoi in _Context.Appointment
							   join pack in _Context.Packs on appoi.packid equals pack.Id
							   where pack.Id == id
							   select new Appointmentview
							   {
								   AppointmentID = appoi.AppointmentID,
								   PatientID = pack.idpatient,
								   AppointmentDate = appoi.AppointmentDate,
								   AppointmentTime = appoi.AppointmentTime,
								   AppointmentStatus = appoi.AppointmentStatus,
								   //AppointmentType = appoi.AppointmentType,
								   Patients = _Context.patients.Where(x => x.PatientID == pack.idpatient ).FirstOrDefault(),
								   payment = _Context.Payments.Where(x => x.packid == pack.Id).FirstOrDefault(),
								   TypePack = _Context.typePacks.Where(x => x.id == pack.idtypepack).FirstOrDefault(),
								   consultation = _Context.Consultation.Where(x => x.AppointmentID == appoi.AppointmentID).FirstOrDefault()
							   }).ToList();
			//var appoi1 = _Context.Appointment.Where(x => x.packid == id).ToList();
			return View(appointment);
		}

	     public IActionResult AddAppoi(int id)
		{
			
			return View();
		}
		[HttpPost]
		public IActionResult AddAppoi(int id, Appointment appointment)
		{
			var t = _Context.Packs.Find(id);
			if (t == null)
			{
				return NotFound();
			}
			appointment.packid = id;
			ModelState.Remove("Pack");
			ModelState.Remove("Consultation");
			ModelState.Remove("WaitingRoom");
			ModelState.Remove("patient");
			if(ModelState.IsValid)
			{

				_Context.Appointment.Add(appointment);
				_Context.SaveChanges();
				TempData["success"] = "add appointment";
				return RedirectToAction("dossier_médical", "Pack", new { id = id });
			}
			TempData["error"] = "check input";
			return View(appointment);
		}

    }
}
