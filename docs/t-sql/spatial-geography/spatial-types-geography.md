---
description: "Spatial Types - geography"
title: "geography (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "geography"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "geography data type [SQL Server], Transact-SQL"
  - "spatial data types [SQL Server]"
ms.assetid: d9e4952a-1841-4465-a64b-11e9288dba1d
author: MladjoA
ms.author: mlandzic 
---
# Spatial Types - geography
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  The geography spatial data type, **geography**, is implemented as a .NET common language runtime (CLR) data type in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. This type represents data in a round-earth coordinate system. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **geography** data type stores ellipsoidal (round-earth) data, such as GPS latitude and longitude coordinates.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports a set of methods for the **geography** spatial data type. This includes methods on **geography** that are defined by the Open Geospatial Consortium (OGC) standard and a set of [!INCLUDE[msCoName](../../includes/msconame-md.md)] extensions to that standard.  
 
 The error tolerance for the **geography** methods can be as large as 1.0e-7 * extents. The extents refer to the approximate maximal distance between points of the **geography**object.
  

## Registering the geography Type  
 The **geography** type is predefined and available in each database. You can create table columns of type **geography** and operate on **geography** data in the same manner as you would use other system-supplied types. Can be used in persisted and non-persisted computed columns.  
  
## Examples  
  
### A. Showing how to add and query geography data  
 The following examples show how to add and query geography data. The first example creates a table with an identity column and a `geography` column, `GeogCol1`. A third column renders the `geography` column into its Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation, and uses the `STAsText()` method. Two rows are then inserted: one row contains a `LineString` instance of `geography`, and one row contains a `Polygon` instance.  
  
```sql  
IF OBJECT_ID ( 'dbo.SpatialTable', 'U' ) IS NOT NULL   
    DROP TABLE dbo.SpatialTable;  
GO  
  
CREATE TABLE SpatialTable   
    ( id int IDENTITY (1,1),  
    GeogCol1 geography,   
    GeogCol2 AS GeogCol1.STAsText() );  
GO  
  
INSERT INTO SpatialTable (GeogCol1)  
VALUES (geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656 )', 4326));  
  
INSERT INTO SpatialTable (GeogCol1)  
VALUES (geography::STGeomFromText('POLYGON((-122.358 47.653 , -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326));  
GO  
```  
  
### B. Returning the intersection of two geography instances  
 The following example uses the `STIntersection()` method to return the points where the two previously inserted `geography` instances intersect.  
  
```sql  
DECLARE @geog1 geography;  
DECLARE @geog2 geography;  
DECLARE @result geography;  
  
SELECT @geog1 = GeogCol1 FROM SpatialTable WHERE id = 1;  
SELECT @geog2 = GeogCol1 FROM SpatialTable WHERE id = 2;  
SELECT @result = @geog1.STIntersection(@geog2);  
SELECT @result.STAsText();  
```  
  
### C. Using geography in a computed column  
 The following example creates a table with a persisted computed column using a **geography** type.  
  
```sql  
IF OBJECT_ID ( 'dbo.SpatialTable', 'U' ) IS NOT NULL   
    DROP TABLE dbo.SpatialTable;  
GO  
  
CREATE TABLE SpatialTable  
(  
    locationId int IDENTITY(1,1),  
    location geography,  
    deliveryArea as location.STBuffer(10) persisted  
);  
```  
  
## See Also  
 [Spatial Data &#40;SQL Server&#41;](../../relational-databases/spatial/spatial-data-sql-server.md)   

  
  
