---
title: "TEXTVALID (Transact-SQL)"
description: "Text and Image Functions - TEXTVALID (Transact-SQL)"
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/06/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "TEXTVALID_TSQL"
  - "TEXTVALID"
helpviewer_keywords:
  - "invalid text pointers [SQL Server]"
  - "valid text pointers [SQL Server]"
  - "TEXTVALID function"
  - "checking text pointers"
  - "text-pointer values"
  - "verifying text pointers"
dev_langs:
  - "TSQL"
---
# Text and Image Functions - TEXTVALID (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

  A **text**, **ntext**, or **image** function that checks whether a specific text pointer is valid.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Alternative functionality is not available.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
TEXTVALID ( 'table.column' ,text_ ptr )  
```  
  
## Arguments
 *table*  
 Is the name of the table that will be used.  
  
 *column*  
 Is the name of the column that will be used.  
  
 *text_ptr*  
 Is the text pointer to be checked.  
  
## Return Types  
 **int**  
  
## Remarks  
 Returns 1 if the pointer is valid and 0 if the pointer is not valid. Note that the identifier for the **text** column must include the table name. You cannot use UPDATETEXT, WRITETEXT, or READTEXT without a valid text pointer.  
  
 The following functions and statements are also useful when you work with **text**, **ntext**, and **image** data.  
  
|Function or statement|Description|  
|---------------------------|-----------------|  
|PATINDEX ( *'_%pattern%_'*, *_expression_* )|Returns the character position of a specified character string in **text** and **ntext** columns.|  
|DATALENGTH ( *_expression_* )|Returns the length of data in **text**, **ntext**, and **image** columns.| 
|SET TEXTSIZE|Returns the limit, in bytes, of the **text**, **ntext**, or **image** data to be returned with a SELECT statement.|  
  
## Examples  
 The following example reports whether a valid text pointer exists for each value in the `logo` column of the `pub_info` table.  
  
> [!NOTE]  
>  To run this example, you must install the **pubs** database.  
  
```sql
USE pubs;  
GO  
SELECT pub_id, 'Valid (if 1) Text data'   
   = TEXTVALID ('pub_info.logo', TEXTPTR(logo))   
FROM pub_info  
ORDER BY pub_id;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
pub_id Valid (if 1) Text data   
------ ----------------------   
0736   1                        
0877   1                        
1389   1                        
1622   1                        
1756   1                        
9901   1                        
9952   1                        
9999   1                        
  
(8 row(s) affected)  
```  
  
## Related content

- [DATALENGTH (Transact-SQL)](datalength-transact-sql.md)
- [PATINDEX (Transact-SQL)](patindex-transact-sql.md)
- [SET TEXTSIZE (Transact-SQL)](../statements/set-textsize-transact-sql.md)
- [Text and Image Functions - TEXTPTR (Transact-SQL)](text-and-image-functions-textptr-transact-sql.md)
