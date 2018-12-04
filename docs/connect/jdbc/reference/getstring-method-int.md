---
title: "getString Method (int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.getString (int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: f3fce8bf-8d6e-476f-aa6d-992daa79b899
author: MightyPen
ms.author: genemi
manager: craigg
---
# getString Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a **String** in the Java programming language given the parameter index.  
  
## Syntax  
  
```  
  
public java.lang.String getString(int index)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 A **String** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getString method is specified by the getString method in the java.sql.CallableStatement interface.  
  
 All columns in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] can be returned as a string. This means that a string representation of all number-based and character-based types, and a hex-string representation of binary columns such as binary, varbinary, varbinary(max), image, timestamp, and uniqueidentifier, can be returned.  
  
 Location-sensitive types such as money, smallmoney, datetime, smalldatetime, float, real, decimal, and numeric will return the canonical toString() format for the underlying value of the type.  
  
 User-defined types are returned as hexadecimal string values.  
  
## See Also  
 [getString Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getstring-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
