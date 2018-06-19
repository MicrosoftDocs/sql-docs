//<snippetApplication_CheckSignatureOnLoadCS>
class ApplicationTests
{
    static void Main(string[] args)
    {
        Application app = new Application();

        Boolean sig = app.CheckSignatureOnLoad;
        Console.Writeline(sig);
    }
}
//</snippetApplication_CheckSignatureOnLoadCS>