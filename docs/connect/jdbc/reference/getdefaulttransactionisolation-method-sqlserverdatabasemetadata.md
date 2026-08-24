---
title: "getDefaultTransactionIsolation Method (SQLServerDatabaseMetaData)"
description: "getDefaultTransactionIsolation Method (SQLServerDatabaseMetaData)"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDatabaseMetaData.getDefaultTransactionIsolation"
apitype: "Assembly"
---
# getDefaultTransactionIsolation Method (SQLServerDatabaseMetaData)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the default transaction isolation level for this database.  
  
## Syntax  
  
```  
  
public int getDefaultTransactionIsolation()  
```  
  
## Return Value  
 An **int** that indicates the default transaction isolation level.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This getDefaultTransactionIsolation method is specified by the getDefaultTransactionIsolation method in the java.sql.DatabaseMetaData interface.  
  
 When using the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] with a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] database, this method returns either a value of TRANSACTION_READ_COMMITTED, or the **int** value 2.  
  
## Related content

- [SQLServerDatabaseMetaData Methods](sqlserverdatabasemetadata-methods.md)
- [SQLServerDatabaseMetaData Members](sqlserverdatabasemetadata-members.md)
- [SQLServerDatabaseMetaData Class](sqlserverdatabasemetadata-class.md)
