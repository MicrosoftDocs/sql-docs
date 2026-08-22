---
title: "isWrapperFor Method (SQLServerStatement)"
description: "isWrapperFor Method (SQLServerStatement)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# isWrapperFor Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether this statement object is a wrapper for the specified interface.  
  
## Syntax  
  
```  
  
public boolean isWrapperFor(Class iface)  
```  
  
#### Parameters  
 *iface*  
  
 A **class** defining an interface.  
  
## Return Value  
 **true** if this object implements the interface or wraps an object that implements the interface. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 The [isWrapperFor](#iswrapperfor-method-sqlserverstatement) method and the [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverstatement.md) method are defined by the java.sql.Wrapper interface, which is introduced in JDBC 4.0.  
  
 If this method returns true, calling [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverstatement.md) with the same argument will succeed.  
  
 For an example code, see [Updating Large Data Sample](../../../connect/jdbc/updating-large-data-sample.md).  
  
 For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## Related content

- [unwrap Method (SQLServerStatement)](unwrap-method-sqlserverstatement.md)
- [SQLServerStatement Members](sqlserverstatement-members.md)
- [SQLServerStatement Class](sqlserverstatement-class.md)
