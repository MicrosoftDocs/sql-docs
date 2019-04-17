---
title: "Z (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "Z (geometry Data Type)"
  - "Z_(geometry_Data_Type)_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Z (geometry Data Type)"
ms.assetid: a62ed736-44df-4591-9109-ce90e1df9bd3
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Z (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

The Z (elevation) value of the instance. The semantics of the elevation value are user-defined.
  
## Syntax  
  
```  
  
.Z  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property will be null if the geometry instance is not a point, as well as for any **Point** instance for which it is not set.  
  
 This property is read-only.  
  
 Z-coordinates are not used in any calculations made by the library and is not carried through any library calculations.  
  
## Examples  
 The following example creates a `Point` instance with Z (elevation) and M (measure) values and uses `Z` to fetch the Z value of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(1 2 3 4)', 0);  
SELECT @g.Z;  
```  
  
## See Also  
 [M &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/m-geometry-data-type.md)   
 [AsTextZM &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/astextzm-geometry-data-type.md)   
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)  
  
  

