---
title: "executeUpdate Method (java.lang.String) | Microsoft Docs"
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
  - "SQLServerPreparedStatement.executeUpdate (java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 91ecb1cd-001d-4ac9-9ae8-5db05c3c2959
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# executeUpdate Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](..%2FToken%2FDriver_JDBC_Download.md)]

  Runs the given SQL statement, which can be an INSERT, UPDATE, MERGE, or DELETE statement; or an SQL statement that returns nothing, such as an SQL DDL statement.  
  
## Syntax  
  
```  
  
public final int executeUpdate(java.lang.String sql)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** that contains the SQL statement.  
  
## Return Value  
 An **int** that indicates the number of rows affected, or 0 if using a DDL statement.  
  
## Exceptions  
 [SQLServerException](../Topic/SQLServerException%20Class.md)  
  
## Remarks  
 This executeUpdate method is specified by the executeUpdate method in the java.sql.PreparedStatement interface.  
  
 Calling this method will result in an exception since the SQL statement for the SQLServerPreparedStatement object is specified when the object is created.  
  
## See Also  
 [executeUpdate Method &#40;SQLServerPreparedStatement&#41;](../Topic/executeUpdate%20Method%20(SQLServerPreparedStatement).md)   
 [SQLServerPreparedStatement Members](../Topic/SQLServerPreparedStatement%20Members.md)   
 [SQLServerPreparedStatement Class](../Topic/SQLServerPreparedStatement%20Class.md)  
  
  