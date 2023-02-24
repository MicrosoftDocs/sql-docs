---
description: "STLength (geometry Data Type)"
title: "STLength (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STLength_TSQL"
  - "STLength (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STLength (geometry Data Type)"
ms.assetid: e34dc620-2a65-4248-b099-fff91830ab98
author: MladjoA
ms.author: mlandzic 
---
# STLength (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns the total length of the elements in a **geometry** instance.
  
## Syntax  
  
```  
  
.STLength ( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
 CLR return type: **SqlDouble**  
  
## Remarks  
 If a **geometry** instance is closed, its length is calculated as the total length around the instance; the length of any polygon is its perimeter and the length of a point is 0. The length of any **geometrycollection** type is the sum of the lengths of its contained **geometry** instances.  
  
 STLength() works on both valid and invalid LineStrings. Typically a LineString is invalid due to overlapping segments, which may be caused by anomalies such as inaccurate GPS traces. STLength() does not remove overlapping or invalid segments. It includes overlapping and invalid segments in the length value that it returns. The MakeValid() method can remove overlapping segments from a LineString.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STLength()` to find the length of the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STLength();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

