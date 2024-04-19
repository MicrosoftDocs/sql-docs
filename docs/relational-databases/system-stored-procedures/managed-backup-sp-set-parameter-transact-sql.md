---
title: "managed_backup.sp_set_parameter (Transact-SQL)"
description: "Sets the value of the specified Smart Admin system parameter."
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/31/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_set_parameter_TSQL"
  - "sp_set_parameter"
  - "smart_admin.sp_set_parameter"
  - "smart_admin.sp_set_parameter_TSQL"
helpviewer_keywords:
  - "sp_set_parameter"
  - "smart_admin.sp_set_parameter"
dev_langs:
  - "TSQL"
---
# managed_backup.sp_set_parameter (Transact-SQL)

[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

Sets the value of the specified Smart Admin system parameter.

The available parameters are related to [!INCLUDE [ss-managed-backup](../../includes/ss-managed-backup-md.md)]. These parameters are used to set the email notifications, enable specific extended events, and enable user set policy based management policies. You must specify the parameter name and the parameter value pairs.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
EXEC managed_backup.sp_set_parameter
    [ @parameter_name = ] {
        N'SSMBackup2WANotificationEmailIds'
        | N'SSMBackup2WAEnableUserDefinedPolicy'
        | N'SSMBackup2WADebugXevent'
        | N'FileRetentionDebugXevent'
        | N'StorageOperationDebugXevent'
    }
    , [ @parameter_value = ] N'parameter_value'
[ ; ]
```

## Arguments

#### [ @parameter_name = ] N'*parameter_name*'

The name of the parameter you want to set the value for. *@parameter_name* is **nvarchar(128)**. The available parameter names are:

- `SSMBackup2WANotificationEmailIds`
- `SSMBackup2WADebugXevent`
- `SSMBackup2WAEnableUserDefinedPolicy`
- `FileRetentionDebugXevent`
- `StorageOperationDebugXevent`.

#### [ @parameter_value = ] N'*parameter_value*'

The value for the parameter you want to set. *@parameter_value* is **nvarchar(128)**. The following table shows allowed parameter name and value pairs:

| *@parameter_name* | *@parameter_value* |
| --- | --- |
| 'SSMBackup2WANotificationEmailIds' | 'email' |
| 'SSMBackup2WAEnableUserDefinedPolicy' | { 'true' \| 'false' }
| 'SSMBackup2WADebugXevent' | { 'true' \| 'false' }
| 'FileRetentionDebugXevent' | { 'true' \| 'false' }
| 'StorageOperationDebugXevent' = { 'true' \| 'false' } | N/A |

## Return code values

`0` (success) or `1` (failure).

## Permissions

Requires EXECUTE permissions on the `managed_backup.sp_set_parameter` stored procedure.

## Examples

The following examples enable operational and debug extended events.

```sql
-- Enable operational events
USE msdb;
GO
EXEC managed_backup.sp_set_parameter N'FileRetentionOperationalXevent', N'True';

-- Enable debug events
USE msdb;
GO
EXEC managed_backup.sp_set_parameter N'FileRetentionDebugXevent', N'True';
```

The following example enables email notifications of errors and warnings, and sets the email address for sending notifications:

```sql
USE msdb;
GO
EXEC managed_backup.sp_set_parameter @parameter_name = 'SSMBackup2WANotificationEmailIds', @parameter_value = '<email address>';
```
