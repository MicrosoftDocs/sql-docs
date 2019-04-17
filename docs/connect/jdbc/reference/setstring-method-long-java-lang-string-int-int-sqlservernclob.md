---
title: "setString Method (long, java.lang.String, int, int) - NClob | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 2d5e9f50-15b2-4c76-8bfc-3b5be49c2781
author: MightyPen
ms.author: genemi
manager: craigg
---
# setString Method (long, java.lang.String, int, int) (SQLServerNClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Writes the specified string to the NCLOB starting at the specified position, based on the specified offset and length.  
  
## Syntax  
  
```  
  
int setString(long pos,  
              java.lang.String str,  
              int offset,  
              int len)  
```  
  
#### Parameters  
 *pos*  
  
 The position at which to start writing to the **NCLOB**; the first position is 1.  
  
 *str*  
  
 The String to be written to the **NCLOB**.  
  
 *offset*  
  
 The offset into *str* to start reading the characters to be written.  
  
 *len*  
  
 The number of characters to be written.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setString method is specified by the setString method in the java.sql.NClob interface.  
  
## See Also  
 [SQLServerNClob Methods](../../../connect/jdbc/reference/sqlservernclob-methods.md)   
 [SQLServerNClob Members](../../../connect/jdbc/reference/sqlservernclob-members.md)   
 [SQLServerNClob Class](../../../connect/jdbc/reference/sqlservernclob-class.md)  
  
  
