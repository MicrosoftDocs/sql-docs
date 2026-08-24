---
title: "Mapping Source and Target Databases (AccessToSQL)"
description: Learn to specify a target database for Access database migration to SQL Server or Azure SQL Database, including multiple databases to multiple databases.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.schemamappingpanel.f1"
helpviewer_keywords:
  - "database schemas"
  - "mapping, databases"
  - "schemas, mapping to"
  - "schemas, Azure SQL"
  - "schemas, SQL Server"
  - "source database"
  - "target database"
---
# Map source and target databases (AccessToSQL)

When you connect to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL with SQL Server Migration Assistant (SSMA), you need to specify a target database for migration. If you have multiple Access databases, you can map them to multiple [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases (or schemas) or to multiple schemas under the connected Azure SQL Database.

## SQL Server or Azure SQL Database schemas

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases use the concept of schemas to separate objects within a database into logical groups. For example, a library database could use three schemas named `books`, `audio`, and `video` to separate book, audio, and video objects from each other. By default, the access database is mapped to `master` database and `dbo` schema in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and to connected database and `dbo` schema in Azure SQL.

Unless you customize the mapping between each Access database and the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database and schema, SSMA migrates all the schemas and data associated with the access database to the default database mapped.

## Modify the target database and schema

SSMA lets you map each Access database to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. The following procedure describes how to customize the mapping per database.

1. In the Access Metadata Explorer pane, select **access-metadata**.

   Schema mapping is also available when you select the **Databases** node or any database node. The schema mapping list is customized for the selected object.

1. In the right pane, select the **Schema Mapping** tab.

   You see a table containing access database names and its corresponding ssNoVersion or Azure SQL schema. The target schema is denoted in a two part notation (database.schema).

1. Select the row that contains the mapping you want to customize, and then select **Modify**.

1. In the **Choose Target Schema** dialog box, you might browse for available target database and schema or type the database and schema name in the textbox in a two part notation (database.schema) and then select **OK**.

### Modes of mapping

You can map a source database to any target database.

#### Map to SQL Server

By default, SSMA maps the source database to the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database that you connected to. If the target database you want to map doesn't exist on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], a message prompts you:

```output
The Database and/or schema does not exist in target SQL Server metadata. It would be created during synchronization. Do you wish to continue?
```

Select **Yes**.

Similarly, you can map a schema to a nonexisting schema under the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. SSMA creates this schema during synchronization.

#### Map to Azure SQL

You can map the source database to the connected target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database or to any schema in the connected target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. If you map source schema to a nonexisting schema under the connected target database, a message prompts you:

```output
Schema does not exist in target metadata. It would be created during synchronization. Do you wish to continue?
```

Select **Yes**.

## Revert to your initial database and schema

If you customize the mapping between an Access database and a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database, you can revert the mapping back to the database that you specified when you connected to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL.

Under the schema mapping tab, select any row and select **Reset to Default** to revert to the default database and schema.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Convert Access database objects](converting-access-database-objects-accesstosql.md)
