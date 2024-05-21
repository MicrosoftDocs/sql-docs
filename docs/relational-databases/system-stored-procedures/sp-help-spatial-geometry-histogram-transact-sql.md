---
title: "sp_help_spatial_geometry_histogram (Transact-SQL)"
description: Facilitates the keying of bounding box and grid parameters for a spatial index.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 05/14/2024
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_spatial_geometry_histogram"
  - "sp_help_spatial_geometry_histogram_TSQL"
helpviewer_keywords:
  - "sp_help_spatial_geometry_histogram"
dev_langs:
  - "TSQL"
---
# sp_help_spatial_geometry_histogram (Transact-SQL)

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Facilitates the keying of bounding box and grid parameters for a spatial index.

## Syntax

```syntaxsql
sp_help_spatial_geometry_histogram
    [ @tabname = ] N'tabname'
    , [ @colname = ] N'colname'
    , [ @resolution = ] resolution
    , [ @xmin = ] xmin
    , [ @ymin = ] ymin
    , [ @xmax = ] xmax
    , [ @ymax = ] ymax
    [ , [ @sample = ] sample ]
[ ; ]
```

## Arguments

#### [ @tabname = ] N'*tabname*'

The qualified or nonqualified name of the table for which the spatial index is specified. *@tabname* is **sysname**, with no default.

Quotation marks are required only if a qualified table is specified. If a fully qualified name, including a database name, is provided, the database name must be the name of the current database.

#### [ @colname = ] N'*colname*'

The name of the spatial column specified. *@colname* is **sysname**, with no default.

#### [ @resolution = ] *resolution*

The resolution of the bounding box. Valid values are from 10 to 5000. *@resolution* is **int**, with no default.

#### [ @xmin = ] *xmin*

The X-minimum bounding box property. *@xmin* is **float**, with no default.

#### [ @ymin = ] *ymin*

The Y-minimum bounding box property. *@ymin* is **float**, with no default.

#### [ @xmax = ] *xmax*

The X-maximum bounding box property. *@xmax* is **float**, with no default.

#### [ @ymax = ] *ymax*

The Y-maximum bounding box property. *@ymax* is **float**, with no default.

#### [ @sample = ] *sample*

The percentage of the table that is used. Valid values are from 0 to 100. *@sample* is **float**, with a default of `100`.

## Return values

A table value is returned. The following grid describes the column contents of the table.

| Column name | Data type | Description |
| --- | --- | --- |
| `cellid` | **int** | Represents the unique ID of each cell, counting starts from 1. |
| `cell` | **geometry** | A rectangular polygon that represents each cell. Cell shape is identical to the cell shape used for the spatial indexing. |
| `row_count` | **bigint** | Indicates the number of spatial objects that are touching or containing the cell. |

## Permissions

User must be a member of the **public** role. Requires `READ ACCESS` permission on the server and the object.

## Remarks

The [!INCLUDE [ssmanstudiofull-md](../../includes/ssmanstudiofull-md.md)] (SSMS) spatial tab shows a graphical representation of the results. You can query the results against the spatial window to get an approximate number of result items. Objects in the table might cover more than one cell, so the sum of cells might be larger than the number of actual objects.

An additional row might be added to the result set that holds the number of objects that are outside of the bounding box or touching the border of the bounding box. The `cellid` of this row is `0` and the `cell` of this row contains a `LineString` that represents the bounding box. This row represents the entire space outside the bounding box.

## Examples

The following example creates a sample table and then calls `sp_help_spatial_geometry_histogram` on the table. In this example, the database compatibility level is set to 110, but it can be higher.

```sql
USE AdventureWorksDW2012;
GO

-- Set database compatibility for circular arc segments
ALTER DATABASE AdventureWorksDW2012
SET COMPATIBILITY_LEVEL = 110;
GO

-- Create table to execute sp_help_spatial_geometry_histogram on
CREATE TABLE TownSites (
    Location geometry NULL,
    SiteName NVARCHAR(50) NULL
)
GO

-- Insert site data into table
DECLARE @g geometry;

SET @g = geometry::Parse('POINT(0 0)');

INSERT INTO TownSites (Location, SiteName)
SELECT @g, N'Booth Map';

SET @g = geometry::Parse('POLYGON((1 1, 1 2, 2 2, 2 1, 1 1))');

INSERT INTO TownSites (Location, SiteName)
SELECT @g, N'Town Hall';

SET @g = geometry::Parse('CURVEPOLYGON(COMPOUNDCURVE(CIRCULARSTRING(-1 0, 0 -1, 1 0),(1 0, 1 2, -1 0)))');

INSERT INTO TownSites (Location, SiteName)
SELECT @g, N'Main Park';

SET @g = geometry::Parse('CIRCULARSTRING(1 5, 2 2, 5 1)');

INSERT INTO TownSites (Location, SiteName)
SELECT @g, N'Main Road';

-- Call proc to see data within bounding box
EXEC sp_help_spatial_geometry_histogram
    @tabname = TownSites,
    @colname = Location,
    @resolution = 64,
    @xmin = -2,
    @ymin = -2,
    @xmax = 3,
    @ymax = 3,
    @sample = 100;
GO

DROP TABLE TownSites;
GO
```

## Related content

- [Spatial index stored procedures - arguments and properties](spatial-index-stored-procedures-arguments-and-properties.md)
