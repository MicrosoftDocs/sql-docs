---
description: "IsValidDetailed (geography Data Type)"
title: "IsValidDetailed (geography Data Type) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: t-sql
ms.topic: reference
f1_keywords: 
  - "IsValidDetailed_TSQL"
  - "IsValidDetailed"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "IsValidDetailed geography"
ms.assetid: f5f0b753-c825-43ce-987d-98655d8d8702
author: MladjoA
ms.author: mlandzic 
---
# IsValidDetailed (geography Data Type)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a message that can help to identify problems with a spatial object that is not valid. When the object is not valid, only the first error is returned. When the object is valid, a value of 24400 is returned.  
  
## Syntax  
  
```  
  
.IsValidDetailed()  
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Return Types
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] return type: **nvarchar(max)**  
  
 CLR return type: **string**  
  
## Remarks  
 The following table contains possible return values:  
  
|Return value|Description|  
|------------------|-----------------|  
|24400|Valid|  
|24401|Not valid, reason unknown.|  
|24402|Not valid because point {0} is an isolated point, which is not valid in this type of object.|  
|24403|Not valid because some pair of polygon edges overlap.|  
|24404|Not valid because polygon ring {0} intersects itself or some other ring.|  
|24405|Not valid because some polygon ring intersects itself or some other ring.|  
|24406|Not valid because curve {0} degenerates to a point.|  
|24407|Not valid because polygon ring {0} collapses to a line at point {1}.|  
|24408|Not valid because polygon ring {0} is not closed.|  
|24409|Not valid because some portion of polygon ring {0} lies in the interior of a polygon.|  
|24410|Not valid because ring {0} is the first ring in a polygon of which it is not the exterior ring.|  
|24411|Not valid because ring {0} lies outside the exterior ring {1} of its polygon.|  
|24412|Not valid because the interior of a polygon with rings {0} and {1} is not connected.|  
|24413|Not valid because of two overlapping edges in curve {0}.|  
|24414|Not valid because an edge of curve {0} overlaps an edge of curve {1}.|  
|24415|Not valid some polygon has an invalid ring structure.|  
|24416|Not valid because in curve {0} the edge that starts at point {1} is either a line or a degenerate arc with antipodal endpoints.|  
  
## Examples  
 The following example of an invalid spatial object illustrates how the **IsValidDetailed()** methods behaves.  
  
```sql  
DECLARE @p GEOGRAPHY = 'Polygon((2 2, 4 4, 4 2, 2 4, 2 2))'  
SELECT @p.IsValidDetailed()  
--Returns: 24409: Not valid because some portion of polygon ring (1) lies in the interior of a polygon.  
```  
  
## See Also  
 [Extended Methods on Geography Instances](../../t-sql/spatial-geography/extended-methods-on-geography-instances.md)  
  
  
