using AkvelonTestTask.Domain;
using AkvelonTestTask.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace AkvelonTestTask.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProjectsController : ControllerBase
    {
        DataManager dataManager;
        public ProjectsController(DataManager dm)
        {
            dataManager = dm;
        }

        // Returns all projects in data base
        [HttpGet]
        public ActionResult GetAllProjects()
        {
            try
            {
                return Ok(dataManager.ProjectsRepository.GetAll().ToList());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Returns information about the project by its id
        [HttpGet("get/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Project>> Get(int id)
        {
            try
            {
                var result = await dataManager.ProjectsRepository.Read(id);

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

        // Create new project
        [HttpPost("post/")]
        public async System.Threading.Tasks.Task<ActionResult<Project>> Post(Project project)
        {
            try
            {
                if (project == null)
                {
                    return BadRequest();
                }

                var createdProject = await dataManager.ProjectsRepository.Create(project);

                return CreatedAtAction(nameof(Get), new { id = createdProject.Id },
                    project);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Delete project by id
        [HttpDelete("delete/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Project>> Delete(int id)
        {
            try
            {
                var projectToDelete = await dataManager.ProjectsRepository.Read(id);

                if (projectToDelete == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }

                return await dataManager.ProjectsRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        // Edit project information by id
        [HttpPut("update/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Project>> Update(int id, Project project)
        {
            try
            {
                /*if (id != project.Id)
                {
                    return BadRequest("Project ID mismatch");
                }*/

                var projectToUpdate = await dataManager.ProjectsRepository.Read(id);

                if (projectToUpdate == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }
                project.Id = id;

                return await dataManager.ProjectsRepository.Update(project);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

        }

        // Get all project's tasks
        [HttpGet("gettasks/{id}")]
        public async System.Threading.Tasks.Task<ActionResult> GetTasks(int id)
        {
            try
            {
                var result = await dataManager.ProjectsRepository.Read(id);

                
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result.Tasks);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        // Add task to project
        [HttpPost("addtask/{id}")]
        public async System.Threading.Tasks.Task<ActionResult<Task>> AddTask(int id, Task task)
        {
            try
            {
                /*if (id != task.ProjectId)
                {
                    return BadRequest("Project ID mismatch");
                }*/
                var projectToAddTask = await dataManager.ProjectsRepository.Read(id);

                if (projectToAddTask == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }
                if (task == null)
                {
                    return BadRequest();
                }
                task.ProjectId = id;
                var createdTask = await dataManager.TasksRepository.Create(task);

                return CreatedAtAction(nameof(Get), new { id = createdTask.Id },
                    task);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }

        }

        // Delete task from project
        // id - id of the project from which the task will be removed.
        // taskId - id of the task that will be removed.
        [HttpDelete("deletetask/{id}/{taskId}")]
        public async System.Threading.Tasks.Task<ActionResult<Task>> Delete(int id,int taskId)
        {
            try
            {
                var project = await dataManager.ProjectsRepository.Read(id);

                if (project == null)
                {
                    return NotFound($"Project with Id = {id} not found");
                }
                var taskToDelete = project.Tasks.FirstOrDefault(t => t.Id == taskId);
                if (taskToDelete == null)
                {
                    return NotFound($"Project with Id = {id} does not contain task with Id = {taskId}");
                }

                return await dataManager.TasksRepository.Delete(taskId);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }


    }
}
