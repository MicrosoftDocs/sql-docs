---
title: "setCursorName Method (SQLServerStatement)"
description: "setCursorName Method (SQLServerStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.setCursorName"
apitype: "Assembly"
---
# setCursorName Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the SQL cursor name to the given String, which will be used by subsequent execute methods.  
  
> [!NOTE]  
>  This method is not currently supported by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. Calling this method has no effect.  
  
## Syntax  
  
```  
  
public final void setCursorName(java.lang.String name)  
```  
  
#### Parameters  
 *name*  
  
 A **String** that contains the cursor name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setCursorName method is specified by the setCursorName method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
