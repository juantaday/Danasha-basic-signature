using Domain.Data.Entities;
using DomainSQLite.Setting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Data.Repositories
{
    public static class BodegaRepository
    {
        public static int Insert(Bodega entidad, string connectionString)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            using (var context = CrearContexto(connectionString))
            {
                context.Bodegas.Add(entidad);
                context.SaveChanges();
                return entidad.IdBodega;
            }
        }

        public static bool Update(Bodega entidad, string connectionString)
        {
            if (entidad == null)
                throw new ArgumentNullException(nameof(entidad));

            using (var context = CrearContexto(connectionString))
            {
                var existente = context.Bodegas.FirstOrDefault(b => b.IdBodega == entidad.IdBodega);
                if (existente == null)
                    return false;

                existente.NomBodega = entidad.NomBodega;
                existente.DescripcionBodega = entidad.DescripcionBodega;
                existente.DireccionBodega = entidad.DireccionBodega;
                existente.Telefono1Bodega = entidad.Telefono1Bodega;
                existente.Telefono2Bodega = entidad.Telefono2Bodega;
                existente.Telefono3Bodega = entidad.Telefono3Bodega;
                existente.Resp1IdEmpleado = entidad.Resp1IdEmpleado;
                existente.Resp2IdEmpleado = entidad.Resp2IdEmpleado;
                existente.Resp3IdEmpleado = entidad.Resp3IdEmpleado;
                existente.FechaApertura = entidad.FechaApertura;
                existente.TypoBodega = entidad.TypoBodega;
                existente.CodEstablec = entidad.CodEstablec;
                existente.EsSucursalRemota = entidad.EsSucursalRemota;
                existente.CiudadSucursal = entidad.CiudadSucursal;
                context.SaveChanges();
                return true;
            }
        }


        public static bool UpdateRemoteConfig(int idBodega,
            string tailscaleIp, string tailscaleUsuario,
            string tailscalePassword, string tailscaleDatabase,
            string connectionString)
        {
            using (var context = CrearContexto(connectionString))
            {
                var entidad = context.Bodegas.FirstOrDefault(b => b.IdBodega == idBodega);
                if (entidad == null)
                    return false;

                entidad.TailscaleIp = tailscaleIp;
                entidad.TailscaleUsuario = tailscaleUsuario;
                entidad.TailscalePassword = tailscalePassword;
                entidad.TailscaleDatabase = tailscaleDatabase;

                context.SaveChanges();
                return true;
            }
        }


        public static bool Delete(int idBodega, string connectionString)
        {
            using (var context = CrearContexto(connectionString))
            {
                var entidad = context.Bodegas.FirstOrDefault(b => b.IdBodega == idBodega);
                if (entidad == null)
                    return false;

                context.Bodegas.Remove(entidad);
                context.SaveChanges();
                return true;
            }
        }

        public static List<Bodega> TraeListaExeptEsta(int idBodega, string connectionString)
        {
            using (var context = CrearContexto(connectionString))
            {
                return context.Bodegas
                    .Where(b => b.IdBodega != idBodega)
                    .ToList();
            }
        }

        public static List<Bodega> TraeListaExepRemoto(string connectionString)
        {
            using (var context = CrearContexto(connectionString))
            {
                return context.Bodegas
                    .Where(b => b.EsSucursalRemota != true)
                    .ToList();
            }
        }

        public static List<Bodega> TraeListaRemoto(string connectionString)
        {
            using (var context = CrearContexto(connectionString))
            {
                return context.Bodegas
                    .Where(b => b.EsSucursalRemota == true)
                    .ToList();
            }
        }


        private static DataContext CrearContexto(string connectionString)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer(connectionString ?? Configuration.ConectionString);
            return new DataContext(optionsBuilder.Options);
        }


    }
}
