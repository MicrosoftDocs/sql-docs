---
title: "How to: Run SQL Server Unit Tests from Team Foundation Build | Microsoft Docs"
ms.custom: 
  - "SSDT"
ms.date: "02/09/2017"
ms.prod: "sql"
ms.technology: ssdt
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 24f5b85d-d6f9-415f-b09f-933b78dc0b67
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
---
# How to: Run SQL Server Unit Tests from Team Foundation Build
You can use Team Foundation Build to run your SQL Server unit tests as part of a build verification test (BVT). You can configure your unit tests to deploy the database, generate test data, and then run selected tests. If you are not familiar with Team Foundation Build, you should review the following information before you follow the procedures in this topic:  
  
-   [Creating and Defining SQL Server Unit Tests](../ssdt/creating-and-defining-sql-server-unit-tests.md)  
  
-   [How to: Configure and Run Scheduled Tests After Building Your Application](https://msdn.microsoft.com/library/ms182465(VS.100).aspx)  
  
-   [Create a Basic Build Definition](https://msdn.microsoft.com/library/ms181716(VS.100).aspx)  
  
Before you use these procedures, you must first configure your working environment by performing the following tasks:  
  
-   Install Team Foundation Build and Team Foundation version control. You probably have to install Team Foundation Build and Team Foundation version control on different computers.  
  
-   Install MicrosoftSQL Server Data Tools Build Utilities on the same computer as Team Foundation Build. To install the SQL Server Data Tools Build Utilities, first perform an administrative installation point. For more information about an administrative installation point, see [Install SQL Server Data Tools](../ssdt/install-sql-server-data-tools.md). Then install SSDTBuildUtilties.msi onto the build server from the location (/location) used for the administrative installation point.  
  
-   Connect to an instance of Visual Studio Team Foundation Server.  
  
After you configure your working environment, you must then follow these steps:  
  
1.  Create a database project.  
  
2.  Import or create the schema and objects for the database project.  
  
3.  Configure the database project properties for build and deployment.  
  
4.  Create one or more unit tests.  
  
5.  Add the solution that contains the database project and the unit test project to version control, and check in all files.  
  
The procedures in this topic describe how to create a build definition to run your unit tests as part of an automated test run:  
  
1.  [Configure Test Settings to Run Database Unit Tests on an x64 Build Agent](#ConfigureX64)  
  
2.  [Assign Tests to a Test Category (Optional)](#CreateATestList)  
  
3.  [Modify the Test Project](#ModifyTestProject)  
  
4.  [Check in the Solution](#CheckInTheTestList)  
  
5.  [Create a Build Definition](#CreateBuildDef)  
  
6.  [Run the New Build Definition](#RunBuild)  
  
**Running SQL Server Unit Tests on a Build Computer**  
  
When you run unit tests on a build computer, the unit tests might be unable to find the database project files (.sqlproj). This problem occurs because the app.config file references those files by using relative paths. In addition, your unit tests might fail if they cannot find the instance of SQL Server that you want to use to run the unit tests. This problem can occur if the connection strings that are stored in the app.config file are not valid from the build computer.  
  
To resolve these issues, you must specify an override section in your app.config that overrides app.config with a configuration file that is specific to your Team Foundation Build environment. For more information, see [Modify the Test Project](#ModifyTestProject), later in this topic.  
  
## <a name="ConfigureX64"></a>Configure Test Settings to Run SQL Server Unit Tests on an x64 Build Agent  
Before you can run unit tests on an x64 build agent, you must configure your test settings to change the Host Process Platform.  
  
#### To specify the Host Process Platform  
  
1.  Open the solution that contains the test project for which you want to configure settings.  
  
2.  In **Solution Explorer**, in the **Solution Items** folder, double-click the **Local.testsettings** file.  
  
    The **Test Settings** dialog box appears.  
  
3.  In the list, click **Hosts**.  
  
4.  In the details pane, in **Host Process Platform**, click **MSIL** to configure your tests to run on an x64 build agent.  
  
5.  Click **Apply**.  
  
## <a name="CreateATestList"></a>Assign Tests to a Test Category (Optional)  
Typically, when you create a build definition to run unit tests, you specify one or more test categories. All tests in the specified categories are run when the build is run.  
  
#### To assign tests to a test category  
  
1.  Open the **Test View** window.  
  
2.  Select a test.  
  
3.  In the properties pane, click **Test Categories** and then click the ellipsis (...) in the right-most column.  
  
4.  In the **Test Category** window, in the **Add New Category** box, type a name for your new test category.  
  
5.  Click **Add** and then click **OK**.  
  
    The new test category will be assigned to your test and it will be available to the other tests through their properties.  
  
## <a name="ModifyTestProject"></a>Modify the Test Project  
By default, Team Foundation Build creates a configuration file from the project's app.config file when it builds the unit tests project. The path to the database project is stored as relative path in the app.config file. The relative paths that work in Visual Studio will not work because Team Foundation Build puts the built files in different locations relative to where you run the unit tests. In addition, your app.config file contains the connection strings that specify the database that you want to test. You also need a separate app.config file for Team Foundation Build if the unit tests must connect to a different database than the one that was used when the test project was created. By making the modifications in the next procedure, you can set up your test project and build server so that Team Foundation Build will use a different configuration.  
  
> [!IMPORTANT]  
> You must perform this procedure for each test project (.vbproj or .vsproj).  
  
#### To specify an app.config file for Team Foundation Build  
  
1.  In **Solution Explorer**, right-click the app.config file, and click **Copy**.  
  
2.  Right-click the test project and click **Paste**.  
  
3.  Right-click the file called **Copy of app.config**, and click Rename.  
  
4.  Type _BuildComputer_**.sqlunitttest.config** and press ENTER, where *BuildComputer* is the name of the computer on which your build agent runs.  
  
5.  Double-click *BuildComputer*.sqlunitttest.config.  
  
    The configuration file opens in the editor.  
  
6.  Change the relative path to the .sqlproj file by adding a folder level for the Sources folder and a subfolder with the same name as the solution. For example, if the configuration file initially contains the following entry:  
  
    ```  
    <DatabaseDeployment DatabaseProjectFileName="..\..\..\Database3\Database3.sqlproj"      Configuration="Debug" />  
    ```  
  
    Update the file as follows:  
  
    ```  
    <DatabaseDeployment DatabaseProjectFileName="..\..\..\Database3\Database3.sqlproj"      Configuration="Debug" />  
    ```  
  
    When you are finished, your *BuildComputer*.sqlunitttest.config file should resemble the following example for Visual Studio 2010:  
  
    ```  
    <SqlUnitTesting_VS2010>  
        <DatabaseDeployment DatabaseProjectFileName="..\..\..\Database4\Database4.sqlproj"  
            Configuration="Debug" />  
        <DataGeneration ClearDatabase="true" />  
        <ExecutionContext Provider="System.Data.SqlClient" ConnectionString="Data Source=(localdb)\Projects;Initial Catalog=Database4;Integrated Security=True;Pooling=False"  
            CommandTimeout="30" />  
        <PrivilegedContext Provider="System.Data.SqlClient" ConnectionString="Data Source=(localdb)\Projects;Initial Catalog=Database4;Integrated Security=True;Pooling=False"  
            CommandTimeout="30" />  
    </SqlUnitTesting_VS2010>  
    ```  
  
    Or, if you are using Visual Studio 2012:  
  
    ```  
    <SqlUnitTesting_VS2012>  
            <DatabaseDeployment DatabaseProjectFileName="..\..\..\Database4\Database4.sqlproj"  
                Configuration="Debug" />  
            <DataGeneration ClearDatabase="true" />  
            <ExecutionContext Provider="System.Data.SqlClient" ConnectionString="Data Source=(localdb)\Projects;Initial Catalog=Database4;Integrated Security=True;Pooling=False"  
                CommandTimeout="30" />  
            <PrivilegedContext Provider="System.Data.SqlClient" ConnectionString="Data Source=(localdb)\Projects;Initial Catalog=Database4;Integrated Security=True;Pooling=False"  
                CommandTimeout="30" />  
        </SqlUnitTesting_VS2012>  
    ```  
  
7.  Update the ConnectionString attribute for ExecutionContext and PrivilegedContext to specify connections to the target database to which you want to deploy.  
  
8.  On the **File** menu, click **Save All**.  
  
9. In Solution Explorer, double-click app.config.  
  
10. In the editor, for each \<SqlUnitTesting_*VSVersion*> node, add `AllowConfigurationOverride="true"`. For example:  
  
    ```  
    -- Update SqlUnitTesting_VS2010 node to:  
    <SqlUnitTesting_VS2010 AllowConfigurationOverride="true">   
  
    -- Update SqlUnitTesting_VS2012 node to:  
    <SqlUnitTesting_VS2012 AllowConfigurationOverride="true">  
    ```  
  
    By making this change, you allow Team Foundation Build to use the replacement configuration file that you created.  
  
11. On the **File** menu, click **Save All**.  
  
    Next, you must update Local.testsettings to include your customized configuration file.  
  
#### To customize Local.testsettings to deploy the customized configuration file  
  
1.  In Solution Explorer, double-click Local.testsettings.  
  
    The **Test Settings** dialog box appears.  
  
2.  In the list of categories, click **Deployment**.  
  
3.  Select the **Enable deployment** check box.  
  
4.  Click **Add File**.  
  
5.  In the **Add Deployment Files** dialog box, specify the *BuildComputer*.sqlunitttest.config file that you created.  
  
6.  Click **Apply**.  
  
7.  Click **Close**.  
  
8.  On the **File** menu, click **Save All**.  
  
    Next, you check in your solution to version control.  
  
## <a name="CheckInTheTestList"></a>Check in the Solution  
In this procedure, you check in all the files of your solution. These files include the test metadata file of your solution, which contains your test category associations and tests. Whenever you add, delete, reorganize, or change the contents of tests, your test metadata file is automatically updated to reflect these changes.  
  
> [!NOTE]  
> This procedure describes the steps if you are using Team Foundation version control. If you are using different version control software, you must follow the steps that are appropriate for your software.  
  
#### To check in the solution  
  
1.  Connect to a computer that is running Team Foundation Server.  
  
    For more information, see [Using Source Control Explorer](https://msdn.microsoft.com/library/ms181370(VS.100).aspx).  
  
2.  If your solution is not already in source control, add it to source control.  
  
    For more information, see [Add a Project or Solution to Version Control](https://msdn.microsoft.com/library/ms181374(VS.100).aspx).  
  
3.  Click **View**, and then click **Pending Checkins**.  
  
4.  Check in all the files of your solution.  
  
    For more information, see [Check In Pending Changes](https://msdn.microsoft.com/library/ms181411(VS.100).aspx).  
  
    > [!NOTE]  
    > You might have a specific team process that governs how automated tests are created and managed. For example, the process might require that you verify your build locally before you check in that code together with the tests that will be run on it.  
  
    In **Solution Explorer**, a padlock icon appears next to each file to indicate that it is checked in. For more information, see [View Version Control File and Folder Properties](https://msdn.microsoft.com/library/ms245468(VS.100).aspx).  
  
    Your tests are available to Team Foundation Build. You can now create a build definition that contains the tests that you want to run.  
  
## <a name="CreateBuildDef"></a>Create a Build Definition  
  
#### To create a build definition  
  
1.  In Team Explorer, click your team project, right-click the **Builds** node, and click **New Build Definition**.  
  
    The **New Build Definition** window appears.  
  
2.  In **Build definition name**, type the name that you want to use for the build definition.  
  
3.  In the navigation bar, click **Build Defaults**.  
  
4.  In **Copy build output to the following drop folder (UNC path, such as \\\server\share)**, specify a folder to contain the build output.  
  
    You can specify a shared folder on your local computer or any network location to which the build process will have permissions.  
  
5.  In the navigation bar, click **Process**.  
  
6.  In the **Required** group, in **Items to Build**, click the browse (...) button.  
  
7.  In the **Build Project List Editor** dialog box, click **Add**.  
  
8.  Specify the solution file (.sln) that you added to version control earlier in this walkthrough, and click **OK**.  
  
    The solution appears in the **Project or solution files to build** list.  
  
9. Click **OK**.  
  
10. In the **Basic** group, in **Automated Tests**, specify the tests that you want to run. By default, the tests that are contained in files named \*test\*.dll from your solution will be run.  
  
11. On the **File** menu, click **Save** *ProjectName*.  
  
    You have created a build definition. Next you modify the test project.  
  
## <a name="RunBuild"></a>Run the New Build Definition  
  
#### To run the new build type  
  
1.  In Team Explorer, expand the team project node, expand the Builds node, right-click the build definition that you want to run, and then click Queue New Build.  
  
    The **Queue Build {**_TeamProjectName_**}** dialog box appears with a list of all existing build types.  
  
2.  If necessary, in **Build definition**, click your new build definition.  
  
3.  Confirm that the values in the **Build definition**, **Build agent**, and **Drop folder for this Build** fields are all appropriate, and then click **Queue**.  
  
    The **Queued** tab of **Build Explorer** appears. For more information, see [Manage and View Completed Builds (Visual Studio 2010)](https://msdn.microsoft.com/library/ms181730(VS.100).aspx) or [Manage Your Builds in Build Explorer (Visual Studio 2012)](https://msdn.microsoft.com/library/ms181732.aspx).  
  
## See Also  
[Running SQL Server Unit Tests](../ssdt/running-sql-server-unit-tests.md)  
[Create a Basic Build Definition](https://msdn.microsoft.com/library/ms181716(VS.100).aspx)  
[Queue a Build](https://msdn.microsoft.com/library/ms181722(VS.100).aspx)  
[Monitor Progress of a Running Build](https://msdn.microsoft.com/library/ms181724(VS.100).aspx)  
  
