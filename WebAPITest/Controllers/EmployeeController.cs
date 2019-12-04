namespace WebAPITest.Controllers
{
    using System;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Web.Http;

    public class EmployeeController : ApiController
    {
        public EmployeeController()
        {
            //int p = Convert.ToInt32("");
        }
        EntityModel context = new EntityModel();

        public IQueryable<Employee> Get()
        {
            return context.Employees;
        }

        /// <summary>
        /// Read Operation
        /// </summary>
        /// <param name="id">Key</param>
        /// <returns></returns>
        public HttpResponseMessage Get(int id)
        {
            Employee data = context.Employees.Where(k => k.Id == id).FirstOrDefault();
            string p = data.EmailAddress;
            if (data == null)
            {
                string message = string.Format("No Employee found with ID = {0}", id);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound, message);
            }
            return Request.CreateResponse(HttpStatusCode.OK, data); ;
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            context.Dispose();
            base.Dispose(disposing);
        }
    }
}
