---
title: "sp_addextendedproperty (Transact-SQL)"
description: Adds a new extended property to a database object.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 12/27/2023
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_addextendedproperty"
  - "sp_addextendedproperty_TSQL"
helpviewer_keywords:
  - "sp_addextendedproperty"
dev_langs:
  - "TSQL"
monikerRange: "=azuresqldb-current || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# sp_addextendedproperty (Transact-SQL)

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Adds a new extended property to a database object.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_addextendedproperty
    [ @name = ] N'name'
    [ , [ @value = ] value ]
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

The name of the property to be added. *@name* is **sysname**, with no default, and can't be `NULL`. Names can include blank or non-alphanumeric character strings, and binary values.

#### [ @value = ] *value*

The value to be associated with the property. *@value* is **sql_variant**, with a default of `NULL`. The size of *@value* can't be more than 7,500 bytes.

#### [ @level0type = ] '*level0type*'

The type of level 0 object. *@level0type* is **varchar(128)**, with a default of `NULL`.

Valid inputs are:

- `ASSEMBLY`
- `CONTRACT`
- `EVENT NOTIFICATION`
- `FILEGROUP`
- `MESSAGE TYPE`
- `PARTITION FUNCTION`
- `PARTITION SCHEME`
- `REMOTE SERVICE BINDING`
- `ROUTE`
- `SCHEMA`
- `SERVICE`
- `USER`
- `TRIGGER`
- `TYPE`
- `PLAN GUIDE`
- `NULL`

> [!IMPORTANT]  
> The ability to specify `USER` as a level-0 type in an extended property of a level-1 type object will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use `SCHEMA` as the level-0 type instead. For example, when defining an extended property on a table, specify the schema of the table instead of a user name. The ability to specify `TYPE` as level-0 type will be removed in a future version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. For TYPE, use `SCHEMA` as the level-0 type and `TYPE` as the level-1 type.

#### [ @level0name = ] N'*level0name*'

The name of the level 0 object type specified. *@level0name* is **sysname**, with a default of `NULL`.

#### [ @level1type = ] '*level1type*'

The type of level 1 object. *@level1type* is **varchar(128)**, with a default of `NULL`.

Valid inputs are:

- `AGGREGATE`
- `DEFAULT`
- `FUNCTION`
- `LOGICAL FILE NAME`
- `PROCEDURE`
- `QUEUE`
- `RULE`
- `SEQUENCE`
- `SYNONYM`
- `TABLE`
- `TABLE_TYPE`
- `TYPE`
- `VIEW`
- `XML SCHEMA COLLECTION`
- `NULL`

#### [ @level1name = ] N'*level1name*'

The name of the level 1 object type specified. *@level1name* is **sysname**, with a default of `NULL`.

#### [ @level2type = ] '*level2type*'

The type of level 2 object. *@level2type* is **varchar(128)**, with a default of `NULL`.

Valid inputs are:

- `COLUMN`
- `CONSTRAINT`
- `EVENT NOTIFICATION`
- `INDEX`
- `PARAMETER`
- `TRIGGER`
- `NULL`

#### [ @level2name = ] N'*level2name*'

The name of the level 2 object type specified. *@level2name* is **sysname**, with a default of `NULL`.

## Return code values

`0` (success) or `1` (failure).

## Remarks

When you specify extended properties, the objects in a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database are classified into three levels: 0, 1, and 2. Level 0 is the highest level and is defined as objects that are contained at the database scope. Level 1 objects are contained in a schema or user scope, and level 2 objects are contained by level 1 objects. Extended properties can be defined for objects at any of these levels.

References to an object in one level must be qualified with the names of the higher level objects that own or contain them. For example, when you add an extended property to a table column (level 2), you must also specify the table name (level 1) that contains the column and the schema (level 0) that contains the table.

If all object types and names are null, the property belongs to the current database itself.

Extended properties aren't allowed on system objects, objects outside the scope of a user-defined database, or objects not listed in Arguments as valid inputs.

Extended properties aren't allowed on memory-optimized tables.

## Replicate extended properties

Extended properties are replicated only in the initial synchronization between the Publisher and the Subscriber. If you add or modify an extended property after the initial synchronization, the change isn't replicated. For more information about how to replicate database objects, see [Publish Data and Database Objects](../replication/publish/publish-data-and-database-objects.md).

## Schema versus user

We don't recommend specifying `USER` as a level-0 type when you apply an extended property to a database object, because this can cause name resolution ambiguity. For example, assume user `Mary` owns two schemas (`Mary` and `MySchema`), and these schemas both contain a table named `MyTable`. If Mary adds an extended property to table `MyTable` and specifies `@level0type = 'USER', @level0name = N'Mary'`, it isn't clear to which table the extended property is applied. To maintain backward compatibility, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] applies the property to the table that is contained in the schema named `Mary`.

