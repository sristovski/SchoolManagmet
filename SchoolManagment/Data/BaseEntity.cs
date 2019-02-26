
using System;
namespace SchoolManagment.Data
{
    public  abstract class BaseEntity
    {
        public DateTime Created_At { get; set; }
        public DateTime Updated_At { get; set; }
    }
}