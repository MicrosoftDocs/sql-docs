---
title: "unwrap Method (SQLServerPreparedStatement)"
description: "unwrap Method (SQLServerPreparedStatement)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# unwrap Method (SQLServerPreparedStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns an object that implements the specified interface to allow access to the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]-specific methods.  
  
## Syntax  
  
```  
  
public <T> T unwrap(Class<T> iface)  
```  
  
#### Parameters  
 *iface*  
  
 A class of type **T** defining an interface.  
  
## Return Value  
 An object that implements the specified interface.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 The [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverpreparedstatement.md) method is defined by the java.sql.Wrapper interface, which is introduced in the JDBC 4.0 Spec.  
  
 Applications might need to access extensions to the JDBC API that are specific to the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. The unwrap method supports unwrapping to public classes that this object extends, if the classes expose vendor extensions.  
  
 When this method is called, the object unwraps to the following classes: [SQLServerStatement](../../../connect/jdbc/reference/sqlserverstatement-class.md) and [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md).  
  
 For example code, see [unwrap Method &#40;SQLServerCallableStatement&#41;](../../../connect/jdbc/reference/unwrap-method-sqlservercallablestatement.md).  
  
 For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## See Also  
 [isWrapperFor Method &#40;SQLServerPreparedStatement&#41;](../../../connect/jdbc/reference/iswrapperfor-method-sqlserverpreparedstatement.md)   
 [SQLServerPreparedStatement Members](../../../connect/jdbc/reference/sqlserverpreparedstatement-members.md)   
 [SQLServerPreparedStatement Class](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
  
