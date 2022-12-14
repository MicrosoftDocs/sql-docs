---
description: "EnvelopeCenter (geography Data Type )"
title: "EnvelopeCenter (geography Data Type ) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "EnvelopeCenter"
  - "EnvelopeCenter_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "EnvelopeCenter method"
ms.assetid: dee9d807-faad-45b8-b3f3-7e8aa7d07147
author: MladjoA
ms.author: mlandzic 
---
# EnvelopeCenter (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

Returns a point that you can use as the bounding circle's center for the **geography** instance.  
  
Each point in the instance is described as a vector. To figure out the bounding circle, the vector extends from the Earth's center to the point on the Earth's surface. The bounding circle's center point is calculated by averaging all of the vectors. For closed loops, either in a **Polygon** instance or a **LineString** instance, the first and last point is used only once.  
  
This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
EnvelopeCenter( )  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
CLR return type: **SqlGeography**  
  
## Remarks  
This method returns a **point**. When used with `EnvelopeAngle()`, `EnvelopeCenter()` returns a bounding circle of a **geography** instance.  
  
> [!NOTE]  
>  `EnvelopeCenter()` returns a bounding circle for a **geography** instance, but the results are not guaranteed to produce the minimal bounding circle. In contrast, the **geometry** data type method `STEnvelope()` is guaranteed to return the minimum bounding box when it is applied to a **geometry** instance.  
  
In [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and higher, returns the center of the circle representing the envelope of this instance as a **point**. For all large objects as defined by `EnvelopeAngle()` = 180, `EnvelopeCenter()` will return (90,0).  
  
This method isn't precise.  
  
## Examples  
  
```sql
DECLARE @g geography = 'LINESTRING(-120 45, -120 0, -90 0)';  
SELECT @g.EnvelopeCenter().ToString();  
```  
  
## See Also  
[Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
[EnvelopeAngle &#40;geography Data Type&#41;](../../t-sql/spatial-geography/envelopeangle-geography-data-type.md)  
  
  
