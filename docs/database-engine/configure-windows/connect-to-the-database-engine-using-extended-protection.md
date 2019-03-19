---
title: "Connect to the Database Engine Using Extended Protection | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "spoofing attacks"
  - "service binding"
  - "luring attacks"
  - "Schannel"
  - "channel binding"
  - "Extended Protection"
ms.assetid: ecfd783e-7dbb-4a6c-b5ab-c6c27d5dd57f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Connect to the Database Engine Using Extended Protection
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports **Extended Protection** beginning with [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]. **Extended Protection for Authentication** is a feature of the network components implemented by the operating system. **Extended Protection** is supported in Windows 7 and Windows Server 2008 R2. **Extended Protection** is included in service packs for older [!INCLUDE[msCoName](../../includes/msconame-md.md)] operating systems. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is more secure when connections are made using **Extended Protection**.  
  
> [!IMPORTANT]  
>  Windows does not enable **Extended Protection** by default. For information about how to enable **Extended Protection** in Windows, see [Extended Protection for Authentication](https://support.microsoft.com/kb/968389).  
  
## Description of Extended Protection  
 **Extended Protection** uses service binding and channel binding to help prevent an authentication relay attack. In an authentication relay attack, a client that can perform NTLM authentication (for example, Windows Explorer, [!INCLUDE[msCoName](../../includes/msconame-md.md)] Outlook, a .NET SqlClient application, etc.), connects to an attacker (for example, a malicious CIFS file server). The attacker uses the client's credentials to masquerade as the client and authenticate to a service (for example, an instance of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service).  
  
 There are two variations of this attack:  
  
-   In a luring attack, the client is lured to voluntarily connect to the attacker.  
  
-   In a spoofing attack, the client intends to connect to a valid service, but is unaware that one or both of DNS and IP routing are poisoned to redirect the connection to the attacker instead.  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports service binding and channel binding to help reduce these attacks on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instances.  
  
### Service Binding  
 Service binding addresses luring attacks by requiring a client to send a signed service principal name (SPN) of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service that the client intends to connect to. As part of the authentication response, the service validates that the SPN received in the packet matches its own SPN. If a client is lured to connect to an attacker, the client will include the signed SPN of the attacker. The attacker cannot relay the packet to authenticate to the real [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service as the client, because it would include the SPN of the attacker. Service binding incurs a one-time, negligible cost, but it does not address spoofing attacks. Service Binding occurs when a client application does not use encryption to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
### Channel Binding  
 Channel binding establishes a secure channel (Schannel) between a client and an instance of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. The service verifies the authenticity of the client by comparing the client's channel binding token (CBT) specific to that channel, with its own CBT. Channel binding addresses both luring and spoofing attacks. However, it incurs a larger runtime cost, because it requires Transport Layer Security (TLS) encryption of all the session traffic. Channel Binding occurs when a client application uses encryption to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], regardless of whether encryption is enforced by the client or by the server.  
  
> [!WARNING]  
>  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[msCoName](../../includes/msconame-md.md)] data providers for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] support TLS 1.0 and SSL 3.0. If you enforce a different protocol (such as TLS 1.1 or TLS 1.2) by making changes in the operating system SChannel layer, your connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might fail.  
  
### Operating System Support  
 The following links provide more information about how Windows supports **Extended Protection**:  
  
