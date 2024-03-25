---
title: Configure TLS 1.3 on SQL Server 2022 and later
description: Explains and demonstrates how to configure and validate TLS 1.3 is configured for an instance of SQL Server.
author: x509cert
ms.author: mikehow
ms.reviewer: mikeray
ms.date: 03/11/2024
ms.service: sql
ms.subservice: security
ms.topic: conceptual
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16"
---

# Configure TLS 1.3

[!INCLUDE [sqlserver2022-and-later](../../../includes/applies-to-version/sqlserver2022-and-later.md)]

This article explains how to:

1. Configure an instance of [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] to use Transport Layer Security (TLS) 1.3 and TLS 1.2
1. Verify that the protocols are operational
1. Disable older, insecure protocols including TLS 1.0 and 1.1

## Requirements

TLS 1.3 support in [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] requires:

- Windows Server 2022
- [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] with Cumulative Update 1 or later
- The [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance uses TCP/IP as a network protocol
- A valid X.509 server certificate installed along with its private key

> [!IMPORTANT]
> This document assumes that your requirements include both TLS 1.3 and TLS 1.2 in the short term, and only TLS 1.3 in the long term.

## SQL Server and TLS

[!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] doesn't perform TLS operations itself, rather this work is performed by Windows using [Schannel SSP](/windows-server/security/tls/tls-ssl-schannel-ssp-overview). Schannel is a Security Support Provider (SSP) that contains and exposes Microsoft's implementation of Internet standard security protocols such as TLS. Schannel is to Windows what OpenSSL is to Linux.

Configuring TLS for [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] requires configuring TLS for Windows.

With [!INCLUDE [sssql22-md](../../../includes/sssql22-md.md)] on Windows Server 2022, [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] supports TLS 1.0, 1.1, 1.2 and 1.3. To verify this, use .NET code available in [GitHub at TlsTest](https://github.com/x509cert/TlsTest). The output from the tool looks like this:

```output
Trying Ssl2
Authentication failed, see inner exception.
Exception: The client and server cannot communicate, because they do not possess a common algorithm.
Trying Ssl3
Authentication failed, see inner exception.
Exception: The client and server cannot communicate, because they do not possess a common algorithm.
Trying Tls
Tls using TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA
Trying Tls11
Tls11 using TLS_ECDHE_RSA_WITH_AES_256_CBC_SHA
Trying Tls12
Tls12 using TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384
Trying Tls13
Tls13 using TLS_AES_256_GCM_SHA384
```

### Configure Windows to only use TLS 1.2 and TLS 1.3

Windows has a set of registry keys under `HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\SecurityProviders\SCHANNEL` that control TLS protocol versions as well as cipher suites. For this scenario, only the protocol versions that affect servers matter, because the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] instance acts as a server.

The following PowerShell script [updates the registry to](/powershell/scripting/samples/working-with-registry-entries) enable or disable TLS 1.0 and TLS 1.1 when used by servers:

> [!WARNING]
> Before you proceed, [back up the registry](https://support.microsoft.com/topic/how-to-back-up-and-restore-the-registry-in-windows-855140ad-e318-2a13-2829-d428a2ab0692). This will allow you to restore the registry in the future, if necessary.

:::code language="powershell" source="~/../sql-server-samples/samples/features/security/tls-1-3/set-reset-tls.ps1":::

This code is available in [GitHub](https://github.com/microsoft/sql-server-samples/blob/master/samples/features/security/tls-1-3/set-reset-tls.ps1).

Once you run this script, restart the [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] process for the new TLS settings to take effect. If you now run the code mentioned at the start of the article, it returns:

```output
Trying Ssl2
Authentication failed, see inner exception.
Exception: The client and server cannot communicate, because they do not possess a common algorithm.
Trying Ssl3
Authentication failed, see inner exception.
Exception: The client and server cannot communicate, because they do not possess a common algorithm.
Trying Tls
Received an unexpected EOF    or 0 bytes from the transport stream.
Exception:
Trying Tls11
Received an unexpected EOF or 0 bytes from the transport stream.
Exception:
Trying Tls12
Tls12 using TLS_ECDHE_RSA_WITH_AES_256_GCM_SHA384
Trying Tls13
Tls13 using TLS_AES_256_GCM_SHA384
```

Notice that SSL 2.0, SSL 3.0, TLS 1.0 and TLS 1.1 all fail to connect, but both TLS 1.2 and TLS 1.3 succeed.

After the registry update, Windows and this instance of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] only allow TLS 1.2 and TLS 1.3 connections. Later, when more clients support TLS 1.3, you can also disable TLS 1.2.

### Set SQL Server instance to force strict encryption

The final step is to set the instance to use `Force Strict Encryption`. With `Force Strict Encryption`, the SQL instance uses a supported version of tabular data stream (TDS 8.0 or later).

Use [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)] Configuration Manager to set this setting.

1. Expand **SQL Server Network Configuration**

1. Right-click **Protocols for** `<instance name>`, and select **Properties**

   A default instance name is **MSSQLSERVER**.

1. On the **Flags** tab, set **Force Strict Encryption** to **Yes**

   :::image type="content" source="media/connect-with-tls-1-3/configuration-manager.png" alt-text="Screenshot of the UI control for SQL Server Configuration Manager, configure protocols dialog.":::

## Verify security

This section demonstrates how to use Wireshark, OpenSSL, and Nmap to verify encryption.

### Wireshark

You can use network sniffers to determine the TLS protocol version and agreed-upon cipher suite. You might find some data confusing. If you look at the screen-shot below from Wireshark, it shows the packet is a TLS v1.3 Record Layer, but the protocol version is TLS 1.2 and the Handshake Protocol version is also TLS 1.2. This is all part of the TLS 1.2 specification and is correct and expected. The agreed upon protocol version is in the Extensions section, and as you can see, **supported_versions** is TLS 1.3.

:::image type="content" source="media/connect-with-tls-1-3/tls-extensions-description.png" alt-text="Screenshot of the TLS extension section.":::

### OpenSSL

You can also use openssl to discover the agreed-upon TLS information. 

Use the following command:

```console
openssl s_client 127.0.0.1:1433

```

The command returns results like:

```output
Post-Handshake New Session Ticket arrived:
SSL-Session:
   Protocol   : TLSv1.3
   Cipher     : TLS_AES_256_GCM_SHA384
   Session-ID : 516D56D99088BCDE1 <snip> 098EDB1A
   Session-ID-ctx:
   Resumption PSD: B2B9CB92B59aa1 <snip> BD824CBA
   PSK identity: None
```

### Nmap

The current version of Nmap, version 7.94, appears to not detect TLS 1.3 when using:

`nmap -sV --script ssl-enum-ciphers -p 1433 127.0.0.1`.

## Related content

- [Connect to SQL Server with strict encryption](connect-with-strict-encryption.md)
- [Transport Layer Security and digital certificates](../../../database-engine/configure-windows/certificate-overview.md)
- [Certificate requirements for SQL Server](../../../database-engine/configure-windows/certificate-requirements.md)
- [Configure SQL Server Database Engine for encrypting connections](../../../database-engine/configure-windows/configure-sql-server-encryption.md)
- [TLS 1.3 support](tls-1-3.md)
- [TDS 8.0](tds-8.md)
- [Transport Layer Security (TLS) registry settings](/windows-server/security/tls/tls-registry-settings?tabs=diffie-hellman)
