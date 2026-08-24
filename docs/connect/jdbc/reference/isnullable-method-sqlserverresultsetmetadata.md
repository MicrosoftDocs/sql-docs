---
title: "isNullable Method (SQLServerResultSetMetaData)"
description: "isNullable Method (SQLServerResultSetMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.isNullable"
apitype: "Assembly"
---
# isNullable Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates the nullability of values in the designated column.  
  
## Syntax  
  
```  
  
public int isNullable(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 **true** if the column can be null. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isNullable method is specified by the isNullable method in the java.sql.ResultSetMetaData interface.  
  
## Related content

- [SQLServerResultSetMetaData Methods](sqlserverresultsetmetadata-methods.md)
- [SQLServerResultSetMetaData Members](sqlserverresultsetmetadata-members.md)
- [SQLServerResultSetMetaData Class](sqlserverresultsetmetadata-class.md)
