---
title: "updateDate Method (int, java.sql.Date)"
description: "updateDate Method (int, java.sql.Date)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateDate (int, java.sql.Date)"
apitype: "Assembly"
---
# updateDate Method (int, java.sql.Date)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a date value given the column index.  
  
## Syntax  
  
```  
  
public void updateDate(int index,  
                       java.sql.Date x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A date value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateDate method is specified by the updateDate method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateDate Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatedate-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
