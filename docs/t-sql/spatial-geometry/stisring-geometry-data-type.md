---
description: "STIsRing (geometry Data Type)"
title: "STIsRing (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STIsRing_TSQL"
  - "STIsRing (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIsRing (geometry Data Type)"
ms.assetid: ea0063be-1c74-4cc4-ac6f-b65321ddfa54
author: MladjoA
ms.author: mlandzic 
---
# STIsRing (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns 1 if a **geometry** instance fulfills the following requirements:
-   It is a **LineString** instance.  
-   It is closed.  
-   It is simple.  
-   Returns 0 if the **LineString** instance does not meet the requirements.  

 For a **geometry** instance to be closed and simple, both [STIsClosed()](../../t-sql/spatial-geometry/stisclosed-geometry-data-type.md) and [STIsSimple()](../../t-sql/spatial-geometry/stissimple-geometry-data-type.md) must return 1 when invoked on the instance. To determine the instance type of a **geometry**, use [STGeometryType()](../../t-sql/spatial-geometry/stgeometrytype-geometry-data-type.md).  
  
## Syntax  
  
```  
  
.STIsRing ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method returns null if the instance is not a **LineString**.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STIsRing()` to test whether the instance is a ring.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0, 0 0)', 0);  
SELECT @g.STIsRing();  
```  
  
## See Also  
 [STIsClosed &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stisclosed-geometry-data-type.md)   
 [STGeometryType &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stgeometrytype-geometry-data-type.md)   
 [STIsSimple &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stissimple-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

