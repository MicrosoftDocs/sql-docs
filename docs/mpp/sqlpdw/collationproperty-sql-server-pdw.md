---
title: "COLLATIONPROPERTY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 45031bbe-ce9c-4d67-b29c-c3719e02b561
caps.latest.revision: 8
author: BarbKess
---
# COLLATIONPROPERTY (SQL Server PDW)
Returns the property of a specified collation. For more information, see the [COLLATIONPROPERTY (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms190305.aspx)documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
COLLATIONPROPERTY(collation_name,property )  
```  
  
## Arguments  
*collation_name*  
Is the name of the collation. *collation_name* is **nvarchar(128)**, and has no default.  
  
*property*  
Is the property of the collation. *property* is **varchar(128)**, and can be any one of the following values:  
  
|Property name|Description|  
|-----------------|---------------|  
|**CodePage**|Non-Unicode code page of the collation.|  
|**LCID**|Windows LCID of the collation.|  
|**ComparisonStyle**|Windows comparison style of the collation. Returns 0 for all binary collations .|  
|**Version**|The version of the collation, derived from the version field of the collation ID. Returns 2, 1, or 0.<br /><br />Collations with "100" in the name) return 2.<br /><br />Collations with "90" in the name) return 1.<br /><br />All other collations return 0.|  
  
PDW supports Windows only collations and this function works for all PDW supported collations. Constant literals, variables, column references, and arbitrary expressions can be used as input parameters.  
  
## Return Types  
**sql_variant**  
  
## Examples  
  
```  
SELECT COLLATIONPROPERTY('Traditional_Spanish_CS_AS_KS_WS', 'CodePage')  
```  
  
Here is the result set.  
  
```  
1252  
```  
  
## See Also  
[Collations &#40;SQL Server PDW&#41;](../sqlpdw/collations-sql-server-pdw.md)  
  
