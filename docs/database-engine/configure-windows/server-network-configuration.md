---
title: "Server Network Configuration | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "Named Pipes [SQL Server], configuring"
  - "connections [SQL Server], server network configuration"
  - "Database Engine [SQL Server], network configurations"
  - "server network configuration [SQL Server]"
  - "protocols [SQL Server], choosing"
  - "ports [SQL Server], changing"
  - "server configuration [SQL Server]"
ms.assetid: 890c09a1-6dad-4931-aceb-901c02ae34c5
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Server Network Configuration
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Server network configuration tasks include enabling protocols, modifying the port or pipe used by a protocol, configuring encryption, configuring the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service, exposing or hiding the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] on the network, and registering the Server Principal Name. Most of the time, you do not have to change the server network configuration. Only reconfigure the server network protocols if special network requirements.  
  
 Network configuration for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is done using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. For earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use the Server Network Utility that ships with those products.  
  
## Protocols  
 Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to enable or disable the protocols used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], and to configure the options available for the protocols. More than one protocol can be enabled. You must enable all protocols that you want clients to use. All protocols have equal access to the server. For information about which protocols you should use, see [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md) and [Default SQL Server Network Protocol Configuration](../../database-engine/configure-windows/default-sql-server-network-protocol-configuration.md).  
  
### Changing a Port  
 You can configure the TCP/IP protocol to listen on a designated port. By default, the default instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] listens on TCP port 1433. Named instances of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssEW](../../includes/ssew-md.md)] are configured for dynamic ports. This means they select an available port when the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service is started. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service helps clients identify the port when they connect.  
  
 When configured for dynamic ports, the port used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] may change each time it is started. When connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] through a firewall, you must open the port used by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Configure [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to use a specific port, so you can configure the firewall to allow communication to the server. For more information, see [Configure a Server to Listen on a Specific TCP Port &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/configure-a-server-to-listen-on-a-specific-tcp-port.md).  
  
### Changing a Named Pipe  
 You can configure the named pipe protocol to listen on a designated named pipe. By default, the default instance of [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] listens on pipe \\\\.\pipe\sql\query for the default instance and \\\\.\pipe\MSSQL$*\<instancename>*\sql\query for a named instance. The [!INCLUDE[ssDE](../../includes/ssde-md.md)] can only listen on one named pipe, but you can change the pipe to another name if you wish. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service helps clients identify the pipe when they connect. For more information, see [Configure a Server to Listen on an Alternate Pipe &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/configure-a-server-to-listen-on-an-alternate-pipe.md).  
  
## Force Encryption  
 The [!INCLUDE[ssDE](../../includes/ssde-md.md)] can be configured to require encryption when communicating with client applications. For more information, see [Enable Encrypted Connections to the Database Engine &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).  
  
## Extended Protection for Authentication  
 Support for Extended Protection for Authentication by using channel binding and service binding is available for operating systems that support Extended Protection. For more information, see [Connect to the Database Engine Using Extended Protection](../../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md).  
  
## Authenticating by Using Kerberos  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports Kerberos authentication. For more information, see [Register a Service Principal Name for Kerberos Connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md) and [Microsoft Kerberos Configuration Manager for SQL Server](https://www.microsoft.com/download/details.aspx?id=39046).  
  
### Registering a Server Principal Name (SPN)  
 The Kerberos authentication service uses an SPN to authenticate a service. For more information, see [Register a Service Principal Name for Kerberos Connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).  
  
 SPNs may also be used to make client authentication more secure when connecting with NTLM. For more information, see [Connect to the Database Engine Using Extended Protection](../../database-engine/configure-windows/connect-to-the-database-engine-using-extended-protection.md).  
  
## SQL Server Browser Service  
 The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service runs on the server, and helps client computers to find instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser service does not need to be configured, but must be running under some connection scenarios. For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser, see [SQL Server Browser Service &#40;Database Engine and SSAS&#41;](../../database-engine/configure-windows/sql-server-browser-service-database-engine-and-ssas.md)  
  
## Hiding SQL Server  
 When running, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser responds to queries, with the name, version, and connection information for each installed instance. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], the **HideInstance** flag, indicates that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Browser should not respond with information about this server instance. Client applications can still connect, but they must know the required connection information. For more information, see [Hide an Instance of SQL Server Database Engine](../../database-engine/configure-windows/hide-an-instance-of-sql-server-database-engine.md).  
  
## Related Content  
 [Client Network Configuration](../../database-engine/configure-windows/client-network-configuration.md)  
  
 [Manage the Database Engine Services](../../database-engine/configure-windows/manage-the-database-engine-services.md)  
  
  
