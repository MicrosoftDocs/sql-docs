---
description: "MakeValid (geography Data Type)"
title: "MakeValid (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "MakeValid method (geography)"
ms.assetid: f67038e3-4f62-4465-994e-e95ac27d8ada
author: MladjoA
ms.author: mlandzic 
---
# MakeValid (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Converts a **geography** instance that is not valid into a valid **geography** instance with a valid Open Geospatial Consortium (OGC) type.  
  
 If an input object returns False for STIsValid(), `MakeValid()` converts the instance that is not valid to a valid instance.  
  
 This geography data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.MakeValid ()  
```  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 This method may change the type of the **geography** instance. Additionally, the points of a **geography** instance may shift slightly. Results from some methods such as NumPoint() may change.  
  
 In cases where the invalid spatial instance intersects the equator and has an EnvelopeAngle() = 180, a **FullGlobe** instance will be returned. The `MakeValid()`**geography** data type method will make the best attempt at returning valid instances but the results are not guaranteed to be accurate or precise.  
  
> [!NOTE]  
>  Objects that are not valid can be stored in the database. The methods that can be executed on invalid instances (those instances for which STIsValid() return False) are methods that check validity or allow for export: STIsValid(), MakeValid(), STAsText(), STAsBinary(), ToString(), AsTextZM(), and AsGml().  
  
 This method is not precise.  
  
## Examples  
 The first example creates an invalid `LineString` instance that overlaps itself and uses `STIsValid()` to confirm that it is an invalid instance. `STIsValid()` returns the value of 0 for an invalid instance.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(0 2, 1 1, 1 0, 1 1, 2 2)', 4326);  
SELECT @g.STIsValid();  
```  
  
 The second example uses `MakeValid()` to make the instance valid and to test that the instance is indeed valid. `STIsValid()` returns the value of 1 for a valid instance.  
  
```sql
SET @g = @g.MakeValid();  
SELECT @g.STIsValid();  
```  
  
 The third example verifies how the instance has been changed to make it a valid instance.  
  
```sql
SELECT @g.ToString();  
```  
  
 In this example, when the `LineString` instance is selected, the values are returned as a valid `MultiLineString` instance.  
  
```sql
MULTILINESTRING ((0 2, 1 1, 2 2), (1 1, 1 0))  
```  
  
## See Also  
 [STIsValid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stisvalid-geometry-data-type.md)   
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
