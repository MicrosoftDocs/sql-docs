---
title: "Create, Construct, and Query geography Instances | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
ms.topic: conceptual
helpviewer_keywords: 
  - "geography data type [SQL Server]"
  - "geodetic data type [SQL Server]"
  - "geography data type [SQL Server], about geography data type"
ms.assetid: b585851e-d15b-411f-adeb-aeabeb777c0b
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Create, Construct, and Query geography Instances
  The geography spatial data type, `geography`, represents data in a round-earth coordinate system. This type is implemented as a .NET common language runtime (CLR) data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] `geography` data type stores ellipsoidal (round-earth) data, such as GPS latitude and longitude coordinates.  
  
 The `geography` type is predefined and available in each database. You can create table columns of type `geography` and operate on `geography` data in the same manner as you would use other system-supplied types.  
  
##  <a name="creating"></a> Creating or constructing a new geography instance  
  
###  <a name="existing"></a> Creating a New geography Instance from an Existing Instance  
 The `geography` data type provides numerous built-in methods you can use to create new `geography` instances based on existing instances.  
  
 **To create a buffer around a geography**  
 [STBuffer &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stbuffer-geography-data-type)  
  
 **To create a buffer around a geography, allowing for a tolerance**  
 [BufferWithTolerance &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/bufferwithtolerance-geography-data-type)  
  
 **To create a geography from the intersection of two geography instances**  
 [STIntersection &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stintersection-geography-data-type)  
  
 **To create a geography from the union of two geography instances**  
 [STUnion &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stunion-geography-data-type)  
  
 **To create a geography from the points where one geography does not overlap another**  
 [STDifference &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stdifference-geography-data-type)  
  
###  <a name="wkt"></a> Constructing a geography Instance from Well-Known Text Input  
 The `geography` data type provides several built-in methods that generate a geography from the Open Geospatial Consortium (OGC) WKT representation. The WKT standard is a text string that allows geography data to be exchanged in textual form.  
  
 **To construct any type of geography instance from WKT input**  
 [STGeomFromText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stgeomfromtext-geography-data-type)  
  
 [Parse &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/parse-geography-data-type)  
  
 **To construct a geography Point instance from WKT input**  
 [STPointFromText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stpointfromtext-geography-data-type)  
  
 **To construct a geography MultiPoint instance from WKT input**  
 [STMPointFromText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stmpointfromtext-geography-data-type)  
  
 **To construct a geography LineString instance from WKT input**  
 STLineFromText (geography Data Type)  
  
 **To construct a geography MultiLineString instance from WKT input**  
 [STMLineFromText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stmlinefromtext-geography-data-type)  
  
 **To construct a geography Polygon instance from WKT input**  
 [STPolyFromText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stpolyfromtext-geography-data-type)  
  
 **To construct a geography MultiPolygon instance from WKT input**  
 [STMPolyFromText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stmpolyfromtext-geography-data-type)  
  
 **To construct a geography GeometryCollection instance from WKT input**  
 [STGeomCollFromText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stgeomcollfromtext-geography-data-type)  
  
###  <a name="wkb"></a> Constructing a geography Instance from Well-Known Binary Input  
 WKB is a binary format specified by the OGC that permits `Geography` data to be exchanged between a client application and an SQL database. The following functions accept WKB input to construct geography instances:  
  
 **To construct any type of geography instance from WKB input**  
 [STGeomFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stgeomfromwkb-geography-data-type)  
  
 **To construct a geography Point instance from WKB input**  
 [STPointFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stpointfromwkb-geography-data-type)  
  
 **To construct a geography MultiPoint instance from WKB input**  
 [STMPointFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stmpointfromwkb-geography-data-type)  
  
 **To construct a geography LineString instance from WKB input**  
 [STLineFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stlinefromwkb-geography-data-type)  
  
 **To construct a geography MultiLineString instance from WKB input**  
 [STMLineFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stmlinefromwkb-geography-data-type)  
  
 **To construct a geography Polygon instance from WKB input**  
 [STPolyFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stpolyfromwkb-geography-data-type)  
  
 **To construct a geography MultiPolygon instance from WKB input**  
 [STMPolyFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stmpolyfromwkb-geography-data-type)  
  
 **To construct a geography GeometryCollection instance from WKB input**  
 [STGeomCollFromWKB &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stgeomcollfromwkb-geography-data-type)STGeomCollFromWKB (geography Data Type)  
  
