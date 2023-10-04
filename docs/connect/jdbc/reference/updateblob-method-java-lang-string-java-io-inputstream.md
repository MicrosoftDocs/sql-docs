---
title: "updateBlob Method (java.lang.String, java.io.InputStream)"
description: "updateBlob Method (java.lang.String, java.io.InputStream)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# updateBlob Method (java.lang.String, java.io.InputStream)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column using the specified input stream.  
  
## Syntax  
  
```  
  
public void updateBlob(java.lang.String columnLabel,  
                       java.io.InputStream inputStream)  
```  
  
#### Parameters  
 *columnLabel*  
  
 A **String** that contains the column label.  
  
 *inputStream*  
  
 An InputStream object.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBlob method is specified by the updateBlob method in the java.sql.ResultSet interface.  
  
## See Also  
 [updateBlob Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updateblob-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
