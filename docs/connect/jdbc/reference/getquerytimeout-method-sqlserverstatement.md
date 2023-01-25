---
title: "getQueryTimeout Method (SQLServerStatement)"
description: "getQueryTimeout Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getQueryTimeout"
apitype: "Assembly"
---
# getQueryTimeout Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the number of seconds [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will wait for this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object to run.  
  
## Syntax  
  
```  
  
public final int getQueryTimeout()  
```  
  
## Return Value  
 An **int** that indicates the number of seconds that the JDBC driver will wait, or 0 if there is no limit.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getQueryTimeout method is specified by the getQueryTimeout method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
