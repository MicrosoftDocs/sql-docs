---
title: "getCharacterStream Method (long, long)"
description: "getCharacterStream Method (long, long)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# getCharacterStream Method (long, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the **Clob** data as a Reader object or as a stream of characters with the specified position and length.  
  
## Syntax  
  
```  
  
public java.io.Reader getCharacterStream(long pos,  
                                         long length)  
```  
  
#### Parameters  
 *pos*  
  
 A **long** that indicates the offset to the first character of the partial value to be retrieved.  
  
 *length*  
  
 A **long** that indicates the length in characters of the partial value to be retrieved.  
  
## Return Value  
 A Reader object that contains the **Clob** data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCharacterStream method is specified by the getCharacterStream method in the java.sql.Clob interface.  
  
## See Also  
 [getCharacterStream Method &#40;SQLServerClob&#41;](../../../connect/jdbc/reference/getcharacterstream-method-sqlserverclob.md)   
 [SQLServerClob Methods](../../../connect/jdbc/reference/sqlserverclob-methods.md)   
 [SQLServerClob Members](../../../connect/jdbc/reference/sqlserverclob-members.md)  
  
  
