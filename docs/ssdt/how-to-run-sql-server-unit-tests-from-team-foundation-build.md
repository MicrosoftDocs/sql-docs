---
title: Run SQL Server Unit Tests from Team Foundation Build
description: Learn how to run SQL Server unit tests from Team Foundation Build. See how to create a build definition and run unit tests in an automated test run.
author: cwebb-msft
ms.author: chweb
ms.reviewer: vanto
ms.date: 09/09/2025
ms.service: sql
ms.subservice: ssdt
ms.topic: how-to
ms.custom:
  - sfi-ropc-nochange
---

# How to: Run SQL Server unit tests from Team Foundation Build

You can use Team Foundation Build to run your SQL Server unit tests as part of a build verification test (BVT). You can configure your unit tests to deploy the database, generate test data, and then run selected tests. If you aren't familiar with Team Foundation Build, you should review the following information before you follow the procedures in this article:

- [Create and define SQL Server unit tests](creating-and-defining-sql-server-unit-tests.md)
- [How to: Configure and Run Scheduled Tests After Building Your Application](/previous-versions/visualstudio/visual-studio-2010/ms182465(v=vs.100))
- [Create a Basic Build Definition](/previous-versions/visualstudio/visual-studio-2010/ms181716(v=vs.100))

Before you use these procedures, you must first configure your working environment by performing the following tasks:

- Install Team Foundation Build and Team Foundation version control. You probably have to install Team Foundation Build and Team Foundation version control on different computers.

- Install Microsoft SQL Server Data Tools Build Utilities on the same computer as Team Foundation Build. To install the SQL Server Data Tools Build Utilities, first perform an administrative installation point. For more information about an administrative installation point, see [Install SQL Server Data Tools (SSDT) for Visual Studio](download-sql-server-data-tools-ssdt.md). Then install SSDTBuildUtilities.msi onto the build server from the location (/location) used for the administrative installation point.

- Connect to an instance of Azure DevOps Server.

After you configure your working environment, you must then follow these steps:

1. Create a database project.
1. Import or create the schema and objects for the database project.
1. Configure the database project properties for build and deployment.
1. Create one or more unit tests.
1. Add the solution that contains the database project and the unit test project to version control, and check in all files.

The procedures in this article describe how to create a build definition to run your unit tests as part of an automated test run:

