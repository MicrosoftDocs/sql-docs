---
title: "getFloat Method (int) (SQLServerResultSet)"
description: "getFloat Method (int) (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getFloat (int)"
apitype: "Assembly"
---
# getFloat Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **float** in the Java programming language.  
  
## Syntax  
  
```  
  
public float getFloat(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **float** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getFloat method is specified by the getFloat method in the java.sql.ResultSet interface.  
  
 This method returns all number-based types with Java **float** fidelity.  
  
## See Also  
 [getFloat Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getfloat-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
