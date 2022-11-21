---
title: "getTrustStore Method (SQLServerDataSource)"
description: "getTrustStore Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "getTrustStore Method (SQLServerDataSource)"
apiname: "getTrustStore Method (SQLServerDataSource)"
apitype: "Assembly"
---
# getTrustStore Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the path (including file name) to the certificate trustStore file.  
  
## Syntax  
  
```  
  
public java.lang.String getTrustStore()  
```  
  
## Return Value  
 A **String** that contains the path (including file name) to the certificate trustStore file, or null if no value is set.  
  
## Remarks  
 If the trustStore property is not set, the [getTrustStore](../../../connect/jdbc/reference/gettruststore-method-sqlserverdatasource.md) method returns null.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
