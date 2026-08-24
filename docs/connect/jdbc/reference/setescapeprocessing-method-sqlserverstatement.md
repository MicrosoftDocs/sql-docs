---
title: "setEscapeProcessing Method (SQLServerStatement)"
description: "setEscapeProcessing Method (SQLServerStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerStatement.setEscapeProcessing"
apitype: "Assembly"
---
# setEscapeProcessing Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the escape processing mode.  
  
> [!NOTE]  
>  Escape processing for [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] is always enabled. Setting this method to false has no effect.  
  
## Syntax  
  
```  
  
public final void setEscapeProcessing(boolean enable)  
```  
  
#### Parameters  
 *enable*  
  
 **true** to enable escape processing. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setEscapeProcessing method is specified by the setEscapeProcessing method in the java.sql.Statement interface.  
  
## Related content

- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
