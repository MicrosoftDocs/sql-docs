---
title: "Mapping Source and Target Data Types (AccessToSQL)"
description: "Mapping Source and Target Data Types (AccessToSQL)"
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
  - "ssma.access.typemappingeditform.f1"
helpviewer_keywords:
  - "customizing data type mappings"
  - "data types, mapping"
  - "mapping, data types"
  - "source data types"
  - "target data types"
---
# Map source and target data types (AccessToSQL)

Access database types differ from [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database types. When you use SQL Server Migration Assistant (SSMA) to convert Access database objects to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] objects, you must specify how to map the data types.

You can accept the default data type mappings, or you can customize the mappings as shown in the following procedures.

## Default mappings

SSMA has a default set of data type mappings. For the list of default mappings, see [Project Settings (Type Mapping)](project-settings-type-mapping-accesstosql.md).

## Customize data type mappings

By using the **Project Settings** dialog box, you can customize how types are mapped for all databases and database objects in a project. The type mappings for a project apply to all databases and database objects that don't have custom type mappings.

You can also customize data type mapping at the database or table level.

The following procedure shows how to map data types at the project, database, or database object level.

1. To customize data type mapping for the whole project, open the **Project Settings** dialog box:

   1. On the **Tools** menu, select **Project Settings**.

   1. In the left pane, select **Type Mapping**.

      The type mapping chart and buttons appear in the right pane.

   Or, to customize data type mapping at the database or table level, select the database or table in the Access Metadata Explorer pane:

   1. In the Access Metadata Explorer pane, expand **Access-metabase**, and then expand **Databases**.

   1. Select the database or table for which you want to customize the data type mapping.

   1. In the right pane, select **Type Mapping**.

1. To add a new mapping, complete the following steps:

   1. In the Type Mapping pane, select **Add**.

   1. In the **New Type Mapping** dialog box, under **Source type**, select the Access data type to map.

   1. If the type requires a length, specify the minimum and maximum data lengths for the mapping by selecting the **From** and **To** check boxes, and then entering the values.

      This setting customizes the data mapping for smaller and larger values of the same data type.

   1. Under **Target type**, select the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type.

      Some types require a target data type length. If it's required, enter the new data length in the **Replace With** box, and then select **OK**.

1. To edit a data type mapping, complete the following steps:

   1. In the Type Mapping pane, select **Edit**.

   1. In the **Type Mapping List** dialog box, under **Source type**, select the Access data type to map.

   1. If the type requires a length, specify the minimum and maximum data lengths for the mapping by selecting the **From** and **To** check boxes, and then entering the values.

      This setting customizes the data mapping for smaller and larger values of the same data type.

   1. Under **Target type**, select the target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data type.

      Some types require a target data type length. If it's required, enter the new data length in the **Replace With** box, and then select **OK**.

1. To remove a data type mapping, complete the following steps:

   1. In the Type Mapping pane, select the row in the type mapping list that contains the data type mapping you want to remove.

   1. Select **Remove**.

## Related content

- [Migrate Access databases to SQL Server and Azure SQL](migrating-access-databases-to-sql-server-azure-sql-db-accesstosql.md)
- [Convert Access database objects](converting-access-database-objects-accesstosql.md)
