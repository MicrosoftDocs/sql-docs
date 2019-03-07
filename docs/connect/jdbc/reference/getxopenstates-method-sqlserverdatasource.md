---
title: "getXopenStates Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDataSource.getXopenStates"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: de6fdf6b-8345-4490-b35e-7115b61e782e
author: MightyPen
ms.author: genemi
manager: craigg
---
# getXopenStates Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a **boolean** value that indicates if converting SQL states to XOPEN compliant states is enabled.  
  
## Syntax  
  
```  
  
public boolean getXopenStates()  
```  
  
## Return Value  
 **true** if converting SQL states to XOPEN compliant states is enabled. Otherwise, **false**.  
  
## Remarks  
 If the xopenStates property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will convert SQL states to XOPEN compliant states. The default is **false**, which causes the JDBC driver to generate SQL 99 state codes. If xopenStates is not set, the getXopenStates method returns the default value of **false**.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
