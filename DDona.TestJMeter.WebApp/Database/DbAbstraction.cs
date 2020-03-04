using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DDona.TestJMeter.WebApp.Database
{
    public class DbAbstraction
    {
        public IDbConnection Connection { get; set; } = null;
        public IDbTransaction Transaction { get; set; } = null;

        public DbAbstraction()
        {
            Connection = new SqlConnection(@"Server=.\SQLExpress;Database=testes-gerais;User Id=sa;Password=123123;");
        }

        public void Begin()
        {
            Connection.Open();

            if(Transaction == null)
            {
                Transaction = Connection.BeginTransaction();
            }
        }

        public void Commit()
        {
            Transaction.Commit();
            Transaction.Dispose();
            Connection.Dispose();
        }

        public void RollBack()
        {
            Transaction.Rollback();
            Transaction.Dispose();
            Connection.Dispose();
        }

        public async Task<int> Inserir<T>(T modelo) where T: class
        {
            return await Connection.InsertAsync(modelo, Transaction);
        }
    }
}
