---
title: "executeBatch Method (SQLServerPreparedStatement)"
description: "executeBatch Method (SQLServerPreparedStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerPreparedStatement.executeBatch"
apitype: "Assembly"
---
# executeBatch Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Submits a batch of commands to the database to be run. If all commands run successfully, returns an array of update counts.  
  
## Syntax  
  
```  
  
public int[] executeBatch()  
```  
  
## Return Value  
 An array of ints that contains the update counts.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
 java.sql.BatchUpdateException  
  
## Remarks  
 This executeBatch method is specified by the executeBatch method in the java.sql.Statement interface.  
    
 This method overrides [SQLServerStatement.executeBatch](../../../connect/jdbc/reference/executebatch-method-sqlserverstatement.md).  
  
## Related content

- [SQLServerPreparedStatement Members](sqlserverpreparedstatement-members.md)
- [SQLServerPreparedStatement Class](sqlserverpreparedstatement-class.md)
