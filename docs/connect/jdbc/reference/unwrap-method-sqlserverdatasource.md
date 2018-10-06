---
title: "unwrap Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: eb8abe29-f3ec-4752-a590-1d5dc3e48f08
author: MightyPen
ms.author: genemi
manager: craigg
---
# unwrap Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns an object that implements the specified interface to allow access to the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]-specific methods.  
  
## Syntax  
  
```  
  
public <T> T unwrap(Class<T> iface)  
```  
  
#### Parameters  
 *iface*  
  
 A class of type T defining an interface.  
  
## Return Value  
 An object that implements the specified interface.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 The [unwrap](../../../connect/jdbc/reference/unwrap-method-sqlserverdatasource.md) method is defined by the java.sql.Wrapper interface, which is introduced in the JDBC 4.0 Spec.  
  
 Applications might need to access extensions to the JDBC API that are specific to the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]. The unwrap method supports unwrapping to public classes that this object extends if the classes expose vendor extensions.  
  
 When this method is called, the object unwraps to the [SQLServerDataSource](../../../connect/jdbc/reference/sqlserverdatasource-class.md) class.  
  
 For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
## See Also  
 [isWrapperFor Method &#40;SQLServerDataSource&#41;](../../../connect/jdbc/reference/iswrapperfor-method-sqlserverdatasource.md)   
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
