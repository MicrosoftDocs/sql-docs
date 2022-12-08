---
title: Extend Database Project Deployment to Analyze the Deployment Plan
description: Create a DeploymentPlanExecutor deployment contributor. Set up a contributor that keeps a record of the events that occur when you deploy a database project.
ms.service: sql
ms.subservice: ssdt
ms.topic: conceptual
ms.assetid: 9ead8470-93ba-44e3-8848-b59322e37621
author: markingmyname
ms.author: maghan
ms.custom: seo-lt-2019
ms.date: 02/09/2017
---

# Walkthrough: Extend Database Project Deployment to Analyze the Deployment Plan

You can create deployment contributors to perform custom actions when you deploy a SQL project. You can create either a DeploymentPlanModifier or a DeploymentPlanExecutor. Use a DeploymentPlanModifier to change the plan before it is executed and a DeploymentPlanExecutor to perform operations while the plan is being executed. In this walkthrough, you create a DeploymentPlanExecutor named DeploymentUpdateReportContributor that creates a report about the actions that are performed when you deploy a database project. Because this build contributor accepts a parameter to control whether the report is generated, you must perform an additional required step.  
  
In this walkthrough, you will accomplish the following major tasks:  
  
-   [Create the DeploymentPlanExecutor type of deployment contributor](#CreateDeploymentContributor)  
  
-   [Install the deployment contributor](#InstallDeploymentContributor)  
  
-   [Test your deployment contributor](#TestDeploymentContributor)  
  
## Prerequisites  
You need the following components to complete this walkthrough:  
  
-   You must have installed a version of Visual Studio that includes SQL Server Data Tools (SSDT) and supports C# or VB development.  
  
-   You must have a SQL project that contains SQL objects.  
  
-   An instance of SQL Server to which you can deploy a database project.  
  
> [!NOTE]  
> This walkthrough is intended for users who are already familiar with the SQL features of SSDT. You are also expected to be familiar with basic Visual Studio concepts, such as how to create a class library and how to use the code editor to add code to a class.  
  
## <a name="CreateDeploymentContributor"></a>Create a Deployment Contributor  
To create a deployment contributor, you must perform the following tasks:  
  
-   Create a class library project and add required references.  
  
-   Define a class named DeploymentUpdateReportContributor that inherits from [DeploymentPlanExecutor](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanexecutor).  
  
-   Override the OnExecute method.  
  
-   Add a private helper class.  
  
-   Build the resulting assembly.  
  
#### To create a class library project  
  
1.  Create a Visual Basic or Visual C# class library project named MyDeploymentContributor.  
  
2.  Rename the file "Class1.cs" to "DeploymentUpdateReportContributor.cs."  
  
3.  In Solution Explorer, right-click the project node and then click **Add Reference**.  
  
4.  Select **System.ComponentModel.Composition** on the Frameworks tab.  
  
5.  Add required SQL references: right-click the project node and then click **Add Reference**. Click **Browse** and navigate to the **C:\Program Files (x86)\Microsoft SQL Server\110\DAC\Bin** folder. Choose the **Microsoft.SqlServer.Dac.dll**, **Microsoft.SqlServer.Dac.Extensions.dll**, and **Microsoft.Data.Tools.Schema.Sql.dll** entries, click **Add**, and then click **OK**.  
  
    Next, start to add code to the class.  
  
#### To define the DeploymentUpdateReportContributor class  
  
1.  In the code editor, update the DeploymentUpdateReportContributor.cs file to match the following **using** statements:  
  
    ```csharp  
    using System;  
    using System.IO;  
    using System.Text;  
    using System.Xml;  
    using Microsoft.SqlServer.Dac.Deployment;  
    using Microsoft.SqlServer.Dac.Extensibility;  
    using Microsoft.SqlServer.Dac.Model;  
  
    ```  
  
2.  Update the class definition to match the following:  
  
    ```csharp  
    /// <summary>  
        /// An executor that generates a report detailing the steps in the deployment plan. Will only run  
        /// if a "GenerateUpdateReport=true" contributor argument is set in the project file, in a targets file or  
        /// passed as an additional argument to the DacServices API. To set in a project file, add the following:  
        ///   
        /// <PropertyGroup>  
        ///     <ContributorArguments Condition="'$(Configuration)' == 'Debug'">  
        /// $(ContributorArguments);DeploymentUpdateReportContributor.GenerateUpdateReport=true;  
        ///     </ContributorArguments>  
        /// <PropertyGroup>  
        ///   
        /// </summary>  
        [ExportDeploymentPlanExecutor("MyDeploymentContributor.DeploymentUpdateReportContributor", "1.0.0.0")]  
        public class DeploymentUpdateReportContributor : DeploymentPlanExecutor  
        {  
        }  
  
    ```  
  
    Now you have defined your deployment contributor that inherits from DeploymentPlanExecutor. During the build and deployment processes, custom contributors are loaded from a standard extension directory. Deployment plan executor contributors are identified by an [ExportDeploymentPlanExecutor](/dotnet/api/microsoft.sqlserver.dac.deployment.exportdeploymentplanexecutorattribute) attribute.  
  
    This attribute is required so that contributors can be discovered. It should look similar to the following:  
  
    ```csharp  
    [ExportDeploymentPlanExecutor("MyDeploymentContributor.DeploymentUpdateReportContributor", "1.0.0.0")]  
  
    ```  
  
    In this case the first parameter to the attribute should be a unique identifier - this will be used to identify your contributor in project files. A best practice is to combine your library's namespace - in this walkthrough, MyDeploymentContributor - with the class name - in this walkthrough, DeploymentUpdateReportContributor - to produce the identifier.  
  
3.  Next, add the following member that you will use to enable this provider to accept a command-line parameter:  
  
    ```csharp  
    public const string GenerateUpdateReport = "DeploymentUpdateReportContributor.GenerateUpdateReport";  
    ```  
  
    This member enables the user to specify whether the report should be generated by using the GenerateUpdateReport option.  
  
    Next, you override the OnExecute method to add the code that you want to run when a database project is deployed.  
  
#### To override OnExecute  
  
-   Add the following method to your DeploymentUpdateReportContributor class:  
  
    ```csharp  
    /// <summary>  
            /// Override the OnExecute method to perform actions when you execute the deployment plan for  
            /// a database project.  
            /// </summary>  
            protected override void OnExecute(DeploymentPlanContributorContext context)  
            {  
                // determine whether the user specified a report is to be generated  
                bool generateReport = false;  
                string generateReportValue;  
                if (context.Arguments.TryGetValue(GenerateUpdateReport, out generateReportValue) == false)  
                {  
                    // couldn't find the GenerateUpdateReport argument, so do not generate  
                    generateReport = false;  
                }  
                else  
                {  
                    // GenerateUpdateReport argument was specified, try to parse the value  
                    if (bool.TryParse(generateReportValue, out generateReport))  
                    {  
                        // if we end up here, the value for the argument was not valid.  
                        // default is false, so do nothing.  
                    }  
                }  
  
                if (generateReport == false)  
                {  
                    // if user does not want to generate a report, we are done  
                    return;  
                }  
  
                // We will output to the same directory where the deployment script  
                // is output or to the current directory  
                string reportPrefix = context.Options.TargetDatabaseName;  
                string reportPath;  
                if (string.IsNullOrEmpty(context.DeploymentScriptPath))  
                {  
                    reportPath = Environment.CurrentDirectory;  
                }  
                else  
                {  
                    reportPath = Path.GetDirectoryName(context.DeploymentScriptPath);  
                }  
                FileInfo summaryReportFile = new FileInfo(Path.Combine(reportPath, reportPrefix + ".summary.xml"));  
                FileInfo detailsReportFile = new FileInfo(Path.Combine(reportPath, reportPrefix + ".details.xml"));  
  
                // Generate the reports by using the helper class DeploymentReportWriter  
                DeploymentReportWriter writer = new DeploymentReportWriter(context);  
                writer.WriteReport(summaryReportFile);  
                writer.IncludeScripts = true;  
                writer.WriteReport(detailsReportFile);  
  
                string msg = "Deployment reports ->"  
                    + Environment.NewLine + summaryReportFile.FullName  
                    + Environment.NewLine + detailsReportFile.FullName;  
  
                ExtensibilityError reportMsg = new ExtensibilityError(msg, Severity.Message);  
                base.PublishMessage(reportMsg);  
            }  
        /// <summary>  
        /// Override the OnExecute method to perform actions when you execute the deployment plan for  
        /// a database project.  
        /// </summary>  
            protected override void OnExecute(DeploymentPlanContributorContext context)  
            {  
                // determine whether the user specified a report is to be generated  
                bool generateReport = false;  
                string generateReportValue;  
                if (context.Arguments.TryGetValue(GenerateUpdateReport, out generateReportValue) == false)  
                {  
                    // couldn't find the GenerateUpdateReport argument, so do not generate  
                    generateReport = false;  
                }  
                else  
                {  
                    // GenerateUpdateReport argument was specified, try to parse the value  
                    if (bool.TryParse(generateReportValue, out generateReport))  
                    {  
                        // if we end up here, the value for the argument was not valid.  
                        // default is false, so do nothing.  
                    }  
                }  
  
                if (generateReport == false)  
                {  
                    // if user does not want to generate a report, we are done  
                    return;  
                }  
  
                // We will output to the same directory where the deployment script  
                // is output or to the current directory  
                string reportPrefix = context.Options.TargetDatabaseName;  
                string reportPath;  
                if (string.IsNullOrEmpty(context.DeploymentScriptPath))  
                {  
                    reportPath = Environment.CurrentDirectory;  
                }  
                else  
                {  
                    reportPath = Path.GetDirectoryName(context.DeploymentScriptPath);  
                }  
                FileInfo summaryReportFile = new FileInfo(Path.Combine(reportPath, reportPrefix + ".summary.xml"));  
                FileInfo detailsReportFile = new FileInfo(Path.Combine(reportPath, reportPrefix + ".details.xml"));  
  
                // Generate the reports by using the helper class DeploymentReportWriter  
                DeploymentReportWriter writer = new DeploymentReportWriter(context);  
                writer.WriteReport(summaryReportFile);  
                writer.IncludeScripts = true;  
                writer.WriteReport(detailsReportFile);  
  
                string msg = "Deployment reports ->"  
                    + Environment.NewLine + summaryReportFile.FullName  
                    + Environment.NewLine + detailsReportFile.FullName;  
  
                DataSchemaError reportMsg = new DataSchemaError(msg, ErrorSeverity.Message);  
                base.PublishMessage(reportMsg);  
            }  
    ```  
  
    The OnExecute method is passed a [DeploymentPlanContributorContext](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplancontributorcontext) object that provides access to any specified arguments, the source and target database model, build properties, and extension files. In this example, we get the model, and then call helper functions to output information about the model. We use the PublishMessage helper method on the base class to report any errors that occur.  
  
    Additional types and methods of interest include: [TSqlModel](/dotnet/api/microsoft.sqlserver.dac.model.tsqlmodel), [ModelComparisonResult](/dotnet/api/microsoft.sqlserver.dac.deployment.modelcomparisonresult), [DeploymentPlanHandle](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanhandle), and [SqlDeploymentOptions](/dotnet/api/microsoft.sqlserver.dac.deployment.sqldeploymentoptions).  
  
    Next, you define the helper class that digs into the details of the deployment plan.  
  
#### To add the helper class that generates the report body  
  
-   Add the helper class and its methods by adding the following code:  
  
    ```csharp  
    /// <summary>  
            /// This class is used to generate a deployment  
            /// report.   
            /// </summary>  
            private class DeploymentReportWriter  
            {  
                readonly TSqlModel _sourceModel;  
                readonly ModelComparisonResult _diff;  
                readonly DeploymentStep _planHead;  
  
                /// <summary>  
                /// The constructor accepts the same context info  
                /// that was passed to the OnExecute method of the  
                /// deployment contributor.  
                /// </summary>  
                public DeploymentReportWriter(DeploymentPlanContributorContext context)  
                {  
                    if (context == null)  
                    {  
                        throw new ArgumentNullException("context");  
                    }  
  
                    // save the source model, source/target differences,  
                    // and the beginning of the deployment plan.  
                    _sourceModel = context.Source;  
                    _diff = context.ComparisonResult;  
                    _planHead = context.PlanHandle.Head;  
                }  
                /// <summary>  
                /// Property indicating whether script bodies  
                /// should be included in the report.  
                /// </summary>  
                public bool IncludeScripts { get; set; }  
  
                /// <summary>  
                /// Drives the report generation, opening files,   
                /// writing the beginning and ending report elements,  
                /// and calling helper methods to report on the  
                /// plan operations.  
                /// </summary>  
                internal void WriteReport(FileInfo reportFile)  
                {// Assumes that we have a valid report file  
                    if (reportFile == null)  
                    {  
                        throw new ArgumentNullException("reportFile");  
                    }  
  
                    // set up the XML writer  
                    XmlWriterSettings xmlws = new XmlWriterSettings();  
                    // Indentation makes it a bit more readable  
                    xmlws.Indent = true;  
                    FileStream fs = new FileStream(reportFile.FullName, FileMode.Create, FileAccess.Write, FileShare.ReadWrite);  
                    XmlWriter xmlw = XmlWriter.Create(fs, xmlws);  
  
                    try  
                    {  
                        xmlw.WriteStartDocument(true);  
                        xmlw.WriteStartElement("DeploymentReport");  
  
                        // Summary report of the operations that  
                        // are contained in the plan.  
                        ReportPlanOperations(xmlw);  
  
                        // You could add a method call here  
                        // to produce a detailed listing of the   
                        // differences between the source and  
                        // target model.  
                        xmlw.WriteEndElement();  
                        xmlw.WriteEndDocument();  
                        xmlw.Flush();  
                        fs.Flush();  
                    }  
                    finally  
                    {  
                        xmlw.Close();  
                        fs.Dispose();  
                    }  
                }  
  
                /// <summary>  
                /// Writes details for the various operation types  
                /// that could be contained in the deployment plan.  
                /// Optionally writes script bodies, depending on  
                /// the value of the IncludeScripts property.  
                /// </summary>  
                private void ReportPlanOperations(XmlWriter xmlw)  
                {// write the node to indicate the start  
                    // of the list of operations.  
                    xmlw.WriteStartElement("Operations");  
  
                    // Loop through the steps in the plan,  
                    // starting at the beginning.  
                    DeploymentStep currentStep = _planHead;  
                    while (currentStep != null)  
                    {  
                        // Report the type of step  
                        xmlw.WriteStartElement(currentStep.GetType().Name);  
  
                        // based on the type of step, report  
                        // the relevant information.  
                        // Note that this procedure only handles   
                        // a subset of all step types.  
                        if (currentStep is SqlRenameStep)  
                        {  
                            SqlRenameStep renameStep = (SqlRenameStep)currentStep;  
                            xmlw.WriteAttributeString("OriginalName", renameStep.OldName);  
                            xmlw.WriteAttributeString("NewName", renameStep.NewName);  
                            xmlw.WriteAttributeString("Category", GetElementCategory(renameStep.RenamedElement));  
                        }  
                        else if (currentStep is SqlMoveSchemaStep)  
                        {  
                            SqlMoveSchemaStep moveStep = (SqlMoveSchemaStep)currentStep;  
                            xmlw.WriteAttributeString("OrignalName", moveStep.PreviousName);  
                            xmlw.WriteAttributeString("NewSchema", moveStep.NewSchema);  
                            xmlw.WriteAttributeString("Category", GetElementCategory(moveStep.MovedElement));  
                        }  
                        else if (currentStep is SqlTableMigrationStep)  
                        {  
                            SqlTableMigrationStep dmStep = (SqlTableMigrationStep)currentStep;  
                            xmlw.WriteAttributeString("Name", GetElementName(dmStep.SourceTable));  
                            xmlw.WriteAttributeString("Category", GetElementCategory(dmStep.SourceElement));  
                        }  
                        else if (currentStep is CreateElementStep)  
                        {  
                            CreateElementStep createStep = (CreateElementStep)currentStep;  
                            xmlw.WriteAttributeString("Name", GetElementName(createStep.SourceElement));  
                            xmlw.WriteAttributeString("Category", GetElementCategory(createStep.SourceElement));  
                        }  
                        else if (currentStep is AlterElementStep)  
                        {  
                            AlterElementStep alterStep = (AlterElementStep)currentStep;  
                            xmlw.WriteAttributeString("Name", GetElementName(alterStep.SourceElement));  
                            xmlw.WriteAttributeString("Category", GetElementCategory(alterStep.SourceElement));  
                        }  
                        else if (currentStep is DropElementStep)  
                        {  
                            DropElementStep dropStep = (DropElementStep)currentStep;  
                            xmlw.WriteAttributeString("Name", GetElementName(dropStep.TargetElement));  
                            xmlw.WriteAttributeString("Category", GetElementCategory(dropStep.TargetElement));  
                        }  
  
                        // If the script bodies are to be included,  
                        // add them to the report.  
                        if (this.IncludeScripts)  
                        {  
                            using (StringWriter sw = new StringWriter())  
                            {  
                                currentStep.GenerateBatchScript(sw);  
                                string tsqlBody = sw.ToString();  
                                if (string.IsNullOrEmpty(tsqlBody) == false)  
                                {  
                                    xmlw.WriteCData(tsqlBody);  
                                }  
                            }  
                        }  
  
                        // close off the current step  
                        xmlw.WriteEndElement();  
                        currentStep = currentStep.Next;  
                    }  
                    xmlw.WriteEndElement();  
                }  
  
                /// <summary>  
                /// Returns the category of the specified element  
                /// in the source model  
                /// </summary>  
                private string GetElementCategory(TSqlObject element)  
                {  
                    return element.ObjectType.Name;  
                }  
  
                /// <summary>  
                /// Returns the name of the specified element  
                /// in the source model  
                /// </summary>  
                private static string GetElementName(TSqlObject element)  
                {  
                    StringBuilder name = new StringBuilder();  
                    if (element.Name.HasExternalParts)  
                    {  
                        foreach (string part in element.Name.ExternalParts)  
                        {  
                            if (name.Length > 0)  
                            {  
                                name.Append('.');  
                            }  
                            name.AppendFormat("[{0}]", part);  
                        }  
                    }  
  
                    foreach (string part in element.Name.Parts)  
                    {  
                        if (name.Length > 0)  
                        {  
                            name.Append('.');  
                        }  
                        name.AppendFormat("[{0}]", part);  
                    }  
  
                    return name.ToString();  
                }  
            }        /// <summary>  
            /// This class is used to generate a deployment  
            /// report.   
            /// </summary>  
            private class DeploymentReportWriter  
            {  
                /// <summary>  
                /// The constructor accepts the same context info  
                /// that was passed to the OnExecute method of the  
                /// deployment contributor.  
                /// </summary>  
                public DeploymentReportWriter(DeploymentPlanContributorContext context)  
                {  
               }  
                /// <summary>  
                /// Property indicating whether script bodies  
                /// should be included in the report.  
                /// </summary>  
                public bool IncludeScripts { get; set; }  
  
                /// <summary>  
                /// Drives the report generation, opening files,   
                /// writing the beginning and ending report elements,  
                /// and calling helper methods to report on the  
                /// plan operations.  
                /// </summary>  
                internal void WriteReport(FileInfo reportFile)  
                {  
                }  
  
                /// <summary>  
                /// Writes details for the various operation types  
                /// that could be contained in the deployment plan.  
                /// Optionally writes script bodies, depending on  
                /// the value of the IncludeScripts property.  
                /// </summary>  
                private void ReportPlanOperations(XmlWriter xmlw)  
                {  
                }  
  
                /// <summary>  
                /// Returns the category of the specified element  
                /// in the source model  
                /// </summary>  
                private string GetElementCategory(IModelElement element)  
                {  
                }  
  
                /// <summary>  
                /// Returns the name of the specified element  
                /// in the source model  
                /// </summary>  
                private string GetElementName(IModelElement element)  
                {  
                }  
            }  
    ```  
  
-   Save the changes to the class file. A number of useful types are referenced in the helper class:  
  
    |**Code Area**|**Useful Types**|  
    |-----------------|--------------------|  
    |Class members|[TSqlModel](/dotnet/api/microsoft.sqlserver.dac.model.tsqlmodel), [ModelComparisonResult](/dotnet/api/microsoft.sqlserver.dac.deployment.modelcomparisonresult), [DeploymentStep](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentstep)|  
    |WriteReport method|XmlWriter and XmlWriterSettings|  
    |ReportPlanOperations method|Types of interest include the following: [DeploymentStep](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentstep), [SqlRenameStep](/dotnet/api/microsoft.sqlserver.dac.deployment.sqlrenamestep), [SqlMoveSchemaStep](/dotnet/api/microsoft.sqlserver.dac.deployment.sqlmoveschemastep), [SqlTableMigrationStep](/dotnet/api/microsoft.sqlserver.dac.deployment.sqltablemigrationstep), [CreateElementStep](/dotnet/api/microsoft.sqlserver.dac.deployment.createelementstep), [AlterElementStep](/dotnet/api/microsoft.sqlserver.dac.deployment.alterelementstep), [DropElementStep](/dotnet/api/microsoft.sqlserver.dac.deployment.dropelementstep).<br /><br />There are a number of other steps - see the API documentation for a full list of steps.|  
    |GetElementCategory|[TSqlObject](/dotnet/api/microsoft.sqlserver.dac.model.tsqlobject)|  
    |GetElementName|[TSqlObject](/dotnet/api/microsoft.sqlserver.dac.model.tsqlobject)|  
  
    Next, you build the class library.  
  
#### To sign and build the assembly  
  
1.  On the **Project** menu, click **MyDeploymentContributor Properties**.  
  
2.  Click the **Signing** tab.  
  
3.  Click **Sign the assembly**.  
  
4.  In **Choose a strong name key file**, click **\<New\>**.  
  
5.  In the **Create Strong Name Key** dialog box, in **Key file name**, type **MyRefKey**.  
  
6.  (optional) You can specify a password for your strong name key file.  
  
7.  Click **OK**.  
  
8.  On the **File** menu, click **Save All**.  
  
9. On the **Build** menu, click **Build Solution**.  
  
Next, you must install the assembly so that it will be loaded when you build and deploy SQL projects.  
  
## <a name="InstallDeploymentContributor"></a>Install a Deployment Contributor  
To install a deployment contributor, you must copy the assembly and associated .pdb file to the Extensions folder.  
  
#### To install the MyDeploymentContributor assembly  
  
-   Next, you will copy the assembly information to the Extensions directory. When Visual Studio starts, it will identify any extensions in the %Program Files%\Microsoft SQL Server\110\DAC\Bin\Extensions directory and subdirectories, and make them available for use:  
  
-   Copy the **MyDeploymentContributor.dll** assembly file from the output directory to the %Program Files%\Microsoft SQL Server\110\DAC\Bin\Extensions directory. By default, the path of your compiled .dll file is YourSolutionPath\YourProjectPath\bin\Debug or YourSolutionPath\YourProjectPath\bin\Release.  
  
## <a name="TestDeploymentContributor"></a>Test your Deployment Contributor  
To test your deployment contributor, you must perform the following tasks:  
  
-   Add properties to the .sqlproj file that you plan to deploy.  
  
-   Deploy the project by using MSBuild and providing the appropriate parameter.  
  
### Add Properties to the SQL Project (.sqlproj) File  
You must always update the SQL project file to specify the ID of the contributors you wish to run. In addition because this contributor expects a "GenerateUpdateReport" argument this must be specified as a contributor argument.  
  
You can do this in one of two ways. You can manually modify the .sqlproj file to add the required arguments. You might choose to do this if your contributor does not have any contributor arguments required for configuration, or if you do not intend to reuse the contributor across a large number of projects. If you choose this option, add the following statements to the .sqlproj file after the first Import node in the file:  
  
```  
<PropertyGroup>  
    <DeploymentContributors>$(DeploymentContributors); MyDeploymentContributor.DeploymentUpdateReportContributor</DeploymentContributors>  
<ContributorArguments Condition="'$(Configuration)' == 'Debug'">$(ContributorArguments);DeploymentUpdateReportContributor.GenerateUpdateReport=true;</ContributorArguments>  
  </PropertyGroup>  
```  
  
The second method is to create a targets file containing the required contributor arguments. This is useful if you are using the same contributor for multiple projects and have contributor arguments required, since it will include the default values. In this case, create a targets file in the MSBuild extensions path.  
  
1.  Navigate to %Program Files%\MSBuild.  
  
2.  Create a new folder "MyContributors" where your targets files will be stored.  
  
3.  Create a new file "MyContributors.targets" inside this directory, add the following text to it and save the file:  
  
    ```  
    <?xml version="1.0" encoding="utf-8"?>  
  
    <Project xmlns="https://schemas.microsoft.com/developer/msbuild/2003">  
      <PropertyGroup>  
    <DeploymentContributors>$(DeploymentContributors);MyDeploymentContributor.DeploymentUpdateReportContributor</DeploymentContributors>  
    <ContributorArguments Condition="'$(Configuration)' == 'Debug'">$(ContributorArguments); DeploymentUpdateReportContributor.GenerateUpdateReport=true;</ContributorArguments>  
      </PropertyGroup>  
    </Project>  
    ```  
  
4.  Inside the .sqlproj file for any project you want to run contributors, import the targets file by adding the following statement to the .sqlproj file after the \<Import Project="$(MSBuildExtensionsPath)\Microsoft\VisualStudio\v$(VisualStudioVersion)\SSDT\Microsoft.Data.Tools.Schema.SqlTasks.targets" \/> node in the file :  
  
    ```  
    <Import Project="$(MSBuildExtensionsPath)\MyContributors\MyContributors.targets " />  
    ```  
  
After you have followed one of these approaches, you can use MSBuild to pass in the parameters for command-line builds.  
  
> [!NOTE]  
> You must always update the "DeploymentContributors" property to specify your contributor ID. This is the same ID used in the "ExportDeploymentPlanExecutor" attribute in your contributor source file. Without this your contributor will not be run when building the project. The "ContributorArguments" property needs to be updated only if you have arguments required for your contributor to run.  
  
### Deploy the Database Project  
Your project can be published or deployed as normal inside Visual Studio. Simply open a solution containing your SQL project and choose the "Publish..." option from the right-click context menu for the project, or use F5 for a debug deployment to LocalDB. In this example we will use the "Publish..." dialog to generate a deployment script.  
  
##### To deploy your SQL project and generate a deployment report  
  
1.  Open Visual Studio and open the solution containing your SQL Project.  
  
2.  Select your project and hit "F5" to do a debug deployment. Note: since the ContributorArguments element is set to only be included if the configuration is "Debug", for now the deployment report is only generated for debug deployments. To change this, remove the Condition="'$(Configuration)' == 'Debug'" statement from the ContributorArguments definition.  
  
3.  Output such as the following should be present in the output window:  
  
    ```  
    ------ Deploy started: Project: Database1, Configuration: Debug Any CPU ------  
    Finished verifying cached model in 00:00:00  
    Deployment reports ->  
  
      C:\Users\UserName\Documents\Visual Studio 2012\Projects\MyDatabaseProject\MyDatabaseProject\sql\debug\MyTargetDatabase.summary.xml  
      C:\Users\UserName\Documents\Visual Studio 2012\Projects\MyDatabaseProject\MyDatabaseProject\sql\debug\MyTargetDatabase.details.xml  
  
      Deployment script generated to:  
      C:\Users\UserName\Documents\Visual Studio 2012\Projects\MyDatabaseProject\MyDatabaseProject\sql\debug\MyDatabaseProject.sql  
  
    ```  
  
4.  Open MyTargetDatabase.summary.xml and examine the contents. The file resembles the following example that shows a new database deployment:  
  
    ```  
    <?xml version="1.0" encoding="utf-8" standalone="yes"?>  
    <DeploymentReport>  
      <Operations>  
        <DeploymentScriptStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptStep />  
        <DeploymentScriptStep />  
        <DeploymentScriptStep />  
        <DeploymentScriptStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptStep />  
        <DeploymentScriptDomStep />  
        <BeginPreDeploymentScriptStep />  
        <DeploymentScriptStep />  
        <EndPreDeploymentScriptStep />  
        <SqlBeginPreservationStep />  
        <SqlEndPreservationStep />  
        <SqlBeginDropsStep />  
        <SqlEndDropsStep />  
        <SqlBeginAltersStep />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales" Category="Schema" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.Customer" Category="Table" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.PK_Customer_CustID" Category="Primary Key" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.Orders" Category="Table" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.PK_Orders_OrderID" Category="Primary Key" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.Def_Customer_YTDOrders" Category="Default Constraint" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.Def_Customer_YTDSales" Category="Default Constraint" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.Def_Orders_OrderDate" Category="Default Constraint" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.Def_Orders_Status" Category="Default Constraint" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.FK_Orders_Customer_CustID" Category="Foreign Key" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.CK_Orders_FilledDate" Category="Check Constraint" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.CK_Orders_OrderDate" Category="Check Constraint" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.uspCancelOrder" Category="Procedure" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.uspFillOrder" Category="Procedure" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.uspNewCustomer" Category="Procedure" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.uspPlaceNewOrder" Category="Procedure" />  
        <SqlPrintStep />  
        <CreateElementStep Name="Sales.uspShowOrderDetails" Category="Procedure" />  
        <SqlEndAltersStep />  
        <DeploymentScriptStep />  
        <BeginPostDeploymentScriptStep />  
        <DeploymentScriptStep />  
        <EndPostDeploymentScriptStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptDomStep />  
        <DeploymentScriptDomStep />  
      </Operations>  
    </DeploymentReport>  
  
    ```  
  
    > [!NOTE]  
    > If you deploy a database project that is identical to the target database, the resulting report will not be very meaningful. For more meaningful results, either deploy changes to a database or deploy a new database.  
  
5.  Open MyTargetDatabase.details.xml and examine the contents. A small section of the details file shows the entries and script that create the Sales schema, that print a message about creating a table, and that create the table:  
  
    ```  
    <CreateElementStep Name="Sales" Category="Schema"><![CDATA[CREATE SCHEMA [Sales]  
        AUTHORIZATION [dbo];  
  
    ]]></CreateElementStep>  
        <SqlPrintStep><![CDATA[PRINT N'Creating [Sales].[Customer]...';  
  
    ]]></SqlPrintStep>  
        <CreateElementStep Name="Sales.Customer" Category="Table"><![CDATA[CREATE TABLE [Sales].[Customer] (  
        [CustomerID]   INT           IDENTITY (1, 1) NOT NULL,  
        [CustomerName] NVARCHAR (40) NOT NULL,  
        [YTDOrders]    INT           NOT NULL,  
        [YTDSales]     INT           NOT NULL  
    );  
  
    ]]></CreateElementStep>  
  
    ```  
  
    By analyzing the deployment plan as it is executed, you can report on any information that is contained in the deployment and can take additional actions based on the steps in that plan.  
  
## Next Steps  
You could create additional tools to perform processing of the output XML files. This is just one example of a [DeploymentPlanExecutor](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanexecutor). You could also create a [DeploymentPlanModifier](/dotnet/api/microsoft.sqlserver.dac.deployment.deploymentplanmodifier) to change a deployment plan before it is executed.  
  
## See Also  
[Walkthrough: Extend Database Project Build to Generate Model Statistics](/previous-versions/visualstudio/visual-studio-2010/ee461508(v=vs.100))  
[Walkthrough: Extend Database Project Deployment to Modify the Deployment Plan](/previous-versions/visualstudio/visual-studio-2010/ee461507(v=vs.100))  
[Customize Database Build and Deployment by Using Build and Deployment Contributors](/previous-versions/visualstudio/visual-studio-2010/ee461505(v=vs.100))  
