---
title: "sp_OASetProperty (Transact-SQL)"
description: sp_OASetProperty sets a property of an OLE object to a new value.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_OASetProperty"
  - "sp_OASetProperty_TSQL"
helpviewer_keywords:
  - "sp_OASetProperty"
dev_langs:
  - "TSQL"
---
# sp_OASetProperty (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Sets a property of an OLE object to a new value.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_OASetProperty objecttoken
    , propertyname
    , newvalue
    [ , index... ]
[ ; ]
```

## Arguments

#### *objecttoken*

The object token of an OLE object previously created by `sp_OACreate`.

#### *propertyname*

The property name of the OLE object to set to a new value.

#### *newvalue*

The new value of the property, and must be a value of the appropriate data type.

#### *index*

An index parameter. If specified, *index* must be a value of the appropriate data type.

Some properties have parameters. These properties are called indexed properties, and the parameters are called index parameters. A property can have multiple index parameters.

> [!NOTE]  
> The parameters for this stored procedure are specified by position, not name.

## Return code values

`0` (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.

For more information about HRESULT return codes, see [OLE automation return codes and error information](../stored-procedures/ole-automation-return-codes-and-error-information.md).

## Permissions

Requires membership in the **sysadmin** fixed server role or execute permission directly on this stored procedure. The [Ole Automation Procedures](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md) server configuration option must be enabled to use any system procedure related to OLE Automation.

## Examples

The following example sets the `HostName` property (of the previously created `SQLServer` object) to a new value.

```sql
EXEC @hr = sp_OASetProperty @object,
    'HostName',
    'Gizmo';

IF @hr <> 0
BEGIN
    EXEC sp_OAGetErrorInfo @object

    RETURN
END;
```

## Related content

- [OLE Automation stored procedures (Transact-SQL)](ole-automation-stored-procedures-transact-sql.md)
- [OLE Automation Sample Script](../stored-procedures/ole-automation-sample-script.md)
