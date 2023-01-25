---
title: "setTrustStore Method (SQLServerDataSource)"
description: "setTrustStore Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "setTrustStore Method (SQLServerDataSource)"
apiname: "setTrustStore Method (SQLServerDataSource)"
apitype: "Assembly"
---
# setTrustStore Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the path (including file name) to the certificate trustStore file.  
  
## Syntax  
  
```  
  
public void setTrustStore(java.lang.String trustStore)  
```  
  
#### Parameters  
 *trustStore*  
  
 A **String** that contains the path (including file name) to the certificate trustStore file.  
  
## Remarks  
 If the trustStore property is unspecified or set to null, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will rely on the trust manager factory's look up rules to determine which certificate store to use. The default SunX509 TrustManagerFactory tries to find the trust material in the following locations in this order:  
  
-   1. A file specified by the "javax.net.ssl.trustStore" Java Virtual Machine (JVM) system property.  
  
-   2. "\<java-home>/lib/security/jssecacerts" file.  
  
-   3. "\<java-home>/lib/security/cacerts" file.  
  
 For more information, see the SunX509 TrustManager Interface documentation on the Sun Microsystems Web site.  
  
 If the trustStore property is set to a string or an empty string "", the driver will use that value to find the trustStore file to validate the server TLS/SSL certificate.  
  
 The trustStorePassword property can be specified along with the trustStore property and its value is used to open the trustStore file. For more information, see [setTrustStorePassword](../../../connect/jdbc/reference/settruststorepassword-method-sqlserverdatasource.md).  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
