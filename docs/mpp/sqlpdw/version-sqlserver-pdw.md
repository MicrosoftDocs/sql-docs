---
title: "VERSION (SQLServer PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 8969e22a-81bf-4cc7-b884-16d4a6726c9b
caps.latest.revision: 10
author: BarbKess
---
# VERSION (SQLServer PDW)
Returns the version of SQL Server PDW running on the appliance.  
  
![Topic link icon](../../mpp/sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
VERSION ()  
```  
  
## Arguments  
  
## General Remarks  
A table name must be specified in a [FROM](../../mpp/sqlpdw/from-sql-server-pdw.md) clause for this function to return results. A result row will be returned for each row in the result set for the query; use [TOP](../../mpp/sqlpdw/top-sql-server-pdw.md) to limit the number of returned rows.  
  
## Examples  
The following example returns the version number.  
  
```  
SELECT VERSION();  
```  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
[SESSION_ID &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/session-id-sql-server-pdw.md)  
[DB_NAME &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/db-name-sql-server-pdw.md)  
  
