---
title: "Discontinued Management Tools Features in SQL Server 2014 | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: 6e58acd0-73c5-4161-9fbc-8ea531bc681c
author: stevestein
ms.author: sstein
manager: craigg
---
# Discontinued Management Tools Features in SQL Server 2014
  This topic describes [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Management Tools features that are no longer available in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)].  
  
## Features Removed in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]  
 None  
  
## Features Removed in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]  
  
### SQL Server Compact Edition  
 The SQL Server Compact Edition code editor has been removed from [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)]. Support for SQL Server Compact Edition has also been removed from Object Explorer, Solution Explorer, and Template Explorer. Use the Transact-SQL editors in Microsoft Visual Studio 2010 Service Pack 1 or Webmatrix instead.  
  
### ActiveX Subsystem for SQL Server Agent  
 The ActiveX subsystem for SQL Server Agent has been removed in this release. There is no replacement functionality.  
  
### sp_addtask, sp_deletetask, sp_updatetask  
 Sp_addtask, sp_deletetask, and sp_updatetask have been removed in this release. Do not use this functionality in new or updated applications.  
  
### Net Send and Pager Notification  
 Net Send and Pager Notification have been removed in this release. Do not use this functionality in new or updated applications.  
  
### Data-tier Applications  
 Some data-tier application (DAC) features present in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] have been removed in [!INCLUDE[ssSQL11](../includes/sssql11-md.md)]. However, the Data-Tier Application Framework (DACfx version 3.0) released with [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] is compatible with [!INCLUDE[ssVersion2005](../includes/ssversion2005-md.md)] through [!INCLUDE[ssSQL11](../includes/sssql11-md.md)] and [!INCLUDE[ssSDSfull](../includes/sssdsfull-md.md)]. DAC version 3.0 is not supported by earlier versions of [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] including [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] in [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)]. Visual Studio 2010 Database Projects do not support DAC 3.0 DACPAC packages or DAC Export (BACPAC) packages generated with DACfx version 3.0 or later.  
  
 Microsoft recommends using the latest available version [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools Database Projects.  
  
 DACfx 3.0 API and [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools do support reading DACPAC and BACPAC files created using earlier [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] tools and DACfx versions: extracting databases into DACPAC files from these versions, and deploying databases into supported versions of [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] through [!INCLUDE[ssManStudio](../includes/ssmanstudio-md.md)] or [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Data Tools.  
  
## See Also  
 [Backward Compatibility](../../2014/getting-started/backward-compatibility.md)  
  
  
