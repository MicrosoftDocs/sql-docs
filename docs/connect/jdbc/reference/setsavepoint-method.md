---
title: "setSavepoint Method ()"
description: "setSavepoint Method ()"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.setSavepoint ()"
apitype: "Assembly"
---
# setSavepoint Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates an unnamed savepoint in the current transaction, and returns the new [SQLServerSavepoint](../../../connect/jdbc/reference/sqlserversavepoint-class.md) object that represents it.  
  
## Syntax  
  
```  
  
public java.sql.Savepoint setSavepoint()  
```  
  
## Return Value  
 A SavePoint object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setSavePoint method is specified by the setSavePoint method in the java.sql.Connection interface.  
  
## Related content

- [setSavepoint Method (SQLServerConnection)](setsavepoint-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
