---
title: "isSearchable Method (SQLServerResultSetMetaData)"
description: "isSearchable Method (SQLServerResultSetMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.isSearchable"
apitype: "Assembly"
---
# isSearchable Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether the designated column can be used in a SQL WHERE clause.  
  
## Syntax  
  
```  
  
public boolean isSearchable(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 **true** if the column the column can be used in a WHERE clause. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isSearchable method is specified by the isSearchable method in the java.sql.ResultSetMetaData interface.  
  
## Related content

- [SQLServerResultSetMetaData Methods](sqlserverresultsetmetadata-methods.md)
- [SQLServerResultSetMetaData Members](sqlserverresultsetmetadata-members.md)
- [SQLServerResultSetMetaData Class](sqlserverresultsetmetadata-class.md)
