---
title: "Deploy Integration Services (SSIS) Projects and Packages | Microsoft Docs"
ms.custom: ""
ms.date: 06/04/2018
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.ssis.bids.converttolegacydeployment.f1"
  - "sql13.ssis.deploymentwizard.f1"
  - "sql13.ssis.ssms.isenvprop.permissions.f1"
  - "sql13.ssis.ssms.isenvprop.general.f1"
  - "sql13.ssis.ssms.iscreateenv.f1"
  - "sql13.ssis.ssms.isenvprop.variables.f1"
  - "sql13.ssis.migrationwizard.f1"
ms.assetid: bea8ce8d-cf63-4257-840a-fc9adceade8c
author: janinezhang
ms.author: janinez
manager: craigg
---
# Deploy Integration Services (SSIS) Projects and Packages
  [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] supports two deployment models, the project deployment model and the legacy package deployment model. The project deployment model enables you to deploy your projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
For more information about the legacy package deployment model, see [Legacy Package Deployment &#40;SSIS&#41;](../../integration-services/packages/legacy-package-deployment-ssis.md).  
  
> [!NOTE]  
>  The project deployment model was introduced in [!INCLUDE[ssISversion11](../../includes/ssisversion11-md.md)]. With this deployment model, you were not able to deploy one or more packages without deploying the whole project. [!INCLUDE[ssISversion13](../../includes/ssisversion13-md.md)] introduced the package deployment model, which lets you deploy one or more packages without deploying the whole project.  

> [!NOTE]
> This article describes how to deploy SSIS packages in general, and how to deploy packages on premises. You can also deploy SSIS packages to the following platforms:
> - **The Microsoft Azure cloud**. For more info, see [Lift and shift SQL Server Integration Services workloads to the cloud](../lift-shift/ssis-azure-lift-shift-ssis-packages-overview.md).
> - **Linux**. For more info, see [Extract, transform, and load data on Linux with SSIS](../../linux/sql-server-linux-migrate-ssis.md).

## Compare Project Deployment Model and legacy Package Deployment Model  
 The type of deployment model that you choose for a project determines which development and administrative options are available for that project. The following table shows the differences and similarities between using the project deployment model and using the package deployment model.  
  
|When Using the Project Deployment Model|When Using the legacy Package Deployment Model|  
|---------------------------------------------|----------------------------------------------------|  
|A project is the unit of deployment.|A package is the unit of deployment.|  
|Parameters are used to assign values to package properties.|Configurations are used to assign values to package properties.|  
|A project, containing packages and parameters, is built to a project deployment file (.ispac extension).|Packages (.dtsx extension) and configurations (.dtsConfig extension) are saved individually to the file system.|  
|A project, containing packages and parameters, is deployed to the SSISDB catalog on an instance of SQL Server.|Packages and configurations are copied to the file system on another computer. Packages can also be saved to the MSDB database on an instance of SQL Server.|  
|CLR integration is required on the database engine.|CLR integration is not required on the database engine.|  
|Environment-specific parameter values are stored in environment variables.|Environment-specific configuration values are stored in configuration files.|  
|Projects and packages in the catalog can be validated on the server before execution. You can use SQL Server Management Studio, stored procedures, or managed code to perform the validation.|Packages are validated just before execution. You can also validate a package with dtExec or managed code.|  
|Packages are executed by starting an execution on the database engine. A project identifier, explicit parameter values (optional), and environment references (optional) are assigned to an execution before it is started.<br /><br /> You can also execute packages using **dtExec**.|Packages are executed using the **dtExec** and **DTExecUI** execution utilities. Applicable configurations are identified by command-prompt arguments (optional).|  
|During execution, events that are produced by the package are captured automatically and saved to the catalog. You can query these events with Transact-SQL views.|During execution, events that are produced by a package are not captured automatically. A log provider must be added to the package to capture events.|  
|Packages are run in a separate Windows process.|Packages are run in a separate Windows process.|  
|SQL Server Agent is used to schedule package execution.|SQL Server Agent is used to schedule package execution.|  
  
 The project deployment model was introduced in [!INCLUDE[ssISversion11](../../includes/ssisversion11-md.md)]. If you used this model, you were not able to deploy one or more packages without deploying the whole project. The [!INCLUDE[ssISversion13](../../includes/ssisversion13-md.md)] introduced the Incremental Package Deployment feature that allows you to deploy one or more packages without deploying the whole project.   
  
## Features of Project Deployment Model  
 The following table lists the features that are available to projects developed only for the project deployment model.  
  
|Feature|Description|  
|-------------|-----------------|  
|Parameters|A parameter specifies the data that will be used by a package. You can scope parameters to the package level or project level with package parameters and project parameters, respectively. Parameters can be used in expressions or tasks. When the project is deployed to the catalog, you can assign a literal value for each parameter or use the default value that was assigned at design time. In place of a literal value, you can also reference an environment variable. Environment variable values are resolved at the time of package execution.|  
|Environments|An environment is a container of variables that can be referenced by [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] projects. Each project can have multiple environment references, but a single instance of package execution can only reference variables from a single environment. Environments allow you to organize the values that you assign to a package. For example, you might have environments named "Dev", "test", and "Production".|  
|Environment variables|An environment variable defines a literal value that can be assigned to a parameter during package execution. To use an environment variable, create an environment reference (in the project that corresponds to the environment having the parameter), assign a parameter value to the name of the environment variable, and specify the corresponding environment reference when you configure an instance of execution.|  
|SSISDB catalog|All [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects are stored and managed on an instance of SQL Server in a database referred to as the SSISDB catalog. The catalog allows you to use folders to organize your projects and environments. Each instance of SQL Server can have one catalog. Each catalog can have zero or more folders. Each folder can have zero or more projects and zero or more environments. A folder in the catalog can also be used as a boundary for permissions to [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects.|  
|Catalog stored procedures and views|A large number of stored procedures and views can be used to manage [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] objects in the catalog. For example, you can specify values to parameters and environment variables, create and start executions, and monitor catalog operations. You can even see exactly which values will be used by a package before execution starts.|  
  
## Project Deployment  
 At the center of the project deployment model is the project deployment file (.ispac extension). The project deployment file is a self-contained unit of deployment that includes only the essential information about the packages and parameters in the project. The project deployment file does not capture all of the information contained in the Integration Services project file (.dtproj extension). For example, additional text files that you use for writing notes are not stored in the project deployment file and thus are not deployed to the catalog.  

## Permissions Required to Deploy SSIS Projects and Packages

If you change the SSIS service account from the default, you may have to give additional permissions to the non-default service account before you can deploy packages successfully. If the non-default service account doesn't have the required permissions, you may see the following error message.

*A .NET Framework error occurred during execution of user-defined routine or aggregate "deploy_project_internal":
System.ComponentModel.Win32Exception: A required privilege is not held by the client.*

This error is typically the result of missing DCOM permissions. To fix the error, do the following things.

1.  Open the **Component Services** console (or run Dcomcnfg.exe).
2.  In the **Component Services** console, expand **Component Services** > **Computers** > **My Computer** > **DCOM Config**.
3.  In the list, locate **Microsoft SQL Server Integration Services xx.0** for the version of SQL Server that you're using. For example, SQL Server 2016 is version 13.
4.  Right-click and select **Properties**.
5.  In the **Microsoft SQL Server Integration Services 13.0 Properties** dialog box, select the **Security** tab.
6.  For each of the three sets of permissions - Launch and Activation, Access, and Configuration - select **Customize**, then select **Edit** to open the **Permission** dialog box.
7.  In the **Permission** dialog box, add the non-default service account and grant **Allow** permissions as required. Typically, an account has **Local Launch** and **Local Activation** permissions.
8.  Click **OK** twice, then close the **Component Services** console.

For more info about the error described in this section and about the permissions required by the SSIS service account, see the following blog post.  
[System.ComponentModel.Win32Exception: A required privilege is not held by the client while Deploying SSIS Project](https://blogs.msdn.microsoft.com/dataaccesstechnologies/2013/08/20/system-componentmodel-win32exception-a-required-privilege-is-not-held-by-the-client-while-deploying-ssis-project/)

## Deploy Projects to Integration Services Server
  In the current release of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], you can deploy your projects to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. The [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server enables you to manage packages, run packages, and configure runtime values for packages by using environments.  
  
> [!NOTE]  
>  As in earlier versions of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], in the current release you can also deploy your packages to an instance of SQL Server and use [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] service to run and manage the packages. You use the package deployment model. For more information, see [Legacy Package Deployment &#40;SSIS&#41;](../../integration-services/packages/legacy-package-deployment-ssis.md).  
  
 To deploy a project to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server, complete the following tasks:  
  
1.  Create an SSISDB catalog, if you haven't already. For more information, see [SSIS Catalog](../../integration-services/catalog/ssis-catalog.md).  
  
2.  Convert the project to the project deployment model by running the **Integration Services Project Conversion Wizard** . For more information, see the instructions below: [To convert a project to the project deployment model](#convert)  
  
    -   If you created the project in [!INCLUDE[ssISversion12](../../includes/ssisversion12-md.md)] or later, by default the project uses the project deployment model.  
  
    -   If you created the project in an earlier release of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], after you open the project file in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], convert the project to the project deployment model.  
  
        > [!NOTE]  
        >  If the project contains one or more datasources, the datasources are removed when the project conversion is completed. To create a connection to a data source that the packages in the project can share, add a connection manager at the project level. For more information, see [Add, Delete, or Share a Connection Manager in a Package](https://msdn.microsoft.com/library/6f2ba4ea-10be-4c40-9e80-7efcf6ee9655).  
  
         Depending on whether you run the **Integration Services Project Conversion Wizard** from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] or from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the wizard performs different conversion tasks.  
  
        -   If you run the wizard from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], the packages contained in the project are converted from [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] 2005, 2008, or 2008 R2 to the format that is used by the current version of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)]. The original project (.dtproj) and package (.dtsx) files are upgraded.  
  
        -   If you run the wizard from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the wizard generates a project deployment file (.ispac) from the packages and configurations contained in the project. The original package (.dtsx) files are not upgraded.  
  
             You can select an existing file or create a new file, in the **Selection Destination** page of the wizard.  
  
             To upgrade package files when a project is converted, run the **Integration Services Project Conversion Wizard** from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)]. To upgrade package files separately from a project conversion, run the **Integration Services Project Conversion Wizard** from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and then run the **SSIS Package Upgrade Wizard**. If you upgrade the package files separately, ensure that you save the changes. Otherwise, when you convert the project to the project deployment model, any unsaved changes to the package are not converted.  
  
     For more information on package upgrade, see [Upgrade Integration Services Packages](../../integration-services/install-windows/upgrade-integration-services-packages.md) and [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](../../integration-services/install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md).  
  
