using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Staff.WebApi.Model
{
    public class StaffList
    {
        //pageSize: number;
        public int PageSize { get; set; }

        //pageNum: number;
        public int PageNum { get; set; }

        //staff: IStaffItem[];
        public StaffItem[] StaffItems { get; set; }

        public int PagesCount { get; set; }
    }
}
