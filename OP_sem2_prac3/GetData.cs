using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace OP_sem2_prac3
{
    public class GetData
    {
        public List<Person> GetPeople()
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("prac3_DB")))
            {   
                return connection.Query<Person>("dbo.GetPeople").ToList();
            }
        }
        public void AddPerson(string login, string password)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("prac3_DB")))
            {
                List<Person> ppl = new List<Person>();
                ppl.Add(new Person { Name = "", Surname = "", Login = login, Password = password, IsActive = true, PassRestr = false });
                connection.Execute("dbo.InsertPerson @Name, @Surname, @Login, @Password, @IsActive, @PassRestr", ppl);
            }
        }
        public void UpdatePerson(string name, string surname, string login, string password, bool isActive, bool passRestr)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("prac3_DB")))
            {
                List<Person> ppl = new List<Person>();
                ppl.Add(new Person { Name = name, Surname = surname, Login = login, Password = password, IsActive = isActive, PassRestr = passRestr });
                connection.Execute("dbo.UpdatePeople @Name, @Surname, @Login, @Password, @IsActive, @PassRestr", ppl);
            }
        }

        public void DeletePerson(string login)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Helper.CnnVal("prac3_DB")))
            {
                connection.Execute("dbo.DeletePerson @Login", new { Login = login });
            }
        }
    }
}
