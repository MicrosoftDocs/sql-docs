---
title: "Create, construct, and query geometry instances"
description: "Geometry instances represent data in a Euclidean (flat) coordinate system. Learn how to create, construct, and query geometry data in SQL Database Engine spatial data."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: mlandzic, jovanpop
ms.date: 11/04/2024
ms.service: sql
ms.topic: how-to
ms.custom:
  - ignite-2025
helpviewer_keywords:
  - "planar spatial data [SQL Server], getting started"
  - "geometry data type [SQL Server], getting started"
monikerRange: "=azuresqldb-current || >=sql-server-2017 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# Create, construct, and query geometry instances

[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance Fabric SQL endpoint Fabric DW FabricSQLDB](../../includes/applies-to-version/sql-asdb-asdbmi-fabricse-fabricdw-fabricsqldb.md)]  

  The planar spatial data type, **geometry**, represents data in a Euclidean (flat) coordinate system. This type is implemented as a common language runtime (CLR) data type in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The **geometry** type is predefined and available in each database. You can create table columns of type **geometry** and operate on **geometry** data in the same manner as you would use other CLR types.  
  
 The **geometry** data type (planar) supported by [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] conforms to the Open Geospatial Consortium (OGC) Simple Features for SQL Specification version 1.1.0.  
  
 For more information on OGC specifications, see the following:  
  
