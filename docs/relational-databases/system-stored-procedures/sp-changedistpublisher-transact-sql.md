---
title: "sp_changedistpublisher (Transact-SQL)"
description: "Changes the properties of the distribution Publisher."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_changedistpublisher_TSQL"
  - "sp_changedistpublisher"
helpviewer_keywords:
  - "sp_changedistpublisher"
dev_langs:
  - "TSQL"
---
# sp_changedistpublisher (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Changes the properties of the distribution Publisher. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_changedistpublisher
    [ @publisher = ] N'publisher'
    [ , [ @property = ] N'property' ]
    [ , [ @value = ] N'value' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The Publisher name. *@publisher* is **sysname**, with no default.

#### [ @property = ] N'*property*'

A property to change for the given Publisher. *@property* is **sysname**, and can be one of the properties in the table listed under *@value*.

#### [ @value = ] N'*value*'

The value for the given property. *@value* is **nvarchar(255)**, and can be one of the values in the following table.

[!INCLUDE [Azure SQL Database link](../../includes/azure-sql-db-repl-for-more-information.md)]

This table describes the properties of Publishers and the values for those properties.

| Property | Values | Description |
| --- | --- | --- |
| `active` | `true` | Activates the Publisher. |
| | `false` | Deactivates the publisher |
| `distribution_db` | | Name of the distribution database. |
| `login` | | Login name. |
| `password` | | Strong password for the supplied login. |
| `security_mode` <sup>1</sup> | `1` | Use Windows Authentication when connecting to the Publisher. |
| | `0` | Use [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication when connecting to the Publisher. |
| `working_directory` | | Working directory used to store data and schema files for the publication. |
| `NULL` (default) | | All available *property* options are printed. |
| `storage_connection_string` | Access key | The access key for the working directory when the database is Azure SQL Managed Instance. |

<sup>1</sup> This can't be changed for a non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] publisher.

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_changedistpublisher` is used in all types of replication.

If you're changing the `working_directory` property and the `storage_connection_string` property has to be updated, execute the stored procedure separately by updating the `working_directory` property, followed by updating the `storage_connection_string` property, or vice-versa.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_changedistpublisher`.

## Related content

- [View and Modify Distributor and Publisher Properties](../replication/view-and-modify-distributor-and-publisher-properties.md)
- [sp_adddistpublisher (Transact-SQL)](sp-adddistpublisher-transact-sql.md)
- [sp_dropdistpublisher (Transact-SQL)](sp-dropdistpublisher-transact-sql.md)
- [sp_helpdistpublisher (Transact-SQL)](sp-helpdistpublisher-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
