---
title: "setNull Method (java.lang.String, int, java.lang.String)"
description: "setNull Method (java.lang.String, int, java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.setNull (java.lang.String, int, java.lang.String)"
apitype: "Assembly"
---
# setNull Method (java.lang.String, int, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to a null value, given the type and name of the parameter to set.  
  
## Syntax  
  
```  
  
public void setNull(java.lang.String sCol,  
                    int nType,  
                    java.lang.String sTypeName)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** contthat contains aining the parameter name.  
  
 *nType*  
  
 A JDBC type code that is defined by java.sql.Types.  
  
 *sTypeName*  
  
 A **String** that indicates the fully qualified name of the parameter that is being set.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setNull method is specified by the setNull method in the java.sql.CallableStatement interface.  
  
## See Also  
 [setNull Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/setnull-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
