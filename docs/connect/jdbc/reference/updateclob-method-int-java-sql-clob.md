---
title: "updateClob Method (int, java.sql.Clob)"
description: "updateClob Method (int, java.sql.Clob)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateClob (int, java.sql.Clob)"
apitype: "Assembly"
---
# updateClob Method (int, java.sql.Clob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a java.sql.Clob value given the column index.  
  
## Syntax  
  
```  
  
public void updateClob(int columnIndex,  
                       java.sql.Clob clobValue)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *clobValue*  
  
 A Clob object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateClob method is specified by the updateClob method in the java.sql.ResultSet interface.  
  
## Related content

- [updateClob Method (SQLServerResultSet)](updateclob-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
