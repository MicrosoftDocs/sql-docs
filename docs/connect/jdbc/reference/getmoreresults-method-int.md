---
title: "getMoreResults Method (int)"
description: "getMoreResults Method (int)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.getMoreResults (int)"
apitype: "Assembly"
---
# getMoreResults Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Moves to the next result of this [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) object and deals with any currently open result set objects according to the instructions specified by the given mode.  
  
## Syntax  
  
```  
  
public final boolean getMoreResults(int mode)  
```  
  
#### Parameters  
 *mode*  
  
 An **int** that indicates how to handle currently open result set objects. Must be one of the following constants:  
  
 CLOSE_CURRENT_RESULT  
  
 KEEP_CURRENT_RESULT (not supported by the JDBC driver)  
  
 CLOSE_ALL_RESULTS  
  
## Return Value  
 **true** if the returned result is a result set. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getMoreResults method is specified by the getMoreResults method in the java.sql.Statement interface.  
  
 If the getMoreResults method is called before results are retrieved, it behaves as specified by the *mode* argument and moves to the next result.  
  
> [!NOTE]  
>  The JDBC driver does not support using the KEEP_CURRENT_RESULT constant. If it is used, an exception will be thrown.  
  
## See Also  
 [getMoreResults Method &#40;SQLServerStatement&#41;](../../../connect/jdbc/reference/getmoreresults-method-sqlserverstatement.md)   
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
