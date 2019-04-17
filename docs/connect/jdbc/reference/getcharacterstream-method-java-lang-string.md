---
title: "getCharacterStream Method (java.lang.String) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getCharacterStream (java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: cdddc572-05c1-480d-b3e5-28270001575c
author: MightyPen
ms.author: genemi
manager: craigg
---
# getCharacterStream Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a java.io.Reader object.  
  
## Syntax  
  
```  
  
public java.io.Reader getCharacterStream(java.lang.String columnName)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
## Return Value  
 A Reader object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCharacterStream method is specified by the getCharacterStream method in the java.sql.ResultSet interface.  
  
 This method will read only [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Unicode character data types such as nchar, nvarchar, nvarchar(max), and ntext. All other data types, including the ASCII character types, will cause an exception to be thrown. To read the ASCII data types, use the [getAsciiStream](../../../connect/jdbc/reference/getasciistream-method-sqlserverresultset.md) method.  
  
## See Also  
 [getCharacterStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getcharacterstream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
