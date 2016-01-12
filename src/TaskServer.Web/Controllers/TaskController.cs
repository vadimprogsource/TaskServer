using Microsoft.AspNet.Mvc;
using System;
using System.Linq;
using TaskServer.Dto;
using TaskServer.Dto.Filtration;
using TaskServer.Dto.Specialized;
using TaskServer.Interfaces;
using TaskServer.Interfaces.Services;
using TaskServer.Interfaces.Specialized;

namespace TaskServer.Web.Controllers
{
    [Route("/")]
    public class TaskController : Controller
    {

       
        private IClassifiersService  clsService;
        private ITaskWorkflowService taskService;




        
        public TaskController(IClassifiersService clsService, ITaskWorkflowService taskService)
        {
            this.clsService = clsService;
            this.taskService = taskService;
        }




        private DtoDelta<ITask> Dto(IDelta<ITask> task)
        {

            DtoLazyCollection<UserDto> systemUsers = new DtoLazyCollection<UserDto>(() => clsService.GetSystemUsers().Select(x => new UserDto(x)));

            DtoDelta<ITask> dtoResult = new DtoDelta<ITask>(task);

            if (task.CanReadWrite(x => x.Author))
            {
                dtoResult.AddPropertyValues(x => x.Author, systemUsers);
            }

            if (task.CanReadWrite(x => x.Manager))
            {
                dtoResult.AddPropertyValues(x => x.Manager, systemUsers);
            }

            if (task.CanReadWrite(x => x.ToEmployee))
            {
                dtoResult.AddPropertyValues(x => x.ToEmployee, systemUsers);
            }


            if (task.CanReadWrite(x => x.Priority))
            {
                dtoResult.AddPropertyValues(x => x.Priority, clsService.GetPriorities().Select(x => new PriorityDto(x)));
            }


            return dtoResult;


        }

        [HttpGet]
        public IActionResult Get()
        {

            return Redirect("/index.html");
        }


        [HttpGet("create")]
        public dynamic CreateTask()
        {
            return Dto(taskService.CreateTask());
        }

        [HttpGet("{id}")]
        public dynamic GetTask(Guid id)
        {
            return Dto(taskService.GetTask(id));
        }

        [HttpPost]
        public CompositeTaskSetDto ApplyFilter(TaskFilterDto filter)
        {
            return new CompositeTaskSetDto(taskService.GetCompositeTasks(filter));
        }


        [HttpPut("new")]
        public dynamic SaveAsNew([FromBody]TaskDto task)
        {
            return Dto(taskService.AddTask(task));
        }
    }
}

