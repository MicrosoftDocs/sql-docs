---
title: "SET ARITHIGNORE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5214fcf9-dacb-4018-9ae4-f3622c790dc9
caps.latest.revision: 8
author: BarbKess
---
# SET ARITHIGNORE (SQL Server PDW)
Controls whether error messages are returned from overflow or divide-by-zero errors during a SQL Server PDW query. In this release, ARITHIGNORE can only be set to OFF, and therefore SQL Server PDW does not return error messages for these errors.  
  
For more information, see the [SET ARITHIGNORE (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms184341(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ARITHIGNORE OFF   
[ ; ]  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
SQL Server PDW returns a NULL in a calculation involving an overflow or divide-by-zero error, regardless of the ARITHIGNORE setting.  
  
This setting does not affect errors occurring during INSERT, UPDATE, and DELETE statements.  
  
The SET ARITHIGNORE setting is set at execute or run time and not at parse time.  
  
## Examples  
The following example demonstrates the divide by zero and the overflow errors. This example does not return an error message for these errors because ARITHIGNORE is OFF.  
  
```  
-- SET ARITHIGNORE OFF and testing.  
SET ARITHIGNORE OFF;  
SELECT 1 / 0 AS DivideByZero;  
SELECT CAST(256 AS TINYINT) AS Overflow;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
