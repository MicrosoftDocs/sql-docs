---
title: "NULLIF (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: ba351cc8-2d4d-486a-b6a5-b7177ee44b65
caps.latest.revision: 13
author: BarbKess
---
# NULLIF (SQL Server PDW)
Returns a null value if the two specified expressions are equal in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
NULLIF (expression ,expression )  
```  
  
## Arguments  
*expression*  
Is any valid scalar expression.  
  
## Return Types  
Returns the same type as the first *expression*.  
  
## Return Values  
NULLIF returns the first *expression* if the two expressions are not equal. If the expressions are equal, NULLIF returns a null value of the type of the first *expression*.  
  
## Examples  
The following example creates a `budgets` table, loads data, and uses `NULLIF` to return a null if neither `current_year` nor `previous_year` contains data.  
  
```  
CREATE TABLE budgets (  
   dept           tinyint,  
   current_year   decimal(10,2),  
   previous_year  decimal(10,2)  
);  
  
INSERT INTO budgets VALUES(1, 100000, 150000);  
INSERT INTO budgets VALUES(2, NULL, 300000);  
INSERT INTO budgets VALUES(3, 0, 100000);  
INSERT INTO budgets VALUES(4, NULL, 150000);  
INSERT INTO budgets VALUES(5, 300000, 300000);  
  
SELECT dept, NULLIF(current_year,  
   previous_year) AS LastBudget  
FROM budgets;  
```  
  
Here is the result set.  
  
<pre>dept   LastBudget  
----   -----------  
1      100000.00  
2      null  
3      0.00  
4      null  
5      null</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[ISNULL &#40;SQL Server PDW&#41;](../sqlpdw/isnull-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[NULL and UNKNOWN &#40;SQL Server PDW&#41;](../sqlpdw/null-and-unknown-sql-server-pdw.md)  
  
