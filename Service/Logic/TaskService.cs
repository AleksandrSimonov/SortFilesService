using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Service.Logic
{
    internal class TaskService
    {
        private string _folder;
        public List<string> Extensions { get; }
        public TaskService(string folder, List<string> extension)
        {
            _folder = folder;
            Extensions = extension;
        }
        public void DoServiceLogic()
        {
            var files = Directory.GetFiles(_folder);
            int filesCount = files.Length;
            FileInfo file;
            string ext;

            for (int i = 0; i < filesCount; i++)
            {
                file = new FileInfo(files[i]);
                ext = file.Extension;
                if (Extensions.Contains(ext))
                {
                    if (!Directory.Exists(_folder + @"\" + ext))
                    {
                        Directory.CreateDirectory(_folder + @"\" + ext);
                        Logger.Log.Info("добавленна папка " + ext);
                    }

                    if (File.Exists(_folder + @"\" + ext + @"\" + file.Name))
                    {
                        var onlyName = new Regex(@"^(.+)\(\d+\)\..+$");
                        string name = onlyName.Match(file.Name).Groups[1].Value;

                        var indexNumberReg = new Regex(@"\((\d+)\)\..+$");
                        int indexNumber = int.Parse(indexNumberReg.Match(file.Name).Groups[1].Value);
                        try
                        {
                            file.MoveTo(_folder + @"\" + ext + @"\" + name + " (" + (indexNumber + 1) + ")" + file.Extension);
                            Logger.Log.Info("файл " + file.Name + " был перемещен");
                        }
                        catch (IOException)
                        {
                            Logger.Log.Error("не удалось переместить файл " + file.Name);
                        }
                    }
                    else
                    {
                        try
                        {
                            file.MoveTo(_folder + @"\" + ext + @"\" + file.Name);
                            Logger.Log.Info("файл " + file.Name + " был перемещен");
                        }
                        catch (IOException)
                        {
                            Logger.Log.Error("не удалось переместить файл " + file.Name);
                        }
                    }
                }
                else
                {
                    if (!Directory.Exists(_folder + @"\trash"))
                    {
                        Directory.CreateDirectory(_folder + @"\trash");
                        Logger.Log.Info("добавленна папка " + ext);
                    }


                    try
                    {
                        file.MoveTo(_folder + @"\trash\" + file.Name);
                        Logger.Log.Info("файл " + file.Name + " был перемещен");
                    }
                    catch (IOException)
                    {
                        Logger.Log.Error("не удалось переместить файл " + file.Name);
                    }
                }
            }
        }
        public async Task RunAsync(int timeout)
        {
            while (true)
            {
                await Task.Run(DoServiceLogic);
                await Task.Delay(timeout);
            }

        }
        //избавиться от прямой зависимости от log4net
    }
}
