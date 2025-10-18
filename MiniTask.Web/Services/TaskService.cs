using System.Net.Http.Json;
using MiniTask.Web.Models;

namespace MiniTask.Web.Services
{
    public class TaskService
    {
        private readonly HttpClient _http;
        private readonly string apiUrl = "https://localhost:7052/api/Tasks"; 

        public TaskService(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<TaskItem>> GetTasksAsync()
        {
            return await _http.GetFromJsonAsync<List<TaskItem>>(apiUrl);
        }

        public async Task<TaskItem> GetTaskAsync(int id)
        {
            return await _http.GetFromJsonAsync<TaskItem>($"{apiUrl}/{id}");
        }

        public async Task CreateTaskAsync(TaskItem task)
        {
            await _http.PostAsJsonAsync(apiUrl, task);
        }

        public async Task UpdateTaskAsync(TaskItem task)
        {
            await _http.PutAsJsonAsync($"{apiUrl}/{task.Id}", task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _http.DeleteAsync($"{apiUrl}/{id}");
        }
    }
}
