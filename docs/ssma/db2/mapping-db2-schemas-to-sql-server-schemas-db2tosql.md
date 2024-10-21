---
title: "Map Db2 schemas to SQL Server schemas (Db2ToSQL)"
description: Learn how to customize the mapping between Db2 schemas and SQL Server databases in SSMA for Db2.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.db2.schemamappingpanel.f1"
---
# Map Db2 schemas to SQL Server schemas (Db2ToSQL)

In Db2, each database has one or more schemas. By default, SQL Server Migration Assistant (SSMA) migrates all objects in an Db2 schema to a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database named for the schema. However, you can customize the mapping between Db2 schemas and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] databases.

## Db2 and SQL Server schemas

An Db2 database contains schemas. An instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] contains multiple databases, each of which can have multiple schemas.

The Db2 concept of a schema maps to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] concept of a database and one of its schemas. For example, Db2 might have a schema named `HR`. An instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might have a database named `HR`, and within that database are schemas. One schema is the `dbo` (or database owner) schema. By default, the Db2 schema `HR` is mapped to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database and schema `HR.dbo`. SSMA refers to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] combination of database and schema as a schema.

You can modify the mapping between Db2 and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] schemas.

<a id="modifying-the-target-database-and-schema"></a>

## Modify the target database and schema

In SSMA, you can map an Db2 schema to any available [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] schema.

#### Modify the database and schema

1. In Db2 Metadata Explorer, select **Schemas**.

    The **Schema Mapping** tab is also available when you select an individual database, the **Schemas** folder, or individual schemas. The list in the **Schema Mapping** tab is customized for the selected object.

1. In the right pane, select the **Schema Mapping** tab.

    You see a list of all Db2 schemas, followed by a target value. This target is denoted in a two part notation (*database.schema*) in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] where your objects and data are migrated.

1. Select the row that contains the mapping that you want to change, and then select **Modify**.

    In the **Choose Target Schema** dialog box, you might browse for available target database and schema or type the database and schema name in the textbox in a two part notation (database.schema) and then select **OK**.

1. The target changes on the **Schema Mapping** tab.

#### Modes of mapping

**Mapping to SQL Server**

You can map source database to any target database. By default, the source database is mapped to the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database that you connected to using SSMA. If the target database being mapped is non-existing on [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], then you're prompted with a message:

```output
The Database and/or schema does not exist in target SQL Server metadata. It would be created during synchronization. Do you wish to continue?
```

Select **Yes**. Similarly, you can map schema to non-existing schema under target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database, which is created during synchronization.

## Revert to the default database and schema

If you customize the mapping between an Db2 schema and a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] schema, you can revert the mapping back to the default values.

1. Under the schema mapping tab, select any row and select **Reset to Default** to revert to the default database and schema.

## Related content

- [Connect to SQL Server (Db2ToSQL)](connecting-to-sql-server-db2tosql.md)
- [Migrate Db2 Databases to SQL Server (Db2ToSQL)](migrating-db2-databases-to-sql-server-db2tosql.md)
- [Data Migration Report (Db2ToSQL)](data-migration-report-db2tosql.md)
