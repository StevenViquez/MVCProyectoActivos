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
    public class RepositoryMarca : IRepositoryMarca
    {
        public void DeleteMarca(int id)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    Marca marca = new Marca()
                    {
                        MarcaID = id
                    };
                    ctx.Entry(marca).State = EntityState.Deleted;
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

        public IEnumerable<Marca> GetMarca()
        {

            try
            {
                // Forzar error
                // int x = 0;
                // x = 25 / x;


                IEnumerable<Marca> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.Marca.ToList<Marca>();
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

        public Marca GetMarcaByID(int id)
        {
            Marca marca = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    marca = ctx.Marca.Find(id);
                }

                return marca;
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


        public Marca Save(Marca marca)
        {
            int retorno = 0;
            Marca oMarca = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {

                    ctx.Configuration.LazyLoadingEnabled = false;
                    oMarca = GetMarcaByID(marca.MarcaID);
                    if (oMarca == null)
                    {
                        ctx.Marca.Add(marca);
                    }
                    else
                    {
                        ctx.Entry(marca).State = EntityState.Modified;
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
                    oMarca = GetMarcaByID(marca.MarcaID);

                return oMarca;
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
