---
title: Certificate Requirements for SQL Server
description: This article describes the requirements for SQL Server encryption and how to check if a certificate meets the requirements.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 02/27/2026
ai-usage: ai-assisted
ms.service: sql
ms.subservice: configuration
ms.topic: concept-article
---

# Certificate requirements for SQL Server

This article describes certificate requirements for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and how to check if a certificate meets these requirements.

## Certificate requirements for SQL Server encryption

For using Transport Layer Security (TLS) for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] encryption, you need to provision a certificate (one of the three digital types) that meets the following conditions:

- The certificate must be in either the local computer certificate store or the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service account certificate store. Use the local computer certificate store to avoid reconfiguring certificates when the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] startup account changes.

- The [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service account must have the necessary permission to access the TLS certificate. For more information, see [Encrypt connections to SQL Server by importing a certificate](configure-sql-server-encryption.md).

- The current system time must be after the value of the property **Valid from** and before the value of the property **Valid to** of the certificate. For more information, see [Expired Certificates](#expired-certificates).

  > [!NOTE]  
  > The certificate must be meant for server authentication. This requires the **Enhanced Key Usage** property of the certificate to specify **Server Authentication (1.3.6.1.5.5.7.3.1)**.

- The certificate must be created using the `KeySpec` option of `AT_KEYEXCHANGE`. This requires a certificate that uses a [legacy Cryptographic Storage Provider](/windows/win32/seccertenroll/cryptoapi-cryptographic-service-providers) to store the private key. Usually, the certificate's key usage property (**KEY_USAGE**) also includes key encipherment (`CERT_KEY_ENCIPHERMENT_KEY_USAGE`) and a digital signature (`CERT_DIGITAL_SIGNATURE_KEY_USAGE`).

- The **Subject Alternative Name** should include all the names your clients might use to connect to a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

The client must be able to verify the ownership of the certificate used by the server. If the client has the public key certificate of the certification authority that signed the server certificate, no further configuration is necessary. Microsoft Windows includes the public key certificates of many certification authorities. If the server certificate was signed by a public or private certification authority for which the client doesn't have the public key certificate, you must install the public key certificate of the certification authority that signed the server certificate on each client that is going to connect to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] won't start if a certificate exists in the computer store, but only meets some requirements in the above list and if it's manually configured for use by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager or through registry entries. Select another certificate that meets all the requirements or remove the certificate from being used by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] until you can provision one that meets requirements or use a self-generated certificate as discussed in [SQL Server generated self-signed certificates](configure-sql-server-encryption.md#sql-server-generated-self-signed-certificates).

### Always On availability group

If your SQL Server instance is part of an [Always On availability group](../availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md), you can use one of the following methods to create your certificate: 

- **Method 1: Use one certificate on all replicas of the availability group**. The common name is arbitrary so can be any placeholder value. The hostname and the FQDN of all the SQL Server replicas in the availability group, and the availability group listener names, should be included in the **Subject Alternative Name** of the certificate. If additional replicas are added to the availability group after the original certificate is generated, then the certificate must be regenerated with the names of all the replicas and [reimported](configure-sql-server-encryption.md) to each replica. The certificate must also be [imported](certificate-procedures.md) to the certificate store on all the clients that connect to the availability group replica or availability group listener, unless the certificate is signed by a public or official Certificate Authority (CA). If you don't include the availability group replicas and listener names in the certificate, then you need to include one of values of the **Subject Alternative Name** in the `HostNameInCertificate` or the path to the certificate in the `ServerCertificate` values of the connection string when connecting to the availability group. Specifying names in the certificate is the recommended approach. 

   The following is an example of the properties that define a correctly configured certificate for an availability group with two servers named `test1.<your company>.com` and `test2.<your company>.com`, and an availability group listener named `aglistener.<your company>.com`:
   
   ```certificate-config
   CN = <hostname is recommended but not required when certificates are configured using SQL Server Configuration Manager>
   DNS Name = aglistener.<your company>.com 
   DNS Name = test1.<your company>.com
   DNS Name = test2.<your company>.com
   DNS Name = aglistener
   DNS Name = test1
   DNS Name = test2
   ```

- **Method 2: Use a separate certificate for each replica of the availability group.** Adding replicas to an availability group after a certificate is generated is easier when using separate certificates as you only need to generate a certificate for the new replica, instead of modifying all the certificates on all of the existing replicas. The common name is arbitrary so can be any placeholder value. The hostname and FQDN of the respective SQL Server instance and the availability group listener names ***must be*** included in the **Subject Alternative Name** of the certificate for each respective replica. [Import](certificate-procedures.md) each certificate to its respective replica, and, unless the certificate is signed by a public or official Certificate Authority (CA), then [import](certificate-procedures.md) *all* of the certificates to all the certificate stores on all the clients that connect to the replicas, or availability group listener.

   The following are examples of the properties that define correctly configured certificates for an availability group with two instances named `test1.<your company>.com` and `test2.<your company>.com`, and an availability group listener named `aglistener.<your company>.com`:
   :::row:::
       :::column:::
           Certificate on test1: 
           ```certificate-config
           CN= <hostname is recommended but not required when certificates are configured using SQL Server Configuration Manager>
           DNS Name= test1.<your company>.com 
           DNS Name= aglistener.<your company>.com
           DNS Name= aglistener
           DNS Name= test1
           ``` 
       :::column-end:::
       :::column:::
           Certificate on test2:
           ```certificate-config
           CN= <hostname is recommended but not required when certificates are configured using SQL Server Configuration Manager>
           DNS Name= test2.<your company>.com 
           DNS Name= aglistener.<your company>.com 
           DNS Name= aglistener
           DNS Name= test2 
           ``` 
       :::column-end:::
   :::row-end:::


### Failover cluster instance

If [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is configured as a [failover cluster instance](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md), you must install the server certificate with the hostname, or fully qualified DNS name (FQDN) of the virtual server on all nodes in the failover cluster. and the certificates must be provisioned on all nodes of the failover cluster. For example, if you have a two node cluster, with nodes named `test1.<your company>.com` and `test2.<your company>.com`, and you have a virtual server named *virtsql*, you need to install a certificate for `virtsql.<your company>.com` on both nodes. 

Import the certificate to the failover cluster in the sequence documented in [Configure SQL Server Database Engine for encrypting connections](configure-sql-server-encryption.md#extra-procedure-for-failover-cluster-instances). 

The following is an example of the properties that define a correctly configured certificate for a failover cluster instance:

```certificate-config
CN = virtsql.<your company>.com
DNS Name = virtsql.<your company>.com
DNS Name = virtsql
```

For more information on SQL clusters, see [Before Installing Failover Clustering](../../sql-server/failover-clusters/install/before-installing-failover-clustering.md).

## Check if a certificate meets the requirements

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager automatically validates all certificate requirements during the configuration phase itself. If [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] successfully starts after you configure a certificate, it's a good indication that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] can use that certificate. But some client applications might still have other requirements for certificates that can be used for encryption, and you might experience different errors depending on the application being used. In that scenario, you need to check the client application's support documentation for more information on the subject.

### Verify KeySpec and Key Usage

The `KeySpec` requirement (`AT_KEYEXCHANGE`) is a common cause of certificate configuration failures. Use the following methods to verify that your certificate meets this requirement.

#### Use certutil

Run `certutil` with the `-v` option to display detailed certificate properties, including `KeySpec` and `Key Usage`:

```cmd
certutil -v -store My "<certificate_thumbprint>"
```

In the output, look for the following values:

```output
KeySpec = 1 -- AT_KEYEXCHANGE
Key Usage = Key Encipherment, Digital Signature (a0)
Enhanced Key Usage:
    Server Authentication (1.3.6.1.5.5.7.3.1)
```

If `KeySpec = 2` (`AT_SIGNATURE`), the certificate can't be used for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] encryption.

#### Use PowerShell

Run the following PowerShell commands to check `KeySpec` for certificates in the local computer store:

```powershell
Get-ChildItem Cert:\LocalMachine\My | ForEach-Object {
    $cert = $_
    $key = $cert.PrivateKey
    [PSCustomObject]@{
        Subject   = $cert.Subject
        Thumbprint = $cert.Thumbprint
        KeySpec   = if ($key) { $key.CspKeyContainerInfo.KeyNumber } else { 'No private key' }
        NotAfter  = $cert.NotAfter
    }
} | Format-Table -AutoSize
```

Verify that `KeySpec` shows `Exchange` (corresponding to `AT_KEYEXCHANGE`). If it shows `Signature`, request a new certificate with the correct `KeySpec` setting.

### Create a certificate using AD CS

If your organization uses Active Directory Certificate Services (AD CS) as an internal certificate authority (CA), create a certificate that meets [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] requirements by following these steps:

1. Open the **Certificates** MMC snap-in for the local computer (`certlm.msc`).
1. Expand **Personal**, right-click **Certificates**, and select **All Tasks** > **Request New Certificate**.
1. Select **Active Directory Enrollment Policy** and select **Next**.
1. Choose a certificate template that supports server authentication. A **Web Server** or custom template configured for server authentication typically meets the requirements. Verify with your CA administrator that the template uses a legacy Cryptographic Service Provider (CSP) with `KeySpec = AT_KEYEXCHANGE`, not a Key Storage Provider (KSP).
1. On the **Certificate Properties** page:
   - Set the **Common Name (CN)** to the hostname or FQDN of your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.
   - On the **Subject Alternative Name** tab, add DNS entries for all hostnames that clients use to connect (hostname, FQDN, and any aliases).
1. Complete the enrollment wizard and verify the new certificate appears in **Personal** > **Certificates**.
1. Verify the `KeySpec` by using certutil or PowerShell as described in [Verify KeySpec and Key Usage](#verify-keyspec-and-key-usage).

> [!IMPORTANT]
> Certificates created with a Key Storage Provider (KSP), such as the **Microsoft Software Key Storage Provider**, use `KeySpec = 0` and aren't compatible with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. When creating your certificate template in AD CS, select a legacy CSP like **Microsoft RSA SChannel Cryptographic Provider** to ensure `KeySpec = AT_KEYEXCHANGE`.

You can use one of the following methods to check the validity of the certificate for use with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

- **sqlcheck tool**: `sqlcheck` is a command-line tool that examines the current computer and service account settings and produce a text report to the Console window that is useful for troubleshooting various connection errors. The output has the following information regarding certificates:

  ```output
  Details for SQL Server Instance: This Certificate row in this section provides more details regarding the certificate being used by SQL Server (Self-generated, hard-coded thumbprint value, etc.).

  Certificates in the Local Computer MY Store: This section shows detailed information regarding all the certificates found in the computer certificate store.
  ```

  For more information on the tool's capabilities and for download instructions, see [Welcome to the CSS_SQL_Networking_Tools wiki](https://github.com/microsoft/CSS_SQL_Networking_Tools/wiki/).

- **certutil tool**: `certutil.exe` is a command-line program, installed as part of Certificate Services. You can use certutil.exe to dump and display certificate information. Use the `-v` option to get detailed information. For more information, see [certutil](/windows-server/administration/windows-commands/certutil).

- **Certificates snap-in**: You can also use the **Certificates snap-in** window to view more information about certificates in various certificate stores on the computer. But this tool doesn't show `KeySpec` information. For more information on how to view certificates with the MMC snap-in, see [How to: View certificates with the MMC snap-in](/dotnet/framework/wcf/feature-details/how-to-view-certificates-with-the-mmc-snap-in).

<a id="importing-a-certificate-with-a-different-name-to-the-hostname"></a>

## Import a certificate with a different name to the hostname

Currently, you can only import a certificate using the SQL Server Configuration Manager if the subject name of the certificate matches the hostname of the computer.

If you want to use a certificate with a different subject name, follow these steps:

1. Import the certificate to the local computer certificate store using the [Certificates snap-in](/dotnet/framework/wcf/feature-details/how-to-view-certificates-with-the-mmc-snap-in).
1. In SQL Server Configuration Manager, expand **SQL Server Network Configuration** right-click the instance of SQL Server, and then choose Properties to open the **Protocols for <instance_name> Properties** dialog box.
1. On the **Certificate** tab, select the certificate you imported to the certificate store from the **Certificate** dropdown list:

   :::image type="content" source="media/certificate-requirements/certificate.png" alt-text="Screenshot of the certificate tab of the properties dialog box in SQL Server Configuration Manager.":::

Importing a certificate with a different name to the hostname results in the following error message:

```text
The selected certificate name does not match FQDN of this hostname. This property is required by SQL Server
Certificate name: random-name
Computer name: sqlserver.domain.com
```

## Expired certificates

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] only checks the validity of the certificates at the time of configuration. For example, you can't use SQL Server Configuration Manager on [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, to provision an expired certificate. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] continues to run without problems if the certificate expires after it's already provisioned. But, some client applications like Power BI check the validity of the certificate on each connection and raise an error if the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance is configured to use an expired certificate for encryption. We recommend that you don't use an expired certificate for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] encryption.

## Next step

- [Configure the SQL Server Database Engine to encrypt connections](configure-sql-server-encryption.md) by importing a certificate. 

## Related content

- [TLS 1.3 support](../../relational-databases/security/networking/tls-1-3.md)
- [Configure TLS 1.3](../../relational-databases/security/networking/connect-with-tls-1-3.md)
