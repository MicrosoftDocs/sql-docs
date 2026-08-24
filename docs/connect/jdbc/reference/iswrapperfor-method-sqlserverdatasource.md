---
title: "isWrapperFor Method (SQLServerDataSource)"
description: "isWrapperFor Method (SQLServerDataSource)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# isWrapperFor Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether this data source object is a wrapper for the specified interface.  
  
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
 The [isWrapperFor](#iswrapperfor-method-sqlserverdatasource) method and the [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverdatasource.md) method are defined by the java.sql.Wrapper interface, which is introduced in the JDBC 4.0 Spec.  
  
 If this method returns true, calling [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverdatasource.md) with the same argument will succeed.  
  
 For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## Related content

- [unwrap Method (SQLServerDataSource)](unwrap-method-sqlserverdatasource.md)
- [SQLServerDataSource Members](sqlserverdatasource-members.md)
- [SQLServerDataSource Class](sqlserverdatasource-class.md)
