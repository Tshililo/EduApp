using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EduApp.Models;

namespace EduApp.Controllers
{
    public class HomeController : Controller
    {
		EduNationEntities db = new EduNationEntities();
		public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            
            return View();    
        }

		#region BurialRecordApplication
		public ActionResult ApplicationsUpdateEntryToForm(Guid ObjId, Profile model)
		{
			var mymodel = db.Profiles;
			var ApplicationsInfo = mymodel.Where(s => s.ObjId == ObjId).FirstOrDefault();

			if (ApplicationsInfo == null)
			{
				model.ObjId = ObjId;
				return PartialView("CreateApplicationsEditPartial", model);
			}

			if (ApplicationsInfo != null)
			{

				return PartialView("CreateApplicationsEditPartial", ApplicationsInfo);
			}

			return null;

		}

		public ActionResult ApplicationsGridViewPartial()
		{
			var model = db.Profiles;

			// DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
			return PartialView("GridViewPartialView", model.ToList());
		}

		public ActionResult WriteExersizePopUp()
		{
			ExcersizeModel model = new ExcersizeModel();
			return PartialView("WriteExersizeEdit", model);
		}

		public ActionResult StudyMaterials()
		{
			ExcersizeModel model = new ExcersizeModel();
			return PartialView("StudyMaterials", model);
		}



		public ActionResult ApplicationsEdit(Profile item)
		{
			var modelRepo = db.Profiles;
			var exists = modelRepo.Where(c => c.ObjId == item.ObjId).SingleOrDefault();
			Profile Tosave = new Profile();
			if (exists == null)
			{
				modelRepo.Add(item);
				db.SaveChanges();
			}
			if (exists != null)
			{

				//this.UpdateModel(item);
				exists.IdNo = item.IdNo;
				modelRepo.Attach(exists);
				db.SaveChanges();
			}

			// DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
			return PartialView("GridViewPartialView", modelRepo.ToList());

		}

		[HttpPost, ValidateInput(false)]
		public ActionResult ApplicationsDelete(Guid ObjId)
		{

			var model = db.Profiles;
			if (ObjId != null)
			{
				try
				{
					var item = model.FirstOrDefault(it => it.ObjId == ObjId);
					if (item != null)
					{
						model.Remove(item);

					}

					db.SaveChanges();
				}
				catch (Exception e)
				{
					ViewData["EditError"] = e.Message;
				}
			}
			var BurialRecords = db.Profiles.ToList();
			// DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
			return PartialView("GridViewPartialView", BurialRecords);
		}
		#endregion

	}
}