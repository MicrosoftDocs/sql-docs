---
description: "sp_dbmmonitorhelpmonitoring (Transact-SQL)"
title: "sp_dbmmonitorhelpmonitoring (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords: 
  - "sp_dbmmonitorhelpmonitoring"
  - "sp_dbmmonitorhelpmonitoring_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_dbmmonitorhelpmonitoring"
  - "database mirroring [SQL Server], monitoring"
ms.assetid: a085cf87-269f-454a-a146-21f80a113b72
author: markingmyname
ms.author: maghan
---
# sp_dbmmonitorhelpmonitoring (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the current update period.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
