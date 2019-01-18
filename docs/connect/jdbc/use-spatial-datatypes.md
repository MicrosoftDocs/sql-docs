---
title: "Using Spatial Datatypes | Microsoft Docs"
ms.custom: ""
ms.date: "07/30/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Spatial Datatypes

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Spatial datatypes (Geometry and Geography) are supported starting JDBC Driver preview release 6.5.0. Spatial datatypes are currently not supported with stored procedures, Table Valued Parameters (TVP), BulkCopy, and Always Encrypted. This page shows various use cases of Geometry and Geography data types with the JDBC Driver. For an overview on spatial datatypes, check [Spatial Data Types Overview](https://docs.microsoft.com/sql/relational-databases/spatial/spatial-data-types-overview) page.

## Creating a Geometry / Geography object

There are two main ways to create a Geometry / Geography object - either convert from a Well-Known Text (WKT) or a Well-Known Binary (WKB).

### Creating from WKT

```java
String geoWKT = "LINESTRING(1 0, 0 1, -1 0)";
Geometry geomWKT = Geometry.STGeomFromText(geoWKT, 0);
Geography geogWKT = Geography.STGeomFromText(geoWKT, 4326);
```

This will create a LINESTRING Geometry object with Spatial Reference System Identifier (SRID) 0, and a Geography object with SRID 4326.

### Creating from WKB

```java
byte[] geomWKB = Hex.decodeHex("00000000010403000000000000000000F03F00000000000000000000000000000000000000000000F03F000000000000F0BF000000000000000001000000010000000001000000FFFFFFFF0000000002".toCharArray());
byte[] geogWKB = Hex.decodeHex("E61000000104030000000000000000000000000000000000F03F000000000000F03F00000000000000000000000000000000000000000000F0BF01000000010000000001000000FFFFFFFF0000000002".toCharArray());

Geometry geomWKT = Geometry.deserialize(geomWKB);
Geography geogWKT = Geography.deserialize(geogWKB);
```

This will create a Geometry and Geography object that is equivalent to the ones created from the WKT previously.

## Working with a Geometry / Geography object

Assuming the user has a table on SQL Server like below:

```sql
CREATE TABLE sampleTable (c1 geometry)  
```

A sample script to insert a Geometry value would be:

```java
String geoWKT = "LINESTRING(1 0, 0 1, -1 0)";
Geometry geomWKT = Geometry.STGeomFromText(geoWKT, 0);
SQLServerPreparedStatement pstmt = (SQLServerPreparedStatement) connection.prepareStatement("insert into sampleTable values (?)");
pstmt.setGeometry(1, geomWKT);  
pstmt.execute();
```

The same can be done for the Geography counterpart, using a Geography column and **setGeography()** method.

To read a Geometry / Geography column:

```java
try(SQLServerResultSet rs = (SQLServerResultSet)stmt.executeQuery("select * from geomTable")) {
    while(rs.next()){
        rs.getGeometry(1);
    }
}
```

The same can be done for the Geography counterpart, using a Geography column and **getGeography()** method.

## Newly introduced APIs

These are the new public APIs that have been introduced with this addition, in the classes **SQLServerPreparedStatement**, **SQLServerResultSet**, **Geometry**, and **Geography**.

### SQLServerPreparedStatement

|Method|Description|
|:------|:----------|
|void setGeometry(int n, Geometry x)| Sets the designated parameter to the given microsoft.sql.Geometry Class object.
|void setGeography(int n, Geography x)| Sets the designated parameter to the given microsoft.sql.Geography Class object.

### SQLServerResultSet

|Method|Description|
|:------|:----------|
|Geometry getGeometry(int colunIndex)| Returns the value of the designated column in the current row of this ResultSet object as a com.microsoft.sqlserver.jdbc.Geometry object in the Java programming language.
|Geometry getGeometry(String columnName)| Returns the value of the designated column in the current row of this ResultSet object as a com.microsoft.sqlserver.jdbc.Geometry object in the Java programming language.
|Geography getGeography(int colunIndex)| Returns the value of the designated column in the current row of this ResultSet object as a com.microsoft.sqlserver.jdbc.Geography object in the Java programming language.
|Geography getGeography(String columnName)| Returns the value of the designated column in the current row of this ResultSet object as a com.microsoft.sqlserver.jdbc.Geography object in the Java programming language.

### Geometry

|Method|Description|
|:------|:----------|
|Geometry STGeomFromText(String wkt, int SRID)| Constructor for a Geometry instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
|Geometry STGeomFromWKB(byte[] wkb)| Constructor for a Geometry instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
|Geometries deserialize(byte[] wkb)| Constructor for a Geometry instance from an internal SQL Server format for spatial data.
|Geometry parse(String wkt)| Constructor for a Geometry instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation. Spatial Reference Identifier is defaulted to 0.
|Geometry point(double x, double y, int SRID)| Constructor for a Geometry instance that represents a Point instance from its X and Y values and a Spatial Reference Identifier.
|String STAsText()| Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a Geometry instance. This text will not contain any Z (elevation) or M (measure) values carried by the instance.
|byte[] STAsBinary()| Returns the Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation of a Geometry instance. This value will not contain any Z or M values carried by the instance.
|byte[] serialize()| Returns the bytes that represent an internal SQL Server format of Geometry type.
|boolean hasM()| Returns if the object contains an M (measure) value.
|boolean hasZ()| Returns if the object contains a Z (elevation) value.
|Double getX()| Returns the X coordinate value.
|Double getY()| Returns the Y coordinate value.
|Double getM()| Returns the M (measure) value of the object.
|Double getZ()| Returns the Z (elevation) value of the object.
|int getSrid()| Returns the Spatial Reference Identifier (SRID) value.
|boolean isNull()| Returns if the Geometry object is null.
|int STNumPoints()| Returns the number of points in the Geometry object.
|String STGeometryType()| Returns the Open Geospatial Consortium (OGC) type name represented by a geometry instance.
|String asTextZM()| Returns the Well-Known Text (WKT) representation of the Geometry object.
|String toString()| Returns the String representation of the Geometry object.

### Geography

|Method|Description|
|:------|:----------|
|Geography STGeomFromText(String wkt, int SRID)| Constructor for a Geography instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
|Geography STGeomFromWKB(byte[] wkb)| Constructor for a Geography instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation.
|Geography deserialize(byte[] wkb)| Constructor for a Geography instance from an internal SQL Server format for spatial data.
|Geography parse(String wkt)| Constructor for a Geography instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation. Spatial Reference Identifier is defaulted to 0.
|Geography point(double lat, double lon, int SRID)| Constructor for a Geography instance that represents a Point instance from its latitude and longitude values and a Spatial Reference Identifier.
|String STAsText()| Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a Geography instance. This text will not contain any Z (elevation) or M (measure) values carried by the instance.
|byte[] STAsBinary())| Returns the Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation of a Geography instance. This value will not contain any Z or M values carried by the instance.
|byte[] serialize()| Returns the bytes that represent an internal SQL Server format of Geography type.
|boolean hasM()| Returns if the object contains an M (measure) value.
|boolean hasZ()| Returns if the object contains a Z (elevation) value.
|Double getLatitude()| Returns the latitude value.
|Double getLongitude()| Returns the longitude value.
|Double getM()| Returns the M (measure) value of the object.
|Double getZ()| Returns the Z (elevation) value of the object.
|int getSrid()| Returns the Spatial Reference Identifier (SRID) value.
|boolean isNull()| Returns if the Geography object is null.
|int STNumPoints()| Returns the number of points in the Geography object.
|String STGeographyType()| Returns the Open Geospatial Consortium (OGC) type name represented by a geography instance.
|String asTextZM()| Returns the Well-Known Text (WKT) representation of the Geography object.
|String toString()| Returns the String representation of the Geography object.

## Limitations of Spatial Datatypes

1. The spatial sub-datatypes **CircularString**, **CompoundCurve**, **CurvePolygon**, and **FullGlobe** are only supported starting from SQL Server 2012 and above.

2. Always Encrypted cannot be used with spatial datatypes.

3. Stored procedures, TVP, and BulkCopy operations are currently not supported with spatial datatypes.

## See Also

[Spatial Data Types Sample (JDBC)](../../connect/jdbc/spatial-data-types-sample.md)
