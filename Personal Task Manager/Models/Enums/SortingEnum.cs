using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.Models.Enums
{
    public enum SortingEnum
    {
        [Description("Due date ascending")]
        ByDueDateAscending,
        [Description("Due date descending")]
        ByDueDateDescending,
    }
}
