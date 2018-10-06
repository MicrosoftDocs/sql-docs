---
title: "STContains  (geography Data Type) | Microsoft Docs"
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
  - "STContains method (geography)"
ms.assetid: b10e8f0a-2926-449a-82ea-be42543420ca
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# STContains  (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Specifies whether the calling **geography** instance spatially contains the **geography** instance passed to the method.  
  
## Syntax  
  
```  
  
.STContains ( other_geography )  
```  
  
## Arguments  
 *other_geography*  
 Is another **geography** instance to compare against the instance on which `STContains()` is invoked.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **bit**  
  
 CLR return type: **SqlBoolean**  
  
## Remarks  
 Returns 1 if the calling **geography** instance spatially contains the **geography** instance passed to the method, and returns 0 if it does not. Returns **null** if the SRID of the two **geography** instances are not the same.  
  
## Examples  
 The following example uses `STContains()` to test two `geography` instances to see if the first instance contains the second instance.  
  
```  
DECLARE @g geography;  
DECLARE @h geography;  
SET @g = geography::Parse('CURVEPOLYGON (COMPOUNDCURVE (CIRCULARSTRING (-122.200928 47.454094, -122.810669 47.00648, -122.942505 46.687131, -121.14624 45.786679, -119.119263 46.183634), (-119.119263 46.183634, -119.273071 47.107523, -120.640869 47.569114, -122.200928 47.454094)))');  
SET @h = geography::Parse('POINT(-121.703796 46.893985)');  
```  
  
 `SELECT @g.STContains(@h);`  
  
  
