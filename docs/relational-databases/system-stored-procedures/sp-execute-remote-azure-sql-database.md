---
title: "sp_execute_remote (Azure SQL Database)"
description: sp_execute_remote executes a Transact-SQL statement on a single remote Azure SQL Database or set of databases serving as shards in a horizontal partitioning scheme.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql-database
ms.topic: conceptual
f1_keywords:
  - "sp_execute_remote"
  - "sp_execute_remote_TSQL"
helpviewer_keywords:
  - "remote execution"
  - "queries, remote execution"
dev_langs:
  - "TSQL"
monikerRange: "= azuresqldb-current"
---
# sp_execute_remote (Azure SQL Database)

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

Executes a [!INCLUDE [tsql](../../includes/tsql-md.md)] (T-SQL) statement on a single remote Azure SQL Database or set of databases serving as shards in a horizontal partitioning scheme.

The stored procedure is part of the elastic query feature. See [Azure SQL Database elastic query overview (preview)](/azure/azure-sql/database/elastic-query-overview) and [Reporting across scaled-out cloud databases (preview)](/azure/azure-sql/database/elastic-query-horizontal-partitioning).

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_execute_remote
    [ @data_source_name = ] data_source_name
    , [ @stmt = ] stmt
    [
        { , [ @params = ] N'@parameter_name data_type [ , ...n ]' }
        { , [ @param1 = ] 'value1' [ , ...n ] }
    ]
[ ; ]
```

## Arguments

#### [ @data_source_name = ] *data_source_name*

Identifies the external data source where the statement is executed. See [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). The external data source can be of type `RDBMS` or `SHARD_MAP_MANAGER`.

#### [ @stmt = ] *stmt*

A Unicode string that contains a [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch. *@stmt* must be either a Unicode constant or a Unicode variable. More complex Unicode expressions, such as concatenating two strings with the `+` operator, aren't allowed. Character constants aren't allowed. If a Unicode constant is specified, it must be prefixed with an `N`. For example, the Unicode constant `N'sp_who'` is valid, but the character constant `'sp_who'` isn't.

The size of the string is limited only by available database server memory. On 64-bit servers, the size of the string is limited to 2 GB, the maximum size of **nvarchar(max)**.

> [!NOTE]  
> *@stmt* can contain parameters having the same form as a variable name, for example: `N'SELECT * FROM HumanResources.Employee WHERE EmployeeID = @IDParameter'`.

Each parameter included in *@stmt* must have a corresponding entry in both the *@params* parameter definition list and the parameter values list.

#### [ @params = ] N'*@parameter_name* *data_type* [ ,... *n* ]'

One string that contains the definitions of all parameters that have been embedded in *@stmt*. The string must be either a Unicode constant or a Unicode variable. Each parameter definition consists of a parameter name and a data type. *n* is a placeholder that indicates more parameter definitions. Every parameter specified in *@stmt* must be defined in *@params*. If the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in @stmt doesn't contain parameters, *@params* isn't required. The default value for this parameter is `NULL`.

#### [ @param1 = ] '*value1*'

A value for the first parameter that is defined in the parameter string. The value can be a Unicode constant or a Unicode variable. There must be a parameter value supplied for every parameter included in *@stmt*. The values aren't required when the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement or batch in *@stmt* has no parameters.

#### *n*

A placeholder for the values of extra parameters. Values can only be constants or variables. Values can't be more complex expressions such as functions, or expressions built by using operators.

## Return code values

`0` (success) or non-zero (failure).

## Result set

Returns the result set from the first T-SQL statement.

## Permissions

Requires `ALTER ANY EXTERNAL DATA SOURCE` permission.

## Remarks

`sp_execute_remote` parameters must be entered in the specific order as described in the [Syntax](#syntax) section. If the parameters are entered out of order, an error message occurs.

`sp_execute_remote` has the same behavior as [EXECUTE](../../t-sql/language-elements/execute-transact-sql.md) regarding batches and the scope of names. The T-SQL statement or batch in the *@stmt* parameter isn't compiled until the `sp_execute_remote` statement is executed.

`sp_execute_remote` adds an extra column to the result set named `$ShardName` that contains the name of the remote database that produced the row.

`sp_execute_remote` can be used in a similar way to [sp_executesql](sp-executesql-transact-sql.md).

## Examples

### A. Basic example

The following example creates and executes a basic `SELECT` statement on a remote database.

```sql
EXEC sp_execute_remote
    N'MyExtSrc',
    N'SELECT COUNT(w_id) AS Count_id FROM warehouse';
```

### B. Example with multiple parameters

This example performs the following actions:

1. Creates a database scoped credential in a user database, specifying administrator credentials for the `master` database.

1. Creates an external data source pointing to the `master` database and specifying the database scoped credential.

1. Executes the `sp_set_firewall_rule` procedure in the `master` database. The `sp_set_firewall_rule` procedure requires three parameters, and requires the `@name` parameter to be Unicode.

```sql
EXEC sp_execute_remote @data_source_name = N'PointToMaster',
    @stmt = N'sp_set_firewall_rule @name, @start_ip_address, @end_ip_address',
    @params = N'@name nvarchar(128), @start_ip_address varchar(50), @end_ip_address varchar(50)',
    @name = N'TempFWRule',
    @start_ip_address = '0.0.0.2',
    @end_ip_address = '0.0.0.2';
```

## Related content

- [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md)
- [CREATE EXTERNAL DATA SOURCE (Transact-SQL)](../../t-sql/statements/create-external-data-source-transact-sql.md)
