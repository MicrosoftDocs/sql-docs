---
title: Extend Database Project Build to Generate Model Stats
description: Find out how to create, install, and test a build contributor that outputs statistics from the SQL database model when you build a database project.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: d44935ce-63bf-46df-976a-5a54866c8119
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# Walkthrough: Extend Database Project Build to Generate Model Statistics

You can create a build contributor to perform custom actions when you build a database project. In this walkthrough, you create a build contributor named ModelStatistics that outputs statistics from the SQL database model when you build a database project. Because this build contributor takes parameters when you build, some additional steps are required.  
  
In this walkthrough, you will accomplish the following major tasks:  
  
-   [Create a build contributor](#CreateBuildContributor)  
  
-   [Install the build contributor](#InstallBuildContributor)  
  
-   [Test your build contributor](#TestBuildContributor)  
  
## Prerequisites  
You need the following components to complete this walkthrough:  
  
-   You must have installed a version of Visual Studio that includes SQL Server Data Tools (SSDT) and supports C# or VB development.  
  
-   You must have a SQL project that contains SQL objects.  
  
> [!NOTE]  
> This walkthrough is intended for users who are already familiar with the SQL features of SSDT. You are also expected to be familiar with basic Visual Studio concepts, such as how to create a class library and how to use the code editor to add code to a class.  
  
## Build Contributor Background  
Build contributors are run during project build, after the model representing the project has been generated but before the project is saved to disk. They can be used for a number of scenarios, such as  
  
-   Validating the model contents and reporting validation errors to the caller. This can be done by adding errors to a list passed as a parameter to the OnExecute method.  
  
-   Generating model statistics and reporting to the user. This is the example shown here.  
  
The main entry point for build contributors is the OnExecute method. All classes inheriting from BuildContributor must implement this method. A BuildContributorContext object is passed to this method - this contains all the relevant data for the build, such as a model of the database, build properties, and arguments/files to be used by build contributors.  
  
**TSqlModel and the database model API**  
  
The most useful object will be the database model, represented by a TSqlModel object. This is a logical representation of a database, including all the tables, view, and other elements, plus the relationships between them. There is a strongly-typed schema that can be used to query for specific types of elements and traverse interesting relationships. You will see examples of how this is used in the walkthrough code.  
  
Here are some of the commands used by the example contributor in this walkthrough:  
  
|**Class**|**Method/Property**|**Description**|  
|-------------|------------------------|-------------------|  
|[TSqlModel](/dotnet/api/microsoft.sqlserver.dac.model.tsqlmodel)|GetObjects()|Queries the model for objects, and is the main entry point to the model API. Only top-level types such as Table or View can be queried - types such as Columns can only be found by traversing the model. If no ModelTypeClass filters are specified then all top level types will be returned.|  
|[TSqlObject](/dotnet/api/microsoft.sqlserver.dac.model.tsqlobject)|GetReferencedRelationshipInstances()|Finds relationships to elements referenced by the current TSqlObject. For instance, for a table this will return objects like the Table's columns. In this case, a ModelRelationshipClass filter can be used to specify exact relationships to query (for instance using the "Table.Columns" filter would ensure only columns were returned).<br /><br />There are a number of similar methods, such as GetReferencingRelationshipInstances, GetChildren, and GetParent. See the API documentation for more information.|  
  
**Uniquely Identifying your Contributor**  
  
During the build process, custom contributors are loaded from a standard extension directory. Build Contributors are identified by an [ExportBuildContributor](/dotnet/api/microsoft.sqlserver.dac.deployment.exportbuildcontributorattribute) attribute. This attribute is required so that contributors can be discovered. This attribute should look similar to the following:  
  
```  
[ExportBuildContributor("ExampleContributors.ModelStatistics", "1.0.0.0")]  
  
```  
  
In this case the first parameter to the attribute should be a unique identifier - this will be used to identify your contributor in project files. Best practice is to combine your library's namespace (in this walkthrough, "ExampleContributors") with the class name (in this walkthrough, "ModelStatistics") to produce the identifier. You will see how this namespace is used to specify that your contributor should be run later in the walkthrough.  
  
## <a name="CreateBuildContributor"></a>Create a Build Contributor  
To create a build contributor, you must perform the following tasks:  
  
-   Create a class library project and add required references.  
  
-   Define a class named ModelStatistics that inherits from [BuildContributor](/dotnet/api/microsoft.sqlserver.dac.deployment.buildcontributor).  
  
-   Override the OnExecute method.  
  
-   Add a few private helper methods.  
  
-   Build the resulting assembly.  
  
#### To create a class library project  
  
1.  Create a Visual Basic or Visual C# class library project named MyBuildContributor.  
  
2.  Rename the file "Class1.cs" to "ModelStatistics.cs."  
  
3.  In Solution Explorer, right-click the project node and then click **Add Reference**.  
  
4.  Select the **System.ComponentModel.Composition** entry and then click **OK**.  
  
5.  Add required SQL references: right-click the project node and then click **Add Reference**. Click the **Browse** button. Navigate to the **C:\Program Files (x86)\Microsoft SQL Server\110\DAC\Bin** folder. Choose the **Microsoft.SqlServer.Dac.dll**, **Microsoft.SqlServer.Dac.Extensions.dll**, and **Microsoft.Data.Tools.Schema.Sql.dll** entries, and then click **OK**.  
  
    Next, you start to add code to the class.  
  
#### To define the ModelStatistics class  
  
1.  The ModelStatistics class processes the database model passed to the OnExecute method, and produces and XML Report detailing the contents of the model.  
  
    In the code editor, update the ModelStatistics.cs  file to match the following:  
  
    ```csharp  
    using System;  
    using System.Collections.Generic;  
    using System.IO;  
    using System.Linq;  
    using System.Xml.Linq;  
    using Microsoft.Data.Schema;  
    using Microsoft.Data.Schema.Build;  
    using Microsoft.Data.Schema.Extensibility;  
    using Microsoft.Data.Schema.SchemaModel;  
    using Microsoft.Data.Schema.Sql;  
  
    namespace ExampleContributors  
    {  
    /// <summary>  
        /// A BuildContributor that generates statistics about a model and saves this to the output directory.  
        /// Will only run if a "GenerateModelStatistics=true" contributor argument is set in the project file, or a targets file.   
        /// Statistics can be sorted by "none, "name" or "value", with "none" being the default sort behavior.  
        ///   
        /// To set contributor arguments in a project file, add the following:  
        ///   
        /// <PropertyGroup>  
        ///     <ContributorArguments Condition="'$(Configuration)' == 'Debug'">  
        /// $(ContributorArguments);ModelStatistics.GenerateModelStatistics=true;ModelStatistics.SortModelStatisticsBy="name";  
        ///     </ContributorArguments>  
        /// <PropertyGroup>      
        ///   
        /// This will generate model statistics when building in Debug mode only - remove the condition to generate in all build modes.  
        /// </summary>  
        [ExportBuildContributor("ExampleContributors.ModelStatistics", "1.0.0.0")]  
        public class ModelStatistics : BuildContributor  
        {  
            public const string GenerateModelStatistics = "ModelStatistics.GenerateModelStatistics";  
            public const string SortModelStatisticsBy = "ModelStatistics.SortModelStatisticsBy";  
            public const string OutDir = "ModelStatistics.OutDir";  
            public const string ModelStatisticsFilename = "ModelStatistics.xml";  
            private enum SortBy { None, Name, Value };  
            private static Dictionary<string, SortBy> SortByMap = new Dictionary<string, SortBy>(StringComparer.OrdinalIgnoreCase)  
            {  
                { "none", SortBy.None },  
                { "name", SortBy.Name },  
                { "value", SortBy.Value },  
            };  
  
            private SortBy _sortBy = SortBy.None;  
  
            /// <summary>  
            /// Override the OnExecute method to perform actions when you build a database project.  
            /// </summary>  
            protected override void OnExecute(BuildContributorContext context, IList<ExtensibilityError> errors)  
            {  
                // handle related arguments, passed in as part of  
                // the context information.  
                bool generateModelStatistics;  
                ParseArguments(context.Arguments, errors, out generateModelStatistics);  
  
                // Only generate statistics if requested to do so  
                if (generateModelStatistics)  
                {  
                    // First, output model-wide information, such  
                    // as the type of database schema provider (DSP)  
                    // and the collation.  
                    StringBuilder statisticsMsg = new StringBuilder();  
                    statisticsMsg.AppendLine(" ")  
                                 .AppendLine("Model Statistics:")  
                                 .AppendLine("===")  
                                 .AppendLine(" ");  
                    errors.Add(new ExtensibilityError(statisticsMsg.ToString(), Severity.Message));  
  
                    var model = context.Model;  
  
                    // Start building up the XML that will later  
                    // be serialized.  
                    var xRoot = new XElement("ModelStatistics");  
  
                    SummarizeModelInfo(model, xRoot, errors);  
  
                    // First, count the elements that are contained   
                    // in this model.  
                    IList<TSqlObject> elements = model.GetObjects(DacQueryScopes.UserDefined).ToList();  
                    Summarize(elements, element => element.ObjectType.Name, "UserDefinedElements", xRoot, errors);  
  
                    // Now, count the elements that are defined in  
                    // another model. Examples include built-in types,  
                    // roles, filegroups, assemblies, and any   
                    // referenced objects from another database.  
                    elements = model.GetObjects(DacQueryScopes.BuiltIn | DacQueryScopes.SameDatabase | DacQueryScopes.System).ToList();  
                    Summarize(elements, element => element.ObjectType.Name, "OtherElements", xRoot, errors);  
  
                    // Now, count the number of each type  
                    // of relationship in the model.  
                    SurveyRelationships(model, xRoot, errors);  
  
                    // Determine where the user wants to save  
                    // the serialized XML file.  
                    string outDir;  
                    if (context.Arguments.TryGetValue(OutDir, out outDir) == false)  
                    {  
                        outDir = ".";  
                    }  
                    string filePath = Path.Combine(outDir, ModelStatisticsFilename);  
                    // Save the XML file and tell the user  
                    // where it was saved.  
                    xRoot.Save(filePath);  
                    ExtensibilityError resultArg = new ExtensibilityError("Result was saved to " + filePath, Severity.Message);  
                    errors.Add(resultArg);  
                }  
            }  
  
            /// <summary>  
            /// Examine the arguments provided by the user  
            /// to determine if model statistics should be generated  
            /// and, if so, how the results should be sorted.  
            /// </summary>  
            private void ParseArguments(IDictionary<string, string> arguments, IList<ExtensibilityError> errors, out bool generateModelStatistics)  
            {  
                // By default, we don't generate model statistics  
                generateModelStatistics = false;  
  
                // see if the user provided the GenerateModelStatistics   
                // option and if so, what value was it given.  
                string valueString;  
                arguments.TryGetValue(GenerateModelStatistics, out valueString);  
                if (string.IsNullOrWhiteSpace(valueString) == false)  
                {  
                    if (bool.TryParse(valueString, out generateModelStatistics) == false)  
                    {  
                        generateModelStatistics = false;  
  
                        // The value was not valid from the end user  
                        ExtensibilityError invalidArg = new ExtensibilityError(  
                            GenerateModelStatistics + "=" + valueString + " was not valid.  It can be true or false", Severity.Error);  
                        errors.Add(invalidArg);  
                        return;  
                    }  
                }  
  
                // Only worry about sort order if the user requested  
                // that we generate model statistics.  
                if (generateModelStatistics)  
                {  
                    // see if the user provided the sort option and  
                    // if so, what value was provided.  
                    arguments.TryGetValue(SortModelStatisticsBy, out valueString);  
                    if (string.IsNullOrWhiteSpace(valueString) == false)  
                    {  
                        SortBy sortBy;  
                        if (SortByMap.TryGetValue(valueString, out sortBy))  
                        {  
                            _sortBy = sortBy;  
                        }  
                        else  
                        {  
                            // The value was not valid from the end user  
                            ExtensibilityError invalidArg = new ExtensibilityError(  
                                SortModelStatisticsBy + "=" + valueString + " was not valid.  It can be none, name, or value", Severity.Error);  
                            errors.Add(invalidArg);  
                        }  
                    }  
                }  
            }  
  
            /// <summary>  
            /// Retrieve the database schema provider for the  
            /// model and the collation of that model.  
            /// Results are output to the console and added to the XML  
            /// being constructed.  
            /// </summary>  
            private static void SummarizeModelInfo(TSqlModel model, XElement xContainer, IList<ExtensibilityError> errors)  
            {  
                // use a Dictionary to accumulate the information  
                // that will later be output.  
                var info = new Dictionary<string, string>();  
  
                // Two things of interest: the database schema  
                // provider for the model, and the language id and  
                // case sensitivity of the collation of that  
                // model  
                info.Add("Version", model.Version.ToString());  
  
                TSqlObject options = model.GetObjects(DacQueryScopes.UserDefined, DatabaseOptions.TypeClass).FirstOrDefault();  
                if (options != null)  
                {  
                    info.Add("Collation", options.GetProperty<string>(DatabaseOptions.Collation));  
                }  
  
                // Output the accumulated information and add it to   
                // the XML.  
                OutputResult("Basic model info", info, xContainer, errors);  
            }  
  
            /// <summary>  
            /// For a provided list of model elements, count the number  
            /// of elements for each class name, sorted as specified  
            /// by the user.  
            /// Results are output to the console and added to the XML  
            /// being constructed.  
            /// </summary>  
            private void Summarize<T>(IList<T> set, Func<T, string> groupValue, string category, XElement xContainer, IList<ExtensibilityError> errors)  
            { // Use a Dictionary to keep all summarized information  
                var statistics = new Dictionary<string, int>();  
  
                // For each element in the provided list,  
                // count items based on the specified grouping  
                var groups =  
                    from item in set  
                    group item by groupValue(item) into g  
                    select new { g.Key, Count = g.Count() };  
  
                // order the groups as requested by the user  
                if (this._sortBy == SortBy.Name)  
                {  
                    groups = groups.OrderBy(group => group.Key);  
                }  
                else if (this._sortBy == SortBy.Value)  
                {  
                    groups = groups.OrderBy(group => group.Count);  
                }  
  
                // build the Dictionary of accumulated statistics  
                // that will be passed along to the OutputResult method.  
                foreach (var item in groups)  
                {  
                    statistics.Add(item.Key, item.Count);  
                }  
  
                statistics.Add("subtotal", set.Count);  
                statistics.Add("total items", groups.Count());  
  
                // output the results, and build up the XML  
                OutputResult(category, statistics, xContainer, errors);  
            }  
  
            /// <summary>  
            /// Iterate over all model elements, counting the  
            /// styles and types for relationships that reference each   
            /// element  
            /// Results are output to the console and added to the XML  
            /// being constructed.  
            /// </summary>  
            private static void SurveyRelationships(TSqlModel model, XElement xContainer, IList<ExtensibilityError> errors)  
            {  
                // get a list that contains all elements in the model  
                var elements = model.GetObjects(DacQueryScopes.All);  
                // We are interested in all relationships that  
                // reference each element.  
                var entries =  
                    from element in elements  
                    from entry in element.GetReferencedRelationshipInstances(DacExternalQueryScopes.All)  
                    select entry;  
  
                // initialize our counting buckets  
                var composing = 0;  
                var hierachical = 0;  
                var peer = 0;  
  
                // process each relationship, adding to the   
                // appropriate bucket for style and type.  
                foreach (var entry in entries)  
                {  
                    switch (entry.Relationship.Type)  
                    {  
                        case RelationshipType.Composing:  
                            ++composing;  
                            break;  
                        case RelationshipType.Hierarchical:  
                            ++hierachical;  
                            break;  
                        case RelationshipType.Peer:  
                            ++peer;  
                            break;  
                        default:  
                            break;  
                    }  
                }  
  
                // build a dictionary of data to pass along  
                // to the OutputResult method.  
                var stat = new Dictionary<string, int>  
                {  
                    {"Composing", composing},  
                    {"Hierarchical", hierachical},  
                    {"Peer", peer},  
                    {"subtotal", entries.Count()}  
                };  
  
                OutputResult("Relationships", stat, xContainer, errors);  
            }  
  
            /// <summary>  
            /// Performs the actual output for this contributor,  
            /// writing the specified set of statistics, and adding any   
            /// output information to the XML being constructed.  
            /// </summary>  
            private static void OutputResult<T>(string category, Dictionary<string, T> statistics, XElement xContainer, IList<ExtensibilityError> errors)  
            {  
                var maxLen = statistics.Max(stat => stat.Key.Length) + 2;  
                var format = string.Format("{{0, {0}}}: {{1}}", maxLen);  
  
                StringBuilder resultMessage = new StringBuilder();  
                //List<ExtensibilityError> args = new List<ExtensibilityError>();  
                resultMessage.AppendLine(category);  
                resultMessage.AppendLine("-----------------");  
  
                // Remove any blank spaces from the category name  
                var xCategory = new XElement(category.Replace(" ", ""));  
                xContainer.Add(xCategory);  
  
                foreach (var item in statistics)  
                {  
                    //Console.WriteLine(format, item.Key, item.Value);  
                    var entry = string.Format(format, item.Key, item.Value);  
                    resultMessage.AppendLine(entry);  
                    // Replace any blank spaces in the element key with  
                    // underscores.  
                    xCategory.Add(new XElement(item.Key.Replace(' ', '_'), item.Value));  
                }  
                resultMessage.AppendLine(" ");  
                errors.Add(new ExtensibilityError(resultMessage.ToString(), Severity.Message));  
            }  
        }  
    }  
  
    ```  
  
    Next, you will build the class library.  
  
### To sign and build the assembly  
  
1.  On the **Project** menu, click **MyBuildContributor Properties**.  
  
2.  Click the **Signing** tab.  
  
3.  Click **Sign the assembly**.  
  
4.  In **Choose a strong name key file**, click **\<New\>**.  
  
5.  In the **Create Strong Name Key** dialog box, in **Key file name**, type **MyRefKey**.  
  
6.  (optional) You can specify a password for your strong name key file.  
  
7.  Click **OK**.  
  
8.  On the **File** menu, click **Save All**.  
  
9. On the **Build** menu, click **Build Solution**.  
  
    Next, you must install the assembly so that it will be loaded when you build SQL projects.  
  
## <a name="InstallBuildContributor"></a>Install a Build Contributor  
To install a build contributor, you must copy the assembly and associated .pdb file to the Extensions folder.  
  
#### To install the MyBuildContributor assembly  
  
1.  Next, you will copy the assembly information to the Extensions directory. When Visual Studio starts, it will identify any extensions in the %Program Files%\Microsoft SQL Server\110\DAC\Bin\Extensions directory and subdirectories, and make them available for use.  
  
2.  Copy the **MyBuildContributor.dll** assembly file from the output directory to the %Program Files%\Microsoft SQL Server\110\DAC\Bin\Extensions directory.  
  
    > [!NOTE]  
    > By default, the path of your compiled .dll file is YourSolutionPath\YourProjectPath\bin\Debug or YourSolutionPath\YourProjectPath\bin\Release.  
  
## <a name="TestBuildContributor"></a>Run or Test your Build Contributor  
To run or test your build contributor, you must perform the following tasks:  
  
-   Add properties to the .sqlproj file that you plan to build.  
  
-   Build the database project using MSBuild and providing the appropriate parameters.  
  
### Add Properties to the SQL Project (.sqlproj) File  
You must always update the SQL project file to specify the ID of the contributors you wish to run. In addition because this build contributor accepts command-line parameters from MSBuild, you must modify the SQL project to enable users to pass those parameters through MSBuild.  
  
You can do this in one of two ways:  
  
-   You can manually modify the .sqlproj file to add the required arguments. You might choose to do this if you do not intend to reuse the build contributor across a large number of projects. If you choose this option, add the following statements to the .sqlproj file after the first Import node in the file  
  
    ```  
    <PropertyGroup>  
        <BuildContributors>  
            $(BuildContributors);ExampleContributors.ModelStatistics  
        </BuildContributors>  
        <ContributorArguments Condition="'$(Configuration)' == 'Debug'">  
            $(ContributorArguments);ModelStatistics.GenerateModelStatistics=true;ModelStatistics.SortModelStatisticsBy=name;  
        </ContributorArguments>  
    </PropertyGroup>  
  
    ```  
  
-   The second method is to create a targets file containing the required contributor arguments. This is useful if you are using the same contributor for multiple projects, since it will include the default values.  
  
    In this case, create a targets file in the MSBuild extensions path:  
  
    1.  Navigate to %Program Files%\MSBuild\\.  
  
    2.  Create a new folder "MyContributors" where your targets files will be stored.  
  
    3.  Create a new file "MyContributors.targets" inside this directory, add the following text to it, and then save the file:  
  
        ```  
        <?xml version="1.0" encoding="utf-8"?>  
  
        <Project xmlns="https://schemas.microsoft.com/developer/msbuild/2003">  
          <PropertyGroup>  
            <BuildContributors>$(BuildContributors);ExampleContributors.ModelStatistics</BuildContributors>  
            <ContributorArguments Condition="'$(Configuration)' == 'Debug'">$(ContributorArguments);ModelStatistics.GenerateModelStatistics=true;ModelStatistics.SortModelStatisticsBy=name;</ContributorArguments>  
          </PropertyGroup>  
        </Project>  
        ```  
  
    4.  Inside the .sqlproj file for any project you want to run contributors, import the targets file by adding the following statement to the .sqlproj file after the \<Import Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets" \/> node in the file:  
  
        ```  
        <Import Project="$(MSBuildExtensionsPath)\MyContributors\MyContributors.targets " />  
        ```  
  
After you have followed one of these approaches, you can use MSBuild to pass in the parameters for command-line builds.  
  
> [!NOTE]  
> You must always update the "BuildContributors" property to specify your contributor ID. This is the same ID used in the "ExportBuildContributor" attribute in your contributor source file. Without this, your contributor will not be run when building the project. The "ContributorArguments" property must be updated only if you have arguments required for your contributor to run.  
  
### Build the SQL Project  
  
##### To rebuild your database project by using MSBuild and generate statistics  
  
1.  In Visual Studio, right-click on your project and select "Rebuild". This will rebuild the project, and you should see the model statistics generated, with the output included in the build output and saved to ModelStatistics.xml. Note that you may need to choose "Show all Files" in Solution Explorer to see the xml file.  
  
2.  Open a Visual Studio command prompt: On the **Start** menu, click **All Programs**, click **Microsoft Visual Studio \<Visual Studio Version\>**, click **Visual Studio Tools**, and then click **Visual Studio Command Prompt (\<Visual Studio Version\>)**.  
  
3.  At the command prompt, navigate to the folder that contains your SQL project.  
  
4.  At the command prompt, type the following command:  
  
    ```  
    MSBuild /t:Rebuild MyDatabaseProject.sqlproj /p:BuildContributors=$(BuildContributors);ExampleContributors.ModelStatistics /p:ContributorArguments=$(ContributorArguments);GenerateModelStatistics=true;SortModelStatisticsBy=name;OutDir=.\;  
    ```  
  
    Replace *MyDatabaseProject* with the name of the database project that you want to build. If you had changed the project after you last built it, you could use /t:Build instead of /t:Rebuild.  
  
    Inside the output you should see build information like the following:  
  
```  
Model Statistics:  
===  
  
Basic model info  
-----------------  
    Version: Sql110  
  Collation: SQL_Latin1_General_CP1_CI_AS  
  
UserDefinedElements  
-----------------  
  DatabaseOptions: 1  
         subtotal: 1  
      total items: 1  
  
OtherElements  
-----------------  
                Assembly: 1  
       BuiltInServerRole: 9  
           ClrTypeMethod: 218  
  ClrTypeMethodParameter: 197  
         ClrTypeProperty: 20  
                Contract: 6  
                DataType: 34  
                Endpoint: 5  
               Filegroup: 1  
             MessageType: 14  
                   Queue: 3  
                    Role: 10  
                  Schema: 13  
                 Service: 3  
                    User: 4  
         UserDefinedType: 3  
                subtotal: 541  
             total items: 16  
  
Relationships  
-----------------  
     Composing: 477  
  Hierarchical: 6  
          Peer: 19  
      subtotal: 502  
  
```  
  
1.  Open ModelStatistics.xml and examine the contents.  
  
    The results that were reported are also persisted to the XML file.  
  
## Next Steps  
You could create additional tools to perform processing of the output XML file. This is just one example of a build contributor. You could, for example, create a build contributor to output a data dictionary file as part of your build.  
  
## See Also  
[Customize Database Build and Deployment by Using Build and Deployment Contributors](../ssdt/use-deployment-contributors-to-customize-database-build-and-deployment.md)  
[Walkthrough: Extend Database Project Deployment to Analyze the Deployment Plan](../ssdt/walkthrough-extend-database-project-deployment-to-analyze-the-deployment-plan.md)  
