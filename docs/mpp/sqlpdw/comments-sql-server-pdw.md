---
title: "Comments (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 56ca8f99-9f0f-453c-9e33-1e19966578f8
caps.latest.revision: 9
author: BarbKess
---
# Comments (SQL Server PDW)
Examples for creating comments in SQL for SQL Server PDW.  
  
## Examples  
  
### A. Use two hyphens to comment a line  
You can comment a line by starting it with `--`.  
  
```  
--View all columns in AdventureWorksPDW2012 having Latin1_General_Bin2 collation  
USE AdventureWorksPDW2012;  
SELECT a.name AS 'Table Name', b.name AS 'Column Name', b.collation_name AS 'Collation Name'  
FROM sys.tables a JOIN sys.columns b ON a.object_id=b.object_id  
WHERE b.collation_name = 'Latin1_General_Bin2';  
```  
  
### B. Use /* and \*/ to comment a chunk  
You can comment a chunk of text by starting it with `/*` and ending it with `*/`.  
  
```  
/* Save this code for later use.  
USE AdventureWorksPDW2012;  
SELECT a.name AS 'Table Name', b.name AS 'Column Name', b.collation_name AS 'Collation Name'  
FROM sys.tables a JOIN sys.columns b ON a.object_id=b.object_id  
WHERE b.collation_name = 'Latin1_General_Bin2';  
*/  
```  
  
### C. Use nested comments  
You can nest /* and \*/ to create a comment around a chunk that contains a comment.  
  
```  
/* Don't run this.  
/* View all columns in AdventureWorksPDW2012 having Latin1_General_Bin2 collation */  
USE AdventureWorksPDW2012;  
SELECT a.name AS 'Table Name', b.name AS 'Column Name', b.collation_name AS 'Collation Name'  
FROM sys.tables a JOIN sys.columns b ON a.object_id=b.object_id  
WHERE b.collation_name = 'Latin1_General_Bin2';  
*/  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Language Elements &#40;SQL Server PDW&#41;](../sqlpdw/language-elements-sql-server-pdw.md)  
[SQL Reference &#40;SQL Server PDW&#41;](../sqlpdw/sql-reference-sql-server-pdw.md)  
  
