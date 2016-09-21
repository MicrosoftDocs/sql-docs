---
title: "SET DATEFIRST (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 4106c9d9-338e-4578-81f8-ac0613ba163f
caps.latest.revision: 8
author: BarbKess
---
# SET DATEFIRST (SQL Server PDW)
Sets the first day of the week to a number. In this SQL Server PDW release, 7 is the only valid value, which is Sunday, the U.S. English default.  
  
For more information, see [SET DATEFIRST (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms181598(v=sql11).aspx)  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET DATEFIRST 7 ;  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
To see the current setting of SET DATEFIRST, use the [@@DATEFIRST &#40;SQL Server PDW&#41;](../sqlpdw/datefirst-sql-server-pdw.md) function.  
  
## Example  
  
```  
-- SET DATEFIRST to U.S. English default value of 7.  
SET DATEFIRST 7;  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[@@DATEFIRST &#40;SQL Server PDW&#41;](../sqlpdw/datefirst-sql-server-pdw.md)  
  
