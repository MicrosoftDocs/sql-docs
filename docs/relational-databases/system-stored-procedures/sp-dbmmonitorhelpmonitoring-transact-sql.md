---
title: "sp_dbmmonitorhelpmonitoring (Transact-SQL)"
description: "sp_dbmmonitorhelpmonitoring (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "03/03/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dbmmonitorhelpmonitoring"
  - "sp_dbmmonitorhelpmonitoring_TSQL"
helpviewer_keywords:
  - "sp_dbmmonitorhelpmonitoring"
  - "database mirroring [SQL Server], monitoring"
dev_langs:
  - "TSQL"
---
# sp_dbmmonitorhelpmonitoring (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the current update period.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dbmmonitorhelpmonitoring   
```  
  
## Arguments  
 None  
  
## Return Code Values  
 None  
  
## Result Sets  
 Returns the current update period, that is, the number of minutes between updates of database mirroring status table. This value ranges from 1 to 120 minutes.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example returns the current update period.  
  
```  
EXEC sp_dbmmonitorhelpmonitoring;  
```  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [sp_dbmmonitorresults &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql.md)  
  
  
