---
title: "sp_OACreate (Transact-SQL)"
description: sp_OACreate creates an instance of an OLE object.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/21/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_OACreate"
  - "sp_OACreate_TSQL"
helpviewer_keywords:
  - "sp_OACreate"
dev_langs:
  - "TSQL"
---
# sp_OACreate (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Creates an instance of an OLE object.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_OACreate { progid | clsid }
    , objecttoken OUTPUT
    [ , context ]
[ ; ]
```

## Arguments

#### *progid*

The programmatic identifier (ProgID) of the OLE object to create. This character string describes the class of the OLE object and has the form: `<OLEComponent>.<Object>`.

*OLEComponent* is the component name of the OLE Automation server, and *Object* is the name of the OLE object. The specified OLE object must be valid and must support the `IDispatch` interface.

For example, `SQLDMO.SQLServer` is the ProgID of the SQL-DMO `SQLServer` object. SQL-DMO has a component name of SQLDMO, the `SQLServer` object is valid, and (like all SQL-DMO objects) the `SQLServer` object supports `IDispatch`.

#### *clsid*

The class identifier (CLSID) of the OLE object to create. This character string describes the class of the OLE object and has the form: `{<nnnnnnnn-nnnn-nnnn-nnnn-nnnnnnnnnnnn>}`. The specified OLE object must be valid and must support the `IDispatch` interface.

For example, `{00026BA1-0000-0000-C000-000000000046}` is the CLSID of the SQL-DMO `SQLServer` object.

#### *objecttoken* OUTPUT

The returned object token, and must be a local variable of data type **int**. This object token identifies the created OLE object and is used in calls to the other OLE Automation stored procedures.

#### *context*

Specifies the execution context in which the newly created OLE object runs. If specified, this value must be one of the following options:

- `1` = In-process (`.dll`) OLE server only
- `4` = Local (`.exe`) OLE server only
- `5` = Both in-process and local OLE server allowed

If not specified, the default value is `5`. This value is passed as the *dwClsContext* parameter of the call to `CoCreateInstance`.

If an in-process OLE server is allowed (by using a context value of `1` or `5` or by not specifying a context value), it has access to memory and other resources owned by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. An in-process OLE server might damage [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] memory or resources and cause unpredictable results, such as a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] access violation.

When you specify a context value of `4`, a local OLE server doesn't have access to any [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] resources, and it can't damage [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] memory or resources.

> [!NOTE]  
> The parameters for this stored procedure are specified by position, not by name.

## Return code values

`0` (success) or a nonzero number (failure) that is the integer value of the HRESULT returned by the OLE Automation object.

For more information about HRESULT return codes, see [OLE automation return codes and error information](../stored-procedures/ole-automation-return-codes-and-error-information.md).

## Remarks

If OLE automation procedures are enabled, a call to `sp_OACreate` starts the OLE Automation shared execution environment. For more information about enabling OLE automation, see [Ole Automation Procedures (server configuration option)](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md).

The created OLE object is automatically destroyed at the end of the [!INCLUDE [tsql](../../includes/tsql-md.md)] statement batch.

## Permissions

Requires membership in the **sysadmin** fixed server role or execute permission directly on this stored procedure. The [Ole Automation Procedures](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md) server configuration option must be enabled to use any system procedure related to OLE Automation.

## Examples

### A. Use ProgID

The following example creates a SQL-DMO `SQLServer` object by using its ProgID.

```sql
DECLARE @object INT;
DECLARE @hr INT;
DECLARE @src VARCHAR(255),
    @desc VARCHAR(255);

EXEC @hr = sp_OACreate 'SQLDMO.SQLServer',
    @object OUTPUT;

IF @hr <> 0
BEGIN
    EXEC sp_OAGetErrorInfo @object,
        @src OUTPUT,
        @desc OUTPUT

    RAISERROR ('Error Creating COM Component 0x%x, %s, %s', 16, 1, @hr, @src, @desc);

    RETURN
END;
GO
```

### B. Use CLSID

The following example creates a SQL-DMO `SQLServer` object by using its CLSID.

```sql
DECLARE @object INT;
DECLARE @hr INT;
DECLARE @src VARCHAR(255),
    @desc VARCHAR(255);

EXEC @hr = sp_OACreate '{00026BA1-0000-0000-C000-000000000046}',
    @object OUTPUT;

IF @hr <> 0
BEGIN
    EXEC sp_OAGetErrorInfo @object,
        @src OUTPUT,
        @desc OUTPUT

    RAISERROR ('Error Creating COM Component 0x%x, %s, %s', 16, 1, @hr, @src, @desc);

    RETURN
END;
GO
```

## Related content

- [OLE Automation stored procedures (Transact-SQL)](ole-automation-stored-procedures-transact-sql.md)
- [Ole Automation Procedures (server configuration option)](../../database-engine/configure-windows/ole-automation-procedures-server-configuration-option.md)
- [OLE Automation Sample Script](../stored-procedures/ole-automation-sample-script.md)
