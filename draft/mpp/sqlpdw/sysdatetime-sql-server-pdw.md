---
title: "SYSDATETIME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c97f3523-dbe4-416f-be76-7d8c4904270d
caps.latest.revision: 21
author: BarbKess
---
# SYSDATETIME (SQL Server PDW)
Returns a **datetime** value that contains the current appliance date and time from the Control Node in SQL Server PDW.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SYSDATETIME ()  
```  
  
## Return Type  
**datetime2(7)**  
  
## General Remarks  
SQL statements can refer to SYSDATETIME anywhere they can refer to a **datetime** expression.  
  
## Limitations and Restrictions  
SYSDATETIME is a nondeterministic function. Views and expressions that reference this function in a column cannot be indexed.  
  
## Examples  
  
```  
SELECT SYSDATETIME();  
```  
  
Here is the result set.  
  
<pre>--------------------------  
7/20/2013 2:49:59 PM</pre>  
  
## See Also  
[Functions &#40;SQL Server PDW&#41;](../sqlpdw/functions-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
