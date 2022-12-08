---
title: "getGeneratedKeys Method (SQLServerStatement)"
description: "getGeneratedKeys Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getGeneratedKeys"
apitype: "Assembly"
---
# getGeneratedKeys Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves any auto-generated keys that are created as a result of running this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
## Syntax  
  
```  
  
public final java.sql.ResultSet getGeneratedKeys()  
```  
  
## Return Value  
 A ResultSet object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getGeneratedKeys method is specified by the getGeneratedKeys method in the java.sql.Statement interface.  
  
 For more information about how to use this method, see [Using Auto Generated Keys](../../../connect/jdbc/using-auto-generated-keys.md).  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
