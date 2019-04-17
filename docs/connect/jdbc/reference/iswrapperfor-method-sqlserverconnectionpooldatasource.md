---
title: "isWrapperFor Method (SQLServerConnectionPoolDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 09ed10eb-6e46-437b-a7c0-3c55574aad38
author: MightyPen
ms.author: genemi
manager: craigg
---
# isWrapperFor Method (SQLServerConnectionPoolDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Indicates whether this object is a wrapper for the specified interface.  
  
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
 The [isWrapperFor](../../../connect/jdbc/reference/iswrapperfor-method-sqlserverxadatasource.md) method and the [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverconnectionpooldatasource.md) method are defined by the java.sql.Wrapper interface, which is introduced in the JDBC 4.0 Spec.  
  
 If this method returns true, calling [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverconnectionpooldatasource.md) with the same argument will succeed.  
  
 For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## See Also  
 [unwrap Method &#40;SQLServerConnectionPoolDataSource&#41;](../../../connect/jdbc/reference/unwrap-method-sqlserverconnectionpooldatasource.md)   
 [SQLServerConnectionPoolDataSource Methods](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-methods.md)   
 [SQLServerConnectionPoolDataSource Members](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-members.md)   
 [SQLServerConnectionPoolDataSource Class](../../../connect/jdbc/reference/sqlserverconnectionpooldatasource-class.md)  
  
  
