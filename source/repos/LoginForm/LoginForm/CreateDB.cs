using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using LoginForm.DTO;
namespace LoginForm
{
    public class CreateDB:DropCreateDatabaseIfModelChanges<QLENG>
    {
        protected override void Seed(QLENG context)
        {
            String ida = DateTime.Now.ToString();
           context.AccountUsers.Add(new accountUser
           {
               id=ida,
               userName="b",
               passwordUser="b",
           });
            context.Inforusers.Add(new inforuser
            {
                id = ida + ida,
                idaccount = ida,
                email = "b",
                firstName = "b",
                lastName = "b",
                gender = "Male",
                roleUser = "Admin",
                sinhnhat = DateTime.Now,               
             }) ;
            Seed(context);
            context.SaveChanges();
            
        }
    }
}
