---
description: "UPDATETEXT (Transact-SQL)"
title: "UPDATETEXT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/23/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "UPDATETEXT"
  - "UPDATETEXT_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "text updates [SQL Server]"
  - "updating data [SQL Server]"
  - "data updates [SQL Server], UPDATETEXT statement"
  - "UPDATETEXT statement"
ms.assetid: d73c28ee-3972-4afd-af8d-ebbbd9e50793
author: VanMSFT
ms.author: vanto
---
# UPDATETEXT (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Updates an existing **text**, **ntext**, or **image** field. Use UPDATETEXT to change only a part of a **text**, **ntext**, or **image** column in place. Use WRITETEXT to update and replace a whole **text**, **ntext**, or **image** field.  
  
> [!IMPORTANT]
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the large-value data types and the **.WRITE** clause of the [UPDATE](../../t-sql/queries/update-transact-sql.md) statement instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
UPDATETEXT [BULK] { table_name.dest_column_name dest_text_ptr }  
  { NULL | insert_offset }  
     { NULL | delete_length }  
     [ WITH LOG ]  
     [ inserted_data  
    | { table_name.src_column_name src_text_ptr } ]  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 BULK  
 Enables upload tools to upload a binary data stream. The stream must be provided by the tool at the TDS protocol level. When the data stream is not present the query processor ignores the BULK option.  
  
> [!IMPORTANT]  
>  We recommend that the BULK option not be used in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-based applications. This option might be changed or removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 *table_name* **.** *dest_column_name*  
 Is the name of the table and **text**, **ntext**, or **image** column to be updated. Table names and column names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the database name and owner names is optional.  
  
 *dest_text_ptr*  
 Is a text pointer value (returned by the TEXTPTR function) that points to the **text**, **ntext**, or **image** data to be updated. *dest_text_ptr* must be **binary(**16**)**.  
  
 *insert_offset*  
 Is the zero-based starting position for the update. For **text** or **image** columns, *insert_offset* is the number of bytes to skip from the start of the existing column before inserting new data. For **ntext** columns, *insert_offset*is the number of characters (each **ntext** character uses 2 bytes). The existing **text**, **ntext**, or **image** data starting at this zero-based starting position is shifted to the right to make room for the new data. A value of 0 inserts the new data at the beginning of the existing data. A value of NULL appends the new data to the existing data value.  
  
 *delete_length*  
 Is the length of data to delete from the existing **text**, **ntext**, or **image** column, starting at the *insert_offset* position. The *delete_length*value is specified in bytes for **text** and **image** columns and in characters for **ntext** columns. Each **ntext** character uses 2 bytes. A value of 0 deletes no data. A value of NULL deletes all data from the *insert_offset* position to the end of the existing **text** or **image** column.  
  
 WITH LOG  
 Logging is determined by the recovery model in effect for the database.  
  
 *inserted_data*  
 Is the data to be inserted into the existing **text**, **ntext**, or **image** column at the *insert_offset* location. This is a single **char**, **nchar**, **varchar**, **nvarchar**, **binary**, **varbinary**, **text**, **ntext**, or **image** value. *inserted_data* can be a literal or a variable.  
  
 *table_name.src_column_name*  
 Is the name of the table and **text**, **ntext**, or **image** column used as the source of the inserted data. Table names and column names must comply with the rules for identifiers.  
  
 *src_text_ptr*  
 Is a text pointer value (returned by the TEXTPTR function) that points to a **text**, **ntext**, or **image** column used as the source of the inserted data.  
  
> [!NOTE]  
>  *scr_text_ptr* value must not be the same as *dest_text_ptr*value.  
  
## Remarks  
 Newly inserted data can be a single *inserted_data* constant, table name, column name, or text pointer.  
  
|Update action|UPDATETEXT parameters|  
|-------------------|---------------------------|  
|To replace existing data|Specify a nonnull *insert_offset* value, a nonzero *delete_length* value, and the new data to be inserted.|  
|To delete existing data|Specify a nonnull *insert_offset* value and a nonzero *delete_length*. Do not specify new data to be inserted.|  
|To insert new data|Specify the *insert_offset* value, a *delete_length* of 0, and the new data to be inserted.|  
  
 For best performance we recommend that **text**, **ntext** and **image** data be inserted or updated in chunks sizes that are multiples of 8,040 bytes.  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], in-row text pointers to **text**, **ntext**, or **image** data may exist but may not be valid. For information about the text in row option, see [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md). For information about invalidating text pointers, see [sp_invalidate_textptr &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-invalidate-textptr-transact-sql.md).  
  
 To initialize **text** columns to NULL, use WRITETEXT; UPDATETEXT initializes **text** columns to an empty string.  
  
## Permissions  
 Requires UPDATE permission on the specified table.  
  
## Examples  
 The following example puts the text pointer into the local variable `@ptrval`, and then uses `UPDATETEXT` to update a spelling error.  
  
> [!NOTE]  
>  To run this example, you must install the pubs database.  
  
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
UPDATETEXT pub_info.pr_info @ptrval 88 1 'b';  
GO  
ALTER DATABASE pubs SET RECOVERY FULL;  
GO  
```  
  
## See Also  
 [READTEXT &#40;Transact-SQL&#41;](../../t-sql/queries/readtext-transact-sql.md)   
 [TEXTPTR &#40;Transact-SQL&#41;](../../t-sql/functions/text-and-image-functions-textptr-transact-sql.md)   
 [WRITETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/writetext-transact-sql.md)  
  
  