###  <a name="gml"></a> Constructing a geography Instance from GML Text Input  
 The `geography` data type provides a method that generates a `geography` instance from GML, an XML representation of a `geography` instance. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a subset of GML.  
  
 For more information on Geography Markup Language, see the OGC Specification: [OGC Specifications, Geography Markup Language.](https://go.microsoft.com/fwlink/?LinkId=93629)  
  
 **To construct any type of geography instance from GML input**  
 [GeomFromGML &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/geomfromgml-geography-data-type)  
  
##  <a name="returning"></a> Returning Well-Known Text and Well-Known Binary from a geography Instance  
 You can use the following methods to return either the WKT or WKB format of a `geography` instance:  
  
 **To return the WKT representation of a geography instance**  
 [STAsText &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stastext-geography-data-type)  
  
 [ToString &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/tostring-geography-data-type)  
  
 **To return the WKT representation of a geography instance including any Z and M values**  
 [AsTextZM &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/astextzm-geography-data-type)  
  
 **To return the WKB representation of a geography instance**  
 [STAsBinary &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stasbinary-geography-data-type)  
  
 **To return a GML representation of a geography instance**  
 [AsGml &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/asgml-geography-data-type)  
  
##  <a name="query"></a> Querying the Properties and Behaviors of geography Instances  
 All `geography` instances have a number of properties that can be retrieved through methods that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] provides. The following topics define the properties and behaviors of geography types, and the methods for querying each one.  
  
###  <a name="valid"></a> Validity, Instance Type, and GeometryCollection Information  
 After a `geography` instance is constructed, you can use the following methods to return the instance type, or if it is a `GeometryCollection` instance, return a specific `geography` instance.  
  
 **To return the instance type of a geography**  
 [STGeometryType &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stgeometrytype-geography-data-type)  
  
 **To determine if a geography is a given instance type**  
 [InstanceOf &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/instanceof-geography-data-type)  
  
 **To determine if a geography instance is well-formed for its instance type**  
 [STNumGeometries &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stnumgeometries-geography-data-type)  
  
 **To return a specific geography in a GeometryCollection instance**  
 [STGeometryN &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stgeometryn-geography-data-type)STGeometryN (geography Data Type)  
  
###  <a name="number"></a> Number of Points  
 All nonempty `geography` instances are comprised of *points*. These points represent the latitude and longitude coordinates of the earth on which the `geography` instances are drawn. The data type `geography` provides numerous built-in methods for querying the points of an instance.  
  
 **To return the number of points that comprise an instance**  
 [STNumPoints &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stnumpoints-geography-data-type)  
  
 **To return a specific point in an instance**  
 [STPointN &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stpointn-geometry-data-type)  
  
 **To return the start point of an instance**  
 [STStartPoint &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/ststartpoint-geography-data-type)  
  
 **To return the end point of an instance**  
 [STEndpoint &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stendpoint-geography-data-type)  
  
###  <a name="dimension"></a> Dimension  
 A nonempty `geography` instance can be 0-, 1-, or 2-dimensional. Zero-dimensional `geography` instances, such as `Point` and `MultiPoint`, have no length or area. One-dimensional objects, such as `LineString, CircularString`, `CompoundCurve`, and `MultiLineString`, have length. Two-dimensional instances, such as `Polygon, CurvePolygon`, and `MultiPolygon`, have area and length. Empty instances report a dimension of -1, and a `GeometryCollection` reports the maximum dimension of its contents.  
  
 **To return the dimension of an instance**  
 [STDimension &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stdimension-geography-data-type)  
  
 **To return the length of an instance**  
 [STLength &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stlength-geography-data-type)  
  
 **To return the area of an instance**  
 [STArea &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/starea-geography-data-type)  
  
###  <a name="empty"></a> Empty  
 An *empty*`geography` instance does not have any points. The length of empty `LineString, CircularString`, `CompoundCurve`, and `MultiLineString` instances is 0. The area of empty `Polygon, CurvePolygon` and `MultiPolygon` instances is 0.  
  
 **To determine if an instance is empty**  
 [STIsEmpty &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stisempty-geography-data-type)  
  
