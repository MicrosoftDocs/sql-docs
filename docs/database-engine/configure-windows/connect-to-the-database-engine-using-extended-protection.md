---
title: Connect to the Database Engine Using Extended Protection
description: Learn how Extended Protection uses service binding and channel binding to help prevent authentication relay attacks. See how to enable this feature.
author: VanMSFT
ms.author: vanto
ms.reviewer: vanto
ms.date: 05/07/2026
ms.service: sql
ms.subservice: configuration
ms.topic: how-to
helpviewer_keywords:
  - "spoofing attacks"
  - "service binding"
  - "luring attacks"
  - "Schannel"
  - "channel binding"
  - "Extended Protection"
ai-usage: ai-assisted
---

# Connect to the database engine with Extended Protection

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Extended Protection helps prevent authentication relay attacks by ensuring the client knows the service it connects to.

Extended Protection is a feature of the network components implemented by the operating system. Extended Protection is supported in Windows.

SQL Server is more secure when connections are made using Extended Protection.

## Requirements checklist

For Extended Protection to be effective, all the following conditions must be met:

1. Use a [client driver](#driver-support) that supports channel binding.
1. Enable TLS encryption on the connection so the client can produce a channel binding token (CBT).
1. Connect with the correct Service Principal Name (SPN) matching the SQL Server instance.
1. Use Windows authentication (NTLM or Kerberos). Extended Protection doesn't apply to SQL authentication.
1. Enable Extended Protection on the SQL Server instance (set to **Allowed** or **Required**).

> [!NOTE]  
> For an example of a connection string in C# that supports Extended Protection, see [Example connection string](#example-connection-string).

## Description of Extended Protection

Extended Protection uses service binding and channel binding to help prevent an authentication relay attack. In an authentication relay attack, a client that can perform NTLM authentication (for example, Windows Explorer, Outlook, a .NET SqlClient application, and others), connects to an attacker (for example, a malicious CIFS file server). The attacker uses the client's credentials to masquerade as the client and authenticate to a service (for example, an instance of the Database Engine).

Two variations of this attack exist:

- In a *luring attack*, the client voluntarily connects to the attacker.

- In a *spoofing attack*, the client intends to connect to a valid service but is unaware that one or both DNS and IP routing are poisoned to redirect the connection to the attacker instead.

SQL Server supports service binding and channel binding to help reduce these attacks on SQL Server instances.

### Service binding

*Service binding* addresses luring attacks by requiring a client to send a signed service principal name (SPN) of the SQL Server service that the client intends to connect to. As part of the authentication response, the service validates that the SPN received in the packet matches its own SPN. If a client is lured to connect to an attacker, the client includes the attacker's signed SPN. The attacker can't relay the packet to authenticate to the real SQL Server service as the client because it would include the SPN of the attacker. Service binding incurs a one-time, negligible cost but doesn't address spoofing attacks. Service binding occurs when a client application doesn't use encryption to connect to the SQL Server.

### Channel binding

*Channel binding* establishes a secure channel (Schannel) between a client and an instance of the SQL Server service. The service verifies the client's authenticity by comparing the client's channel binding token (CBT) specific to that channel with its own CBT. Channel binding addresses both luring and spoofing attacks. However, it incurs a larger runtime cost because it requires Transport Layer Security (TLS) encryption of all the session traffic. Channel binding occurs when a client application uses encryption to connect to the SQL Server, regardless of whether encryption is enforced by the client or by the server.

> [!WARNING]  
> SQL Server and Microsoft data providers for SQL Server support older protocols, including TLS 1.0 and SSL 3.0. If you enforce a different protocol (such as TLS 1.2 or TLS 1.3) by making changes in the operating system SChannel layer, your connections to SQL Server might fail. Make sure that you have the latest build of SQL Server to support TLS 1.2 or TLS 1.3. For more information, see [TLS 1.2](/troubleshoot/sql/database-engine/connect/tls-1-2-support-microsoft-sql-server) and [TLS 1.3](../../relational-databases/security/networking/tls-1-3.md).

### Operating system support

The following links provide more information about how Windows supports Extended Protection:

- [Integrated Windows Authentication with Extended Protection](/previous-versions/visualstudio/visual-studio-2008/dd639324(v=vs.90))
- [Microsoft Security Advisory (973811), Extended Protection for Authentication](/security-updates/SecurityAdvisories/2009/973811)

### Driver support

The only drivers that support Extended Protection are Windows-based:

- Microsoft ODBC Driver for SQL Server (on Windows only)
- Microsoft OLE DB Driver for SQL Server
- System.Data.SqlClient (in .NET Framework on Windows)
- Microsoft.Data.SqlClient (on Windows)

## Settings

Three SQL Server connection settings affect service binding and channel binding. You can configure these settings using the SQL Server Configuration Manager or WMI. View these settings using the **Server Protocol Settings** facet of Policy Based Management.

### Force Encryption

Possible values are **Yes** and **No**. To use channel binding, set **Force Encryption** to **Yes**, and all clients must encrypt. If it's **No**, only service binding is guaranteed. **Force Encryption** is on the **Protocols for MSSQLSERVER Properties (Flags Tab)** in SQL Server Configuration Manager. Starting with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], you can also set **Force Strict Encryption** to **Yes** for stronger protection by using TDS 8.0. For more information, see [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md).

Set **Force Encryption** to **Yes** when used with [Extended Protection](#enable-encryption-with-extended-protection).

### Extended Protection

Possible values are **Off**, **Allowed**, and **Required**. Use the **Extended Protection** variable to set the Extended Protection level for each SQL Server instance. You can find **Extended Protection** on the **Protocols for MSSQLSERVER Properties (Advanced Tab)** in SQL Server Configuration Manager.

- Set to **Off** to disable Extended Protection. The instance of SQL Server accepts connections from any client regardless of whether the client is protected or not. **Off** is compatible with older and unpatched operating systems but is less secure. Use this setting when the client operating systems don't support extended protection.

- Set to **Allowed** to require Extended Protection for connections from operating systems that support Extended Protection. Extended Protection is ignored for connections from operating systems that don't support Extended Protection. Connections from unprotected client applications running on protected client operating systems are rejected. This setting is more secure than **Off**, but it isn't the most secure. Use this setting in mixed environments. Some operating systems support Extended Protection, and others don't.

- Set to **Required** to accept only connections from protected applications on protected operating systems. This setting is the most secure, but connections from operating systems or applications that don't support Extended Protection can't connect to SQL Server.

For more information about recommended settings, see [Enable encryption with Extended Protection](#enable-encryption-with-extended-protection).

### Accepted NTLM SPNs

Specify the **Accepted NTLM SPNs** variable when more than one SPN knows a server. When a client attempts to connect to the server using a valid SPN that the server doesn't know, service binding fails. To avoid this problem, specify several SPNs representing the server using the **Accepted NTLM SPNs**. **Accepted NTLM SPNs** is a series of SPNs separated by semicolons. For example, to allow the SPNs **MSSQLSvc/ HostName1.Contoso.com** and **MSSQLSvc/ HostName2.Contoso.com**, type **MSSQLSvc/HostName1.Contoso.com;MSSQLSvc/HostName2.Contoso.com** in the **Accepted NTLM SPNs** box. The variable has a maximum length of 2,048 characters. You can find **Accepted NTLM SPNs** on the **Protocols for MSSQLSERVER Properties (Advanced Tab)** in SQL Server Configuration Manager.

## Enable Extended Protection for the database engine

To use Extended Protection, both the server and the client must have an operating system that supports Extended Protection and must be enabled on the operating system. For more information about how to enable Extended Protection for the operating system, see [Extended Protection for Authentication](/dotnet/framework/WCF/feature-details/extended-protection-for-authentication-overview).

While **Extended Protection** and **NTLMv2** are enabled by default in all supported versions of Windows, Extended Protection isn't enabled by default for SQL Server connections. You must manually enable it in [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

To enable Extended Protection for SQL Server connections, administrators must configure the settings in [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md). This configuration includes options for service binding and channel binding to mitigate various types of authentication relay attacks. For detailed instructions, refer to the SQL Server documentation on configuring Extended Protection.

### Enable encryption with Extended Protection

[!INCLUDE [extended-protection-recommendation](../../includes/extended-protection-recommendation.md)]

### After you enable Extended Protection

After you enable **Extended Protection** on the server computer, use the following steps to enable **Extended Protection**:

1. Go to **SQL Server Configuration Manager** from the Windows Start menu.

1. Expand **SQL Server Network Configuration**, and then right-click **Protocols for** *\<InstanceName>*. Select **Properties**.

1. On the **Advanced** tab, set **Extended Protection** to the appropriate setting for both channel binding and service binding.

1. Optionally, when more than one SPN knows a server, configure the **Accepted NTLM SPNs** field on the **Advanced** tab, as described in the [Settings](#settings) section.

1. For channel binding, on the **Flags** tab, set **Force Encryption** to **Yes**. This setting is [recommended](#enable-encryption-with-extended-protection).

1. Restart the [!INCLUDE [ssDE](../../includes/ssde-md.md)] service.

## Configure other SQL Server components

For more information about how to configure [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Extended protection for authentication with Reporting Services](../../reporting-services/security/extended-protection-for-authentication-with-reporting-services.md).

When you use IIS to access [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] data with an HTTP or HTTPS connection, [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)] can take advantage of Extended Protection provided by IIS. For more information about how to configure IIS to use Extended Protection, see [Configure Extended Protection in IIS 7.5](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ee909472(v=ws.10)).

## Example connection string

In the following C# example, the connection string uses Windows authentication (`Integrated Security=true`). The channel binding token is enabled through TLS encryption (`Encrypt=true`). Certificate validation is enforced with `TrustServerCertificate=false`. Although not required, `HostNameInCertificate` is recommended and included in this example.

```csharp
using (var conn = new SqlConnection(
    "Server=sql01.contoso.com;" +
    "Database=AdventureWorks2025;" +
    "Integrated Security=true;" +        
    "Encrypt=true;" +                   
    "TrustServerCertificate=false;" +    
    "HostNameInCertificate=sql01.contoso.com;")) 
{
    conn.Open();
}
```

## Related content

- [Server network configuration](server-network-configuration.md)
- [Client network configuration](client-network-configuration.md)
- [Extended Protection for Authentication Overview](/previous-versions/dotnet/netframework-3.5/dd767318(v=vs.90))
- [Integrated Windows Authentication with Extended Protection](/previous-versions/visualstudio/visual-studio-2008/dd639324(v=vs.90))
