---
title: "MSSQLSERVER_4064 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "4064 (Database Engine error)"
ms.assetid: 32112b90-0a2f-4834-a027-756811732be7
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_4064
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|4064|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DB_UFAIL_FATAL|  
|Message Text|Cannot open user default database. Login failed.|  
  
## Explanation  
 The SQL Server login was unable to connect because of a problem with its default database. Either the database itself is invalid or the login lacks CONNECT permission on the database.  
  
## User Action  
 Use ALTER LOGIN to change the login's default database. Grant CONNECT permission to the login.  
  
  
