---
title: "createStatement Method (int, int)"
description: "createStatement Method (int, int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: 03/27/2026
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.createStatement (int, int)"
apitype: "Assembly"
---
# createStatement Method (int, int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object that generates [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) objects with the given type and concurrency.  
  
## Syntax  
  
```  
  
public java.sql.Statement createStatement(int resultSetType,  
                                          int resultSetConcurrency)  
```  
  
#### Parameters
 *resultSetType*

 The **int** value representing the result set type. Use one of the following `java.sql.ResultSet` constants:

 | Constant | Description |
 |---|---|
 | `ResultSet.TYPE_FORWARD_ONLY` | Cursor moves forward only. Best performance for sequential reads. |
 | `ResultSet.TYPE_SCROLL_INSENSITIVE` | Scrollable result set that doesn't reflect changes made to the underlying data after the result set is created. |
 | `ResultSet.TYPE_SCROLL_SENSITIVE` | Scrollable result set that reflects changes made to the underlying data. |

 *resultSetConcurrency*

 The **int** value representing the result set concurrency type. Use one of the following `java.sql.ResultSet` constants:

 | Constant | Description |
 |---|---|
 | `ResultSet.CONCUR_READ_ONLY` | The result set can't be updated. |
 | `ResultSet.CONCUR_UPDATABLE` | The result set can be updated by using positioned updates and deletes. |  
  
## Return value  
 The Statement object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This createStatement method is specified by the createStatement method in the java.sql.Connection interface.  
  
## Related content

- [createStatement Method (SQLServerConnection)](createstatement-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
