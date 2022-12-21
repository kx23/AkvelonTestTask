using AkvelonTestTask.Domain;
using AkvelonTestTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AkvelonTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        DataManager dataManager;
        public TasksController(DataManager dm)
        {
            dataManager = dm;
        }


        // Returns all tasks in data base
        [HttpGet]
        public ActionResult GetAllTasks()
        {
            try 
            {
                return Ok(dataManager.TasksRepository.GetAll().ToList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Returns information about the task by its id
        [HttpGet("get/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Task>> Get(int id)
        {
            try
            {
                var result = await dataManager.TasksRepository.Read(id);

                if (result == null)
                {
                    return NotFound();
                }

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Create new task
        [HttpPost("post/")]
        public async System.Threading.Tasks.Task<ActionResult<Task>> Post(Task task)
        {
            try
            {
                if (task == null)
                {
                    return BadRequest();
                }

                var project = await dataManager.ProjectsRepository.Read(task.ProjectId);
                if (project == null)
                {
                    return NotFound($"Project with Id = {task.ProjectId} not found");
                }
                task.Id = default;
                var createdTask = await dataManager.TasksRepository.Create(task);

                return CreatedAtAction(nameof(Get), new { id = createdTask.Id },
                    task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Delete task by id
        [HttpDelete("delete/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Task>> Delete(int id)
        {
            try
            {
                var taskToDelete = await dataManager.TasksRepository.Read(id);

                if (taskToDelete == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }

                return await dataManager.TasksRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        
        // Edit task information by id
        [HttpPut("update/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Task>> Update(int id, Task task)
        {
            try
            {
                /*if (id != task.Id)
                {
                    return BadRequest("Task ID mismatch");
                }*/

                var project = await dataManager.ProjectsRepository.Read(task.ProjectId);
                if (project == null)
                {
                    return NotFound($"Project with Id = {task.ProjectId} not found");
                }

                var taskToUpdate = await dataManager.TasksRepository.Read(id);

                if (taskToUpdate == null)
                {
                    return NotFound($"Task with Id = {id} not found");
                }
                task.Id = id;

                return await dataManager.TasksRepository.Update(task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

        }

    }
}
