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

                userrole = dbContext.art_userrole.Include("art_userrole")
                            .Select(user => new UserRoleModel()
                            {
                                roleId = user.role_id,
                                roleName = user.role_name,
                                descriptions = user.description
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
                userRoleModel = dbContext.art_userrole
                    .Where(s => s.role_id == id)
                    .Select(user => new UserRoleModel()
                {
                    roleId = user.role_id,
                    roleName = user.role_name,
                    descriptions = user.description
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
                var userRoleModel = db.art_userrole
                    .Where(user => user.role_id == id)
                    .FirstOrDefault();
                db.Entry(userRoleModel).State = System.Data.Entity.EntityState.Deleted;
                db.SaveChanges();
            }
            return Ok("Deleted Success!!");
        }

        public IHttpActionResult Post(UserRoleModel userRole)
        {

            if (!ModelState.IsValid) {
                return BadRequest("Invalid data.");
            }
            using(var db=new ArticleEntities()){
                db.art_userrole.Add(new art_userrole()
                {
                    role_name = userRole.roleName,
                    description =userRole.descriptions
                });
                db.SaveChanges();
            }

            return Ok("Added Successed!");
        }

        public IHttpActionResult Put(UserRoleModel userRole)
        {
          
            using(var db=new ArticleEntities()){
              var  userRoleModel = db.art_userrole
                    .Where(user => user.role_id == userRole.roleId)
                    .FirstOrDefault<art_userrole>();
              if (userRoleModel != null) {
                  userRoleModel.role_name = userRole.roleName;
                  userRoleModel.description = userRole.descriptions;
                  db.SaveChanges();              
              }
             }
            return Ok("update successed!");
        }
    }
}
