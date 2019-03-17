---
title: "MultiPolygon | Microsoft Docs"
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "MultiPolygon geometry subtype [SQL Server]"
  - "geometry subtypes [SQL Server]"
ms.assetid: 2c5db358-2a16-49d9-aac5-a74e86813932
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# MultiPolygon
[!INCLUDE[appliesto-ss-asdb-xxxx-xxx-md](../../includes/appliesto-ss-asdb-xxxx-xxx-md.md)]
  A **MultiPolygon** instance is a collection of zero or more **Polygon** instances.  
  
## Polygon Instances  
 The illustration below shows examples of **MultiPolygon** instances.  
  
 ![Examples of geometry MultiPolygon instances](../../relational-databases/spatial/media/multipolygon.gif "Examples of geometry MultiPolygon instances")  
  
 As shown in the illustration:  
  
-   Figure 1 is a **MultiPolygon** instance with two **Polygon** elements. The boundary is defined by the two exterior rings and the three interior rings.  
  
-   Figure 2 is a **MultiPolygon** instance with two **Polygon** elements. The boundary is defined by the two exterior rings and the three interior rings. The two **Polygon** elements intersect at a tangent point.  
  
### Accepted Instances  
 A **MultiPolygon** instance is accepted one of the following conditions is met.  
  
-   It is an empty **MultiPolygon** instance.  
  
-   All instances comprising the **MultiPolygon** instance are accepted **Polygon** instances. For more information on accepted **Polygon** instances, see [Polygon](../../relational-databases/spatial/polygon.md).  
  
The following examples show accepted **MultiPolygon** instances.  
  
```sql  
DECLARE @g1 geometry = 'MULTIPOLYGON EMPTY';  
DECLARE @g2 geometry = 'MULTIPOLYGON(((1 1, 1 -1, -1 -1, -1 1, 1 1)),((1 1, 3 1, 3 3, 1 3, 1 1)))';  
DECLARE @g3 geometry = 'MULTIPOLYGON(((2 2, 2 -2, -2 -2, -2 2, 2 2)),((1 1, 3 1, 3 3, 1 3, 1 1)))';  
```  
  
The following example shows a MultiPolygon instance that will throw a `System.FormatException`.  
  
```sql  
DECLARE @g geometry = 'MULTIPOLYGON(((1 1, 1 -1, -1 -1, -1 1, 1 1)),((1 1, 3 1, 3 3)))';  
```  
  
The second instance in the MultiPolygon is a LineString instance and not an accepted Polygon instance.  
  
### Valid Instances  
 A **MultiPolygon** instance is valid if it is an empty **MultiPolygon** instance or if it meets the following criteria.  
  
1.  All of the instances comprising the **MultiPolygon** instance are valid **Polygon** instances. For valid **Polygon** instances, see [Polygon](../../relational-databases/spatial/polygon.md).  
  
2.  None of the **Polygon** instances comprising the **MultiPolygon** instance overlap.  
  
The following example shows two valid **MultiPolygon** instances and one invalid **MultiPolygon** instance.  
  
```sql  
DECLARE @g1 geometry = 'MULTIPOLYGON EMPTY';  
DECLARE @g2 geometry = 'MULTIPOLYGON(((1 1, 1 -1, -1 -1, -1 1, 1 1)),((1 1, 3 1, 3 3, 1 3, 1 1)))';  
DECLARE @g3 geometry = 'MULTIPOLYGON(((2 2, 2 -2, -2 -2, -2 2, 2 2)),((1 1, 3 1, 3 3, 1 3, 1 1)))';  
SELECT @g1.STIsValid(), @g2.STIsValid(), @g3.STIsValid();  
```  
  
`@g2` is valid because the two **Polygon** instances touch only at a tangent point. `@g3` is not valid because the interiors of the two **Polygon** instances overlap each other.  
  
## Examples  
### Example A.
The following example shows the creation of a `geometry``MultiPolygon` instance and returns the Well-Known Text (WKT) of the second component.  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::Parse('MULTIPOLYGON(((0 0, 0 3, 3 3, 3 0, 0 0), (1 1, 1 2, 2 1, 1 1)), ((9 9, 9 10, 10 9, 9 9)))');  
SELECT @g.STGeometryN(2).STAsText();  
```  
  
## Example B.
This example instantiates an empty `MultiPolygon` instance.  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::Parse('MULTIPOLYGON EMPTY');  
```  
  
## See Also  
 [Polygon](../../relational-databases/spatial/polygon.md)   
 [STArea &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/starea-geometry-data-type.md)   
 [STCentroid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stcentroid-geometry-data-type.md)   
 [STPointOnSurface &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stpointonsurface-geometry-data-type.md)   
 [Spatial Data &#40;SQL Server&#41;](../../relational-databases/spatial/spatial-data-sql-server.md)  
  
  
