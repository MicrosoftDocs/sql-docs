---
title: "DROP EXTERNAL LIBRARY (Transact-SQL)"
description: DROP EXTERNAL LIBRARY (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: 08/26/2020
ms.service: sql
ms.subservice: machine-learning
ms.topic: reference
f1_keywords:
  - "DROP EXTERNAL LIBRARY"
  - "DROP_EXTERNAL_LIBRARY_TSQL"
helpviewer_keywords:
  - "DROP EXTERNAL LIBRARY"
dev_langs:
  - "TSQL"
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# DROP EXTERNAL LIBRARY (Transact-SQL)  
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

Deletes an existing package library. Package libraries are used by supported external runtimes, such as R, Python, or Java.

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
> [!NOTE]
> In SQL Server 2017, R language and Windows platform are supported. R, Python, and Java on the Windows and Linux platforms are supported in SQL Server 2019 and later.
::: moniker-end

::: moniker range="=azuresqldb-mi-current"
> [!NOTE]
> In Azure SQL Managed Instance, R and Python languages are supported.
::: moniker-end

## Syntax

```syntaxsql
DROP EXTERNAL LIBRARY library_name
[ AUTHORIZATION owner_name ];
```

### Arguments

**library_name**

Specifies the name of an existing package library.

Libraries are scoped to the user. Library names must be unique within the context of a specific user or owner.

**owner_name**

Specifies the name of the user or role that owns the external library.

Database owners can delete libraries created by other users.

## Permissions

To delete a library requires the privilege ALTER ANY EXTERNAL LIBRARY. By default, any database owner, or the owner of the object, can also delete an external library.

### Return values

An informational message is returned if the statement was successful.

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Remarks

Unlike other `DROP` statements in SQL Server, this statement supports specifying an optional authorization clause. This allows **dbo** or users in the **db_owner** role to drop a package library uploaded by a regular user in the database.

A number of packages, referred to as *system packages*, are pre-installed in a SQL instance. System packages cannot be added, updated, or removed by the user.

## Examples

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
Add the custom R package, `customPackage`, to a database:

```sql
CREATE EXTERNAL LIBRARY customPackage 
FROM (CONTENT = 'C:\temp\customPackage_v1.1.zip')
WITH (LANGUAGE = 'R');
GO
```
::: moniker-end

Delete the `customPackage` library.

```sql
DROP EXTERNAL LIBRARY customPackage;
```

## See also

[CREATE EXTERNAL LIBRARY (Transact-SQL)](create-external-library-transact-sql.md)  
[ALTER EXTERNAL LIBRARY (Transact-SQL)](alter-external-library-transact-sql.md)  
[sys.external_library_files](../../relational-databases/system-catalog-views/sys-external-library-files-transact-sql.md)  
[sys.external_libraries](../../relational-databases/system-catalog-views/sys-external-libraries-transact-sql.md)  
