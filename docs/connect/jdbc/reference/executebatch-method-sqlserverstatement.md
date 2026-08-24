---
title: "executeBatch Method (SQLServerStatement)"
description: "executeBatch Method (SQLServerStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.executeBatch"
apitype: "Assembly"
---
# executeBatch Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Submits a batch of commands to the database to be run. If all commands run successfully, returns an array of update counts.  
  
## Syntax  
  
```  
  
public int[] executeBatch()  
```  
  
## Return Value  
 An array of **int** that contain the update counts.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
 java.sql.BatchUpdateException  
  
## Remarks  
 This executeBatch method is specified by the executeBatch method in the java.sql.Statement interface.  
  
 After submitting commands to the database, this method clears any command in the batch.  
  
## Related content

- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
