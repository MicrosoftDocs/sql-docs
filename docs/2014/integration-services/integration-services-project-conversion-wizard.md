---
title: "Integration Services Project Conversion Wizard | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.ssis.migrationwizard.f1"
ms.assetid: a192b094-4d0f-4c21-b911-460ec844a49f
author: janinezhang
ms.author: janinez
manager: craigg
---
# Integration Services Project Conversion Wizard
  The **Integration Services Project Conversion Wizard** converts a project to the project deployment model.  
  
> [!NOTE]  
>  If the project contains one or more datasources, the datasources are removed when the project conversion is completed. To create a connection to a data source that can be shared by the packages in the project, add a connection manager at the project level. For more information, see [Add, Delete, or Share a Connection Manager in a Package](../../2014/integration-services/add-delete-or-share-a-connection-manager-in-a-package.md).  
  
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
  
##  <a name="open_dialog"></a> Open the Integration Services Project Conversion Wizard  
 Do one of the following to open the **Integration Services Project Conversion** Wizard.  
  
-   Open the project in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)], and then in Solution Explorer, right-click the project and click **Convert to Project Deployment Model**.  
  
-   From Object Explorer in [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], right-click the **Projects** node and select **Import Packages**.  
  
 Depending on whether you run the **Integration Services Project Conversion Wizard** from [!INCLUDE[vsprvs](../includes/vsprvs-md.md)] or from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], the wizard performs different conversion tasks. For more information, see [Deploy Projects to Integration Services Server](../../2014/integration-services/deploy-projects-to-integration-services-server.md).  
  
##  <a name="locate"></a> Set Options on the Locate Packages Page  
  
> [!NOTE]  
>  The **Locate Packages** page is available only when you run the wizard from [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].  
  
 The following option displays on the page when you select **File system** in the **Source** drop-down list. Select this option when the package is resides in the file system.  
  
 **Folder**  
 Type the package path, or navigate to the package by clicking **Browse**.  
  
 The following options display on the page when you select **SSIS Package Store** in the **Source** drop-down list. For more information about the package store, see [Package Management &#40;SSIS Service&#41;](service/package-management-ssis-service.md).  
  
 **Server**  
 Type the server name or select the server.  
  
 **Folder**  
 Type the package path, or navigate to the package by clicking **Browse**.  
  
 The following options display on the page when you select **Microsoft SQL Server** in the **Source** drop-down list. Select this option when the package resides in Microsoft [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
 **Server**  
 Type the server name or select the server.  
  
 **Use Windows authentication**  
 Microsoft Windows Authentication mode allows a user to connect through a Windows user account. If you use Windows Authentication, you do not need to provide a user name or password.  
  
 **Use SQL Server authentication**  
 When a user connects with a specified login name and password from a non-trusted connection, [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] authenticates the connection by checking to see if a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] login account has been set up and if the specified password matches the one previously recorded. If [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] does not have a login account set, authentication fails, and the user receives an error message.  
  
 **User name**  
 Specify a user name when you are using SQL Server Authentication.  
  
 **Password**  
 Provide the password when you are using SQL Server Authentication.  
  
 **Folder**  
 Type the package path, or navigate to the package by clicking **Browse**.  
  
##  <a name="selectPackages"></a> Set Options on the Select Packages Page  
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
  
##  <a name="destination"></a> Set Options on the Select Destination Page  
 On this page, specify the name and path for a new project deployment file (.ispac) or select an existing file.  
  
> [!NOTE]  
>  The **Select Destination** page is available only when you run the wizard from [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)].  
  
 **Output path**  
 Type the path for the deployment file or navigate to the file by clicking **Browse**.  
  
 **Project name**  
 Type the project name.  
  
 **Protection level**  
 Select the protection level. For more information, see [Access Control for Sensitive Data in Packages](security/access-control-for-sensitive-data-in-packages.md).  
  
 **Project description**  
 Type an optional description for the project.  
  
##  <a name="projectProperties"></a> Set Options on the Specify Project Properties Page  
  
> [!NOTE]  
>  The **Specify Project Properties** page is available only when you run the wizard from [!INCLUDE[vsprvs](../includes/vsprvs-md.md)].  
  
 **Project name**  
 Lists the project name.  
  
 **Protection level**  
 Select a protection level for the packages contained in the project. For more information about protection levels, see [Access Control for Sensitive Data in Packages](security/access-control-for-sensitive-data-in-packages.md).  
  
 **Project description**  
 Type an optional project description.  
  
##  <a name="executePackage"></a> Set Options on the Update Execute Package Task Page  
 Update Execute Package Tasks contain in the packages, to use a project-based reference. For more information, see [Execute Package Task Editor](../../2014/integration-services/execute-package-task-editor.md).  
  
 **Parent Package**  
 Lists the name of the package that executes the child package using the Execute Package task.  
  
 **Task name**  
 Lists the name of the Execute Package task.  
  
 **Original reference**  
 Lists the current path of the child package.  
  
 **Assign reference**  
 Select a child package stored in the project.  
  
##  <a name="configurations"></a> Set Options on the Select Configurations Page  
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
  
##  <a name="createParameters"></a> Set Options on the Create Parameters Page  
 Select the parameter name and scope for each configuration property.  
  
 **Package**  
 Lists the package file.  
  
 **Parameter Name**  
 Lists the parameter name.  
  
 **Scope**  
 Select the scope of the parameter, either package or project.  
  
##  <a name="configureParameters"></a> Set Options on the Configure Parameters Page  
 **Name**  
 Lists the parameter name.  
  
 **Scope**  
 Lists the scope of the parameter.  
  
 **Value**  
 Lists the parameter value.  
  
 Click the ellipsis button next to the value field to configure the parameter properties.  
  
 In the **Set Parameter Details** dialog box, you can edit the parameter value. You can also specify whether the parameter value must be provided when you run the package.  
  
 You can modify value in the **Parameters** page of the **Configure** dialog box in [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], by clicking the browse button next to the parameter. The **Set Parameter Value** dialog box appears.  
  
 The **Set Parameter Details** dialog box also lists the data type of the parameter value and the origin of the parameter.  
  
##  <a name="review"></a> Set the Options on the Review page  
 Use the **Review** page to confirm the options that you've selected for the conversion of the project.  
  
 **Previous**  
 Click to change an option.  
  
 **Convert**  
 Click to convert the project to the project deployment model.  
  
##  <a name="conversion"></a> Set the Options on the Perform Conversion  
 The Perform Conversion page shows status of the project conversion.  
  
 **Action**  
 Lists a specific conversion step.  
  
 **Result**  
 Lists the status of each conversion step. Click the status message for more information.  
  
 The project conversion is not saved until the project is saved in [!INCLUDE[vsprvs](../includes/vsprvs-md.md)].  
  
 **Save report**  
 Click to save a summary of the project conversion in an .xml file.  
  
## See Also  
 [Deploy Projects to Integration Services Server](../../2014/integration-services/deploy-projects-to-integration-services-server.md)  
  
  
