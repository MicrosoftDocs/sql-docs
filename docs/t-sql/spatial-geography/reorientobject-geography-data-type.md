---
title: "ReorientObject (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "ReorientObject"
  - "ReorientObject_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "ReorientObject method (geography)"
ms.assetid: e2a1a4f1-211b-4e82-abed-03fc7140a83c
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# ReorientObject (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Returns a **geography** instance with interchanged interior regions and exterior regions.  
  
 This **geography** data type method supports **FullGlobe** instances or spatial instances that are larger than a hemisphere.  
  
## Syntax  
  
```  
  
.ReorientObject (geography)  
```  
  
## Arguments  
 *geography*  
 Is another **geography** instance on which `ReorientObject()` is invoked.  
  
## Return Value  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
 CLR return type: **SqlGeography**  
  
## Remarks  
 This method changes the ring orientation of all **Polygons** in a **GeometryCollection** but does not remove or change any **Points** or **Linestrings** in the given collection.  
  
 If a **GeometryCollection** is passed to this method, each instance in the collection is reoriented, but the collection as a whole is not reoriented.  
  
## Examples  
  
```  
DECLARE @R GEOGRAPHY = GEOGRAPHY::Parse('Polygon((-10 -10, -10 10, 10 10, 10 -10, -10 -10))');  
SELECT @R.ReorientObject().STAsText();  
--Result: POLYGON ((10 10, -10 10, -10 -10, 10 -10, 10 10))  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
