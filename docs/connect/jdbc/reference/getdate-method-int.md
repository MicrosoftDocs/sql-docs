---
title: "getDate Method (int)"
description: "getDate Method (int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getDate (int)"
apitype: "Assembly"
---
# getDate Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a java.sql.Date object in the Java programming language given the parameter index.  
  
## Syntax  
  
```  
  
public java.sql.Date getDate(int index)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 A Date object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDate method is specified by the getDate method in the java.sql.CallableStatement interface.  
  
 This method returns a valid date part of a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] **datetime** or **smalldatetime** data type, with the time part set to the Java time baseline of 00:00 (midnight).  
  
## See Also  
 [getDate Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getdate-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
