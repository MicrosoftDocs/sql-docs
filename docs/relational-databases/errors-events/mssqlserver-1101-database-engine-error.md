---
title: "MSSQLSERVER_1101 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
helpviewer_keywords: 
  - "1101 (Database Engine error)"
ms.assetid: d63b67d5-59f5-4f77-904e-5ba67f2dd850
caps.latest.revision: 18
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
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
  
