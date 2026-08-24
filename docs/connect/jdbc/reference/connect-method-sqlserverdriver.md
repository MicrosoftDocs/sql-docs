---
title: "connect Method (SQLServerDriver)"
description: "connect Method (SQLServerDriver)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDriver.connect"
apitype: "Assembly"
---
# connect Method (SQLServerDriver)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Makes a connection to the database.  
  
## Syntax  
  
```  
  
public java.sql.Connection connect(java.lang.String Url,  
                                   java.util.Properties suppliedProperties)  
```  
  
#### Parameters  
 *Url*  
  
 A **String** value that contains the URL that is used to connect to the database.  
  
 *suppliedProperties*  
  
 A set of string value pairs used as connection arguments.  
  
## Return Value  
 A Connection object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This connect method is specified by the connect method in the java.sql.Driver interface.  
  
## Related content

- [SQLServerDriver Methods](sqlserverdriver-methods.md)
- [SQLServerDriver Members](sqlserverdriver-members.md)
- [SQLServerDriver Class](sqlserverdriver-class.md)
