---
title: "getMoreResults Method ()"
description: "getMoreResults Method ()"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getMoreResults ()"
apitype: "Assembly"
---
# getMoreResults Method ()
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves to the next result of this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object.  
  
## Syntax  
  
```  
  
public final boolean getMoreResults()  
```  
  
## Return Value  
 **true** if the returned result is a result set. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMoreResults method is specified by the getMoreResults method in the java.sql.Statement interface.  
  
 Calling the getMoreResults method implicitly closes any currently open result set objects that are obtained with the [getResultSet](../../../connect/jdbc/reference/getresultset-method-sqlserverstatement.md) method.  
  
## See Also  
 [getMoreResults Method &#40;SQLServerStatement&#41;](../../../connect/jdbc/reference/getmoreresults-method-sqlserverstatement.md)   
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
