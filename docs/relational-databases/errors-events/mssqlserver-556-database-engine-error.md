---
description: "MSSQLSERVER_556"
title: "MSSQLSERVER_556 | Microsoft Docs"
ms.custom: ""
ms.date: "09/15/2021"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "556 (Database Engine error)"
author: MikeRayMSFT
ms.author: mikeray
---
# MSSQLSERVER_556
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|556|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|WRONGPAGE|  
|Message Text|INSERT EXEC failed because the stored procedure altered the schema of the target table.|  
  
## Explanation  

Query store is on.

The plan is removed after the query store fills up.

## User action

Increase the size of the query store.

Clear the procedure cache when query store comes back from READ WRITE state

Recompile just the INSERT EXEC statement to force it to go through the regular compile path and avoid hitting this exception due to unplanned recompilation caused by plan missing in query store.

For more information, see our [troubleshooting article on error 556](/troubleshoot/sql/database-design/error-556-insert-exec-failed).
  
## See also

[DBCC CHECKTABLE &#40;Transact-SQL&#41;](~/t-sql/database-console-commands/dbcc-checktable-transact-sql.md)