---
title: "CircularString"
description: "CircularString is a collection of zero or more continuous circular arc segments in SQL Database Engine spatial data."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mlandzic, jovanpop
ms.date: 11/04/2024
ms.service: sql
ms.topic: concept-article
ms.custom:
  - ignite-2025
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# CircularString

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SQL endpoint Fabric DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]  

A **CircularString** is a collection of zero or more continuous circular arc segments. A circular arc segment is a curved segment defined by three points in a two-dimensional plane; the first point cannot be the same as the third point. If all three points of a circular arc segment are collinear, the arc segment is treated as a line segment.  
  
## CircularString instances  
 The drawing below shows valid **CircularString** instances:  
  
 :::image type="content" source="media/circularstring/circularstring.gif" alt-text="Diagram of CircularString example spatial measurements.":::
  
### Accepted instances
 A **CircularString** instance is accepted if it is either empty or contains an odd number of points, n, where n > 1. The following **CircularString** instances are accepted.  
  
```sql  
DECLARE @g1 geometry = 'CIRCULARSTRING EMPTY';  
DECLARE @g2 geometry = 'CIRCULARSTRING(1 1, 2 0, -1 1)';  
DECLARE @g3 geometry = 'CIRCULARSTRING(1 1, 2 0, 2 0, 2 0, 1 1)';  
```  
  
 `@g3` shows that **CircularString** instance might be accepted, but not valid. The following CircularString instance declaration is not accepted. This declaration throws a `System.FormatException`.  
  
```sql  
DECLARE @g geometry = 'CIRCULARSTRING(1 1, 2 0, 2 0, 1 1)';  
```  
  
### Valid instances
 A valid **CircularString** instance must be empty or have the following attributes:  
  
-   It must contain at least one circular arc segment (that is, have a minimum of three points).  
-   The last endpoint for each circular arc segment in the sequence, except for the last segment, must be the first endpoint for the next segment in the sequence.  
-   It must have an odd number of points.  
-   It cannot overlap itself over an interval.  
-   Although **CircularString** instances can contain line segments, these line segments must be defined by three collinear points.  
  
The following example shows valid **CircularString** instances.  
  
```sql  
DECLARE @g1 geometry = 'CIRCULARSTRING EMPTY';  
DECLARE @g2 geometry = 'CIRCULARSTRING(1 1, 2 0, -1 1)';  
DECLARE @g3 geometry = 'CIRCULARSTRING(1 1, 2 0, 2 0, 1 1, 0 1)';  
DECLARE @g4 geometry = 'CIRCULARSTRING(1 1, 2 2, 2 2)';  
SELECT @g1.STIsValid(), @g2.STIsValid(), @g3.STIsValid(),@g4.STIsValid();  
```  
  
A **CircularString** instance must contain at least two circular arc segments to define a complete circle. A **CircularString** instance cannot use a single circular arc segment (such as (1 1, 3 1, 1 1)) to define a complete circle. Use (1 1, 2 2, 3 1, 2 0, 1 1) to define the circle.  
  
The following example shows CircularString instances that are not valid.  
  
```sql  
DECLARE @g1 geometry = 'CIRCULARSTRING(1 1, 2 0, 1 1)';  
DECLARE @g2 geometry = 'CIRCULARSTRING(0 0, 0 0, 0 0)';  
SELECT @g1.STIsValid(), @g2.STIsValid();  
```  
  
### Instances with collinear points

In the following cases a circular arc segment will be treated as a line segment:  
  
-   When all three points are collinear (for example, (1 3, 4 4, 7 5)).  
-   When the first and middle point are the same, but the third point is different (for example, (1 3, 1 3, 7 5)).  
-   When the middle and last point are the same, but the first point is different (for example, (1 3, 4 4, 4 4)).  
  
## Examples
  
<a id="a-instantiating-a-geometry-instance-with-an-empty-circularstring"></a>

### A. Instantiate a Geometry Instance with an Empty CircularString
 This example shows how to create an empty **CircularString** instance:  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::Parse('CIRCULARSTRING EMPTY');  
