---
title: "Using Encryption Without Validation | Microsoft Docs"
description: Learn how the SQL Server Native Client OLE DB provider and ODBC driver support encryption without validation and recommendations for when to use it.
ms.custom: ""
ms.date: "12/21/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "data access [SQL Server Native Client], encryption"
  - "cryptography [SQL Server Native Client]"
  - "SQLNCLI, encryption"
  - "encryption [SQL Server Native Client]"
  - "SQL Server Native Client, encryption"
ms.assetid: f4c63206-80bb-4d31-84ae-ccfcd563effa
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Using Encryption Without Validation in SQL Server Native Client
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

> [!IMPORTANT] 
> [!INCLUDE[snac-removed-oledb-and-odbc](../../../includes/snac-removed-oledb-and-odbc.md)]

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] always encrypts network packets associated with logging in. If no certificate has been provisioned on the server when it starts up, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] generates a self-signed certificate which is used to encrypt login packets.  

Self-signed certificates do not guarantee security. The encrypted handshake is based on NT LAN Manager (NTLM). It is highly recommended that you provision a verifiable certificate on SQL Server for secure connectivity. Transport Security Layer (TLS) can be made secure only with certificate validation.

Applications may also request encryption of all network traffic by using connection string keywords or connection properties. The keywords are "Encrypt" for ODBC and OLE DB when using a provider string with **IDbInitialize::Initialize**, or "Use Encryption for Data" for ADO and OLE DB when using an initialization string with **IDataInitialize**. This may also be configured by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager using the **Force Protocol Encryption** option, and by configuring the client to request encrypted connections. By default, encryption of all network traffic for a connection requires that a certificate be provisioned on the server. By setting your client to trust the certificate on the server, you might become vulnerable to man-in-the-middle attacks. If you deploy a verifiable certificate on the server, ensure that you change the client settings about trust the certificate to FALSE.

For information about connection string keywords, see [Using Connection String Keywords with SQL Server Native Client](../../../relational-databases/native-client/applications/using-connection-string-keywords-with-sql-server-native-client.md).  
  
 To enable encryption to be used when a certificate has not been provisioned on the server, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager can be used to set both the **Force Protocol Encryption** and the **Trust Server Certificate** options. In this case, encryption will use a self-signed server certificate without validation if no verifiable certificate has been provisioned on the server.  
  
 Applications may also use the "TrustServerCertificate" keyword or its associated connection attribute to guarantee that encryption takes place. Application settings never reduce the level of security set by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Client Configuration Manager, but may strengthen it. For example, if **Force Protocol Encryption** is not set for the client, an application may request encryption itself. To guarantee encryption even when a server certificate has not been provisioned, an application may request encryption and "TrustServerCertificate". However, if "TrustServerCertificate" is not enabled in the client configuration, a provisioned server certificate is still required. The following table describes all cases:  
  
|Force Protocol Encryption client setting|Trust Server Certificate client setting|Connection string/connection attribute Encrypt/Use Encryption for Data|Connection string/connection attribute Trust Server Certificate|Result|  
|----------------------------------------------|---------------------------------------------|------------------------------------------------------------------------------|----------------------------------------------------------------------|------------|  
|No|N/A|No (default)|Ignored|No encryption occurs.|  
|No|N/A|Yes|No (default)|Encryption occurs only if there is a verifiable server certificate, otherwise the connection attempt fails.|  
|No|N/A|Yes|Yes|Encryption always occurs, but may use a self-signed server certificate.|  
|Yes|No|Ignored|Ignored|Encryption occurs only if there is a verifiable server certificate, otherwise the connection attempt fails.|  
|Yes|Yes|No (default)|Ignored|Encryption always occurs, but may use a self-signed server certificate.|  
|Yes|Yes|Yes|No (default)|Encryption occurs only if there is a verifiable server certificate, otherwise the connection attempt fails.|  
|Yes|Yes|Yes|Yes|Encryption always occurs, but might use a self-signed server certificate.|  

> [!CAUTION]
> The preceding table only provides a guide on the system behavior under different configurations. For secure connectivity, ensure that the client and server both require encryption. Also ensure that the server has a verifiable certificate, and that the **TrustServerCertificate** setting on the client is set to FALSE.

## SQL Server Native Client OLE DB Provider  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client OLE DB provider supports encryption without validation through the addition of the SSPROP_INIT_TRUST_SERVER_CERTIFICATE data source initialization property, which is implemented in the DBPROPSET_SQLSERVERDBINIT property set. In addition, a new connection string keyword, "TrustServerCertificate", as been added. It accepts yes or no values; no is the default. When using service components, it accepts true or false values; false is the default.  
  
 For more information about enhancements made to the DBPROPSET_SQLSERVERDBINIT property set, see [Initialization and Authorization Properties](../../../relational-databases/native-client-ole-db-data-source-objects/initialization-and-authorization-properties.md).  
  
## SQL Server Native Client ODBC Driver  
 The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver supports encryption without validation through additions to the [SQLSetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlsetconnectattr.md) and [SQLGetConnectAttr](../../../relational-databases/native-client-odbc-api/sqlgetconnectattr.md) functions. SQL_COPT_SS_TRUST_SERVER_CERTIFICATE has been added to accept either SQL_TRUST_SERVER_CERTIFICATE_YES or SQL_TRUST_SERVER_CERTIFICATE_NO, with SQL_TRUST_SERVER_CERTIFICATE_NO being the default. In addition, a new connection string keyword, "TrustServerCertificate", has been added. It accepts yes or no values; "no" is the default.  
  
## See Also  
 [SQL Server Native Client Features](../../../relational-databases/native-client/features/sql-server-native-client-features.md)  
  
  
