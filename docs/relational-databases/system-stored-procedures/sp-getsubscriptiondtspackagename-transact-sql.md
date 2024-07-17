---
title: "sp_getsubscriptiondtspackagename (Transact-SQL)"
description: Returns the name of the Data Transformation Services (DTS) package used to transform data before sending them to a Subscriber.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_getsubscriptiondtspackagename"
  - "sp_getsubscriptiondtspackagename_TSQL"
helpviewer_keywords:
  - "sp_getsubscriptiondtspackagename"
dev_langs:
  - "TSQL"
---
# sp_getsubscriptiondtspackagename (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the name of the Data Transformation Services (DTS) package used to transform data before they are sent to a Subscriber. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_getsubscriptiondtspackagename
    [ @publication = ] N'publication'
    [ , [ @subscriber = ] N'subscriber' ]
[ ; ]
```

## Arguments

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @subscriber = ] N'*subscriber*'

The name of the Subscriber. *@subscriber* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `new_package_name` | **sysname** | The name of the DTS package. |

## Remarks

`sp_getsubscriptiondtspackagename` is used in snapshot replication and transactional replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_getsubscriptiondtspackagename`.
