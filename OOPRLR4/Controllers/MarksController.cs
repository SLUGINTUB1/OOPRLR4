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
    public class MarksController : Controller
    {
        private readonly OOPRLR4Context _context;

        public MarksController(OOPRLR4Context context)
        {
            _context = context;
        }

        // GET: Marks
        public async Task<IActionResult> Index()
        {
            var oOPRLR4Context = _context.Mark.Include(m => m.Exam);
            return View(await oOPRLR4Context.ToListAsync());
        }

        // GET: Marks/Group
        //11. Продемонструвати групування даних за допомогою оператора group.
        public IActionResult Group()
        {

            var marksCountByExam = _context.Mark.Include(m => m.Exam)
                   .GroupBy(v => v.Value)
                   .Select(g => new Mark
                   {
                       Value = g.Key,
                       SubjectIdent = g.Count(a => a.ExamId!=0?true:false)
                   }
                   );

            foreach (var m in marksCountByExam)
            {
                Console.WriteLine($"{m.Value}: {m.SubjectIdent}");
            }

            return RedirectToAction(nameof(Index));
        }

        // GET:Marks/MarksLeftJoin
        //18. Продемонструвати операції з множинами: різниця.
        public IActionResult MarksLeftJoin()
        {
            var marks = _context.Mark;
            var exams = _context.Exam;

            var query = marks.GroupJoin(exams, mark => mark.ExamId, exam => exam.ExamId,
               (mark, exam) => new { mark, subgroup = exam.DefaultIfEmpty() })
               .Select(gj => new
               {
                   gj.mark.Value,
                   exam = gj.subgroup.FirstOrDefault().Date
               });


            foreach (var v in query)
            {
                Console.WriteLine($"{v.Value}: {v.exam}");
            }

            return RedirectToAction(nameof(Index));
        }


        // GET: Marks/MarksFullJoin
        //17. Продемонструвати з'єднання таблиць.
        //18. Продемонструвати операції з множинами: об'єднання
        //13. При формуванні запитів використати анонімні типи.
        //1. Продемонструвати агрегатні операції
        public IActionResult MarksFullJoin()
        {
            var marks = _context.Mark;
            var exams = _context.Exam;

            var queryLeftJoin = marks.GroupJoin(exams, mark => mark.ExamId, exam => exam.ExamId,
               (mark, exam) => new { mark, subgroup = exam.DefaultIfEmpty() })
               .Select(gj => new
               {
                   gj.mark.Value,
                   exam = gj.subgroup.FirstOrDefault().Date
               });

            var queryRightJoin = exams.GroupJoin(marks, exam => exam.ExamId, mark => mark.ExamId,
               (exam, mark) => new { exam, subgroup = mark.DefaultIfEmpty() })
               .Select(gj => new
               {
                   gj.subgroup.FirstOrDefault().Value,
                   exam = gj.exam.Date
               });

            var FullOuterJoin = queryRightJoin.Union(queryLeftJoin);

            foreach (var v in FullOuterJoin)
            {
                Console.WriteLine($"{v.Value}: {v.exam}");
            }

            double averageGrade = _context.Mark.Average(t => t.Value);
            Console.WriteLine($"Avarage mark: {averageGrade}");

            return RedirectToAction(nameof(Index));
        }



        // GET: Marks/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Mark
                .Include(m => m.Exam)
                .FirstOrDefaultAsync(m => m.MarkId == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // GET: Marks/Create
        public IActionResult Create()
        {
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId");
            return View();
        }

        // POST: Marks/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MarkId,Value,SubjectIdent,Evaluated,ExamId")] Mark mark)
        {
           // if (ModelState.IsValid)
            //{
                _context.Add(mark);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            //ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", mark.ExamId);
            //return View(mark);
        }

        // GET: Marks/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Mark.FindAsync(id);
            if (mark == null)
            {
                return NotFound();
            }
            ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", mark.ExamId);
            return View(mark);
        }

        // POST: Marks/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MarkId,Value,SubjectIdent,Evaluated,ExamId")] Mark mark)
        {
            if (id != mark.MarkId)
            {
                return NotFound();
            }

            //if (ModelState.IsValid)
            //{
                try
                {
                    _context.Update(mark);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkExists(mark.MarkId))
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
            //ViewData["ExamId"] = new SelectList(_context.Exam, "ExamId", "ExamId", mark.ExamId);
            //return View(mark);
        }

        // GET: Marks/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mark = await _context.Mark
                .Include(m => m.Exam)
                .FirstOrDefaultAsync(m => m.MarkId == id);
            if (mark == null)
            {
                return NotFound();
            }

            return View(mark);
        }

        // POST: Marks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mark = await _context.Mark.FindAsync(id);
            if (mark != null)
            {
                _context.Mark.Remove(mark);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkExists(int id)
        {
            return _context.Mark.Any(e => e.MarkId == id);
        }
    }
}
