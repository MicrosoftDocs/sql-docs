---
description: "MSSQLSERVER_17065"
title: "MSSQLSERVER_17065 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "17065 (Database Engine error)"
ms.assetid: 63c2ba5a-be34-461e-bee1-03c25b396cd2
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_17065
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|17065|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLASSERT_BOTH|  
|Message Text|SQL Server Assertion: File: \<%s>, line = %d Failed Assertion = '%s' %s. This error may be timing-related. If the error persists after rerunning the statement, use DBCC CHECKDB to check the database for structural integrity, or restart the server to ensure in-memory data structures are not corrupted.|  
  
## Explanation  
This error can be caused by transient, timing-related errors, or by in-memory or on-disk data corruption.  
  
## User Action  
Rerun the statement that caused the exception to fire. If the error was caused by a timing-related event, the error may not recur. If the problem persists, run DBCC CHECKDB to check for on-disk corruption. Restart the server to ensure the in-memory data structures are not corrupted.  
  
## See Also  
[DBCC CHECKDB &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checkdb-transact-sql.md)  
  
