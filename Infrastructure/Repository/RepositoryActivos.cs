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
    public class RepositoryActivos : IRepositoryActivos
    {
        public void DeleteActivo(Activos activo)
        {
            
            int returno;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    //Activos Activo = new Activos()
                    //{
                    //    ActivoID = id
                    //};



                    ctx.Entry(activo).State = EntityState.Deleted;
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

        public IEnumerable<Activos> GetActivos()
        {

            try
            {
                // Forzar error
                // int x = 0;
                // x = 25 / x;


                IEnumerable<Activos> lista = null;
                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    // mal muy mal ...
                    lista = ctx.Activos.ToList<Activos>();
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

        public Activos GetActivoByID(int id)
        {
            Activos activo = null;
            try
            {

                using (MyContext ctx = new MyContext())
                {
                    ctx.Configuration.LazyLoadingEnabled = false;
                    activo = ctx.Activos.Find(id);
                }

                return activo;
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


//        public Activos Save(Activos activo)
//        {
//            int retorno = 0;
//            Activos oActivo = null;
//            try
//            {

//                using (MyContext ctx = new MyContext())
//                {

//                    ctx.Configuration.LazyLoadingEnabled = false;
//                    oActivo = GetActivoByID(activo.ActivoID);
//                    if (oActivo == null)
//                    {
//                        ctx.Activos.Add(activo);
//                    }
//                    else
//                    {
//                        ctx.Entry(activo).State = EntityState.Modified;
//                    }
//                    retorno = ctx.SaveChanges();


//#if !DEBUG
//                   // Error por exception
//                    int x = 0;
//                    x = 25 / x; 

//                    // Error por Entity Framework 
//                    // Forzar un error por duplicidad
//                   // bodega.IdBodega = 1;
//                   // ctx.Bodega.Add(bodega);
//                   // retorno = ctx.SaveChanges();
//#endif
//                }

//                if (retorno >= 0)
//                    oActivo = GetActivoByID(activo.ActivoID);

//                return oActivo;
//            }
//            catch (DbUpdateException dbEx)
//            {
//                string mensaje = "";
//                Log.Error(dbEx, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
//                throw new Exception(mensaje);
//            }
//            //Checking any Entity Validation Exception
//            catch (DbEntityValidationException e)
//            {
//                foreach (var eve in e.EntityValidationErrors)
//                {
//                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
//                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
//                    foreach (var ve in eve.ValidationErrors)
//                    {
//                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
//                            ve.PropertyName, ve.ErrorMessage);
//                    }
//                }
//                throw;
//            }
//            catch (Exception ex)
//            {
//                string mensaje = "";
//                Log.Error(ex, System.Reflection.MethodBase.GetCurrentMethod(), ref mensaje);
//                throw;
//            }
//        }


        //SaveTransaccion is in charge of save "Activo" and "Depreciacion"
        //Based on "FechaCompra" the value of "ValorActual" is received 
        //update from the previous method ServiceActivo >> "SaveTransaccion"
        public Activos SaveTransaccion(Activos activo)
        {
            int retorno = 0;
            Activos oActivo = null;

            using (MyContext ctx = new MyContext())
            {
                //Transaction Begin statement
                using (DbContextTransaction transaccion = ctx.Database.BeginTransaction())
                { 

                    try
                    {
                        ctx.Configuration.LazyLoadingEnabled = false;
                        oActivo = GetActivoByID(activo.ActivoID);
                        if (oActivo == null)
                        {
                            ctx.Activos.Add(activo);
                        }
                        else
                        {
                            ctx.Entry(activo).State = EntityState.Modified;
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


                        if (retorno >= 0) 
                        { 
                            //oActivo = GetActivoByID(activo.ActivoID);

                            //Guardamos la depreciacion basados en el nuevo ID y en el calculo realizado previamente
                            //Necesito cambiar Valor a decimal en la base de datos
                            Depreciacion oDepreciacion = new Depreciacion();
                            oDepreciacion.Valor = activo.ValorActual.ToString();
                            oDepreciacion.Fecha = DateTime.Now;
                            oDepreciacion.Activo = activo.ActivoID;

                            ctx.Depreciacion.Add(oDepreciacion);
                            retorno = ctx.SaveChanges();
                        }

                        if (retorno >= 0)//Se insertan llena la tabla Activos y depreciacion en la DB
                        {
                            transaccion.Commit();

                        }

                        return oActivo;

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

        //docs.microsoft.com/en-us/dotnet/api/system.data.linq.table-1.insertonsubmit?view=netframework-4.8
        //techfunda.com/howto/288/insert-record-using-transactions



        }







    }
}
