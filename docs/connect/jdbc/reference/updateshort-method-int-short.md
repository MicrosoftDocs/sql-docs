---
title: "updateShort Method (int, short)"
description: "updateShort Method (int, short)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateShort (int, short)"
apitype: "Assembly"
---
# updateShort Method (int, short)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **short** value given the column index.  
  
## Syntax  
  
```  
  
public void updateShort(int index,  
                        short x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A **short** value.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateShort method is specified by the updateShort method in the java.sql.ResultSet interface.  
  
## Related content

- [updateShort Method (SQLServerResultSet)](updateshort-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
