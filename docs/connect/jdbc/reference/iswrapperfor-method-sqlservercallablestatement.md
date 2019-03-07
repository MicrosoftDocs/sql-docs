---
title: "isWrapperFor Method (SQLServerCallableStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 71156863-3588-453e-b5a5-0573b2c1bebf
author: MightyPen
ms.author: genemi
manager: craigg
---
# isWrapperFor Method (SQLServerCallableStatement)
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
 The [isWrapperFor](../../../connect/jdbc/reference/iswrapperfor-method-sqlservercallablestatement.md) method and the [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlservercallablestatement.md) method are defined by the java.sql.Wrapper interface, which is introduced in JDBC 4.0.  
  
 If this method returns **true**, calling [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlservercallablestatement.md) with the same argument will succeed.  
  
 For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## See Also  
 [unwrap Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/unwrap-method-sqlservercallablestatement.md)   
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
  
