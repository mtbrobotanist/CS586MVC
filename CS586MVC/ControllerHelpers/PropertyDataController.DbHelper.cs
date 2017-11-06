﻿using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

using CS586MVC.Data;
using CS586MVC.Models;

namespace CS586MVC.Controllers
 {
     public partial class PropertyDataController
     {
     private static class DbHelper
     {      
         public static PropertyMismanagementContext Context { set; private get; }

         public static async Task<AptComplex> AptComplex(int id, bool include = true)
         {
             return include ? 
                 await Context.AptComplexes.Include(e => e.Units).ThenInclude(e => e.Lease).FirstOrDefaultAsync(e => e.Id == id) :  
                 await Context.AptComplexes.FirstOrDefaultAsync();
         }
         
         public static async Task<IEnumerable<AptComplex>> AllAptComplexes(bool include = true)
         {
            return include ? 
                await Context.AptComplexes.Include(e => e.Units).ThenInclude(e => e.Lease).ToListAsync() :        
                await Context.AptComplexes.ToListAsync();
         }

         public static async Task <AptComplexUnit> AptComplexUnit(int id, bool include = true)
         {
             return include ?
                 await Context.AptComplexUnits.Include(e => e.AptComplex).Include(e => e.AptUnit).FirstOrDefaultAsync(e => e.Id == id) :
                 await Context.AptComplexUnits.FirstOrDefaultAsync(e => e.Id == id);
         }
         
         public static async Task<IEnumerable<AptComplexUnit>> AllAptComplexUnits(bool include = true)
         {
             return include ? 
                 await Context.AptComplexUnits.Include(e => e.AptComplex).ToListAsync() :    
                 await Context.AptComplexUnits.ToListAsync();
         }
         
         public static async Task<Lease> Lease(int id, bool include = true)
         {
             return include ?
                 await Context.Leases.Include(a => a.AptComplexUnit).Include(p => p.Person).FirstOrDefaultAsync() :
                 await Context.Leases.FirstOrDefaultAsync(e => e.Id == id);
         }
         
         public static async Task<IEnumerable<Lease>> AllLeases(bool include = true)
         {
             return include ?
                 await Context.Leases.Include(a => a.AptComplexUnit).Include(p => p.Person).ToListAsync() :      
                 await Context.Leases.ToListAsync();
         }
         
         public static async Task<Person> Person(int id, bool include = true)
         {
             return include ?
                 await Context.Persons.Include(p => p.Leases).FirstOrDefaultAsync(e => e.Id == id) :
                 await Context.Persons.FirstOrDefaultAsync(e => e.Id == id);
         }
         
         public static async Task<IEnumerable<Person>> AllPersons(bool include = true)
         {
             //return include ? 
             if (include)
             {
                 return await Context.Persons.Include(a => a.Leases).ThenInclude(a => a.AptComplexUnit).ToListAsync();
             }
              
             return await Context.Persons.ToListAsync();
         }
     } 
     }
 }