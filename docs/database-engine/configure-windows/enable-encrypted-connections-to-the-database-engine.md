---
title: "Enable encrypted connections"
description: Encrypt data across communication channels. Enable encrypted connections for an instance of the SQL Server Database Engine by using SQL Server Configuration Manager.
author: VanMSFT
ms.author: vanto
ms.date: "03/31/2022"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
ms.custom: contperf-fy20q4
helpviewer_keywords:
  - "connections [SQL Server], encrypted"
  - "SSL [SQL Server]"
  - "Secure Sockets Layer (SSL)"
  - "TLS [SQL Server]"
  - "Transport Layer Security (TLS)"
  - "encryption [SQL Server], connections"
  - "cryptography [SQL Server], connections"
  - "certificates [SQL Server], installing"
  - "requesting encrypted connections"
  - "installing certificates"
  - "security [SQL Server], encryption"
  - "TLS certificates"
---
# Enable encrypted connections to the Database Engine

 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Learn how to encrypt data across communication channels.  You enable encrypted connections for an instance of the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)] and use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager to specify a certificate.
 
 The server computer must have a certificate provisioned. To provision the certificate on the server computer, you [import it into Windows](#single-server). The client machine must be set up to [trust the certificate's root authority](#about).  
  
> [!IMPORTANT]
> Starting with [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], Secure Sockets Layer (SSL) has been discontinued. Use Transport Layer Security (TLS) instead.

## Transport Layer Security (TLS)

[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] can use Transport Layer Security (TLS) to encrypt data that is transmitted across a network between an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a client application. The TLS encryption is performed within the protocol layer and is available to all supported [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients.

TLS can be used for server validation when a client connection requests encryption. If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running on a computer that has been assigned a certificate from a public certification authority, identity of the computer and the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is vouched for by the chain of certificates that lead to the trusted root authority. Such server validation requires that the computer on which the client application is running be configured to trust the root authority of the certificate that is used by the server. For more information about server certificates and encryption, see [Using TrustServerCertificate](/dotnet/framework/data/adonet/connection-string-syntax#using-trustservercertificate).


Encryption with a self-signed certificate is possible and is described in the following section, but a self-signed certificate offers only limited protection.
The level of encryption used by TLS, 40-bit or 128-bit, depends on the version of the Microsoft Windows operating system that is running on the application and database computers.

> [!WARNING]
> Usage of 40-bit encryption level is considered unsafe.
>
> TLS connections that are encrypted by using a self-signed certificate do not provide strong security. They are susceptible to man-in-the-middle attacks. You should not rely on TLS using self-signed certificates in a production environment or on servers that are connected to the Internet.

Enabling TLS encryption increases the security of data transmitted across networks between instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and applications. However, when all traffic between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and a client application is encrypted using TLS, the following additional processing is required:
-  An extra network roundtrip is required at connect time.
-  Packets sent from the application to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be encrypted by the client TLS stack and decrypted by the server TLS stack.
-  Packets sent from the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to the application must be encrypted by the server TLS stack and decrypted by the client TLS stack.

## <a name="about"></a> About certificates

 The certificate must be issued for **Server Authentication**. The name of the certificate must be the fully qualified domain name (FQDN) of the computer.  
  
 Certificates are stored locally for the users on the computer. To install a certificate for use by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you must be running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager with an account that has local administrator privileges.

 The client must be able to verify the ownership of the certificate used by the server. If the client has the public key certificate of the certification authority that signed the server certificate, no further configuration is necessary. [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows includes the public key certificates of many certification authorities. If the server certificate was signed by a public or private certification authority for which the client doesn't have the public key certificate, you must install the public key certificate of the certification authority that signed the server certificate.  
  
> [!NOTE]  
> To use encryption with a failover cluster, you must install the server certificate with the fully qualified DNS name of the virtual server on all nodes in the failover cluster. For example, if you have a two-node cluster, with nodes named ***test1.\*\<your company>\*.com*** and ***test2.\*\<your company>\*.com***, and you have a virtual server named ***virtsql***, you need to install a certificate for ***virtsql.\*\<your company>\*.com*** on both nodes. You can set the value of the **ForceEncryption** option on the **Protocols for virtsql** property box of **SQL Server Network Configuration** to **Yes**.
>
> When creating encrypted connections for an Azure Search indexer to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on an Azure VM, see [Configure a connection from an Azure Search indexer to SQL Server on an Azure VM](/azure/search/search-howto-connecting-azure-sql-iaas-to-azure-search-using-indexers). 

## Certificate requirements

For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to load a TLS certificate, the certificate must meet the following conditions:

- The certificate must be in either the local computer certificate store or the current user certificate store.

- The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Service Account must have the necessary permission to access the TLS certificate.

- The current system time must be after the **Valid from** property of the certificate and before the **Valid to** property of the certificate.

  > [!NOTE]  
  > Certificate validity is evaluated when connecting to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with client connections that are initiated specifying the encryption option to true, unless overridden by the Trust Server Certificate setting. 

- The certificate must be meant for server authentication. This requires the **Enhanced Key Usage** property of the certificate to specify **Server Authentication (1.3.6.1.5.5.7.3.1)**.

- The certificate must be created by using the **KeySpec** option of **AT_KEYEXCHANGE**. This requires a legacy certificate. Usually, the certificate's key usage property (**KEY_USAGE**) will also include key encipherment (**CERT_KEY_ENCIPHERMENT_KEY_USAGE**).

- The **Subject** property of the certificate must indicate that the common name (CN) is the same as the host name or fully qualified domain name (FQDN) of the server computer or it must match the DNS suffix if using a wildcard certificate. When using the host name, the DNS suffix must be specified in the certificate. If [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  is running on a failover cluster, the common name must match the host name or FQDN of the virtual server and the certificates must be provisioned on all nodes in the failover cluster. 

- As [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] only allows one certificate to be installed on the server, if connections are made to the server using multiple domain names, these domains must be covered in the Subject Alternate Name (SAN) of the certificate. The domains in the SAN can also be wildcard domains (for example, `\*.yourcompany.com`).


- For stand-alone servers, wildcard certificates can be set using SQL Server Configuration Manager tool. However, in case of a Failover cluster, wildcard certificates cannot be selected in the tooI. To use a wildcard certificate in a Failover cluster, edit the `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQLServer\SuperSocketNetLib` registry key, and enter the thumbprint of the certificate, without spaces, to the **Certificate** value.


- [!INCLUDE[ssKilimanjaro](../../includes/ssKilimanjaro-md.md)] and the [!INCLUDE[ssKilimanjaro](../../includes/ssKilimanjaro-md.md)] Native Client (SNAC) support wildcard certificates. SNAC has since been deprecated and replaced with the [Microsoft OLE DB Driver for SQL Server](../../connect/oledb/oledb-driver-for-sql-server.md) and [Microsoft ODBC Driver for SQL Server](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md). Other clients might not support wildcard certificates.        

  > [!WARNING]  
  > [!INCLUDE[ssnoteregistry_md](../../includes/ssnoteregistry-md.md)]  

## <a name="single-server"></a>Install on single server

With [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], certificate management is integrated into the SQL Server Configuration Manager. SQL Server Configuration Manager for [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] can be used with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Refer to [Certificate Management (SQL Server Configuration Manager)](../../database-engine/configure-windows/manage-certificates.md) to add a certificate on a single [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

If using [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], and SQL Server Configuration Manager for [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] isn't available, follow these steps:

1. On the **Start** menu, select **Run**, and in the **Open** box, type **MMC** and select **OK**.  
  
2. In the MMC console, on the **File** menu, select **Add/Remove Snap-in**.  
  
3. In the **Add/Remove Snap-in** dialog box, select **Add**.  
  
4. In the **Add Standalone Snap-in** dialog box, select **Certificates**, select **Add**.  
  
5. In the **Certificates snap-in** dialog box, select **Computer account**, and then select **Finish**.  
  
6. In the **Add Standalone Snap-in** dialog box, select **Close.**  
  
7. In the **Add/Remove Snap-in** dialog box, select **OK**.  
  
8. In the **Certificates** snap-in, expand **Certificates**, expand **Personal**, and then right-click **Certificates**, point to **All Tasks**, and then select **Import**.  

9. Right-click the imported certificate, point to **All Tasks**, and then select **Manage Private Keys**. In the **Security** dialog box, add read permission for the user account used by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account.  
  
10. Complete the **Certificate Import Wizard**, to add a certificate to the computer, and close the MMC console. For more information about adding a certificate to a computer, see your Windows documentation.  

> [!IMPORTANT]
> For production environments, it is recommended to obtain a trusted certificate from a Certificate Authority.    
> For testing purposes, self-signed certificate can also be used. To create a self-signed certificate, see the [Powershell Cmdlet New-SelfSignedCertificate](/powershell/module/pki/new-selfsignedcertificate) or [certreq command](/windows-server/administration/windows-commands/certreq_1).
  
## Install across multiple servers

With [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)], certificate management is integrated into the SQL Server Configuration Manager. SQL Server Configuration Manager for [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] can be used with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Refer to [Certificate Management (SQL Server Configuration Manager)](../../database-engine/configure-windows/manage-certificates.md) to add a certificate in a Failover Cluster configuration or in an Availability Group configuration.

If using [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssSQL17](../../includes/sssql17-md.md)], and SQL Server Configuration Manager for [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)] isn't available, follow the steps in section [To provision (install) a certificate on a single server](#single-server) for every server.

## Export server certificate  
  
1. From the **Certificates** snap-in, locate the certificate in the **Certificates** / **Personal** folder, right-click the **Certificate**, point to **All Tasks**, and then select **Export**.  
  
2. Complete the **Certificate Export Wizard**, storing the certificate file in a convenient location.  
  
## Configure server

Configure the server to force encrypted connections.

> [!IMPORTANT]
> The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account must have read permissions on the certificate used to force encryption on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. For a non-privileged service account, read permissions will need to be added to the certificate. Failure to do so can cause the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service restart to fail.
  
1. In **SQL Server Configuration Manager**, expand **SQL Server Network Configuration**, right-click **Protocols for** _\<server instance>_, and then select **Properties**.  
  
2. In the **Protocols for** _\<instance name>_ **Properties** dialog box, on the **Certificate** tab, select the desired certificate from the drop-down for the **Certificate** box, and then select **OK**.  
  
3. On the **Flags** tab, in the **ForceEncryption** box, select **Yes**, and then select **OK** to close the dialog box.  
  
4. Restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service.  

> [!NOTE]
> To ensure secure connectivity between client and server, configure the client to request encrypted connections. More details are explained [later in this article](#configure-client).

## <a name="configure-client"></a>Configure client

Configure the client to request encrypted connections.

1. Copy either the original certificate or the exported certificate file to the client computer.  
  
2. On the client computer, use the **Certificates** snap-in to install either the root certificate or the exported certificate file.  
  
3. Using SQL Server Configuration Manager, right-click **SQL Server Native Client Configuration**, and then select **Properties**.  
  
4. On the **Flags** page, in the **Force protocol encryption** box, select **Yes**.  
  
## Use SQL Server Management Studio
  
To encrypt a connection from [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]:  

1. On the Object Explorer toolbar, select **Connect**, and then select **Database Engine**.  
  
2. In the **Connect to Server** dialog box, complete the connection information, and then select **Options**.  
  
3. On the **Connection Properties** tab, select **Encrypt connection**.  

## Internet Protocol Security (IPSec)
[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data can be encrypted during transmission by using IPSec. IPSec is provided by the client and server operating systems and requires no [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] configuration. For information about IPSec, see your Windows or networking documentation.

## Next steps

+ [TLS 1.2 support for Microsoft SQL Server](https://support.microsoft.com/kb/3135244)     
+ [Configure the Windows Firewall to Allow SQL Server Access](../../sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md)     
+ [Powershell Cmdlet New-SelfSignedCertificate](/powershell/module/pki/new-selfsignedcertificate)
