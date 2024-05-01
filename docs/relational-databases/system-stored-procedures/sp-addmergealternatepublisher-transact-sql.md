---
title: "sp_addmergealternatepublisher (Transact-SQL)"
description: Adds the ability for a Subscriber to use an alternate synchronization partner.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/23/2023
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_addmergealternatepublisher_TSQL"
  - "sp_addmergealternatepublisher"
helpviewer_keywords:
  - "sp_addmergealternatepublisher"
dev_langs:
  - "TSQL"
---
# sp_addmergealternatepublisher (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Adds the ability for a Subscriber to use an alternate synchronization partner. The publication properties must specify that Subscribers can synchronize with other Publishers. This stored procedure is executed at the Subscriber on the subscription database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addmergealternatepublisher
    [ @publisher = ] N'publisher'
    , [ @publisher_db = ] N'publisher_db'
    , [ @publication = ] N'publication'
    , [ @alternate_publisher = ] N'alternate_publisher'
    , [ @alternate_publisher_db = ] N'alternate_publisher_db'
    , [ @alternate_publication = ] N'alternate_publication'
    , [ @alternate_distributor = ] N'alternate_distributor'
    [ , [ @friendly_name = ] N'friendly_name' ]
    [ , [ @reserved = ] N'reserved' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the Publisher. *@publisher* is **sysname**, with no default.

#### [ @publisher_db = ] N'*publisher_db*'

The name of the publication database. *@publisher_db* is **sysname**, with no default.

#### [ @publication = ] N'*publication*'

The name of the publication. *@publication* is **sysname**, with no default.

#### [ @alternate_publisher = ] N'*alternate_publisher*'

The name of the alternate Publisher. *@alternate_publisher* is **sysname**, with no default.

#### [ @alternate_publisher_db = ] N'*alternate_publisher_db*'

The name of the publication database on the alternate publisher. *@alternate_publisher_db* is **sysname**, with no default.

#### [ @alternate_publication = ] N'*alternate_publication*'

The name of the publication on the alternate synchronization partner. *@alternate_publication* is **sysname**, with no default.

#### [ @alternate_distributor = ] N'*alternate_distributor*'

The name of the Distributor for the alternate synchronization partner. *@alternate_distributor* is **sysname**, with no default.

#### [ @friendly_name = ] N'*friendly_name*'

A display name by which the association of Publisher, publication, and Distributor that makes up an alternate synchronization partner can be identified. *@friendly_name* is **nvarchar(255)**, with a default of `NULL`.

#### [ @reserved = ] N'*reserved*'

[!INCLUDE [ssInternalOnly](../../includes/ssinternalonly-md.md)]

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_addmergealternatepublisher` is used in merge replication.

## Permissions

Only members of the **sysadmin** fixed server role or **db_owner** fixed database role can execute `sp_addmergealternatepublisher`.

## Related content

- [sp_dropmergealternatepublisher (Transact-SQL)](sp-dropmergealternatepublisher-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
