---
title: "setFetchDirection Method (SQLServerStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerStatement.setFetchDirection"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 18176517-2fb3-4266-924d-0f01253083d2
author: MightyPen
ms.author: genemi
manager: craigg
---
# setFetchDirection Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gives [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] a hint as to the direction in which result set rows should be processed.  
  
> [!NOTE]  
>  The JDBC driver currently ignores the hint that is given by this method.  
  
## Syntax  
  
```  
  
public final void setFetchDirection(int nDir)  
```  
  
#### Parameters  
 *nDir*  
  
 An **int** that indicates the row processing direction, which can be one of the following values:  
  
 FETCH_FORWARD  
  
 FETCH_REVERSE  
  
 FETCH_UNKNOWN  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setFetchDirection method is specified by the setFetchDirection method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
