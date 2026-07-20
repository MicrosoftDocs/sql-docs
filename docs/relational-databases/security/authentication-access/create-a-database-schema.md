---
title: "Create a Database Schema"
description: Learn how to create a schema in SQL Server by using SQL Server Management Studio or Transact-SQL, including limitations and restrictions.
author: VanMSFT
ms.author: vanto
ms.date: 01/31/2025
ms.service: sql
ms.subservice: security
ms.topic: how-to
ms.custom:
  - ignite-2025
f1_keywords:
  - "sql13.swb.schemas.general.f1"
helpviewer_keywords:
  - "creating schemas with Management Studio"
  - "CREATE SCHEMA [Management Studio]"
  - "database schemas"
  - "schemas [SQL Server], creating"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Create a database schema

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricdw-fabricsqldb.md)]

This article describes how to create a schema in [!INCLUDE [ssnoversion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../../includes/tsql-md.md)].

<a id="Permissions"></a>

## Permissions

- To create a schema, you must have CREATE SCHEMA permission on the database.

- To specify another user as the owner of the schema being created, the caller must have IMPERSONATE permission on that user. If a database role is specified as the owner, the caller must meet one of the following criteria: membership in the role or ALTER permission on the role.

<a id="SSMSProcedure"></a>

## Using SQL Server Management Studio to create a schema

1. In Object Explorer, expand the **Databases** folder.

1. Expand the database in which to create the new database schema.

1. Right-click the **Security** folder, point to **New**, and select **Schema**.

1. In the **Schema - New** dialog box, on the **General** page, enter a name for the new schema in the **Schema name** box.

1. In the **Schema owner** box, enter the name of a database user or role to own the schema. Alternately, select **Search** to open the **Search Roles and Users** dialog box.

1. Select **OK**.

> [!NOTE]  
> A dialog box will not appear if you are creating a Schema using SSMS against an **Azure SQL Database** or an **Azure Synapse Analytics**. You will need to run the **Create Schema Template T-SQL Statement** that is generated.

<a id="additional-options"></a>

The **Schema - New** dialog box also offers options on two extra pages: **Permissions** and **Extended Properties**.

- The **Permissions** page lists all possible securables and the permissions on those securables that can be granted to the login.

- The **Extended properties** page allows you to add custom properties to database users.

<a id="TsqlProcedure"></a>

## Using Transact-SQL to create a schema

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. The following example creates a schema named `Chains`, and then creates a table named `Sizes`.

   ```sql
   CREATE SCHEMA Chains;

   GO

   CREATE TABLE Chains.Sizes
   (
       ChainID INT,
       width DECIMAL (10, 2)
   );
   ```

1. More options can be performed in a single statement. The following example creates the schema `Sprockets` owned by `Joe` that contains the table `NineProngs`. The statement grants `SELECT` to `Bob` and denies `SELECT` to `John`.

   ```sql
   CREATE SCHEMA Sprockets
       AUTHORIZATION Joe;
   GO

   CREATE TABLE NineProngs
   (
      source INT,
      cost INT,
      partnumber INT
   );
   GO

   GRANT SELECT
      ON SCHEMA::Sprockets TO Bob;
   GO

   DENY SELECT
      ON SCHEMA::Sprockets TO John;
   GO
   ```

1. Execute the following statement to view the schemas in the current database:

   ```sql
   SELECT *
   FROM sys.schemas;
   ```

<a id="Restrictions"></a>

## Limitations and restrictions

- The new schema is owned by one of the following database-level principals: database user, database role, or application role. Objects created within a schema are owned by the owner of the schema, and have a `NULL` `principal_id` in `sys.objects` Ownership of schema-contained objects can be transferred to any database-level principal, but the schema owner always retains CONTROL permission on objects within the schema.

- The domain principal is added to the database as a schema when creating a database object if you specify a valid domain principal (user or group) as the object owner. The new schema is owned by that domain principal.

## Related content

- [CREATE SCHEMA (Transact-SQL)](../../../t-sql/statements/create-schema-transact-sql.md)

