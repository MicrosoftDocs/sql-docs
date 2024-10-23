---

title: "Map Db2 and SQL Server data types (Db2ToSQL)"
description: Learn how to map between Db2 and SQL Server data types in SSMA for Db2.
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
  - "ssma.db2.typemappingeditform.f1"
---
# Map Db2 and SQL Server data types (Db2ToSQL)

Db2 database types differ from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database types. When you use SQL Server Migration Assistant (SSMA) to convert Db2 database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects, you must specify how to map data types from Db2 to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can accept the default data type mappings, or you can customize the mappings as shown in the following sections.

## Default mappings

SSMA has a default set of data type mappings. For the list of default mappings, see [Project Settings (Type Mapping)](project-settings-type-mapping-db2tosql.md).

## Type mapping inheritance

You can customize type mappings at the project level, object category level (such as all stored procedures), or object level. Settings are inherited from the higher level unless they're overridden at a lower level. For example, if you map **smallmoney** to **money** at the project level, all objects in the project use this mapping unless you customize the mapping at the object or category level.

When you view the **Type Mapping** tab in SSMA, the background is color-coded to show which type mappings are inherited. The background of a type mapping is yellow for any inherited type mapping, and white for any mapping that is specified at the current level.

## Customize data type mappings

The following procedure shows how to map data types at the project, database, or object level:

1. To customize data type mapping for the whole project, open the **Project Settings** dialog box:

   1. Navigate to **Tools** > **Project Settings**.

   1. In the left pane, select **Type Mapping**.

      The type mapping chart and buttons appear in the right pane.

   Or, to customize data type mapping at the database, table, view, or stored procedure level, select the database, object category, or object in Db2 Metadata Explorer:

   1. In Db2 Metadata Explorer, select the folder or object to customize.

   1. In the right pane, select the **Type Mapping** tab.

1. To add a new mapping, do the following steps:

   1. Select **Add**.

   1. Under **Source type**, select the Db2 data type to map.

   1. If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box.

      This lets you customize the data mapping for smaller and larger values of the same data type.

   1. Under **Target type**, select the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type.

      Some types require a target data type length. If necessary, enter the new data length in the **Replace with** box.

   1. Select **OK**.

1. To modify a data type mapping, do the following steps:

   1. Select **Edit**.

   1. Under **Source type**, select the Db2 data type to map.

   1. If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box.

      This lets you customize the data mapping for smaller and larger values of the same data type.

   1. Under **Target type**, select the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type.

      Some types require a target data type length. If necessary, enter the new data length in the **Replace with** box, and then select **OK**.

1. To remove a custom data type mapping, do the following steps:

   1. Select the row in the type mapping list that contains the data type mapping you want to remove.

   1. Select **Remove**.

      You can't remove inherited mappings. However, inherited mappings are overridden by custom mappings on a specific object or object category.

## Related content

- [Migrate Db2 Databases to SQL Server (Db2ToSQL)](migrating-db2-databases-to-sql-server-db2tosql.md)
- [Assessment Report (Db2ToSQL)](assessment-report-db2tosql.md)
- [Convert Db2 schemas (Db2ToSQL)](converting-db2-schemas-db2tosql.md)
