---
title: Configure SQL Server Database Engine for encryption
description: This article describes how to configure a SQL Server instance to enable encrypted connections.
author: sevend2
ms.author: v-sidong
ms.reviewer: sureshka, randolphwest
ms.date: 04/18/2024
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
---
# Configure SQL Server Database Engine for encrypting connections

You can encrypt all incoming connections to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or enable encryption for just a specific set of clients. For either of these scenarios, you first have to configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to use a certificate that meets [Certificate requirements for SQL Server](certificate-requirements.md) before taking additional steps on the server computer or client computers to encrypt data.

This article describes how to configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for certificates ([Step 1](#step-1-configure-sql-server-to-use-certificates)) and change encryption settings of the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance ([Step 2](#step-2-configure-encryption-settings-in-sql-server)). Both steps are required to encrypt all incoming connections to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] when using a certificate from a public commercial authority. For other scenarios, see [Special cases for encrypting connections to SQL Server](special-cases-for-encrypting-connections-sql-server.md).

## Step 1: Configure SQL Server to use certificates

To configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to use the certificates described in [Certificate requirements for SQL Server](certificate-requirements.md#certificate-requirements-for-sql-server-encryption), follow these steps:

1. Install the certificate on the computer that's running [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].
1. Configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to use the installed certificate.

Depending on the version of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager you have access to on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] computer, use one of the following procedures to install and configure the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance.

### Computers with SQL Server Configuration Manager for SQL Server 2019 and later versions

In [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions, certificate management is integrated into [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, and can be used with earlier versions of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. To add a certificate on a single [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance, in a failover cluster configuration, or in an availability group configuration, see [Certificate management (SQL Server Configuration Manager)](manage-certificates.md). The Configuration Manager greatly simplifies certificate management by taking care of installing the certificate and configuring [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] for using the installed certificate with just a few steps.

Certificates are stored locally for the users on the computer. To install a certificate for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to use, you must run [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager with an account that has local administrator privileges.

You can temporarily install an Express edition of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] or a later version to use [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, which supports integrated certificate management.

### Computers with SQL Server Configuration Manager for SQL Server 2017 and earlier versions

If you use [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] or an earlier version, and [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager for [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] isn't available, follow these steps to install and configure the certificate on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] computer:

1. On the **Start** menu, select **Run**, and in the **Open** box, type *MMC* and select **OK**.
1. In the **MMC** console, on the **File** menu, select **Add/Remove Snap-in...**.
1. In the **Add or Remove Snap-ins** dialog box, select **Certificates**, and then select **Add**.
1. In the **Certificates snap-in** dialog box, select **Computer account**, and then select **Next** > **Finish**.
1. In the **Add or Remove Snap-ins** dialog box, select **OK**.
1. In the **MMC** console, expand **Certificates (Local Computer)** > **Personal**, right-click **Certificates**, point to **All Tasks**, and select **Import**.
1. Complete the **Certificate Import Wizard** to add a certificate to the computer.
1. In the **MMC** console, right-click the imported certificate, point to **All Tasks**, and select **Manage Private Keys**. In the **Security** dialog box, add read permission for the user account used by the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service account.
1. In **SQL Server Configuration Manager**, expand **SQL Server Network Configuration**, right-click **Protocols for \<server instance>**, and select **Properties**.
1. In the **Protocols for \<instance name> Properties** dialog box, on the **Certificate** tab, select the desired certificate from the dropdown list for the **Certificate** box, and then select **OK**.
1. If you require all the connections to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to be encrypted, see [Step 2: Configure encryption settings in SQL Server](#step-2-configure-encryption-settings-in-sql-server). If you only want to enable encryption for specific clients, restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service and see [Special cases for encrypting connections to SQL Server](special-cases-for-encrypting-connections-sql-server.md).

> [!NOTE]  
> To install certificates in the availability group configuration, repeat the above procedure on each node in your availability group, starting with the primary node.

> [!IMPORTANT]  
> The [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service account must have read permissions on the certificate used to force encryption on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance. For a non-privileged service account, read permissions must be added to the certificate. Failure to do so can cause the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service restart to fail.

#### Extra procedure for failover cluster instances

The certificate used by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to encrypt connections is specified in the following registry key:

`HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL.x\MSSQLServer\SuperSocketNetLib\Certificate`

This key contains a property of the certificate known as a thumbprint, which identifies each certificate in the server. In a clustered environment, this key is set to `Null` even though the correct certificate exists in the store. To resolve this issue, you must take these extra steps on each of your cluster nodes after you install the certificate to each node:

1. Navigate to the certificate store where the fully qualified domain name (FQDN) certificate is stored. On the properties page for the certificate, go to the **Details** tab and copy the thumbprint value of the certificate to a **Notepad** window.
1. Remove the spaces between the hex characters in the thumbprint value in **Notepad**.
1. Start **Registry Editor**, navigate to the following registry key, and paste the value from Step 2:

   `HKLM\SOFTWARE\Microsoft\Microsoft SQL Server\<instance>\MSSQLServer\SuperSocketNetLib\Certificate`

1. If the SQL virtual server is currently on this node, fail over to another node in your cluster and restart the node where the registry change occurred.
1. Repeat this procedure on all the nodes.

> [!WARNING]  
> Incorrectly editing the registry can severely damage your system. Before making changes to the registry, we recommend you back up any valued data on the computer.

> [!NOTE]  
> [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] and [!INCLUDE [sql2008r2-md](../../includes/sql2008r2-md.md)] Native Client (SNAC) support wildcard certificates. SNAC has since been deprecated and replaced with the [Microsoft OLE DB Driver for SQL Server](../../connect/oledb/oledb-driver-for-sql-server.md) and [Microsoft ODBC Driver for SQL Server](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md). Other clients might not support wildcard certificates.

Wildcard certificate can't be selected by using [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager. To use a wildcard certificate, you must edit the `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQLServer\SuperSocketNetLib` registry key, and enter the thumbprint of the certificate, without spaces, to the **Certificate** value.

> [!NOTE]  
> To use encryption with a failover cluster, you must install the server certificate with the fully qualified DNS name of the virtual server on all nodes in the failover cluster. You can set the value of the **ForceEncryption** option on the **Protocols for virtsql** property box of **SQL Server Network Configuration** to **Yes**.

When creating encrypted connections for an Azure Search indexer to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on an Azure VM, see [Indexer connections to a SQL Server instance on an Azure virtual machine](/azure/search/search-howto-connecting-azure-sql-iaas-to-azure-search-using-indexers).

## Step 2: Configure encryption settings in SQL Server

The following steps are only required if you want to force encrypted communications for all the clients:

1. In [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Configuration Manager, expand **SQL Server Network Configuration**, right-click **Protocols for \<server instance>**, and then select **Properties**.
1. On the **Flags** tab, in the **ForceEncryption** box, select **Yes**, and then select **OK** to close the dialog box.
1. Restart the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] service.

> [!NOTE]  
> Some certificate scenarios might require you to implement additional steps on the client computer and in your client application to ensure encrypted connections between the client and server. For more information, see [Special cases for encrypting connections to SQL Server](special-cases-for-encrypting-connections-sql-server.md).

## More information

### Login packet encryption vs. data packet encryption

At a high level, there are two types of packets in the network traffic between a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] client application and [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]: credential packets (login packets) and data packets. When you configure encryption (either server-side or client-side), both these packet types are always encrypted. But, even when you don't configure encryption, the credentials (in the login packet) that are transmitted when a client application connects to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] are always encrypted. [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses a certificate that meets the certificate requirements from a trusted certification authority if available. This certificate is either manually configured by the system administrator, using one of the procedures previously discussed in the article, or present in the certificate store on the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] computer.

### SQL Server-generated self-signed certificates

[!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] uses a certificate from a trusted certification authority if available for encrypting login packets. If a trusted certificate isn't installed, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] generates a self-signed certificate (fallback certificate) during startup and use that self-signed certificate to encrypt the credentials. This self-signed certificate helps increase security, but it doesn't protect against identity spoofing by the server. If the self-signed certificate is used, and the value of the **ForceEncryption** option is set to **Yes**, all data transmitted across a network between [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and the client application is encrypted using the self-signed certificate.

When you use a self-signed certificate, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] logs the following message to the error log:

> A self-generated certificate was successfully loaded for encryption.

[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and earlier versions use the SHA1 algorithm. However, the SHA1 algorithm and many older algorithms are deprecated beginning with [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)]. For more information, see [Deprecated Database Engine features in SQL Server 2016](../deprecated-database-engine-features-in-sql-server-2016.md).

In these environments, if you're using the automatically generated self-signed certificate generated by [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], either just for the prelogin handshake or for encrypting all server-client communications, your vulnerability detection software or security software or company policies might flag this use as a security issue. You have the following options for these scenarios:

- Create a new self-signed certificate or a third-party certificate that uses stronger encryption algorithms and configure [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] to use this new certificate.
- Since you now understand the reason for the flag, you can ignore the message (not recommended).
- Upgrade to [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] or a later version that uses a stronger hash algorithm (SHA256) for self-signed certificates.

### PowerShell script to create self-signed certificate for SQL Server

The following code snippet can be used to create a self-signed certificate on a computer running [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. The certificate meets requirements for encryption for a stand-alone [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance and is saved in the local computer's certificate store (PowerShell must be launched as an administrator):

```powershell
# Define parameters
$certificateParams = @{
    Type = "SSLServerAuthentication"
    Subject = "CN=$env:COMPUTERNAME"
    DnsName = @("{0}" -f [System.Net.Dns]::GetHostByName($env:computerName).HostName, 'localhost')
    KeyAlgorithm = "RSA"
    KeyLength = 2048
    HashAlgorithm = "SHA256"
    TextExtension = "2.5.29.37={text}1.3.6.1.5.5.7.3.1"
    NotAfter = (Get-Date).AddMonths(36)
    KeySpec = "KeyExchange"
    Provider = "Microsoft RSA SChannel Cryptographic Provider"
    CertStoreLocation = "cert:\LocalMachine\My"
}

# Call the cmdlet
New-SelfSignedCertificate @certificateParams
```

### Verify network encryption

To verify that network encryption is configured and enabled successfully, run the following Transact-SQL query:

```sql
USE [master];
GO

SELECT DISTINCT (encrypt_option)
FROM sys.dm_exec_connections;
GO
```

The `encrypt_option` column is a Boolean value indicating whether encryption is enabled for this connection. If the value is `TRUE`, the connection is securely encrypted. If the value is `FALSE`, the connection isn't encrypted.

### SQL Server certificate behavior with permissions

The SQL Server service detects and uses the certificate automatically for encryption if all of the following conditions are true:

- The certificate has a subject that contains the FQDN of the machine
- The certificate is installed in the Local Computer's certificate store
- The SQL Server service account is granted access to the certificate's private key

This use happens even if the certificate isn't selected in SQL Server Configuration Manager.

To override this behavior, either:

- Configure another certificate to be used in the SQL Server Configuration Manager

  or

- Remove the SQL Server service account permissions to the undesired certificate
