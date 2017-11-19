﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using CS586MVC.Data;
using CS586MVC.Models;

namespace CS586MVC.Controllers
{
    public partial class PropertyDataController : Controller
    {
        public PropertyDataController(PropertyMismanagementContext context)
        {    
            DbHelper.Context = context;
        }
        
        [HttpGet]
        public async Task<IEnumerable<AptComplex>> Properties(int? id)
        {
            return id.HasValue ?
                new List<AptComplex> { await DbHelper.AptComplex((int)id) } :
                await DbHelper.AllAptComplexes();
        }
        
        [HttpPost]
        public async Task Properties(AptComplex ac)
        {
            Console.WriteLine($"Received new AptComplex:{ac.Address}, {ac.Size}");
            await DbHelper.InsertAptComplex(ac);
        }
        
        [HttpPut]
        public async Task Properties(int id, [FromBody] AptComplex ac)
        {
            Console.WriteLine($"Updating Existing AptComplex: {ac.Address}, {ac.Size}");
            await DbHelper.UpdateAptComplex(id, ac);
        }

        [HttpDelete]
        public async Task Properties(int id)
        {
            Console.WriteLine($"Deleting AptComplex with ID:{id}");
            await DbHelper.RemoveAptComplex(id);
        }

        [HttpGet]
        public async Task<IEnumerable<AptComplexUnit>> PropertyUnits(int? id)
        {
            return id.HasValue ?
                new List<AptComplexUnit> { await DbHelper.AptComplexUnit((int)id) } :
                await DbHelper.AllAptComplexUnits();
        }
        
        [HttpPost]
        public async Task PropertyUnits([FromBody] AptComplexUnit acu)
        {
            Console.WriteLine($"Received new AptComplexUnit:{acu}");
            await DbHelper.InsertAptComplexUnit(acu);
        }

        [HttpPut]
        public async Task PropertyUnits(int id, [FromBody] AptComplexUnit acu)
        {
            //todo
        }
        
        [HttpDelete]
        public async Task PropertyUnits(int id)
        {
            //todo
        }

        [HttpGet]
        public async Task<IEnumerable<Person>> Tenants(int? id)
        {
            return id.HasValue ? 
                new List<Person> { await DbHelper.Person((int)id) } : 
                await DbHelper.AllPersons();
        }
        
        [HttpPost]
        public async Task Tenants([FromBody]Person p)
        {
            Console.WriteLine($"Received new Person: {p.FirstName} {p.LastName}");
            await DbHelper.InsertPerson(p);
        }
        
        [HttpPut]
        public async Task Tenants(int id, [FromBody]Person p)
        {
            Console.WriteLine($"Updating existing person with id:{id}");
            await DbHelper.UpdatePerson(id, p);
        }

        [HttpDelete]
        public async Task Tenants(int id)
        {
            await DbHelper.RemovePerson(id);
        }

        [HttpGet]
        public async Task<IEnumerable<Lease>> Leases(int? id)
        {
            return id.HasValue ? 
                new List<Lease> { await DbHelper.Lease((int) id) } : 
                await DbHelper.AllLeases();
        }
        
        [HttpPost]
        public async Task Leases(Lease l)
        {
            Console.WriteLine($"Received new Lease: {l}");
            await DbHelper.InsertLease(l);
        }

        [HttpPut]
        public async Task Leases(int id, Lease l)
        {
            
        }

        [HttpDelete]
        public async Task Leases(int id)
        {
            await DbHelper.RemoveLease(id);
        }
        
    }
}
