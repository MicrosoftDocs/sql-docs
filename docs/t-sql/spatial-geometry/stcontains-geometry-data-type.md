---
title: "STContains (geometry Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "STContains (geometry Data Type)"
  - "STContains_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STContains (geometry Data Type)"
ms.assetid: 865ceca1-9200-45ed-a7d8-e286e2679fdc
caps.latest.revision: 26
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STContains (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns 1 if a **geometry** instance completely contains another **geometry** instance. Returns 0 if it does not.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](http://msdn.microsoft.com/library/bb500435.aspx)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
  
## Syntax  
  
```  
  
.STContains ( other_geometry )  
```  
  
## Arguments  
 *other_geometry*  
 Is another **geometry** instance to compare against the instance on which `STContains()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 `STContains()` always returns null if the spatial reference IDs (SRIDs) of the **geometry** instances do not match.  
  
## Examples  
 The following example uses `STContains()` to test two `geometry` instances to see if the first instance contains the second instance.  
  
```  
DECLARE @g geometry;  
DECLARE @h geometry;  
SET @g = geometry::STGeomFromText('POLYGON((0 0, 2 0, 2 2, 0 2, 0 0))', 0);  
SET @h = geometry::STGeomFromText('POINT(1 1)', 0);  
SELECT @g.STContains(@h);  
```  
  
## See Also  
 [Spatial Indexes Overview](../../relational-databases/spatial/spatial-indexes-overview.md)   
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  
