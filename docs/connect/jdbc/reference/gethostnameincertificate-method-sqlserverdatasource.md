---
title: "getHostNameInCertificate Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "getHostNameInCertificate Method (SQLServerDataSource)"
apilocation: 
  - "getHostNameInCertificate Method (SQLServerDataSource)"
apitype: "Assembly"
ms.assetid: 45ea04e2-9ea5-4171-9136-d09f8a95e128
author: MightyPen
ms.author: genemi
manager: craigg
---
# getHostNameInCertificate Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns the host name used in validating the SQL Server Secure Sockets Layer (SSL) certificate.  
  
## Syntax  
  
```  
  
public java.lang.String getHostNameInCertificate()  
```  
  
## Return Value  
 A **String** that contains the host name, or null if no value is set.  
  
## Remarks  
 The host name is used to validate the SQL Server SSL certificate value when the communication layer is encrypted using SSL.  
  
 If the host name is not set, the [getHostNameInCertificate](../../../connect/jdbc/reference/gethostnameincertificate-method-sqlserverdatasource.md) method returns null.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
