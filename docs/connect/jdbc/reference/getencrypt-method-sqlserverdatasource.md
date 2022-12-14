---
title: "getEncrypt Method (SQLServerDataSource)"
description: "getEncrypt Method (SQLServerDataSource)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "getEncrypt Method (SQLServerDataSource)"
apiname: "getEncrypt Method (SQLServerDataSource)"
apitype: "Assembly"
---
# getEncrypt Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Returns a **Boolean** value that indicates if the encrypt property is enabled.  
  
## Syntax  
  
```  
  
public boolean getEncypt()  
```  
  
## Return Value  
 **true** if encrypt is enabled. Otherwise, **false**.  
  
## Remarks  
 If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] ensures that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses TLS encryption for all data sent between the client and the server if the server has a certificate installed.  
  
 If the encrypt property is unspecified or set to **false**, the driver will not enforce the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to support TLS encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is not configured to force the TLS encryption, a connection is established without any encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is configured to force the TLS encryption, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will automatically enable TLS encryption when running on properly configured Java Virtual Machine (JVM), or else the connection is terminated and the driver will raise an error. If the encryption property is not set, the [getEncrypt](../../../connect/jdbc/reference/getencrypt-method-sqlserverdatasource.md) method returns the default value of **false**.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
