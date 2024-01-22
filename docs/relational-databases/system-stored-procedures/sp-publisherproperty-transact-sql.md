---
title: "sp_publisherproperty (Transact-SQL)"
description: sp_publisherproperty displays or changes publisher properties for non-SQL Server Publishers.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "sp_publisherproperty"
  - "sp_publisherproperty_TSQL"
helpviewer_keywords:
  - "sp_publisherproperty"
dev_langs:
  - "TSQL"
---
# sp_publisherproperty (Transact-SQL)

[!INCLUDE [SQL Server SQL MI](../../includes/applies-to-version/sql-asdbmi.md)]

Displays or changes publisher properties for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers. This stored procedure is executed at the Distributor.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_publisherproperty
    [ @publisher = ] N'publisher'
    [ , [ @propertyname = ] N'propertyname' ]
    [ , [ @propertyvalue = ] N'propertyvalue' ]
[ ; ]
```

## Arguments

#### [ @publisher = ] N'*publisher*'

The name of the heterogeneous Publisher. *@publisher* is **sysname**, with no default.

#### [ @propertyname = ] N'*propertyname*'

The name of the property being set. *@propertyname* is **sysname**, and can be one of the following values.

| Value | Description |
| --- | --- |
| `xactsetbatching` | Specifies whether transactions at the Publisher are grouped into transactionally consistent sets (Xactsets) for subsequent processing. A value of `enabled` means that Xactsets can be created, which is the default. A value of `disabled` means that existing Xactsets are processed by no new Xactsets are created. |
| `xactsetjob` | Specifies whether the Xactset job is enabled for the creation of Xactsets. A value of `enabled` means that the Xactset job runs periodically to create Xactsets at the publisher. A value of `disabled` means that the Xactsets are only created when the Log Reader Agent polls the Publisher for changes. |
| `xactsetjobinterval` | Interval between executions of the Xactset job, in minutes. |

When *@propertyname* is omitted, all settable properties are returned.

#### [ @propertyvalue = ] N'*propertyvalue*'

The new value for the property setting. *@propertyvalue* is **sysname**, with a default of `NULL`. When *@propertyvalue* is omitted, the current setting for the property is returned.

## Result set

| Column name | Data type | Description |
| --- | --- | --- |
| `propertyname` | **sysname** | Returns the following publication properties that can be set:<br /><br />`xactsetbatching`<br />`xactsetjob`<br />`xactsetjobinterval` |
| `propertyvalue` | **sysname** | The current setting for the property in the `propertyname` column. |

## Return code values

`0` (success) or `1` (failure).

## Remarks

`sp_publisherproperty` is used in transactional replication for non-[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Publishers.

When only *@publisher* is specified, the result set includes the current settings for all properties that can be set.

When *@propertyname* is specified, only the named property appears in the result set.

When all parameters are specified, the property is changed and a result set isn't returned.

When changing the `xactsetjobinterval` property for a running job, you must restart the job for the new interval to take effect.

## Permissions

Only members of the **sysadmin** fixed server role at the Distributor can execute `sp_publisherproperty`.

## Related content

- [Configure the Transaction Set Job for an Oracle Publisher](../replication/administration/configure-the-transaction-set-job-for-an-oracle-publisher.md)
- [System stored procedures (Transact-SQL)](system-stored-procedures-transact-sql.md)
