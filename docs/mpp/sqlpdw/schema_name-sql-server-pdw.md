---
title: "SCHEMA_NAME (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: f9f3741b-ca50-4a88-997e-09ba672830d3
caps.latest.revision: 7
author: BarbKess
---
# SCHEMA_NAME (SQL Server PDW)
Returns the schema name associated with a schema ID.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SCHEMA_NAME ( [ schema_id ] )  
```  
  
## Arguments  
  
|Term|Definition|  
|--------|--------------|  
|*schema_id*|The ID of the schema. *schema_id* is an **int**. If *schema_id* is not defined, SCHEMA_NAME will return the name of the default schema of the caller.|  
  
## Return Types  
**sysname**  
  
Returns NULL when *schema_id* is not a valid ID.  
  
## Remarks  
SCHEMA_NAME returns names of system schemas and user-defined schemas. SCHEMA_NAME can be called in a select list, in a WHERE clause, and anywhere an expression is allowed.  
  
## Examples  
  
### A. Returning the name of the default schema of the caller  
  
```  
SELECT SCHEMA_NAME();  
```  
  
### B. Returning the name of a schema by using an ID  
  
```  
SELECT SCHEMA_NAME(1);  
```  
  
## See Also  
[SCHEMA_ID &#40;SQL Server PDW&#41;](../sqlpdw/schema-id-sql-server-pdw.md)  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
