---
title: "SQLServerCallableStatement Class"
description: "SQLServerCallableStatement Class"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerCallableStatement Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Lets you specify the stored procedure name to call along with input and output parameters. This class also provides the ability to retrieve the return status value with the `? = call( ?, ..)` syntax.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Implements:** [ISQLServerCallableStatement](../../../connect/jdbc/reference/sqlservercallablestatement-class.md)  
  
 **Extends:** [SQLServerPreparedStatement](../../../connect/jdbc/reference/sqlserverpreparedstatement-class.md)  
  
## Syntax  
  
```  
  
public final class SQLServerCallableStatement  
```  
  
## Remarks  
 SQLServerCallableStatement lets you specify the stored procedure name to call along with input and output parameters. SQLServerCallableStatement also provides the ability to retrieve the return status value with the `? = call( ?, ..)` syntax.  
  
 This class supports unwrapping to SQLServerCallableStatement class, ISQLServerCallableStatement interface, java.sql.CallableStatement interface, and the classes and interfaces supported by SQLServerPreparedStatement for unwrapping. For more information, see [Wrappers and Interfaces](../../../connect/jdbc/wrappers-and-interfaces.md).  
  
 When one of the SQLServerCallableStatement set methods is called for a type, if that type conflicts with the type specified with [registerOutParameter](../../../connect/jdbc/reference/registeroutparameter-method-sqlservercallablestatement.md), the type specified by the last SQLServerCallableStatement set method is used. However, this may cause incompatible data type conversion errors. If a SQLServerCallableStatement set method is not called, the type specified with the first [registerOutParameter](../../../connect/jdbc/reference/registeroutparameter-method-sqlservercallablestatement.md) call is used.  
  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0 is compliant with the JDBC 4.0 recommendation that a result set and update counts must be retrieved before retrieving OUT parameters. If OUT parameters are retrieved before the result set and update counts have been completely processed, any result sets and update counts that have not been processed are lost.  
  
## See Also  
 [SQLServerCallableStatement Members](../../../connect/jdbc/reference/sqlservercallablestatement-members.md)   
 [JDBC Driver API Reference](../../../connect/jdbc/reference/jdbc-driver-api-reference.md)  
  
  
