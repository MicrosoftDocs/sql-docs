---
title: "M (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "M (geography Data Type)"
  - "M_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "M method"
ms.assetid: cdba04f0-4e17-48f6-bafb-b1f918c5a501
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# M (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  The **M** (measure) value of the **geography** instance. The semantics of the measure value are user-defined but generally describe the distance along a linestring. For example, the measure value could be used to keep track of mileposts along a road.  
  
## Syntax  
  
```  
  
.M  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] type: **float**  
  
 CLR type: **SqlDouble**  
  
## Remarks  
 The value of this property is null if the **geography** instance is not a **Point**, as well as for any **Point** instance for which it is not set.  
  
 This property is read-only.  
  
 M values are not used in any calculations made by the library and will not be carried through any library calculations.  
  
## Examples  
 The following example creates a `Point` instance with Z (elevation) and M (measure) values and uses `M` to fetch the `M` value of the instance.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POINT(-122.34900 47.65100 10.3 12)', 4326);  
SELECT @g.M;  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)   
 [Z &#40;geography Data Type&#41;](../../t-sql/spatial-geography/z-geography-data-type.md)  
  
  
