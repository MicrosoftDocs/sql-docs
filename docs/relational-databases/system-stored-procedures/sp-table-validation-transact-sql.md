---
description: "sp_table_validation (Transact-SQL)"
title: "sp_table_validation (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/08/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: replication
ms.topic: "reference"
dev_langs: 
  - "TSQL"
f1_keywords: 
  - "sp_table_validation_TSQL"
  - "sp_table_validation"
helpviewer_keywords: 
  - "sp_table_validation"
ms.assetid: 31b25f9b-9b62-496e-a97e-441d5fd6e767
author: markingmyname
ms.author: maghan
---
# sp_table_validation (Transact-SQL)
[!INCLUDE[sqlserver](../../includes/applies-to-version/sqlserver.md)]

  Either returns rowcount or checksum information on a table or indexed view, or compares the provided rowcount or checksum information with the specified table or indexed view. This stored procedure is executed at the Publisher on the publication database and at the Subscriber on the subscription database. *Not supported for Oracle Publishers*.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_table_validation [ @table = ] 'table'  
    [ , [ @expected_rowcount = ] type_of_check_requested OUTPUT]  
    [ , [ @expected_checksum = ] expected_checksum OUTPUT]  
    [ , [ @rowcount_only = ] rowcount_only ]  
    [ , [ @owner = ] 'owner' ]  
    [ , [ @full_or_fast = ] full_or_fast ]  
    [ , [ @shutdown_agent = ] shutdown_agent ]  
    [ , [ @table_name = ] table_name ]  
    [ , [ @column_list = ] 'column_list' ]  
```  
  
## Arguments  
`[ @table = ] 'table'`
 Is the name of the table. *table* is **sysname**, with no default.  
  
`[ @expected_rowcount = ] expected_rowcountOUTPUT`
 Specifies whether to return the expected number of rows in the table. *expected_rowcount* is **int**, with a default of NULL. If NULL, the actual rowcount is returned as an output parameter. If a value is provided, that value is checked against the actual rowcount to identify any differences.  
  
`[ @expected_checksum = ] expected_checksumOUTPUT`
 Specifies whether to return the expected checksum for the table. *expected_checksum* is **numeric**, with a default of NULL. If NULL, the actual checksum is returned as an output parameter. If a value is provided, that value is checked against the actual checksum to identify any differences.  
  
`[ @rowcount_only = ] type_of_check_requested`
 Specifies what type of checksum or rowcount to perform. *type_of_check_requested* is **smallint**, with a default of **1**.  
  
 If **0**, perform a rowcount and a [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] 7.0-compatible checksum.  
  
 If **1**, perform a rowcount check only.  
  
 If **2**, perform a rowcount and binary checksum.  
  
`[ @owner = ] 'owner'`
 Is the name of the owner of the table. *owner* is **sysname**, with a default of NULL.  
  
`[ @full_or_fast = ] full_or_fast`
 Is the method used to calculate the rowcount. *full_or_fast* is **tinyint**, with a default of **2**, and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**0**|Does full count using COUNT(*).|  
|**1**|Does fast count from **sysindexes.rows**. Counting rows in **sysindexes** is much faster than counting rows in the actual table. However, because **sysindexes** is lazily updated, the rowcount may not be accurate.|  
|**2** (default)|Does conditional fast counting by first trying the fast method. If fast method shows differences, reverts to full method. If *expected_rowcount* is NULL and the stored procedure is being used to get the value, a full COUNT(*) is always used.|  
  
`[ @shutdown_agent = ] shutdown_agent`
 If the Distribution Agent is executing **sp_table_validation**, specifies whether the Distribution Agent should shut down immediately upon completion of the validation. *shutdown_agent* is **bit**, with a default of **0**. If **0**, the replication agent does not shut down. If **1**, error 20578 is raised and the replication agent is signaled to shut down. This parameter is ignored when **sp_table_validation** is executed directly by a user.  
  
`[ @table_name = ] table_name`
 Is the table name of the view used for output messages. *table_name* is **sysname**, with a default of **\@table**.  
  
`[ @column_list = ] 'column_list'`
 Is the list of columns that should be used in the checksum function. *column_list* is **nvarchar(4000)**, with a default of NULL. Enables validation of merge articles to specify a column list that excludes computed and timestamp columns.  
  
## Return Code Values  
 If performing a checksum validation and the expected checksum equals the checksum in the table, **sp_table_validation** returns a message that the table passed checksum validation. Otherwise, it returns a message that the table may be out of synchronization and reports the difference between the expected and the actual number of rows.  
  
 If performing a rowcount validation and the expected number of rows equals the number in the table, **sp_table_validation** returns a message that the table passed rowcount validation. Otherwise, it returns a message that the table may be out of synchronization and reports the difference between the expected and the actual number of rows.  
  
## Remarks  
 **sp_table_validation** is used in all types of replication. **sp_table_validation** is not supported for Oracle Publishers.  
  
 Checksum computes a 32-bit cyclic redundancy check (CRC) on the entire row image on the page. It does not selectively check columns and cannot operate on a view or vertical partition of the table. Also, the checksum skips the contents of **text** and **image** columns (by design).  
  
 When doing a checksum, the structure of the table must be identical between the two servers; that is, the tables must have the same columns existing in the same order, same data types and lengths, and same NULL/NOT NULL conditions. For example, if the Publisher did a CREATE TABLE, then an ALTER TABLE to add columns, but the script applied at the Subscriber is a simple CREATE table, the structure is NOT the same. If you are not certain that the structure of the two tables is identical, look at [syscolumns](../../relational-databases/system-compatibility-views/sys-syscolumns-transact-sql.md) and confirm that the offset in each table is the same.  
  
 Floating point values are likely to generate checksum differences if character-mode **bcp** was used, which is the case if the publication has non- [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] subscribers. These are due to minor and unavoidable differences in precision when doing conversion to and from character mode.  
  
## Permissions  
 To execute **sp_table_validation**, you must have SELECT permissions on the table being validated.  
  
## See Also  
 [CHECKSUM &#40;Transact-SQL&#41;](../../t-sql/functions/checksum-transact-sql.md)   
 [@@ROWCOUNT &#40;Transact-SQL&#41;](../../t-sql/functions/rowcount-transact-sql.md)   
 [sp_article_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-article-validation-transact-sql.md)   
 [sp_publication_validation &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-publication-validation-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
