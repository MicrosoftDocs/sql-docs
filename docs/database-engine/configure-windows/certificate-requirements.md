---
title: Certificate requirements for SQL Server
description: This article describes the requirements for SQL Server encryption and how to check if a certificate meets the requirements.
author: sevend2
ms.author: v-sidong
ms.reviewer: randolphwest
ms.date: 04/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Certificate requirements for SQL Server

This article describes certificate requirements for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and how to check if a certificate meets these requirements.

## Certificate requirements for SQL Server encryption

For using TLS for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] encryption, you need to provision a certificate (one of the three digital types) that meets the following conditions:

- The certificate must be in either the local computer certificate store or the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service account certificate store. We recommend local computer certificate store as it avoids reconfiguring certificates with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] startup account changes.

- The [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service account must have the necessary permission to access the TLS certificate. For more information, see [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md).

- The current system time must be after the value of the property **Valid from** and before the value of the property **Valid to** of the certificate. For more information, see [Expired Certificates](#expired-certificates).

  > [!NOTE]  
  > The certificate must be meant for server authentication. This requires the **Enhanced Key Usage** property of the certificate to specify **Server Authentication (1.3.6.1.5.5.7.3.1)**.

- The certificate must be created using the `KeySpec` option of `AT_KEYEXCHANGE`. This requires a certificate that uses a [legacy Cryptographic Storage Provider](/windows/win32/seccertenroll/cryptoapi-cryptographic-service-providers) to store the private key. Usually, the certificate's key usage property (**KEY_USAGE**) also includes key encipherment (`CERT_KEY_ENCIPHERMENT_KEY_USAGE`) and a digital signature (`CERT_DIGITAL_SIGNATURE_KEY_USAGE`).

- The **Subject** property of the certificate must indicate that the common name (CN) is the same as the host name or fully qualified domain name (FQDN) of the server computer. When you use the host name, the DNS suffix must be specified in the certificate. If [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is running on a failover cluster, the common name must match the host name or FQDN of the virtual server, and the certificates must be provisioned on all nodes in the failover cluster. For example, if you have a two-node cluster, with nodes named `test1.*<your company>*.com` and `test2.*<your company>*.com`, and you have a virtual server named *virtsql*, you need to install a certificate for `virtsql.*<your company>*.com` on both nodes. For more information on SQL clusters, see [Before Installing Failover Clustering](../../sql-server/failover-clusters/install/before-installing-failover-clustering.md).

- When you connect to an availability group listener, the certificates that are provisioned for each participating server node in the failover cluster should also have a list of all availability group listeners set in the **Subject Alternate Name** of the certificate. For more information, see [Listeners and TLS/SSL certificates](../availability-groups/windows/listeners-client-connectivity-application-failover.md#SSLcertificates). For more information on SQL Always On, see [Connect to an Always On availability group listener](../availability-groups/windows/listeners-client-connectivity-application-failover.md).

- The **Subject Alternate Name** should include all the names your clients might use to connect to a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. If using Availability Groups, the Subject Alternate Name should include the NetBIOS and Fully Qualified Domain Name (FQDN) of the localhost and created listeners.

The client must be able to verify the ownership of the certificate used by the server. If the client has the public key certificate of the certification authority that signed the server certificate, no further configuration is necessary. Microsoft Windows includes the public key certificates of many certification authorities. If the server certificate was signed by a public or private certification authority for which the client doesn't have the public key certificate, you must install the public key certificate of the certification authority that signed the server certificate on each client that is going to connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] will not start if a certificate exists in the computer store, but only meets some requirements in the above list and if it's manually configured for use by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager or through registry entries. Select another certificate that meets all the requirements or remove the certificate from being used by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] until you can provision one that meets requirements or use a self-generated certificate as discussed in [SQL Server generated self-signed certificates](configure-sql-server-encryption.md#sql-server-generated-self-signed-certificates).

## Check if a certificate meets the requirements

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager automatically validates all certificate requirements during the configuration phase itself. If [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] successfully starts after you configure a certificate, it's a good indication that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can use that certificate. But some client applications might still have other requirements for certificates that can be used for encryption, and you might experience different errors depending on the application being used. In that scenario, you need to check the client application's support documentation for more information on the subject.

You can use one of the following methods to check the validity of the certificate for use with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

- **sqlcheck tool**: `sqlcheck` is a command-line tool that examines the current computer and service account settings and produce a text report to the Console window that is useful for troubleshooting various connection errors. The output has the following information regarding certificates:

  ```Output
  Details for SQL Server Instance: This Certificate row in this section provides more details regarding the certificate being used by SQL Server (Self-generated, hard-coded thumbprint value, etc.).

  Certificates in the Local Computer MY Store: This section shows detailed information regarding all the certificates found in the computer certificate store.
  ```

  For more information on the tool's capabilities and for download instructions, see [Welcome to the CSS_SQL_Networking_Tools wiki](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/).

- **certutil tool**: `certutil.exe` is a command-line program, installed as part of Certificate Services. You can use certutil.exe to dump and display certificate information. Use the `-v` option to get detailed information. For more information, see [certutil](/windows-server/administration/windows-commands/certutil).

- **Certificates snap-in**: You can also use the **Certificates snap-in** window to view more information about certificates in various certificate stores on the computer. But this tool doesn't show `KeySpec` information. For more information on how to view certificates with the MMC snap-in, see [How to: View certificates with the MMC snap-in](/dotnet/framework/wcf/feature-details/how-to-view-certificates-with-the-mmc-snap-in).

## More information

### Expired certificates

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only checks the validity of the certificates at the time of configuration. For example, you can't use Configuration Manager on [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, to provision an expired certificate. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] continues to run without problems if the certificate expires after it is already provisioned. But, some client applications like Power BI check the validity of the certificate on each connection and raise an error if the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance is configured to use an expired certificate for encryption. We recommend that you don't use an expired certificate for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] encryption.

## Related content

- [TLS 1.3 support](../../relational-databases/security/networking/tls-1-3.md)
- [Configure TLS 1.3](../../relational-databases/security/networking/connect-with-tls-1-3.md)