## Permissions

Members of the **db_owner** and **db_ddladmin** fixed database roles can add extended properties to any object with the following exception: **db_ddladmin** can't add properties to the database itself, or to users or roles.

Users can add extended properties to objects they own or have ALTER or CONTROL permissions on.

## Examples

### A. Add an extended property to a database

The following example adds the property name `Caption` with a value of `AdventureWorks2022 Sample OLTP Database` to the [!INCLUDE [sssampledbobject-md](../../includes/sssampledbobject-md.md)] sample database.

```sql
USE AdventureWorks2022;
GO

--Add a caption to the AdventureWorks2022 Database object itself.
EXEC sp_addextendedproperty @name = N'Caption',
    @value = 'AdventureWorks2022 Sample OLTP Database';
```

### B. Add an extended property to a column in a table

The following example adds a caption property to column `PostalCode` in table `Address`.

```sql
USE AdventureWorks2022;
GO

EXEC sp_addextendedproperty @name = N'Caption',
    @value = 'Postal code is a required column.',
    @level0type = 'SCHEMA', @level0name = N'Person',
    @level1type = 'TABLE', @level1name = N'Address',
    @level2type = 'COLUMN', @level2name = N'PostalCode';
GO
```

### C. Add an input mask property to a column

The following example adds an input mask property `99999 or 99999-9999 or #### ###` to the column `PostalCode` in the table `Address`.

```sql
USE AdventureWorks2022;
GO

EXEC sp_addextendedproperty @name = N'Input Mask ',
    @value = '99999 or 99999-9999 or #### ###',
    @level0type = 'SCHEMA', @level0name = N'Person',
    @level1type = 'TABLE', @level1name = N'Address',
    @level2type = 'COLUMN', @level2name = N'PostalCode';
GO
```

### D. Add an extended property to a filegroup

The following example adds an extended property to the `PRIMARY` filegroup.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_addextendedproperty @name = N'MS_DescriptionExample',
    @value = N'Primary filegroup for the AdventureWorks2022 sample database.',
    @level0type = 'FILEGROUP', @level0name = N'PRIMARY';
GO
```

### E. Add an extended property to a schema

The following example adds an extended property to the `HumanResources` schema.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_addextendedproperty @name = N'MS_DescriptionExample',
    @value = N'Contains objects related to employees and departments.',
    @level0type = 'SCHEMA', @level0name = N'HumanResources';
```

### F. Add an extended property to a table

The following example adds an extended property to the `Address` table in the `Person` schema.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_addextendedproperty @name = N'MS_DescriptionExample',
    @value = N'Street address information for customers, employees, and vendors.',
    @level0type = 'SCHEMA', @level0name = N'Person',
    @level1type = 'TABLE', @level1name = N'Address';
GO
```

### G. Add an extended property to a role

The following example creates an application role and adds an extended property to the role.

```sql
USE AdventureWorks2022;
GO

CREATE APPLICATION ROLE Buyers
    WITH Password = '987G^bv876sPY)Y5m23';
GO

EXEC sys.sp_addextendedproperty @name = N'MS_Description',
    @value = N'Application Role for the Purchasing Department.',
    @level0type = 'USER', @level0name = N'Buyers';
```

### H. Add an extended property to a type

The following example adds an extended property to a type.

```sql
USE AdventureWorks2022;
GO

EXEC sys.sp_addextendedproperty @name = N'MS_Description',
    @value = N'Data type (alias) to use for any column that represents an order number. For example a sales order number or purchase order number.',
    @level0type = 'SCHEMA', @level0name = N'dbo',
    @level1type = 'TYPE', @level1name = N'OrderNumber';
```

### I. Add an extended property to a user

The following example creates a user and adds an extended property to the user.

```sql
USE AdventureWorks2022;
GO

CREATE USER CustomApp WITHOUT LOGIN;
GO

EXEC sys.sp_addextendedproperty @name = N'MS_Description',
    @value = N'User for an application.',
    @level0type = 'USER', @level0name = N'CustomApp';
```

## Related content

- [Database Engine stored procedures (Transact-SQL)](database-engine-stored-procedures-transact-sql.md)
- [sys.fn_listextendedproperty (Transact-SQL)](../system-functions/sys-fn-listextendedproperty-transact-sql.md)
- [sp_dropextendedproperty (Transact-SQL)](sp-dropextendedproperty-transact-sql.md)
- [sp_updateextendedproperty (Transact-SQL)](sp-updateextendedproperty-transact-sql.md)
