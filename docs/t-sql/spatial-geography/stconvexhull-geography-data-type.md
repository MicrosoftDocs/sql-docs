---
description: "STConvexHull (geography Data Type)"
title: "STConvexHull (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STConvexHull method (geography)"
ms.assetid: fb435db7-31bb-4243-9d8b-35379184cfb4
author: MladjoA
ms.author: mlandzic 
---
# STConvexHull (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns an object that represents the convex hull of a **geography** instance.  
  
## Syntax  
  
```  
  
.STConvexHull ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 Returns a `FullGlobe` object for **geography** instance which has an envelope angle larger than 90 degrees.  
  
 Returns an empty **geography** collection for an empty **geography** instance.  
  
 Returns **null** for an uninitialized **geography** instance.  
  
## Examples  
  
### A. Using STConvexHull() on an uninitialized geography instance  
 The following example uses `STConvexHull()` on an uninitialized **geography** instance.  
  
```sql
 DECLARE @g geography;  
 SELECT @g.STConvexHull();
```  
  
### B. Using STConvexHull on an empty geography instance  
 The following example uses `STConvexHull()` on an empty `Polygon` instance.  
  
```sql
 DECLARE @g geography = 'POLYGON EMPTY';  
 SELECT @g.STConvexHull().ToString();
```  
  
### C. Finding the convex hull of a non-convex Polygon instance  
 The following example uses `STConvexHull()` to find the convex hull of a non-convex `Polygon` instance.  
  
```sql
 DECLARE @g geography;  
 SET @g = geography::Parse('POLYGON((-120.533 46.566, -118.283 46.1, -122.3 47.45, -120.533 46.566))');  
 SELECT @g.STConvexHull().ToString();  
```  
  
### D. Finding the convex hull on a geography instance with an envelope angle larger than 90 degrees  
 The following example uses `STConvexHull()` on a **geography** instance with an envelope angle larger than 90 degrees.  
  
```sql
 DECLARE @g geography = 'POLYGON((20.533 46.566, -18.283 46.1, -22.3 47.45, 20.533 46.566))';  
 SELECT @g.STConvexHull().ToString();
```  
  
  
