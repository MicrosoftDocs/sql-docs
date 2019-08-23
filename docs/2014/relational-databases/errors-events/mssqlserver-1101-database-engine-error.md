---
title: "MSSQLSERVER_1101 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "1101 (Database Engine error)"
ms.assetid: d63b67d5-59f5-4f77-904e-5ba67f2dd850
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1101
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1101|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NOALLOCPG|  
|Message Text|Could not allocate a new page for database '%.*ls' because of insufficient disk space in filegroup '%.\*ls'. Create the necessary space by dropping objects in the filegroup, adding additional files to the filegroup, or setting autogrowth on for existing files in the filegroup.|  
  
## Explanation  
 No disk space available in a filegroup.  
  
## User Action  
 The following actions may make space available in the filegroup.  
  
-   Turn on AUTOGROW.  
  
-   Add more files to the file group.  
  
-   Free up disk space by dropping unnecessary indexes or tables in the filegroup.  
  
  
