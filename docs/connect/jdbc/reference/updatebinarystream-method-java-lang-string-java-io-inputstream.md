---
title: "updateBinaryStream Method (java.io.InputStream)"
description: "updateBinaryStream Method (java.lang.String, java.io.InputStream)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateBinaryStream Method (java.lang.String, java.io.InputStream)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with a binary stream value.  
  
## Syntax  
  
```  
  
public void updateBinaryStream(java.lang.String columnLabel,  
                               java.io.InputStream x)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that contains the column label.  
  
 *x*  
  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBinaryStream method is specified by the updateBinaryStream method in the java.sql.ResultSet interface.  
  
 Using this method for the **image**, **text**, and **ntext** SQL Server data types might impact the performance.  
  
 This method passes bytes from an InputStream object to selected [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] binary columns such as binary, varbinary, varbinary(max), image, xml, and udt. Updating character columns is not supported with this method. To update character columns with an InputStream, use the [updateAsciiStream](../../../connect/jdbc/reference/updateasciistream-method-sqlserverresultset.md) method.  
  
## See Also  
 [updateBinaryStream Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatebinarystream-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
