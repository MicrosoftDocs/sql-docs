---
title: "sp_OAMethod (Transact-SQL)"
description: sp_OAMethod calls a method of an OLE object.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_OAMethod"
  - "sp_OAMethod_TSQL"
helpviewer_keywords:
  - "sp_OAMethod"
dev_langs:
  - "TSQL"
---
# sp_OAMethod (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Calls a method of an OLE object.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_OAMethod objecttoken , methodname
    [ , returnvalue OUTPUT ]
    [ , [ @parametername = ] parameter [ OUTPUT ] [ ...n ] ]
[ ; ]
```

## Arguments

#### *objecttoken*

The object token of an OLE object that was previously created by using `sp_OACreate`.

#### *methodname*

The method name of the OLE object to call.

#### *returnvalue* OUTPUT

The return value of the method of the OLE object. If specified, it must be a local variable of the appropriate data type.

If the method returns a single value, either:

- specify a local variable for *returnvalue*, which returns the method return value in the local variable; or
- don't specify *returnvalue*, which returns the method return value to the client as a single-column, single-row result set.

If the method return value is an OLE object, *returnvalue* must be a local variable of data type **int**. An object token is stored in the local variable, and this object token can be used with other OLE Automation stored procedures.

When the method return value is an array, if *returnvalue* is specified, it's set to `NULL`.

An error is raised when any one of the following occurs:

- *returnvalue* is specified, but the method doesn't return a value.
- The method returns an array with more than two dimensions.
- The method returns an array as an output parameter.

#### [ @parametername = ] parameter [ OUTPUT ]

A method parameter. If specified, *parameter* must be a value of the appropriate data type.

To obtain the return value of an output parameter, *parameter* must be a local variable of the appropriate data type, and `OUTPUT` must be specified. If a constant parameter is specified, or if `OUTPUT` isn't specified, any return value from an output parameter is ignored.

If specified, *parametername* must be the name of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)] named parameter. *@parametername* isn't a [!INCLUDE [tsql](../../includes/tsql-md.md)] local variable. The at sign (`@`) is removed, and *parametername* is passed to the OLE object as the parameter name. All named parameters must be specified after all positional parameters are specified.

#### *n*

A placeholder indicating that multiple parameters can be specified.

> [!NOTE]  
> *@parametername* can be a named parameter because it's part of the specified method and is passed through to the object. The other parameters for this stored procedure are specified by position, not name.

## Return code values

`0` (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.

For more information about HRESULT return codes, [OLE automation return codes and error information](../stored-procedures/ole-automation-return-codes-and-error-information.md).

## Result set

If the method return value is an array with one or two dimensions, the array is returned to the client as a result set:

- A one-dimensional array is returned to the client as a single-row result set with as many columns as there are elements in the array. In other words, the array is returned as (columns).

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

### A. Call a method

The following example calls the `Connect` method of the previously created **SQLServer** object.

```sql
EXEC @hr = sp_OAMethod @object,
    'Connect',
    NULL,
    'my_server',
    'my_login',
    'my_password';

IF @hr <> 0
BEGIN
    EXEC sp_OAGetErrorInfo @object

    RETURN
END;
```

### B. Get a property

The following example gets the `HostName` property (of the previously created `SQLServer` object) and stores it in a local variable.

```sql
DECLARE @property VARCHAR(255);

EXEC @hr = sp_OAMethod @object,
    'HostName',
    @property OUTPUT;

IF @hr <> 0
BEGIN
    EXEC sp_OAGetErrorInfo @object

    RETURN
END;

PRINT @property;
```

## Related content

- [OLE Automation stored procedures (Transact-SQL)](ole-automation-stored-procedures-transact-sql.md)
- [OLE Automation Sample Script](../stored-procedures/ole-automation-sample-script.md)
