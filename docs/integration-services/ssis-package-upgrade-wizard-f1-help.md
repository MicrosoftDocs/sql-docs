---
description: "SSIS Package Upgrade Wizard F1 Help"
title: "SSIS Package Upgrade Wizard F1 Help | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: "reference"
f1_keywords: 
  - "sql13.is.upgradewizard.ssisupgradewizard.f1"
  - "sql13.is.upgradewizard.selectsourcelocation.f1"
  - "sql13.is.upgradewizard.selectdestinationlocation.f1"
  - "sql13.is.upgradewizard.selectpackagemgtoptions.f1"
  - "sql13.is.upgradewizard.selectpackages.f1"
  - "sql13.is.upgradewizard.completewizard.f1"
  - "sql13.is.upgradewizard.upgradingpackage.f1"
ms.assetid: 7fe886ff-1ea5-48d5-9d20-d5da36dd1cd7
author: chugugrace
ms.author: chugu
---
# SSIS Package Upgrade Wizard F1 Help

[!INCLUDE[sqlserver-ssis](../includes/applies-to-version/sqlserver-ssis.md)]


  Use the SSIS Package Upgrade Wizard to upgrade packages created by earlier versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to the package format for the current release of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)].  
  
 **To run the SSIS Package Upgrade Wizard**  
  
-   [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](../integration-services/install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md)  

## SSIS Upgrade Wizard
  
### Options  
 **Do not show this page again.**  
 Skip the Welcome page the next time that you open the wizard.  
 
## Select Source Location page
 Use the **Select Source Location** page to specify the source from which to upgrade packages.  
  
> [!NOTE]  
>  This page is only available when you run the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or at the command prompt.  
  
### Static Options  
 **Package source**  
 Select the storage location that contains the packages to be upgraded. This option has the values listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File System**|Indicates that the packages to be upgraded are in a folder on the local computer.<br /><br /> To have the wizard back up the original packages before upgrading those packages, the original packages must be stored in the file system. For more information, see How To Topic.|  
|**SSIS Package Store**|Indicates that the packages to be upgraded are in the package store. The package store consists of the set of file system folders that the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service manages. For more information, see [Package Management &#40;SSIS Service&#41;](../integration-services/service/package-management-ssis-service.md).<br /><br /> Selecting this value displays the corresponding **Package source** dynamic options.|  
|**Microsoft SQL Server**|Indicates the packages to be upgraded are from an existing instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].<br /><br /> Selecting this value displays the corresponding **Package source** dynamic options.|  
  
 **Folder**  
 Type the name of a folder that contains the packages you want to upgrade or click **Browse** and locate the folder.  
  
 **Browse**  
 Browse to locate the folder that contains the packages you want to upgrade.  
  
### Package Source Dynamic Options  
  
#### Package source = SSIS Package Store  
 **Server**  
 Type the name of the server that has the packages to be upgraded, or select this server in the list.  
  
#### Package source = Microsoft SQL Server  
 **Server**  
 Type the name of the server that has the packages to be upgraded, or select this server from the list.  
  
 **Use Windows authentication**  
 Select to use Windows Authentication to connect to the server.  
  
 **Use SQL Server authentication**  
 Select to use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication to connect to the server. If you use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, you must provide a user name and password.  
  
 **User name**  
 Type the user name that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication will use to connect to the server.  
  
 **Password**  
 Type the password that [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication will use to connect to the server.  
 
## Select Destination Location page
 Use the **Select Destination Location** page to specify the destination to which to save the upgraded packages.  
  
> [!NOTE]  
>  This page is only available when you run the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or at the command prompt.  
 
### Static Options  
 **Save to source location**  
 Save the upgraded packages to the same location as specified on the **Select Source Location** page of the wizard.  
  
 If the original packages are stored in the file system and you want the wizard to back up those packages, select the **Save to source location** option. For more information, see [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](../integration-services/install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md).  
  
 **Select new destination location**  
 Save the upgraded packages to the destination location that is specified on this page.  
  
 **Package source**  
 Specify where the upgrade packages are to be stored. This option has the values listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File System**|Indicates that the upgraded packages are to be saved to a folder on the local computer.|  
|**SSIS Package Store**|Indicates that the upgraded packages are to be saved to the Integration Services package store. The package store consists of the set of file system folders that the Integration Services service manages. For more information, see [Package Management &#40;SSIS Service&#41;](../integration-services/service/package-management-ssis-service.md).<br /><br /> Selecting this value displays the corresponding **Package source** dynamics options.|  
|**Microsoft SQL Server**|Indicates that the upgraded packages are to be saved to an existing instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].<br /><br /> Selecting this value displays the corresponding dynamic **Package source** dynamic options.|  
  
 **Folder**  
 Type the name of a folder to which the upgraded packages will be saved, or click **Browse** and locate the folder.  
  
 **Browse**  
 Browse to locate the folder to which the upgraded packages will be saved.  
  
### Package Source Dynamic Options  
  
#### Package source = SSIS Package Store  
 **Server**  
 Type the name of the server to which the upgrade packages will be saved, or select a server in the list.  
  
#### Package source = Microsoft SQL Server  
 **Server**  
 Type the name of the server to which the upgrade packages will be saved, or select this server in the list.  
  
 **Use Windows authentication**  
 Select to use Windows Authentication to connect to the server.  
  
 **Use SQL Server authentication**  
 Select to use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication to connect to the server. If you use [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, you must provide a user name and password.  
  
 **User name**  
 Type the user name to be used when using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication to connect to the server.  
  
 **Password**  
 Type the password to be used when using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication to connect to the server.  
 
## Select Package Management Options page
  Use the **Select Package Management Options** page to specify options for upgrading packages.  
  
 **To run the SSIS Package Upgrade Wizard**  
  
-   [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](../integration-services/install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md)  
  
### Options  
 **Update connection strings to use new provider names**  
 Update the connection strings to use the names for the following providers for the current release of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)]:  
  
