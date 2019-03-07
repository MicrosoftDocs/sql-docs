---
title: "Select Package Management Options (SSIS Package Upgrade Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.is.upgradewizard.selectpackagemgtoptions.f1"
ms.assetid: 810fc020-51cd-43ed-9f35-af99b4f35947
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Select Package Management Options (SSIS Package Upgrade Wizard)
  Use the **Select Package Management Options** page to specify options for upgrading packages.  
  
 **To run the SSIS Package Upgrade Wizard**  
  
-   [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md)  
  
## Options  
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
  
 For more information, see [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md).  
  
## See Also  
 [Upgrade Integration Services Packages](install-windows/upgrade-integration-services-packages.md)  
  
  
