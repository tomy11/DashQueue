using Dapper;
using DashReport.Data;
using DashReport.Data.Model;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DashReport.Service
{
    public class StaffRepository : IStaffRepository
    {
        private readonly IConfiguration _connectionString;
        public StaffRepository(IConfiguration configuration)
        {
            _connectionString = configuration;
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(_connectionString.GetConnectionString("DefaultConnection"));
            }
        }
          
        public async Task<List<Staff>> GetAll()
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var result = await dbConnection.QueryAsync<Staff>("SELECT * FROM tb_staff");
                    return result.ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<Staff> GetByID(int id)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", id);
                    var result = await dbConnection.QueryAsync<Staff>("SELECT * FROM tb_staff WHERE uid = '"+id+"' ");
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Staff> Save(Staff modeldata)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Username", modeldata.username);
                    param.Add("@Password", modeldata.password);
                    param.Add("@Name", modeldata.name);
                    param.Add("@Role", modeldata.role);
                    param.Add("@Status", modeldata.status);
                    param.Add("@Cwhen", modeldata.cwhen);
                    var result = await dbConnection.QueryAsync("INSERT INTO tb_staff (username,password,name,role,status,cwhen) VALUES(@Username,@Password,@Name,@Role,@Status,@Cwhen)");
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<Staff> Edit(Staff modeldata)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", modeldata.uid);
                    param.Add("@Username", modeldata.username);
                    param.Add("@Password", modeldata.password);
                    param.Add("@Name", modeldata.name);
                    param.Add("@Role", modeldata.role);
                    param.Add("@Status", modeldata.status);
                    param.Add("@Cwhen", modeldata.cwhen);
                    var result = await dbConnection.QueryAsync("UPDATE tb_staff SET username=@Username,password=@Password,name=@Name,role=@Role,status=@Status,cwhen=@Cwhen WHERE uid = @Id ");
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<Staff> Delete(int id)
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    DynamicParameters param = new DynamicParameters();
                    param.Add("@Id", id);
                    var result = await dbConnection.QueryAsync<Staff>("DELETE FROM tb_staff WHERE uid = '" + id + "' ");
                    return result.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Staff> ShowAll()
        {
            try
            {
                using (IDbConnection dbConnection = Connection)
                {
                    dbConnection.Open();
                    var result = dbConnection.Query<Staff>("SELECT * FROM tb_staff");
                    return result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
