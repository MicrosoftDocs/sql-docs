---
title: "Using Spatial Datatypes | Microsoft Docs"
ms.custom: ""
ms.date: "07/19/2018"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.suite: "sql"
ms.technology: connectivity
ms.tgt_pltfrm: ""
ms.topic: conceptual
ms.assetid: 
caps.latest.revision: 1
author: MightyPen
ms.author: genemi
manager: craigg
---
# Using Spatial Datatypes

[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

Spatial datatypes (Geometry and Geography) are supported starting JDBC Driver preview release 6.5.0. Spatial datatypes are currently not supported with stored procedures, Table Valued Parameters (TVP), BulkCopy, and Always Encrypted. This page shows various use cases of Geometry and Geography data types with the JDBC Driver. For an overview on spatial datatypes, check [Spatial Data Types Overview](https://docs.microsoft.com/en-us/sql/relational-databases/spatial/spatial-data-types-overview) page.

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
try(SQLServerPreparedStatement pstmt = (SQLServerPreparedStatement)con.prepareStatement("insert into geomTable values (?)")) {
    pstmt.setGeometry(1, geomWKT);  
    pstmt.execute();
}
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

## Limitations of Spatial Datatypes

1. The spatial sub-datatypes **CircularString**, **CompoundCurve**, **CurvePolygon**, and **FullGlobe** are only supported starting from SQL Server 2012 and above.

2. Always Encrypted cannot be used with spatial datatypes.

3. Stored procedures, TVP and BulkCopy operations are currently not supported with spatial datatypes.

## See Also

[Working with Data Types (JDBC)](../../connect/jdbc/code-samples/working-with-data-types-jdbc.md)
