---
title: "SIGN (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 97fb3cbc-474a-41e6-a1ea-decdb3fc2747
caps.latest.revision: 18
author: BarbKess
---
# SIGN (SQL Server PDW)
Returns the positive (+1), zero (0), or negative (-1) sign of the specified expression in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SIGN (numeric_expression )  
```  
  
## Arguments  
*numeric_expression*  
An expression of the exact numeric or approximate **numeric** data type categories, except for the **bit** data type.  
  
## Return Types  
  
|Specified expression|Return type|  
|------------------------|---------------|  
|**bigint**|**bigint**|  
|**int/smallint/tinyint**|**int**|  
|**decimal**|**decimal**|  
|**Other types**|**float**|  
  
## Examples  
The following example returns the SIGN values of three numbers.  
  
```  
SELECT SIGN(-125), SIGN(0), SIGN(564);  
```  
  
Here is the result set.  
  
<pre>-----  -----  -----  
-1     0      1</pre>  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[Expressions &#40;SQL Server PDW&#41;](../sqlpdw/expressions-sql-server-pdw.md)  
[Data Types &#40;SQL Server PDW&#41;](../sqlpdw/data-types-sql-server-pdw.md)  
  
