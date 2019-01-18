---
title: "sp_help_spatial_geometry_histogram (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_help_spatial_geometry_histogram"
  - "sp_help_spatial_geometry_histogram_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_help_spatial_geometry_histogram"
ms.assetid: 036aaf61-df3e-40f7-aa4e-62983c5a37bd
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# sp_help_spatial_geometry_histogram (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Facilitates the keying of bounding box and grid parameters for a spatial index.  
  
## Syntax  
  
```  
  
sp_help_spatial_geometry_histogram [ @tabname =] 'tabname'   
     [ , [ @colname = ] 'columnname' ]   
     [ , [ @resolution = ] 'resolution' ]  
     [ , [ @xmin = ] 'minx' ]   
     [ , [ @ymin = ] 'miny' ]   
     [ ,.[ @xmax = ] 'maxx' ]  
     [ , [ @ymax = ] 'maxy' ]  
     [ , [ @sample = ] 'sample' ]  
```  
  
## Arguments  
 [ **@tabname =**] **'***tabname***'**  
 Is the qualified or nonqualified name of the table for which the spatial index has been specified.  
  
 Quotation marks are required only if a qualified table is specified. If a fully qualified name, including a database name, is provided, the database name must be the name of the current database. *tabname* is **sysname**, with no default.  
  
 [ **@colname =** ] **'***colname***'**  
 Is the name of the spatial column specified. *colname* is a **sysname**, with no default.  
  
 [ **@resolution =** ] **'***resolution***'**  
 Is the resolution of the bounding box. Valid values are from 10 to 5000. *resolution* is a **tinyint**, with no default.  
  
 [ **@xmin =** ] **'***xmin***'**  
 Is the X-minimum bounding box property. *xmin* is a **float**, with no default.  
  
 [ **@ymin =** ] **'***ymin***'**  
 Is the Y-minimum bounding box property. *ymin* is a **float**, with no default.  
  
 [ **@xmax =** ] **'***xmax***'**  
 Is the X-maximum bounding box property. *xmax* is a **float**, with no default.  
  
 [ **@ymax =** ] **'***ymax***'**  
 Is the Y-maximum bounding box property. *ymax* is a **float**, with no default.  
  
 [ **@sample =** ] **'***sample***'**  
 Is the percentage of the table that is used. Valid values are from 0 to 100. *sample* is a **float**. Default value is 100.  
  
## Property Value/Return Value  
 A table value is returned. The following grid describes the column contents of the table.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**cellid**|**int**|Represents the unique ID of each cell, counting starts from 1.|  
|**cell**|**geometry**|Is a rectangular polygon that represents each cell. Cell shape is identical to the cell shape used for the spatial indexing.|  
|**row_count**|**bigint**|Indicates the number of spatial objects that are touching or containing the cell.|  
  
## Permissions  
 User must be a member of the **public** role. Requires READ ACCESS permission on the server and the object.  
  
## Remarks  
 SSMS spatial tab shows a graphical representation of the results. You can query the results against the spatial window to get an approximate number of result items. Objects in the table may cover more than one cell, so the sum of cells may be larger than the number of actual objects.  
  
 An additional row may be added to the result set that holds the number of objects that are outside of the bounding box or touching the border of the bounding box. The **cellid** of this row is 0 and the **cell** of this row contains a **LineString** that represents the bounding box. This row represents the entire space outside the bounding box.  
  
## Examples  
 The following example creates a sample table and then calls **sp_help_spatial_geometry_histogram** on the table.  
  
 `USE AdventureWorksDW2012`  
  
 `GO`  
  
 `-- Set database compatibility for circular arc segments`  
  
 `ALTER DATABASE AdventureWorksDW2012`  
  
 `SET COMPATIBILITY_LEVEL = 110;`  
  
 `GO`  
  
 `-- Create table to execute sp_help_spatial_geometry_histogram on`  
  
 `CREATE TABLE TownSites`  
  
 `(`  
  
 `Location geometry NULL,`  
  
 `SiteName nvarchar(50) NULL`  
  
 `)`  
  
 `GO`  
  
 `-- Insert site data into table`  
  
 `DECLARE @g geometry;`  
  
 `SET @g = geometry::Parse('POINT(0 0)');`  
  
 `INSERT INTO TownSites(Location, SiteName)`  
  
 `SELECT @g, N'Booth Map';`  
  
 `SET @g = geometry::Parse('POLYGON((1 1, 1 2, 2 2, 2 1, 1 1))');`  
  
 `INSERT INTO TownSites(Location, SiteName)`  
  
 `SELECT @g, N'Town Hall';`  
  
 `SET @g = geometry::Parse('CURVEPOLYGON(COMPOUNDCURVE(CIRCULARSTRING(-1 0, 0 -1, 1 0),(1 0, 1 2, -1 0)))');`  
  
 `INSERT INTO TownSites(Location, SiteName)`  
  
 `SELECT @g, N'Main Park';`  
  
 `SET @g = geometry::Parse('CIRCULARSTRING(1 5, 2 2, 5 1)');`  
  
 `INSERT INTO TownSites(Location, SiteName)`  
  
 `SELECT @g, N'Main Road';`  
  
 `-- Call proc to see data within bounding box`  
  
 `EXEC sp_help_spatial_geometry_histogram @tabname = TownSites, @colname = Location, @resolution = 64, @xmin = -2, @ymin = -2, @xmax = 3, @ymax = 3, @sample = 100;`  
  
 `GO`  
  
 `DROP TABLE TownSites;`  
  
 `GO`  
  
## See Also  
 [Spatial Index Stored Procedures &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/1be0f34e-3d5a-4a1f-9299-bd482362ec7a)  
  
  
