---
title: "executeUpdate Method (java.lang.String) (SQLServerStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerStatement.executeUpdate (java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 85e7c3a2-f2da-49bf-9d90-5fd246fd60e1
author: MightyPen
ms.author: genemi
manager: craigg
---
# executeUpdate Method (java.lang.String) (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the given SQL statement, which can be an INSERT, UPDATE, or DELETE statement; or an SQL statement that returns nothing, such as an SQL DDL statement. Beginning in [!INCLUDE[msCoName](../../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0, executeUpdate will return the correct number of rows updated in a MERGE operation.  
  
## Syntax  
  
```  
  
public int executeUpdate(java.lang.String sql)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains the SQL statement.  
  
## Return Value  
 An **int** that indicates the number of rows affected, or 0 if using a DDL statement.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This executeUpdate method is specified by the executeUpdate method in the java.sql.Statement interface.  
  
 If executing a stored procedure results in an update count that is greater than one, or that generates more than one result set, use the [execute](../../../connect/jdbc/reference/execute-method-sqlserverstatement.md) method to execute the stored procedure.  
  
## See Also  
 [executeUpdate Method &#40;SQLServerStatement&#41;](../../../connect/jdbc/reference/executeupdate-method-sqlserverstatement.md)   
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
