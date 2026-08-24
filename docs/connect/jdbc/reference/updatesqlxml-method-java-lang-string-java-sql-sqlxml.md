---
title: "updateSQLXML Method (java.lang.String, java.sql.SQLXML)"
description: "updateSQLXML Method (java.lang.String, java.sql.SQLXML)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateSQLXML Method (java.lang.String, java.sql.SQLXML)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a java.sql.SQLXML value.  
  
## Syntax  
  
```  
  
public void updateSQLXML(java.lang.String columnLabel,  
                         java.sql.SQLXML xmlObject)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that indicates the column label.  
  
 *xmlObject*  
  
 A SQLXML object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateSQLXML method is specified by the updateSQLXML method in the java.sql.ResultSet interface.  
  
## Related content

- [updateSQLXML Method (SQLServerResultSet)](updatesqlxml-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
