---
title: "Object hierarchy syntax (Transact-SQL)"
description: "Object hierarchy syntax (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/24/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
helpviewer_keywords:
  - "objects [SQL Server], hierarchy syntax"
dev_langs:
  - "TSQL"
---
# Object hierarchy syntax (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The *propertyname* parameter of `sp_OAGetProperty` and `sp_OASetProperty` and the *methodname* parameter of `sp_OAMethod` support an object hierarchy syntax similar to that of [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)]. When this special syntax is used, these parameters have the following general form.

## Syntax

```vbscript
TraversedObject.PropertyOrMethod
```

## Arguments

#### *TraversedObject*

An OLE object in the hierarchy under the *objecttoken* specified in the stored procedure. Use [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)] syntax to specify a series of collections, object properties, and methods that return objects. Each object specifier in the series must be separated by a period (`.`).

An item in the series can be the name of a collection. Use this syntax to specify a collection:

```vbscript
Collection("item")
```

The double quotation marks (`"`) are required. The [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)] exclamation point (`!`) syntax for collections isn't supported.

#### *PropertyOrMethod*

The name of a property or method of the *TraversedObject*.

To specify all index or method parameters inside the parentheses (causing all index or method parameters of `sp_OAGetProperty`, `sp_OASetProperty`, or `sp_OAMethod` to be ignored) use the following syntax:

```vbscript
PropertyOrMethod ( [ ParameterName := ] "parameter" [ , ... ] )
```

The double quotation marks (`"`) are required. All named parameters must be specified after all positional parameters are specified.

## Remarks

If *TraversedObject* isn't specified, *PropertyOrMethod* is required.

If *PropertyOrMethod* isn't specified, the *TraversedObject* is returned as an object token output parameter from the OLE Automation stored procedure.

If *PropertyOrMethod* is specified, the property or method of the *TraversedObject* is called. The property value or method return value is returned as an output parameter from the OLE Automation stored procedure.

If any item in the *TraversedObject* list doesn't return an OLE object, an error is raised.

For more information about [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)] OLE object syntax, see the [!INCLUDE [visual-basic](../../includes/visual-basic-md.md)] documentation.

For more information about `HRESULT` return codes, see [sp_OACreate (Transact-SQL)](sp-oacreate-transact-sql.md).

## Examples

The following are examples of object hierarchy syntax that use a SQL-DMO SQLServer object.

```sql
-- Get the AdventureWorks2022 Person.Address Table object.
EXEC @hr = sp_OAGetProperty @object,
   'Databases("AdventureWorks2022").Tables("Person.Address")',
   @table OUT

-- Get the Rows property of the AdventureWorks2022 Person.Address table.
EXEC @hr = sp_OAGetProperty @object,
   'Databases("AdventureWorks2022").Tables("Person.Address").Rows',
   @rows OUT

-- Call the CheckTable method to validate the
-- AdventureWorks2022 Person.Address table.
EXEC @hr = sp_OAMethod @object,
   'Databases("AdventureWorks2022").Tables("Person.Address").CheckTable',
   @checkoutput OUT
```

## Related content

- [OLE Automation Sample Script](../stored-procedures/ole-automation-sample-script.md)
- [OLE Automation stored procedures (Transact-SQL)](ole-automation-stored-procedures-transact-sql.md)
