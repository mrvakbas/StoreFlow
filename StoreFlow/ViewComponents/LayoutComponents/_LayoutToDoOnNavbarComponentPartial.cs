using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;

namespace StoreFlow.ViewComponents.LayoutComponents
{
	public class _LayoutToDoOnNavbarComponentPartial : ViewComponent
	{
		private readonly StoreContext _context;

		public _LayoutToDoOnNavbarComponentPartial(StoreContext context)
		{
			_context = context;
		}

		public IViewComponentResult Invoke()
		{
			var values = _context.Todos.Where(y => y.Status == false).OrderByDescending(x => x.TodoId).Take(5).ToList();
			ViewBag.todoTotalCount = _context.Todos.Count();	
			return View(values);
		}
	}
}
