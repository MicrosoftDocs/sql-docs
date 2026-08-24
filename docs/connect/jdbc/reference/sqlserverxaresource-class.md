---
title: "SQLServerXAResource Class"
description: "SQLServerXAResource Class"
author: dlevy-msft-sql
ms.author: dlevy
ms.reviewer: davidengel, machavan, sunilbs
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
---
# SQLServerXAResource Class
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Represents an XAResource for XA distributed transaction management.  
  
 **Package:** com.microsoft.sqlserver.jdbc  
  
 **Extends:** java.lang.Object  
  
 **Implements:** javax.transaction.xa.XAResource  
  
## Syntax  
  
```  
  
public class SQLServerXAResource  
```  
  
## Remarks  
 XA transactions are implemented in [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] by using [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Distributed Transaction Manager (DTC). The SQLServerXAResource class makes calls to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] extended dll named sqljdbc_xa.dll, which interfaces with DTC. XA calls that are received by SQLServerXAResource (XA_START, XA_END, XA_PREPARE, and so forth) are mapped to the corresponding calls to DTC functions.  
  
## Related content

- [SQLServerXAResource Members](sqlserverxaresource-members.md)
- [JDBC driver API reference](jdbc-driver-api-reference.md)
