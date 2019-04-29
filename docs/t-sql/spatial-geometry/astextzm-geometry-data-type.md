---
title: "AsTextZM (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "AsTextZM_TSQL"
  - "AsTextZM"
  - "AsTextZM (geometry Data Type)"
  - "AsTextZM_(geometry_Data_Type)_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "AsTextZM (geometry Data Type)"
ms.assetid: 08ac8aa0-aff7-4b22-87e0-1a1d55dcbc04
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# AsTextZM (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a geometry instance augmented with any **Z** (elevation) and **M** (measure) values carried by the instance.
  
## Syntax  
  
```  
  
.AsTextZM ()  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **SqlChars**  
  
## Remarks  
  
## Examples  
 The following example creates a `Point` instance that contains **Z** (elevation) and **M** (measure) values. `STAsText()` selects the WKT values, (1 2); `AsTextZM()` selects the same WKT values and also returns the values for **Z** and **M**, yielding (1 2 3 4).  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('POINT(1 2 3 4)', 0);  
SELECT @g.STAsText();  
SELECT @g.AsTextZM();  
```  
  
## See Also  
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)   
 [M &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/m-geometry-data-type.md)   
 [Z &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/z-geometry-data-type.md)  
  
  

