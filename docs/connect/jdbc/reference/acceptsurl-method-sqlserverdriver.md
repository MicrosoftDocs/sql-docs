---
title: "acceptsURL Method (SQLServerDriver)"
description: "acceptsURL Method (SQLServerDriver)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDriver.acceptsURL"
apitype: "Assembly"
---
# acceptsURL Method (SQLServerDriver)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Checks if the given URL is valid.  
  
## Syntax  
  
```  
  
public boolean acceptsURL(java.lang.String url)  
```  
  
#### Parameters  
 *url*  
  
 A **String** value containing the URL used to connect to the database.  
  
## Return Value  
 **true** if the given URL is valid. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This acceptsURL method is specified by the acceptsURL method in the java.sql.Driver interface.  
  
## Related content

- [SQLServerDriver Methods](sqlserverdriver-methods.md)
- [SQLServerDriver Members](sqlserverdriver-members.md)
- [SQLServerDriver Class](sqlserverdriver-class.md)
