using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Logic
{
    class SortByExtension
    {
        public string Folder { get; }
        public int Timer { get; }
        public List<string> Extension { get; }

        public SortByExtension(string folder, int timer)
        {
            Folder = folder;
            Timer = timer;
            Extension = new List<string>();
            Extension.Add(".xml");
            Extension.Add(".txt");
        }
        public async void Start() 
        {
            var task = new TaskService(Folder, Extension);
            task.RunAsync(Timer);
        }

    }
}
