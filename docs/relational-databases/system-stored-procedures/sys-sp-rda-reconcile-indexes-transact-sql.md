---
title: "sys.sp_rda_reconcile_indexes (Transact-SQL) | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/10/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "dbe-stretch"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "sp_rda_reconcile_indexes"
  - "sp_rda_reconcile_indexes_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_rda_reconcile_indexes stored procedure"
ms.assetid: 96b31ab9-bf84-46d6-9990-81f5c51f885a
caps.latest.revision: 9
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# sys.sp_rda_reconcile_indexes (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Queues a schema task to reconcile indexes on the remote table. After this task finishes successfully, the remote table has the same indexes that exist on the local Stretch-enabled table.  
  
 If there is another task queued to reconcile indexes when you call **sp_rda_reconcile_indexes**, this stored procedure does not queue a duplicate task.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
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
  
  
