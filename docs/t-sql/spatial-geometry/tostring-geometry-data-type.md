---
title: "ToString (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "08/03/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ToString (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ToString (geometry Data Type)"
ms.assetid: 2e55fa98-aa22-4baa-a516-7c233a33e212
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ToString (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

Returns the Open Geospatial Consortium (OGC) Well-Known Text (WKT) representation of a geometry instance augmented with any Z (elevation) and M (measure) values carried by the instance.
  
## Syntax  
  
```  
  
.ToString ()  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **SqlString**  
  
## Remarks  
 This method will return the string "Null" when called on null instances.  
  
 On non-null instances, this method is equivalent to using `AsTextZM().`  
  
## Examples  
 The following example create a `LineString` instance and uses `ToString()` to fetch the text description of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 0 1, 1 0)', 0);  
SELECT @g.ToString();  
```  
  
## See Also  
 [STAsText &#40;geometry Data Type&#41;](../../t-sql/spatial-geometry/stastext-geometry-data-type.md)   
 [Extended Methods on Geometry Instances](../../t-sql/spatial-geometry/extended-methods-on-geometry-instances.md)  
  
  

