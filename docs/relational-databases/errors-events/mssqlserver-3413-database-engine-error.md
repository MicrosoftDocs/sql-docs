---
title: "MSSQLSERVER_3413"
description: "MSSQLSERVER_3413"
author: MashaMSFT
ms.author: mathoma
ms.date: "04/04/2017"
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "3413 (Database Engine error)"
---
# MSSQLSERVER_3413
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3413|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|MARKDB|  
|Message Text|Database ID %d. Could not mark database as suspect. Getnext NC scan on sys.databases.database_id failed. Refer to previous errors in the error log to identify the cause and correct any associated problems.|  
  
## Explanation  
There was an unexpected failure while trying to mark a user database as SUSPECT in the catalog. The SUSPECT state will not be persisted.  
  
## User Action  
See previous errors and correct the problem.  
  
