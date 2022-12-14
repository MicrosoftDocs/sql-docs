---
title: "registerOutParameter Method to type and scale"
description: "registerOutParameter Method (java.lang.String, int, int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.registerOutParameter (java.lang.String, int, int)"
apitype: "Assembly"
---
# registerOutParameter Method (java.lang.String, int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Registers the OUT parameter with the specified name to the given JDBC type and scale.  
  
## Syntax  
  
```  
  
public void registerOutParameter(java.lang.String s,  
                                 int n,  
                                 int n1)  
```  
  
#### Parameters  
 *s*  
  
 A **String** that contains the parameter name.  
  
 *sqlType*  
  
 A JDBC type code as defined in java.sql.Types.  
  
 *scale*  
  
 An **int** that indicates the number of digits to be placed to the right of the decimal point.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This registerOutParameter method is specified by the registerOutParameter method in the java.sql.CallableStatement interface.  
  
## See Also  
 [registerOutParameter Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/registeroutparameter-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
