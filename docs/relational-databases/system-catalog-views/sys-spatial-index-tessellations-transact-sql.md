---
title: "sys.spatial_index_tessellations (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "spatial_index_tessellations"
  - "sys.spatial_index_tessellations_TSQL"
  - "spatial_index_tessellations_TSQL"
  - "sys.spatial_index_tessellations"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.spatial_index_tessellations catalog view"
ms.assetid: 8b17a9a4-b57f-4220-8138-fc73581b1670
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.spatial_index_tessellations (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

 Represents the information about the tessellation scheme and parameters of each of the spatial indexes.  
  
> [!NOTE]  
>  For information about tessellation, see [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md).  
  

|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|object_id|**int**|ID of the object on which the index is defined. Each (object_id, index_id) pair has a corresponding entry in [sys.spatial_indexes](../../relational-databases/system-catalog-views/sys-spatial-indexes-transact-sql.md).|  
|index_id|**int**|ID of the spatial index in which the indexed column is defined|  
|tessellation_scheme|**sysname**|Name of the tessellation scheme, one of: GEOMETRY_GRID,  GEOGRAPHY_GRID|  
|bounding_box_xmin|**float(53)**|X-coordinate of the lower-left corner of the bounding box, one of: NULL = Not applicable for a given tessellation scheme (such as GEOGRAPHY_GRID) *n* = If tessellation_scheme is GEOMETRY_GRID, the x-min coordinate value.                     **Note:** The coordinates defined by the bounding box parameters are interpreted for each object according to its [Spatial Reference Identifier (SRID)](../../relational-databases/spatial/spatial-reference-identifiers-srids.md).|  
|bounding_box_ymin|**float(53)**|Y-coordinate of the lower-left corner of the bounding box, one of: NULL = Not applicable for a given tessellation scheme (such as GEOGRAPHY_GRID) *n* = If tessellation_scheme is GEOMETRY_GRID, the y-min coordinate value|  
|bounding_box_xmax|**float(53)**|X-coordinate of the upper-right corner of the bounding box, one of: NULL = Not applicable for a given tessellation scheme (such as GEOGRAPHY_GRID) *n* = If tessellation_scheme is GEOMETRY_GRID, the x-max coordinate value|  
|bounding_box_ymax|**float(53)**|Y-coordinate of upper-right corner of the bounding box, one of: NULL = Not applicable for a given tessellation scheme (such as GEOGRAPHY_GRID) *n* = If tessellation_scheme is GEOMETRY_GRID, the y-max coordinate value|  
|level_1_grid|**smallint**|Grid density for the top-level grid. If tessellation_scheme is GEOMETRY_GRID or GEOGRAPHY_GRID, one of:          16 = 4 by 4 grid (LOW) 64 = 8 by 8 grid (MEDIUM) 256 = 16 by 16 grid (HIGH) NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|level_1_grid_desc|**nvarchar(60)**|Grid density for the top-level grid, one of: LOW MEDIUM HIGH NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|level_2_grid|**smallint**|Grid density for the 2nd-level grid. If tessellation_scheme is GEOMETRY_GRID or GEOGRAPHY_GRID, one of: 16 = 4 by 4 grid (LOW) 64 = 8 by 8 grid (MEDIUM) 256 = 16 by 16 grid (HIGH) NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|level_2_grid_desc|**nvarchar(60)**|Grid density for the 2nd-level grid, one of: LOW MEDIUM HIGH NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|level_3_grid|**smallint**|Grid density for the 3rd-level grid.   If tessellation_scheme is GEOMETRY_GRID or GEOGRAPHY_GRID, one of: 16 = 4 by 4 grid (LOW) 64 = 8 by 8 grid (MEDIUM) 256 = 16 by 16 grid (HIGH) NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|level_3_grid_desc|**nvarchar(60)**|Grid density for the 3rd-level grid, one of:LOW MEDIUM HIGH NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|level_4_grid|**smallint**|Grid density for the 4th-level grid. If tessellation_scheme is GEOMETRY_GRID or GEOGRAPHY_GRID, one of: 16 = 4 by 4 grid (LOW)64 = 8 by 8 grid (MEDIUM) 256 = 16 by 16 grid (HIGH) NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|level_4_grid_desc|**nvarchar(60)**|Grid density for the 4th-level grid, one of:< LOW MEDIUM HIGH NULL = Not applicable for given spatial index type or tessellation scheme.  NULL is returned when new SQL Server 11 tessellation is used.|  
|cells_per_object|**int**|Number of cells per spatial object, one of: If tessellation_scheme is GEOMETRY_GRID or GEOGRAPHY_GRID, *n* = number of cells per object NULL = Not applicable for given  spatial index type or tessellation scheme|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
## See Also  
 [Object Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/object-catalog-views-transact-sql.md)   
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)   
 [sys.objects &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md)   
 [sys.spatial_indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-spatial-indexes-transact-sql.md)   
 [sys.indexes &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-indexes-transact-sql.md)   
 [sys.index_columns &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-index-columns-transact-sql.md)  
  
  
