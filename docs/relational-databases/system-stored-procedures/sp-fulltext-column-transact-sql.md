---
title: "sp_fulltext_column (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_fulltext_column_TSQL"
  - "sp_fulltext_column"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_fulltext_column"
ms.assetid: a84cc45d-1b50-44af-85df-2ea033b8a6a9
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_fulltext_column (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-xxx-md.md)]

  Specifies whether or not a particular column of a table participates in full-text indexing.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use [ALTER FULLTEXT INDEX](../../t-sql/statements/alter-fulltext-index-transact-sql.md) instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_fulltext_column [ @tabname= ] 'qualified_table_name' ,   
     [ @colname= ] 'column_name' ,   
     [ @action= ] 'action'   
     [ , [ @language= ] 'language_term' ]   
     [ , [ @type_colname= ] 'type_column_name' ]  
```  
  
## Arguments  
`[ @tabname = ] 'qualified_table_name'`
 Is a one- or two-part table name. The table must exist in the current database. The table must have a full-text index. *qualified_table_name* is **nvarchar(517)**, with no default value.  
  
`[ @colname = ] 'column_name'`
 Is the name of a column in *qualified_table_name*. The column must be either a character, **varbinary(max)** or **image** column and cannot be a computed column. *column_name* is **sysname**, with no default.  
  
> [!NOTE]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can create full-text indexes of text data stored in columns that are of **varbinary(max)** or **image** data type. Images and pictures are not indexed.  
  
`[ @action = ] 'action'`
 Is the action to be performed. *action* is **varchar(20)**, with no default value, and can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|**add**|Adds *column_name* of *qualified_table_name* to the table's inactive full-text index. This action enables the column for full-text indexing.|  
|**drop**|Removes *column_name* of *qualified_table_name* from the table's inactive full-text index.|  
  
`[ @language = ] 'language_term'`
 Is the language of the data stored in the column. For a list of languages included in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], see [sys.fulltext_languages &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-fulltext-languages-transact-sql.md).  
  
> [!NOTE]  
>  Use 'Neutral' when a column contains data in multiple languages or in an unsupported language. The default is specified by the configuration option 'default full-text language'.  
  
`[ @type_colname = ] 'type_column_name'`
 Is the name of a column in *qualified_table_name* that holds the document type of *column_name*. This column must be **char**, **nchar**, **varchar**, or **nvarchar**. It is only used when the data type of *column_name* is of type **varbinary(max)** or **image**. *type_column_name* is **sysname**, with no default.  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Result Sets  
 None  
  
## Remarks  
 If the full-text index is active, any ongoing population is stopped. Furthermore, if a table with an active full-text index has change tracking enabled, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ensures that the index is current. For example, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stops any current population on the table, drops the existing index, and starts a new population.  
  
 If change tracking is on and columns need to be added or dropped from the full-text index while preserving the index, the table should be deactivated, and the required columns should be added or dropped. These actions freeze the index. The table can be activated later when starting a population is practical.  
  
## Permissions  
 User must be a member of the **db_ddladmin** fixed database role, or a member of the **db_owner** fixed database role, or the owner of the table.  
  
## Examples  
 The following example adds the `DocumentSummary` column from the `Document` table to the full-text index of the table.  
  
```  
USE AdventureWorks2012;  
GO  
EXEC sp_fulltext_column 'Production.Document', DocumentSummary, 'add';  
GO  
```  
  
 The following example assumes you created a full-text index on a table named `spanishTbl`. To add the `spanishCol` column to the full-text index, execute the following stored procedure:  
  
```  
EXEC sp_fulltext_column 'spanishTbl', 'spanishCol', 'add', 0xC0A;  
GO  
```  
  
 When you run this query:  
  
```  
SELECT *   
FROM spanishTbl   
WHERE CONTAINS(spanishCol, 'formsof(inflectional, trabajar)')  
```  
  
 The result set would include rows with different forms of `trabajar` (to work), such as `trabajo`, `trabajamos`, and `trabajan`.  
  
> [!NOTE]  
>  All columns listed in a single full-text query function clause must use the same language.  
  
## See Also  
 [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md)   
 [sp_help_fulltext_columns &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-columns-transact-sql.md)   
 [sp_help_fulltext_columns_cursor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-columns-cursor-transact-sql.md)   
 [sp_help_fulltext_tables &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-tables-transact-sql.md)   
 [sp_help_fulltext_tables_cursor &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-help-fulltext-tables-cursor-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Full-Text Search and Semantic Search Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/full-text-search-and-semantic-search-stored-procedures-transact-sql.md)  
  
  
