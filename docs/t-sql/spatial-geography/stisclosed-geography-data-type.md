---
title: "STIsClosed (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "STIsClosed (geography Data Type)"
  - "STIsClosed_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STIsClosed method"
ms.assetid: eba1643f-07c4-4500-8643-b7e90f908147
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STIsClosed (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns 1 if the start and end points of the given **geography** instance are the same. Returns 1 for **geography** collection types if each contained **geography** instance is closed. Returns 0 if the instance is not closed.  
  
 This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.STIsClosed ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 This method returns 0 if any figures of a **geography** instance are points, or if the instance is empty.  
  
 This method returns true if a **FullGlobe** instance is a **Polygon** or other type of instance.  
  
 All **Polygon** instances are considered closed.  
  
## Examples  
 The following example creates a `Polygon` instance and uses `STIsClosed()` to test if the `Polygon` is closed.  
  
```  
DECLARE @g geography;  
SET @g = geography::STGeomFromText('POLYGON((-122.358 47.653, -122.348 47.649, -122.348 47.658, -122.358 47.658, -122.358 47.653))', 4326);  
SELECT @g.STIsClosed();  
```  
  
## See Also  
 [OGC Methods on Geography Instances](../../t-sql/spatial-geography/ogc-methods-on-geography-instances.md)  
  
  
