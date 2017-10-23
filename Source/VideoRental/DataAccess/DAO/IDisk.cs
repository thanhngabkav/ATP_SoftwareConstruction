using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;
namespace DataAccess.DAO
{
    public interface IDisk
    {
        void writeDiskStatus(String diskID, String status);

        Disk getAllRentedDisk();


    }
}
