---
title: "updateByte Method (java.lang.String, byte)"
description: "updateByte Method (java.lang.String, byte)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateByte (java.lang.String, byte)"
apitype: "Assembly"
---
# updateByte Method (java.lang.String, byte)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **byte** value given the column name.  
  
## Syntax  
  
```  
  
public void updateByte(java.lang.String columnName,  
                       byte x)  
```  
  
#### Parameters  
 *columnName*  
  
 A **String** that contains the column name.  
  
 *x*  
  
 A **byte** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateByte method is specified by the updateByte method in the java.sql.ResultSet interface.  
  
## Related content

- [updateByte Method (SQLServerResultSet)](updatebyte-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
