---
title: "Save Packages | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
helpviewer_keywords: 
  - "Integration Services packages, saving"
  - "packages [Integration Services], saving"
  - "saving packages"
  - "SSIS packages, saving"
  - "SQL Server Integration Services packages, saving"
ms.assetid: 17c1de2c-637f-45c2-a148-79294bae0af4
author: janinezhang
ms.author: janinez
manager: craigg
---
# Save Packages
  In [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)] you build packages by using [!INCLUDE[ssIS](../includes/ssis-md.md)] Designer and save the packages to the file system as XML files (.dtsx files). You can also save copies of the package XML file to the msdb database in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or to the package store. The package store represents the folders in the file system location that the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service manages.  
  
 If you save a package to the file system, you can later use the [!INCLUDE[ssISnoversion](../includes/ssisnoversion-md.md)] service to import the package to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] or to the package store. For more information, see [Import and Export Packages &#40;SSIS Service&#41;](../../2014/integration-services/import-and-export-packages-ssis-service.md).  
  
 You can also use a command prompt utility, **dtutil**, to copy a package between the file system and msdb. For more information, see [dtutil Utility](dtutil-utility.md).  
  
### To save a package  
  
-   [Save a Package to the File System](../../2014/integration-services/save-a-package-to-the-file-system.md)  
  
-   [Save a Copy of a Package](../../2014/integration-services/save-a-copy-of-a-package.md)  
  
  
