---
title: "setFetchDirection Method (SQLServerStatement)"
description: "setFetchDirection Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.setFetchDirection"
apitype: "Assembly"
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
  
  
