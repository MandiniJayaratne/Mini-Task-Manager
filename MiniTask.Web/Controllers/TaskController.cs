using Microsoft.AspNetCore.Mvc;
using MiniTask.Web.Models;
using MiniTask.Web.Services;

namespace MiniTask.Web.Controllers
{
    public class TaskController : Controller
    {
        private readonly TaskService _taskService;

        public TaskController(TaskService taskService)
        {
            _taskService = taskService;
        }

        // Dashboard 
        public async Task<IActionResult> Index(string searchString = "", string statusFilter = "All")
        {
            // Get all from service
            var tasks = await _taskService.GetTasksAsync();

            // Filter by search 
            if (!string.IsNullOrEmpty(searchString))
            {
                tasks = tasks.Where(t => t.Title.Contains(searchString, StringComparison.OrdinalIgnoreCase)
                                      || t.Description.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            // Filter by status 
            if (!string.IsNullOrEmpty(statusFilter) && statusFilter != "All")
            {
                tasks = tasks.Where(t => t.Status.Equals(statusFilter, StringComparison.OrdinalIgnoreCase))
                             .ToList();
            }

            return View(tasks);
        }

        // GET: Create Task
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create Task
        [HttpPost]
        public async Task<IActionResult> Create(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.CreateTaskAsync(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // GET: Edit Task
        public async Task<IActionResult> Edit(int id)
        {
            var task = await _taskService.GetTaskAsync(id);
            if (task == null) return NotFound();
            return View(task);
        }

        // POST: Edit Task
        [HttpPost]
        public async Task<IActionResult> Edit(TaskItem task)
        {
            if (ModelState.IsValid)
            {
                await _taskService.UpdateTaskAsync(task);
                return RedirectToAction(nameof(Index));
            }
            return View(task);
        }

        // Delete Task
        public async Task<IActionResult> Delete(int id)
        {
            await _taskService.DeleteTaskAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
