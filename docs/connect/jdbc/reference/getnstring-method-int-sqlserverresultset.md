---
title: "getNString Method (int) (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: c8cc4636-01e9-4dc8-a40c-728337ca08f5
author: MightyPen
ms.author: genemi
manager: craigg
---
# getNString Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a String object.  
  
## Syntax  
  
```  
  
public java.lang.String getNString(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A String object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNString method is specified by the getNString method in the java.sql.SQLServerResultSet interface.  
  
 This method can be used to retrieve the value of an **nvarchar**, **nchar**, **nvarchar(max)**, **ntext**, or **xml** column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object. If you try to use this method to retrieve values of other data types, an exception will be thrown.  
  
## See Also  
 [getNString Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getnstring-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)  
  
  
