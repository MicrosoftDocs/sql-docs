---
title: "Create, Construct, and Query geometry Instances | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "planar spatial data [SQL Server], getting started"
  - "geometry data type [SQL Server], getting started"
ms.assetid: c6b5c852-37d2-48d0-a8ad-e43bb80d6514
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create, Construct, and Query geometry Instances
  The planar spatial data type, `geometry`, represents data in a Euclidean (flat) coordinate system. This type is implemented as a common language runtime (CLR) data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 The `geometry` type is predefined and available in each database. You can create table columns of type `geometry` and operate on `geometry` data in the same manner as you would use other CLR types.  
  
 The `geometry` data type (planar) supported by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] conforms to the Open Geospatial Consortium (OGC) Simple Features for SQL Specification version 1.1.0.  
  
 For more information on OGC specifications, see the following:  
  
-   [OGC Specifications, Simple Feature Access Part 1 - Common Architecture](https://go.microsoft.com/fwlink/?LinkId=93628)  
  
-   [OGC Specifications, Simple Feature Access Part 2 - SQL Options](https://go.microsoft.com/fwlink/?LinkId=93629)  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a subset of the existing GML 3.1 standard which is defined in the following schema: [https://schemas.microsoft.com/sqlserver/profiles/gml/SpatialGML.xsd](https://go.microsoft.com/fwlink/?LinkId=230959).  
  
##  <a name="creating"></a> Creating or constructing a new geometry instance  
  
###  <a name="existing"></a> Creating a New geometry Instance from an Existing Instance  
 The `geometry` data type provides numerous built-in methods you can use to create new `geometry` instances based on existing instances.  
  
 **To create a buffer around a geometry**  
 [STBuffer &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stbuffer-geometry-data-type)  
  
 [BufferWithTolerance &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/bufferwithtolerance-geometry-data-type)  
  
 **To create a simplified version of a geometry**  
 [Reduce &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/reduce-geometry-data-type)  
  
 **To create the convex hull of a geometry**  
 [STConvexHull &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stconvexhull-geometry-data-type)  
  
 **To create a geometry from the intersection of two geometries**  
 [STIntersection &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stintersection-geometry-data-type)  
  
 **To create a geometry from the union of two geometries**  
 [STUnion &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stunion-geometry-data-type)  
  
 **To create a geometry from the points where one geometry does not overlap another**  
 [STDifference &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stdifference-geometry-data-type)  
  
 **To create a geometry from the points where two geometries do not overlap**  
 [STSymDifference &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stsymdifference-geometry-data-type)  
  
 **To create an arbitrary Point instance that lies on an existing geometry**  
 [STPointOnSurface &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stpointonsurface-geometry-data-type)  
  
  
  
###  <a name="wkt"></a> Constructing a geometry Instance from Well-Known Text Input  
 The `geometry` data type provides several built-in methods that generate a geometry from the Open Geospatial Consortium (OGC) WKT representation. The WKT standard is a text string that allows geometry data to be exchanged in textual form.  
  
 **To construct any type of geometry instance from WKT input**  
 [STGeomFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stgeomfromtext-geometry-data-type)  
  
 [Parse &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/parse-geometry-data-type)  
  
 **To construct a geometry Point instance from WKT input**  
 [STPointFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stpointfromtext-geometry-data-type)  
  
 **To construct a geometry MultiPoint instance from WKT input**  
 [STMPointFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stmpointfromtext-geometry-data-type)  
  
 **To construct a geometry LineString instance from WKT input**  
 [STLineFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stlinefromtext-geometry-data-type)  
  
 **To construct a geometry MultiLineString instance from WKT input**  
 [STMLineFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stmlinefromtext-geometry-data-type)  
  
 **To construct a geometry Polygon instance from WKT input**  
 [STPolyFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stpolyfromtext-geometry-data-type)  
  
 **To construct a geometry MultiPolygon instance from WKT input**  
 [STMPolyFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stmpolyfromtext-geometry-data-type)  
  
 **To construct a geometry GeometryCollection instance from WKT input**  
 [STGeomCollFromText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stgeomcollfromtext-geometry-data-type)  
  
  
  
###  <a name="wkb"></a> Constructing a geometry Instance from Well-Known Binary Input  
 WKB is a binary format specified by the Open Geospatial Consortium (OGC) that permits `geometry` data to be exchanged between a client application and an SQL database. The following functions accept WKB input to construct geometries:  
  
 **To construct any type of geometry instance from WKB input**  
 [STGeomFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stgeomfromwkb-geometry-data-type)  
  
 **To construct a geometry Point instance from WKB input**  
 [STPointFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stpointfromwkb-geometry-data-type)  
  
 **To construct a geometry MultiPoint instance from WKB input**  
 [STMPointFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stmpointfromwkb-geometry-data-type)  
  
 **To construct a geometry LineString instance from WKB input**  
 [STLineFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stlinefromwkb-geometry-data-type)  
  
 **To construct a geometry MultiLineString instance from WKB input**  
 [STMLineFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stmlinefromwkb-geometry-data-type)  
  
 **To construct a geometry Polygon instance from WKB input**  
 [STPolyFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stpolyfromwkb-geometry-data-type)  
  
 **To construct a geometry MultiPolygon instance from WKB input**  
 [STMPolyFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stmpolyfromwkb-geometry-data-type)  
  
 **To construct a geometry GeometryCollection instance from WKB input**  
 [STGeomCollFromWKB &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stgeomcollfromwkb-geometry-data-type)  
  
  
  
###  <a name="gml"></a> Constructing a geometry Instance from GML Text Input  
 The `geometry` data type provides a method that generates a `geometry` instance from GML, an XML representation of geometric objects. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a subset of GML.  
  
 **To construct any type of geometry instance from GML input**  
 [GeomFromGml &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/geomfromgml-geometry-data-type)  
  
  
  
##  <a name="returning"></a> Returning Well-Known Text and Well-Known Binary from a geometry Instance  
 You can use the following methods to return either the WKT or WKB format of a `geometry` instance:  
  
 **To return the WKT representation of a geometry instance**  
 [STAsText &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stastext-geometry-data-type)  
  
 [ToString &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/tostring-geometry-data-type)  
  
 **To return the WKT representation of a geometry instance including any Z and M values**  
 [AsTextZM &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/astextzm-geometry-data-type)  
  
 **To return the WKB representation of a geometry instance**  
 [STAsBinary &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stasbinary-geometry-data-type)  
  
 **To return a GML representation of a geometry instance**  
 [AsGml &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/asgml-geometry-data-type)  
  
  
  
##  <a name="querying"></a> Querying the Properties and Behaviors of geometry Instances  
 All `geometry` instances have a number of properties that can be retrieved through methods that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides. The following topics define the properties and behaviors of geometry types, and the methods for querying each one.  
  
###  <a name="valid"></a> Validity, Instance Type, and GeometryCollection Information  
 Once a `geometry` instance is constructed, you can use the following methods to determine if it is well-formed, return the instance type, or, if it is a collection instance, return a specific `geometry` instance.  
  
 **To return the instance type of a geometry**  
 [STGeometryType &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stgeometrytype-geometry-data-type)  
  
 **To determine if a geometry is a given instance type**  
 [InstanceOf &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/instanceof-geometry-data-type)  
  
 **To determine if a geometry instance is well-formed for its instance type**  
 [STIsValid &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stisvalid-geometry-data-type)  
  
 **To convert a geometry instance to a well-formed geometry instance with an instance type**  
 [MakeValid &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/makevalid-geometry-data-type)  
  
 **To return the number of geometries in a geometry collection instance**  
 [STNumGeometries &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stnumgeometries-geometry-data-type)  
  
 To return a specific geometry in a geometry collection instance  
 [STGeometryN &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stgeometryn-geometry-data-type)STGeometryN (geometry Data type)  
  
  
  
###  <a name="number"></a> Number of Points  
 All nonempty `geometry` instances are comprised of *points*. These points represent the X- and Y-coordinates of the plane on which the geometries are drawn. `geometry` provides numerous built-in methods for querying the points of an instance.  
  
 **To return the number of points that comprise an instance**  
 [STNumPoints &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stnumpoints-geometry-data-type)  
  
 **To return a specific point in an instance**  
 [STPointN](/sql/t-sql/spatial-geometry/stpointn-geometry-data-type)  
  
 **To return an arbitrary point that lies on an instance**  
 [STPointOnSurface](/sql/t-sql/spatial-geometry/stpointonsurface-geometry-data-type)  
  
 **To return the start point of an instance**  
 [STStartPoint](/sql/t-sql/spatial-geometry/ststartpoint-geometry-data-type)  
  
 **To return the end point of an instance**  
 [STEndpoint](/sql/t-sql/spatial-geometry/stendpoint-geometry-data-type)  
  
 **To return the X-coordinate of a Point instance**  
 [STX &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stx-geometry-data-type)  
  
 **To return the Y-coordinate of a Point instance**  
 [STY](/sql/t-sql/spatial-geometry/sty-geometry-data-type)  
  
 **To return the geometric center point of a Polygon, CurvePolygon, or MultiPolygon instance**  
 [STCentroid](/sql/t-sql/spatial-geometry/stcentroid-geometry-data-type)  
  
  
  
###  <a name="dimension"></a> Dimension  
 A nonempty `geometry` instance can be 0-, 1-, or 2-dimensional. Zero-dimensional `geometries`, such as `Point` and `MultiPoint`, have no length or area. One-dimensional objects, such as `LineString, CircularString, CompoundCurve`, and `MultiLineString`, have length. Two-dimensional instances, such as `Polygon`, `CurvePolygon`, and `MultiPolygon`, have area and length. Empty instances will report a dimension of -1, and a `GeometryCollection` will report an area dependent on the types of its contents.  
  
 **To return the dimension of an instance**  
 [STDimension](/sql/t-sql/spatial-geometry/stdimension-geometry-data-type)  
  
 **To return the length of an instance**  
 [STLength](/sql/t-sql/spatial-geometry/stlength-geometry-data-type)  
  
 **To return the area of an instance**  
 [STArea](/sql/t-sql/spatial-geometry/starea-geometry-data-type)  
  
  
  
###  <a name="empty"></a> Empty  
 An *empty*`geometry` instance does not have any points. The length of empty `LineString, CircularString`, `CompoundCurve`, and `MultiLineString` instances is zero. The area of empty `Polygon`, `CurvePolygon`, and `MultiPolygon` instances is 0.  
  
 **To determine if an instance is empty**  
 [STIsEmpty](/sql/t-sql/spatial-geometry/stisempty-geometry-data-type).  
  
  
  
###  <a name="simple"></a> Simple  
 For a `geometry` of the instance to be *simple*, it must meet both of these requirements:  
  
-   Each figure of the instance must not intersect itself, except at its endpoints.  
  
-   No two figures of the instance can intersect each other at a point that is not in both of their boundaries.  
  
> [!NOTE]  
>  Empty geometries are always simple.  
  
 **To determine if an instance is simple**  
 [STIsSimple](/sql/t-sql/spatial-geometry/stissimple-geometry-data-type).  
  
  
  
###  <a name="boundary"></a> Boundary, Interior, and Exterior  
 The *interior* of a `geometry` instance is the space occupied by the instance, and the *exterior* is the space not occupied it.  
  
 *Boundary* is defined by the OGC as follows:  
  
-   `Point` and `MultiPoint` instances do not have a boundary.  
  
-   `LineString` and `MultiLineString` boundaries are formed by the start points and end points, removing those that occur an even number of times.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::Parse('MULTILINESTRING((0 1, 0 0, 1 0, 0 1), (1 1, 1 0))');  
SELECT @g.STBoundary().ToString();  
```  
  
 The boundary of a `Polygon` or `MultiPolygon` instance is the set of its rings.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::Parse('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0), (1 1, 1 2, 2 2, 2 1, 1 1))');  
SELECT @g.STBoundary().ToString();  
```  
  
 **To return the boundary of an instance**  
 [STBoundary](/sql/t-sql/spatial-geometry/stboundary-geometry-data-type)  
  
  
  
###  <a name="envelope"></a> Envelope  
 The *envelope* of a `geometry` instance, also known as the *bounding box*, is the axis-aligned rectangle formed by the minimum and maximum (X,Y) coordinates of the instance.  
  
 **To return the envelope of an instance**  
 [STEnvelope](/sql/t-sql/spatial-geometry/stenvelope-geometry-data-type)  
  
  
  
###  <a name="closure"></a> Closure  
 A *closed*`geometry` instance is a figure whose start points and end points are the same. `Polygon` instances are considered closed. `Point` instances are not closed.  
  
 A ring is a simple, closed `LineString` instance.  
  
 **To determine if an instance is closed**  
 [STIsClosed](/sql/t-sql/spatial-geometry/stisclosed-geometry-data-type)  
  
 **To determine if an instance is a ring**  
 [STIsRing](/sql/t-sql/spatial-geometry/stisring-geometry-data-type)  
  
 **To return the exterior ring of a Polygon instance**  
 [STExteriorRing](/sql/t-sql/spatial-geometry/stexteriorring-geometry-data-type)  
  
 **To return the number of interior rings in a Polygon**  
 [STNumInteriorRing](/sql/t-sql/spatial-geometry/stnuminteriorring-geometry-data-type)  
  
 **To return a specified interior ring of a Polygon**  
 [STInteriorRingN](/sql/t-sql/spatial-geometry/stinteriorringn-geometry-data-type)  
  
  
  
###  <a name="srid"></a> Spatial Reference ID (SRID)  
 The spatial reference ID (SRID) is an identifier specifying which coordinate system the `geometry` instance is represented in. Two instances with different SRIDs are incomparable.  
  
 **To set or return the SRID of an instance**  
 [STSrid](/sql/t-sql/spatial-geometry/stsrid-geometry-data-type)  
  
 This property can be modified.  
  
  
  
##  <a name="rel"></a> Determining Relationships between geometry Instances  
 The `geometry` data type provides many built-in methods you can use to determine relationships between two `geometry` instances.  
  
 **To determine if two instances comprise the same point set**  
 [STEquals](/sql/t-sql/spatial-geometry/stequals-geometry-data-type)  
  
 **To determine if two instances are disjoint**  
 [STDisjoint](/sql/t-sql/spatial-geometry/stdisjoint-geometry-data-type)  
  
 **To determine if two instances intersect**  
 [STIntersects](/sql/t-sql/spatial-geometry/stintersects-geometry-data-type)  
  
 **To determine if two instances touch**  
 [STTouches](/sql/t-sql/spatial-geometry/sttouches-geometry-data-type)  
  
 **To determine if two instances overlap**  
 [STOverlaps](/sql/t-sql/spatial-geometry/stoverlaps-geometry-data-type)  
  
 **To determine if two instances cross**  
 [STCrosses](/sql/t-sql/spatial-geometry/stcrosses-geometry-data-type)  
  
 **To determine if one instance is within another**  
 [STWithin](/sql/t-sql/spatial-geometry/stwithin-geometry-data-type)  
  
 **To determine if one instance contains another**  
 [STContains](/sql/t-sql/spatial-geometry/stcontains-geometry-data-type)  
  
 **To determine if one instance overlaps another**  
 [STOverlaps](/sql/t-sql/spatial-geometry/stoverlaps-geometry-data-type)  
  
 **To determine if two instances are spatially related**  
 [STRelate](/sql/t-sql/spatial-geometry/strelate-geometry-data-type)  
  
 **To determine the shortest distance between points in two geometries**  
 [STDistance](/sql/t-sql/spatial-geometry/stdistance-geometry-data-type)  
  
  
  
##  <a name="defaultsrid"></a> geometry Instances Default to Zero SRID  
 The default SRID for `geometry` instances in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is 0. With `geometry` spatial data, the specific SRID of the spatial instance is not required to perform calculations; thus, instances can reside in undefined planar space. To indicate undefined planar space in the calculations of `geometry` data type methods, the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] uses SRID 0.  
  
##  <a name="examples"></a> Examples  
 The following two examples show how to add and query geometry data.  
  
-   The first example creates a table with an identity column and a `geometry` column `GeomCol1`. A third column renders the `geometry` column into its Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, and uses the `STAsText()` method. Two rows are then inserted: one row contains a `LineString` instance of `geometry`, and one row contains a `Polygon` instance.  
  
    ```  
    IF OBJECT_ID ( 'dbo.SpatialTable', 'U' ) IS NOT NULL   
        DROP TABLE dbo.SpatialTable;  
    GO  
  
    CREATE TABLE SpatialTable   
        ( id int IDENTITY (1,1),  
        GeomCol1 geometry,   
        GeomCol2 AS GeomCol1.STAsText() );  
    GO  
  
    INSERT INTO SpatialTable (GeomCol1)  
    VALUES (geometry::STGeomFromText('LINESTRING (100 100, 20 180, 180 180)', 0));  
  
    INSERT INTO SpatialTable (GeomCol1)  
    VALUES (geometry::STGeomFromText('POLYGON ((0 0, 150 0, 150 150, 0 150, 0 0))', 0));  
    GO  
    ```  
  
-   The second example uses the `STIntersection()` method to return the points where the two previously inserted `geometry` instances intersect.  
  
    ```  
    DECLARE @geom1 geometry;  
    DECLARE @geom2 geometry;  
    DECLARE @result geometry;  
  
    SELECT @geom1 = GeomCol1 FROM SpatialTable WHERE id = 1;  
    SELECT @geom2 = GeomCol1 FROM SpatialTable WHERE id = 2;  
    SELECT @result = @geom1.STIntersection(@geom2);  
    SELECT @result.STAsText();  
    ```  
  
  
  
## See Also  
 [Spatial Data &#40;SQL Server&#41;](spatial-data-sql-server.md)  
  
  
