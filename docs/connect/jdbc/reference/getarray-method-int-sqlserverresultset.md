---
title: "getArray Method (int) (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getArray (int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 377746c7-8c9c-41f5-8490-ca0dd56fd57a
author: MightyPen
ms.author: genemi
manager: craigg
---
# getArray Method (int) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as an Array object.  
  
## Syntax  
  
```  
  
public java.sql.Array getArray(int i)  
```  
  
#### Parameters  
 *i*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 An Array object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getArray method is specified by the getArray method in the java.sql.ResultSet interface.  
  
## See Also  
 [getArray Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getarray-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
