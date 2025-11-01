using Microsoft.AspNetCore.Mvc;
using StoreFlow.Context;
using StoreFlow.Entities;
using System.Threading.Tasks;

namespace StoreFlow.Controllers
{
	public class TodoController : Controller
	{
		private readonly StoreContext _context;

		public TodoController(StoreContext context)
		{
			_context = context;
		}

		public IActionResult Index()
		{
			return View();
		}

		[HttpGet]
		public async Task<IActionResult> CreateToDo()
		{
			var todos = new List<Todo>
			{
				new Todo { Description = "Mail gönder", Status = true, Priority = "Birincil" },
				new Todo { Description = "Rapor hazırla", Status = true, Priority = "İkincil" },
				new Todo { Description = "Toplantıya katıl", Status = false, Priority = "Birincil" }
			};

			await _context.Todos.AddRangeAsync(todos);
			await _context.SaveChangesAsync();

			return View();
		}

		public IActionResult TodoAggreagatePriority()
		{
			//Birincil Öncelik
			var priorityFirstlyTodo = _context.Todos
				.Where(x => x.Priority == "Birincil")
				.Select(y => y.Description)
				.ToList();
			string result = null;

			if (priorityFirstlyTodo.Any())

				result = priorityFirstlyTodo.Aggregate((acc, desc) => acc + ", " + desc);

			ViewBag.ListOfDescription = priorityFirstlyTodo;
			ViewBag.ResultsString = result;


			//ikincil öncelik
			var priorityFirstlyTodo2 = _context.Todos
				.Where(x => x.Priority == "İkincil")
				.Select(y => y.Description)
				.ToList();
			string result2 = null;

			if (priorityFirstlyTodo2.Any())

				result2 = priorityFirstlyTodo2.Aggregate((acc, desc) => acc + ", " + desc);

			ViewBag.ListOfDescription2 = priorityFirstlyTodo2;
			ViewBag.ResultsString2 = result2;


			//Üçüncül öncelik
			var priorityFirstlyTodo3 = _context.Todos
				.Where(x => x.Priority == "Üçüncül")
				.Select(y => y.Description)
				.ToList();
			string result3 = null;

			if (priorityFirstlyTodo3.Any())

				result3 = priorityFirstlyTodo3.Aggregate((acc, desc) => acc + ", " + desc);

			ViewBag.ListOfDescription3 = priorityFirstlyTodo3;
			ViewBag.ResultsString3 = result3;

			//Dördüncül öncelik
			var priorityFirstlyTodo4 = _context.Todos
				.Where(x => x.Priority == "Dördüncül")
				.Select(y => y.Description)
				.ToList();
			string result4 = null;

			if (priorityFirstlyTodo4.Any())

				result4 = priorityFirstlyTodo4.Aggregate((acc, desc) => acc + ", " + desc);

			ViewBag.ListOfDescription4 = priorityFirstlyTodo4;
			ViewBag.ResultsString4 = result4;

			return View();
		}

		public IActionResult IncompleteTask()
		{
			var values = _context.Todos
				.Where(x => !x.Status)
				.Select(y => y.Description)
				.ToList()
				.Prepend("Gün başında tüm görevleri kontrol etmeyi unutmayın!")
				.ToList();

			return View(values);
		}

		public IActionResult TodoChunk()
		{
			var values = _context.Todos
				.Where(x => !x.Status)
				.ToList()
				.Chunk(2)
				.ToList();
			return View(values);
		}

		public IActionResult TodoConcat()
		{
			var values = _context.Todos
				.Where(x => x.Priority == "Birincil")
				.ToList()
				.Concat(_context.Todos.Where(y => y.Priority == "İkincil").ToList())
				.ToList();
			return View(values);
		}
		public IActionResult TodoUnion()
		{
			var values = _context.Todos.Where(x => x.Priority == "Birincil").ToList();
			var values2 = _context.Todos.Where(x => x.Priority == "İkincil").ToList();
			var result = values.UnionBy(values2, x => x.Description).ToList();
			return View(result);
		}


	}
}
