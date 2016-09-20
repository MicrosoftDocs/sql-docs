---
title: "SQUARE (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 44035177-d3ac-4683-8fce-5d8a4942716b
caps.latest.revision: 10
author: BarbKess
---
# SQUARE (SQL Server PDW)
Returns the square of the specified **float** value in SQL Server PDW.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SQUARE (float_expression )  
```  
  
## Arguments  
*float_expression*  
An [expression](../../mpp/sqlpdw/expressions-sql-server-pdw.md) of type **float** or of a type that can be implicitly converted to float.  
  
## Return Types  
**float**  
  
## Examples  
The following example returns the square of each value in the `volume` column in the `containers` table.  
  
```  
USE AdventureWorksPDW2012;  
  
CREATE TABLE Containers (  
    ID int NOT NULL,  
    Name varchar(20),  
    Volume float(24));  
  
INSERT INTO Containers VALUES (1, 'Cylinder', '125.22');  
INSERT INTO Containers VALUES (2, 'Cube', '23.98');  
  
SELECT Name, SQUARE(Volume) AS VolSquared   
FROM Containers;  
```  
  
Here is the result set.  
  
<pre>Name           VolSquared  
-------------  ----------  
Cylinder       15680.05  
Cube             575.04</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[POWER &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/power-sql-server-pdw.md)  
[Functions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/functions-sql-server-pdw.md)  
  
