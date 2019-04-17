---
title: "getBoolean Method (int) (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getBoolean (int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 50fcc0c3-36a1-47b2-b18c-7aa2ac9b27d3
author: MightyPen
ms.author: genemi
manager: craigg
---
# getBoolean Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a **boolean** in the Java programming language.  
  
## Syntax  
  
```  
  
public boolean getBoolean(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **boolean** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBoolean method is specified by the getBoolean method in the java.sql.ResultSet interface.  
  
 This method is supported only on number and character data types. It converts values "1", 1, and "**true**" to **true**, and values "0", 0, and "**false**" to **false**. For all other values the behavior is undefined.  
  
## See Also  
 [getBoolean Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getboolean-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
