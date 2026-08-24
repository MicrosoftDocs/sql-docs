---
title: "getMinutesOffset Method (DateTimeOffset)"
description: "getMinutesOffset Method (DateTimeOffset)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getMinutesOffset Method (DateTimeOffset)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the offset, in minutes from GMT, of this DateTimeOffset object.  
  
## Syntax  
  
```  
  
public int getMinutesOffset()  
```  
  
## Return Value  
 The offset in minutes.  
  
## Remarks  
 For a DateTimeOffset object representing 8 March 2010, 11:35:48 -0800, getMinutesOffset returns the value 480.  
  
## Related content

- [DateTimeOffset Class](datetimeoffset-class.md)
- [DateTimeOffset Members](datetimeoffset-members.md)
