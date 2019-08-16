using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EditorContentSaveInDB.Models;

namespace EditorContentSaveInDB.Controllers
{
    public class EditorContentController : Controller
    {
        private readonly EditorDataContext _context;

        public EditorContentController(EditorDataContext context)
        {
            _context = context;
        }

        // GET: EditorContent
        public async Task<IActionResult> Index()
        {
            return View(await _context.EditorData.ToListAsync());
        }

        // GET: EditorContent/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorData = await _context.EditorData
                .FirstOrDefaultAsync(m => m.ContentId == id);
            if (editorData == null)
            {
                return NotFound();
            }

            return View(editorData);
        }

        // GET: EditorContent/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EditorContent/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ContentId,EditorContent")] EditorData editorData)
        {
            if (ModelState.IsValid)
            {
                _context.Add(editorData);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(editorData);
        }

        // GET: EditorContent/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorData = await _context.EditorData.FindAsync(id);
            if (editorData == null)
            {
                return NotFound();
            }
            return View(editorData);
        }

        // POST: EditorContent/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ContentId,EditorContent")] EditorData editorData)
        {
            if (id != editorData.ContentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(editorData);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EditorDataExists(editorData.ContentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(editorData);
        }

        // GET: EditorContent/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var editorData = await _context.EditorData
                .FirstOrDefaultAsync(m => m.ContentId == id);
            if (editorData == null)
            {
                return NotFound();
            }

            return View(editorData);
        }

        // POST: EditorContent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var editorData = await _context.EditorData.FindAsync(id);
            _context.EditorData.Remove(editorData);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EditorDataExists(int id)
        {
            return _context.EditorData.Any(e => e.ContentId == id);
        }
    }
}