###  <a name="closure"></a> Closure  
 A *closed*`geography` instance is a figure whose start points and end points are the same. `Polygon` instances are considered closed. `Point` instances are not closed.  
  
 A ring is a simple, closed `LineString` instance.  
  
 **To determine if an instance is closed**  
 [STIsClosed &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stisclosed-geography-data-type)  
  
 **To return the number of rings in a Polygon instance**  
 [NumRings &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/numrings-geography-data-type)  
  
 **To return a specified ring of a geography instance**  
 [RingN &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/ringn-geography-data-type)  
  
###  <a name="srid"></a> Spatial Reference ID (SRID)  
 The spatial reference ID (SRID) is an identifier specifying which ellipsoidal coordinate system the `geography` instance is represented in. Two `geography` instances with different SRIDs cannot be compared.  
  
 **To set or return the SRID of an instance**  
 [STSrid &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stsrid-geography-data-type)  
  
 This property can be modified.  
  
##  <a name="rel"></a> Determining Relationships between geography Instances  
 The `geography` data type provides many built-in methods you can use to determine relationships between two `geography` instances.  
  
 **To determine if two instances comprise the same point set**  
 [STEquals &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stequals-geometry-data-type)  
  
 **To determine if two instances are disjoint**  
 [STDisjoint &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stdisjoint-geometry-data-type)  
  
 **To determine if two instances intersect**  
 [STIntersects &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stintersects-geometry-data-type)  
  
 **To determine the point or points where two instances intersect**  
 [STIntersection &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stintersection-geography-data-type)  
  
 **To determine the shortest distance between points in two geography instances**  
 [STDistance &#40;geometry Data Type&#41;](/sql/t-sql/spatial-geometry/stdistance-geometry-data-type)  
  
 **To determine the difference in points between two geography instances**  
 [STDifference &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stdifference-geography-data-type)  
  
 **To derive the symmetric difference, or unique points, of one geography instance compared with another instance**  
 [STSymDifference &#40;geography Data Type&#41;](/sql/t-sql/spatial-geography/stsymdifference-geography-data-type)  
  
##  <a name="supportedsrid"></a> geography Instances Must Use Supported SRID  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports SRIDs based on the EPSG standards. A [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]-supported SRID for `geography` instances must be used when performing calculations or using methods with geography spatial data. The SRID must match one of the SRIDs displayed in the **sys.spatial_reference_systems** catalog view. As mentioned previously, when you perform calculations on your spatial data using the `geography` data type, your results will depend on which ellipsoid was used in the creation of your data, as each ellipsoid is assigned a specific spatial reference identifier (SRID).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the default SRID of 4326, which maps to the WGS 84 spatial reference system, when using methods on `geography` instances. If you use data from a spatial reference system other than WGS 84 (or SRID 4326), you will need to determine the specific SRID for your geography spatial data.  
  
##  <a name="examples"></a> Examples  
 The following examples show how to add and query geography data.  
  
-   The first example creates a table with an identity column and a `geography` column `GeogCol1`. A third column renders the `geography` column into its Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, and uses the `STAsText()` method. Two rows are then inserted: one row contains a `LineString` instance of `geography`, and one row contains a `Polygon` instance.  
  
    ```  
    IF OBJECT_ID ( 'dbo.SpatialTable', 'U' ) IS NOT NULL   
        DROP TABLE dbo.SpatialTable;  
    GO  
  
    CREATE TABLE SpatialTable   
        ( id int IDENTITY (1,1),  
        GeogCol1 geography,   
        GeogCol2 AS GeogCol1.STAsText() );  
    GO  
  
    INSERT INTO SpatialTable (GeogCol1)  
    VALUES (geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326));  
  
    INSERT INTO SpatialTable (GeogCol1)  
    VALUES (geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326));  
    GO  
    ```  
  
-   The second example uses the `STIntersection()` method to return the points where the two previously inserted `geography` instances intersect.  
  
    ```  
    DECLARE @geog1 geography;  
    DECLARE @geog2 geography;  
    DECLARE @result geography;  
  
    SELECT @geog1 = GeogCol1 FROM SpatialTable WHERE id = 1;  
    SELECT @geog2 = GeogCol1 FROM SpatialTable WHERE id = 2;  
    SELECT @result = @geog1.STIntersection(@geog2);  
    SELECT @result.STAsText();  
    ```  
  
## See Also  
 [Spatial Data &#40;SQL Server&#41;](spatial-data-sql-server.md)  
  
  
