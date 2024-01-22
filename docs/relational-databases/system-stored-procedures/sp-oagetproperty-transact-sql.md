---
title: "sp_OAGetProperty (Transact-SQL)"
description: sp_OAGetProperty gets a property value of an OLE object.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_OAGetProperty_TSQL"
  - "sp_OAGetProperty"
helpviewer_keywords:
  - "sp_OAGetProperty"
dev_langs:
  - "TSQL"
---
# sp_OAGetProperty (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Gets a property value of an OLE object.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_OAGetProperty objecttoken , propertyname
    [ , propertyvalue OUTPUT ]
    [ , index... ]
[ ; ]
```

## Arguments

#### *objecttoken*

The object token of an OLE object that was previously created by using `sp_OACreate`.

#### *propertyname*

The property name of the OLE object to return.

#### *propertyvalue* OUTPUT

The returned property value. If specified, it must be a local variable of the appropriate data type.

If the property returns an OLE object, *propertyvalue* must be a local variable of data type **int**. An object token is stored in the local variable, and this object token can be used with other OLE Automation stored procedures.

If the property returns a single value, either:

- specify a local variable for *propertyvalue*, which returns the property value in the local variable; or
- don't specify *propertyvalue*, which returns the property value to the client as a single-column, single-row result set.

When the property returns an array, if *propertyvalue* is specified, it's set to `NULL`.

If *propertyvalue* is specified, but the property doesn't return a value, an error occurs. If the property returns an array with more than two dimensions, an error occurs.

#### *index*

An index parameter. If specified, *index* must be a value of the appropriate data type.

Some properties have parameters. These properties are called indexed properties, and the parameters are called index parameters. A property can have multiple index parameters.

> [!NOTE]  
> The parameters for this stored procedure are specified by position, not name.

## Return code values

`0` (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.

For more information about HRESULT return codes, see [OLE automation return codes and error information](../stored-procedures/ole-automation-return-codes-and-error-information.md).

## Result set

If the property returns an array with one or two dimensions, the array is returned to the client as a result set:

- A one-dimensional array is returned to the client as a single-row result set with as many columns as there are elements in the array. In other words, the array is returned as columns.

- A two-dimensional array is returned to the client as a result set with as many columns as there are elements in the first dimension of the array and with as many rows as there are elements in the second dimension of the array. In other words, the array is returned as (columns, rows).

When a property return value or method return value is an array, `sp_OAGetProperty` or `sp_OAMethod` returns a result set to the client. (Method output parameters can't be arrays.) These procedures scan all the data values in the array to determine the appropriate [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data types and data lengths to use for each column in the result set. For a particular column, these procedures use the data type and length required to represent all data values in that column.

When all data values in a column share the same data type, that data type is used for the whole column. When data values in a column are of different data types, the data type of the whole column is chosen based on the following chart.

| | int | float | money | datetime | varchar | nvarchar |
| --- | --- | --- | --- | --- | --- | --- |
| **int** | **int** | **float** | **money** | **varchar** | **varchar** | **nvarchar** |
| **float** | **float** | **float** | **money** | **varchar** | **varchar** | **nvarchar** |
| **money** | **money** | **money** | **money** | **varchar** | **varchar** | **nvarchar** |
| **datetime** | **varchar** | **varchar** | **varchar** | **datetime** | **varchar** | **nvarchar** |
| **varchar** | **varchar** | **varchar** | **varchar** | **varchar** | **varchar** | **nvarchar** |
| **nvarchar** | **nvarchar** | **nvarchar** | **nvarchar** | **nvarchar** | **nvarchar** | **nvarchar** |

## Remarks

You can also use `sp_OAMethod` to get a property value.

## Permissions

Requires membership in the **sysadmin** fixed server role or execute permission directly on this stored procedure. The [Ole Automation Procedures](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md) server configuration option must be enabled to use any system procedure related to OLE Automation.

## Examples

### A. Use a local variable

The following example gets the `HostName` property (of the previously created `SQLServer` object) and stores it in a local variable.

```sql
DECLARE @property VARCHAR(255);

EXEC @hr = sp_OAGetProperty @object,
    'HostName',
    @property OUTPUT;

IF @hr <> 0
BEGIN
    EXEC sp_OAGetErrorInfo @object

    RETURN
END

PRINT @property;
```

### B. Use a result set

The following example gets the `HostName` property (of the previously created `SQLServer` object) and returns it to the client as a result set.

```sql
EXEC @hr = sp_OAGetProperty @object,
    'HostName';

IF @hr <> 0
BEGIN
    EXEC sp_OAGetErrorInfo @object

    RETURN
END;
```

## Related content

- [OLE Automation stored procedures (Transact-SQL)](ole-automation-stored-procedures-transact-sql.md)
- [OLE Automation Sample Script](../stored-procedures/ole-automation-sample-script.md)