1. [Configure Test Settings to Run Database Unit Tests on an x64 Build Agent](#ConfigureX64)
1. [Assign Tests to a Test Category (Optional)](#CreateATestList)
1. [Modify the Test Project](#ModifyTestProject)
1. [Check in the Solution](#CheckInTheTestList)
1. [Create a Build Definition](#CreateBuildDef)
1. [Run the New Build Definition](#RunBuild)

## Running SQL Server unit tests on a build computer

When you run unit tests on a build computer, the unit tests might be unable to find the database project files (`.sqlproj`). This problem occurs because the app.config file references those files by using relative paths. In addition, your unit tests might fail if they can't find the instance of SQL Server that you want to use to run the unit tests. This problem can occur if the connection strings that are stored in the app.config file aren't valid from the build computer.

To resolve these issues, you must specify an override section in your app.config that overrides app.config with a configuration file that is specific to your Team Foundation Build environment. For more information, see [Modify the Test Project](#ModifyTestProject), later in this article.

<a id="ConfigureX64"></a>

## Configure test settings to run SQL Server unit tests on an x64 build agent

Before you can run unit tests on an x64 build agent, you must configure your test settings to change the Host Process Platform.

### Specify the host process platform

1. Open the solution that contains the test project for which you want to configure settings.

1. In **Solution Explorer**, in the **Solution Items** folder, double-click the **Local.testsettings** file.

   The **Test Settings** dialog box appears.

1. In the list, select **Hosts**.

1. In the details pane, in **Host Process Platform**, select **MSIL** to configure your tests to run on an x64 build agent.

1. Select **Apply**.

<a id="CreateATestList"></a>

## Assign tests to a test category (Optional)

Typically, when you create a build definition to run unit tests, you specify one or more test categories. All tests in the specified categories are run when the build is run.

### Assign tests to a test category

1. Open the **Test View** window.

1. Select a test.

1. In the properties pane, select **Test Categories** and then select the ellipsis (...) in the right-most column.

1. In the **Test Category** window, in the **Add New Category** box, type a name for your new test category.

1. Select **Add** and then select **OK**.

   The new test category is assigned to your test and it's available to the other tests through their properties.

<a id="ModifyTestProject"></a>

## Modify the test project

By default, Team Foundation Build creates a configuration file from the project's app.config file when it builds the unit tests project. The path to the database project is stored as relative path in the app.config file. The relative paths that work in Visual Studio don't work because Team Foundation Build puts the built files in different locations relative to where you run the unit tests. In addition, your app.config file contains the connection strings that specify the database that you want to test. You also need a separate app.config file for Team Foundation Build if the unit tests must connect to a different database than the one that was used when the test project was created. By making the modifications in the next procedure, you can set up your test project and build server so that Team Foundation Build uses a different configuration.

> [!IMPORTANT]  
> You must perform this procedure for each test project (`.vbproj` or `.vsproj`).

### Specify an app.config file for Team Foundation Build

1. In **Solution Explorer**, right-click the app.config file, and select **Copy**.

1. Right-click the test project and select **Paste**.

1. Right-click the file called **Copy of app.config**, and select Rename.

1. Type _BuildComputer_**.sqlunitttest.config** and press ENTER, where *BuildComputer* is the name of the computer on which your build agent runs.

1. Double-click *BuildComputer*.sqlunitttest.config.

   The configuration file opens in the editor.

1. Change the relative path to the `.sqlproj` file by adding a folder level for the Sources folder and a subfolder with the same name as the solution. For example, if the configuration file initially contains the following entry:

   ```xml
   <DatabaseDeployment DatabaseProjectFileName="..\..\..\Database3\Database3.sqlproj"      Configuration="Debug" />
   ```

   Update the file as follows:

   ```xml
   <DatabaseDeployment DatabaseProjectFileName="..\..\..\Database3\Database3.sqlproj"      Configuration="Debug" />
   ```

   When you're finished, your *BuildComputer*.sqlunitttest.config file should resemble the following example for Visual Studio 2010:

   ```xml
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

   Or, if you're using Visual Studio 2012:

   ```xml
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

1. Update the ConnectionString attribute for ExecutionContext and PrivilegedContext to specify connections to the target database to which you want to deploy.

1. On the **File** menu, select **Save All**.

1. In Solution Explorer, double-click app.config.

1. In the editor, for each \<SqlUnitTesting_*VSVersion*> node, add `AllowConfigurationOverride="true"`. For example:

   ```xml
   -- Update SqlUnitTesting_VS2010 node to:
   <SqlUnitTesting_VS2010 AllowConfigurationOverride="true">

   -- Update SqlUnitTesting_VS2012 node to:
   <SqlUnitTesting_VS2012 AllowConfigurationOverride="true">
   ```

   By making this change, you allow Team Foundation Build to use the replacement configuration file that you created.

1. On the **File** menu, select **Save All**.

   Next, you must update `Local.testsettings` to include your customized configuration file.

### Customize Local.testsettings to deploy the customized configuration file

1. In Solution Explorer, double-click `Local.testsettings`.

   The **Test Settings** dialog box appears.

1. In the list of categories, select **Deployment**.

1. Select the **Enable deployment** check box.

1. Select **Add File**.

1. In the **Add Deployment Files** dialog box, specify the *BuildComputer*.sqlunitttest.config file that you created.

1. Select **Apply**.

1. Select **Close**.

1. On the **File** menu, select **Save All**.

   Next, you check in your solution to version control.

<a id="CheckInTheTestList"></a>

## Check in the solution

In this procedure, you check in all the files of your solution. These files include the test metadata file of your solution, which contains your test category associations and tests. Whenever you add, delete, reorganize, or change the contents of tests, your test metadata file is automatically updated to reflect these changes.

> [!NOTE]  
> This procedure describes the steps if you're using Team Foundation version control. If you're using different version control software, you must follow the steps that are appropriate for your software.

### Check in solution

1. Connect to a computer that is running Azure DevOps Server.

   For more information, see [Using Source Control Explorer](/previous-versions/visualstudio/visual-studio-2010/ms181370(v=vs.100)).

1. If your solution isn't already in source control, add it to source control.

   For more information, see [Add a Project or Solution to Version Control](/previous-versions/visualstudio/visual-studio-2010/ms181374(v=vs.100)).

1. Select **View**, and then select **Pending Checkins**.

1. Check in all the files of your solution.

   For more information, see [Check In Pending Changes](/previous-versions/visualstudio/visual-studio-2010/ms181411(v=vs.100)).

   > [!NOTE]  
   > You might have a specific team process that governs how automated tests are created and managed. For example, the process might require that you verify your build locally before you check in that code together with the tests that are run on it.

   In **Solution Explorer**, a padlock icon appears next to each file to indicate that it's checked in. For more information, see [View Version Control File and Folder Properties](/previous-versions/visualstudio/visual-studio-2010/ms245468(v=vs.100)).

   Your tests are available to Team Foundation Build. You can now create a build definition that contains the tests that you want to run.

<a id="CreateBuildDef"></a>

## Create a build definition

1. In Team Explorer, select your team project, right-click the **Builds** node, and select **New Build Definition**.

   The **New Build Definition** window appears.

1. In **Build definition name**, type the name that you want to use for the build definition.

1. In the navigation bar, select **Build Defaults**.

1. In **Copy build output to the following drop folder (UNC path, such as \\\server\share)**, specify a folder to contain the build output.

   You can specify a shared folder on your local computer or any network location to which the build process has permissions.

1. In the navigation bar, select **Process**.

1. In the **Required** group, in **Items to Build**, select the browse (...) button.

1. In the **Build Project List Editor** dialog box, select **Add**.

1. Specify the solution file (`.sln`) that you added to version control earlier in this walkthrough, and select **OK**.

   The solution appears in the **Project or solution files to build** list.

1. Select **OK**.

1. In the **Basic** group, in **Automated Tests**, specify the tests that you want to run. By default, the tests that are contained in files named \*test\*.dll from your solution are run.

1. On the **File** menu, select **Save** *ProjectName*.

   You have created a build definition. Next you modify the test project.

<a id="RunBuild"></a>

## Run the new build definition

1. In Team Explorer, expand the team project node, expand the Builds node, right-click the build definition that you want to run, and then select Queue New Build.

   The **Queue Build {**_TeamProjectName_**}** dialog box appears with a list of all existing build types.

1. If necessary, in **Build definition**, select your new build definition.

1. Confirm that the values in the **Build definition**, **Build agent**, and **Drop folder for this Build** fields are all appropriate, and then select **Queue**.

   The **Queued** tab of **Build Explorer** appears. For more information, see [Manage and View Completed Builds (Visual Studio 2010)](/previous-versions/visualstudio/visual-studio-2010/ms181730(v=vs.100)) or [Manage Your Builds in Build Explorer (Visual Studio 2012)](/previous-versions/ms181732(v=vs.140)).

## Related content

- [Run SQL Server unit tests](running-sql-server-unit-tests.md)
- [Create a Basic Build Definition](/previous-versions/visualstudio/visual-studio-2010/ms181716(v=vs.100))
- [Queue a Build](/previous-versions/visualstudio/visual-studio-2010/ms181722(v=vs.100))
- [Monitor Progress of a Running Build](/previous-versions/visualstudio/visual-studio-2010/ms181724(v=vs.100))
