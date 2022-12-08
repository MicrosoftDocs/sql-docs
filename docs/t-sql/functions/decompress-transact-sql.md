---
title: "DECOMPRESS (Transact-SQL)"
description: "DECOMPRESS (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "10/11/2018"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "DECOMPRESS"
  - "DECOMPRESS_TSQL"
helpviewer_keywords:
  - "DECOMPRESS function"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current || = azuresqldb-mi-current || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqledge-current || = azure-sqldw-latest"
---
# DECOMPRESS (Transact-SQL)
[!INCLUDE [sqlserver2016-asdb-asdbmi-asa](../../includes/applies-to-version/sqlserver2016-asdb-asdbmi-asa.md)]

This function will decompress an input expression value, using the GZIP algorithm. `DECOMPRESS` will return a byte array (VARBINARY(MAX) type).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql  
DECOMPRESS ( expression )  
```  
  
## Arguments
 *expression*  
A **varbinary(**_n_**)**, **varbinary(max)**, or **binary(**_n_**)** value. See [Expressions &#40;Transact-SQL&#41;](../../t-sql/language-elements/expressions-transact-sql.md) for more information.  
  
## Return Types  
A value of data type **varbinary(max)**. `DECOMPRESS` will use the ZIP algorithm to decompress the input argument. The user should explicitly cast result to a target type if necessary.  
  
## Remarks  
  
## Examples  
  
### A. Decompress Data at Query Time  
This example shows how to return compressed table data:  


  
```sql  
SELECT _id, name, surname, datemodified,  
             CAST(DECOMPRESS(info) AS NVARCHAR(MAX)) AS info  
FROM player;  
```  
  
### B. Display Compressed Data Using Computed Column

> [!NOTE]
> This example does not apply to Azure Synapse Analytics.

This example shows how to create a table for decompressed data storage:  
  
```sql  
CREATE TABLE example_table (  
    _id INT PRIMARY KEY IDENTITY,  
    name NVARCHAR(max),  
    surname NVARCHAR(max),  
    info VARBINARY(max),  
    info_json as CAST(DECOMPRESS(info) as NVARCHAR(max))  
);  
```  
  
## See Also  
 [String Functions &#40;Transact-SQL&#41;](../../t-sql/functions/string-functions-transact-sql.md)   
 [COMPRESS &#40;Transact-SQL&#41;](../../t-sql/functions/compress-transact-sql.md)  
  
  
