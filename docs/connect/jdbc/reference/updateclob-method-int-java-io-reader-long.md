---
title: "updateClob Method (int, java.io.Reader, long)"
description: "updateClob Method (int, java.io.Reader, long)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateClob Method (int, java.io.Reader, long)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column using the specified Reader object, which is the specified number of characters long.  
  
## Syntax  
  
```  
  
public void updateClob(int columnIndex,  
                        java.io.Reader reader,  
                        long length)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *reader*  
  
 A Reader object.  
  
 *length*  
  
 The number of characters in the parameter data.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateClob method is specified by the updateClob method in the java.sql.ResultSet interface.  
  
## Related content

- [updateClob Method (SQLServerResultSet)](updateclob-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
