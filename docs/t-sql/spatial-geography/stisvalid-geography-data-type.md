---
title: "STIsValid (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIsValid method (geography)"
ms.assetid: 1bfe787f-ddf0-4fc7-af6a-570a58faab23
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STIsValid (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns true if a **geography** instance is well-formed and recognized as a valid geography object based on its Open Geospatial Consortium (OGC) type. Returns false if a **geography** instance is not well-formed. This method is precise.  
  
 This geography data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.STIsValid ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 The OGC type of a **geography** instance can be determined by invoking [STGeometryType()](../../t-sql/spatial-geography/stgeometrytype-geography-data-type.md).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] produces only valid **geography** instances, but allows for the storage and retrieval of invalid instances. A valid instance representing the same point set of an invalid instance can be retrieved using the `MakeValid()` method.  
  
## Examples  
 The following example creates a `geography` instance and uses `STIsValid()` to test if the instance is valid.  
  
```  
DECLARE @g geography = geography::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 4326);  
SELECT @g.STIsValid();  
DECLARE @g geography  
```  
  
## See Also  
 [STGeometryType &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stgeometrytype-geography-data-type.md)   
 [MakeValid &#40;geography Data Type&#41;](../../t-sql/spatial-geography/makevalid-geography-data-type.md)   
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
