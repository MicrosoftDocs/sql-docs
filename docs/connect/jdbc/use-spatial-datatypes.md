---
title: Learn how to use spatial data types with the Microsoft JDBC Driver for SQL Server.
description: Using spatial data types
author: David-Engel
ms.author: v-davidengel
ms.date: 07/31/2020
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Using spatial data types

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Spatial data types (Geometry and Geography) are supported starting JDBC Driver preview release 6.5.0. Spatial data types are currently not supported with stored procedures, Table Valued Parameters (TVP), BulkCopy, and Always Encrypted. This page shows various use cases of Geometry and Geography data types with the JDBC Driver. For an overview on spatial data types, check [Spatial Data Types Overview](../../relational-databases/spatial/spatial-data-types-overview.md) page.

## Creating a geometry / geography object

There are two main ways to create a Geometry / Geography object - either convert from a Well-Known Text (WKT) or an internal SQL Server format (CLR).

### Creating from WKT

```java
String geoWKT = "LINESTRING(1 0, 0 1, -1 0)";
Geometry geomWKT = Geometry.STGeomFromText(geoWKT, 0);
Geography geogWKT = Geography.STGeomFromText(geoWKT, 4326);
```

This code will create a LINESTRING Geometry object with Spatial Reference System Identifier (SRID) 0, and a Geography object with SRID 4326.

### Creating from CLR

```java
byte[] geomCLR = Hex.decodeHex("00000000010403000000000000000000F03F00000000000000000000000000000000000000000000F03F000000000000F0BF000000000000000001000000010000000001000000FFFFFFFF0000000002".toCharArray());
byte[] geogCLR = Hex.decodeHex("E61000000104030000000000000000000000000000000000F03F000000000000F03F00000000000000000000000000000000000000000000F0BF01000000010000000001000000FFFFFFFF0000000002".toCharArray());

Geometry geomWKT = Geometry.deserialize(geomCLR);
Geography geogWKT = Geography.deserialize(geogCLR);
```

This code will create a Geometry and Geography object that is equivalent to the ones created from the WKT previously.

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

These methods are the new public APIs that have been introduced with this addition, in the classes **SQLServerPreparedStatement**, **SQLServerResultSet**, **Geometry**, and **Geography**.

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
|Geometry STGeomFromText(String `wkt`, int `SRID`)| Constructor for a Geometry instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation augmented with any Z (elevation) and M (measure) values carried by the instance.
|Geometry STGeomFromWKB(byte[] `wkb`)| Constructor for a Geometry instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation. Note: This method currently uses internal SQL Server format (CLR) to create a Geometry instance, which is a known issue in the driver and is planned to be changed to accept WKB data instead. For existing users who are already using this method, consider switching to deserialize(byte) instead.
|Geometries deserialize(byte[] clr)| Constructor for a Geometry instance from an internal SQL Server format for spatial data.
|Geometry parse(String wkt)| Constructor for a Geometry instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation. Spatial Reference Identifier is defaulted to 0.
|Geometry point(double x, double y, int SRID)| Constructor for a Geometry instance that represents a Point instance from its X and Y values and a Spatial Reference Identifier.
|String STAsText()| Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a Geometry instance. This text won't contain any Z (elevation) or M (measure) values carried by the instance.
|byte[] STAsBinary()| Returns the internal SQL Server format (CLR) representation of a Geometry instance. This value won't contain any Z or M values carried by the instance.
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
|Geography STGeomFromWKB(byte[] `wkb`)| Constructor for a Geography instance from an Open Geospatial Consortium (OGC) Well-Known Binary (WKB) representation. Note: This method currently uses internal SQL Server format (CLR) to create a Geometry instance, but in the future this method will be changed to accept WKB data instead, as the SQL Server counterpart of this method (STGeomFromWKB) uses WKB. For existing users who are already using this method, consider switching to deserialize(byte) instead.
|Geography deserialize(byte[] clr)| Constructor for a Geography instance from an internal SQL Server format for spatial data.
|Geography parse(String wkt)| Constructor for a Geography instance from an Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation. Spatial Reference Identifier is defaulted to 0.
|Geography point(double lon, double lat, int SRID)| Constructor for a Geography instance that represents a Point instance from its longitude and latitude and a Spatial Reference Identifier.
|String STAsText()| Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a Geography instance. This text won't contain any Z (elevation) or M (measure) values carried by the instance.
|byte[] STAsBinary())| Returns the internal SQL Server format (CLR) representation of a Geography instance. This value won't contain any Z or M values carried by the instance.
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

## Limitations of spatial data types

1. The spatial sub data types **CircularString**, **CompoundCurve**, **CurvePolygon**, and **FullGlobe** are only supported starting from SQL Server 2012 and above.

2. Always Encrypted can't be used with spatial data types.

3. Stored procedures, TVP, and BulkCopy operations are currently not supported with spatial data types.

## See also

[Spatial data types sample (JDBC)](../../connect/jdbc/spatial-data-types-sample.md)