-   [Integrated Windows Authentication with Extended Protection](https://msdn.microsoft.com/library/dd639324.aspx)  
  
-   [Microsoft Security Advisory (973811), Extended Protection for Authentication](https://www.microsoft.com/technet/security/advisory/973811.mspx)  
  
## Settings  
 There are three [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] connection settings that affect service binding and channel binding. The settings can be configured by using the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager, or by using WMI, and can by viewed by using the **Server Protocol Settings** facet of Policy Based Management.  
  
-   **Force Encryption**  
  
     Possible values are **On** and **Off**. To use channel binding, **Force Encryption** must be set to **On**, and all clients will be forced to encrypt. If it is **Off**, only service binding is guaranteed. **Force Encryption** is on the **Protocols for MSSQLSERVER Properties (Flags Tab)** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
-   **Extended Protection**  
  
     Possible values are **Off**, **Allowed**, and **Required**. The **Extended Protection** variable lets users configure the **Extended Protection** level for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. **Extended Protection** is on the **Protocols for MSSQLSERVER Properties (Advanced Tab)** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
    -   When set to **Off**, **Extended Protection** is disabled. The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will accept connections from any client regardless of whether the client is protected or not. **Off** is compatible with older and unpatched operating systems, but is less secure. Use this setting when you know that the client operating systems do not support extended protection.  
  
    -   When set to **Allowed**, **Extended Protection** is required for connections from operating systems that support **Extended Protection**. **Extended Protection** is ignored for connections from operating systems that do not support **Extended Protection**. Connections from unprotected client applications that are running on protected client operating systems are rejected. This setting is more secure than **Off**, but is not the most secure setting. Use this setting in mixed environments, where some operating systems support **Extended Protection** and some do not.  
  
    -   When set to **Required**, only connections from protected applications on protected operating systems are accepted. This setting is the most secure but connections from operating systems or applications that do not support **Extended Protection** will not be able to connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Accepted NTLM SPNs**  
  
     The **Accepted NTLM SPNs** variable is needed when a server is known by more than one SPN. When a client attempts to connect to the server by using a valid SPN that the server does not know, service binding will fail. To avoid this problem, users can specify several SPNs that represent the server by using **Accepted NTLM SPNs**. **Accepted NTLM SPNs** is a series of SPNs separated my semicolons. For example, to allow the SPNs **MSSQLSvc/ HostName1.Contoso.com** and **MSSQLSvc/ HostName2.Contoso.com**, type **MSSQLSvc/HostName1.Contoso.com;MSSQLSvc/HostName2.Contoso.com** in the **Accepted NTLM SPNs** box. The variable has a maximum length of 2,048 characters. **Accepted NTLM SPNs** is on the **Protocols for MSSQLSERVER Properties (Advanced Tab)** in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
## Enabling Extended Protection for the Database Engine  
 To use **Extended Protection**, both the server and the client must have an operating system on that supports **Extended Protection**, and **Extended Protection** must be enabled on the operating system. For more information about how to enable **Extended Protection** for the operating system, see [Extended Protection for Authentication](https://support.microsoft.com/kb/968389).  
  
 [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] supports **Extended Protection** beginning with [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]. **Extended Protection** for some earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will be made available in future updates. After enabling **Extended Protection** on the server computer, use the following steps to enable **Extended Protection**:  
  
1.  On the **Start** menu, choose **All Programs**, point to **Microsoft SQL Server** and then click **SQL Server Configuration Manager**.  
  
2.  Expand **SQL Server Network Configuration**, and then right-click **Protocols for** _\<_InstanceName*>*, and then click **Properties**.  
  
3.  For both channel binding and service binding, on the **Advanced** tab, set **Extended Protection** to the appropriate setting.  
  
4.  Optionally, when a server is known by more than one SPN, on the **Advanced** tab configure the **Accepted NTLM SPNs** field as described in the "Settings" section.  
  
5.  For channel binding, on the **Flags** tab, set **Force Encryption** to **On**.  
  
6.  Restart the [!INCLUDE[ssDE](../../includes/ssde-md.md)] service.  
  
## Configuring Other SQL Server Components  
 For more information about how to configure [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Extended Protection for Authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md).  
  
 When using IIS to access [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] data using an HTTP or HTTPs connection, [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)] can take advantage of Extended Protection provided by IIS. For more information about how to configure IIS to use Extended Protection, see [Configure Extended Protection in IIS 7.5](https://go.microsoft.com/fwlink/?LinkId=181105).  
  
## See Also  
 [Server Network Configuration](../../database-engine/configure-windows/server-network-configuration.md)   
 [Client Network Configuration](../../database-engine/configure-windows/client-network-configuration.md)   
 [Extended Protection for Authentication Overview](https://go.microsoft.com/fwlink/?LinkID=177943)   
 [Integrated Windows Authentication with Extended Protection](https://go.microsoft.com/fwlink/?LinkId=179922)  
  
  
