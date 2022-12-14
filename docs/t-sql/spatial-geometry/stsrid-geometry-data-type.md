---
description: "STSrid (geometry Data Type)"
title: "STSrid (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STSrid (geometry Data Type)"
  - "STSrid_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STSrid (geometry Data Type)"
ms.assetid: 5e0de983-a0fe-48b7-9e08-30588d7271e2
author: MladjoA
ms.author: mlandzic 
---
# STSrid (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  **STSrid** is an integer representing the spatial reference identifier of the instance.  
  
This property can be modified.
  
## Syntax  
  
```  
  
STSrid  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **int**  
  
 CLR type: **SqlInt32**  
  
## Examples  
 The first example creates a **geometry** instance with the SRID value 13 and uses `STSrid` to confirm the SRID.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 3 0, 3 3, 0 3, 0 0))', 13);  
SELECT @g.STSrid;  
```  
  
 The second example uses `STSrid` to change the SRID value of the instance to 23 and then confirms the modified SRID value.  
  
```sql
SET @g.STSrid = 23;  
SELECT @g.STSrid;  
```  
  
## See Also  
 [STX &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stx-geometry-data-type.md)   
 [STY &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/sty-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

