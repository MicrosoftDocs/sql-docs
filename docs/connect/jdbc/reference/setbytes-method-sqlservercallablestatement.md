---
title: "setBytes Method (SQLServerCallableStatement)"
description: "setBytes Method (SQLServerCallableStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerCallableStatement.setBytes"
apitype: "Assembly"
---
# setBytes Method (SQLServerCallableStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the designated parameter to the given array of **byte** values.  
  
## Syntax  
  
```  
  
public void setBytes(java.lang.String sCol,  
                     byte[] b)  
```  
  
#### Parameters  
 *sCol*  
  
 A **String** that contains the parameter name.  
  
 *b*  
  
 An array of **byte** values.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 In a previous version of the driver, you could use SQLServerCallableStatement.setBytes to convert values between byte arrays and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type **date**, **time**, **datetime2**, or **datetimeoffset**. Now, using this method with those data types will cause an exception indicating that the conversion is not supported.  
  
 This setBytes method is specified by the setBytes method in the java.sql.CallableStatement interface.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
