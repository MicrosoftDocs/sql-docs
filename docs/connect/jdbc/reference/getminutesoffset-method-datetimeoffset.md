---
title: "getMinutesOffset Method (DateTimeOffset) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 18ba844a-ea36-42de-87da-bbc222082efe
author: MightyPen
ms.author: genemi
manager: craigg
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
  
## See Also  
 [DateTimeOffset Class](../../../connect/jdbc/reference/datetimeoffset-class.md)   
 [DateTimeOffset Members](../../../connect/jdbc/reference/datetimeoffset-members.md)  
  
  
