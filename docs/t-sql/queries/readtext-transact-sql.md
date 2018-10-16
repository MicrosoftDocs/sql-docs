---
title: "READTEXT (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/24/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "READTEXT_TSQL"
  - "READTEXT"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "column reading [SQL Server]"
  - "READTEXT statement"
  - "reading columns"
ms.assetid: 91b69853-1381-4306-8343-afdb73105738
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# READTEXT (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Reads **text**, **ntext**, or **image** values from a **text**, **ntext**, or **image** column, starting from a specified offset and reading the specified number of bytes.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Use the [SUBSTRING](../../t-sql/functions/substring-transact-sql.md) function instead.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
READTEXT { table.column text_ptr offset size } [ HOLDLOCK ]  
```  
  
## Arguments  
 *table* **.** *column*  
 Is the name of a table and column from which to read. Table and column names must comply with the rules for [identifiers](../../relational-databases/databases/database-identifiers.md). Specifying the table and column names is required; however, specifying the database name and owner names is optional.  
  
 *text_ptr*  
 Is a valid text pointer. *text_ptr* must be **binary(16)**.  
  
 *offset*  
 Is the number of bytes (when the **text** or **image** data types are used) or characters (when the **ntext** data type is used) to skip before it starts to read the **text**, **image**, or **ntext** data.  
  
 *size*  
 Is the number of bytes (when the **text** or **image** data types are used) or characters (when the **ntext** data type is used) of data to read. If *size* is 0, 4 KB bytes of data is read.  
  
 HOLDLOCK  
 Causes the text value to be locked for reads until the end of the transaction. Other users can read the value, but they cannot modify it.  
  
## Remarks  
 Use the [TEXTPTR](../../t-sql/functions/text-and-image-functions-textptr-transact-sql.md) function to obtain a valid *text_ptr* value. TEXTPTR returns a pointer to the **text**, **ntext**, or **image** column in the specified row or to the **text**, **ntext**, or **image** column in the last row returned by the query if more than one row is returned. Because TEXTPTR returns a 16-byte binary string, we recommend declaring a local variable to hold the text pointer, and then use the variable with READTEXT. For more information about declaring a local variable, see [DECLARE @local_variable &#40;Transact-SQL&#41;](../../t-sql/language-elements/declare-local-variable-transact-sql.md).  
  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], in-row text pointers may exist but may not be valid. For more information about the **text in row** option, see [sp_tableoption &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-tableoption-transact-sql.md). For more information about invalidating text pointers, see [sp_invalidate_textptr &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-invalidate-textptr-transact-sql.md).  
  
 The value of the @@TEXTSIZE function supersedes the size specified for READTEXT if it is less than the specified size for READTEXT. The @@TEXTSIZE function specifies the limit on the number of bytes of data to be returned set by the SET TEXTSIZE statement. For more information about how to set the session setting for TEXTSIZE, see [SET TEXTSIZE &#40;Transact-SQL&#41;](../../t-sql/statements/set-textsize-transact-sql.md).  
  
## Permissions  
 READTEXT permissions default to users that have SELECT permissions on the specified table. Permissions are transferable when SELECT permissions are transferred.  
  
## Examples  
 The following example reads the second through twenty-sixth characters of the `pr_info` column in the `pub_info` table.  
  
> [!NOTE]  
>  To run this example, you must install the **pubs** sample database.  
  
```  
USE pubs;  
GO  
DECLARE @ptrval varbinary(16);  
SELECT @ptrval = TEXTPTR(pr_info)   
   FROM pub_info pr INNER JOIN publishers p  
      ON pr.pub_id = p.pub_id   
      AND p.pub_name = 'New Moon Books'  
READTEXT pub_info.pr_info @ptrval 1 25;  
GO  
```  
  
## See Also  
 [@@TEXTSIZE &#40;Transact-SQL&#41;](../../t-sql/functions/textsize-transact-sql.md)   
 [UPDATETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/updatetext-transact-sql.md)   
 [WRITETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/writetext-transact-sql.md)  
  
  
