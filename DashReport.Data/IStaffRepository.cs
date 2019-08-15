using DashReport.Data.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DashReport.Data
{
    public interface IStaffRepository
    {
        IEnumerable<Staff> ShowAll();
        Task<Staff> GetByID(int id);
        Task<List<Staff>> GetAll();
        Task<Staff> Save(Staff modeldata);
        Task<Staff> Edit(Staff modeldata);
        Task<Staff> Delete(int id);
    }
}
