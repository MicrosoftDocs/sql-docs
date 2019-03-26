---
title: "TEXTPTR (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "10/23/2017"
ms.prod: sql
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "TEXTPTR_TSQL"
  - "TEXTPTR"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "TEXTPTR function"
  - "viewing text pointer values"
  - "text-pointer values"
  - "displaying text pointer values"
ms.assetid: 2672b8cb-f747-46f3-9358-9b49b3583b8e
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Text and Image Functions - TEXTPTR (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns the text-pointer value that corresponds to a **text**, **ntext**, or **image** column in **varbinary** format. The retrieved text pointer value can be used in READTEXT, WRITETEXT, and UPDATETEXT statements.  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] Alternative functionality is not available.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
TEXTPTR ( column )  
```  
  
## Arguments  
 *column*  
 Is the **text**, **ntext**, or **image** column that will be used.  
  
## Return Types  
 **varbinary**  
  
## Remarks  
 For tables with in-row text, TEXTPTR returns a handle for the text to be processed. You can obtain a valid text pointer even if the text value is null.  
  
 You cannot use the TEXTPTR function on columns of views. You can only use it on columns of tables. To use the TEXTPTR function on a column of a view, you must set the compatibility level to 80 by using [ALTER DATABASE Compatibility Level](../../t-sql/statements/alter-database-transact-sql-compatibility-level.md). If the table does not have in-row text, and if a **text**, **ntext**, or **image** column has not been initialized by an UPDATETEXT statement, TEXTPTR returns a null pointer.  
  
 Use TEXTVALID to test whether a text pointer exists. You cannot use UPDATETEXT, WRITETEXT, or READTEXT without a valid text pointer.  
  
 These functions and statements are also useful when you work with **text**, **ntext**, and **image** data.  
  
|Function or statement|Description|  
|---------------------------|-----------------|  
|PATINDEX<b>('</b>_%pattern%_**' ,** _expression_**)**|Returns the character position of a specified character string in **text** or **ntext** columns.|  
|DATALENGTH<b>(</b>_expression_**)**|Returns the length of data in **text**, **ntext**, and **image** columns.|  
|SET TEXTSIZE|Returns the limit, in bytes, of the **text**, **ntext**, or **image** data to be returned with a SELECT statement.|  
|SUBSTRING<b>(</b>_text_column_, _start_, _length_**)**|Returns a **varchar** string specified by the specified *start* offset and *length*. The length should be less than 8 KB.|  
  
## Examples  
  
> [!NOTE]  
>  To run the following examples, you must install the **pubs** database.  
  
### A. Using TEXTPTR  
 The following example uses the `TEXTPTR` function to locate the **image** column `logo` associated with `New Moon Books` in the `pub_info` table of the `pubs` database. The text pointer is put into a local variable `@ptrval.`  
  
```  
USE pubs;  
GO  
DECLARE @ptrval varbinary(16);  
SELECT @ptrval = TEXTPTR(logo)  
FROM pub_info pr, publishers p  
WHERE p.pub_id = pr.pub_id   
   AND p.pub_name = 'New Moon Books';  
GO  
```  
  
### B. Using TEXTPTR with in-row text  
 In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the in-row text pointer must be used inside a transaction, as shown in the following example.  
  
```  
CREATE TABLE t1 (c1 int, c2 text);  
EXEC sp_tableoption 't1', 'text in row', 'on';  
INSERT t1 VALUES ('1', 'This is text.');  
GO  
BEGIN TRAN;  
   DECLARE @ptrval VARBINARY(16);  
   SELECT @ptrval = TEXTPTR(c2)  
   FROM t1  
   WHERE c1 = 1;  
   READTEXT t1.c2 @ptrval 0 1;  
COMMIT;  
```  
  
### C. Returning text data  
 The following example selects the `pub_id` column and the 16-byte text pointer of the `pr_info` column from the `pub_info` table.  
  
```  
USE pubs;  
GO  
SELECT pub_id, TEXTPTR(pr_info)  
FROM pub_info  
ORDER BY pub_id;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
pub_id                                      
------ ----------------------------------   
0736   0x6c0000000000feffb801000001000100   
0877   0x6d0000000000feffb801000001000300   
1389   0x6e0000000000feffb801000001000500   
1622   0x700000000000feffb801000001000900   
1756   0x710000000000feffb801000001000b00   
9901   0x720000000000feffb801000001000d00   
9952   0x6f0000000000feffb801000001000700   
9999   0x730000000000feffb801000001000f00   
  
(8 row(s) affected)  
```  
  
 The following example shows how to return the first `8000` bytes of text without using TEXTPTR.  
  
```  
USE pubs;  
GO  
SET TEXTSIZE 8000;  
SELECT pub_id, pr_info  
FROM pub_info  
ORDER BY pub_id;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
pub_id pr_info                                                                                                                                                                                                                                                           
------ -----------------------------------------------------------------  
0736   New Moon Books (NMB) has just released another top ten publication. With the latest publication this makes NMB the hottest new publisher of the year!                                                                                                             
0877   This is sample text data for Binnet & Hardley, publisher 0877 in the pubs database. Binnet & Hardley is located in Washington, D.C.  
  
This is sample text data for Binnet & Hardley, publisher 0877 in the pubs database. Binnet & Hardley is located in Washi   
1389   This is sample text data for Algodata Infosystems, publisher 1389 in the pubs database. Algodata Infosystems is located in Berkeley, California.  
  
9999   This is sample text data for Lucerne Publishing, publisher 9999 in the pubs database. Lucerne publishing is located in Paris, France.  
  
This is sample text data for Lucerne Publishing, publisher 9999 in the pubs database. Lucerne publishing is located in   
  
(8 row(s) affected)  
```  
  
### D. Returning specific text data  
 The following example locates the `text` column (`pr_info`) associated with `pub_id``0736` in the `pub_info` table of the `pubs` database. It first declares the local variable `@val`. The text pointer (a long binary string) is then put into `@val` and supplied as a parameter to the `READTEXT` statement. This returns 10 bytes starting at the fifth byte (offset of 4).  
  
```  
USE pubs;  
GO  
DECLARE @val varbinary(16);  
SELECT @val = TEXTPTR(pr_info)   
FROM pub_info  
WHERE pub_id = '0736';  
READTEXT pub_info.pr_info @val 4 10;  
GO  
```  
  
 [!INCLUDE[ssResult](../../includes/ssresult-md.md)]  
  
```  
pr_info                                                                                                                                                                                                                                                           
-----------------------------------------------------------------------  
 is sample  
(1 row(s) affected)  
```  
  
## See Also  
 [DATALENGTH &#40;Transact-SQL&#41;](../../t-sql/functions/datalength-transact-sql.md)   
 [PATINDEX &#40;Transact-SQL&#41;](../../t-sql/functions/patindex-transact-sql.md)   
 [READTEXT &#40;Transact-SQL&#41;](../../t-sql/queries/readtext-transact-sql.md)   
 [SET TEXTSIZE &#40;Transact-SQL&#41;](../../t-sql/statements/set-textsize-transact-sql.md)   
 [Text and Image Functions &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/b9c70488-1bf5-4068-a003-e548ccbc5199)   
 [UPDATETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/updatetext-transact-sql.md)   
 [WRITETEXT &#40;Transact-SQL&#41;](../../t-sql/queries/writetext-transact-sql.md)  
  
  
