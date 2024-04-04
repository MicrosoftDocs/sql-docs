---
title: "sys.sp_rda_reauthorize_db (Transact-SQL)"
description: Restores the authenticated connection between a local database enabled for Stretch and the remote database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/26/2023
ms.service: sql
ms.subservice: stored-procedures
ms.topic: "reference"
f1_keywords:
  - "sp_rda_reauthorize_db"
  - "sp_rda_reauthorize_db_TSQL"
helpviewer_keywords:
  - "sys.sp_rda_reauthorize_db stored procedure"
dev_langs:
  - "TSQL"
---
# sys.sp_rda_reauthorize_db (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Restores the authenticated connection between a local database enabled for Stretch and the remote database.

> [!IMPORTANT]  
> [!INCLUDE [stretch-database-deprecation](../../includes/stretch-database-deprecation.md)]

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_rda_reauthorize_db
    [ @credential = ] credential
      , [ @with_copy = ] with_copy
    [ , [ @azure_servername = ] azure_servername
      , [ @azure_databasename = ] azure_databasename ]
[ ; ]
```

## Arguments

#### [ @credential = ] N'*credential*'

The database scoped credential associated with the local Stretch-enabled database. *@credential* is **sysname**.

#### [ @with_copy = ] *with_copy*

Specifies whether to make a copy of the remote data and connect to the copy (recommended). *@with_copy* is **bit**.

#### [ @azure_servername = ] *azure_servername*

Specifies the name of the Azure server that contains the remote data. *@azure_servername* is **sysname**.

#### [ @azure_databasename = ] *azure_databasename*

Specifies the name of the Azure database that contains the remote data. *@azure_databasename* is **sysname**.

## Return code values

`0` (success) or `> 0` (failure).

## Permissions

Requires **db_owner** permissions.

## Remarks

When you run [sys.sp_rda_reauthorize_db (Transact-SQL)](sys-sp-rda-reauthorize-db-transact-sql.md) to reconnect to the remote Azure database, this operation automatically resets the query mode to `LOCAL_AND_REMOTE`, which is the default behavior for Stretch Database. That is, queries return results from both local and remote data.

## Examples

The following example restores the authenticated connection between a local database enabled for Stretch and the remote database. It makes a copy of the remote data (recommended) and connects to the new copy.

```sql
DECLARE @credentialName NVARCHAR(128);

SET @credentialName = N'<existing_database_scoped_credential_name>';

EXEC sp_rda_reauthorize_db
    @credential = @credentialName,
    @with_copy = 1;
```

## Related content

- [sys.sp_rda_deauthorize_db (Transact-SQL)](sys-sp-rda-deauthorize-db-transact-sql.md)
- [Stretch Database](../../sql-server/stretch-database/stretch-database.md)
