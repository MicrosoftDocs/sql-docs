---
title: "Point (geography Data Type) | Microsoft Docs"
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
  - "Point"
  - "Point_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Point method"
  - "Point (geography Data Type)"
ms.assetid: 0dc6f422-7aae-4016-b7f4-3289fa8f989c
caps.latest.revision: 17
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# Point (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Constructs a **geography** instance representing a **Point** instance from its latitude and longitude values and a spatial reference ID (SRID).  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [current version](http://go.microsoft.com/fwlink/p/?LinkId=299658)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] (Initial release through [current release](http://go.microsoft.com/fwlink/p/?LinkId=299659)).|  
  
## Syntax  
  
```  
  
Point ( Lat, Long, SRID )  
```  
  
## Arguments  
 *Lat*  
 Is a **float** expression representing the x-coordinate of the **Point** being generated.  
  
 *Long*  
 Is a **float** expression representing the y-coordinate of the **Point** being generated. For more information on valid latitude and longitude values, see [Point](../../relational-databases/spatial/point.md).  
  
 *SRID*  
 Is an **int** expression representing the SRID of the **geography** instance you wish to return.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
> [!NOTE]  
>  Arguments for the Point (geography Data Type) method have coordinates reversed compared to WKT.  
  
## Examples  
 The following example uses `Point()` to create a `geography` instance.  
  
```  
DECLARE @g geography;   
SET @g = geography::Point(47.65100, -122.34900, 4326)  
SELECT @g.ToString();  
```  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  