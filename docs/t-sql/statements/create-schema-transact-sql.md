---
title: "CREATE SCHEMA (Transact-SQL)"
description: CREATE SCHEMA creates a schema in the current database.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 08/18/2025
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "CREATE_SCHEMA_TSQL"
  - "SCHEMA"
  - "CREATE SCHEMA"
  - "SCHEMA_TSQL"
helpviewer_keywords:
  - "owners [SQL Server], schemas"
  - "table creation [SQL Server], CREATE SCHEMA statement"
  - "permissions [SQL Server], schemas"
  - "schemas [SQL Server], creating"
  - "CREATE SCHEMA statement"
dev_langs:
  - "TSQL"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# CREATE SCHEMA (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Creates a schema in the current database. The `CREATE SCHEMA` transaction can also create tables and views within the new schema, and set `GRANT`, `DENY`, or `REVOKE` permissions on those objects.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

Syntax for SQL Server, Azure SQL Database, and SQL database in Microsoft Fabric.

```syntaxsql
CREATE SCHEMA schema_name_clause [ <schema_element> [ ...n ] ]

<schema_name_clause> ::=
    {
    schema_name
    | AUTHORIZATION owner_name
    | schema_name AUTHORIZATION owner_name
    }

<schema_element> ::=
    {
        table_definition | view_definition | grant_statement |
        revoke_statement | deny_statement
    }
```

Syntax for Azure Synapse Analytics and Parallel Data Warehouse.

```syntaxsql
CREATE SCHEMA schema_name [ AUTHORIZATION owner_name ] [;]
```

## Arguments

#### *schema_name*

Specifies the name of the schema within the database.

#### AUTHORIZATION *owner_name*

Specifies the name of the database-level principal that will own the schema. This principal might own other schemas, and might not use the current schema as its default schema.

#### *table_definition*

Specifies a `CREATE TABLE` statement that creates a table within the schema. The principal executing this statement must have `CREATE TABLE` permission on the current database.

#### *view_definition*

Specifies a `CREATE VIEW` statement that creates a view within the schema. The principal executing this statement must have `CREATE VIEW` permission on the current database.

#### *grant_statement*

Specifies a `GRANT` statement that grants permissions on any securable except the new schema.

#### *revoke_statement*

Specifies a `REVOKE` statement that revokes permissions on any securable except the new schema.

#### *deny_statement*

Specifies a `DENY` statement that denies permissions on any securable except the new schema.

## Remarks

Statements that contain `CREATE SCHEMA AUTHORIZATION`, but don't specify a name, are only permitted for backward compatibility. The statement doesn't cause an error, but doesn't create a schema.

`CREATE SCHEMA` can create a schema, the tables and views it contains, and `GRANT`, `REVOKE`, or `DENY` permissions on any securable in a single statement. You must execute this statement as a separate batch. Objects created by the `CREATE SCHEMA` statement are created inside the schema that you create.

`CREATE SCHEMA` transactions are atomic. If any error occurs during the execution of a `CREATE SCHEMA` statement, none of the specified securables are created and no permissions are granted.

You can list securables to be created by `CREATE SCHEMA` in any order, except for views that reference other views. In that case, the referenced view must be created before the view that references it.

Therefore, a `GRANT` statement can grant permission on an object before the object itself is created, or a `CREATE VIEW` statement can appear before the `CREATE TABLE` statements that create the tables referenced by the view. Also, `CREATE TABLE` statements can declare foreign keys to tables that are defined later in the `CREATE SCHEMA` statement.

> [!NOTE]  
> `DENY` and `REVOKE` are supported inside `CREATE SCHEMA` statements. `DENY` and `REVOKE` clauses are executed in the order in which they appear in the `CREATE SCHEMA` statement.

The principal that executes `CREATE SCHEMA` can specify another database principal as the owner of the schema being created. This action requires extra permissions, as described in the [Permissions](#permissions) section later in this article.

The new schema is owned by one of the following database-level principals: database user, database role, or application role. Objects created within a schema are owned by the owner of the schema, and have a null `principal_id` in `sys.objects`. You can transfer ownership of schema-contained objects to any database-level principal, but the schema owner always retains `CONTROL` permission on objects within the schema.

> [!NOTE]  
> [!INCLUDE [ssCautionUserSchema](../../includes/sscautionuserschema-md.md)]

### Implicit schema and user creation

[!INCLUDE [entra-id](../../includes/entra-id.md)]

In some cases, a user can use a database without having a database user account (a database principal in the database). This condition can happen in the following situations:

- A login has `CONTROL SERVER` privileges.

- A Windows user doesn't have an individual database user account (a database principal in the database), but accesses a database as a member of a Windows group that has a database user account (a database principal for the Windows group).

- A Microsoft Entra user doesn't have an individual database user account (a database principal in the database), but accesses a database as a member of a Microsoft Entra group that has a database user account (a database principal for the Microsoft Entra group).

When a user without a database user account creates an object without specifying an existing schema, a database principal and default schema are created in the database automatically for that user. The created database principal and schema have the same name as the name that user used when connecting to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] (the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] authentication login name or the Windows user name).

