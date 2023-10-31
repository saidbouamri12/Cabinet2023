using Cabinet.Core.Models;
using Cabinet.Core.modelview;
using Cabinet.EF.DbContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Cabinet.Web.Controllers
{
	public class PackController : Controller
	{
		public readonly ApplicationDbContext _Context;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		public PackController(ApplicationDbContext Context,UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager) 
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_Context =Context;
			_roleManager = roleManager;
		}
		public IActionResult Index()
		{
            var dossier = (from pack in _Context.Packs
                           join pai in _Context.patients on pack.idpatient equals pai.PatientID
                           //join payment in _Context.Payments on pack.Id equals payment.packid
                           join typepack in _Context.typePacks on pack.idtypepack equals typepack.id
                           select new packpatientview
                           {
                               Packid = pack.Id,
                               patient = _Context.patients.Where(x => x.PatientID == pai.PatientID).FirstOrDefault(),
                               user = _Context.Users.Where(x => x.Id == pack.doctorid).FirstOrDefault(),
                               TypePack = _Context.typePacks.Where(x => x.id == pack.idtypepack).FirstOrDefault(),
                               payment = _Context.Payments.Where(x => x.packid == pack.Id).FirstOrDefault(),
                               appointment = _Context.Appointment.Where(x => x.packid == pack.Id).ToList()
                           }).ToList();
            return View(dossier);
		}
		public async Task<IActionResult> Create()
		{
			ViewBag.typepack = _Context.typePacks.ToList();
			ViewBag.Patient = _Context.patients.ToList();
			var doctorRole = await _roleManager.FindByNameAsync("doctor");
			if(doctorRole==null)
			{
				return NotFound();
			}
			ViewBag.doctorUser = await _userManager.GetUsersInRoleAsync("Doctor");
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Create(Pack pack)
		{
			ModelState.Remove("TypePack");
			ModelState.Remove("Patient");
			ModelState.Remove("ApplicationUser");
			ModelState.Remove("Payments");
			if(ModelState.IsValid)
			{
				try
				{
					Pack pack1 = new Pack();
					pack1.doctorid = pack.doctorid;
					pack1.idpatient = pack.idpatient;
					pack1.idtypepack = pack.idtypepack;

					_Context.Packs.Add(pack1);
					_Context.SaveChangesAsync();
					TempData["success"] = "Ajouter dossier maladie success";
					return RedirectToAction("Index");
				}
				catch(Exception ex)
				{
					return BadRequest(ex.ToString());
				}
            }
			return View(pack);
		}
		public IActionResult dossier_médical(int id)
		{
			var dossier = (from pack in _Context.Packs
						   join pai in _Context.patients on pack.idpatient equals pai.PatientID
						   //join payment in _Context.Payments on pack.Id equals payment.packid
						   join typepack in _Context.typePacks on pack.idtypepack equals typepack.id
						   where pack.Id == id
						   select new packpatientview
						   {
							   Packid = pack.Id,
							   patient = (from patient in _Context.patients
										  join pack in _Context.Packs
				on patient.PatientID equals pack.idpatient
										  join appoi in _Context.Appointment

							   on pack.Id equals appoi.packid
										  select patient

							   ).FirstOrDefault(),
							   user = _Context.Users.Where(x => x.Id == pack.doctorid).FirstOrDefault(),
							   TypePack = _Context.typePacks.Where(x => x.id == pack.idtypepack).FirstOrDefault(),
							   payment = _Context.Payments.Where(x => x.packid == pack.Id).FirstOrDefault(),
							   appointment = _Context.Appointment.Where(x=>x.packid == pack.Id).ToList()
						   }).ToList();
			return View(dossier);
		}

		public async Task<IActionResult> Edit(int id)
		{
			ViewBag.typepack = _Context.typePacks.ToList();
			ViewBag.Patient = _Context.patients.ToList();
			var doctorRole = await _roleManager.FindByNameAsync("doctor");
			if (doctorRole == null)
			{
				return NotFound();
			}
			ViewBag.doctorUser = await _userManager.GetUsersInRoleAsync("Doctor");
			var pack = _Context.Packs.Where(x=>x.Id == id).FirstOrDefault();
			if(pack== null)
			{
				return NotFound();
			}
			return View(pack);
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(int id , Pack pack)
		{
            ModelState.Remove("TypePack");
            ModelState.Remove("Patient");
            ModelState.Remove("ApplicationUser");
            ModelState.Remove("Payments");
			ModelState.Remove("idpatient");
			var pack1 = _Context.Packs.Where(x=>x.Id==id).FirstOrDefault();
			if (pack1== null)
			{
				return NotFound();
			}
            if (ModelState.IsValid)
			{
				try
				{
                    pack1.doctorid = pack.doctorid;
                    //pack1.idpatient = pack.idpatient;
                    pack1.idtypepack = pack.idtypepack;
                    _Context.SaveChanges();
					TempData["success"] = "pack edit";
					return RedirectToAction("Index");
                }
				catch(Exception ex)
				{ 
					return BadRequest(ex.Message);
				}
			}
			return View(pack);
		}
		[HttpPost]
		public IActionResult Delete(int id)
		{
			var pack1 = _Context.Packs.Where(x => x.Id == id).FirstOrDefault();
			if (pack1 == null)
			{
				return NotFound();
			}
			_Context.Packs.Remove(pack1);
			_Context.SaveChanges();
			TempData["success"] = "Pack Deleted";
			return RedirectToAction("Index");
		}
	}
}
