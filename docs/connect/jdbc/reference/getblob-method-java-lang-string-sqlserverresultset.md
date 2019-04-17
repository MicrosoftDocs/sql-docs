---
title: "getBlob Method (java.lang.String) (SQLServerResultSet) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getBlob (java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 9f730d45-b54a-4961-950e-f4447f7225e1
author: MightyPen
ms.author: genemi
manager: craigg
---
# getBlob Method (java.lang.String) (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a Blob object in the Java programming language.  
  
## Syntax  
  
```  
  
public java.sql.Blob getBlob(java.lang.String colName)  
```  
  
#### Parameters  
 *colName*  
  
 A **String** that contains the column name.  
  
## Return Value  
 A Blob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getBlob method is specified by the getBlob method in the java.sql.ResultSet interface.  
  
## See Also  
 [getBlob Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getblob-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
