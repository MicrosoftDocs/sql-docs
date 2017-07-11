---
title: "STLength (geometry Data Type) | Microsoft Docs"
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
  - "STLength_TSQL"
  - "STLength (geometry Data Type)"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "STLength (geometry Data Type)"
ms.assetid: e34dc620-2a65-4248-b099-fff91830ab98
caps.latest.revision: 20
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# STLength (geometry Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Returns the total length of the elements in a **geometry** instance.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
  
## Syntax  
  
```  
  
.STLength ( )  
```  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **float**  
  
 CLR return type: **SqlDouble**  
  
## Remarks  
 If a **geometry** instance is closed, its length is calculated as the total length around the instance; the length of any polygon is its perimeter and the length of a point is 0. The length of any **geometrycollection** type is the sum of the lengths of its contained **geometry** instances.  
  
 STLength() works on both valid and invalid LineStrings. Typically a LineString is invalid due to overlapping segments, which may be caused by anomalies such as inaccurate GPS traces. STLength() does not remove overlapping or invalid segments. It includes overlapping and invalid segments in the length value that it returns. The MakeValid() method can remove overlapping segments from a LineString.  
  
## Examples  
 The following example creates a `LineString` instance and uses `STLength()` to find the length of the instance.  
  
```  
DECLARE @g geometry;  
SET @g = geometry::STGeomFromText('LINESTRING(0 0, 2 2, 1 0)', 0);  
SELECT @g.STLength();  
```  
  
## See Also  
 [OGC Methods on Geometry Instances](../../t-sql/spatial-geometry/ogc-methods-on-geometry-instances.md)  
  
  