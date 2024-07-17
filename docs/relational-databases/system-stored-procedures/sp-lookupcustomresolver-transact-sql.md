---
title: "sp_lookupcustomresolver (Transact-SQL)"
description: Returns the information on a business logic handler or the class identifier (CLSID) value of a COM-based custom resolver component that is registered at the Distributor.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_lookupcustomresolver_TSQL"
  - "sp_lookupcustomresolver"
helpviewer_keywords:
  - "sp_lookupcustomresolver"
dev_langs:
  - "TSQL"
---
# sp_lookupcustomresolver (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the information on a business logic handler or the class identifier (CLSID) value of a COM-based custom resolver component that is registered at the Distributor. This stored procedure is executed at the Publisher on the publication database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_lookupcustomresolver
    [ @article_resolver = ] N'article_resolver'
    , [ @resolver_clsid = ] N'resolver_clsid' OUTPUT
    [ , [ @is_dotnet_assembly = ] is_dotnet_assembly OUTPUT ]
    [ , [ @dotnet_assembly_name = ] N'dotnet_assembly_name' OUTPUT ]
    [ , [ @dotnet_class_name = ] N'dotnet_class_name' OUTPUT ]
    [ , [ @publisher = ] N'publisher' ]
[ ; ]
```

## Arguments

#### [ @article_resolver = ] N'*article_resolver*'

Specifies the name of the custom business logic being unregistered. *@article_resolver* is **nvarchar(255)**, with no default. If the business logic being removed is a COM component, then this parameter is the friendly name of the component. If the business logic is a [!INCLUDE [msCoName](../../includes/msconame-md.md)] .NET Framework assembly, then this parameter is the name of the assembly.

#### [ @resolver_clsid = ] N'*resolver_clsid*' OUTPUT

The CLSID value of the COM object associated with the name of the custom business logic specified in the *@article_resolver* parameter. *@resolver_clsid* is an OUTPUT parameter of type **nvarchar(50)**.

#### [ @is_dotnet_assembly = ] *is_dotnet_assembly* OUTPUT

Specifies the type of custom business logic that is being registered. *@is_dotnet_assembly* is an OUTPUT parameter of type **bit**.

- `1` indicates that the custom business logic being registered is a business logic handler assembly.
- `0` (default) indicates that it's a COM component.

#### [ @dotnet_assembly_name = ] N'*dotnet_assembly_name*' OUTPUT

The name of the assembly that implements the business logic handler. *@dotnet_assembly_name* is an OUTPUT parameter of type **nvarchar(255)**.

#### [ @dotnet_class_name = ] N'*dotnet_class_name*' OUTPUT

The name of the class that overrides <xref:Microsoft.SqlServer.Replication.BusinessLogicSupport.BusinessLogicModule> to implement the business logic handler. *@dotnet_class_name* is an OUTPUT parameter of type **nvarchar(255)**.

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with a default of `NULL`. Use this parameter when the stored procedure isn't called from the Publisher. If not specified, it assumes that the local server is the Publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_lookupcustomresolver` is used in merge replication.

`sp_lookupcustomresolver` returns a `NULL` value for *resolver_clsid* when the component isn't registered at the Distribution and a value of `00000000-0000-0000-0000-000000000000` when the registration belongs to a .NET Framework assembly registered as a business logic handler.

`sp_lookupcustomresolver` is called by [sp_addmergearticle](sp-addmergearticle-transact-sql.md) and [sp_changemergearticle](sp-changemergearticle-transact-sql.md) to validate the specified *article_resolver*.

## Permissions

Only members of the **db_owner** fixed database role on the publication database can execute `sp_lookupcustomresolver`.

## Related content

- [Advanced Merge Replication - Conflict Detection and Resolution](../replication/merge/advanced-merge-replication-conflict-detection-and-resolution.md)
- [Execute Business Logic During Merge Synchronization](../replication/merge/execute-business-logic-during-merge-synchronization.md)
- [Implement a Business Logic Handler for a Merge Article](../replication/implement-a-business-logic-handler-for-a-merge-article.md)
- [Specify a Merge Article Resolver](../replication/publish/specify-a-merge-article-resolver.md)
- [sp_registercustomresolver (Transact-SQL)](sp-registercustomresolver-transact-sql.md)
- [sp_unregistercustomresolver (Transact-SQL)](sp-unregistercustomresolver-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
