---
title: "Import Package Dialog Box UI Reference | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.dtsserver.importpackage.f1"
helpviewer_keywords: 
  - "Import Package dialog box"
ms.assetid: 0e5fb127-c7ff-4dfa-b90e-d9bcf0ce763b
caps.latest.revision: 19
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Import Package Dialog Box UI Reference
  Use the **Import Package** dialog box, available in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], to import a [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] package and to set or modify the protection level of the package.  
  
## Options  
 **Package location**  
 Select the type of storage location to import the package to. The following options are available:  
  
 **SQL Server**  
  
 **File System**  
  
 **SSIS Package Store**  
  
 **Server**  
 Type a server name or select a server from the list.  
  
 **Authentication**  
 Select Windows Authentication or [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication. This option is available only if the storage location is [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  Whenever possible, use Windows Authentication.  
  
 **Authentication type**  
 Select an authentication type.  
  
 **User name**  
 If using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, provide a user name.  
  
 **Password**  
 If using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication, provide a password.  
  
 **Package path**  
 Type the package path, or click the browse button **(…)** and locate the package.  
  
 **Package name**  
 Optionally, rename the package. The default name is the name of the package to import.  
  
 **Protection level**  
 Click the browse button **(…)** and, in the **Package Protection Level** dialog box, update the protection level. For more information, see [Package and Project Protection Level Dialog Box](../../integration-services/packages/package-and-project-protection-level-dialog-box.md).  
  
## See Also  
 [Save Copy of Package](http://msdn.microsoft.com/library/7b44c0d7-d8fa-4491-8836-0899f621d3a8)   
 [Export Package Dialog Box UI Reference](../../integration-services/service/export-package-dialog-box-ui-reference.md)   
 [Save Packages](../../integration-services/save-packages.md)   
 [Import and Export Packages &#40;SSIS Service&#41;](../../integration-services/service/import-and-export-packages-ssis-service.md)  
  
  