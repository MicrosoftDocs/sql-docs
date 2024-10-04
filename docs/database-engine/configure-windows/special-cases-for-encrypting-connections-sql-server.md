---
title: Configure client computer and application for encryption
description: Learn how to configure the client computer and application for encryption using self-signed certificates and a certificate automatically by SQL Server.
author: suresh-kandoth
ms.author: sureshka
ms.reviewer: randolphwest, vanto
ms.date: 09/17/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---

# Special cases for encrypting connections to SQL Server

The client computer must trust the server certificate so that the client can request the TLS encryption, and the certificate must already exist on the server. The most common scenario for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] encryption involves environments that:

- Forces encryption for all incoming client connections to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].
- Use certificates from a public commercial certification authority that Windows already trusts. The corresponding root certificate for the CA is installed in the [Trusted Root Certification Authorities certificate store](/windows-hardware/drivers/install/trusted-root-certification-authorities-certificate-store) on all the computers in your network.

In this scenario, you don't need to perform extra steps for successful encryption after configuring [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for encryption as per the procedure described in [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md).
This article provides the procedures for encrypting connections to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for less common scenarios that aren't covered in [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md).

> [!NOTE]  
> For a complete list of participants in the Microsoft Trusted Root Program, see [List of Participants - Microsoft Trusted Root Program](/security/trusted-root/participants-list).

## Use a certificate issued by a public commercial certificate authority and only some clients need encrypted connections

1. Configure the certificate on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] as per the procedure documented in [Configure SQL Server to use certificates](configure-sql-server-encryption.md#step-1-configure-sql-server-to-use-certificates).
1. Specify the encryption keyword in connection properties to *Yes* or *True*. For example, if you're using Microsoft ODBC Driver for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the connection string should specify `Encrypt=yes;`.

## Use a certificate issued by an internal CA or created by using New-SelfSignedCertificate or makecert

### Scenario 1: You want to encrypt all the connections to SQL Server

After completing both the procedures documented in [Step 1: Configure SQL Server to use certificates](configure-sql-server-encryption.md#step-1-configure-sql-server-to-use-certificates) and [Step 2: Configure encryption settings in SQL Server](configure-sql-server-encryption.md#step-2-configure-encryption-settings-in-sql-server) documented in [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md), use one of the following options to configure your client application for encryption.

<a id="scenario1option1"></a>**Option 1:** Configure client applications to **Trust Server Certificate**. This setting causes the client to skip the step that validates the server certificate and continue with the encryption process. For example, if you're using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Management Studio, you can select **Trust Server Certificate** on the **Options** page.

<a id="scenario1option2"></a>**Option 2:** On each client, add the certificate's issuing authority to the trusted root authority store by performing the following steps:

1. Export the certificate from a computer that's running [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] by using the procedure documented in [Export server certificate](certificate-procedures.md#export-server-certificates).
1. Import the certificate by using the procedure documented in [Export and import certificates](certificate-procedures.md).

### Scenario 2: Only some clients need encrypted connections

After configuring the certificate for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] use as documented in [Step 1: Configure SQL Server to use certificates](configure-sql-server-encryption.md#step-1-configure-sql-server-to-use-certificates) in [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md), use one of the following options to configure your client application for encryption:

<a id="scenario2option1"></a>**Option 1**: Configure client applications to trust the server certificate and specify the encryption keyword in connection properties to *Yes* or *True*. For example, if you're using Microsoft ODBC Driver for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the connection string should specify `Encrypt=Yes;Trust Server Certificate=Yes;`.

For more information about server certificates and encryption, see [Using TrustServerCertificate](/dotnet/framework/data/adonet/connection-string-syntax).

<a id="scenario2option2"></a>**Option 2**: On each client, add the certificate's issuing authority to the trusted root authority store and specify encryption parameters to *Yes* in the connection string:

1. Export the certificate from a computer that's running [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] by using the procedure documented in [Export the certificate](certificate-procedures.md#export-server-certificates) from a computer that's running [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].
1. [Import the certificate](certificate-procedures.md#add-a-private-certification-authority-ca-to-the-trusted-root-certification-authorities-certificate-store).
1. Specify the encryption keyword in connection properties to *Yes* or *True*. For example, if you're using Microsoft OLEDB Driver for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the connection string should specify *Use Encryption for Data = True;*

## Use the self-signed certificate automatically created by SQL Server

### Scenario 1: You want to encrypt all incoming connections to SQL Server

1. Enable encryption on [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] using the procedure [Step 2: Configure encryption settings in SQL Server](configure-sql-server-encryption.md#step-2-configure-encryption-settings-in-sql-server) documented in [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md).

1. Configure client applications to trust the server certificate. Trusting the server certificate causes the client to skip the step that validates the server certificate and continue with the encryption process. For example, if you're using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Management Studio, you can select **Trust Server Certificate** on the **Options** page.

### Scenario 2: Only some clients need encrypted connections

Configure client applications to trust the server certificate and specify the encryption keyword in connection properties to *Yes* or *True*. For example, if you're using Microsoft ODBC Driver for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], the connection string should specify `Encrypt=Yes;Trust Server Certificate=Yes;`.

No additional configuration is required on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for this scenario.

> [!WARNING]  
> SSL connections that're encrypted by using a self-signed certificate don't provide strong security because the length of the key in the self-signed certificates is shorter than the key in the certificates that're generated by the CA. They are susceptible to man-in-the-middle attacks. You shouldn't rely on SSL using self-signed certificates in a production environment or on servers that're connected to the Internet.

## Related content

- [SQL Server and client encryption summary](sql-server-and-client-encryption-summary.md)
