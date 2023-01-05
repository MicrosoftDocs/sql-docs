---
title: "getFloat Method (java.lang.String)"
description: "getFloat Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getFloat (java.lang.String)"
apitype: "Assembly"
---
# getFloat Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a **float** in the Java programming language given the parameter name.  
  
## Syntax  
  
```  
  
public float getFloat(java.lang.String sCol)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 A **float** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getFloat method is specified by the getFloat method in the java.sql.CallableStatement interface.  
  
 This method returns all number-based types with Java **float** fidelity.  
  
## See Also  
 [getFloat Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getfloat-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
