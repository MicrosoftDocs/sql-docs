---
title: "execute Method (java.lang.String, int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerStatement.execute (java.lang.String.int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: c8ea589e-5736-412d-9cd1-79bc4964f847
caps.latest.revision: 12
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# execute Method (java.lang.String, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Runs the given SQL statement, which can return multiple results, and signals to [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] that any auto-generated keys should be made available for retrieval.  
  
## Syntax  
  
```  
  
public final boolean execute(java.lang.String sql,  
                             int flag)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains an SQL statement.  
  
 *flag*  
  
 An **int** value that indicates if auto-generated keys should be made available. It must be one of the following constants:  
  
 RETURN_GENERATED_KEYS  
  
 NO_GENERATED_KEYS  
  
## Return Value  
 **true** if the first result is a result set. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This execute method is specified by the execute method in the java.sql.Statement interface.  
  
## See Also  
 [execute Method &#40;SQLServerStatement&#41;](../../../connect/jdbc/reference/execute-method-sqlserverstatement.md)   
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  