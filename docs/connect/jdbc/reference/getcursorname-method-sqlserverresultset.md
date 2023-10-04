---
title: "getCursorName Method (SQLServerResultSet)"
description: "getCursorName Method (SQLServerResultSet)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.getCursorName"
apitype: "Assembly"
---
# getCursorName Method (SQLServerResultSet)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the name of the SQL cursor that is used by this [SQLServerResultSet](../../../connect/jdbc/reference/sqlserverresultset-class.md) object.  
  
> [!NOTE]  
>  This method is not currently supported by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. If called, an exception will be thrown.  
  
## Syntax  
  
```  
  
public java.lang.String getCursorName()  
```  
  
## Return Value  
 A **String** that contains the cursor name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getCursorName method is specified by the getCursorName method in the java.sql.ResultSet interface.  
  
## See Also  
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
