---
description: "Point (geography Data Type)"
title: "Point (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "10/10/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "Point"
  - "Point_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Point method"
  - "Point (geography Data Type)"
ms.assetid: 0dc6f422-7aae-4016-b7f4-3289fa8f989c
author: MladjoA
ms.author: mlandzic 
---
# Point (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Constructs a **geography** instance representing a **Point** instance from its latitude and longitude values and a spatial reference ID (SRID).
  
## Syntax  
  
```  
  
Point ( Lat, Long, SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *Lat*  
 Is a **float** expression representing the y-coordinate of the **Point** being generated.  
  
 *Long*  
 Is a **float** expression representing the x-coordinate of the **Point** being generated. For more information on valid latitude and longitude values, see [Point](../../relational-databases/spatial/point.md).  
  
 *SRID*  
 Is an **int** expression representing the [Spatial Reference Identifier](../../relational-databases/spatial/spatial-reference-identifiers-srids.md) of the **geography** instance you wish to return.  
  
> [!NOTE]  
>  Arguments for the Point (geography Data Type) method have coordinates reversed compared to WKT.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Examples  
 The following example uses `Point()` to create a `geography` instance.  
  
```sql
DECLARE @g geography;   
SET @g = geography::Point(47.65100, -122.34900, 4326)  
SELECT @g.ToString();  
```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)
