---
title: "updateNull Method (int)"
description: "updateNull Method (int)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateNull (int)"
apitype: "Assembly"
---
# updateNull Method (int)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a null value given the column index.  
  
## Syntax  
  
```  
  
public void updateNull(int index)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateNull method is specified by the updateNull method in the java.sql.ResultSet interface.  
  
## Related content

- [updateNull Method (SQLServerResultSet)](updatenull-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
