---
title: "sp_help_spatial_geometry_index (Transact-SQL)"
description: Returns the names and values for a specified set of properties about a geometry spatial index.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_spatial_geometry_index"
  - "sp_help_spatial_geometry_index_TSQL"
helpviewer_keywords:
  - "sp_help_spatial_geometry_index procedure"
dev_langs:
  - "TSQL"
---
# sp_help_spatial_geometry_index (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the names and values for a specified set of properties about a **geometry** spatial index. The result is returned in a table format. You can choose to return a core set of properties or all properties of the index.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_spatial_geometry_index
    [ @tabname = ] N'tabname'
    , [ @indexname = ] N'indexname'
    , [ @verboseoutput = ] verboseoutput
    , [ @query_sample = ] query_sample
[ ; ]
```

## Arguments and properties

See [Spatial index stored procedures - arguments and properties](spatial-index-stored-procedures-arguments-and-properties.md).

## Permissions

User must be assigned a **public** role to access the procedure. Requires READ ACCESS permission on the server and the object.

## Remarks

Properties containing `NULL` values aren't included in the return set.

## Examples

The following example uses `sp_help_spatial_geometry_index` to investigate the spatial index `SIndx_SpatialTable_geometry_col2` defined on table `geometry_col` for the given query sample in `@qs`. This example returns only the core properties of the specified index.

```sql
DECLARE @qs geometry = 'POLYGON((-90.0 -180.0, -90.0 180.0, 90.0 180.0, 90.0 -180.0, -90.0 -180.0))';

EXEC sp_help_spatial_geometry_index
    'geometry_col',
    'SIndx_SpatialTable_geometry_col2',
    0,
    @qs;
```

## Related content

- [Spatial index stored procedures - arguments and properties](spatial-index-stored-procedures-arguments-and-properties.md)
- [sp_help_spatial_geometry_index_xml (Transact-SQL)](sp-help-spatial-geometry-index-xml-transact-sql.md)
- [Spatial Indexes Overview](../spatial/spatial-indexes-overview.md)
- [Spatial Data](../spatial/spatial-data-sql-server.md)
