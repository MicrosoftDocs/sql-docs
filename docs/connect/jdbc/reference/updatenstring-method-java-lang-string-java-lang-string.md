---
title: "updateNString Method (java.lang.String, java.lang.String)"
description: "updateNString Method (java.lang.String, java.lang.String)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateNString Method (java.lang.String, java.lang.String)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a **String** value using the specified column label.  
  
## Syntax  
  
```  
  
public void updateNString(java.lang.String columnLabel,  
                          java.lang.String nString)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that contains the column label.  
  
 *nString*  
  
 A **String** object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateNString method is specified by the updateNString method in the java.sql.ResultSet interface.  
  
 This method passes Java **String** to selected **nchar**, **nvarchar(max)**, **ntext**, and **xml** columns. Using this method on other data type columns will throw an exception.  
  
## Related content

- [updateNString Method (SQLServerResultSet)](updatenstring-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