```  
  
<a id="b-instantiating-a-geometry-instance-using-a-circularstring-with-one-circular-arc-segment"></a>

### B. Instantiate a Geometry Instance Using a CircularString with One Circular Arc Segment
 The following example shows how to create a **CircularString** instance with a single circular arc segment (half-circle):  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry:: STGeomFromText('CIRCULARSTRING(2 0, 1 1, 0 0)', 0);  
SELECT @g.ToString();  
```  
  
<a id="c-instantiating-a-geometry-instance-using-a-circularstring-with-multiple-circular-arc-segments"></a>

### C. Instantiate a Geometry Instance Using a CircularString with Multiple Circular Arc Segments
 The following example shows how to create a **CircularString** instance with more than one circular arc segment (full circle):  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::Parse('CIRCULARSTRING(2 1, 1 2, 0 1, 1 0, 2 1)');  
SELECT 'Circumference = ' + CAST(@g.STLength() AS NVARCHAR(10));    
```  
  
[!INCLUDE [ssResult](../../includes/ssresult-md.md)]
  
```output
Circumference = 6.28319  
```  
  
Compare the output when **LineString** is used instead of **CircularString**:  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(2 1, 1 2, 0 1, 1 0, 2 1)', 0);  
SELECT 'Perimeter = ' + CAST(@g.STLength() AS NVARCHAR(10));  
```  
  
[!INCLUDE [ssResult](../../includes/ssresult-md.md)]

```output
Perimeter = 5.65685  
```  
  
The value for the **CircularString** example is close to 2∏, which is the actual circumference of the circle.  
  
<a id="d-declaring-and-instantiating-a-geometry-instance-with-a-circularstring-in-the-same-statement"></a>

### D. Declare and Instantiating a Geometry Instance with a CircularString in the Same Statement
 This snippet shows how to declare and instantiate a **geometry** instance with a **CircularString** in the same statement:  
  
```sql  
DECLARE @g geometry = 'CIRCULARSTRING(0 0, 1 2.1082, 3 6.3246, 0 7, -3 6.3246, -1 2.1082, 0 0)';  
```  
  
<a id="e-instantiating-a-geography-instance-with-a-circularstring"></a>

### E. Instantiate a Geography Instance with a CircularString
 The following example shows how to declare and instantiate a **geography** instance with a **CircularString**:  
  
```sql  
DECLARE @g geography = 'CIRCULARSTRING(-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653)';  
```  
  
<a id="f-instantiating-a-geometry-instance-with-a-circularstring-that-is-a-straight-line"></a>

### F. Instantiate a Geometry Instance with a CircularString that is a Straight Line
 The following example shows how to create a **CircularString** instance that is a straight line:  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('CIRCULARSTRING(0 0, 1 2, 2 4)', 0);  
```  
  
## Related content

- [Spatial Data Types Overview](spatial-data-types-overview.md)
- [CompoundCurve](compoundcurve.md)
- [MakeValid (geography Data Type)](../../t-sql/spatial-geography/makevalid-geography-data-type.md)
- [MakeValid (geometry Data Type)](../../t-sql/spatial-geometry/makevalid-geometry-data-type.md)
- [STIsValid (geometry Data Type)](../../t-sql/spatial-geometry/stisvalid-geometry-data-type.md)
- [STIsValid (geography Data Type)](../../t-sql/spatial-geography/stisvalid-geography-data-type.md)
- [STLength (geometry Data Type)](../../t-sql/spatial-geometry/stlength-geometry-data-type.md)
- [STStartPoint (geometry Data Type)](../../t-sql/spatial-geometry/ststartpoint-geometry-data-type.md)
- [STEndpoint (geometry Data Type)](../../t-sql/spatial-geometry/stendpoint-geometry-data-type.md)
- [STPointN (geometry Data Type)](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md)
- [STNumPoints (geometry Data Type)](../../t-sql/spatial-geometry/stnumpoints-geometry-data-type.md)
- [STIsRing (geometry Data Type)](../../t-sql/spatial-geometry/stisring-geometry-data-type.md)
- [STIsClosed (geometry Data Type)](../../t-sql/spatial-geometry/stisclosed-geometry-data-type.md)
- [STPointOnSurface (geometry Data Type)](../../t-sql/spatial-geometry/stpointonsurface-geometry-data-type.md)
- [LineString](linestring.md)
