---
title: "SET ANSI_NULL_DFLT_ON (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 5969aaf8-ab7d-42f2-a374-34205675238a
caps.latest.revision: 8
author: BarbKess
---
# SET ANSI_NULL_DFLT_ON (SQL Server PDW)
Alters the behavior of the SQL Server PDW session to override default nullability of new columns when the **ANSI_NULL_DFLT_OFF** setting is OFF.  
  
In this release, ANSI_NULL_DFLT_ON is always ON and ANSI_NULL_DFLT_OFF is always OFF. Therefore, new columns are always nullable unless NOT NULL is explicitly stated in the column definition.  
  
For more information, see the [SET ANSI_NULL_DFLT_ON (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms187375(v=sql11)) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SET ANSI_NULL_DFLT_ON ON;  
```  
  
## Permissions  
Requires membership in the **public** role.  
  
## General Remarks  
This setting only affects the nullability of new columns when the nullability of the column is not specified in the CREATE TABLE and ALTER TABLE statements. New columns that are specified with an explicit NOT NULL are not affected by the ANSI_NULL_DFLT_ON setting.  
  
The SET ANSI_NULL_DFLT_OFF is set at execute or run time and not at parse time.  
  
## Permissions  
Requires membership in the **public** role.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
