---
description: "Point (geometry Data Type)"
title: "Point (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
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
  - "Point (geometry Data Type)"
ms.assetid: 7a2e593a-4d4c-4214-b0c5-02d1ac46bc66
author: MladjoA
ms.author: mlandzic 
---
# Point (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Constructs a **geometry** instance representing a **Point** instance from its X and Y values and an SRID.
  
## Syntax  
  
```  
  
Point ( X, Y, SRID )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *X*  
 Is a **float** expression representing the X-coordinate of the **Point** being generated.  
  
 *Y*  
 Is a **float** expression representing the Y-coordinate of the **Point** being generated.  
  
 *SRID*  
 Is an **int** expression representing the spatial reference ID (SRID) of the **geometry** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geometry**  
  
 CLR return type: **SqlGeometry**  
  
## Remarks  
  
## Examples  
 The following example uses `Point()` to create a `geometry` instance.  
  
```sql
DECLARE @g geometry;   
SET @g = geometry::Point(1, 10, 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [Extended Static Geometry Methods](../../t-sql/spatial-geometry/extended-static-geometry-methods.md)  
  
  

