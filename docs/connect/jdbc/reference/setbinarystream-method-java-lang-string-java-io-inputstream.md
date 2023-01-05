---
title: "setBinaryStream Method to input stream)"
description: "setBinaryStream Method (java.lang.String, java.io.InputStream)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# setBinaryStream Method (java.lang.String, java.io.InputStream)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the specified input stream.  
  
## Syntax  
  
```  
  
public void setBinaryStream(java.lang.String parameterName,  
                            java.io.InputStream x)  
```  
  
#### Parameters  
 *parameterName*  
  
 A **String** that contains the name of the parameter.  
  
 *x*  
  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setBinaryStream method is specified by the setBinaryStream method in the java.sql.CallableStatement interface.  
  
## See Also  
 [setBinaryStream &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/setbinarystream-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)  
  
  
