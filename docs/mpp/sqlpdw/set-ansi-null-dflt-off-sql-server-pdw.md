---
title: "SET ANSI_NULL_DFLT_OFF (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: d3224259-98a0-4f39-8651-64b3e948cabb
caps.latest.revision: 7
author: BarbKess
---
# SET ANSI_NULL_DFLT_OFF (SQL Server PDW)
Alters the behavior of the session to override default nullability of new columns when the **ANSI_NULL_DFLT_ON** setting is ON.  
  
In this release, ANSI_NULL_DFLT_OFF is always OFF and ANSI_NULL_DFLT_ON is always ON. Therefore, new columns are always nullable unless NOT NULL is explicitly stated in the column definition.  
  
For more information, see the [SET ANSI_NULL_DFLT_OFF (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187356(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ANSI_NULL_DFLT_OFF OFF  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
This setting only affects the nullability of new columns when the nullability of the column is not specified in the CREATE TABLE and ALTER TABLE statements. New columns  that are specified with an explicit NOT NULL are not affected by the ANSI_NULL_DFLT_ON setting.  
  
The setting of SET ANSI_NULL_DFLT_OFF is set at execute or run time and not at parse time.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
