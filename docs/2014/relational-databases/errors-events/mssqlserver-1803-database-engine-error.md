---
title: "MSSQLSERVER_1803 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "1803 (Database Engine error)"
ms.assetid: d4315390-82f1-4c4c-8d1b-1a4989537cca
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_1803
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|1803|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|NO_SPACE|  
|Message Text|CREATE DATABASE failed. Primary file must be at least %d MB to accommodate a copy of the model database.|  
  
## Explanation  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates a database by making a copy of the model database. Then [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] renames the copy, and enlarges the new database to the requested size. In this case, the user tried to create a database smaller than the model database. The operation failed because the copy of the model database could not fit on the primary data file, because the file was smaller than the model database.  
  
## User Action  
 Create the database by using a larger database file size. Then shrink the database if you want by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], or the DBCC SHRINKDATABASE statement.  
  
  
