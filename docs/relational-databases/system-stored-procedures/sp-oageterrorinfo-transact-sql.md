---
title: "sp_OAGetErrorInfo (Transact-SQL)"
description: sp_OAGetErrorInfo obtains OLE Automation error information.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_OAGetErrorInfo_TSQL"
  - "sp_OAGetErrorInfo"
helpviewer_keywords:
  - "sp_OAGetErrorInfo"
dev_langs:
  - "TSQL"
---
# sp_OAGetErrorInfo (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Obtains OLE Automation error information.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_OAGetErrorInfo [ objecttoken ]
    [ , source OUTPUT ]
    [ , description OUTPUT ]
    [ , helpfile OUTPUT ]
    [ , helpid OUTPUT ]
[ ; ]
```

## Arguments

#### *objecttoken*

Either the object token of an OLE object that was previously created by using `sp_OACreate`, or `NULL`. If *objecttoken* is specified, error information for that object is returned. If `NULL` is specified, the error information for the entire batch is returned.

#### *source* OUTPUT

The source of the error information. If specified, it must be a local **char**, **nchar**, **varchar**, or **nvarchar** variable. The return value is truncated to fit the local variable if necessary.

#### *description* OUTPUT

The description of the error. If specified, it must be a local **char**, **nchar**, **varchar**, or **nvarchar** variable. The return value is truncated to fit the local variable if necessary.

#### *helpfile* OUTPUT

The help file for the OLE object. If specified, it must be a local **char**, **nchar**, **varchar**, or **nvarchar** variable. The return value is truncated to fit the local variable if necessary.

#### *helpid* OUTPUT

The help file context ID. If specified, it must be a local **int** variable.

> [!NOTE]  
> The parameters for this stored procedure are specified by position, not name.

## Return code values

`0` (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.

For more information about HRESULT return codes, see [OLE automation return codes and error information](../stored-procedures/ole-automation-return-codes-and-error-information.md).

## Result set

If no output parameters are specified, the error information is returned to the client as a result set.

| Column name | Data type | Description |
| --- | --- | --- |
| `Error` | **binary(4)** | Binary representation of the error number. |
| `Source` | **nvarchar(nn)** | Source of the error. |
| `Description` | **nvarchar(nn)** | Description of the error. |
| `Helpfile` | **nvarchar(nn)** | Help file for the source. |
| `HelpID` | **int** | Help context ID in the Help source file. |

## Remarks

Each call to an OLE Automation stored procedure (except `sp_OAGetErrorInfo`) resets the error information; therefore, `sp_OAGetErrorInfo` obtains error information only for the most recent OLE Automation stored procedure call. Because `sp_OAGetErrorInfo` doesn't reset the error information, it can be called multiple times to get the same error information.

The following table lists OLE Automation errors and their common causes.

| Error and HRESULT | Common cause |
| --- | --- |
| **Bad variable type (0x80020008)** | Data type of a [!INCLUDE [tsql](../../includes/tsql-md.md)] value passed as a method parameter didn't match the [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)] data type of the method parameter, or a `NULL` value was passed as a method parameter. |
| **Unknown name (0x8002006)** | Specified property or method name wasn't found for the specified object. |
| **Invalid class string (0x800401f3)** | Specified ProgID or CLSID isn't registered as an OLE object on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Custom OLE automation servers must be registered before they can be instantiated using `sp_OACreate`. You can register servers using the `Regsvr32.exe` utility for in-process (`.dll`) servers, or the `/REGSERVER` command-line switch for local (`.exe`) servers. |
| **Server execution failed (0x80080005)** | Specified OLE object is registered as a local OLE server (`.exe` file) but the .exe file couldn't be found or started. |
| **The specified module couldn't be found (0x8007007e)** | Specified OLE object is registered as an in-process OLE server (`.dll` file), but the .dll file couldn't be found or loaded. |
| **Type mismatch (0x80020005)** | Data type of a [!INCLUDE [tsql](../../includes/tsql-md.md)] local variable that is used to store a returned property value or a method return value didn't match the [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)] data type of the property or method return value. Or, the return value of a property or a method was requested, but it doesn't return a value. |
| **Data type or value of the 'context' parameter of `sp_OACreate` is invalid. (0x8004275B)** | The value of the context parameter should be one of: 1, 4, or 5. |

For more information about processing HRESULT return codes, see [OLE automation return codes and error information](../stored-procedures/ole-automation-return-codes-and-error-information.md).

## Permissions

Requires membership in the **sysadmin** fixed server role or execute permission directly on this stored procedure. The [Ole Automation Procedures](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md) server configuration option must be enabled to use any system procedure related to OLE Automation.

## Examples

The following example displays OLE Automation error information.

```sql
DECLARE @output VARCHAR(255);
DECLARE @hr INT;
DECLARE @source VARCHAR(255);
DECLARE @description VARCHAR(255);

PRINT 'OLE Automation Error Information';

EXEC @hr = sp_OAGetErrorInfo @object,
    @source OUTPUT,
    @description OUTPUT;

IF @hr = 0
BEGIN
    SELECT @output = '  Source: ' + @source;
    PRINT @output;
    SELECT @output = '  Description: ' + @description;
    PRINT @output;
END
ELSE
BEGIN
    PRINT '  sp_OAGetErrorInfo failed.'

    RETURN
END;
```

## Related content

- [OLE Automation stored procedures (Transact-SQL)](ole-automation-stored-procedures-transact-sql.md)
- [OLE Automation Sample Script](../stored-procedures/ole-automation-sample-script.md)
