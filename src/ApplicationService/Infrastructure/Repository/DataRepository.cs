using Infrastructure.Abstractions;
using Infrastructure.Constants;
using Infrastructure.DataServices;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository
{
    public class DataRepository : IDataRepository
    {

        private readonly StorageData storageData;
        public DataRepository(StorageData storage)
        {
            this.storageData = storage;
        }
        public async Task<int> AddAsync(WebPictureInfo entity)
        {
            return await this.SaveEntityAsync(entity);
        }
        public async Task<int> AddRangeAsync(List<WebPictureInfo> entities)
        {
            return await this.SaveRangeAsync(entities);
        }

        public async Task<WebPictureInfo> GetByIdAsync(int id)
        {
            using (SqlConnection sql = new SqlConnection(this.storageData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlCommands.Select_ById, sql))
                {
                    cmd.Parameters.Add(new SqlParameter("@id", id));
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        WebPictureInfo webPictureInfo = null;
                        while (await reader.ReadAsync())
                        {
                            webPictureInfo = new WebPictureInfo()
                            {
                                Id = int.Parse(reader[0].ToString()),
                                Brand = reader[1].ToString(),
                                Url = reader[3].ToString(),
                                Rating = int.Parse(reader[4].ToString())
                            };
                        }
                        return webPictureInfo;
                    }
                }
            }

        }

        public async Task<WebPictureInfo> GetByNameAsync(string brand)
        {
            using (SqlConnection sql = new SqlConnection(this.storageData.ConnectionString))
            {

                using (SqlCommand cmd = new SqlCommand("", sql))
                {
                    string name = "%" + brand + "%";
                    cmd.Parameters.Add(new SqlParameter("@name", name));
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        WebPictureInfo webPictureInfo = null;
                        while (await reader.ReadAsync())
                        {
                            webPictureInfo = new WebPictureInfo()
                            {
                                Id = int.Parse(reader[0].ToString()),
                                Brand = reader[1].ToString(),
                                Url = reader[3].ToString(),
                                Rating = int.Parse(reader[4].ToString())
                            };

                        }
                        return webPictureInfo;
                    }
                }
            }
        }

        public async Task<IEnumerable<WebPictureInfo>> ListEntitiesAsync()
        {
            List<WebPictureInfo> entities = new List<WebPictureInfo>();
            using (SqlConnection sql = new SqlConnection(this.storageData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlCommands.Select_WebPictures))
                {
                    await sql.OpenAsync();
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            WebPictureInfo webPictureInfo = new WebPictureInfo()
                            {
                                Id = int.Parse(reader[0].ToString()),
                                Brand = reader[1].ToString(),
                                Url = reader[3].ToString(),
                                Rating = int.Parse(reader[4].ToString())
                            };
                            entities.Add(webPictureInfo);
                        }
                    }
                }
            }
            return entities;
        }

        public async Task<List<WebPictureInfo>> GetTop10()
        {
            List<WebPictureInfo> models = new List<WebPictureInfo>();

            using (SqlConnection sql = new SqlConnection(this.storageData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlCommands.Select_Top_10, sql))
                {
                    await sql.OpenAsync();
                    using (var data = await cmd.ExecuteReaderAsync())
                    {
                        while (await data.ReadAsync())
                        {
                            models.Add(new WebPictureInfo()
                            {
                                Brand = data[0].ToString(),
                                Rating = int.Parse(data[1].ToString())
                            });
                        }
                    }
                }
            }
            return models;
        }

        private async Task<int> SaveRangeAsync(List<WebPictureInfo> models)
        {

            using (SqlConnection sql = new SqlConnection(storageData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertData", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var res = 0;
                    try
                    {


                        foreach (var item in models)
                        {
                            cmd.Parameters.AddWithValue("@brandName", item.Brand);
                            cmd.Parameters.AddWithValue("@url", item.Url);
                            cmd.Parameters.AddWithValue("@rate", item.Rating);

                            await sql.OpenAsync();
                            res = await cmd.ExecuteNonQueryAsync();

                            sql.Close();
                            cmd.Parameters.Clear();
                        }

                        return res;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
            }
        }
        private async Task<int> SaveEntityAsync(WebPictureInfo model)
        {
            using (SqlConnection sql = new SqlConnection(storageData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("InsertData", sql))
                {

                    try
                    {
                        cmd.CommandType = CommandType.StoredProcedure;


                        await sql.OpenAsync();

                        cmd.Parameters.AddWithValue("@brandName", model.Brand);
                        cmd.Parameters.AddWithValue("@url", model.Url);
                        cmd.Parameters.AddWithValue("@rate", model.Rating);

                        var res = await cmd.ExecuteNonQueryAsync();
                        return res;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                    finally
                    {
                        sql.Close();

                    }

                }
            }
        }

        public async Task<Dictionary<string, List<Picture>>> ListBrandsAsync()
        {
            Dictionary<string, List<Picture>> dictionary = new Dictionary<string, List<Picture>>();

            using (SqlConnection sql = new SqlConnection(this.storageData.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(SqlCommands.Select_WebPictures, sql))
                {
                    await sql.OpenAsync();
                    using (var data = await cmd.ExecuteReaderAsync())
                    {
                        while (await data.ReadAsync())
                        {
                            string Brand = data[0].ToString();

                            Picture picture = new Picture()
                            {
                                Url = data[1].ToString(),
                                Rate = int.Parse(data[2].ToString())
                            };

                            if (!dictionary.ContainsKey(Brand))
                            {
                                dictionary.Add(Brand, new List<Picture>());
                            }
                            dictionary[Brand].Add(picture);
                        }
                    }
                }
            }

            return dictionary;
        }
    }
}
