---
title: "setTrustServerCertificate Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "setTrustServerCertificate Method (SQLServerDataSource)"
apilocation: 
  - "setTrustServerCertificate Method (SQLServerDataSource)"
apitype: "Assembly"
ms.assetid: 6c37b518-147e-4cd9-9eff-b48a3f5888c6
author: MightyPen
ms.author: genemi
manager: craigg
---
# setTrustServerCertificate Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets a **Boolean** value that indicates if the trustServerCertificate property is enabled.  
  
## Syntax  
  
```  
  
public void setTrustServerCertificate(boolean trustServerCertificate)  
```  
  
#### Parameters  
 *trustServerCertificate*  
  
 **true** if the server Secure Sockets Layer (SSL) certificate should be automatically trusted when the communication layer is encrypted using SSL. Otherwise, **false**.  
  
## Remarks  
 If the trustServerCertificate property is set to **true**, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] SSL certificate is automatically trusted when the communication layer is encrypted using SSL. In other words, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will not validate the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] SSL certificate. The default value is **false**.  
  
 If the trustServerCertificate property is set to **false**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will validate the server SSL certificate.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
