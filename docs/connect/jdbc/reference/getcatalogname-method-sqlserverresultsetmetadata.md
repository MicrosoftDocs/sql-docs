---
title: "getCatalogName Method (SQLServerResultSetMetaData)"
description: "getCatalogName Method (SQLServerResultSetMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.getCatalogName"
apitype: "Assembly"
---
# getCatalogName Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Gets the catalog name for the table that includes the designated column.  
  
## Syntax  
  
```  
  
public java.lang.String getCatalogName(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 A **String** that contains the catalog name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCatalogName method is specified by the getCatalogName method in the java.sql.ResultSetMetaData interface.  
  
## Related content

- [SQLServerResultSetMetaData Methods](sqlserverresultsetmetadata-methods.md)
- [SQLServerResultSetMetaData Members](sqlserverresultsetmetadata-members.md)
- [SQLServerResultSetMetaData Class](sqlserverresultsetmetadata-class.md)
