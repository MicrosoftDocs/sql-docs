---
title: "Configure Client Protocols | Microsoft Docs"
ms.custom: ""
ms.date: "07/27/2016"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "default protocols"
  - "network protocols [SQL Server], client configuration"
  - "TCP/IP [SQL Server], client protocols"
  - "disabling client protocols"
  - "ordering protocols [SQL Server]"
  - "protocols [SQL Server], order for client computers"
  - "configure client protocols"
  - "client protocols [SQL Server]"
  - "protocols [SQL Server], client configuration"
  - "default protocols, client"
ms.assetid: 3dfa2702-ba65-43b4-a777-6727846e133a
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Configure Client Protocols
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to configure client protocols used by client applications in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager. Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports client communication with the TCP/IP network protocol and the named pipes protocol. The shared memory protocol is also available if the client is connecting to an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] on the same computer. There are three common methods of selecting the protocol.  
  
-   Configure all client applications to use the same network protocol by setting the protocol order in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
-   Configure a single client application to use a different network protocol by creating an alias. For more information, see [Create or Delete a Server Alias for Use by a Client &#40;SQL Server Configuration Manager&#41;](../../database-engine/configure-windows/create-or-delete-a-server-alias-for-use-by-a-client.md).  
  
-   Some client applications, such as sqlcmd.exe, can specify the protocol as part of the connection string. For more information, see [Connect to the Database Engine With sqlcmd](../../relational-databases/scripting/sqlcmd-connect-to-the-database-engine.md).  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Configuration Manager  
  
###  <a name="EnableDisable"></a> To enable or disable a client protocol  
  
1.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Native Client Configuration**, right-click **Client Protocols**, and then click **Properties**.  
  
2.  Click a protocol in the **Disabled Protocols** box, and then click **Enable**, to enable a protocol.  
  
3.  Click a protocol in the **Enabled Protocols** box, and then click **Disable**, to disable a protocol.  
  
###  <a name="ChangeDefault"></a> To change the default protocol or the protocol order for client computers  
  
1.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Native Client Configuration**, right-click **Client Protocols**, and then click **Properties**.  
  
2.  In the **Enabled Protocols** box, click **Move Up** or **Move Down**, to change the order in which protocols are tried, when attempting to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The top protocol in the **Enabled Protocols** box is the default protocol.  
  
    > [!IMPORTANT]  
    >  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager creates registry entries for the server alias configurations and default client network library. However, the application does not install either the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client network libraries or the network protocols. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] client network libraries are installed during [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup; the network protocols are installed as part of Microsoft Windows Setup (or through **Networks** in **Control Panel**). A particular network protocol may not be available as part of Windows Setup. For more information about installing these network protocols, see the vendor documentation.  
  
###  <a name="Configure"></a> To configure a client to use TCP/IP  
  
1.  In [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Native Client Configuration**, right-click **Client Protocols**, and then click **Properties**.  
  
2.  In the **Enabled Protocols** box, click the up and down arrows to change the order in which protocols are tried, when attempting to connect to SQL Server. The top protocol in the **Enabled Protocols** box is the default protocol.  
  
 The shared memory protocol is enabled separately by checking the **Enabled Shared Memory Protocol** box.  
  
## See Also  
 [Configure the remote login timeout Server Configuration Option](../../database-engine/configure-windows/configure-the-remote-login-timeout-server-configuration-option.md)  
  
  
