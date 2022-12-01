---
description: "M (geometry Data Type)"
title: "M (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "M (geometry Data Type)"
  - "M_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "M (geometry Data Type)"
ms.assetid: 443ae2ea-739b-41ef-96cc-ac5dfd65e10b
author: MladjoA
ms.author: mlandzic 
---
# M (geometry Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  The **M** (measure) value of the **geometry** instance. The semantics of the measure value are user-defined.  

## Syntax  
  
```  
  
.M  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property is null if the **geometry** instance is not a **Point**, as well as for any **Point** instance for which it is not set.  
  
 This property is read-only.  
  
 **M** values are not used in any calculations made by the library and will not be carried through any library calculations.  
  
## Examples  
 The following example creates a `Point` instance with Z (elevation) and M (measure) values and uses `M` to fetch the M value of the instance.  
  
```sql
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(1 2 3 4)', 0);  
SELECT @g.M;  
```  
  
## See Also  
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)   
 [Z &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/z-geometry-data-type.md)   
 [AsTextZM &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/astextzm-geometry-data-type.md)  
  
  

