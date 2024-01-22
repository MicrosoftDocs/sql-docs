---
title: "sp_help_spatial_geography_histogram (Transact-SQL)"
description: Facilitates the keying of grid parameters for a spatial index.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_spatial_geography_histogram_TSQL"
  - "sp_help_spatial_geography_histogram"
helpviewer_keywords:
  - "sp_help_spatial_geography_histogram"
dev_langs:
  - "TSQL"
---
# sp_help_spatial_geography_histogram (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Facilitates the keying of grid parameters for a spatial index.

## Syntax

```syntaxsql
sp_help_spatial_geography_histogram
    [ @tabname = ] N'tabname'
    , [ @colname = ] N'colname'
    , [ @resolution = ] resolution
    [ , [ @sample = ] sample ]
[ ; ]
```

## Arguments

#### [ @tabname = ] N'*tabname*'

The qualified or nonqualified name of the table for which the spatial index was specified. *@tabname* is **sysname**, with no default.

Quotation marks are required only if a qualified table is specified. If a fully qualified name, including a database name, is provided, the database name must be the name of the current database.

#### [ @colname = ] N'*colname*'

The name of the spatial column specified. *@colname* is **sysname**, with no default.

#### [ @resolution = ] *resolution*

The resolution of the bounding box. Valid values are from 10 to 5000. *@resolution* is **int**, with no default.

#### [ @sample = ] *sample*

The percentage of the table that is used. Valid values are from 0 to 100. *@sample* is **float**, with a default of `100`.

## Return values

A table value is returned. The following grid describes the column contents of the table.

| Column name | Data type | Description |
| --- | --- | --- |
| `cellid` | **int** | Represents the Unique ID of each cell, with a starting count of 1. |
| `cell` | **geography** | A rectangular polygon that represents each cell. Cell shape is identical to the cell shape used for the spatial indexing. |
| `row_count` | **bigint** | Indicates the number of spatial objects that are touching or containing the cell. |

## Permissions

User must be a member of the **public** role. Requires READ ACCESS permission on the server and the object.

## Remarks

[!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] (SSMS) spatial tab shows a graphical representation of the results. You can query the results against the spatial window to get an approximate number of result items.

> [!NOTE]  
> Objects in the table might cover more than one cell, so the sum of the cells in the table might be larger than the number of actual objects.

The bounding box for the **geography** type is the entire globe.

## Examples

The following example calls `sp_help_spatial_geography_histogram` on the `Person.Address` table in the [!INCLUDE [ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] database.

```sql
EXEC sp_help_spatial_geography_histogram
    @tabname = Person.Address,
    @colname = SpatialLocation,
    @resolution = 64,
    @sample = 30;
```

## Related content

- [Spatial index stored procedures - arguments and properties](spatial-index-stored-procedures-arguments-and-properties.md)
