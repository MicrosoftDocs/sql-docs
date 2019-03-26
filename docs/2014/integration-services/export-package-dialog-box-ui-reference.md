---
title: "Export Package Dialog Box UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.dtsserver.exportpackage.f1"
helpviewer_keywords: 
  - "Export Package dialog box"
ms.assetid: 3742fe8a-ef57-444d-b2fb-cb25d16bae97
author: janinezhang
ms.author: janinez
manager: craigg
---
# Export Package Dialog Box UI Reference
  Use the **Export Package** dialog box, available in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], to export a [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package to a different location and optionally, modify the protection level of the package.  
  
## Options  
 **Package location**  
 Select the type of storage to export the package to. The following options are available:  
  
 **SQL Server**  
  
 **File System**  
  
 **SSIS Package Storage**  
  
 **Server**  
 Type a server name or select a server from the list.  
  
 **Authentication**  
 Select Windows Authentication or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication. This option is available only if the storage location is [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  Whenever possible, use Windows Authentication.  
  
 **Authentication type**  
 Select an authentication type.  
  
 **User name**  
 If using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, provide a user name.  
  
 **Password**  
 If using [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Authentication, provide a password.  
  
 **Package path**  
 Type the package path, or click the browse button **(...)** and locate the folder in which to store the package.  
  
 **Protection level**  
 Click the browse button **(...)** and update the protection level in the **Package Protection Level** dialog box. For more information, see [Package and Project Protection Level Dialog Box](../../2014/integration-services/package-and-project-protection-level-dialog-box.md).  
  
## See Also  
 [Save Copy of Package](../../2014/integration-services/save-copy-of-package.md)   
 [Import Package Dialog Box UI Reference](../../2014/integration-services/import-package-dialog-box-ui-reference.md)   
 [Save Packages](save-packages.md)   
 [Import and Export Packages &#40;SSIS Service&#41;](../../2014/integration-services/import-and-export-packages-ssis-service.md)  
  
  
