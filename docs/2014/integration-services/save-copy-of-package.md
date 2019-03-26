---
title: "Save Copy of Package | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.savecopyas.f1"
helpviewer_keywords: 
  - "Save Copy of Package dialog box"
ms.assetid: 7b44c0d7-d8fa-4491-8836-0899f621d3a8
author: janinezhang
ms.author: janinez
manager: craigg
---
# Save Copy of Package
  Use the **Save Copy of Package** dialog box, available in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], to save a copy of an [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package from [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] to a different location and, optionally, modify the protection level of the package.  
  
## Options  
 **Package location**  
 Select the type of storage location in which to save the package copy. The following options are available:  
  
 **SQL Server**  
  
 **File System**  
  
 **SSIS Package Store**  
  
 **Server**  
 Type a server name or select a server from the list. This option is available only if the storage location is **SQL Server** or **SSIS Package Store**.  
  
 **Authentication**  
 Select Windows Authentication or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. This option is available only if the storage location is [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  Whenever possible use Windows Authentication.  
  
 **Authentication type**  
 Select an authentication type.  
  
 **User name**  
 If using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, provide a user name.  
  
 **Password**  
 If using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, provide a password.  
  
 **Package path**  
 Type the package path, or click the browse **(...)** button and locate the folder in which to store the package.  
  
 **Protection level**  
 Click the browse **(...)** button and update the protection level in the **Package Protection Level** dialog box. For more information, see [Package and Project Protection Level Dialog Box](../../2014/integration-services/package-and-project-protection-level-dialog-box.md).  
  
## See Also  
 [Import Package Dialog Box UI Reference](../../2014/integration-services/import-package-dialog-box-ui-reference.md)   
 [Export Package Dialog Box UI Reference](../../2014/integration-services/export-package-dialog-box-ui-reference.md)   
 [Save Packages](save-packages.md)   
 [Import and Export Packages &#40;SSIS Service&#41;](../../2014/integration-services/import-and-export-packages-ssis-service.md)  
  
  
