---
description: "STAsText (geometry Data Type)"
title: "STAsText (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STAsText_TSQL"
  - "STAsText (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STAsText (geometry Data Type)"
ms.assetid: e0decf5e-2858-4c56-b61a-6123f47fb51c
author: MladjoA
ms.author: mlandzic 
---
# STAsText (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a **geometry** instance. This text will not contain any Z (elevation) or M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
.STAsText ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **SqlChars**  
  
## Examples  
 The following example creates a `LineString` geometry instance from (0,0) to (2,3) from text. `STAsText()` returns the result in text.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 3)', 0);  
SELECT @g.STAsText();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

