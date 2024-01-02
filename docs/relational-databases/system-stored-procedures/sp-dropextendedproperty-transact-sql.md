---
title: "sp_dropextendedproperty (Transact-SQL)"
description: sp_dropextendedproperty drops an existing extended property.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/27/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_dropextendedproperty_TSQL"
  - "sp_dropextendedproperty"
helpviewer_keywords:
  - "sp_dropextendedproperty"
dev_langs:
  - "TSQL"
---
# sp_dropextendedproperty (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Drops an existing extended property.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_dropextendedproperty
    [ @name = ] N'name'
    [ , [ @level0type = ] 'level0type' ]
    [ , [ @level0name = ] N'level0name' ]
    [ , [ @level1type = ] 'level1type' ]
    [ , [ @level1name = ] N'level1name' ]
    [ , [ @level2type = ] 'level2type' ]
    [ , [ @level2name = ] N'level2name' ]
[ ; ]
```

## Arguments

#### [ @name = ] N'*name*'

The name of the property to be dropped. *@name* is **sysname**, and can't be `NULL`.

#### [ @level0type = ] '*level0type*'

The name of the level 0 object type specified. *@level0type* is **varchar(128)**, with a default of `NULL`.

Valid inputs are `ASSEMBLY`, `CONTRACT`, `EVENT NOTIFICATION`, `FILEGROUP`, `MESSAGE TYPE`, `PARTITION FUNCTION`, `PARTITION SCHEME`, `REMOTE SERVICE BINDING`, `ROUTE`, `SCHEMA`, `SERVICE`, `USER`, `TRIGGER`, `TYPE`, and `NULL`.

> [!IMPORTANT]  
> `USER` and `TYPE` as level-0 types will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these features in new development work, and plan to modify applications that currently use these features. Use `SCHEMA` as the level 0 type instead of `USER`. For `TYPE`, use `SCHEMA` as the level 0 type and `TYPE` as the level 1 type.

#### [ @level0name = ] N'*level0name*'

The name of the level 0 object type specified. *@level0name* is **sysname**, with a default of `NULL`.

#### [ @level1type = ] '*level1type*'

The type of level 1 object. *@level1type* is **varchar(128)**, with a default of `NULL`.

Valid inputs are `AGGREGATE`, `DEFAULT`, `FUNCTION`, `LOGICAL FILE NAME`, `PROCEDURE`, `QUEUE`, `RULE`, `SYNONYM`, `TABLE`, `TABLE_TYPE`, `TYPE`, `VIEW`, `XML SCHEMA COLLECTION`, and `NULL`.

#### [ @level1name = ] N'*level1name*'

The name of the level 1 object type specified. *@level1name* is **sysname**, with a default of `NULL`.

#### [ @level2type = ] '*level2type*'

The type of level 2 object. *@level2type* is **varchar(128)**, with a default of `NULL`.

Valid inputs are `COLUMN`, `CONSTRAINT`, `EVENT NOTIFICATION`, `INDEX`, `PARAMETER`, `TRIGGER`, and `NULL`.

#### [ @level2name = ] N'*level2name*'

The name of the level 2 object type specified. *@level2name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

When you specify extended properties, the objects in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database are classified into three levels: 0, 1, and 2. Level 0 is the highest level and is defined as objects contained at the database scope. Level 1 objects are contained in a schema or user scope, and level 2 objects are contained by level 1 objects. Extended properties can be defined for objects at any of these levels. References to an object in one level must be qualified with the types and names of all higher level objects.

Given a valid property name *@name*, if all object types and names are `NULL` and a property exists on the current database, that property is deleted. See [Example B](#b-drop-an-extended-property-on-a-database) that follows later in this article.

## Permissions

Members of the db_owner and **db_ddladmin** fixed database roles can drop extended properties of any object with the following exception: **db_ddladmin** can't add properties to the database itself, or to users or roles.

Users can drop extended properties to objects they own, or on which they have `ALTER` or `CONTROL` permissions.

## Examples

### A. Drop an extended property on a column

The following example removes the property `caption` from column `id` in table `T1` contained in the schema `dbo`.

```sql
CREATE TABLE T1 (id INT, name CHAR(20));
GO

EXEC sp_addextendedproperty @name = 'caption',
    @value = 'Employee ID',
    @level0type = 'SCHEMA',
    @level0name = N'dbo',
    @level1type = 'TABLE',
    @level1name = N'T1',
    @level2type = 'COLUMN',
    @level2name = N'id';
GO

EXEC sp_dropextendedproperty @name = 'caption',
    @level0type = 'SCHEMA',
    @level0name = N'dbo',
    @level1type = 'TABLE',
    @level1name = N'T1',
    @level2type = 'COLUMN',
    @level2name = N'id';
GO

DROP TABLE T1;
GO
```

### B. Drop an extended property on a database

The following example removes the property named `MS_Description` from the [!INCLUDE [ssSampleDBobject](../../includes/sssampledbobject-md.md)] sample database. Because the property is on the database itself, no object types and names are specified.

```sql
USE AdventureWorks2022;
GO

EXEC sp_dropextendedproperty @name = N'MS_Description';
GO
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sys.fn_listextendedproperty (Transact-SQL)](../system-functions/sys-fn-listextendedproperty-transact-sql.md)
- [sp_addextendedproperty (Transact-SQL)](sp-addextendedproperty-transact-sql.md)
- [sp_updateextendedproperty (Transact-SQL)](sp-updateextendedproperty-transact-sql.md)
- [Extended Properties Catalog Views - sys.extended_properties](../system-catalog-views/extended-properties-catalog-views-sys-extended-properties.md)
