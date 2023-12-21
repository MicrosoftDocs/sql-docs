---
title: "sp_dropmergealternatepublisher (Transact-SQL)"
description: "Removes an alternate Publisher from a merge publication. This stored procedure is executed at the Subscriber on the subscription database."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dropmergealternatepublisher"
  - "sp_dropmergealternatepublisher_TSQL"
helpviewer_keywords:
  - "sp_dropmergealternatepublisher"
dev_langs:
  - "TSQL"
---
# sp_dropmergealternatepublisher (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Removes an alternate Publisher from a merge publication. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropmergealternatepublisher
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @alternate_publisher = ] N'alternate_publisher'
    , [ @alternate_publisher_db = ] N'alternate_publisher_db'
    , [ @alternate_publication = ] N'alternate_publication'
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the current Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the current publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the current publication. *@publication* is **sysname**, with no default.

#### [ @alternate_publisher = ] N'*alternate_publisher*'

The name of the alternate Publisher to drop as the alternate synchronization partner. *@alternate_publisher* is **sysname**, with no default.

#### [ @alternate_publisher_db = ] N'*alternate_publisher_db*'

The name of the publication database to drop as the alternate synchronization partner publication database. *@alternate_publisher_db* is **sysname**, with no default.

#### [ @alternate_publication = ] N'*alternate_publication*'

The name of the publication to drop as the alternate synchronization partner publication. *@alternate_publication* is **sysname**, with no default.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_dropmergealternatepublisher` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role or the **db_owner** fixed database role can execute `sp_dropmergelternatepublisher`.

## Related content

- [sp_addmergealternatepublisher (Transact-SQL)](sp-addmergealternatepublisher-transact-sql.md)