3.  Deploy the project to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. For more information, see the instructions below: [To deploy a project to the Integration Services Server](#deploy).  
  
4.  (Optional) Create an environment for the deployed project. 
  
###  <a name="convert"></a> To convert a project to the project deployment model  
  
1.  Open the project in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], and then in Solution Explorer, right-click the project and click **Convert to Project Deployment Model**.  
  
     -or-  
  
     From Object Explorer in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], right-click the **Projects** node and select **Import Packages**.  
  
2.  Complete the wizard.
  
###  <a name="deploy"></a> To deploy a project to the Integration Services Server  
  
1.  Open the project in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], and then From the **Project** menu, select **Deploy** to launch the **Integration Services Deployment Wizard**.  
  
     -or-  
  
     In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] > **SSISDB** node in Object Explorer, and locate the Projects folder for the project you want to deploy. Right-click the **Projects** folder, and then click **Deploy Project**.  
  
     -or-  
  
     From the command prompt, run **isdeploymentwizard.exe** from **%ProgramFiles%\Microsoft SQL Server\130\DTS\Binn**. On 64-bit computers, there is also a 32-bit version of the tool in **%ProgramFiles(x86)%\Microsoft SQL Server\130\DTS\Binn**.  
  
2.  On the **Select Source** page, click **Project deployment file** to select the deployment file for the project.  
  
     -OR-  
  
     Click **Integration Services catalog** to select a project that has already been deployed to the SSISDB catalog.  
  
