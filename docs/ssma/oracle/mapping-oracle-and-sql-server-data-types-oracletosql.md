---
title: "Map Oracle and SQL Server Data Types (OracleToSQL)"
description: Learn how to customize SSMA for Oracle mappings between Oracle data types and SQL Server.
author: nilabjaball
ms.author: niball
ms.reviewer: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.oracle.typemappingeditform.f1"
helpviewer_keywords:
  - "Type Mapping Inheritance"
---

# Map Oracle and SQL Server data types (OracleToSQL)

Oracle database types differ from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database types. When you convert Oracle database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects, you must specify how to map data types from Oracle to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can accept the default data type mappings, or you can customize the mappings as shown in the following sections.

## Default mappings

Microsoft SQL Server Migration Assistant (SSMA) for Oracle has a default set of data type mappings. For the list of default mappings, see [Project Settings (Type Mapping)](project-settings-type-mapping-oracletosql.md).

## Type mapping inheritance

You can customize type mappings at the project level, object category level (such as all stored procedures), or object level. Settings are inherited from the higher level unless they're overwritten at a lower level. For example, if you map **smallmoney** to **money** at the project level, all objects in the project use this mapping unless you customize the mapping at the object or category level.

When you view the **Type Mapping** tab in SSMA, the background is color-coded to show which type mappings are inherited. A yellow background indicates an inherited type mapping, and a white background indicates a mapping that is specified at the current level.

## Customize data type mapping

The following procedures show how to map data types at the project, database, or object level.

### Customize data type mapping at the project level

1. To customize data type mapping for the whole project, open the **Project Settings** dialog.

1. On the **Tools** menu, select **Project Settings**.

1. On the left pane, select **Type Mapping**. The type mapping chart and buttons appear on the right pane.

### Customize data type mapping at the database, table, view, or stored procedure level

1. Select the database, object category, or object in Oracle Metadata Explorer.

1. In Oracle Metadata Explorer, select the folder or object to customize.

1. On the right pane, select the **Type Mapping** tab.

### Add a new mapping

1. Select **Add**.

1. Under **Source type**, select the Oracle data type to map.

1. If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box.

   By taking this step, you can customize the data mapping for smaller and larger values of the same data type.

1. Under **Target type**, select the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type.

   Some types require a target data type length. If necessary, enter the new data length in the **Replace with** box.

1. Select **OK**.

### Modify a data type mapping

1. Select **Edit**.

1. Under **Source type**, select the Oracle data type to map.

1. If the type requires a length, specify the minimum data length for the mapping in the **From** box and the maximum data length in the **To** box. By taking this step, you can customize the data mapping for smaller and larger values of the same data type.

1. Under **Target type**, select the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type.

   Some types require a target data type length. If necessary, enter the new data length in the **Replace with** box, and then select **OK**.

### Remove a custom data type mapping

1. In the type mapping list, select the row that contains the data type mapping that you want to remove.

1. Select **Remove**. You can't remove inherited mappings. However, custom mappings overwrite inherited mappings on a specific object or object category.

## Related content

- [Assess Oracle schemas for conversion](assessing-oracle-schemas-for-conversion-oracletosql.md)
- [Convert Oracle schemas](converting-oracle-schemas-oracletosql.md)
- [Migrate Oracle Databases to SQL Server](migrating-oracle-databases-to-sql-server-oracletosql.md)
