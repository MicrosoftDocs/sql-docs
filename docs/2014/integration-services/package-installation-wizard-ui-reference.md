---
title: "Package Installation Wizard UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.deploymentwizard.confirminstallation.f1"
  - "sql12.dts.deploymentwizard.deploydtspackages.f1"
  - "sql12.dts.deploymentwizard.selectinstfolder.f1"
  - "sql12.dts.deploymentwizard.specifytargetsqlserver.f1"
  - "sql12.dts.deploymentwizard.welcome.f1"
  - "sql12.dts.deploymentwizard.finish.f1"
  - "sql12.dts.deploymentwizard.configurepackages.f1"
  - "sql12.dts.deploymentwizard.packagevalidation.f1"
helpviewer_keywords: 
  - "Package Installer Wizard"
ms.assetid: 6fca44d9-5001-4644-bcf3-c2d10a674b97
author: janinezhang
ms.author: janinez
manager: craigg
---
# Package Installation Wizard UI Reference
  Use the **Package Installation Wizard** to deploy a [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, including the packages and miscellaneous files it contains and any package dependencies.  
  
 Before you deploy packages, you can create configurations and then deploy them with the packages. [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] uses configurations to dynamically update properties of packages and package objects at run time. For example, the connection string of an OLE DB connection can be set dynamically at run time by providing a configuration that maps a value to the property that contains the connection string.  
  
 You cannot run the Package Installation Wizard until you build an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project and create a deployment utility. For more information, see [Deploy Packages by Using the Deployment Utility](../../2014/integration-services/deploy-packages-by-using-the-deployment-utility.md).  
  
 The following sections describe pages of the wizard.  
  
## Welcome to the Package Installation Wizard Page  
 Use the **Package Installation Wizard** to deploy an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project for which you built a package deployment utility.  
  
 **Do not show this starting page again**  
 Select to skip the starting page when you run the wizard again.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
## Configure Packages Page  
 Use the **Configure Packages** page to edit package configurations.  
  
### Options  
 **Configuration file**  
 Edit the contents of a configuration file by selecting the file from the list.  
  
 **Related Topics:** [Create Package Configurations](../../2014/integration-services/create-package-configurations.md)  
  
 **Path**  
 View the path of the property to be configured.  
  
 **Type**  
 View the data type of the property.  
  
 **Value**  
 Specify the value of the configuration.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
## Confirm Installation Page  
 Use the **Confirm Installation** page to start the installation of packages, to view the status, and to view the information that the wizard will use to install files from the specified project.  
  
 **Next**  
 Install the packages and their dependencies and go to the next wizard page when installation is completed.  
  
 **Status**  
 Shows the progress of the package installation.  
  
 **Finish**  
 Go to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all the required options.  
  
## Deploy SSIS Packages Page  
 Use the **Deploy SSIS Packages** page to specify where to install [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] packages and their dependencies.  
  
### Options  
 **File system deployment**  
 Deploy packages and dependencies in a specified folder in the file system.  
  
 **SQL Server deployment**  
 Deploy packages and dependencies in an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Use this option if [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] shares packages between servers. Any package dependencies are installed in the specified folder in the file system.  
  
 **Validate packages after installation**  
 Indicate whether to validate packages after installation.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
## Packages Validation Page  
 Use the **Packages Validation** page to view the progress and results of the package validation.  
  
 **Next**  
 Go to the next page in the wizard.  
  
## Select Installation Folder Page  
 Use the **Select Installation Folder** page to specify the file system folder in which to install the packages and their dependencies.  
  
### Options  
 **Folder**  
 Specify the path and folder in which to copy the package and its dependencies.  
  
 **Browse**  
 Browse to the target folder by using the **Browse For Folder** dialog box.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and if have specified all of the required options.  
  
## Specify Target SQL Server Page  
 Use the **Specify Target SQL Server** page to specify options for deploying the package to an instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
### Options  
 **Server name**  
 Specify the name of the server to deploy the packages to.  
  
 **Use Windows Authentication**  
 Specify whether to use Windows Authentication to log on to the server. Windows Authentication is recommended for better security.  
  
 **Use SQL Server Authentication**  
 Specify whether the package should use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication to log on to the server. If you use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, you must provide a user name and password.  
  
 **User name**  
 Specify a user name.  
  
 **Password**  
 Specify a password.  
  
 **Package path**  
 Specify the name of the logical folder, or enter "/" for the default folder.  
  
 To select the folder in the **SSIS Package** dialog box, click browse (...). However, the dialog box does not provide a means to select the default folder. If you want to use the default folder, you have to enter "/" in the text box.  
  
> [!NOTE]  
>  If you do not enter a valid package path, the following error message appears: "One or more arguments are invalid."  
  
 **Rely on server storage for encryption**  
 Select to use security features of the [!INCLUDE[ssDE](../includes/ssde-md.md)] to help secure the packages.  
  
 **Next**  
 Go to the next page in the wizard.  
  
 **Finish**  
 Skip to the Finish the Package Installation Wizard page. Use this option if you have backtracked through the wizard pages to revise your choices and have specified all of the required options.  
  
## Finish the Package Installation Page  
 Use the **Finish the Package Installation Wizard** page to view a summary of the package installation results. This page provides details such as the name of the deployed [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, the packages that were installed, the configuration files, and the installation location.  
  
 **Finish**  
 Exit the wizard by clicking **Finish**.  
  
## See Also  
 [Package Deployment &#40;SSIS&#41;](packages/legacy-package-deployment-ssis.md)  
  
  
