using APBD5.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;


namespace APBD5.Services
{
    public class DbService : IDbService
    {
        private static string _connectionString =
        "Data Source=db-mssql16;Initial Catalog=2019SBD;Integrated Security=True";

        public async Task<string> AddItem2(Item item)
        {
            DateTime localDate = DateTime.Now;
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand())
            {
                string sql = "AddProductToWarehouse";
                cmd.Connection = conn;
                cmd.CommandText = sql;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@IdProduct", item.IdProduct);
                cmd.Parameters.AddWithValue("@IdWarehouse", item.IdWarehouse);
                cmd.Parameters.AddWithValue("@Amount", item.Amount);
                cmd.Parameters.AddWithValue("@CreatedAt", localDate);
                await conn.OpenAsync();
                await cmd.ExecuteNonQueryAsync();

                cmd.CommandType = CommandType.Text;
                string sql2 = $"select idproductwarehouse" +
                    $" from product_warehouse pw , \"order\" o where pw.idorder = o.idorder; ";
                cmd.CommandText = sql2;
                var dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    return dr["Idproductwarehouse"].ToString();
                }
            }

            return " ";
        }

       
        public async Task<string> AddItem(Item item)
        {
            String sql = $"select count(*) from product where idproduct = 1";
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand(sql , conn))
            {
                await conn.OpenAsync();
                if (!await IsPresent(item))
                {
                    return "This item id doesn't exist";
                }
                else
                {
                    if (!await IsPresent(item.IdWarehouse))
                    {
                        return "This warehouse id doesn't exist";
                    }
                    else
                    {
                        if (item.Amount <= 0)
                        {
                            return ("Invalid amount");
                        }
                        else
                        {
                            if (!await IsRequested(item))
                            {
                                return ("Order not found");
                            }
                            else
                            {
                                if (await IsDone(item))
                                    return ("Already done");
                                await FullfilledAtOrder(item);                               
                            }
                        }
                    }
                }                      
            }
            return await InsertProductWarehouse(item);
        }

        public async Task<bool> IsPresent(Item item)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand())
            {
                string sql = $"select count(*) as Count from product where idproduct = @itemId";
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@itemId", item.IdProduct);
                await conn.OpenAsync();
                var dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr["Count"].ToString().Equals("1"))
                        return true;
                }
            }
            return false;
        }
        public async Task<bool> IsPresent(int idWarehouse)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand())
            {
                string sql = $"select count(*) as Count from warehouse where idwarehouse = @warehouseId";
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@warehouseId", idWarehouse);
                await conn.OpenAsync();
                var dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr["Count"].ToString().Equals("1"))
                        return true;
                }
            }
            return false;
        }

        public async Task<bool> IsRequested(Item item)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand())
            {
                string sql = $"select count(*) as Count from \"order\" where IdProduct = @itemId and amount=@amount and CreatedAt  < CONVERT (DATETIME ,\'{item.CreatedAt.ToString()}\',21)";
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@itemId", item.IdProduct);
                cmd.Parameters.AddWithValue("@amount", item.Amount);
                cmd.Parameters.AddWithValue("@date", item.CreatedAt.ToString());
                Console.WriteLine(item.CreatedAt.ToString());
                await conn.OpenAsync();
                var dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr["Count"].ToString().Equals("1"))
                        return true;
                }
            }
            return false;
        }

        public async Task<bool> IsDone(Item item)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand())
            {
                string sql = $"select count(*) as Count from \"order\" o , product_warehouse pw where pw.idorder= o.idorder";
                cmd.CommandText = sql;
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@itemId", item.IdProduct);
                cmd.Parameters.AddWithValue("@idwarehouse",item.IdWarehouse);
                await conn.OpenAsync();
                var dr = await cmd.ExecuteReaderAsync();
                while (await dr.ReadAsync())
                {
                    if (dr["Count"].ToString().Equals("1"))
                        return true;
                }
            }
            return false;
        }

        public async Task FullfilledAtOrder(Item item)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand())
            {
                DbTransaction db = await conn.BeginTransactionAsync();
                cmd.Transaction = (SqlTransaction)db;
                try
                {
                    string sql = $"update \"order\" set FulfilledAt = CURRENT_TIMESTAMP where idproduct = @itemId;";
                    cmd.Parameters.AddWithValue("@itemId", item.IdProduct);
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    await db.CommitAsync();
                }
                catch (SqlException e)
                {
                    await db.RollbackAsync();
                }
            }
        }

        public async Task<string> InsertProductWarehouse(Item item)
        {
            using (var conn = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand())
            {
                DbTransaction db = await conn.BeginTransactionAsync();
                cmd.Transaction = (SqlTransaction) db;
                try
                {
                    string sql = $"INSERT INTO Product_Warehouse(IdWarehouse,IdProduct,IdOrder,Amount,Price,CreatedAt)" +
                        $" select @warehouseId, @itemId, IdOrder, @amount, @amount * p.price, CURRENT_TIMESTAMP" +
                        $" from \"order\" o , product p where p.IdProduct = o.IdProduct ; ";
                    cmd.Parameters.AddWithValue("@itemId", item.IdProduct);
                    cmd.Parameters.AddWithValue("@warehouseId", item.IdWarehouse);
                    cmd.Parameters.AddWithValue("@amount", item.Amount);
                    cmd.CommandText = sql;
                    cmd.Connection = conn;
                    await conn.OpenAsync();
                    await cmd.ExecuteNonQueryAsync();
                    string sql2 = $"select idproductwarehouse" +
                       $" from product_warehouse pw , \"order\" o where pw.idorder = o.idorder; ";
                    cmd.CommandText = sql2;
                    var dr = await cmd.ExecuteReaderAsync();
                    while (await dr.ReadAsync())
                    {
                        return dr["Idproductwarehouse"].ToString();
                    }
                    await db.CommitAsync();
                }
                catch (SqlException e)
                { 
                    await db.RollbackAsync();
                }
            }
            return null;
        }
    }
}
