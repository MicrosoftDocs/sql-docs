---
title: "sp_dbmmonitoraddmonitoring (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_dbmmonitoraddmonitoring"
  - "sp_dbmmonitoraddmonitoring_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "database mirroring [SQL Server], monitoring"
  - "sp_dbmmonitoraddmonitoring"
ms.assetid: 9489dc30-af29-4363-a172-4645947fc95e
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_dbmmonitoraddmonitoring (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Creates a database mirroring monitor job that periodically updates the mirroring status for every mirrored database on the server instance.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_dbmmonitoraddmonitoring [ update_period ]  
```  
  
## Arguments  
 *update_period*  
 Specifies the interval between updates in minutes. This value can be from 1 to 120 minutes. The default is 1 minute.  
  
> [!NOTE]  
>  If update period is set too low, the response time might increase for clients.  
  
## Return Code Values  
 None  
  
## Result Sets  
 None  
  
## Remarks  
 This procedure requires that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent is allowed to run on the server instance, and for the database mirroring monitor job to run, Agent must be running.  
  
 If database mirroring is started from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], the **sp_dbmmonitoraddmonitoring** procedure is run automatically. If you start mirroring up manually using ALTER DATABASE statements, to monitor mirrored database on the server instance, you must run **sp_dbmmonitoraddmonitoring** manually.  
  
> [!NOTE]  
>  If you run **sp_dbmmonitoraddmonitoring** before you set up database mirroring, the monitoring job will run but will not update the status table in which database mirroring monitor history is stored.  
  
## Permissions  
 Requires membership in the **sysadmin** fixed server role.  
  
## Examples  
 The following example starts monitoring with an update period of `3` minutes.  
  
```  
EXEC sp_dbmmonitoraddmonitoring 3;  
```  
  
## See Also  
 [Monitoring Database Mirroring &#40;SQL Server&#41;](../../database-engine/database-mirroring/monitoring-database-mirroring-sql-server.md)   
 [sp_dbmmonitorchangemonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorchangemonitoring-transact-sql.md)   
 [sp_dbmmonitordropmonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitordropmonitoring-transact-sql.md)   
 [sp_dbmmonitorhelpmonitoring &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorhelpmonitoring-transact-sql.md)   
 [sp_dbmmonitorresults &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dbmmonitorresults-transact-sql.md)  
  
  
