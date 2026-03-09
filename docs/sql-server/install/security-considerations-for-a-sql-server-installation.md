---
title: "Security Considerations"
description: This article discusses some security best practices that you should consider both before and after you install SQL Server.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 08/14/2025
ms.service: sql
ms.subservice: security
ms.topic: best-practice
helpviewer_keywords:
  - "firewall systems [SQL Server]"
  - "server message blocks [SQL Server]"
  - "disabling protocols"
  - "security [SQL Server], installations"
  - "protocols [SQL Server], disabling"
  - "NetBIOS [SQL Server]"
  - "services [SQL Server], security"
  - "SMB [SQL Server]"
  - "isolating services [SQL Server]"
  - "Setup [SQL Server], security"
  - "low SQL Server installation privileges"
  - "NTFS [SQL Server]"
  - "physical security [SQL Server]"
  - "file system security [SQL Server]"
  - "installing SQL Server, security"
---
# Security considerations for a SQL Server installation

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Security is important for every product and every business. By following simple best practices, you can avoid many security vulnerabilities. This article discusses some security best practices that you should consider both before you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and after you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Security guidance for specific features is included in the reference articles for those features.

## Before installing SQL Server

Follow these best practices when you set up the server environment:

- [Enhance physical security](#enhance-physical-security)
- [Use firewalls](#use-firewalls)
- [Isolate services](#isolate-services)
- [Configure a secure file system](#configure-a-secure-file-system)
- [Disable NetBIOS and server message block](#disable-netbios-and-server-message-block)
- [Install SQL Server on a domain controller](#install-sql-server-on-a-domain-controller)

<a id="physical_security"></a>

### Enhance physical security

Physical and logical isolation form the foundation of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] security. To enhance the physical security of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation, complete the following tasks:

- Place the server in a room accessible only to authorized persons.

- Place computers that host a database in a physically protected location, ideally a locked computer room with monitored flood detection and fire detection or suppression systems.

- Install databases in the secure zone of the corporate intranet and don't connect your SQL Server instances directly to the Internet.

- Back up all data regularly and secure the backups in an off-site location.

<a id="firewalls"></a>

### Use firewalls

Firewalls are important for securing the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation. Firewalls are most effective if you follow these guidelines:

- Put a firewall between the server and the Internet. Enable your firewall. If your firewall is turned off, turn it on. If your firewall is turned on, don't turn it off.

- Divide the network into security zones separated by firewalls. Block all traffic, and then selectively admit only what is required.

- In a multitier environment, use multiple firewalls to create screened subnets.

- When you install the server inside a Windows domain, configure interior firewalls to allow Windows Authentication.

- If your application uses distributed transactions, you might have to configure the firewall to allow [!INCLUDE [msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) traffic to flow between separate MS DTC instances. You must also configure the firewall to allow traffic to flow between the MS DTC and resource managers such as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

For more information about the default Windows Firewall settings, and a description of the TCP ports that affect the [!INCLUDE [ssDE](../../includes/ssde-md.md)], [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)], and [!INCLUDE [ssISnoversion](../../includes/ssisnoversion-md.md)], see [Configure the Windows Firewall to allow SQL Server access](configure-the-windows-firewall-to-allow-sql-server-access.md).

<a id="isolated_services"></a>

### Isolate services

Isolating services reduces the risk that one compromised service could be used to compromise others. To isolate services, consider the following guidelines:

- Run separate [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services under separate Windows accounts. Whenever possible, use separate, low-rights Windows or Local user accounts for each [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

<a id="sa_with_least_privileges"></a>

### Configure a secure file system

Using the correct file system increases security. For [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installations, complete the following tasks:

Use the NT file system (NTFS) or Resilient File System (ReFS). NTFS and ReFS are the recommended file systems for installations of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] because they're more stable and recoverable than FAT32 file systems. NTFS or ReFS also enable security options like file and directory access control lists (ACLs). NTFS also supports Encrypting File System (EFS) - file encryption. During installation, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] sets appropriate ACLs on registry keys and files if it detects NTFS. Don't change these permissions. Future releases of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] might not support installation on computers with FAT file systems.

> [!NOTE]  
> If you use EFS, database files are encrypted under the identity of the account running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Only this account can decrypt the files. If you must change the account that runs [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], first decrypt the files under the old account and then re-encrypt them under the new service account.

> [!WARNING]  
> Using file encryption through EFS might lead to slower I/O performance because encryption causes asynchronous I/O to become synchronous. See [Asynchronous disk I/O appears as synchronous on Windows](/troubleshoot/windows/win32/asynchronous-disk-io-synchronous#ntfs-encryption). Instead, consider using SQL Server encryption technologies like [Transparent data encryption (TDE)](../../relational-databases/security/encryption/transparent-data-encryption.md), [Always Encrypted](../../relational-databases/security/encryption/always-encrypted-database-engine.md), and column-level encryption [Cryptographic functions](../../t-sql/functions/cryptographic-functions-transact-sql.md).

<a id="disabled_protocols"></a>

### Disable NetBIOS and Server Message Block

Disable all unnecessary protocols on servers in the perimeter network, including NetBIOS and server message block (SMB).

NetBIOS uses the following ports:

- UDP/137 (NetBIOS name service)
- UDP/138 (NetBIOS datagram service)
- TCP/139 (NetBIOS session service)

SMB uses the following ports:

- TCP/139
- TCP/445

Web servers and Domain Name System (DNS) servers don't require NetBIOS or SMB. On these servers, disable both protocols to reduce the threat of user enumeration.

<a id="Install_DC"></a>

### Install SQL Server on a domain controller

For security reasons, don't install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a domain controller. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup doesn't block installation on a computer that is a domain controller, but the following limitations apply:

- You can't run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services on a domain controller under a local service account.

- After you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer, you can't change the computer from a domain member to a domain controller. You must uninstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain controller.

- After you install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a computer, you can't change the computer from a domain controller to a domain member. You must uninstall [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain member.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instances aren't supported where cluster nodes are domain controllers.

- [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup can't create security groups or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service accounts on a read-only domain controller. In this scenario, Setup fails.

### Ensure required user rights are assigned for successful installation

Setup requires that the following user rights are granted to the account under which [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is installed:

- Backup files and directories
- Debug programs
- Manage auditing and security log

These user privileges are usually granted by default to the local administrator group (`BUILTIN\Administrators`). In most cases, you don't need to take any action to assign them. However, if a security policy revokes these privileges, make sure they're correctly assigned, or [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup fails with the following error:

```output
The account that is running SQL Server Setup doesn't have one or all of the following rights: the right to back up files and directories, the right to manage auditing and the security log and the right to debug programs. To continue, use an account with both of these rights.
```

## During or after installation of SQL Server

After installation, enhance the security of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation by following these best practices regarding accounts and authentication modes:

### Service accounts

- Run [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services by using the lowest possible permissions.

- Associate [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] services with low privileged Windows local user accounts or domain user accounts.

- For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

### Authentication mode

- Require Windows Authentication for connections to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

- Use Kerberos authentication. For more information, see [Register a Service Principal Name for Kerberos connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).

### Strong passwords

- Always assign a strong password to the **sa** account.
- Always enable password policy checking for password strength and expiration.
- Always use strong passwords for all [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] logins.

> [!IMPORTANT]  
> During setup of [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] a login is added for the `BUILTIN\Users` group. This group grants all users of the computer access to the instance of [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] as a member of the public role. You can safely remove the `BUILTIN\Users` group to restrict [!INCLUDE [ssDE](../../includes/ssde-md.md)] access to computer users who have individual logins or are members of other Windows groups with logins.

## Related content

- [Hardware and software requirements for SQL Server 2022](hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [Network Protocols and Network Libraries](network-protocols-and-network-libraries.md)
- [Register a Service Principal Name for Kerberos connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md)
