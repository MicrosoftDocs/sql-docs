---
title: "updateString Method (int, java.lang.String)"
description: "updateString Method (int, java.lang.String)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateString (int, java.lang.String)"
apitype: "Assembly"
---
# updateString Method (int, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **String** value given the column index.  
  
## Syntax  
  
```  
  
public void updateString(int index,  
                         java.lang.String x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A **String** object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateString method is specified by the updateString method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateString Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatestring-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
