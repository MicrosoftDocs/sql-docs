---
title: "updateBoolean Method (int, boolean)"
description: "updateBoolean Method (int, boolean)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateBoolean (int, boolean)"
apitype: "Assembly"
---
# updateBoolean Method (int, boolean)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **boolean** value given the column index.  
  
## Syntax  
  
```  
  
public void updateBoolean(int index,  
                          boolean x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A **boolean** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBoolean method is specified by the updateBoolean method in the java.sql.ResultSet interface.  
  
## Related content

- [updateBoolean Method (SQLServerResultSet)](updateboolean-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
