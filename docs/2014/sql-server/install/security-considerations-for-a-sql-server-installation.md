---
title: "Security Considerations for a SQL Server Installation | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
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
ms.assetid: cf96155f-30a8-48b7-8d6b-24ce90dafdc7
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Security Considerations for a SQL Server Installation
  Security is important for every product and every business. By following simple best practices, you can avoid many security vulnerabilities. This topic discusses some security best practices that you should consider both before you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and after you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Security guidance for specific features is included in the reference topics for those features.  
  
## Before Installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 Follow these best practices when you set up the server environment:  
  
-   [Enhance physical security](#physical_security)  
  
-   [Use firewalls](#firewalls)  
  
-   [Isolate services](#isolated_services)  
  
-   [Configure a secure file system](#sa_with_least_privileges)  
  
-   [Disable NetBIOS and server message block](#disabled_protocols)  
  
-   [Installing SQL Server on a domain controller](../../../2014/sql-server/install/security-considerations-for-a-sql-server-installation.md#Install_DC)  
  
###  <a name="physical_security"></a> Enhance Physical Security  
 Physical and logical isolation make up the foundation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] security. To enhance the physical security of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation, do the following tasks:  
  
-   Place the server in a room accessible only to authorized persons.  
  
-   Place computers that host a database in a physically protected location, ideally a locked computer room with monitored flood detection and fire detection or suppression systems.  
  
-   Install databases in the secure zone of the corporate intranet and do not connect your SQL Servers directly to the Internet.  
  
-   Back up all data regularly and secure the backups in an off-site location.  
  
###  <a name="firewalls"></a> Use Firewalls  
 Firewalls are important to help secure the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation. Firewalls will be most effective if you follow these guidelines:  
  
-   Put a firewall between the server and the Internet. Enable your firewall. If your firewall is turned off, turn it on. If your firewall is turned on, do not turn it off.  
  
-   Divide the network into security zones separated by firewalls. Block all traffic, and then selectively admit only what is required.  
  
-   In a multi-tier environment, use multiple firewalls to create screened subnets.  
  
-   When you are installing the server inside a Windows domain, configure interior firewalls to allow Windows Authentication.  
  
-   If your application uses distributed transactions, you might have to configure the firewall to allow [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC) traffic to flow between separate MS DTC instances. You will also have to configure the firewall to allow traffic to flow between the MS DTC and resource managers such as [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 For more information about the default Windows firewall settings, and a description of the TCP ports that affect the [!INCLUDE[ssDE](../../includes/ssde-md.md)], [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], and [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)], see [Configure the Windows Firewall to Allow SQL Server Access](../../../2014/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access.md).  
  
###  <a name="isolated_services"></a> Isolate Services  
 Isolating services reduces the risk that one compromised service could be used to compromise others. To isolate services, consider the following guidelines:  
  
-   Run separate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services under separate Windows accounts. Whenever possible, use separate, low-rights Windows or Local user accounts for each [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. For more information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
###  <a name="sa_with_least_privileges"></a> Configure a Secure File System  
 Using the correct file system increases security. For [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installations, you should do the following tasks:  
  
-   Use the NTFS file system (NTFS). NTFS is the preferred file system for installations of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] because it is more stable and recoverable than FAT file systems. NTFS also enables security options like file and directory access control lists (ACLs) and Encrypting File System (EFS) file encryption. During installation, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] will set appropriate ACLs on registry keys and files if it detects NTFS. These permissions should not be changed. Future releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might not support installation on computers with FAT file systems.  
  
    > [!NOTE]  
    >  If you use EFS, database files will be encrypted under the identity of the account running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Only this account will be able to decrypt the files. If you must change the account that runs [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you should first decrypt the files under the old account and then re-encrypt them under the new account.  
  
-   Use a redundant array of independent disks (RAID) for critical data files.  
  
###  <a name="disabled_protocols"></a> Disable NetBIOS and Server Message Block  
 Servers in the perimeter network should have all unnecessary protocols disabled, including NetBIOS and server message block (SMB).  
  
 NetBIOS uses the following ports:  
  
-   UDP/137 (NetBIOS name service)  
  
-   UDP/138 (NetBIOS datagram service)  
  
-   TCP/139 (NetBIOS session service)  
  
 SMB uses the following ports:  
  
-   TCP/139  
  
-   TCP/445  
  
 Web servers and Domain Name System (DNS) servers do not require NetBIOS or SMB. On these servers, disable both protocols to reduce the threat of user enumeration.  
  
###  <a name="Install_DC"></a> Installing [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a domain controller  
 For security reasons, we recommend that you do not install [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] on a domain controller. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup will not block installation on a computer that is a domain controller, but the following limitations apply:  
  
-   You cannot run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services on a domain controller under a local service account.  
  
-   After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a computer, you cannot change the computer from a domain member to a domain controller. You must uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain controller.  
  
-   After [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is installed on a computer, you cannot change the computer from a domain controller to a domain member. You must uninstall [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before you change the host computer to a domain member.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instances are not supported where cluster nodes are domain controllers.  
  
-   [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Setup cannot create security groups or provision [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service accounts on a read-only domain controller. In this scenario, Setup will fail.  
  
## During or After Installation of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
 After installation, you can enhance the security of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation by following these best practices regarding accounts and authentication modes:  
  
 **Service accounts**  
  
-   Run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services by using the lowest possible permissions.  
  
-   Associate [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services with low privileged Windows local user accounts, or domain user accounts.  
  
-   For more information, see [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
 **Authentication mode**  
  
-   Require Windows Authentication for connections to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   Use Kerberos authentication. For more information, see [Register a Service Principal Name for Kerberos Connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).  
  
 **Strong passwords**  
  
-   Always assign a strong password to the `sa` account.  
  
-   Always enable password policy checking for password strength and expiration.  
  
-   Always use strong passwords for all [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] logins.  
  
> [!IMPORTANT]  
>  During setup of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] a login is added for the BUILTIN\Users group. This allows all authenticated users of the computer to access the instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] as a member of the public role. The BUILTIN\Users login can be safely removed to restrict [!INCLUDE[ssDE](../../includes/ssde-md.md)] access to computer users who have individual logins or are members of other Windows groups with logins.  
  
## See Also  
 [Hardware and Software Requirements for Installing SQL Server 2014](hardware-and-software-requirements-for-installing-sql-server.md)   
 [Network Protocols and Network Libraries](../../../2014/sql-server/install/network-protocols-and-network-libraries.md)   
 [Register a Service Principal Name for Kerberos Connections](../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md)  
  
  
