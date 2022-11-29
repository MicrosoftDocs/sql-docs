---
description: "STEndPoint (geography Data Type)"
title: "STEndPoint (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STEndPoint (geography Data Type)"
  - "STEndPoint_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STEndPoint method"
ms.assetid: 8974cd07-8ec4-4126-8fc2-fdcf322ccedd
author: MladjoA
ms.author: mlandzic 
---
# STEndPoint (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns the end point of a **geography** instance.  
  
## Syntax  
  
```  
  
.STEndPoint ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
 Open Geospatial Consortium (OGC) type: **Point**  
  
## Remarks  
 STEndPoint() is the equivalent of [STPointN](../../t-sql/spatial-geography/stpointn-geography-data-type.md)`(x.STNumPoints``())`.  
  
 This method returns null if called on an empty **geography** instance.  
  
## Examples  
 The following example creates a `LineString` instance with `STGeomFromText()` and uses `STEndPoint()` to retrieve the end point of the `LineString`.  
  
```sql
DECLARE @g geography;  
SET @g = geography::STGeomFromText('LINESTRING(-122.360 47.656, -122.343 47.656)', 4326);  
SELECT @g.STEndPoint().ToString();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
