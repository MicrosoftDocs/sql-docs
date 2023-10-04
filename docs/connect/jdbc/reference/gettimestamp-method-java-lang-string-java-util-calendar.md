---
title: "getTimestamp Method (java.lang.String, java.util.Calendar)"
description: "getTimestamp Method (java.lang.String, java.util.Calendar)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getTimestamp (java.lang.String,java.util.Calendar)"
apitype: "Assembly"
---
# getTimestamp Method (java.lang.String, java.util.Calendar)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a java.sql.Timestamp object in the Java programming language, given the parameter name, by using a Calendar object.  
  
## Syntax  
  
```  
  
public java.sql.Timestamp getTimestamp(java.lang.String name,  
                                       java.util.Calendar cal)  
```  
  
#### Parameters  
 *name*  
  
 A **String** that contains the parameter name.  
  
 *cal*  
  
 A Calendar object.  
  
## Return Value  
 A Timestamp object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getTimestamp method is specified by the getTimestamp method in the java.sql.CallableStatement interface.  
  
 This method returns values only from [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **datetime** and **smalldatetime** columns.  
  
## See Also  
 [getTimestamp Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/gettimestamp-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
