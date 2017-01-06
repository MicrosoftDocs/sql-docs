---
title: "ISNUMERIC (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 7f933c3b-b78d-4ec1-b7ff-fbb19c45b0bc
caps.latest.revision: 4
author: BarbKess
---
# ISNUMERIC (SQL Server PDW)
Determines whether an expression is a valid numeric type in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
ISNUMERIC (expression )  
```  
  
## Arguments  
*expression*  
Is the [expression](../Topic/Expressions%20(Transact-SQL).md) to be evaluated.  
  
## Return Types  
**int**  
  
## Remarks  
ISNUMERIC returns 1 when the input expression evaluates to a valid numeric data type; otherwise it returns 0. Valid numeric data types include the following:  
  
|||  
|-|-|  
|**int**|**money**|  
|**bigint**|**smallmoney**|  
|**smallint**|**float**|  
|**tinyint**|**real**|  
|**decimal**||  
  
> [!NOTE]  
> ISNUMERIC returns 1 for some characters that are not numbers, such as plus (+), minus (-), and valid currency symbols such as the dollar sign ($).  
  
## Examples  
The following example uses `ISNUMERIC` to return all the postal codes that are not numeric values.  
  
```  
USE master;  
GO  
SELECT name, isnumeric(name) AS IsNameANumber, database_id, isnumeric(database_id) AS IsIdANumber   
FROM sys.databases;  
GO  
```  
  
## See Also  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
  
