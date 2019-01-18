---
title: "sys.sp_rda_set_query_mode (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: stored-procedures
ms.topic: "language-reference"
f1_keywords: 
  - "sys.sp_rda_set_query_mode"
  - "sys.sp_rda_set_query_mode_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.sp_rda_set_query_mode stored procedure"
ms.assetid: 65a0b390-cf87-4db7-972a-1fdf13456c88
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# sys.sp_rda_set_query_mode (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Specifies whether queries against the current Stretch-enabled database and its tables return both local and remote data (the default), or local data only.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_rda_set_query_mode [ @mode = ] @mode   
    [ , [ @force = ]  @force ]  
```  
  
## Arguments  
 [ @mode = ] *@mode*  
 Is one of the following values.  
  
-   **DISABLED** All queries against Stretch-enabled tables fail.  
  
-   **LOCAL_ONLY** Queries against Stretch-enabled tables return local data only.  
  
-   **LOCAL_AND_REMOTE** Queries against Stretch-enabled tables return both local and remote data. This is the default behavior.  
  
 [ @force = ]  *@force*  
 Is an optional bit value that you can set to 1 if you want to change query mode without validation.  
  
## Return Code Values  
 0 (success) or >0 (failure)  
  
## Permissions  
 Requires db_owner permissions.  
  
## Remarks  
 The following extended stored procedures also set the query mode for a Stretch-enabled database.  
  
-   **sp_rda_deauthorize_db**  
  
     After you run **sp_rda_deauthorize_db** , all queries against Stretch-enabled databases and tables fail. That is, the query mode is set to DISABLED. To exit this mode, do one of the following things.  
  
    -   Run [sys.sp_rda_reauthorize_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect to the remote Azure database. This operation automatically resets the query mode to LOCAL_AND_REMOTE, which is the default behavior for Stretch Database. That is, queries return results from both local and remote data.  
  
    -   Run [sys.sp_rda_set_query_mode](../../relational-databases/system-stored-procedures/sys-sp-rda-set-query-mode-transact-sql.md) with the LOCAL_ONLY argument to let queries continue to run against local data only.  
  
-   **sp_rda_reauthorize_db**  
  
     When you run [sys.sp_rda_reauthorize_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect to the remote Azure database, this operation automatically resets the query mode to LOCAL_AND_REMOTE, which is the default behavior for Stretch Database. That is, queries return results from both local and remote data.  
  
## See Also  
 [sys.sp_rda_deauthorize_db &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sys-sp-rda-deauthorize-db-transact-sql.md)   
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
  
  
