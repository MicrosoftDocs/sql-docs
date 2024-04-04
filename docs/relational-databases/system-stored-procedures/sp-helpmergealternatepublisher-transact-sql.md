---
title: "sp_helpmergealternatepublisher (Transact-SQL)"
description: "Returns a list of all servers enabled as alternate Publishers for merge publications."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_helpmergealternatepublisher_TSQL"
  - "sp_helpmergealternatepublisher"
helpviewer_keywords:
  - "sp_helpmergealternatepublisher"
dev_langs:
  - "TSQL"
---
# sp_helpmergealternatepublisher (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a list of all servers enabled as alternate Publishers for merge publications. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_helpmergealternatepublisher
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the alternate Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `alternate_publisher` | **sysname** | Name of the alternate Publisher. |
| `alternate_publisher_db` | **sysname** | Name of the publication database. |
| `alternate_publication` | **sysname** | Name of the publication. |
| `alternate_distributor` | **sysname** | Name of the distributor. |
| `friendly_name` | **nvarchar(255)** | Description of the alternate Publisher. |
| `enabled` | **bit** | Specifies if the server is an alternate Publisher. `1` specifies that the Publisher is enabled as an alternate Publisher. `0` specifies that it isn't enabled. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_helpmergealternatepublisher` is used in merge replication.

During every merge session, the system queries both the Publisher and Subscriber for each one's list of alternate publishers. The merge process adds or drops entries in the list of alternate publishers, the result of which is that the list of alternate publishers at both the Subscriber and Publisher match.

## Permissions

Only members of the publication access list for the publication can execute `sp_helpmergealternatepublisher`.

## Related content

- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
