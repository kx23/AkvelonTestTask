using AkvelonTestTask.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;


namespace AkvelonTestTask
{
    // Populates the database with initial data.
    public class SeedData
    {
        public static System.Threading.Tasks.Task EnsurePopulatedAsync(IApplicationBuilder app)
        {
            AppDbContext context = app.ApplicationServices
            .CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();
            //context.Database.EnsureDeleted();  // uncomment to erase db

            if (context.Database.GetPendingMigrations().Any())
            {
                context.Database.Migrate();
            }
            if (!context.Tasks.Any() && !context.Projects.Any())
            {

                Task task1 = new Task { Name = "Apply button", Description = "Add 'Apply' button for settings UI", Priority = 1, Status = TaskStatus.InProgress };
                Task task2 = new Task {  Name = "Cancel button", Description = "Add 'Cancel' button for settings UI", Priority = 1, Status = TaskStatus.ToDo };
                Task task3 = new Task { Name = "Add home page", Description = "Add home page for site", Priority = 3, Status = TaskStatus.Done };


                Project project1 = new Project {  Name = "Site Project", StartDate = DateTime.Today.AddMonths(-3), CompletionDate = DateTime.Today.AddMonths(-1), Status = ProjectStatus.Completed, Priority = 2 };
                Project project2 = new Project {  Name = "Game Project", StartDate = DateTime.Today, CompletionDate = DateTime.Today.AddMonths(3), Status = ProjectStatus.Active, Priority = 1 };

                project1.Tasks = new List<Task>() { task3 };
                project2.Tasks = new List<Task>() { task1,task2 };




                context.Tasks.AddRange(new List<Task> { task1,task2,task3 });
                context.Projects.AddRange(new List<Project> { project1, project2 });
                context.SaveChanges();
            }

            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
