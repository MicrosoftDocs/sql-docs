---
title: "CHAR (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 68b58377-9152-40d4-9114-03fe40fb874d
caps.latest.revision: 6
author: BarbKess
---
# CHAR (SQL Server PDW)
Converts an **int** ASCII code to a character.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
CHAR (integer_expression )  
```  
  
## Arguments  
*integer_expression*  
Is an integer from 0 through 255. NULL is returned if the integer expression is not in this range.  
  
## Return Types  
**char(1)**  
  
## Remarks  
CHAR can be used to insert control characters into character strings. The following table shows some frequently used control characters.  
  
|Control character|Value|  
|---------------------|---------|  
|Tab|**char(9)**|  
|Line feed|**char(10)**|  
|Carriage return|**char(13)**|  
  
## Examples  
  
### A. Using ASCII and CHAR to print ASCII values from a string  
The following example assumes an ASCII character set and returns the character value for 6 ASCII character numbers.  
  
```  
SELECT CHAR(65) AS [65], CHAR(66) AS [66],   
CHAR(97) AS [97], CHAR(98) AS [98],   
CHAR(49) AS [49], CHAR(50) AS [50];  
```  
  
Here is the result set.  
  
```  
65   66   97   98   49   50  
---- ---- ---- ---- ---- ----  
A    B    a    b    1    2  
```  
  
### B. Using CHAR to insert a control character  
The following example uses `CHAR(13)` to return information about the databases on separate lines when the results are returned in text.  
  
```  
SELECT name, 'was created on ', create_date, CHAR(13), name, 'is currently ', state_desc   
FROM sys.databases;  
GO  
```  
  
Here is the result set.  
  
```  
name     create_date    name    state_desc  
------------------------------------------------------------  
master                   was created on  2003-04-08 09:13:36.390   
master                   is currently  ONLINE  
tempdb                   was created on  2014-01-10 17:24:24.023   
tempdb                   is currently  ONLINE  
AdventureWorksPDW2012    was created on  2014-05-07 09:05:07.083 AdventureWorksPDW2012    is currently  ONLINE  
```  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[ASCII &#40;SQL Server PDW&#41;](../sqlpdw/ascii-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
