using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryAsegurador : IRepositoryAsegurador
    {
        public void DeleteAsegurador(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Asegurador asegurador = new Asegurador()
                    {
                        AseguradorID = id
                    };
                    ctx.Entry(asegurador).State = EntityState.Deleted;
                    returno = ctx.SaveChanges();
                }
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }

        }

        public IEnumerable<Asegurador> GetAsegurador()
        {

            try
            {
                // Forzar error
                // int x = 0;
                // x = 25 / x;


                IEnumerable<Asegurador> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.Asegurador.ToList<Asegurador>();
                }
                return lista;
            }

            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }

        public Asegurador GetAseguradorByID(int id)
        {
            Asegurador asegurador = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    asegurador = ctx.Asegurador.Find(id);
                }

                return asegurador;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }


        public Asegurador Save(Asegurador asegurador)
        {
            int retorno = 0;
            Asegurador oAsegurador = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oAsegurador = GetAseguradorByID(asegurador.AseguradorID);
                    if (oAsegurador == null)
                    {
                        ctx.Asegurador.Add(asegurador);
                    }
                    else
                    {
                        ctx.Entry(asegurador).State = EntityState.Modified;
                    }
                    retorno = ctx.SaveChanges();


#if !DEBUG
                   // Error por exception
                    int x = 0;
                    x = 25 / x; 

                    // Error por Entity Framework 
                    // Forzar un error por duplicidad
                   // bodega.IdBodega = 1;
                   // ctx.Bodega.Add(bodega);
                   // retorno = ctx.SaveChanges();
#endif
                }

                if (retorno >= 0)
                    oAsegurador = GetAseguradorByID(asegurador.AseguradorID);

                return oAsegurador;
            }
            catch (DbUpdateException dbEx)
            {
                string mensaje = "";
                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw new Exception(mensaje);
            }
            catch (Exception ex)
            {
                string mensaje = "";
                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                throw;
            }
        }
    }
}
