---
title: "sp_dsninfo (Transact-SQL)"
description: Returns ODBC or OLE DB data source information from the Distributor associated with the current server.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 07/04/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_dsninfo"
  - "sp_dsninfo_TSQL"
helpviewer_keywords:
  - "sp_dsninfo"
dev_langs:
  - "TSQL"
---
# sp_dsninfo (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns ODBC or OLE DB data source information from the Distributor associated with the current server. This stored procedure is executed at the Distributor on any database.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dsninfo
    [ @dsn = ] 'dsn'
    [ , [ @infotype = ] 'infotype' ]
    [ , [ @login = ] 'login' ]
    [ , [ @password = ] 'password' ]
    [ , [ @dso_type = ] dso_type ]
[ ; ]
```

## Arguments

#### [ @dsn = ] '*dsn*'

The name of the ODBC DSN or OLE DB linked server. *@dsn* is **varchar(128)**, with no default.

#### [ @infotype = ] '*infotype*'

The type of information to return. If *@infotype* isn't specified or if `NULL` is specified, all information types are returned. *@infotype* is **varchar(128)**, and can be one of these values.

| Value | Description |
| --- | --- |
| `DBMS_NAME` | Specifies the data source vendor name. |
| `DBMS_VERSION` | Specifies the data source version. |
| `DATABASE_NAME` | Specifies the database name. |
| `SQL_SUBSCRIBER` | Specifies the data source can be a Subscriber. |

#### [ @login = ] '*login*'

The login for the data source. If the data source includes a login, specify `NULL` or omit the parameter. *@login* is **varchar(128)**, with a default of `NULL`.

#### [ @password = ] '*password*'

The password for the login. If the data source includes a login, specify `NULL` or omit the parameter. *@password* is **varchar(128)**, with a default of `NULL`.

#### [ @dso_type = ] *dso_type*

The data source type. *@dso_type* is **int**, and can be one of these values.

| Value | Description |
| --- | --- |
| `1` (default) | ODBC data source |
| `3` | OLE DB data source |

## Return code values

`0` (success) or `1` (failure).

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `Information Type` | **nvarchar(64)** | Information types such as `DBMS_NAME`, `DBMS_VERSION`, `DATABASE_NAME`, `SQL_SUBSCRIBER`. |
| `Value` | **nvarchar(512)** | Value of the associated information type. |

## Remarks

`sp_dsninfo` is used in all types of replication.

`sp_dsninfo` retrieves ODBC or OLE DB data source information that shows whether the database can be used for replication or querying.

## Permissions

Only members of the **sysadmin** fixed server role can execute `sp_dsninfo`.

## Related content

- [sp_enumdsn (Transact-SQL)](sp-enumdsn-transact-sql.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
