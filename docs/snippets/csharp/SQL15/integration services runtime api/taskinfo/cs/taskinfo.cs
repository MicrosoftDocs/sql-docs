//<snippetTaskInfoCS>
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SqlServer.Dts.Runtime;

namespace TaskType
{
    class Program
    {
        static void Main(string[] args)
        {
            Application myApp = new Application();
            TaskInfos tInfos = myApp.TaskInfos;

            // Iterate through the collection, 
            // printing values for the properties.
            foreach (TaskInfo tInfo in tInfos)
            {
                Console.WriteLine("CreationName:   {0}", tInfo.CreationName);
                Console.WriteLine("Description     {0}", tInfo.Description);
                Console.WriteLine("FileName        {0}", tInfo.FileName);
                // Console.WriteLine("FileNameVersionString   {0}", tInfo.FileNameVersionString);
                Console.WriteLine("IconFile        {0}", tInfo.IconFile);
                Console.WriteLine("IconResource    {0}", tInfo.IconResource);
                Console.WriteLine("ID              {0}", tInfo.ID);
                Console.WriteLine("Name            {0}", tInfo.Name);
                // Console.WriteLine("TaskContact     {0}", tInfo.TaskContact);
                Console.WriteLine("TaskType        {0}", tInfo.TaskType);
                Console.WriteLine("UITypeName      {0}", tInfo.UITypeName);
                Console.WriteLine("----------------------------");
            }
            Console.WriteLine();
        }
    }
}
//</snippetTaskInfoCS>