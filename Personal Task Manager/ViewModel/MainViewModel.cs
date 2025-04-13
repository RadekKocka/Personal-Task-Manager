using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Task_Manager.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        private String searchText;

        public String SearchText
        {
            get => searchText;
            set
            {
                searchText = value;
                OnPropertyChanged(nameof(SearchText));
            }
        }
    }
}
