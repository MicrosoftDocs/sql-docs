---
title: "acceptsURL Method (SQLServerDriver)"
description: "acceptsURL Method (SQLServerDriver)"
author: David-Engel
ms.author: v-davidengel
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
  
## See Also  
 [SQLServerDriver Methods](../../../connect/jdbc/reference/sqlserverdriver-methods.md)   
 [SQLServerDriver Members](../../../connect/jdbc/reference/sqlserverdriver-members.md)   
 [SQLServerDriver Class](../../../connect/jdbc/reference/sqlserverdriver-class.md)  
  
  
