using Personal_Task_Manager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.DataServices
{
    public interface IDataProvider
    {
        /// <summary>
        /// Saves the data to a persistent storage.
        /// </summary>
        /// <param name="data">The data to save.</param>
        void SaveData(List<TaskItem> data);
        /// <summary>
        /// Loads the data from persistent storage.
        /// </summary>
        /// <returns>The loaded data.</returns>
        List<TaskItem> LoadData();
        /// <summary>
        /// Deletes the saved data.
        /// </summary>
    }
}
