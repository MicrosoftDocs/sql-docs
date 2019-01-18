---
title: "Import Package Dialog Box UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.dtsserver.importpackage.f1"
helpviewer_keywords: 
  - "Import Package dialog box"
ms.assetid: 0e5fb127-c7ff-4dfa-b90e-d9bcf0ce763b
author: douglaslms
ms.author: douglasl
manager: craigg
---
# Import Package Dialog Box UI Reference
  Use the **Import Package** dialog box, available in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], to import a [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] package and to set or modify the protection level of the package.  
  
## Options  
 **Package location**  
 Select the type of storage location to import the package to. The following options are available:  
  
 **SQL Server**  
  
 **File System**  
  
 **SSIS Package Store**  
  
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
 Type the package path, or click the browse button **(...)** and locate the package.  
  
 **Package name**  
 Optionally, rename the package. The default name is the name of the package to import.  
  
 **Protection level**  
 Click the browse button **(...)** and, in the **Package Protection Level** dialog box, update the protection level. For more information, see [Package and Project Protection Level Dialog Box](../../2014/integration-services/package-and-project-protection-level-dialog-box.md).  
  
## See Also  
 [Save Copy of Package](../../2014/integration-services/save-copy-of-package.md)   
 [Export Package Dialog Box UI Reference](../../2014/integration-services/export-package-dialog-box-ui-reference.md)   
 [Save Packages](save-packages.md)   
 [Import and Export Packages &#40;SSIS Service&#41;](../../2014/integration-services/import-and-export-packages-ssis-service.md)  
  
  
