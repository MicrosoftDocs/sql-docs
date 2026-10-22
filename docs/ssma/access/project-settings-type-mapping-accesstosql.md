---
title: "Project Settings (Type Mapping) (AccessToSQL)"
description: "Project Settings (Type Mapping) (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: concept-article
ms.collection:
  - sql-migration-content
f1_keywords:
  - "ssma.access.projectsettingstypemapping.f1"
helpviewer_keywords:
  - "Access data types"
  - "data types, default mappings"
  - "default data type mappings"
  - "Project Settings dialog box, Type Mapping"
  - "SQL Server data types"
  - "Type Mapping settings"
---
# Project Settings (Type Mapping) (AccessToSQL)

The SQL Server Migration Assistant (SSMA) Type Mapping project settings let you set default type mappings for the SSMA project. You can also specify type mappings for individual database objects. For more information, see [Map source and target data types](mapping-source-and-target-data-types-accesstosql.md).

You can set type mapping in the **Project Settings** and **Default Project Settings** dialog boxes:

- Use the **Project Settings** dialog box to set configuration options for the current project. To access the type mapping settings, on the **Tools** menu, select **Project Settings**, and then select **Type Mapping** in the left pane.

- Use the **Default Project Settings** dialog box to set configuration options for all projects. To access the type mapping settings, on the **Tools** menu, select **Default Project Settings**. Select the migration project type from the **Migration Target Version** dropdown list. Then, select **Type Mapping** in the left pane.

## Options

#### Source Type

The Access data type to map.

#### Target Type

The target [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL data type for the specified Access data type.

The following table shows the default mapping between source and target data types.

| Access data type | SQL Server data type |
| --- | --- |
| **binary[\*..\*]** | **varbinary(*n*)** |
| **boolean** | **bit** |
| **byte** | **tinyint** |
| **currency** | **money** |
| **date** | **datetime** |
| **decimal** | **float** |
| **double** | **float** |
| **guid** | **uniqueidentifier** |
| **integer** | **smallint** |
| **long** | **int** |
| **longbinary** | **varbinary(max)** |
| **memo** | **nvarchar(max)** |
| **memo** - for Access 97 | **varchar(max)** |
| **single** | **real** |
| **text[\*..\*]** | **nvarchar(*n*)** |
| **text[\*..\*]** - for Access 97 | **varchar(*n*)** |

#### Add

Select to add a data type to the mapping list.

#### Edit

Select to edit a data type in the mapping list.

#### Remove

Select to remove the selected data type mapping from the mapping list.

#### Reset to Default

Select to reset all data type mappings to the SSMA defaults.

## Related content

- [Map source and target data types](mapping-source-and-target-data-types-accesstosql.md)
- [User interface reference](user-interface-reference-accesstosql.md)
