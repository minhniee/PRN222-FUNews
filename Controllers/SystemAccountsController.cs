using FUnews_PRN_ver2.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FUnews_PRN_ver2.Controllers
{
	public class SystemAccountsController : Controller
	{
		private readonly FunewsManagementContext _context;

		public SystemAccountsController(FunewsManagementContext context)
		{
			_context = context;
		}

		// GET: SystemAccounts
		public async Task<IActionResult> Index()
		{
			return View(await _context.SystemAccounts.ToListAsync());
		}

		// GET: SystemAccounts/Details/5
		public async Task<IActionResult> Details(short? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var systemAccount = await _context.SystemAccounts
				.FirstOrDefaultAsync(m => m.AccountId == id);
			if (systemAccount == null)
			{
				return NotFound();
			}

			return View(systemAccount);
		}

		// GET: SystemAccounts/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: SystemAccounts/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
		{
			if (ModelState.IsValid)
			{
				_context.Add(systemAccount);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(systemAccount);
		}

		// GET: SystemAccounts/Edit/5
		public async Task<IActionResult> Edit(short? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var systemAccount = await _context.SystemAccounts.FindAsync(id);
			if (systemAccount == null)
			{
				return NotFound();
			}
			return View(systemAccount);
		}

		// POST: SystemAccounts/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(short id, [Bind("AccountId,AccountName,AccountEmail,AccountRole,AccountPassword")] SystemAccount systemAccount)
		{
			if (id != systemAccount.AccountId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(systemAccount);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!SystemAccountExists(systemAccount.AccountId))
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
			return View(systemAccount);
		}

		// GET: SystemAccounts/Delete/5
		public async Task<IActionResult> Delete(short? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var systemAccount = await _context.SystemAccounts
				.FirstOrDefaultAsync(m => m.AccountId == id);
			if (systemAccount == null)
			{
				return NotFound();
			}
			Console.WriteLine(id);

			return View(systemAccount);
		}

		// POST: SystemAccounts/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(short id)
		{
			var systemAccount = await _context.SystemAccounts.FindAsync(id);
			if (systemAccount != null)
			{
				Console.WriteLine("find");
				_context.SystemAccounts.Remove(systemAccount);
			}
			Console.WriteLine(systemAccount != null);
			Console.WriteLine(id);

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool SystemAccountExists(short id)
		{
			return _context.SystemAccounts.Any(e => e.AccountId == id);
		}
	}
}
