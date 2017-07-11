---
title: "CollectionAggregate (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "CollectionAggregate method (geography)"
ms.assetid: e49a644a-dbf2-46c3-98f5-4b3ec197e2ad
caps.latest.revision: 13
author: "BYHAM"
ms.author: "rickbyh"
manager: "jhubbard"
---
# CollectionAggregate (geography Data Type)
[!INCLUDE[tsql-appliesto-ss2012-asdb-xxxx-xxx_md](../../includes/tsql-appliesto-ss2012-asdb-xxxx-xxx-md.md)]

  Creates a **GeometryCollection** instance from a set of **geography** objects.  
  
||  
|-|  
|**Applies to**: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] ([!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [current version](http://msdn.microsoft.com/library/bb500435.aspx)), [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)].|  
  
## Syntax  
  
```  
  
ConvexHullAggregate ( geography_operand )  
```  
  
## Arguments  
 *geography_operand*  
 Is a **geography** type table column that represents a set of **geography** objects to be listed in the **GeometryCollection** instance.  
  
## Return Types  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **geography**  
  
## Exception  
 Throws a `FormatException` when there are input values that are not valid. See [STIsValid &#40;geography Data Type&#41;](../../t-sql/spatial-geography/stisvalid-geography-data-type.md)  
  
## Remarks  
 Method returns **null** when the input is empty or the input has different SRIDs. See [Spatial Reference Identifiers &#40;SRIDs&#41;](../../relational-databases/spatial/spatial-reference-identifiers-srids.md)  
  
 Method ignores **null** inputs.  
  
> [!NOTE]  
>  Method returns **null** if all inputted values are **null**.  
  
## Examples  
 The following example returns a `GeometryCollection` instance that contains a set of **geography** objects.  
  
 `USE AdventureWorks2012`  
  
 `GO`  
  
 `SELECT geography::CollectionAggregate(SpatialLocation).ToString() AS SpatialLocation`  
  
 `FROM Person.Address`  
  
 `WHERE City LIKE ('Bothell')`  
  
## See Also  
 [Extended Static Geography Methods](../../t-sql/spatial-geography/extended-static-geography-methods.md)  
  
  