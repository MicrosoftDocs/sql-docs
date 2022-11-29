---
description: "WRITETEXT (Transact-SQL)"
title: "WRITETEXT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/23/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "WRITETEXT_TSQL"
  - "WRITETEXT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "replacing data"
  - "WRITETEXT statement"
  - "updating data [SQL Server]"
  - "nonlogged updating"
  - "minimally logged updating [SQL Server]"
  - "overwriting data"
  - "data updates [SQL Server], WRITETEXT statement"
ms.assetid: 80c252fd-a8b8-4a2e-888a-059081ed4109
author: VanMSFT
ms.author: vanto
---
# WRITETEXT (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Permits minimally logged, interactive updating of an existing **text**, **ntext**, or **image** column. WRITETEXT overwrites any existing data in the column it affects. WRITETEXT cannot be used on **text**, **ntext**, and **image** columns in views.  
  
> [!IMPORTANT]
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the large-value data types and the **.WRITE** clause of the [UPDATE](../../t-sql/queries/update-transact-sql.md) statement instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
WRITETEXT [BULK]  
  { table.column text_ptr }  
  [ WITH LOG ] { data }  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 BULK  
 Enables upload tools to upload a binary data stream. The stream must be provided by the tool at the TDS protocol level. When the data stream is not present the query processor ignores the BULK option.  
  
> [!IMPORTANT]  
>  We recommend that the BULK option not be used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-based applications. This option might be changed or removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 *table* **.column**  
 Is the name of the table and **text**, **ntext**, or **image** column to update. Table and column names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the database name and owner names is optional.  
  
 *text_ptr*  
 Is a value that stores the pointer to the **text**, **ntext**, or **image** data. *text_ptr* must be **binary(16)**.To create a text pointer, execute an [INSERT](../../t-sql/statements/insert-transact-sql.md) or [UPDATE](../../t-sql/queries/update-transact-sql.md) statement with data that is not null for the **text**, **ntext**, or **image** column.  
  
 WITH LOG  
 Ignored by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Logging is determined by the recovery model in effect for the database.  
  
 *data*  
 Is the actual **text**, **ntext** or **image** data to store. *data* can be a literal or a parameter. The maximum length of text that can be inserted interactively with WRITETEXT is approximately 120 KB for **text**, **ntext**, and **image** data.  
  
## Remarks  
 Use WRITETEXT to replace **text**, **ntext**, and **image** data and UPDATETEXT to modify **text**, **ntext**, and **image** data. UPDATETEXT is more flexible because it changes only a part of a **text**, **ntext**, or **image** column instead of the whole column.  
  
 For best performance we recommend that **text**, **ntext**, and **image** data be inserted or updated in chunk sizes that are multiples of 8040 bytes.  
  
 If the database recovery model is simple or bulk-logged, **text**, **ntext**, and **image** operations that use WRITETEXT are minimally logged operations when new data is inserted or appended.  
  
> [!NOTE]  
>  Minimal logging is not used when existing values are updated.  
  
 For WRITETEXT to work correctly, the column must already contain a valid text pointer.  
  
 If the table does not have in row text, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] saves space by not initializing **text** columns when explicit or implicit null values are added in **text** columns with INSERT, and no text pointer can be obtained for such nulls. To initialize **text** columns to NULL, use the UPDATE statement. If the table has in row text, you do not have to initialize the text column for nulls and you can always get a text pointer.  
  
 The ODBC SQLPutData function is faster and uses less dynamic memory than WRITETEXT. This function can insert up to 2 gigabytes of **text**, **ntext**, or **image** data.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], in row text pointers to **text**, **ntext**, or **image** data may exist but may not be valid. For information about the text in row option, see [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md). For information about invalidating text pointers, see [sp_invalidate_textptr &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-invalidate-textptr-transact-sql.md).  
  
## Permissions  
 Requires UPDATE permission on the specified table. Permission is transferable when UPDATE permission is transferred.  
  
## Examples  
 The following example puts the text pointer into the local variable `@ptrval`, and then `WRITETEXT` places the new text string into the row pointed to by `@ptrval`.  
  
> [!NOTE]  
>  To run this example, you must install the pubs sample database.  
  
```sql  
USE pubs;  
GO  
ALTER DATABASE pubs SET RECOVERY SIMPLE;  
GO  
DECLARE @ptrval BINARY(16);  
SELECT @ptrval = TEXTPTR(pr_info)   
FROM pub_info pr, publishers p  
WHERE p.pub_id = pr.pub_id   
   AND p.pub_name = 'New Moon Books'  
WRITETEXT pub_info.pr_info @ptrval 'New Moon Books (NMB) has just released another top ten publication. With the latest publication this makes NMB the hottest new publisher of the year!';  
GO  
ALTER DATABASE pubs SET RECOVERY SIMPLE;  
GO  
```  
  
## See Also  
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md)   
 [DELETE &#40;Transact-SQL&#41;](../../t-sql/statements/delete-transact-sql.md)   
 [SELECT &#40;Transact-SQL&#41;](../../t-sql/queries/select-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)   
 [UPDATETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/updatetext-transact-sql.md)  
  
  
