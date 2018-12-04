---
title: "Install SQL Server with SMB fileshare storage | Microsoft Docs"
ms.custom: ""
ms.date: "09/05/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: install
ms.topic: conceptual
ms.assetid: 8b7810b2-637e-46a3-9fe1-d055898ba639
author: MashaMSFT
ms.author: mathoma
monikerRange: ">=sql-server-2016||=sqlallproducts-allversions"
manager: craigg
---
# Install SQL Server with SMB fileshare storage

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Starting with [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)], system databases (Master, Model, MSDB, and TempDB), and [!INCLUDE[ssDE](../../includes/ssde-md.md)] user databases can be installed with Server Message Block (SMB) file server as a storage option. This applies to both [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stand-alone and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installations (FCI).  
  
> [!NOTE]  
>  Filestream is currently not supported on an SMB file share.  
  
## Installation considerations  
  
### SMB fileshare formats:  
 While specifying the SMB file share, the following are supported Universal Naming Convention (UNC) Path formats for standalone and FCI databases:  
  
-   \\\ServerName\ShareName\  
  
-   \\\ServerName\ShareName  
  
 For more information about Universal Naming Convention, see [UNC](https://msdn.microsoft.com/library/gg465305.aspx).  
  
 The loopback UNC path (a UNC path whose server name is localhost, 127.0.0.1, or the local machine name) is not supported. As a special case, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using File Server Cluster which is hosted on the same node [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is running is also not supported. To prevent this situation, it is recommended that [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and File Server Cluster to be created on separated Windows Clusters.  
  
 The below UNC path formats are not supported:  
  
-   Loopback path, e.g., \\\localhost\\..\ or \\\127.0.0.1\\...\  
  
-   Administrative shares, e.g., \\\servername\x$  
  
-   Other UNC path formats like \\\\?\x:\  
  
-   Mapped network drives.  
  
### Supported data definition language (DDL) statements  
 The following [!INCLUDE[tsql](../../includes/tsql-md.md)] DDL statements and database engine stored procedures support SMB file shares:  
  
1.  [CREATE DATABASE &#40;SQL Server Transact-SQL&#41;](../../t-sql/statements/create-database-sql-server-transact-sql.md)  
  
2.  [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
  
3.  [RESTORE &#40;Transact-SQL&#41;](../../t-sql/statements/restore-statements-transact-sql.md)  
  
4.  [BACKUP &#40;Transact-SQL&#41;](../../t-sql/statements/backup-transact-sql.md)  
  
### Installation options  
  
-   In setup UI "Database Engine Configuration" page, "Data Directories" tab, set the parameter "Data root directory as "\\\fileserver1\share1\".  
  
-   In command prompt installation, specify the "/INSTALLSQLDATADIR" as "\\\fileserver1\share1\".  
  
     Here is the sample syntax to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a Standalone server using SMB file share option:  
  
    ```  
    Setup.exe /q /ACTION=Install /FEATURES=SQL /INSTANCENAME=MSSQLSERVER /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<StrongPassword>" /SQLSYSADMINACCOUNTS="<DomainName\UserName>" /AGTSVCACCOUNT="<DomainName\UserName>" /AGTSVCPASSWORD="<StrongPassword>" /INSTALLSQLDATADIR="\\FileServer\Share1\" /IACCEPTSQLSERVERLICENSETERMS  
    ```  
  
     To install a single-node [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance with the [!INCLUDE[ssDE](../../includes/ssde-md.md)] and [!INCLUDE[ssASnoversion](../../includes/ssasnoversion-md.md)], default instance:  
  
    ```  
    setup.exe /q /ACTION=InstallFailoverCluster /InstanceName=MSSQLSERVER /INDICATEPROGRESS /ASSYSADMINACCOUNTS="<DomainName\UserName>" /ASDATADIR=<Drive>:\OLAP\Data /ASLOGDIR=<Drive>:\OLAP\Log /ASBACKUPDIR=<Drive>:\OLAP\Backup /ASCONFIGDIR=<Drive>:\OLAP\Config /ASTEMPDIR=<Drive>:\OLAP\Temp /FAILOVERCLUSTERDISKS="<Cluster Disk Resource Name - for example, 'Disk S:'" /FAILOVERCLUSTERNETWORKNAME="<Insert Network Name>" /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;Cluster Network;xxx.xxx.xxx.x" /FAILOVERCLUSTERGROUP="MSSQLSERVER" /Features=AS,SQL /ASSVCACCOUNT="<DomainName\UserName>" /ASSVCPASSWORD="xxxxxxxxxxx" /AGTSVCACCOUNT="<DomainName\UserName>" /AGTSVCPASSWORD="xxxxxxxxxxx" /INSTALLSQLDATADIR="\\FileServer\Share1\" /SQLCOLLATION="SQL_Latin1_General_CP1_CS_AS" /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="xxxxxxxxxxx" /SQLSYSADMINACCOUNTS="<DomainName\UserName> /IACCEPTSQLSERVERLICENSETERMS  
    ```  
  
     For more information about the usage of various command line parameter options in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], see [Install SQL Server 2016 from the Command Prompt](../../database-engine/install-windows/install-sql-server-2016-from-the-command-prompt.md).  
  
## Operating system considerations (SMB Protocol vs. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)])  
 Different Windows operating systems has different SMB protocol versions, and the SMB protocol version is transparent to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can find the benefits of different SMB protocol versions with respect to [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
|Operating System|SMB2 protocol version|Benefits to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|----------------------|---------------------------|-------------------------------------------|  
|[!INCLUDE[firstref_longhorn](../../includes/firstref-longhorn-md.md)] SP 2|2.0|Improved performance over previous SMB versions.<br /><br /> Durability, which helps recover from temporary network glitches.|  
|[!INCLUDE[winserver2008r2](../../includes/winserver2008r2-md.md)] SP 1, including Server Core|2.1|Support for large MTU, which benefits large data transfers, such as SQL backup and restore. This capability must be enabled by user. For more details on how to enable this capability, see [What's New in SMB](https://go.microsoft.com/fwlink/?LinkID=237319) (https://go.microsoft.com/fwlink/?LinkID=237319).<br /><br /> Significant performance improvements, specifically for SQL OLTP style workloads. These performance improvements require applying a hotfix. For more information on the hotfix, see [this](https://go.microsoft.com/fwlink/?LinkId=237320) (https://go.microsoft.com/fwlink/?LinkId=237320).|  
|[!INCLUDE[win8srv](../../includes/win8srv-md.md)], including Server Core|3.0|Support for transparent failover of file shares providing zero downtime with no administrator intervention required for SQL DBA or file server administrator in file server cluster configurations.<br /><br /> Support for IO using multiple network interfaces simultaneously, as well as tolerance to network interface failure.<br /><br /> Support for network interfaces with RDMA capabilities.<br /><br /> For more information on these features and Server Message Block, see [Server Message Block overview](https://go.microsoft.com/fwlink/?LinkId=253174) (https://go.microsoft.com/fwlink/?LinkId=253174).<br /><br /> Support for Scale Out File Server (SoFS) with continuous availability.|  
|[!INCLUDE[win8srv](../../includes/win8srv-md.md)] R2, including Server Core|3.2|Support for transparent failover of file shares providing zero downtime with no administrator intervention required for SQL DBA or file server administrator in file server cluster configurations.<br /><br /> Support for IO using multiple network interfaces simultaneously, as well as tolerance to network interface failure, using SMB Multichannel.<br /><br /> Support for network interfaces with RDMA capabilities using SMB Direct.<br /><br /> For more information on these features and Server Message Block, see [Server Message Block overview](https://go.microsoft.com/fwlink/?LinkId=253174) (https://go.microsoft.com/fwlink/?LinkId=253174).<br /><br /> Support for Scale Out File Server (SoFS) with continuous availability.<br /><br /> Optimized for small random read/write I/O common to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] OLTP.<br /><br /> Maximum Transmission Unit (MTU) is turned on by default, which significantly enhances performance in large sequential transfers like [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data warehouse and database backup or restore.|  
  
## Security considerations  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] agent service account should have FULL CONTROL share permissions and NTFS permissions on the SMB share folders. The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account can be a domain account or a system account if an SMB file server is used. For more information about share and NTFS permissions, see [Share and NTFS Permissions on a File Server](https://go.microsoft.com/fwlink/?LinkId=245535) (https://go.microsoft.com/fwlink/?LinkId=245535).  
  
    > [!NOTE]  
    >  The FULL CONTROL share permissions and NTFS permissions on the SMB share folders should be restricted to: [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account and windows users with admin server roles.  
  
     It is recommended to use domain account as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service account. If system account is used as a service account, grant the permissions for the machine account in the format: \<*domain_name*>\\<*computer_name*>\*$*.  
  
    > [!NOTE]  
    >  During [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup, it is required to specify domain account as a service account if SMB file share is specified as a storage option. With SMB file share, System account can only be specified as a service account post [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installation.  
    >   
    >  Virtual accounts cannot be authenticated to a remote location. All virtual accounts use the permission of machine account. Provision the machine account in the format \<*domain_name*>\\<*computer_name*>\*$*.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should have FULL CONTROL permissions on the SMB file share folder used as the data directory, or any other data folders (User database directory, user database log directory, TempDB directory, TempDB log directory, backup directory) during Cluster Setup.  
  
-   The account used to install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] should be granted SeSecurityPrivilege privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the Manage auditing and security log policy. This setting is available in the User Rights Assignments section under Local Policies in the Local Security Policy console.  
  
## Known issues  
  
-   After you detach a [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] database that resides on network-attached storage, you might run into database permission issue while trying to reattach the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. The issue is defined in [this KB article](https://go.microsoft.com/fwlink/?LinkId=237321) (https://go.microsoft.com/fwlink/?LinkId=237321). To work around this issue, see the **More Information** section in the KB article.  
  
-   If SMB file share is used as a storage option for a clustered instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], by default the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Diagnostics Log cannot be written to the file share because [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Resource DLL lacks the read/write permission on the file share. To resolve this issue, try one of the following methods:  
  
    1.  Grant read/write permissions on the file share to all computer objects in the cluster.  
  
    2.  Set the location of the diagnostic logs to a local file path. See the following example:  
  
        ```sql  
        ALTER SERVER CONFIGURATION  
        SET DIAGNOSTICS LOG PATH = 'C:\logs';  
        ```  
  
## See also  
 [Planning a SQL Server Installation](../../sql-server/install/planning-a-sql-server-installation.md)   
 [Configure Windows Service Accounts and Permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md)  
  
  
