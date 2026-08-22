---
title: "isDefinitelyWritable Method (SQLServerResultSetMetaData)"
description: "isDefinitelyWritable Method (SQLServerResultSetMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSetMetaData.isDefinitelyWritable"
apitype: "Assembly"
---
# isDefinitelyWritable Method (SQLServerResultSetMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether a write on the designated column will definitely succeed.  
  
## Syntax  
  
```  
  
public boolean isDefinitelyWritable(int column)  
```  
  
#### Parameters  
 *column*  
  
 An **int** that indicates the column index.  
  
## Return Value  
 **true** if the column write will definitely succeed. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This isDefinitelyWritable method is specified by the isDefinitelyWritable method in the java.sql.ResultSetMetaData interface.  
  
> [!NOTE]  
>  When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method will always return false.  
  
## Related content

- [SQLServerResultSetMetaData Methods](sqlserverresultsetmetadata-methods.md)
- [SQLServerResultSetMetaData Members](sqlserverresultsetmetadata-members.md)
- [SQLServerResultSetMetaData Class](sqlserverresultsetmetadata-class.md)
