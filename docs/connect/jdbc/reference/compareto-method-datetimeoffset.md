---
title: "compareTo Method (DateTimeOffset) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: e4cf2ea4-0fe9-40ce-ba79-f2a2b616997e
author: MightyPen
ms.author: genemi
manager: craigg
---
# compareTo Method (DateTimeOffset)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Compares this **DateTimeOffset** object to another **DateTimeOffset** object based on the time at GMT.  
  
## Syntax  
  
```  
  
public int compareTo(DateTimeOffset other)  
```  
  
#### Parameters  
 A [DateTimeOffset](../../../connect/jdbc/reference/datetimeoffset-class.md) value that you want to compare to the current instance.  
  
## Return Value  
 The following table describes the return value of this method:  
  
|Return value|Description|  
|------------------|-----------------|  
|0|Both **DateTimeOffset** objects represent the same point in time.|  
|negative number|This **DateTimeOffset** object represents a point in time that is before *other*.|  
|positive number|This **DateTimeOffset** object represents a point in time that is after *other*.|  
  
## Remarks  
 When two **DateTimeOffset** objects have the same time at GMT, there is no additional ordering of the objects based on the offset.  
  
## See Also  
 [DateTimeOffset Class](../../../connect/jdbc/reference/datetimeoffset-class.md)   
 [DateTimeOffset Members](../../../connect/jdbc/reference/datetimeoffset-members.md)  
  
  
