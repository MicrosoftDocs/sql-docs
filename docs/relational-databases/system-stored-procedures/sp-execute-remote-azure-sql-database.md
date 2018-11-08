---
title: "sp_execute_remote (Azure SQL Database) | Microsoft Docs"
ms.custom: ""
ms.date: "02/01/2017"
ms.prod: ""
ms.prod_service: "sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sp_execute_remote"
  - "sp_execute_remote_TSQL"
helpviewer_keywords: 
  - "remote execution"
  - "queries, remote execution"
ms.assetid: ca89aa4c-c4c1-4c46-8515-a6754667b3e5
author: "CarlRabeler"
ms.author: "carlrab"
manager: craigg
monikerRange: "= azuresqldb-current || = sqlallproducts-allversions"
---
# sp_execute_remote (Azure SQL Database)
[!INCLUDE[tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-xxxxxx-asdb-xxxx-xxx-md.md)]

  Executes a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement on a single remote Azure SQL Database or set of databases serving as shards in a horizontal partitioning scheme.  
  
 The stored procedure is part of the elastic query feature.  See [Azure SQL Database elastic database query overview](https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-overview/) and [Elastic database queries for sharding (horizontal partitioning)](https://azure.microsoft.com/documentation/articles/sql-database-elastic-query-horizontal-partitioning/).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_execute_remote [ @data_source_name = ] datasourcename  
[ , @stmt = ] statement  
[   
  { , [ @params = ] N'@parameter_name data_type [,...n ]' }   
     { , [ @param1 = ] 'value1' [ ,...n ] }  
]  
```  
  
## Arguments  
 [ \@data_source_name = ] *datasourcename*  
 Identifies the external data source where the statement is executed. See [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md). The external data source can be of type "RDBMS" or "SHARD_MAP_MANAGER".  
  
 [ \@stmt= ] *statement*  
 Is a Unicode string that contains a [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch. \@stmt must be either a Unicode constant or a Unicode variable. More complex Unicode expressions, such as concatenating two strings with the + operator, are not allowed. Character constants are not allowed. If a Unicode constant is specified, it must be prefixed with an **N**. For example, the Unicode constant **N'sp_who'** is valid, but the character constant **'sp_who'** is not. The size of the string is limited only by available database server memory. On 64-bit servers, the size of the string is limited to 2 GB, the maximum size of **nvarchar(max)**.  
  
> [!NOTE]  
>  \@stmt can contain parameters having the same form as a variable name, for example: `N'SELECT * FROM HumanResources.Employee WHERE EmployeeID = @IDParameter'`  
  
 Each parameter included in \@stmt must have a corresponding entry in both the \@params parameter definition list and the parameter values list.  
  
 [ \@params= ] N'\@*parameter_name**data_type* [ ,... *n* ] '  
 Is one string that contains the definitions of all parameters that have been embedded in \@stmt. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates additional parameter definitions. Every parameter specified in \@stmtmust be defined in \@params. If the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in \@stmt does not contain parameters, \@params is not required. The default value for this parameter is NULL.  
  
 [ \@param1= ] '*value1*'  
 Is a value for the first parameter that is defined in the parameter string. The value can be a Unicode constant or a Unicode variable. There must be a parameter value supplied for every parameter included in \@stmt. The values are not required when the [!INCLUDE[tsql](../../includes/tsql-md.md)] statement or batch in \@stmt has no parameters.  
  
 *n*  
 Is a placeholder for the values of additional parameters. Values can only be constants or variables. Values cannot be more complex expressions such as functions, or expressions built by using operators.  
  
## Return Code Values  
 0 (success) or non-zero (failure)  
  
## Result Sets  
 Returns the result set from the first SQL statement.  
  
## Permissions  
 Requires `ALTER ANY EXTERNAL DATA SOURCE` permission.  
  
## Remarks  
 `sp_execute_remote` parameters must be entered in the specific order as described in the syntax section above. If the parameters are entered out of order, an error message will occur.  
  
 `sp_execute_remote` has the same behavior as [EXECUTE &#40;Transact-SQL&#41;](../../t-sql/language-elements/execute-transact-sql.md) with regard to batches and the scope of names. The Transact-SQL statement or batch in the sp_execute_remote *\@stmt* parameter is not compiled until the sp_execute_remote statement is executed.  
  
 `sp_execute_remote` adds an additional column to the result set named '$ShardName' that contains the name of the remote database that produced the row.  
  
 `sp_execute_remote` can be used similar to [sp_executesql &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-executesql-transact-sql.md).  
  
## Examples  
### Simple example  
 The following example creates and executes a simple SELECT statement on a remote database.  
  
```sql  
EXEC sp_execute_remote  
    N'MyExtSrc',  
    N'SELECT COUNT(w_id) AS Count_id FROM warehouse'   
```  
  
### Example with multiple parameters  
Create a database scoped credential in a user database, specifying administrator credentials for the master database. Create an external data source pointing to the master database and specifying the database scoped credential. Then following, example in the user database, executes the `sp_set_firewall_rule` procedure in the master database. The `sp_set_firewall_rule` procedure requires 3 parameters, and requires the `@name` parameter to be Unicode.

```
EXEC sp_execute_remote @data_source_name  = N'PointToMaster', 
@stmt = N'sp_set_firewall_rule @name, @start_ip_address, @end_ip_address', 
@params = N'@name nvarchar(128), @start_ip_address varchar(50), @end_ip_address varchar(50)',
@name = N'TempFWRule', @start_ip_address = '0.0.0.2', @end_ip_address = '0.0.0.2';
```

## See Also:

[CREATE DATABASE SCOPED CREDENTIAL](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)  
[CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)  
    
