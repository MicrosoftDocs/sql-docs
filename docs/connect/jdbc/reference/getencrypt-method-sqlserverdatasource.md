---
title: "getEncrypt Method (SQLServerDataSource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "getEncrypt Method (SQLServerDataSource)"
apilocation: 
  - "getEncrypt Method (SQLServerDataSource)"
apitype: "Assembly"
ms.assetid: 1cdb12dd-6e6f-4bbd-8f5f-9e630f3ee2c9
author: MightyPen
ms.author: genemi
manager: craigg
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
 If the encrypt property is set to **true**, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] ensures that [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] uses SSL encryption for all data sent between the client and the server if the server has a certificate installed.  
  
 If the encrypt property is unspecified or set to **false**, the driver will not enforce the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to support SSL encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is not configured to force the SSL encryption, a connection is established without any encryption. If the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance is configured to force the SSL encryption, the [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] will automatically enable SSL encryption when running on properly configured Java Virtual Machine (JVM), or else the connection is terminated and the driver will raise an error. If the encryption property is not set, the [getEncrypt](../../../connect/jdbc/reference/getencrypt-method-sqlserverdatasource.md) method returns the default value of **false**.  
  
## See Also  
 [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)   
 [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
  
  
