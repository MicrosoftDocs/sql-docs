---
title: "getHostNameInCertificate Method (SQLServerDataSource)"
description: "getHostNameInCertificate Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "getHostNameInCertificate Method (SQLServerDataSource)"
apiname: "getHostNameInCertificate Method (SQLServerDataSource)"
apitype: "Assembly"
---
# getHostNameInCertificate Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the host name used in validating the SQL Server Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), certificate.  
  
## Syntax  
  
```  
  
public java.lang.String getHostNameInCertificate()  
```  
  
## Return Value  
 A **String** that contains the host name, or null if no value is set.  
  
## Remarks  
 The host name is used to validate the SQL Server TLS/SSL certificate value when the communication layer is encrypted using TLS/SSL.  
  
 If the host name is not set, the [getHostNameInCertificate](../../../connect/jdbc/reference/gethostnameincertificate-method-sqlserverdatasource.md) method returns null.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
