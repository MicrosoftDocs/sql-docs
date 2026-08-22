---
title: "setCatalog Method (SQLServerConnection)"
description: "setCatalog Method (SQLServerConnection)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerConnection.setCatalog"
apitype: "Assembly"
---
# setCatalog Method (SQLServerConnection)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the given catalog name to select a subspace of this [SQLServerConnection](../../../connect/jdbc/reference/sqlserverconnection-class.md) object's database in which to work.  
  
## Syntax  
  
```  
  
public void setCatalog(java.lang.String catalog)  
```  
  
#### Parameters  
 *catalog*  
  
 A **String** that contains the catalog name.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setCatalog method is specified by the setCatalog method in the java.sql.Connection interface.  
  
 The *catalog* argument is escaped by the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] automatically. Using this method sets the catalog property for the Connection object. It is not set implicitly in any other way.  
  
## Related content

- [SQLServerConnection Members](sqlserverconnection-members.md)
- [SQLServerConnection Class](sqlserverconnection-class.md)
