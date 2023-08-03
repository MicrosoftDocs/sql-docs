---
title: "Protocols for MSSQLSERVER Properties (Flags tab)"
description: Learn how to use the Flags tab on the Protocols for MSSQLSERVER Properties dialog box, to view or specify the protocol encryption and to hide instance options.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 06/19/2023
ms.service: sql
ms.subservice: tools-other
ms.topic: conceptual
helpviewer_keywords:
  - "MSSQLSERVER property protocols"
monikerRange: ">=sql-server-2016"
---
# Protocols for MSSQLSERVER Properties (Flags tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

When a certificate is installed on the server, use the **Flags** tab on the **Protocols for MSSQLSERVER Properties** dialog box to view or specify the protocol encryption and hide instance options. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] must be restarted to enable or disable the **Force Encryption** setting.

To encrypt connections, you should provision the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)] with a certificate. If a certificate isn't installed, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] generates a self-signed certificate when the instance is started. This self-signed certificate can be used instead of a certificate from a trusted certificate authority, but it doesn't provide authentication or non-repudiation.

> [!CAUTION]  
> Transport Layer Security (TLS), previously known as Secure Sockets Layer (SSL), connections encrypted using a self-signed certificate do not provide strong security. They are susceptible to man-in-the-middle attacks. You should not rely on TLS using self-signed certificates in a production environment or on servers that are connected to the Internet.

The login process is always encrypted. When **Force Encryption** is set to **Yes**, all client/server communication is encrypted, and clients connecting to the [!INCLUDE [ssDE](../../includes/ssde-md.md)] must be configured to trust the root authority of the server certificate.

For more in formation on encryption, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

## Cluster servers

If you want to use encryption with a failover cluster, you must install the server certificate with the fully qualified DNS name of the virtual server on all nodes in the failover cluster. For example, if you have a two-node cluster, with nodes named `test1.contoso.com` and `test2.contoso.com` and a virtual server named `virtsql`, you need to install a certificate for `virtsql.contoso.com` on both nodes. You can then check the **Force Encryption** check box on the **SQL Server Configuration Manager** to configure your failover cluster for encryption.

## Options

#### Force Encryption

Encryption is a method for keeping sensitive information confidential by changing data into an unreadable form. Encryption ensures that data remains secure, even if the transmission packets are viewed during the transmission process. To use channel binding, set **Force Encryption** to **On** and configure **Extended Protection** on the **Advanced** tab.

For more information, see [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md).

#### Force Strict Encryption

**Applies to:** [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions.

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Network Configuration forces all clients to use strict as the encryption type. Any clients or features without the strict connection encryption fail to connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

To enable strict encryption, you must [add a certificate](../../database-engine/configure-windows/configure-sql-server-encryption.md) that isn't self-signed, and your application must use a driver that supports TDS 8.0. For more information, see [TDS 8.0 support](../../relational-databases/security/networking/tds-8.md).

#### Hide Instance

Prevent the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Browser Service from exposing this instance of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] to client computers that try to locate the instance by using the **Browse** button. To connect to named instances on the server, client applications must specify the protocol endpoint information. For example, the port number or the named pipe name, such as `tcp:server,5000`. For more information, see [Logging in to SQL Server](../../database-engine/configure-windows/logging-in-to-sql-server.md).

## Next steps

- [Configure SQL Server Database Engine for encrypting connections](../../database-engine/configure-windows/configure-sql-server-encryption.md)
