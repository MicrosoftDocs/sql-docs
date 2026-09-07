---
title: "updateCharacterStream Method (int, java.io.Reader)"
description: "updateCharacterStream Method (int, java.io.Reader)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateCharacterStream Method (int, java.io.Reader)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a character stream value.  
  
## Syntax  
  
```  
  
public void updateCharacterStream(int columnIndex,  
                                  java.io.Reader x)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 A Reader object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateCharacterStream method is specified by the updateCharacterStream method in the java.sql.ResultSet interface.  
  
 This method passes Unicode characters from a Reader object to selected text and binary columns. This includes all text columns and **binary**, **varbinary**, **varbinary(max)**, **image**, and **xml** columns, but not **udt** columns.  
  
 Using this method for the **image**, **text**, and **ntext**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types might affect performance.  
  
## Related content

- [updateCharacterStream Method (SQLServerResultSet)](updatecharacterstream-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
