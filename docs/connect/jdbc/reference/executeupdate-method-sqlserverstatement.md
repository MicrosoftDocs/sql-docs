---
title: "executeUpdate Method (SQLServerStatement)"
description: "executeUpdate Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.executeUpdate"
apitype: "Assembly"
---
# executeUpdate Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the given SQL statement, which can be an INSERT, UPDATE, or DELETE statement; or an SQL statement that returns nothing, such as an SQL DDL statement. Beginning in [!INCLUDE[msCoName](../../../includes/msconame-md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, executeUpdate will return the correct number of rows updated in a MERGE operation.  
  
## Overload List  
  
|Name|Description|  
|----------|-----------------|  
|[executeUpdate (java.lang.String)](../../../connect/jdbc/reference/executeupdate-method-java-lang-string-sqlserverstatement.md)|Runs the given SQL statement, which can be an INSERT, UPDATE, DELETE, or MERGE statement; or an SQL statement that returns nothing, such as an SQL DDL statement.|  
|[executeUpdate (java.lang.String, int)](../../../connect/jdbc/reference/executeupdate-method-java-lang-string-int.md)|Runs the given SQL statement and signals the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with the given flag about whether the auto-generated keys produced by this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object should be made available for retrieval.|  
|[executeUpdate (java.lang.String, int&#91;&#93;)](../../../connect/jdbc/reference/executeupdate-method-java-lang-string.md)|Runs the given SQL statement and signals the JDBC driver that the auto-generated keys that are indicated in the given array should be made available for retrieval.|  
|[executeUpdate (java.lang.String, java.lang.String&#91;&#93;)](../../../connect/jdbc/reference/executeupdate-method-java-lang-string-java-lang-string.md)|Runs the given SQL statement and signals the JDBC driver that the auto-generated keys that are indicated in the given array should be made available for retrieval.|  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
