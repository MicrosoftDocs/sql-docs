---
title: "SCHEMA_ID (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 2b5448d7-face-4d9c-9a87-9644d6289fe2
caps.latest.revision: 7
author: BarbKess
---
# SCHEMA_ID (SQL Server PDW)
Returns the schema ID associated with a schema name.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SCHEMA_ID ( [ schema_name ] )  
```  
  
## Arguments  
  
|Term|Definition|  
|--------|--------------|  
|*schema_name*|The name of the schema. *schema_name* is a **sysname**. If *schema_name* is not specified, SCHEMA_ID will return the ID of the default schema of the caller.|  
  
## Return Types  
**int**  
  
NULL will be returned if *schema_name* is not a valid schema.  
  
## Remarks  
SCHEMA_ID will return IDs of system schemas and user-defined schemas. SCHEMA_ID can be called in a select list, in a WHERE clause, and anywhere an expression is allowed.  
  
## Examples  
  
### A. Returning the default schema ID of a caller  
  
```  
SELECT SCHEMA_ID();  
```  
  
### B. Returning the schema ID of a named schema  
  
```  
SELECT SCHEMA_ID('dbo');  
```  
  
## See Also  
[SCHEMA_NAME &#40;SQL Server PDW&#41;](../sqlpdw/schema_name-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
