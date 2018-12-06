--<snippetApplication_DataTypeInfosCS>
class ApplicationTests1
    {
        static void Main(string[] args)
        {
            DataTypeInfos dti = new Application().DataTypeInfos;

            foreach (DataTypeInfo x in dti) 
                Console.WriteLine(x.TypeName + " , " + x.TypeEnumName);
        }
    }
--</snippetApplication_DataTypeInfosCS>
--<snippetApplication_DBProviderInfosCS>
class ApplicationTests
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            DBProviderInfos dbis = app.DBProviderInfos;
            Console.WriteLine("Database Provider Info:");
            foreach (DBProviderInfo dbi in dbis)
                Console.WriteLine(dbi.Description);
        }
    }
--</snippetApplication_DBProviderInfosCS>
--<snippetApplication_ForEachEnumeratorInfosCS>
class ApplicationTests
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            ForEachEnumeratorInfos fee = app.ForEachEnumeratorInfos;
            foreach(ForEachEnumeratorInfo x in fee)
                Console.WriteLine(x.ID);
        }
    }
--</snippetApplication_ForEachEnumeratorInfosCS>
--<snippetApplication_LogProviderInfosCS>
class ApplicationTests
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            LogProviderInfos lpis = app.LogProviderInfos;
            foreach (LogProviderInfo x in lpis)
                Console.WriteLine(x.Name);
        }
    }
--</snippetApplication_LogProviderInfosCS>
--<snippetApplication_PipelineComponentInfosCS>
class ApplicationTests
    {
        static void Main(string[] args)
        {
            Application app = new Application();
            PipelineComponentInfos pcis = app.PipelineComponentInfos;
            foreach (PipelineComponentInfo x in pcis)
                Console.WriteLine(x.CreationName);
        }
    }
--</snippetApplication_PipelineComponentInfosCS>
--<snippetApplication_TaskInfosCS>
class ApplicationTests
    {
        static void Main(string[] args)
        {
            Application app = new Application();

            TaskInfos tis = app.TaskInfos;
            foreach (TaskInfo x in tis)
                Console.WriteLine(x.FileName + "..." + x.Description);
        }
    }
--</snippetApplication_TaskInfosCS>




