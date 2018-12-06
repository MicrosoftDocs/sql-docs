---
title: "getUnicodeStream Method (int) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerResultSet.getUnicodeStream (int)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 0de79b65-a25e-4028-9cc2-7ac02340115b
author: MightyPen
ms.author: genemi
manager: craigg
---
# getUnicodeStream Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column index in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a stream of Unicode characters.  
  
> [!NOTE]  
>  This method has been deprecated from the JDBC specification, and calling it will cause a "not implemented" exception to be thrown. Instead, you should use the [getCharacterStream](../../../connect/jdbc/reference/getcharacterstream-method-sqlserverresultset.md) method.  
  
## Syntax  
  
```  
  
public java.io.InputStream getUnicodeStream(int columnIndex)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getUnicodeString method is specified by the getUnicodeString method in the java.sql.ResultSet interface.  
  
## See Also  
 [getUnicodeStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/getunicodestream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
