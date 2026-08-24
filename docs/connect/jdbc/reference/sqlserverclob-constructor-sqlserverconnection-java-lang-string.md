---
title: "SQLServerClob Constructor (SQLServerConnection, java.lang.String)"
description: "SQLServerClob Constructor (SQLServerConnection, java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.SQLServerClob (java.lang.String)"
apitype: "Assembly"
---
# SQLServerClob Constructor (SQLServerConnection, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Initializes a new instance of the [SQLServerClob](../../../connect/jdbc/reference/sqlserverclob-class.md) class when given a [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object and a string of data.  
  
> [!NOTE]  
>  This method has been deprecated in JDBC Driver version 2.0. Instead, use the [createClob](../../../connect/jdbc/reference/createclob-method-sqlserverconnection.md) method of the [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) class.  
  
## Syntax  
  
```  
  
public SQLServerClob(SQLServerConnection connection,  
                     java.lang.String data)  
```  
  
#### Parameters  
 *connection*  
  
 A SQLServerConnection object.  
  
 *data*  
  
 The CLOB data.  
  
## Related content

- [SQLServerClob Constructors](sqlserverclob-constructors.md)
- [SQLServerClob Members](sqlserverclob-members.md)
- [SQLServerClob Class](sqlserverclob-class.md)
