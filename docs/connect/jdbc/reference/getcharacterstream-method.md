---
title: "getCharacterStream Method ()"
description: "getCharacterStream Method ()"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerClob.getCharacterStream"
apitype: "Assembly"
---
# getCharacterStream Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the **CLOB** data as a Reader object or as a stream of characters.  
  
## Syntax  
  
```  
  
public java.io.Reader getCharacterStream()  
```  
  
## Return Value  
 A Reader object that contains the **CLOB** data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCharacterStream method is specified by the getCharacterStream method in the java.sql.Clob interface.  
  
## Related content

- [SQLServerClob Methods](sqlserverclob-methods.md)
- [SQLServerClob Members](sqlserverclob-members.md)
- [SQLServerClob Class](sqlserverclob-class.md)
