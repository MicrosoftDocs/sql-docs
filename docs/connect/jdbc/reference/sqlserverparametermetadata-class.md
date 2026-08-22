---
title: "SQLServerParameterMetaData Class"
description: "SQLServerParameterMetaData Class"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerParameterMetaData Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents the metadata for prepared statement parameters.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** java.lang.Object  
  
 **Implements:** java.sql.ParameterMetaData  
  
## Syntax  
  
```  
  
public class SQLServerParameterMetaData  
```  
  
## Remarks  
 To retrieve parameter metadata, prepared statements are run with SET FMT ONLY. Callable statements call sp_sproc_columns to retrieve names and metadata for the procedure parameters.  
  
## Related content

- [SQLServerParameterMetaData Members](sqlserverparametermetadata-members.md)
- [JDBC driver API reference](jdbc-driver-api-reference.md)
