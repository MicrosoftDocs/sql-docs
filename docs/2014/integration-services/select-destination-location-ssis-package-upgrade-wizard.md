---
title: "Select Destination Location (SSIS Package Upgrade Wizard) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.is.upgradewizard.selectdestinationlocation.f1"
ms.assetid: 89274a71-0ffe-4889-84df-f5a7d78459ef
author: janinezhang
ms.author: janinez
manager: craigg
---
# Select Destination Location (SSIS Package Upgrade Wizard)
  Use the **Select Destination Location** page to specify the destination to which to save the upgraded packages.  
  
> [!NOTE]  
>  This page is only available when you run the [!INCLUDE[ssIS](../includes/ssis-md.md)] Package Upgrade Wizard from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] or at the command prompt.  
  
 **To run the SSIS Package Upgrade Wizard**  
  
-   [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md)  
  
## Static Options  
 **Save to source location**  
 Save the upgraded packages to the same location as specified on the **Select Source Location** page of the wizard.  
  
 If the original packages are stored in the file system and you want the wizard to back up those packages, select the **Save to source location** option. For more information, see [Upgrade Integration Services Packages Using the SSIS Package Upgrade Wizard](install-windows/upgrade-integration-services-packages-using-the-ssis-package-upgrade-wizard.md).  
  
 **Select new destination location**  
 Save the upgraded packages to the destination location that is specified on this page.  
  
 **Package source**  
 Specify where the upgrade packages are to be stored. This option has the values listed in the following table.  
  
|Value|Description|  
|-----------|-----------------|  
|**File System**|Indicates that the upgraded packages are to be saved to a folder on the local computer.|  
|**SSIS Package Store**|Indicates that the upgraded packages are to be saved to the Integration Services package store. The package store consists of the set of file system folders that the Integration Services service manages. For more information, see [Package Management &#40;SSIS Service&#41;](service/package-management-ssis-service.md).<br /><br /> Selecting this value displays the corresponding **Package source** dynamics options.|  
|**Microsoft SQL Server**|Indicates that the upgraded packages are to be saved to an existing instance of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].<br /><br /> Selecting this value displays the corresponding dynamic **Package source** dynamic options.|  
  
 **Folder**  
 Type the name of a folder to which the upgraded packages will be saved, or click **Browse** and locate the folder.  
  
 **Browse**  
 Browse to locate the folder to which the upgraded packages will be saved.  
  
## Package Source Dynamic Options  
  
### Package source = SSIS Package Store  
 **Server**  
 Type the name of the server to which the upgrade packages will be saved, or select a server in the list.  
  
### Package source = Microsoft SQL Server  
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
  
## See Also  
 [Upgrade Integration Services Packages](install-windows/upgrade-integration-services-packages.md)  
  
  
