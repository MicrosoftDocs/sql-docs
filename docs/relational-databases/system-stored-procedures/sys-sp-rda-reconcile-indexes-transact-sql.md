---
title: "sys.sp_rda_reconcile_indexes (Transact-SQL)"
description: Learn about sys.sp_rda_reconcile_indexes. See how to use this Transact-SQL stored procedure to queue a schema task to reconcile indexes on a remote table.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sp_rda_reconcile_indexes"
  - "sp_rda_reconcile_indexes_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_reconcile_indexes stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_reconcile_indexes (Transact-SQL)
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Queues a schema task to reconcile indexes on the remote table. After this task finishes successfully, the remote table has the same indexes that exist on the local Stretch-enabled table.  

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]
  
 If there is another task queued to reconcile indexes when you call **sp_rda_reconcile_indexes**, this stored procedure does not queue a duplicate task.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_rda_reconcile_indexes [@objname = ] 'objname'  
  
```  
  
## Arguments  
 [@objname = ] *'objname'*  
 Is the qualified or non-qualified name of the Stretch-enabled table for which you want to reconcile indexes. Quotes are required only if you specify a qualified object.  
  
## Return Code Values  
 0 (success) or >0 (failure)  
  
## See Also  
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
  
  
