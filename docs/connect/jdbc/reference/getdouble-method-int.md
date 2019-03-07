---
title: "getDouble Method (int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.getDouble (int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: c0ed63bb-5ebe-4155-9f91-8fbfeac9c3b2
author: MightyPen
ms.author: genemi
manager: craigg
---
# getDouble Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a **double** in the Java programming language given the parameter index.  
  
## Syntax  
  
```  
  
public double getDouble(int index)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the parameter index.  
  
## Return Value  
 A **double** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDouble method is specified by the getDouble method in the java.sql.CallableStatement interface.  
  
 This method returns all number-based data types with Java **double** fidelity.  
  
## See Also  
 [getDouble Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/getdouble-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
