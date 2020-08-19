using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class RepositoryDepreciacion : IRepositoryDepreciacion
    {
        public IEnumerable<Depreciacion> GetDepreciacionByActivo(int activo)
        {
            try
            {
                // Forzar error
                // int x = 0;
                // x = 25 / x;


                IEnumerable<Depreciacion> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    //Seleccionamos los telefonos que pertecen al vendedor seleccionado en el view "List" en Vendedor
                    //Se desplega modal partial view con los telefono

                    lista = ctx.Depreciacion.Include("Activos").ToList<Depreciacion>().Where(p => p.Activo == activo);
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

        public void DeleteDepreciacion(int idActivo)
        {
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    ////Delete all depreciaciones 

                    foreach (var depreciacion in GetDepreciacionByActivo(idActivo).ToList())
                    {
                        ctx.Entry(depreciacion).State = EntityState.Deleted;
                    }

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


        public Depreciacion SaveTransaccion(Depreciacion depreciacion)
        {
            int retorno = 0;
            Depreciacion oDepreciacion = null;

            using (MyContext ctx = new MyContext())
            {
                //Transaction Begin statement
                using (DbContextTransaction transaccion = ctx.Database.BeginTransaction())
                {


                    try
                    {
                        //Validar la fecha!!!!
                        ctx.Depreciacion.Add(depreciacion);
                        retorno = ctx.SaveChanges();


                        if (retorno >= 0)//Se insertan llena la tabla Activos y depreciacion en la DB
                        {
                            transaccion.Commit();
                            oDepreciacion = depreciacion;

                        }

                        return oDepreciacion;

                    }
                    catch (DbUpdateException dbEx)
                    {
                        transaccion.Rollback(); //Rollback si algo sale mal
                        string mensaje = "";
                        Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                        throw new Exception(mensaje);
                    }
                    //Checking any Entity Validation Exception
                    catch (DbEntityValidationException e)
                    {
                        transaccion.Rollback(); //Rollback si algo sale mal
                        foreach (var eve in e.EntityValidationErrors)
                        {
                            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                                eve.Entry.Entity.GetType().Name, eve.Entry.State);
                            foreach (var ve in eve.ValidationErrors)
                            {
                                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                    ve.PropertyName, ve.ErrorMessage);
                            }
                        }
                        throw;
                    }
                    catch (Exception ex)
                    {
                        transaccion.Rollback(); //Rollback si algo sale mal
                        string mensaje = "";
                        Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
                        throw;
                    }

                }


            }





        }


        public IEnumerable<Depreciacion> GetDepreciacionByID(int id)
        { 
            IEnumerable<Depreciacion> depreciacion = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;

                    depreciacion = ctx.Depreciacion.Include("Activos").ToList<Depreciacion>().Where(p => p.DepreciacionID == id);

                }

                return depreciacion;
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
