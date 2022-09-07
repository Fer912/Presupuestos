﻿using Dapper;
using Microsoft.Data.SqlClient;
using Presupuestos.Models;

namespace Presupuestos.Servicios
{

    public interface IRepositorioTiposCuentas
    {
        Task Actualizar(TipoCuenta tipoCuenta);
        Task Borrar(int id);
        Task Crear(TipoCuenta tipocuenta);
        Task<bool> Existe(string nombre, int usuarioId);
        Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId);
        Task<TipoCuenta> ObtenerPorId(int id, int usuarioId);
    }
    public class RepositorioTiposCuentas : IRepositorioTiposCuentas
    {
        private readonly string connectionString;
        public RepositorioTiposCuentas(IConfiguration configuration)
        {
            connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public async Task Crear(TipoCuenta tipocuenta)
        {
            using var connection = new SqlConnection(connectionString);
            var id =  await connection.QuerySingleAsync<int>($@"INSERT INTO TiposCuentas (Nombre,UsuarioId,Orden) 
                                                 VALUES (@Nombre,@UsuarioId,0);
                                                 SELECT SCOPE_IDENTITY();",tipocuenta);
            tipocuenta.Id = id;
        }


        public async Task<bool> Existe(string nombre, int usuarioId)
        {
            using var conexion = new SqlConnection(connectionString);
            var existe = await conexion.QueryFirstOrDefaultAsync<int>(@"SELECT 1
                                                                        FROM TiposCuentas
                                                                        WHERE Nombre = @Nombre AND UsuarioId = @UsuarioID;",
                                                                        new { nombre, usuarioId});

            return existe == 1;
        }

        public async Task<IEnumerable<TipoCuenta>> Obtener(int usuarioId)
        {
            using var conexion = new SqlConnection(connectionString);
            return await conexion.QueryAsync<TipoCuenta>(@"SELECT Id, Nombre, Orden
                                                            FROM TiposCuentas
                                                            WHERE UsuarioId = @UsuarioId", new { usuarioId});

        }

        public async Task Actualizar(TipoCuenta tipoCuenta)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync(@"UPDATE TiposCuentas Set Nombre=@Nombre WHERE Id=@Id",tipoCuenta);
        }

        public async Task<TipoCuenta> ObtenerPorId(int id,int usuarioId)
        {
            using var connection = new SqlConnection(connectionString);
            return await connection.QueryFirstOrDefaultAsync<TipoCuenta>(@"SELECT Id, Orden
                                                                    FROM TiposCuentas
                                                                    WHERE Id=@Id AND UsuarioId=@UsuarioId;",
                                                                    new { id,usuarioId});
        }

        public async Task Borrar(int id)
        {
            using var connection = new SqlConnection(connectionString);
            await connection.ExecuteAsync("DELETE TiposCuentas WHERE Id=@Id", new { id });
        }
    }
}
