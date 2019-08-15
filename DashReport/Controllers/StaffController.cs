using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DashReport.Data;
using DashReport.Data.Model;
using DashReport.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace DashReport.Controllers
{
    [Route("Staff")]
    public class StaffController : Controller
    {
        private readonly IStaffRepository _staffRepository;
        public StaffController(IStaffRepository staffRepository)
        {
            _staffRepository = staffRepository;

        }

        [HttpGet]
        [ActionName("List")]
        public async Task<ActionResult<List<StaffViewModel>>> List()
        {
            var staffVM = new List<StaffViewModel>(); ///Task<ActionResult<List<StaffViewModel>>>

            var staffs = await _staffRepository.GetAll();

            if (staffs.Count() == 0)
            {
                return View("Empty");
            }

            foreach (var staff in staffs)
            {
                staffVM.Add(new StaffViewModel
                {
                    Staff = staff,

                });
            }
            return View(staffVM);
        }

   
        [ActionName("AddPage")]
        public ActionResult AddPage()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<Staff>> Create([FromBody]Staff modeldata)
        {
            if (modeldata == null || !ModelState.IsValid)
            {
                return View(modeldata);
            } 
            
            await _staffRepository.Save(modeldata);

            return RedirectToAction("List");
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<Staff>> Edit(int id)
        {
            var saffById = await _staffRepository.GetByID(id);

            return View(saffById);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Staff>> Edit([FromBody]Staff modeldata)
        {
            if (modeldata == null || !ModelState.IsValid)
            {
                return View(modeldata);
            }

            await _staffRepository.Edit(modeldata);

            return RedirectToAction("List");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<Staff>> Remove(int id)
        {
            //var Staffs = _staffRepository.GetByID(id);
            await _staffRepository.Delete(id);

            return RedirectToAction("List");
        }
    }
}
