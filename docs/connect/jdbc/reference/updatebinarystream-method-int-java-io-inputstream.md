---
title: "updateBinaryStream Method (int, java.io.InputStream)"
description: "updateBinaryStream Method (int, java.io.InputStream)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateBinaryStream Method (int, java.io.InputStream)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a binary stream value.  
  
## Syntax  
  
```  
  
public void updateBinaryStream(int columnIndex,  
                               java.io.InputStream x)  
```  
  
#### Parameters  
 *columnIndex*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBinaryStream method is specified by the updateBinaryStream method in the java.sql.ResultSet interface.  
  
 Using this method for the **image**, **text**, and **ntext**[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data types might impact the performance.  
  
 This method passes bytes from an InputStream object to selected [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] binary columns such as binary, varbinary, varbinary(max), image, xml, and udt. Updating character columns is not supported with this method. To update character columns with an InputStream, use the [updateAsciiStream](../../../connect/jdbc/reference/updateasciistream-method-sqlserverresultset.md) method.  
  
## Related content

- [updateBinaryStream Method (SQLServerResultSet)](updatebinarystream-method-sqlserverresultset.md)
- [SQLServerResultSet Members](sqlserverresultset-members.md)
- [SQLServerResultSet Class](sqlserverresultset-class.md)
