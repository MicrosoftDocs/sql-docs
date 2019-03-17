---
title: "getMaxFieldSize Method (SQLServerStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerStatement.getMaxFieldSize"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: ed7bbcb8-660b-4e9b-8241-e216c42826f9
author: MightyPen
ms.author: genemi
manager: craigg
---
# getMaxFieldSize Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the maximum number of bytes that can be returned for character and binary column values in a [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object that is produced by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
## Syntax  
  
```  
  
public final int getMaxFieldSize()  
```  
  
## Return Value  
 An **int** that indicates the maximum number of bytes that a column can contain, or 0 if there is no limit.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMaxFieldSize method is specified by the getMaxFieldSize method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Methods](../../../connect/jdbc/reference/sqlserverstatement-methods.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
