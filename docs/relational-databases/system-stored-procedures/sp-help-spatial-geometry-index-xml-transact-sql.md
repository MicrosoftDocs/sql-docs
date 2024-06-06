---
title: "sp_help_spatial_geometry_index_xml (Transact-SQL)"
description: sp_help_spatial_geometry_index_xml returns the names and values for a specified set of properties about a geometry spatial index.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_spatial_geometry_index_xml_TSQL"
  - "sp_help_spatial_geometry_index_xml"
helpviewer_keywords:
  - "sp_help_spatial_geometry_index_xml procedure"
dev_langs:
  - "TSQL"
---
# sp_help_spatial_geometry_index_xml (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Returns the names and values for a specified set of properties about a **geometry** spatial index. You can choose to return a core set of properties or all properties of the index.

Results are returned in an XML fragment that displays the name and value of the properties selected.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
sp_help_spatial_geometry_index_xml
    [ @tabname = ] N'tabname'
    , [ @indexname = ] N'indexname'
    , [ @verboseoutput = ] verboseoutput
    , [ @query_sample = ] query_sample
    , [ @xml_output = ] N'xml_output' OUTPUT
[ ; ]
```

## Arguments and properties

See [Spatial index stored procedures - arguments and properties](spatial-index-stored-procedures-arguments-and-properties.md).

## Permissions

User must be a member of the **public** role. Requires `READ ACCESS` permission on the server and the object.

## Remarks

Properties containing `NULL` values aren't included in the XML return set.

## Examples

The following example uses `sp_help_spatial_geometry_index_xml` to investigate the spatial index `SIndx_SpatialTable_geometry_col2` defined on table `geometry_col` for the given query sample in `@qs`. This example returns the core properties of the specified index in an XML fragment that displays the name and value of the properties selected.

An [XQuery Basics](../../xquery/xquery-basics.md) is then run on the result set, returning a specific property.

```sql
DECLARE @qs geometry = 'POLYGON((-90.0 -180.0, -90.0 180.0, 90.0 180.0, 90.0 -180.0, -90.0 -180.0))';
DECLARE @x XML;

EXEC sp_help_spatial_geometry_index_xml 'geometry_col',
    'SIndx_SpatialTable_geometry_col2',
    0,
    @qs,
    @x OUTPUT;

SELECT @x.value('(/Primary_Filter_Efficiency/text())[1]', 'float');
```

Similar to [sp_help_spatial_geometry_index](sp-help-spatial-geometry-index-transact-sql.md), this stored procedure provides simpler programmatic access to the properties of a spatial index and reports the result set in XML.

## Related content

- [Spatial index stored procedures - arguments and properties](spatial-index-stored-procedures-arguments-and-properties.md)
- [sp_help_spatial_geometry_index (Transact-SQL)](sp-help-spatial-geometry-index-transact-sql.md)
- [Spatial Indexes Overview](../spatial/spatial-indexes-overview.md)
- [Spatial Data](../spatial/spatial-data-sql-server.md)
- [XQuery Basics](../../xquery/xquery-basics.md)
- [XQuery Language Reference (SQL Server)](../../xquery/xquery-language-reference-sql-server.md)
