using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.OData;
using Microsoft.Azure.Mobile.Server;
using TodoSampleBackend.Data;
using TodoSampleBackend.DataObjects;

namespace TodoSampleBackend.Controllers
{
    [Authorize]
    public class TodoItemController : TableController<TodoItem>
    {
        protected override void Initialize(HttpControllerContext controllerContext)
        {
            base.Initialize(controllerContext);
            TodoSampleDataContext context = new TodoSampleDataContext();
            DomainManager = new EntityDomainManager<TodoItem>(context, Request);
        }

        // GET tables/PlanTask
        public IQueryable<TodoItem> GetAllTodoItem()
        {
            var identity = (ClaimsIdentity)User.Identity;
            var userName = identity?.Claims?.FirstOrDefault()?.Value;
            return Query().Where(t=>t.UserId == userName); 
        }

        // GET tables/PlanTask/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public SingleResult<TodoItem> GetTodoItem(string id)
        {
            return Lookup(id);
        }

        // PATCH tables/PlanTask/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task<TodoItem> PatchTodoItem(string id, Delta<TodoItem> patch)
        {
            return UpdateAsync(id, patch);
        }

        // POST tables/PlanTask
        public async Task<IHttpActionResult> PostTodoItem(TodoItem item)
        {
            TodoItem current = await InsertAsync(item);
            return CreatedAtRoute("Tables", new { id = current.Id }, current);
        }

        // DELETE tables/PlanTask/48D68C86-6EA6-4C25-AA33-223FC9A27959
        public Task DeleteTodoItem(string id)
        {
            return DeleteAsync(id);
        }
    }
}