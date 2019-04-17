---
title: "jdbcCompliant Method (SQLServerDriver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerDriver.jdbcCompliant"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: b299b20d-d1cd-45b3-91dc-dcf579498570
author: MightyPen
ms.author: genemi
manager: craigg
---
# jdbcCompliant Method (SQLServerDriver)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Verifies that the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] is compliant with the JDBC specification.  
  
## Syntax  
  
```  
  
public boolean jdbcCompliant()  
```  
  
## Return Value  
 **true** if the JDBC driver meets the minimum requirements. Otherwise, **false**.  
  
## Remarks  
 This jdbcCompliant method is specified by the jdbcCompliant method in the java.sql.Driver interface.  
  
## See Also  
 [SQLServerDriver Methods](../../../connect/jdbc/reference/sqlserverdriver-methods.md)   
 [SQLServerDriver Members](../../../connect/jdbc/reference/sqlserverdriver-members.md)   
 [SQLServerDriver Class](../../../connect/jdbc/reference/sqlserverdriver-class.md)  
  
  
