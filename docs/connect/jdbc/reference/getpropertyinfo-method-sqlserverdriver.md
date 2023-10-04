---
title: "getPropertyInfo Method (SQLServerDriver)"
description: "getPropertyInfo Method (SQLServerDriver)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDriver.getPropertyInfo"
apitype: "Assembly"
---
# getPropertyInfo Method (SQLServerDriver)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Used to discover the properties needed to connect to a database.  
  
## Syntax  
  
```  
  
public java.sql.DriverPropertyInfo[] getPropertyInfo(java.lang.String Url,  
                                                     java.util.Properties Info)  
```  
  
#### Parameters  
 *Url*  
  
 A **String** value that contains the URL that is used to connect to the database.  
  
 *Info*  
  
 A list of property value pairs, null on first use.  
  
## Return Value  
 An array of DriverPropertyInfo objects.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getPropertyInfo method is specified by the getPropertyInfo method in the java.sql.Driver interface.  
  
## See Also  
 [SQLServerDriver Methods](../../../connect/jdbc/reference/sqlserverdriver-methods.md)   
 [SQLServerDriver Members](../../../connect/jdbc/reference/sqlserverdriver-members.md)   
 [SQLServerDriver Class](../../../connect/jdbc/reference/sqlserverdriver-class.md)  
  
  
