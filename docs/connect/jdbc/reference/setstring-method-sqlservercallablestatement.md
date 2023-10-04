---
title: "setString Method (SQLServerCallableStatement)"
description: "setString Method (SQLServerCallableStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.setString"
apitype: "Assembly"
---
# setString Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given Java **String** value.  
  
## Syntax  
  
```  
  
public void setString(java.lang.String sCol,  
                      java.lang.String s)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the name of the parameter.  
  
 *s*  
  
 A **String** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setString method is specified by the setString method in the java.sql.CallableStatement interface.  
  
 String to binary conversions are performed only when [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] knows the destination type is binary. In cases where the JDBC driver does not know the underlying type, it will pass the **String** literal and return a server error if the server cannot perform the conversion.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