-   OLE DB Provider for [!INCLUDE[ssASnoversion](../includes/ssasnoversion-md.md)]  
  
-   [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Native Client  
  
 The [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard updates only connection strings that are stored in connection managers. The wizard does not update connection strings that are constructed dynamically by using the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] expression language, or by using code in a Script task.  
  
 **Validate upgrade packages**  
 Validate the upgrade packages and save only those upgrade packages that pass validation.  
  
 If you do not select this option, the wizard will not validate upgrade packages. Therefore, the wizard will save all upgrade packages, regardless of whether the packages are valid or not. The wizard saves upgrade packages to the destination that is specified on the **SelectDestination Location** page of the wizard.  
  
 Validation adds time to the upgrade process. We recommend that you do not select this option for large packages that are likely to be upgraded successfully.  
  
 **Create new package IDs**  
 Create new package IDs for the upgrade packages.  
  
 **Continue upgrade process when a package upgrade fails**  
 Specify that when a package cannot be upgraded, the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard continues to upgrade the remaining packages.  
  
 **Package name conflicts**  
 Specify how the wizard should handle packages that have the same name. This option has the values listed in the following table.  
  
 **Overwrite existing package files**  
 Replaces the existing package with the upgrade package of the same name.  
  
 **Add numeric suffixes to upgrade package names**  
 Adds a numeric suffix to the name of the upgrade package.  
  
 **Do not upgrade packages**  
 Stops the packages from being upgraded and displays an error when you complete the wizard.  
  
 These options are not available when you select the **Save to source location** option on the **Select Destination Location** page of the wizard.  
  
 **Ignore Configurations**  
 Does not load package configurations during the package upgrade. Selecting this option reduces the time required to upgrade the package.  
  
 **Backup original packages**  
 Have the wizard back up the original packages to an **SSISBackupFolder** folder. The wizard creates the **SSISBackupFolder** folder as a subfolder to the folder that contains the original packages and the upgraded packages.  
  
> [!NOTE]  
>  This option is available only when you specify that the original packages and the upgraded packages are stored in the file system and in the same folder.  

## Select Packages page
  Use the **Select Packages** page to select the packages to upgrade. This page lists the packages that are stored in the location that was specified on the **Select Source Location** page of the wizard.  
  
### Options  
 **Existing package name**  
 Select one or more packages to upgrade.  
  
 **Upgrade package name**  
 Provide the destination package name, or use the default name that the wizard provides.  
  
> [!NOTE]  
>  You can also change the destination package name after upgrading the package. In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] or [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], open the upgraded package and change the package name.  
  
 **Password**  
 Specify the password that is used to decrypt the selected upgrade packages.  
  
 **Apply to selection**  
 Apply the specified password to decrypt the selected upgrade packages.  
 
## Complete the Wizard page
  Use the **Complete the Wizard** page to review and confirm the package upgrade options that you have selected. This is the last wizard page from which you can go back and change options for this session of the wizard.  
  
### Options  
 **Summary of options**  
 Review the upgrade options that you have selected in the wizard. To change any options, click **Back** to return to previous wizard pages.
 
## Upgrading the Packages page
  Use the **Upgrading the Packages** page to view the progress of package upgrade and to interrupt the upgrade process. The [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard upgrades the selected packages one by one.  
  
### Options  
 **Message pane**  
 Displays progress messages and summary information during the upgrade process.  
  
 **Action**  
 View the actions in the upgrade.  
  
 **Status**  
 View the result of each action.  
  
 **Message**  
 View the error messages that each action generates.  
  
 **Stop**  
 Stop the package upgrade.  
  
 **Report**  
 Select what you want to do with the report that contains the results of the package upgrade:  
  
-   View the report online.  
  
-   Save the report to a file.  
  
-   Copy the report to the Clipboard  
  
-   Send the report as an e-mail message.  

## View upgraded packages
### View upgraded packages that were saved to a SQL Server database or to the package store
  
In [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)], in Object Explorer, connect to the local instance of [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)], and then expand the **Stored Packages** node to see the packages that were upgraded.  
  
### View upgraded packages that were upgraded from SQL Server Data Tools  
  
In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], in Solution Explorer, open the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] project, and then expand the **SSIS Packages** node to see the upgraded packages.  
  
## See Also  
 [Upgrade Integration Services Packages](../integration-services/install-windows/upgrade-integration-services-packages.md)  
  
  
