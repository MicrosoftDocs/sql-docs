---
description: "MSSQLSERVER_3056"
title: "MSSQLSERVER_3056 | Microsoft Docs"
ms.custom: ""
ms.date: "08/24/2022"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3056 (Database Engine error)"
author: Pijocoder
ms.author: mathoma
---
# MSSQLSERVER_3056
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3056|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DMPDB_INVALID_FSDATA|  
|Message Text|The backup operation has detected an unexpected file in a FILESTREAM container. The backup operation will continue and include file '%ls'.|  
  
## Explanation

Error 3056 is raised if files exist under the FILESTREAM container (folder) that aren't created by SQL Server. The backup operation will include that file, but this will cause an Inconsistent state of the filestream components of the database. 

## User Action  

The error message includes the name of the unexpected file. Please, investigate how this file ended up in this folder.


### Run DBCC CHECKDB

If you run DBCC CHECKDB it may report error [7908](mssqlserver-7908-database-engine-error.md) or [7906](mssqlserver-7906-database-engine-error.md), but can't repair it.

### Restore from Backup

If the problem isn't hardware related and a known clean backup is available, restore the database from the backup.  