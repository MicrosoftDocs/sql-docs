---
description: "sp_addtabletocontents (Transact-SQL)"
title: "sp_addtabletocontents (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_addtabletocontents_TSQL"
  - "sp_addtabletocontents"
helpviewer_keywords: 
  - "sp_addtabletocontents"
ms.assetid: 2ea27001-74f4-463e-bf1b-b6b5a86b9219
author: markingmyname
ms.author: maghan
---
# sp_addtabletocontents (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Inserts references into the merge tracking tables for any rows in a source table that are not currently included in the tracking tables. Use this option if you have bulk-loaded a large amount of data using **bcp**, which will not fire merge tracking triggers. This stored procedure is executed at the Publisher on the publication database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_addtabletocontents [ @table_name = ] 'table_name'  
    [ , [ @owner_name = ] 'owner_name' ]  
    [ , [ @filter_clause = ] 'filter_clause' ]  
```  
  
## Arguments  
`[ @table_name = ] 'table_name'`
 Is the name of the table. *table_name* is **sysname**, with no default.  
  
`[ @owner_name = ] 'owner_name'`
 Is the name of the owner of the table. *owner_name* is **sysname**, with a default of NULL.  
  
`[ @filter_clause = ] 'filter_clause'`
 Specifies a filter clause that controls which rows of the newly-loaded data should be added to the merge tracking tables. *filter_clause* is **nvarchar(4000)**, with a default value of NULL. If *filter_clause* is **null**, all bulk loaded rows are added.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 **sp_addtabletocontents** is used only in merge replication.  
  
 The rows in the *table_name* are referred to by their **rowguidcol** and the references are added to the merge tracking tables. **sp_addtabletocontents** should be used after bulk copying data into a table that is published using merge replication. The stored procedure initiates tracking of the rows that were copied and ensures that the new rows will be included in the next synchronization.  
  
## Permissions  
 Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute **sp_addtabletocontents**.  
  
## See Also  
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
