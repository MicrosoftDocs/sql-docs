---
title: "setReadOnly Method (SQLServerConnection)"
description: "setReadOnly Method (SQLServerConnection)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.setReadOnly"
apitype: "Assembly"
---
# setReadOnly Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Puts this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object in read-only mode as a hint to the JDBC driver to enable database optimizations.  
  
> [!NOTE]  
>  This method is not supported by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)].  
  
## Syntax  
  
```  
  
public void setReadOnly(boolean readOnly)  
```  
  
#### Parameters  
 *readOnly*  
  
 **true** if the connection is to be read-only. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setReadOnly method is specified by the setReadOnly method in the java.sql.Connection interface.  
  
## Related content

- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
