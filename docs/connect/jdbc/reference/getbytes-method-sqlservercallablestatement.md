---
title: "getBytes Method (SQLServerCallableStatement)"
description: "getBytes Method (SQLServerCallableStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.getBytes"
apitype: "Assembly"
---
# getBytes Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as an array of bytes.  
  
## Overload List  
  
|Name|Description|  
|----------|-----------------|  
|[getBytes (int)](../../../connect/jdbc/reference/getbytes-method-int.md)|Retrieves the value of the designated parameter as an array of bytes value given the parameter index.|  
|[getBytes (java.lang.String)](../../../connect/jdbc/reference/getbytes-method-java-lang-string.md)|Retrieves the value of the designated parameter as an array of bytes value given the parameter name.|  
  
## Remarks  
 In a previous version of the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)], you could use SQLServerCallableStatement.getBytes to convert values between byte arrays and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type **date**, **time**, **datetime2**, or **datetimeoffset**. Now, using this method with those data types will cause an exception indicating that the conversion is not supported.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
