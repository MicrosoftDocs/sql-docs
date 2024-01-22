---
title: "sp_enumdsn (Transact-SQL)"
description: Returns a list of all defined ODBC and OLE DB data source names for a server running under a specific Windows user account.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/16/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_enumdsn"
  - "sp_enumdsn_TSQL"
helpviewer_keywords:
  - "sp_enumdsn"
dev_langs:
  - "TSQL"
---
# sp_enumdsn (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns a list of all defined ODBC and OLE DB data source names for a server running under a specific Windows user account. This stored procedure is executed at the Publisher on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_enumdsn
[ ; ]
```

## Arguments

None.

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `Data Source Name` | **sysname** | Name of the data source. |
| `Description` | **varchar(255)** | Description of the data source. |
| `Type` | **int** | Type of data source:<br /><br />`1` = ODBC DSN<br />`3` = OLE DB data source |
| `Provider Name` | **varchar(255)** | Name of the OLE DB provider. Value is `NULL` for ODBC DSN. |

## Remarks

Every [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service has a user context. A user context is a set of Registry entries that includes the definitions of the ODBC data sources for the user. The user context is provided by the username under which the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is running.

For example, if the server is running under the system account user context, the data source names (DSNs) that are returned are all system DSNs that are associated with the system account. If the server is running under a private user account, only the DSNs defined for that private account of that user is returned.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_enumdsn`.

## Related content

- [sp_dsninfo (Transact-SQL)](sp-dsninfo-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
