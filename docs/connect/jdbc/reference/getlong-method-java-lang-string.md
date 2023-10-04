---
title: "getLong Method (java.lang.String)"
description: "getLong Method (java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getLong (java.lang.String)"
apitype: "Assembly"
---
# getLong Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a **long** in the Java programming language given the parameter name.  
  
## Syntax  
  
```  
  
public long getLong(java.lang.String sCol)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
## Return Value  
 A **long** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getLong method is specified by the getLong method in the java.sql.CallableStatement interface.  
  
 This method is supported only on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types that can safely return an integer value such as **bigint**, **int**, **smallint**, **tinyint**, and **bit**. Using this method on any other data types will cause an exception to be thrown.  
  
## See Also  
 [getLong Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getlong-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
