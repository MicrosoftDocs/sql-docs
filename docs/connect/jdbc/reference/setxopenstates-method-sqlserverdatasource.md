---
title: "setXopenStates Method (SQLServerDataSource)"
description: "setXopenStates Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setXopenStates"
apitype: "Assembly"
---
# setXopenStates Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets a **Boolean** value that indicates if converting SQL states to XOPEN compliant states is enabled.  
  
## Syntax  
  
```  
  
public void setXopenStates(boolean xopenStates)  
```  
  
#### Parameters  
 *xopenStates*  
  
 **true** if converting SQL states to XOPEN compliant states is enabled. Otherwise, **false**.  
  
## Remarks  
 If the xopenStates property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will convert SQL states to XOPEN compliant states. The default is **false**, which causes the JDBC driver to generate SQL 99 state codes. If xopenStates is not set, the [getXopenStates](../../../connect/jdbc/reference/getxopenstates-method-sqlserverdatasource.md) method returns the default value of **false**.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
