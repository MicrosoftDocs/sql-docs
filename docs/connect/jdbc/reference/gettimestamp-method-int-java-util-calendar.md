---
title: "getTimestamp Method (int, java.util.Calendar) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerCallableStatement.getTimestamp (int, java.util.Calendar)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 161c559a-8651-44ba-a914-15eb6a612417
author: MightyPen
ms.author: genemi
manager: craigg
---
# getTimestamp Method (int, java.util.Calendar)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated parameter as a java.sql.Timestamp object in the Java programming language, given the parameter index, by using a Calendar object.  
  
## Syntax  
  
```  
  
public java.sql.Timestamp getTimestamp(int index,  
                                       java.util.Calendar cal)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the parameter index.  
  
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
  
  
