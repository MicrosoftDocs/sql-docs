---
title: "createStatement Method ()"
description: "createStatement Method ()"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.createStatement ()"
apitype: "Assembly"
---
# createStatement Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Creates a [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object for sending SQL statements to the database.  
  
## Syntax  
  
```  
  
public java.sql.Statement createStatement()  
```  
  
## Return Value  
 The Statement object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This createStatement method is specified by the createStatement method in the java.sql.Connection interface.  
  
## Related content

- [createStatement Method (SQLServerConnection)](createstatement-method-sqlserverconnection.md)
- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
