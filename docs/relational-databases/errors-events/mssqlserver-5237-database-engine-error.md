---
title: "MSSQLSERVER_5237"
description: "MSSQLSERVER_5237"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "5237 (Database Engine error)"
---
# MSSQLSERVER_5237
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|5237|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC4_INDEXED_VIEW_CHECK_QUERY_FAILED_NO_ERRORCODE|  
|Message Text|DBCC cross-rowset check failed for object 'NAME' (object ID O_ID) due to an internal query error.|  
  
## Explanation  
An internal error resulted in DBCC not being able to execute the query to check indexed views.  
  
## User Action  
Rerun the DBCC command.  
  