This behavior is necessary to allow users that are based on Windows groups to create and own objects. However, it can result in the unintentional creation of schemas and users. To avoid implicitly creating users and schemas, whenever possible explicitly create database principals and assign a default schema. Or explicitly state an existing schema when creating objects in a database, using two or three-part object names.

The implicit creation of a Microsoft Entra user isn't possible on [!INCLUDE [ssSDS_md](../../includes/sssds-md.md)]. Since creating a Microsoft Entra user from external provider must check the user's status in Microsoft Entra ID, creating the user fails with error 2760: `The specified schema name "<user@domain>" either does not exist or you do not have permission to use it.` And then error 2759: `CREATE SCHEMA failed due to previous errors.`

Attempts to create or alter schemas result in the error 15151: `Cannot find the user '', because it does not exist or you do not have permission.`, also followed by error 2759. To work around these errors, either create the Microsoft Entra user from an external provider, or alter the Microsoft Entra group to assign a default schema. Then rerun the statement creating the object.

In [!INCLUDE [fabricse](../../includes/fabric-se.md)] and [!INCLUDE [fabricdw](../../includes/fabric-dw.md)], schema names can't contain `/` or `\` or end with a `.`.

## Deprecation notice

`CREATE SCHEMA` statements that don't specify a schema name are currently supported for backward compatibility. These statements don't actually create a schema in the database, but they do create tables and views, and grant permissions. Principals don't need `CREATE SCHEMA` permission to execute this earlier form of `CREATE SCHEMA`, because no schema is being created. This functionality will be removed from a future release of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Permissions

Requires `CREATE SCHEMA` permission on the database.

To create an object specified within the `CREATE SCHEMA` statement, the user must have the corresponding `CREATE` permission.

To specify another user as the owner of the schema being created, the caller must have `IMPERSONATE` permission on that user. If a database role is specified as the owner, the caller must have one of the following: membership in the role or `ALTER` permission on the role.

For the backward-compatible syntax, no permissions to `CREATE SCHEMA` are checked because no schema is being created.

### Permissions in Fabric Data Warehouse

In Fabric Data Warehouse, in addition to the `CREATE SCHEMA` permission, the user must be a member of the Admin, Member, or Contributor [workspace role](/fabric/fundamentals/roles-workspaces).

## Examples

### A. Create a schema and granting permissions

The following example creates schema `Sprockets` owned by `Annik` that contains table `NineProngs`. The statement grants `SELECT` to `Mandar` and denies `SELECT` to `Prasanna`. `Sprockets` and `NineProngs` are created in a single statement.

```sql
USE AdventureWorks2022;
GO

CREATE SCHEMA Sprockets AUTHORIZATION Annik
    CREATE TABLE NineProngs
    (
        source INT,
        cost INT,
        partnumber INT
    )

    GRANT SELECT ON SCHEMA::Sprockets TO Mandar
    DENY SELECT ON SCHEMA::Sprockets TO Prasanna;
GO
```

## Examples: Azure Synapse Analytics and Analytics Platform System (PDW)

### B. Create a schema and a table in the schema

The following example creates schema `Sales` and then creates a table `Sales.Region` in that schema.

```sql
CREATE SCHEMA Sales;
GO

CREATE TABLE Sales.Region
(
    Region_id INT NOT NULL,
    Region_Name CHAR (5) NOT NULL
)
WITH (DISTRIBUTION = REPLICATE);
GO
```

### C. Set the owner of a schema

The following example creates a `Production` schema, and sets `Mary` as the owner.

```sql
CREATE SCHEMA Production AUTHORIZATION [Contoso\Mary];
GO
```

## Related content

- [ALTER SCHEMA (Transact-SQL)](alter-schema-transact-sql.md)
- [DROP SCHEMA (Transact-SQL)](drop-schema-transact-sql.md)
- [GRANT (Transact-SQL)](grant-transact-sql.md)
- [DENY (Transact-SQL)](deny-transact-sql.md)
- [REVOKE (Transact-SQL)](revoke-transact-sql.md)
- [CREATE VIEW (Transact-SQL)](create-view-transact-sql.md)
- [EVENTDATA (Transact-SQL)](../functions/eventdata-transact-sql.md)
- [sys.schemas](../../relational-databases/system-catalog-views/schemas-catalog-views-sys-schemas.md)
- [Create a database schema](../../relational-databases/security/authentication-access/create-a-database-schema.md)
