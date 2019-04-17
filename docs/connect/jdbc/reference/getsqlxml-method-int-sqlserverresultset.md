---
title: "getSQLXML Method (int) (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: faa35676-573d-48d5-afd9-850134735728
author: MightyPen
ms.author: genemi
manager: craigg
---
# getSQLXML Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of a designated column in the current row of the [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a SQLXML object.  
  
## Syntax  
  
```  
  
public final java.sql.SQLXML getSQLXML(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 ASQLXMLobject.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getSQLXML method is specified by the getSQLXML method in the java.sql.ResultSet interface.  
  
## See Also  
 [getSQLXML Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getsqlxml-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)  
  
  
