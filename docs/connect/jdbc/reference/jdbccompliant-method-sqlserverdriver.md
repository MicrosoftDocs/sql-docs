---
title: "jdbcCompliant Method (SQLServerDriver)"
description: "jdbcCompliant Method (SQLServerDriver)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDriver.jdbcCompliant"
apitype: "Assembly"
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
  
## Related content

- [SQLServerDriver Methods](sqlserverdriver-methods.md)
- [SQLServerDriver Members](sqlserverdriver-members.md)
- [SQLServerDriver Class](sqlserverdriver-class.md)
