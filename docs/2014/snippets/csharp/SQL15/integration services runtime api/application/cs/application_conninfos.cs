//<snippetApplication_connInfosCS>
class ApplicationTests1
    {
        static void Main(string[] args)
        {
          Application app = new Application();

          ConnectionInfos cis = app.ConnectionInfos;
          foreach (ConnectionInfo x in cis)
              Console.WriteLine("Connection Type" + x.ConnectionType);
        }
    }
}

//</snippetApplication_connInfosCS>