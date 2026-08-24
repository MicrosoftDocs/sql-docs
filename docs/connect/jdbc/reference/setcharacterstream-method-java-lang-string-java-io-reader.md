---
title: "setCharacterStream Method (java.lang.String, java.io.Reader)"
description: "setCharacterStream Method (java.lang.String, java.io.Reader)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setCharacterStream Method (java.lang.String, java.io.Reader)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the specified Reader object.  
  
## Syntax  
  
```  
  
public final void setCharacterStream(java.lang.String parameterName,  
                                             java.io.Reader reader)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that contains the parameter name.  
  
 *reader*  
  
 A Reader object that contains the Unicode data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setCharacterStream method is specified by the setCharacterStream method in the java.sql.CallableStatement interface.  
  
## Related content

- [setCharacterStream Method (SQLServerCallableStatement)](setcharacterstream-method-sqlservercallablestatement.md)
- [SQLServerCallableStatement Members](sqlservercallablestatement-members.md)
