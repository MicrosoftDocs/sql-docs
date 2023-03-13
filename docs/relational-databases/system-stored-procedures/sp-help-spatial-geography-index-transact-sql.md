---
title: "sp_help_spatial_geography_index (Transact-SQL)"
description: "sp_help_spatial_geography_index (Transact-SQL)"
author: markingmyname
ms.author: maghan
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sp_help_spatial_geography_index"
  - "sp_help_spatial_geography_index_TSQL"
helpviewer_keywords:
  - "sp_help_spatial_geography_index procedure"
dev_langs:
  - "TSQL"
---
# sp_help_spatial_geography_index (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the names and values for a specified set of properties about a **geography** spatial index. The result is returned in a table format. You can choose to return a core set of properties or all properties of the index.  
  
 :::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_help_spatial_geography_index [ @tabname =] 'tabname'   
     [ , [ @indexname = ] 'indexname' ]   
     [ , [ @verboseoutput = ] 'verboseoutput' ]   
     [ , [ @query_sample = ] 'query_sample' ]   
```  
  
## Arguments  
 See [Arguments and Properties of Spatial Index Stored Procedures](../../relational-databases/system-stored-procedures/spatial-index-stored-procedures-arguments-and-properties.md).  
  
## Properties  
 See [Arguments and Properties of Spatial Index Stored Procedures](../../relational-databases/system-stored-procedures/spatial-index-stored-procedures-arguments-and-properties.md).  
  
## Permissions  
 User must be assigned a PUBLIC role to access the procedure. Requires READ ACCESS permission on the server and the object.  
  
## Remarks  
  
## Example  
 The following example uses `sp_help_spatial_geography_index` to investigate the **geography** spatial index **SIndx_SpatialTable_geography_col2** defined on table **geography_col** for the given query sample in **\@qs**. This example returns only the core properties of the specified index.  
  
```  
declare @qs geography  
        ='POLYGON((-90.0 -180, -90 180.0, 90 180.0, 90 -180, -90 -180.0))';  
exec sp_help_spatial_geography_index 'geography_col', 'SIndx_SpatialTable_geography_col2', 0, @qs;  
```  
  
 The bounding box of a **geography** instance is the whole earth.  
  
## Requirements  
  
## See Also  
 [Spatial Index Stored Procedures](./spatial-index-stored-procedures-arguments-and-properties.md)   
 [sp_help_spatial_geography_index](../../relational-databases/system-stored-procedures/sp-help-spatial-geography-index-transact-sql.md)   
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)   
 [Spatial Data &#40;SQL Server&#41;](../../relational-databases/spatial/spatial-data-sql-server.md)  
  
