---
title: "ISQLServerCallableStatement Interface"
description: "ISQLServerCallableStatement Interface"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# ISQLServerCallableStatement Interface
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents JDBC callable statements. This interface was added in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] JDBC Driver 3.0.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** java.sql.CallableStatement, [ISQLServerPreparedStatement](../../../connect/jdbc/reference/isqlserverpreparedstatement-interface.md)  
  
## Syntax  
  
```  
  
public interface ISQLServerCallableStatement  
```  
  
## Remarks  
 This interface is implemented by [SQLServerCallableStatement Class](../../../connect/jdbc/reference/sqlservercallablestatement-class.md).  
  
 This interface exposes the following [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)]-specific methods:  
  
|Method|For more information, see|  
|------------|-------------------------------|  
|microsoft.sql.DateTimeOffset getDateTimeOffset(int)|[getDateTimeOffset(int)](../../../connect/jdbc/reference/getdatetimeoffset-method-int.md)|  
|microsoft.sql.DateTimeOffset getDateTimeOffset(String)|[getDateTimeOffset(String)](../../../connect/jdbc/reference/getdatetimeoffset-method-string.md)|  
|void setDateTimeOffset(String, microsoft.sql.DateTimeOffset)|[setDateTimeOffset](../../../connect/jdbc/reference/setdatetimeoffset-method-sqlservercallablestatement.md)|  
  
## Related content

- [JDBC driver API reference](jdbc-driver-api-reference.md)
