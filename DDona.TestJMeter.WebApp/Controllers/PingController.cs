using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;
using DDona.TestJMeter.WebApp.Database;
using DDona.TestJMeter.WebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDona.TestJMeter.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PingController : ControllerBase
    {
        private DbAbstraction _db = new DbAbstraction();

        [Route("async")]
        public async Task<ActionResult<string>> GetAsync()
        {
            Guid guid = Guid.NewGuid();

            Debug.WriteLine($"{guid} começou");
            await Task.Delay(5000);
            Debug.WriteLine($"{guid} terminou");
            return Ok("okay!!!");
        }

        [Route("sync")]
        public ActionResult GetSync()
        {
            Thread.Sleep(5000);
            return Ok("okay!!!");
        }

        [Route("db/async")]
        public async Task<ActionResult> GetDbAsync()
        {
            using (TransactionScope ts = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                try
                {
                    await InserirDados();
                    ts.Complete();
                }
                catch (Exception ex)
                {
                    return Ok($"não inseriu: {ex.Message}");
                }
            }

            return Ok("Inseriu!!!");
        }

        private async Task InserirDados()
        {
            Venda venda = new Venda() { ValorTotal = 122 };

            venda.Id = await _db.Inserir(venda);

            await Task.Delay(3000);

            ItemVenda iv1 = new ItemVenda() { Item = "Maçã", IdVenda = venda.Id };
            ItemVenda iv2 = new ItemVenda() { Item = "Pera", IdVenda = venda.Id };

            await _db.Inserir(iv1);
            await _db.Inserir(iv2);

            //throw new Exception("MEEEEEEEEEEEERDA");
        }
    }
}