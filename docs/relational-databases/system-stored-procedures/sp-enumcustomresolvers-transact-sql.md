---
title: "sp_enumcustomresolvers (Transact-SQL)"
description: sp_enumcustomresolvers returns a list of all available business logic handlers and custom resolvers registered at the Distributor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_enumcustomresolvers"
  - "sp_enumcustomresolvers_TSQL"
helpviewer_keywords:
  - "sp_enumcustomresolvers"
dev_langs:
  - "TSQL"
---
# sp_enumcustomresolvers (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a list of all available business logic handlers and custom resolvers registered at the Distributor. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_enumcustomresolvers [ [ @distributor = ] N'distributor' ]
[ ; ]
```

## Arguments

#### [ @distributor = ] N'*distributor*'

The name of the Distributor where the custom resolver is located. *@distributor* is **sysname**, with a default of `NULL`.

[!INCLUDE [deprecated-parameter](../includes/deprecated-parameter.md)]

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `article_resolver` | **nvarchar(255)** | Friendly name for the business logic handler or conflict resolver. |
| `resolver_clsid` | **nvarchar(50)** | Class ID (CLSID) of the COM-based resolver. For a business logic handler, this column returns a zero CLSID value. |
| `is_dotnet_assembly` | **bit** | Indicates whether the registration is for a business logic handler.<br /><br />`0` = COM-based conflict resolver<br />`1` = business logic handler |
| `dotnet_assembly_name` | **nvarchar(255)** | The name of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] .NET Framework assembly that implements the business logic handler. |
| `dotnet_class_name` | **nvarchar(255)** | The name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> that implements the business logic handler. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_enumcustomresolvers` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role and the **db_owner** fixed database role can execute `sp_enumcustomresolvers`.

## Related content

- [Implement a Business Logic Handler for a Merge Article](../replication/implement-a-business-logic-handler-for-a-merge-article.md)
- [Implement a custom conflict resolver for a Merge article](../replication/implement-a-custom-conflict-resolver-for-a-merge-article.md)
- [sp_lookupcustomresolver (Transact-SQL)](sp-lookupcustomresolver-transact-sql.md)
- [sp_unregistercustomresolver (Transact-SQL)](sp-unregistercustomresolver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
