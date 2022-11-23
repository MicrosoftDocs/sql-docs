---
description: "STWithin (geometry Data Type)"
title: "STWithin (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STWithin_TSQL"
  - "STWithin (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STWithin (geometry Data Type)"
ms.assetid: f845d28c-8029-4e2b-bcf0-71c52a592501
author: MladjoA
ms.author: mlandzic 
---
# STWithin (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns 1 if a **geometry** instance is completely within another **geometry** instance; otherwise, returns 0. The `STWithin` command is case-sensitive.
  
## Syntax  
  
```  
  
.STWithin ( other_geometry )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which `STWithin()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.
  
## Examples  
 The following example uses `STWithin()` to test two `geometry` instances to see if the first instance is completely within the second instance.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 2 0, 2 2, 0 2, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POLYGON((1 1, 3 1, 3 3, 1 3, 1 1))', 0);  
SELECT @g.STWithin(@h);  
```  
  
## See Also  
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