3.  Complete the wizard. 

## Deploy Packages to Integration Services Server
  The Incremental Package Deployment feature introduced in  [!INCLUDE[ssISversion13](../../includes/ssisversion13-md.md)] lets you deploy one or more packages to an existing or new project without deploying the whole project.  
  
###  <a name="DeployWizard"></a> Deploy packages by using the Integration Services Deployment Wizard  
  
1.  From the command prompt, run **isdeploymentwizard.exe** from **%ProgramFiles%\Microsoft SQL Server\130\DTS\Binn**. On 64-bit computers, there is also a 32-bit version of the tool in **%ProgramFiles(x86)%\Microsoft SQL Server\130\DTS\Binn**.  
  
2.  On the **Select Source** page, switch to **Package Deployment model**. Then, select the folder which contains source packages and configure the packages.  
  
3.  Complete the wizard. Follow the remaining steps described in [Package Deployment Model](#PackageModel).  
  
###  <a name="SSMS"></a> Deploy packages by using SQL Server Management Studio  
  
1.  In SQL Server Management Studio, expand the **Integration Services Catalogs** > **SSISDB** node in Object Explorer.  
  
2.  Right-click the **Projects** folder, and then click **Deploy Projects**.  
  
3.  If you see the **Introduction** page, click **Next** to continue.  
  
4.  On the **Select Source** page, switch to **Package Deployment model**. Then, select the folder which contains source packages and configure the packages.  
  
5.  Complete the wizard. Follow the remaining steps described in [Package Deployment Model](#PackageModel).  
  
###  <a name="SSDT"></a> Deploy packages by using SQL Server Data Tools (Visual Studio)  
  
1.  In Visual Studio, with an Integration Services project open, select the package or packages that you want to deploy.  
  
2.  Right-click and select **Deploy Package**. The Deployment Wizard opens with the selected packages configured as the source packages.  
  
3.  Complete the wizard. Follow the remaining steps described in [Package Deployment Model](#PackageModel).  
  
###  <a name="StoredProcedure"></a> Deploy packages by using the deploy_packages stored procedure  
 You can use the **[catalog].[deploy_packages]** stored procedure to deploy one or more SSIS packages to the SSIS Catalog. The following code example demonstrates the use of this stored procedure to deploy packages to an SSIS server. For more info, see [catalog.deploy_packages](../../integration-services/system-stored-procedures/catalog-deploy-packages.md).  
  
```cs
  
private static void Main(string[] args)  
{  
    // Connection string to SSISDB  
    var connectionString = "Data Source=.;Initial Catalog=SSISDB;Integrated Security=True;MultipleActiveResultSets=false";  
  
    using (var sqlConnection = new SqlConnection(connectionString))  
    {  
        sqlConnection.Open();  
  
        var sqlCommand = new SqlCommand  
        {  
            Connection = sqlConnection,  
            CommandType = CommandType.StoredProcedure,  
            CommandText = "[catalog].[deploy_packages]"  
        };  
  
        var packageData = Encoding.UTF8.GetBytes(File.ReadAllText(@"C:\Test\Package.dtsx"));  
  
        // DataTable: name is the package name without extension and package_data is byte array of package.  
        var packageTable = new DataTable();  
        packageTable.Columns.Add("name", typeof(string));  
        packageTable.Columns.Add("package_data", typeof(byte[]));  
        packageTable.Rows.Add("Package", packageData);  
  
        // Set the destination project and folder which is named Folder and Project.  
        sqlCommand.Parameters.Add(new SqlParameter("@folder_name", SqlDbType.NVarChar, ParameterDirection.Input, "Folder", -1));  
        sqlCommand.Parameters.Add(new SqlParameter("@project_name", SqlDbType.NVarChar, ParameterDirection.Input, "Project", -1));  
        sqlCommand.Parameters.Add(new SqlParameter("@packages_table", SqlDbType.Structured, ParameterDirection.Input, packageTable, -1));  
  
        var result = sqlCommand.Parameters.Add("RetVal", SqlDbType.Int);  
        result.Direction = ParameterDirection.ReturnValue;  
  
        sqlCommand.ExecuteNonQuery();  
    }  
}  
  
```  
  
###  <a name="MOMApi"></a> Deploy packages using the Management Object Model API  
 The following code example demonstrates the use of the Management Object Model API to deploy packages to server.  
  
```cs 
  
static void Main()  
 {  
     // Before deploying packages, make sure the destination project exists in SSISDB.  
     var connectionString = "Data Source=.;Integrated Security=True;MultipleActiveResultSets=false";  
     var catalogName = "SSISDB";  
     var folderName = "Folder";  
     var projectName = "Project";  
  
     // Get the folder instance.  
     var sqlConnection = new SqlConnection(connectionString);  
     var store = new Microsoft.SqlServer.Management.IntegrationServices.IntegrationServices(sqlConnection);  
     var folder = store.Catalogs[catalogName].Folders[folderName];  
  
     // Key is package name without extension and value is package binaries.  
     var packageDict = new Dictionary<string, string>();  
  
     var packageData = File.ReadAllText(@"C:\Folder\Package.dtsx");  
     packageDict.Add("Package", packageData);  
  
     // Deploy package to the destination project.  
     folder.DeployPackages(projectName, packageDict);  
 }  
  
```

## Convert to Package Deployment Model Dialog Box
  The **Convert to Package Deployment Model** command allows you to convert a package to the package deployment model after checking the project and each package in the project for compatibility with that model. If a package uses features unique to the project deployment model, such as parameters, then the package cannot be converted.  
  
### Task List  
 Converting a package to the package deployment model requires two steps.  
  
1.  When you select the **Convert to Package Deployment Model** command from the **Project** menu, the project and each package are checked for compatibility with this model. The results are displayed in the **Results** table.  
  
     If the project or a package fails the compatibility test, click **Failed** in the **Result** column for more information. Click **Save Report** to save a copy of this information to a text file.  
  
2.  If the project and all packages pass the compatibility test, then click **OK** to convert the package.  
  
> **NOTE:** To convert a project to the project deployment model, use the **Integration Services Project Conversion Wizard**. For more information, see [Integration Services Project Conversion Wizard](deploy-integration-services-ssis-projects-and-packages.md).  

## Integration Services Deployment Wizard
  The **Integration Services Deployment Wizard** supports two deployment models:
   - Project deployment model
   - Package deployment model 
   
 The **Project Deployment model** allows you to deploy a SQL Server Integration Services (SSIS) project as a single unit to the SSIS Catalog.
 
 The **Package Deployment model** allows you to deploy packages that you have updated to the SSIS Catalog without having to deploy the whole project. 
 
 > **NOTE:** The Wizard default deployment is the Project Deployment model.  
  
### Launch the wizard
Launch the wizard by either:

 - Typing **"SQL Server Deployment Wizard"** in Windows Search 

**OR**

 - Search for the executable file **ISDeploymentWizard.exe** under the SQL Server installation folder; for example: "C:\Program Files (x86)\Microsoft SQL Server\130\DTS\Binn". 
 
 > **NOTE:** If you see the **Introduction** page, click **Next** to switch to the **Select Source** page. 
 
 The settings on this page are different for each deployment model. Follow  steps in the [Project Deployment Model](#ProjectModel) section or [Package Deployment Model](#PackageModel) section based on the model you selected in this page.  
  
###  <a name="ProjectModel"></a> Project Deployment Model  
  
#### Select Source  
 To deploy a project deployment file that you created, select **Project deployment file** and enter the path to the .ispac file. To deploy a project that resides in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog, select **Integration Services catalog**, and then enter the server name and the path to the project in the catalog. Click **Next** to see the **Select Destination** page.  
  
#### Select Destination  
 To select the destination folder for the project in the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] catalog, enter the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance or click **Browse** to select from a list of servers. Enter the project path in SSISDB or click **Browse** to select it. Click **Next** to see the **Review** page.  
  
#### Review (and deploy)  
 The page allows you to review the settings you have selected. You can change your selections by clicking **Previous**, or by clicking any of the steps in the left pane. Click **Deploy** to start the deployment process.  
  
#### Results  
 After the deployment process is complete, you should see the **Results** page. This page displays the success or failure of each action. If the action fails, click the **Failed** in the **Result** column to display an explanation of the error. Click **Save Report...** to save the results to an XML file or Click **Close** to exit the wizard.
  
###  <a name="PackageModel"></a> Package Deployment Model  
  
#### Select Source  
 The **Select Source** page in the **Integration Services Deployment Wizard** shows settings specific to the package deployment model when you selected the **Package Deployment** option for the **deployment model**.  
  
 To select the source packages, click **Browse...** button to select the **folder** that contains the packages or type the folder path in the **Packages folder path** textbox and click **Refresh** button at the bottom of the page. Now, you should see all the packages in the specified folder in the list box. By default, all the packages are selected. Click the **checkbox** in the first column to choose which packages you want to be deployed to server.  
  
 Refer to the **Status** and **Message** columns to verify the status of package. If the status is set to **Ready** or **Warning**, the deployment wizard would not block the deployment process. Whereas, if the status is set to **Error**, the wizard would not proceed further to deploy selected packages. To view the detailed Warning/Error messages, click the link in the **Message** column.  
  
 If the sensitive data or package data are encrypted with a password, type the password in the **Password** column and click the **Refresh** button to verify whether the password is accepted. If the password is correct, the status would change to **Ready** and the warning message will disappear. If there are multiple packages with the same password, select the packages with the same encryption password, type the password in the **Password** textbox and click **Apply** button. The password would be applied to the selected packages.  
  
 If the status of all the selected packages is not set to **Error**, the **Next** button will be enabled so that you can continue with the package deployment process.  
  
#### Select Destination  
 After selecting package sources, click **Next** button to switch to the **Select Destination** page. Packages must be deployed to a project in the SSIS Catalog (SSISDB). Therefore, before deploying packages, please ensure the destination project already exists in the SSIS Catalog. , otherwise create an empty project.In the **Select Destination** page, type the server name in the **Server Name** textbox or click the **Browse...** button to select a server instance. Then click the **Browse...** button next to **Path** textbox to specify the destination project. If the project does not exist, click the **New project...** to create an empty project as the destination project. The project **MUST** be created under a folder.  
  
#### Review and deploy  
 Click **Next** on the **Select Destination** page to switch to the **Review** page in the **Integration Services Deployment Wizard**. In the review page, review the summary report about the deployment action. After the verification, click **Deploy** button to perform the deployment action.  
  
#### Results  
 After the deployment is complete, you should see the **Results** page. In the **Results** page, review results from each step in the deployment process. On the **Results** page, click **Save Report** to save the deployment report or **Close** to the close the wizard.  

## Create and Map a Server Environment
  You create a server environment to specify runtime values for packages contained in a project you've deployed to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. You can then map the environment variables to parameters, for a specific package, for entry-point packages, or for all the packages in a given project. An entry-point package is typically a parent package that executes a child package.  
  
> [!IMPORTANT]  
>  For a given execution, a package can execute only with the values contained in a single server environment.  
  
 You can query views for a list of server environments, environment references, and environment variables. You can also call stored to add, delete, and modify environments, environment references, and environment variables. For more information, see the **Server Environments, Server Variables and Server Environment References** section in [SSIS Catalog](../../integration-services/catalog/ssis-catalog.md).  
  
### To create and use a server environment  
  
1.  In [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], expand the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] Catalogs> **SSISDB** node in Object Explorer, and locate the **Environments** folder of the project for which you want to create an environment.  
  
2.  Right-click the **Environments** folder, and then click **Create Environment**.  
  
3.  Type a name for the environment and optionally a description, and then click **OK**.  
  
4.  Right-click the new environment and then click **Properties**.  
  
5.  On the **Variables** page, do the following to add a variable.  
  
    1.  Select the **Type** for the variable. The name of the variable **does not** need to match the name of the project parameter that you map to the variable.  
  
    2.  Enter an optional **Description** for the variable.  
  
    3.  Enter the **Value** for the environment variable.  
  
         For information about the rules for environment variable names, see the **Environment Variable** section in [SSIS Catalog](../../integration-services/catalog/ssis-catalog.md).  
  
    4.  Indicate whether the variable contains sensitive value, by selecting or clearing the **Sensitive** checkbox.  
  
         If you select **Sensitive**, the variable value does not display in the **Value** field.  
  
         Sensitive values are encrypted in the SSISDB catalog. For more information about the encryption, see [SSIS Catalog](../../integration-services/catalog/ssis-catalog.md).  
  
6.  On the **Permissions** page, grant or deny permissions for selected users and roles by doing the following.  
  
    1.  Click **Browse**, and then select one or more users and roles in the **Browse All Principals** dialog box.  
  
    2.  In the **Logins or roles** area, select the user or role that you want to grant or deny permissions for.  
  
    3.  In the **Explicit** area, click **Grant** or **Deny** next to each permission.  
  
7.  To script the environment, click **Script**. By default, the script displays in a new Query Editor window.  
  
    > [!TIP]  
    >  You need to click **Script** after you've made one or changes to the environment properties, such as adding a variable, and before you click **OK** in the **Environment Properties** dialog box. Otherwise, a script is not generated.  
  
8.  Click **OK** to save your changes to the environment properties.  
  
9. Under the **SSISDB** node in Object Explorer, expand the **Projects** folder, right-click the project, and then click **Configure**.  
  
10. On the **References** page, click **Add** to add an environment, and then click **OK** to save the reference to the environment.  
  
11. Right-click the project again, and then click **Configure**.  
  
12. To map the environment variable to a parameter that you added to the package at design-time or to a parameter that was generated when you converted the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to the project deployment model, do the following.,  
  
    1.  In the **Parameters** tab on the **Parameters** page, click the browse button next to the **Value** field.  
  
    2.  Click **Use environment variable**, and then select the environment variable you created.  
  
13. To map the environment variable to a connection manager property, do the following. Parameters are automatically generated on the SSIS server for the connection manager properties.  
  
    1.  In the **Connection Managers** tab on the **Parameters** page, click the browse button next to the **Value** field.  
  
    2.  Click **Use environment variable**, and then select the environment variable you created.  
  
14. Click **OK** twice to save your changes.  

## Deploy and Execute SSIS Packages using Stored Procedures
  When you configure an [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project to use the project deployment model, you can use stored procedures in the [!INCLUDE[ssIS](../../includes/ssis-md.md)] catalog to deploy the project and execute the packages. For information about the project deployment model, see [Deployment of Projects and Packages](https://msdn.microsoft.com/library/hh213290.aspx).  
  
 You can also use [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to deploy the project and execute the packages. For more information, see the topics in the **See Also** section.  
  
> [!TIP]
>  You can easily generate the Transact-SQL statements for the stored procedures listed in the procedure below, with the exception of catalog.deploy_project, by doing the following:  
> 
>  1.  In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], expand the **Integration Services Catalogs** node in Object Explorer and navigate to the package you want to execute.  
> 2.  Right-click the package, and then click **Execute**.  
> 3.  As needed, set parameters values, connection manager properties, and options in the **Advanced** tab such as the logging level.  
> 
>      For more information about logging levels, see [Enable Logging for Package Execution on the SSIS Server](../../integration-services/performance/integration-services-ssis-logging.md#server_logging).  
> 4.  Before clicking **OK** to execute the package, click **Script**. The Transact-SQL appears in a Query Editor window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)].  
  
### To deploy and execute a package using stored procedures  
  
1.  Call [catalog.deploy_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-deploy-project-ssisdb-database.md) to deploy the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project that contains the package to the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server.  
  
     To retrieve the binary contents of the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] project deployment file, for the *@project_stream* parameter, use a SELECT statement with the OPENROWSET function and the BULK rowset provider. The BULK rowset provider enables you to read data from a file. The SINGLE_BLOB argument for the BULK rowset provider returns the contents of the data file as a single-row, single-column rowset of type varbinary(max). For more information, see [OPENROWSET &#40;Transact-SQL&#41;](../../t-sql/functions/openrowset-transact-sql.md).  
  
     In the following example, the SSISPackages_ProjectDeployment project is deployed to the SSIS Packages folder on the [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] server. The binary data is read from the project file (SSISPackage_ProjectDeployment.ispac) and is stored in the *@ProjectBinary* parameter of type varbinary(max). The *@ProjectBinary* parameter value is assigned to the *@project_stream* parameter.  
  
    ```sql
    DECLARE @ProjectBinary as varbinary(max)  
    DECLARE @operation_id as bigint  
    Set @ProjectBinary = (SELECT * FROM OPENROWSET(BULK 'C:\MyProjects\ SSISPackage_ProjectDeployment.ispac', SINGLE_BLOB) as BinaryData)  
  
    Exec catalog.deploy_project @folder_name = 'SSIS Packages', @project_name = 'DeployViaStoredProc_SSIS', @Project_Stream = @ProjectBinary, @operation_id = @operation_id out  
  
    ```  
  
2.  Call [catalog.create_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-create-execution-ssisdb-database.md) to create an instance of the package execution, and optionally call [catalog.set_execution_parameter_value &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-set-execution-parameter-value-ssisdb-database.md) to set runtime parameter values.  
  
     In the following example, catalog.create_execution creates an instance of execution for package.dtsx that is contained in the SSISPackage_ProjectDeployment project. The project is located in the SSIS Packages folder. The execution_id returned by the stored procedure is used in the call to catalog.set_execution_parameter_value. This second stored procedure sets the LOGGING_LEVEL parameter to 3 (verbose logging) and sets a package parameter named Parameter1 to a value of 1.  
  
     For parameters such as LOGGING_LEVEL the object_type value is 50. For package parameters the object_type value is 30.  
  
    ```sql
    Declare @execution_id bigint  
    EXEC [SSISDB].[catalog].[create_execution] @package_name=N'Package.dtsx', @execution_id=@execution_id OUTPUT, @folder_name=N'SSIS Packages', @project_name=N'SSISPackage_ProjectDeployment', @use32bitruntime=False, @reference_id=1  
  
    Select @execution_id  
    DECLARE @var0 smallint = 3  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=50, @parameter_name=N'LOGGING_LEVEL', @parameter_value=@var0  
  
    DECLARE @var1 int = 1  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=30, @parameter_name=N'Parameter1', @parameter_value=@var1  
  
    GO  
  
    ```  
  
3.  Call [catalog.start_execution &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-start-execution-ssisdb-database.md) to execute the package.  
  
     In the following example, a call to catalog.start_execution is added to the Transact-SQL to start the package execution. The execution_id returned by the catalog.create_execution stored procedure is used.  
  
    ```sql
    Declare @execution_id bigint  
    EXEC [SSISDB].[catalog].[create_execution] @package_name=N'Package.dtsx', @execution_id=@execution_id OUTPUT, @folder_name=N'SSIS Packages', @project_name=N'SSISPackage_ProjectDeployment', @use32bitruntime=False, @reference_id=1  
  
    Select @execution_id  
    DECLARE @var0 smallint = 3  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=50, @parameter_name=N'LOGGING_LEVEL', @parameter_value=@var0  
  
    DECLARE @var1 int = 1  
    EXEC [SSISDB].[catalog].[set_execution_parameter_value] @execution_id,  @object_type=30, @parameter_name=N'Parameter1', @parameter_value=@var1  
  
    EXEC [SSISDB].[catalog].[start_execution] @execution_id  
    GO  
  
    ```  
  
### To deploy a project from server to server using stored procedures  
 You can deploy a project from server to server by using the [catalog.get_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-get-project-ssisdb-database.md) and [catalog.deploy_project &#40;SSISDB Database&#41;](../../integration-services/system-stored-procedures/catalog-deploy-project-ssisdb-database.md) stored procedures.  
  
 You need to do the following before running the stored procedures.  
  
-   Create a linked server object. For more information, see [Create Linked Servers &#40;SQL Server Database Engine&#41;](../../relational-databases/linked-servers/create-linked-servers-sql-server-database-engine.md).  
  
     On the **Server Options** page of the **Linked Server Properties** dialog box, set **RPC** and **RPC Out** to **True**. Also, set **Enable Promotion of Distributed Transactions for RPC** to **False**.  
  
-   Enable dynamic parameters for the provider you selected for the linked server, by expanding the **Providers** node under **Linked Servers** in Object Explorer, right-clicking the provider, and then clicking **Properties**. Select **Enable** next to **Dynamic parameter.**  
  
-   Confirm that the Distributed Transaction Coordinator (DTC) is started on both servers.  
  
 Call catalog.get_project to return the binary for the project, and then call catalog.deploy_project. The value returned by catalog.get_project is inserted into a table variable of type varbinary(max). The linked server can't return results that are varbinary(max).  
  
 In the following example, catalog.get_project returns a binary for the SSISPackages project on the linked server. The catalog.deploy_project deploys the project to the local server, to the folder named DestFolder.  
  
```sql
declare @resultsTableVar table (  
project_binary varbinary(max)  
)  
  
INSERT @resultsTableVar (project_binary)  
EXECUTE [MyLinkedServer].[SSISDB].[catalog].[get_project] 'Packages', 'SSISPackages'  
  
declare @project_binary varbinary(max)  
select @project_binary = project_binary from @resultsTableVar  
  
exec [SSISDB].[CATALOG].[deploy_project] 'DestFolder', 'SSISPackages', @project_binary  
  
```  

## Integration Services Project Conversion Wizard
  The **Integration Services Project Conversion Wizard** converts a project to the project deployment model.  
  
> [!NOTE]  
>  If the project contains one or more datasources, the datasources are removed when the project conversion is completed. To create a connection to a data source that can be shared by the packages in the project, add a connection manager at the project level. For more information, see [Add, Delete, or Share a Connection Manager in a Package](https://msdn.microsoft.com/library/6f2ba4ea-10be-4c40-9e80-7efcf6ee9655).  
  
 **What do you want to do?**  
  
-   [Open the Integration Services Project Conversion Wizard](#open_dialog)  
  
-   [Set Options on the Locate Packages Page](#locate)  
  
-   [Set Options on the Select Packages Page](#selectPackages)  
  
-   [Set Options on the Select Destination Page](#destination)  
  
-   [Set Options on the Specify Project Properties Page](#projectProperties)  
  
-   [Set Options on the Update Execute Package Task Page](#executePackage)  
  
-   [Set Options on the Select Configurations Page](#configurations)  
  
-   [Set Options on the Create Parameters Page](#createParameters)  
  
-   [Set Options on the Configure Parameters Page](#configureParameters)  
  
-   [Set the Options on the Review page](#review)  
  
-   [Set the Options on the Perform Conversion](#conversion)  
  
###  <a name="open_dialog"></a> Open the Integration Services Project Conversion Wizard  
 Do one of the following to open the **Integration Services Project Conversion** Wizard.  
  
-   Open the project in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)], and then in Solution Explorer, right-click the project and click **Convert to Project Deployment Model**.  
  
-   From Object Explorer in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], right-click the **Projects** node and select **Import Packages**.  
  
 Depending on whether you run the **Integration Services Project Conversion Wizard** from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] or from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the wizard performs different conversion tasks.   
  
###  <a name="locate"></a> Set Options on the Locate Packages Page  
  
> [!NOTE]  
>  The **Locate Packages** page is available only when you run the wizard from [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
 The following option displays on the page when you select **File system** in the **Source** drop-down list. Select this option when the package is resides in the file system.  
  
 **Folder**  
 Type the package path, or navigate to the package by clicking **Browse**.  
  
 The following options display on the page when you select **SSIS Package Store** in the **Source** drop-down list. For more information about the package store, see [Package Management &#40;SSIS Service&#41;](../../integration-services/service/package-management-ssis-service.md).  
  
 **Server**  
 Type the server name or select the server.  
  
 **Folder**  
 Type the package path, or navigate to the package by clicking **Browse**.  
  
 The following options display on the page when you select **Microsoft SQL Server** in the **Source** drop-down list. Select this option when the package resides in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 **Server**  
 Type the server name or select the server.  
  
 **Use Windows authentication**  
 Microsoft Windows Authentication mode allows a user to connect through a Windows user account. If you use Windows Authentication, you do not need to provide a user name or password.  
  
 **Use SQL Server authentication**  
 When a user connects with a specified login name and password from a non-trusted connection, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] authenticates the connection by checking to see if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have a login account set, authentication fails, and the user receives an error message.  
  
 **User name**  
 Specify a user name when you are using SQL Server Authentication.  
  
 **Password**  
 Provide the password when you are using SQL Server Authentication.  
  
 **Folder**  
 Type the package path, or navigate to the package by clicking **Browse**.  
  
###  <a name="selectPackages"></a> Set Options on the Select Packages Page  
 **Package Name**  
 Lists the package file.  
  
 **Status**  
 Indicates whether a package is ready to convert to the project deployment model.  
  
 **Message**  
 Displays a message associated with the package.  
  
 **Password**  
 Displays a password associated with the package. The password text is hidden.  
  
 **Apply to selection**  
 Click to apply the password in the **Password** text box, to the selected package or packages.  
  
 **Refresh**  
 Refreshes the list of packages.  
  
###  <a name="destination"></a> Set Options on the Select Destination Page  
 On this page, specify the name and path for a new project deployment file (.ispac) or select an existing file.  
  
> [!NOTE]  
>  The **Select Destination** page is available only when you run the wizard from [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)].  
  
 **Output path**  
 Type the path for the deployment file or navigate to the file by clicking **Browse**.  
  
 **Project name**  
 Type the project name.  
  
 **Protection level**  
 Select the protection level. For more information, see [Access Control for Sensitive Data in Packages](../../integration-services/security/access-control-for-sensitive-data-in-packages.md).  
  
 **Project description**  
 Type an optional description for the project.  
  
###  <a name="projectProperties"></a> Set Options on the Specify Project Properties Page  
  
> [!NOTE]  
>  The **Specify Project Properties** page is available only when you run the wizard from [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
 **Project name**  
 Lists the project name.  
  
 **Protection level**  
 Select a protection level for the packages contained in the project. For more information about protection levels, see [Access Control for Sensitive Data in Packages](../../integration-services/security/access-control-for-sensitive-data-in-packages.md).  
  
 **Project description**  
 Type an optional project description.  
  
###  <a name="executePackage"></a> Set Options on the Update Execute Package Task Page  
 Update Execute Package Tasks contain in the packages, to use a project-based reference. For more information, see [Execute Package Task Editor](../../integration-services/control-flow/execute-package-task-editor.md).  
  
 **Parent Package**  
 Lists the name of the package that executes the child package using the Execute Package task.  
  
 **Task name**  
 Lists the name of the Execute Package task.  
  
 **Original reference**  
 Lists the current path of the child package.  
  
 **Assign reference**  
 Select a child package stored in the project.  
  
###  <a name="configurations"></a> Set Options on the Select Configurations Page  
 Select the package configurations that you want to replace with parameters.  
  
 **Package**  
 Lists the package file.  
  
 **Type**  
 Lists the type of configuration, such as an XML configuration file.  
  
 **Configuration String**  
 Lists the path of the configuration file.  
  
 **Status**  
 Displays a status message for the configuration. Click the message to view the entire message text.  
  
 **Add Configurations**  
 Add package configurations contained in other projects to the list of available configurations that you want to replace with parameters. You can select configurations stored in a file system or stored in SQL Server.  
  
 **Refresh**  
 Click to refresh the list of configurations.  
  
 **Remove configurations from all packages after conversion**  
 It is recommended that you remove all configurations from the project by selecting this option.  
  
 If you don't select this option, only the configurations that you selected to replace with parameters are removed.  
  
###  <a name="createParameters"></a> Set Options on the Create Parameters Page  
 Select the parameter name and scope for each configuration property.  
  
 **Package**  
 Lists the package file.  
  
 **Parameter Name**  
 Lists the parameter name.  
  
 **Scope**  
 Select the scope of the parameter, either package or project.  
  
###  <a name="configureParameters"></a> Set Options on the Configure Parameters Page  
 **Name**  
 Lists the parameter name.  
  
 **Scope**  
 Lists the scope of the parameter.  
  
 **Value**  
 Lists the parameter value.  
  
 Click the ellipsis button next to the value field to configure the parameter properties.  
  
 In the **Set Parameter Details** dialog box, you can edit the parameter value. You can also specify whether the parameter value must be provided when you run the package.  
  
 You can modify value in the **Parameters** page of the **Configure** dialog box in [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)], by clicking the browse button next to the parameter. The **Set Parameter Value** dialog box appears.  
  
 The **Set Parameter Details** dialog box also lists the data type of the parameter value and the origin of the parameter.  
  
###  <a name="review"></a> Set the Options on the Review page  
 Use the **Review** page to confirm the options that you've selected for the conversion of the project.  
  
 **Previous**  
 Click to change an option.  
  
 **Convert**  
 Click to convert the project to the project deployment model.  
  
###  <a name="conversion"></a> Set the Options on the Perform Conversion  
 The Perform Conversion page shows status of the project conversion.  
  
 **Action**  
 Lists a specific conversion step.  
  
 **Result**  
 Lists the status of each conversion step. Click the status message for more information.  
  
 The project conversion is not saved until the project is saved in [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)].  
  
 **Save report**  
 Click to save a summary of the project conversion in an .xml file.  
