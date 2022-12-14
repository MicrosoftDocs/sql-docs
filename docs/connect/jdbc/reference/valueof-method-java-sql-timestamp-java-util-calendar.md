---
title: "valueOf Method (java.sql.Timestamp, java.util.Calendar)"
description: "valueOf Method (java.sql.Timestamp, java.util.Calendar)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# valueOf Method (java.sql.Timestamp, java.util.Calendar)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a **DateTimeOffset** object representing a point in time at a particular offset from GMT given a java.sql.Timestamp value and a java.util.Calendar value indicating the offset.  
  
## Syntax  
  
```  
  
public static DateTimeOffset valueOf(java.sql.Timestamp timestamp, java.util.Calendar calendar)  
```  
  
#### Parameters  
 *timestamp*  
  
 Ajava.sql.Timestamp value.  
  
 *calendar*  
  
 The offset value.  The date and time components of *calendar* will be set according to the *timestamp* value.  
  
## Return Value  
 Returns a DateTimeOffset object representing the point in time given by the java.sql.Timestamp object at the given java.util.Calendar object's time zone.  
  
## Remarks  
 This method also sets the java.util.Calendar object to the point in time given by the java.sql.Timestamp object.  
  
## See Also  
 [DateTimeOffset Class](../../../connect/jdbc/reference/datetimeoffset-class.md)   
 [DateTimeOffset Members](../../../connect/jdbc/reference/datetimeoffset-members.md)  
  
  
