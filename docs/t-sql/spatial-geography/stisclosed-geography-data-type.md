---
title: "STIsClosed (geography Data Type)"
description: "STIsClosed (geography Data Type)"
author: MladjoA
ms.author: mlandzic
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "STIsClosed (geography Data Type)"
  - "STIsClosed_TSQL"
helpviewer_keywords:
  - "STIsClosed method"
dev_langs:
  - "TSQL"
---
# STIsClosed (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns 1 if the start and end points of the given **geography** instance are the same. Returns 1 for **geography** collection types if each contained **geography** instance is closed. Returns 0 if the instance is not closed.  
  
 This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.STIsClosed ( )  
```  
  
## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method returns 0 if any figures of a **geography** instance are points, or if the instance is empty.  
  
 This method returns true if a **FullGlobe** instance is a **Polygon** or other type of instance.  
  
 All **Polygon** instances are considered closed.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STIsClosed()` to test if the `Polygon` is closed.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
SELECT @g.STIsClosed();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
