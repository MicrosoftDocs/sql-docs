---
title: "getUnicodeStream Method (java.lang.String)"
description: "getUnicodeStream Method (java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getUnicodeStream (java.lang.String)"
apitype: "Assembly"
---
# getUnicodeStream Method (java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the value of the designated column name in the current row of this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object as a stream of Unicode characters.  
  
> [!NOTE]  
>  This method has been deprecated from the JDBC specification, and calling it will cause a "not implemented" exception to be thrown. Instead, you should use the [getCharacterStream](../../../connect/jdbc/reference/getcharacterstream-method-sqlserverresultset.md) method.  
  
## Syntax  
  
```  
  
public java.io.InputStream getUnicodeStream(java.lang.String columnName)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
## Return Value  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getUnicodeString method is specified by the getUnicodeString method in the java.sql.ResultSet interface.  
  
## Related content

- [getUnicodeStream Method (SQLServerResultSet)](getunicodestream-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
