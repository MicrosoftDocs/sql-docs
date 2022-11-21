---
description: "STX (geometry Data Type)"
title: "STX (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "06/23/2020"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "STX (geometry Data Type)"
  - "STX_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STX (geometry Data Type)"
ms.assetid: 2aef77e8-0460-43f9-bad6-2aae6d8c36f9
author: MladjoA
ms.author: mlandzic 
---
# STX (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

The  X-coordinate property of a **Point** instance.
  
## Syntax  
  
```  
  
.STX  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property will be null if the **geometry** instance is not a point.  
  
 This property is read-only.  
  
## Examples  
 The following example creates a `Point` instance and uses `STX` to retrieve the X-coordinate of the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(3 8)', 0);  
SELECT @g.STX;  
```  
  
## See Also  
 [STY &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/sty-geometry-data-type.md)   
 [STSrid &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stsrid-geometry-data-type.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  

