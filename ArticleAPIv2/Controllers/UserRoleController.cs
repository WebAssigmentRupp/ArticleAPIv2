using ArticleAPIv2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ArticleAPIv2.Controllers
{
    public class UserRoleController : ApiController
    {

        public IHttpActionResult GetAllStudents()
        {
            ICollection<UserRoleModel> userrole = null;
         

            using (var dbContext = new ArticleEntities())
            {

                userrole = dbContext.art_userroles.Include("art_userrole")
                            .Select(user => new UserRoleModel()
                            {
                                role_id = user.role_id,
                                role_name = user.role_name,
                                description = user.description
                            }).ToList<UserRoleModel>();
            }

            if (userrole.Count == 0)
            {
                return NotFound();
            }

            return Ok(userrole);
        }

        public IHttpActionResult GetStudent(int id)
        {
            UserRoleModel userRoleModel = null;
            using (var dbContext = new ArticleEntities())
            {
                userRoleModel = dbContext.art_userroles
                    .Where(s => s.role_id == id)
                    .Select(user => new UserRoleModel()
                {
                    role_id = user.role_id,
                    role_name = user.role_name,
                    description = user.description
                }).FirstOrDefault<UserRoleModel>();
            }
            if (userRoleModel == null)
            {
                return NotFound();
            }
            return Ok(userRoleModel);
        }

        public IHttpActionResult Delete(int id)
        {

            if (id <= 0)
            {
                return BadRequest("Not a valid student id");
            }
            using (var db = new ArticleEntities())
            {
                var userRoleModel = db.art_userroles
                    .Where(user => user.role_id ==id)
                    .FirstOrDefault();
                db.Entry(userRoleModel).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok("Deleted Success!!");
        }

        public IHttpActionResult POST(UserRoleModel userRole)
        {
            if (!ModelState.IsValid)
               return BadRequest("Invalid data.");

           var user = new art_userrole() { role_name = userRole.role_name, description = userRole.description };
           var db = new ArticleEntities();
           db.art_userroles.Add(user);
           db.SaveChanges();
            return Ok("Added Successed!");
        }

        public IHttpActionResult Put([FromBody] UserRoleModel id)
        {
          
            using(var db=new ArticleEntities()){
                var userRoleModel = db.art_userroles
                    .Where(user => user.role_id == id.role_id)
                    .FirstOrDefault<art_userrole>();
              if (userRoleModel != null) {
                  userRoleModel.role_name = id.role_name;
                  userRoleModel.description = id.description;
                  db.SaveChanges();              
              }
             }
            return Ok("update successed!");
        }
    }
}
