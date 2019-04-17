---
title: "getNCharacterStream Method (int) (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: f1cfa4e4-3e1f-4504-b0de-cc626d653661
author: MightyPen
ms.author: genemi
manager: craigg
---
# getNCharacterStream Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of a designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a Reader object.  
  
## Syntax  
  
```  
  
public java.io.Reader getNCharacterStream(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A Reader object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getNCharacterStream method is specified by the getNCharacterStream method in the java.sql.ResultSet interface.  
  
 This method can be used to retrieve the value of an **nvarchar**, **nchar**, **nvarchar(max)**, **ntext**, or **xml** column in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object. If you try to use this method to retrieve values of other data types, an exception will be thrown.  
  
## See Also  
 [getNCharacterStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getncharacterstream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)  
  
  
