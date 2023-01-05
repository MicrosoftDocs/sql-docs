---
description: "MultiPoint"
title: "MultiPoint | Microsoft Docs"
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: 
ms.topic: conceptual
helpviewer_keywords: 
  - "MultiPoint geometry subtype [SQL Server]"
ms.assetid: 2aaab211-3aba-4dbd-90b7-095d997b1f62
author: MladjoA
ms.author: mlandzic
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# MultiPoint
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]
  A **MultiPoint** is a collection of zero or more points. The boundary of a **MultiPoint** instance is empty.  
  
## Examples  

### Example A.
The following example creates a `geometry MultiPoint` instance with SRID 23 and two points: one point with the coordinates (2, 3), one point with the coordinates (7, 8), and a Z value of 9.5.  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('MULTIPOINT((2 3), (7 8 9.5))', 23);  
```  
  
### Example B. 
The following example expresses the `MultiPoint` instance using `STMPointFromText()`.  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::STMPointFromText('MULTIPOINT((2 3), (7 8 9.5))', 23);  
```  
  
### Example C.
The following example uses the method `STGeometryN()` to retrieve a description of the first point in the collection.  
  
```sql  
SELECT @g.STGeometryN(1).STAsText();  
```  
  
## See Also  
 [Point](../../relational-databases/spatial/point.md)   
 [Spatial Data &#40;SQL Server&#41;](../../relational-databases/spatial/spatial-data-sql-server.md)  
  
  
