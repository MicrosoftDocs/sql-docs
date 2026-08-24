---
title: "valueOf Method (java.sql.Timestamp, int)"
description: "valueOf Method (java.sql.Timestamp, int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# valueOf Method (java.sql.Timestamp, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a **DateTimeOffset** object representing a point in time at a particular offset from GMT given a java.sql.Timestamp value and a value indicating the offset in minutes.  
  
## Syntax  
  
```  
  
public static DateTimeOffset valueOf(java.sql.Timestamp timestamp, int minutesOffset)  
```  
  
#### Parameters  
 *timestamp*  
  
 A java.sql.Timestamp value.  
  
 *minutesOffset*  
  
 The offset in minutes.  
  
## Return Value  
 Returns a DateTimeOffset object representing the point in time given by the java.sql.Timestamp object at the given offset, in minutes, from GMT.  
  
## Related content

- [DateTimeOffset Class](datetimeoffset-class.md)
- [DateTimeOffset Members](datetimeoffset-members.md)
