---
title: "sp_registercustomresolver (Transact-SQL)"
description: Registers a business logic handler or a COM-based custom resolver that can be invoked during the merge replication synchronization process.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/22/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_registercustomresolver"
  - "sp_registercustomresolver_TSQL"
helpviewer_keywords:
  - "sp_registercustomresolver"
dev_langs:
  - "TSQL"
---
# sp_registercustomresolver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Registers a business logic handler or a COM-based custom resolver that can be invoked during the merge replication synchronization process. This stored procedure is executed at the Distributor.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_registercustomresolver
    [ @article_resolver = ] N'article_resolver'
    [ , [ @resolver_clsid = ] N'resolver_clsid' ]
    [ , [ @is_dotnet_assembly = ] N'is_dotnet_assembly' ]
    [ , [ @dotnet_assembly_name = ] N'dotnet_assembly_name' ]
    [ , [ @dotnet_class_name = ] N'dotnet_class_name' ]
[ ; ]
```

## Arguments

#### [ @article_resolver = ] N'*article_resolver*'

Specifies the friendly name for the custom business logic being registered. *@article_resolver* is **nvarchar(255)**, with no default.

#### [ @resolver_clsid = ] N'*resolver_clsid*'

Specifies the CLSID value of the COM object that being registered. *@resolver_clsid* is **nvarchar(50)**, with a default of `NULL`. This parameter must be set to a valid CLSID or set to `NULL` when registering a business logic handler assembly.

#### [ @is_dotnet_assembly = ] N'*is_dotnet_assembly*'

Specifies the type of custom business logic that is being registered. *@is_dotnet_assembly* is **nvarchar(10)**, with a default of `false`.

- `true` indicates that the custom business logic being registered is a business logic handler Assembly.
- `false` indicates that custom business logic is a COM component.

#### [ @dotnet_assembly_name = ] N'*dotnet_assembly_name*'

The name of the assembly that implements the business logic handler. *@dotnet_assembly_name* is **nvarchar(255)**, with a default of `NULL`. You must specify the full path to the assembly if it's not deployed in the same directory as the Merge Agent executable, in the same directory as the application that synchronously starts the Merge Agent, or in the global assembly cache (GAC).

#### [ @dotnet_class_name = ] N'*dotnet_class_name*'

The name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> to implement the business logic handler. *@dotnet_class_name* is **nvarchar(255)**, with a default of `NULL`. The name should be specified in the form `<Namespace>.<Classname>`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_registercustomresolver` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_registercustomresolver`.

## Related content

- [Implement a Business Logic Handler for a Merge Article](../replication/implement-a-business-logic-handler-for-a-merge-article.md)
- [Implement a custom conflict resolver for a Merge article](../replication/implement-a-custom-conflict-resolver-for-a-merge-article.md)
- [sp_lookupcustomresolver (Transact-SQL)](sp-lookupcustomresolver-transact-sql.md)
- [sp_unregistercustomresolver (Transact-SQL)](sp-unregistercustomresolver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