-   [OGC Specifications, Simple Feature Access Part 1 - Common Architecture](https://go.microsoft.com/fwlink/?LinkId=93628)  
  
-   [OGC Specifications, Simple Feature Access Part 2 - SQL Options](https://go.microsoft.com/fwlink/?LinkId=93629)  
  
 [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports a subset of the existing GML 3.1 standard which is defined in the following schema: [https://schemas.microsoft.com/sqlserver/profiles/gml/SpatialGML.xsd](https://go.microsoft.com/fwlink/?LinkId=230959).  
  
<a id="creating"></a>

## Create or constructing a new geometry instance
  
<a id="existing"></a>

### Create a New geometry instance from an Existing Instance
 The **geometry** data type provides numerous built-in methods you can use to create new **geometry** instances based on existing instances.  
  
 **To create a buffer around a geometry**  
 [STBuffer (geometry Data Type)](../../t-sql/spatial-geometry/stbuffer-geometry-data-type.md)  
  
 [BufferWithTolerance (geometry Data Type)](../../t-sql/spatial-geometry/bufferwithtolerance-geometry-data-type.md)  
  
 **To create a simplified version of a geometry**  
 [Reduce (geometry Data Type)](../../t-sql/spatial-geometry/reduce-geometry-data-type.md)  
  
 **To create the convex hull of a geometry**  
 [STConvexHull (geometry Data Type)](../../t-sql/spatial-geometry/stconvexhull-geometry-data-type.md)  
  
 **To create a geometry from the intersection of two geometries**  
 [STIntersection (geometry Data Type)](../../t-sql/spatial-geometry/stintersection-geometry-data-type.md)  
  
 **To create a geometry from the union of two geometries**  
 [STUnion (geometry Data Type)](../../t-sql/spatial-geometry/stunion-geometry-data-type.md)  
  
 **To create a geometry from the points where one geometry does not overlap another**  
 [STDifference (geometry Data Type)](../../t-sql/spatial-geometry/stdifference-geometry-data-type.md)  
  
 **To create a geometry from the points where two geometries do not overlap**  
 [STSymDifference (geometry Data Type)](../../t-sql/spatial-geometry/stsymdifference-geometry-data-type.md)  
  
 **To create an arbitrary Point instance that lies on an existing geometry**  
 [STPointOnSurface (geometry Data Type)](../../t-sql/spatial-geometry/stpointonsurface-geometry-data-type.md)  
  
  
<a id="wkt"></a>

### Construct a geometry instance from Well-Known Text input
 The **geometry** data type provides several built-in methods that generate a geometry from the Open Geospatial Consortium (OGC) WKT representation. The WKT standard is a text string that allows geometry data to be exchanged in textual form.  
  
 **To construct any type of geometry instance from WKT input**  
 [STGeomFromText (geometry Data Type)](../../t-sql/spatial-geometry/stgeomfromtext-geometry-data-type.md)  
  
 [Parse (geometry Data Type)](../../t-sql/spatial-geometry/parse-geometry-data-type.md)  
  
 **To construct a geometry Point instance from WKT input**  
 [STPointFromText (geometry Data Type)](../../t-sql/spatial-geometry/stpointfromtext-geometry-data-type.md)  
  
 **To construct a geometry MultiPoint instance from WKT input**  
 [STMPointFromText (geometry Data Type)](../../t-sql/spatial-geometry/stmpointfromtext-geometry-data-type.md)  
  
 **To construct a geometry LineString instance from WKT input**  
 [STLineFromText (geometry Data Type)](../../t-sql/spatial-geometry/stlinefromtext-geometry-data-type.md)  
  
 **To construct a geometry MultiLineString instance from WKT input**  
 [STMLineFromText (geometry Data Type)](../../t-sql/spatial-geometry/stmlinefromtext-geometry-data-type.md)  
  
 **To construct a geometry Polygon instance from WKT input**  
 [STPolyFromText (geometry Data Type)](../../t-sql/spatial-geometry/stpolyfromtext-geometry-data-type.md)  
  
 **To construct a geometry MultiPolygon instance from WKT input**  
 [STMPolyFromText (geometry Data Type)](../../t-sql/spatial-geometry/stmpolyfromtext-geometry-data-type.md)  
  
 **To construct a geometry GeometryCollection instance from WKT input**  
 [STGeomCollFromText (geometry Data Type)](../../t-sql/spatial-geometry/stgeomcollfromtext-geometry-data-type.md)  
  
  
<a id="wkb"></a>

### Construct a geometry instance from Well-Known Binary input
 WKB is a binary format specified by the Open Geospatial Consortium (OGC) that permits **geometry** data to be exchanged between a client application and a SQL database. The following functions accept WKB input to construct geometries:  
  
 **To construct any type of geometry instance from WKB input**  
 [STGeomFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stgeomfromwkb-geometry-data-type.md)  
  
 **To construct a geometry Point instance from WKB input**  
 [STPointFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stpointfromwkb-geometry-data-type.md)  
  
 **To construct a geometry MultiPoint instance from WKB input**  
 [STMPointFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stmpointfromwkb-geometry-data-type.md)  
  
 **To construct a geometry LineString instance from WKB input**  
 [STLineFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stlinefromwkb-geometry-data-type.md)  
  
 **To construct a geometry MultiLineString instance from WKB input**  
 [STMLineFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stmlinefromwkb-geometry-data-type.md)  
  
 **To construct a geometry Polygon instance from WKB input**  
 [STPolyFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stpolyfromwkb-geometry-data-type.md)  
  
 **To construct a geometry MultiPolygon instance from WKB input**  
 [STMPolyFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stmpolyfromwkb-geometry-data-type.md)  
  
 **To construct a geometry GeometryCollection instance from WKB input**  
 [STGeomCollFromWKB (geometry Data Type)](../../t-sql/spatial-geometry/stgeomcollfromwkb-geometry-data-type.md)  
  
  
<a id="gml"></a>

### Construct a geometry instance from GML Text Input
 The **geometry** data type provides a method that generates a **geometry** instance from GML, an XML representation of geometric objects. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports a subset of GML.  
  
 **To construct any type of geometry instance from GML input**  
 [GeomFromGml (geometry Data Type)](../../t-sql/spatial-geometry/geomfromgml-geometry-data-type.md)  
  
  
<a id="returning"></a>

## Return Well-Known Text and Well-Known Binary from a geometry instance
 You can use the following methods to return either the WKT or WKB format of a **geometry** instance:  
  
 **To return the WKT representation of a geometry instance**  
 [STAsText (geometry Data Type)](../../t-sql/spatial-geometry/stastext-geometry-data-type.md)  
  
 [ToString (geometry Data Type)](../../t-sql/spatial-geometry/tostring-geometry-data-type.md)  
  
 **To return the WKT representation of a geometry instance including any Z and M values**  
 [AsTextZM (geometry Data Type)](../../t-sql/spatial-geometry/astextzm-geometry-data-type.md)  
  
 **To return the WKB representation of a geometry instance**  
 [STAsBinary (geometry Data Type)](../../t-sql/spatial-geometry/stasbinary-geometry-data-type.md)  
  
 **To return a GML representation of a geometry instance**  
 [AsGml (geometry Data Type)](../../t-sql/spatial-geometry/asgml-geometry-data-type.md)  
  
  
<a id="querying"></a>

## Query the properties and behaviors of geometry instances

 All **geometry** instances have a number of properties that can be retrieved through methods that [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] provides. The following topics define the properties and behaviors of geometry types, and the methods for querying each one.  
  
<a id="valid"></a>

### Validity, instance type, and GeometryCollection information
 Once a **geometry** instance is constructed, you can use the following methods to determine if it is well-formed, return the instance type, or, if it is a collection instance, return a specific **geometry** instance.  
  
 **To return the instance type of a geometry**  
 [STGeometryType (geometry Data Type)](../../t-sql/spatial-geometry/stgeometrytype-geometry-data-type.md)  
  
 **To determine if a geometry is a given instance type**  
 [InstanceOf (geometry Data Type)](../../t-sql/spatial-geometry/instanceof-geometry-data-type.md)  
  
 **To determine if a geometry instance is well-formed for its instance type**  
 [STIsValid (geometry Data Type)](../../t-sql/spatial-geometry/stisvalid-geometry-data-type.md)  
  
 **To convert a geometry instance to a well-formed geometry instance with an instance type**  
 [MakeValid (geometry Data Type)](../../t-sql/spatial-geometry/makevalid-geometry-data-type.md)  
  
 **To return the number of geometries in a geometry collection instance**  
 [STNumGeometries (geometry Data Type)](../../t-sql/spatial-geometry/stnumgeometries-geometry-data-type.md)  
  
 To return a specific geometry in a geometry collection instance  
 [STGeometryN (geometry Data Type)](../../t-sql/spatial-geometry/stgeometryn-geometry-data-type.md) STGeometryN (geometry Data type)  
  
  
<a id="number"></a>

### Number of points
 All nonempty **geometry** instances are comprised of *points*. These points represent the X- and Y-coordinates of the plane on which the geometries are drawn. **geometry** provides numerous built-in methods for querying the points of an instance.  
  
 **To return the number of points that comprise an instance**  
 [STNumPoints (geometry Data Type)](../../t-sql/spatial-geometry/stnumpoints-geometry-data-type.md)  
  
 **To return a specific point in an instance**  
 [STPointN (geometry Data Type)](../../t-sql/spatial-geometry/stpointn-geometry-data-type.md)  
  
 **To return an arbitrary point that lies on an instance**  
 [STPointOnSurface (geometry Data Type)](../../t-sql/spatial-geometry/stpointonsurface-geometry-data-type.md)  
  
 **To return the start point of an instance**  
 [STStartPoint (geometry Data Type)](../../t-sql/spatial-geometry/ststartpoint-geometry-data-type.md)  
  
 **To return the endpoint of an instance**  
 [STEndpoint (geometry Data Type)](../../t-sql/spatial-geometry/stendpoint-geometry-data-type.md)  
  
 **To return the X-coordinate of a Point instance**  
 [STX (geometry Data Type)](../../t-sql/spatial-geometry/stx-geometry-data-type.md)  
  
 **To return the Y-coordinate of a Point instance**  
 [STY (geometry Data Type)](../../t-sql/spatial-geometry/sty-geometry-data-type.md)  
  
 **To return the geometric center point of a Polygon, CurvePolygon, or MultiPolygon instance**  
 [STCentroid (geometry Data Type)](../../t-sql/spatial-geometry/stcentroid-geometry-data-type.md)  
  
  
<a id="dimension"></a>

### Dimension
 A nonempty **geometry** instance can be 0-, 1-, or 2-dimensional. Zero-dimensional **geometries**, such as **Point** and **MultiPoint**, have no length or area. One-dimensional objects, such as **LineString, CircularString, CompoundCurve**, and **MultiLineString**, have length. Two-dimensional instances, such as **Polygon**, **CurvePolygon**, and **MultiPolygon**, have area and length. Empty instances will report a dimension of -1, and a **GeometryCollection** will report an area dependent on the types of its contents.  
  
 **To return the dimension of an instance**  
 [STDimension (geometry Data Type)](../../t-sql/spatial-geometry/stdimension-geometry-data-type.md)  
  
 **To return the length of an instance**  
 [STLength (geometry Data Type)](../../t-sql/spatial-geometry/stlength-geometry-data-type.md)  
  
 **To return the area of an instance**  
 [STArea (geometry Data Type)](../../t-sql/spatial-geometry/starea-geometry-data-type.md)  
  
  
<a id="empty"></a>

### Empty
 An _empty_**geometry** instance does not have any points. The length of empty **LineString, CircularString**, **CompoundCurve**, and **MultiLineString** instances is zero. The area of empty **Polygon**, **CurvePolygon**, and **MultiPolygon** instances is 0.  
  
 **To determine if an instance is empty**  
 [STIsEmpty (geometry Data Type)](../../t-sql/spatial-geometry/stisempty-geometry-data-type.md).  
  
<a id="simple"></a>

### Simple
 For a **geometry** of the instance to be *simple*, it must meet both of these requirements:  
  
-   Each figure of the instance must not intersect itself, except at its endpoints.  
  
-   No two figures of the instance can intersect each other at a point that is not in both of their boundaries.  
  
> [!NOTE]  
> Empty geometries are always simple.  
  
 **To determine if an instance is simple**  
 [STIsSimple (geometry Data Type)](../../t-sql/spatial-geometry/stissimple-geometry-data-type.md).  
  
  
<a id="boundary"></a>

### Boundary, interior, and exterior

 The *interior* of a **geometry** instance is the space occupied by the instance, and the *exterior* is the space not occupied it.  
  
 *Boundary* is defined by the OGC as follows:  
  
-   **Point** and **MultiPoint** instances do not have a boundary.  
  
-   **LineString** and **MultiLineString** boundaries are formed by the start points and end points, removing those that occur an even number of times.  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::Parse('MULTILINESTRING((0 1, 0 0, 1 0, 0 1), (1 1, 1 0))');  
SELECT @g.STBoundary().ToString();  
```  
  
 The boundary of a **Polygon** or **MultiPolygon** instance is the set of its rings.  
  
```sql  
DECLARE @g geometry;  
SET @g = geometry::Parse('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0), (1 1, 1 2, 2 2, 2 1, 1 1))');  
SELECT @g.STBoundary().ToString();  
```  
  
 **To return the boundary of an instance**  
 [STBoundary (geometry Data Type)](../../t-sql/spatial-geometry/stboundary-geometry-data-type.md)  
   
<a id="envelope"></a>

### Envelope
 The *envelope* of a **geometry** instance, also known as the *bounding box*, is the axis-aligned rectangle formed by the minimum and maximum (X,Y) coordinates of the instance.  
  
 **To return the envelope of an instance**  
 [STEnvelope (geometry Data Type)](../../t-sql/spatial-geometry/stenvelope-geometry-data-type.md)  
  
<a id="closure"></a>

### Closure
 A _closed_**geometry** instance is a figure whose start points and end points are the same. **Polygon** instances are considered closed. **Point** instances are not closed.  
  
 A ring is a simple, closed **LineString** instance.  
  
 **To determine if an instance is closed**  
 [STIsClosed (geometry Data Type)](../../t-sql/spatial-geometry/stisclosed-geometry-data-type.md)  
  
 **To determine if an instance is a ring**  
 [STIsRing (geometry Data Type)](../../t-sql/spatial-geometry/stisring-geometry-data-type.md)  
  
 **To return the exterior ring of a Polygon instance**  
 [STExteriorRing (geometry Data Type)](../../t-sql/spatial-geometry/stexteriorring-geometry-data-type.md)  
  
 **To return the number of interior rings in a Polygon**  
 [STNumInteriorRing (geometry Data Type)](../../t-sql/spatial-geometry/stnuminteriorring-geometry-data-type.md)  
  
 **To return a specified interior ring of a Polygon**  
 [STInteriorRingN (geometry Data Type)](../../t-sql/spatial-geometry/stinteriorringn-geometry-data-type.md)  
  
<a id="srid"></a>

### Spatial reference ID (SRID)
 The spatial reference ID (SRID) is an identifier specifying which coordinate system the **geometry** instance is represented in. Two instances with different SRIDs are incomparable.  
  
 **To set or return the SRID of an instance**  
 [STSrid (geometry Data Type)](../../t-sql/spatial-geometry/stsrid-geometry-data-type.md)  
  
> [!NOTE]
> This property can be modified.  
  
<a id="rel"></a>

## Determine relationships between geometry instances
 The **geometry** data type provides many built-in methods you can use to determine relationships between two **geometry** instances.  
  
 **To determine if two instances comprise the same point set**  
 [STEquals (geometry Data Type)](../../t-sql/spatial-geometry/stequals-geometry-data-type.md)  
  
 **To determine if two instances are disjoint**  
 [STDisjoint (geometry Data Type)](../../t-sql/spatial-geometry/stdisjoint-geometry-data-type.md)  
  
 **To determine if two instances intersect**  
 [STIntersects (geometry Data Type)](../../t-sql/spatial-geometry/stintersects-geometry-data-type.md)  
  
 **To determine if two instances touch**  
 [STTouches (geometry Data Type)](../../t-sql/spatial-geometry/sttouches-geometry-data-type.md)  
  
 **To determine if two instances overlap**  
 [STOverlaps (geometry Data Type)](../../t-sql/spatial-geometry/stoverlaps-geometry-data-type.md)  
  
 **To determine if two instances cross**  
 [STCrosses (geometry Data Type)](../../t-sql/spatial-geometry/stcrosses-geometry-data-type.md)  
  
 **To determine if one instance is within another**  
 [STWithin (geometry Data Type)](../../t-sql/spatial-geometry/stwithin-geometry-data-type.md)  
  
 **To determine if one instance contains another**  
 [STContains (geometry Data Type)](../../t-sql/spatial-geometry/stcontains-geometry-data-type.md)  
  
 **To determine if one instance overlaps another**  
 [STOverlaps (geometry Data Type)](../../t-sql/spatial-geometry/stoverlaps-geometry-data-type.md)  
  
 **To determine if two instances are spatially related**  
 [STRelate (geometry Data Type)](../../t-sql/spatial-geometry/strelate-geometry-data-type.md)  
  
 **To determine the shortest distance between points in two geometries**  
 [STDistance (geometry Data Type)](../../t-sql/spatial-geometry/stdistance-geometry-data-type.md)  
  
<a id="defaultsrid"></a>

## geometry instances Default to zero SRID
 The default SRID for **geometry** instances in [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is `0`. With **geometry** spatial data, the specific SRID of the spatial instance is not required to perform calculations; thus, instances can reside in undefined planar space. To indicate undefined planar space in the calculations of **geometry** data type methods, the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] uses SRID `0`.

## Remarks

**Geometry** and **geography** types cannot be used as table columns in the [!INCLUDE [Fabric SQL analytics endpoint](../../includes/applies-to-version/_fabric-se.md)] or [!INCLUDE [Fabric Data Warehouse](../../includes/applies-to-version/_fabric-dw.md)].

<a id="examples"></a>

## Examples
The following two examples show how to add and query geometry data.  

### Example A.
This example creates a table with an identity column and a `geometry` column `GeomCol1`. A third column renders the `geometry` column into its Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, and uses the `STAsText()` method. Two rows are then inserted: one row contains a `LineString` instance of `geometry`, and one row contains a `Polygon` instance.  
  
```sql  
IF OBJECT_ID ( 'dbo.SpatialTable', 'U' ) IS NOT NULL   
DROP TABLE dbo.SpatialTable;  
GO  

CREATE TABLE SpatialTable   
  ( id int IDENTITY (1,1),  
    GeomCol1 geometry,   
    GeomCol2 AS GeomCol1.STAsText() 
  );  
GO  

INSERT INTO SpatialTable (GeomCol1)  
VALUES (geometry::STGeomFromText('LINESTRING (100 100, 20 180, 180 180)', 0));  

INSERT INTO SpatialTable (GeomCol1)  
VALUES (geometry::STGeomFromText('POLYGON ((0 0, 150 0, 150 150, 0 150, 0 0))', 0));  
GO  
```  
  
### Example B.
This example uses the `STIntersection()` method to return the points where the two previously inserted `geometry` instances intersect.  
  
```sql  
DECLARE @geom1 geometry;  
DECLARE @geom2 geometry;  
DECLARE @result geometry;  

SELECT @geom1 = GeomCol1 FROM SpatialTable WHERE id = 1;  
SELECT @geom2 = GeomCol1 FROM SpatialTable WHERE id = 2;  
SELECT @result = @geom1.STIntersection(@geom2);  
SELECT @result.STAsText();  
```  
  
## Related content

- [Spatial Data](spatial-data-sql-server.md)
