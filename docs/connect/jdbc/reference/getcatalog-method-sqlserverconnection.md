---
title: "getCatalog Method (SQLServerConnection)"
description: "getCatalog Method (SQLServerConnection)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.getCatalog"
apitype: "Assembly"
---
# getCatalog Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the current catalog name of this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object.  
  
## Syntax  
  
```  
  
public java.lang.String getCatalog()  
```  
  
## Return Value  
 A **String** that contains the catalog name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCatalog method is specified by the getCatalog method in the java.sql.Connection interface.  
  
 Returns the current catalog property of the SQLServerConnection object, or null if it is not set. The catalog property is set explicitly with the [setCatalog](../../../connect/jdbc/reference/setcatalog-method-sqlserverconnection.md) method, or is implicitly updated by reading the environment change on TDS for the current catalog.  
  
## Related content

- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
