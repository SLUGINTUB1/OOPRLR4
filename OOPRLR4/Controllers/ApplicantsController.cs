using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OOPRLR4.Data;
using OOPRLR4.Models;

namespace OOPRLR4.Controllers
{
    public class ApplicantsController : Controller
    {
        private readonly OOPRLR4Context _context;

        public ApplicantsController(OOPRLR4Context context)
        {
            _context = context;
        }

        // GET: Applicants
        //16. Продемонструвати сортування даних в таблицях
        //12. Продемонструвати роботу оператора join
        //18. Продемонструвати операції з множинами: перетин
        //14. Сформувати запити засобами методів розширення
        //15. Використати методи запитів при формуванні запитів до бази даних (наприклад,інформація про студента з максимальною / мінімальною оцінкою).
        public async Task<IActionResult> Index()
        {
            var oOPRLR4Context = _context.Applicant.Include(a => a.Exam).OrderBy(a => a.Name);
            return View(await oOPRLR4Context.ToListAsync());
        }


        // GET: Applicants/Details/5
        //10. Використовуючи оператор where забезпечити відображення інформації, відібраної за певним критерієм
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }


            var applicant = await _context.Applicant
                .Include(a => a.Exam)
                .Where(m => m.ApplicantId == id)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // GET: Applicants/Create
        public IActionResult Create()
        {
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ApplicantId,Name,Gender,Email,PhoneNumber,ExamId")] Applicant applicant)
        {
            //if (ModelState.IsValid)
            //{
                _context.Add(applicant);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", applicant.ExamId);
            // View(applicant);
        }

        // GET: Applicants/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant == null)
            {
                return NotFound();
            }
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", applicant.ExamId);
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ApplicantId,Name,Gender,Email,PhoneNumber,ExamId")] Applicant applicant)
        {
            if (id != applicant.ApplicantId)
            {
                return NotFound();
            }

           // if (ModelState.IsValid)
           // {
                try
                {
                    _context.Update(applicant);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApplicantExists(applicant.ApplicantId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", applicant.ExamId);
            //return View(applicant);
        }

        // GET: Applicants/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var applicant = await _context.Applicant
                .Include(a => a.Exam)
                .FirstOrDefaultAsync(m => m.ApplicantId == id);
            if (applicant == null)
            {
                return NotFound();
            }

            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var applicant = await _context.Applicant.FindAsync(id);
            if (applicant != null)
            {
                _context.Applicant.Remove(applicant);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApplicantExists(int id)
        {
            return _context.Applicant.Any(e => e.ApplicantId == id);
        }
    }
}
