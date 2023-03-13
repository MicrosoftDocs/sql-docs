---
title: "SUSER_NAME (Transact-SQL)"
description: "SUSER_NAME returns the login identification name of the user."
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 12/21/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "SUSER_NAME"
  - "SUSER_NAME_TSQL"
helpviewer_keywords:
  - "security identification names [SQL Server]"
  - "logins [SQL Server], users"
  - "identification names for logins [SQL Server]"
  - "users [SQL Server], logins"
  - "SUSER_NAME function"
  - "logins [SQL Server], names"
  - "names [SQL Server], logins"
dev_langs:
  - "TSQL"
monikerRange: "= azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SUSER_NAME (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi-asa-svrless-poolonly.md)]

Returns the login identification name of the user.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SUSER_NAME ( [ server_user_id ] )
```

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *server_user_id*

The login identification number of the user. *server_user_id*, which is optional, is **int**. *server_user_id* can be the login identification number of any [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] login or Windows user or group that has permission to connect to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. When *server_user_id* isn't specified, the login identification name for the current user is returned. If the parameter contains the word NULL, it will return NULL.

## Return type

**nvarchar(128)**

## Remarks

`SUSER_NAME` returns a login name only for a login that has an entry in the `syslogins` system table.

`SUSER_NAME` can be used in a select list, in a WHERE clause, and anywhere an expression is allowed. Use parentheses after `SUSER_NAME`, even if no parameter is specified.

> [!NOTE]  
> Although the `SUSER_NAME` function is supported on Azure SQL Database, using EXECUTE AS with `SUSER_NAME` is not supported on Azure SQL Database.

## Examples

The following example returns the login identification name of the user with a login identification number of `1`.

```sql
SELECT SUSER_NAME(1);
```

## See also

- [SUSER_ID (Transact-SQL)](../../t-sql/functions/suser-id-transact-sql.md)
- [Principals (Database Engine)](../../relational-databases/security/authentication-access/principals-database-engine.md)
