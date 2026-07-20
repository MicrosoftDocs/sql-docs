---
title: "Install SQL Server with SMB Fileshare Storage"
description: In SQL Server, system databases and Database Engine user databases can be installed with Server Message Block (SMB) file server as a storage option.
author: rwestMSFT
ms.author: randolphwest
ms.date: 06/03/2025
ms.service: sql
ms.subservice: install
ms.topic: install-set-up-deploy
monikerRange: ">=sql-server-2017"
ms.custom:
  - intro-installation
  - sfi-ropc-blocked
---
# Install SQL Server with SMB fileshare storage

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

In [!INCLUDE [ssSQL11](../../includes/sssql11-md.md)] and later versions, system databases (`master`, `model`, `msdb`, and `tempdb`), and [!INCLUDE [ssDE](../../includes/ssde-md.md)] user databases can be installed with Server Message Block (SMB) file server as a storage option. This applies to both [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] stand-alone and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster installations (FCI).

> [!NOTE]  
> FILESTREAM is currently not supported on an SMB file share.

## Installation considerations

### SMB fileshare formats

When you specify the SMB file share, the following values are supported universal naming convention (UNC) path formats for standalone and FCI databases:

- `\\ServerName\ShareName\`
- `\\ServerName\ShareName`

For more information, see [Universal Naming Convention](/openspecs/windows_protocols/ms-dtyp/62e862f4-2a51-452e-8eeb-dc4ff5ee33cc).

The loopback UNC path (a UNC path whose server name is localhost, `127.0.0.1`, or the local machine name) isn't supported. As a special case, [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] with File Server Cluster hosted on the same node as [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is also not supported. To prevent this situation, you should create [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and the File Server Cluster on separated Windows Clusters.

The following UNC path formats aren't supported:

- Loopback path, such as `\\localhost\...\` or `\\127.0.0.1\...\`
- Administrative shares, such as `\\servername\x$`
- Other UNC path formats like `\\?\x:\`
- Mapped network drives

### Supported data definition language (DDL) statements

The following [!INCLUDE [tsql](../../includes/tsql-md.md)] DDL statements and database engine stored procedures support SMB file shares:

1. [CREATE DATABASE](../../t-sql/statements/create-database-transact-sql.md)
1. [ALTER DATABASE](../../t-sql/statements/alter-database-transact-sql.md)
1. [RESTORE statements](../../t-sql/statements/restore-statements-transact-sql.md)
1. [BACKUP](../../t-sql/statements/backup-transact-sql.md)

### Installation options

- In the setup user interface, on the **Database Engine Configuration** page, on the **Data Directories** tab, set the parameter **Data root directory** as `\\<FileServer>\<Share1>\`. Replace `<FileServer>` and `<Share1>` with values from your environment.

- In the command prompt installation, specify `/INSTALLSQLDATADIR` as `\\<FileServer>\<Share1>\`.

  Here's the sample syntax to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a standalone server using the SMB file share option:

  ```cmd
  Setup.exe /q /ACTION=Install /FEATURES=SQL /INSTANCENAME=MSSQLSERVER /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<password>" /SQLSYSADMINACCOUNTS="<DomainName\UserName>" /AGTSVCACCOUNT="<DomainName\UserName>" /AGTSVCPASSWORD="<password>" /INSTALLSQLDATADIR="\\FileServer\Share1\" /IACCEPTSQLSERVERLICENSETERMS
  ```

  [!INCLUDE [sql-eula-link](../../includes/sql-eula-link.md)]

  To install a single-node [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] failover cluster instance with the [!INCLUDE [ssDE](../../includes/ssde-md.md)] and [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)], default instance:

  ```cmd
  setup.exe /q /ACTION=InstallFailoverCluster /InstanceName=MSSQLSERVER /INDICATEPROGRESS /ASSYSADMINACCOUNTS="<DomainName\UserName>" /ASDATADIR=<Drive>:\OLAP\Data /ASLOGDIR=<Drive>:\OLAP\Log /ASBACKUPDIR=<Drive>:\OLAP\Backup /ASCONFIGDIR=<Drive>:\OLAP\Config /ASTEMPDIR=<Drive>:\OLAP\Temp /FAILOVERCLUSTERDISKS="<Cluster Disk Resource Name - for example, 'Disk S:'" /FAILOVERCLUSTERNETWORKNAME="<Insert Network Name>" /FAILOVERCLUSTERIPADDRESSES="IPv4;xx.xxx.xx.xx;Cluster Network;xxx.xxx.xxx.x" /FAILOVERCLUSTERGROUP="MSSQLSERVER" /Features=AS,SQL /ASSVCACCOUNT="<DomainName\UserName>" /ASSVCPASSWORD="<password>" /AGTSVCACCOUNT="<DomainName\UserName>" /AGTSVCPASSWORD="<password>" /INSTALLSQLDATADIR="\\FileServer\Share1\" /SQLCOLLATION="SQL_Latin1_General_CP1_CS_AS" /SQLSVCACCOUNT="<DomainName\UserName>" /SQLSVCPASSWORD="<password>" /SQLSYSADMINACCOUNTS="<DomainName\UserName> /IACCEPTSQLSERVERLICENSETERMS
  ```

  For more information about the usage of various command-line parameter options in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)], see [Install and configure SQL Server on Windows from the command prompt](install-sql-server-from-the-command-prompt.md).

> [!NOTE]  
> Your passwords should follow the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] default [password policy](../../relational-databases/security/password-policy.md). By default, the password must be at least eight characters long and contain characters from three of the following four sets: uppercase letters, lowercase letters, base-10 digits, and symbols. Passwords can be up to 128 characters long. Use passwords that are as long and complex as possible.

## Operating system considerations (SMB protocol vs. SQL Server)

Different Windows operating systems have different SMB protocol versions, and the SMB protocol version is transparent to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. You can find the benefits of different SMB protocol versions with respect to [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)].

| Operating system | SMB2 protocol version | Benefits to SQL Server |
| --- | --- | --- |
| [!INCLUDE [winserver2012](../../includes/winserver2012-md.md)] and later versions, including Server Core | 3.0 | Support for transparent failover of file shares providing zero downtime, with no intervention required for the database administrator or file server administrator in file server cluster configurations.<br /><br />Support for IO using multiple network interfaces simultaneously, and tolerance to network interface failure.<br /><br />Support for network interfaces with RDMA capabilities.<br /><br />For more information on these features and Server Message Block, see [Server Message Block overview](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/hh831795(v=ws.11)).<br /><br />Support for Scale Out File Server (SoFS) with continuous availability. |
| [!INCLUDE [winserver2012](../../includes/winserver2012-md.md)] R2 and later versions, including Server Core | 3.2 | Support for transparent failover of file shares providing zero downtime, with no intervention required for the database administrator or file server administrator in file server cluster configurations.<br /><br />Support for IO using multiple network interfaces simultaneously, and tolerance to network interface failure, using SMB Multichannel.<br /><br />Support for network interfaces with RDMA capabilities using SMB Direct.<br /><br />For more information on these features and Server Message Block, see [Server Message Block overview](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/hh831795(v=ws.11)).<br /><br />Support for Scale Out File Server (SoFS) with continuous availability.<br /><br />Optimized for small random read/write I/O common to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] transactional style workloads.<br /><br />Maximum Transmission Unit (MTU) is turned on by default, which significantly enhances performance in large sequential transfers like [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] data warehouse and database backup or restore. |

## Security considerations

- The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] agent service account should have `FULL CONTROL` share permissions and NTFS permissions on the SMB share folders. The [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account can be a domain account or a system account if an SMB file server is used. For more information about share and NTFS permissions, see [Share and NTFS Permissions on a File Server](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc754178(v=ws.11)).

  > [!NOTE]  
  > The `FULL CONTROL` share permissions and NTFS permissions on the SMB share folders should be restricted to the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account, the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Agent service account, and Windows users with administrator server roles.

  Use a domain account as a [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service account. If system account is used as a service account, grant the permissions for the machine account in the format `<domain-name>\<computer-name>*$*`.

  > [!NOTE]  
  > During [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup, you must specify the domain account as a service account if the SMB file share is specified as a storage option. With SMB file share, the `System` account can only be specified as a service account after installing [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].  
  >
  > Virtual accounts can't be authenticated to a remote location. All virtual accounts use the permission of the machine account. Provision the machine account in the format `<domain-name>\<computer-name>*$*`.

- The account used to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] should have FULL CONTROL permissions on the SMB file share folder used as the data directory, or any other data folders (User database directory, user database log directory, `tempdb` directory, tempdb log directory, backup directory) during Cluster Setup.

- The account used to install [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] should be granted `SeSecurityPrivilege` privileges on the SMB file server. To grant this privilege, use the Local Security Policy console on the file server to add the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] setup account to the **Manage auditing and security log** policy. This setting is available in the **User Rights Assignments** section under **Local Policies**.

## Known issues and limitations

After you detach a [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] database that resides on network-attached storage, you might run into database permission issues while trying to reattach the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] database. For more information, see [Error 5120](../../relational-databases/errors-events/mssqlserver-5120-database-engine-error.md).

If SMB file share is used as a storage option for a clustered instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], by default the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Failover Cluster Diagnostics Log can't be written to the file share because the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Resource DLL lacks read/write permission on the file share. To resolve this issue, try one of the following methods:

1. Grant read/write permissions on the file share to all computer objects in the cluster.

1. Set the location of the diagnostic logs to a local file path. See the following example:

   ```sql
   ALTER SERVER CONFIGURATION SET DIAGNOSTICS LOG PATH = 'C:\logs';
   ```

When you host [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] data files on SMB file shares, all I/O against the files goes through the network interface on the server or virtual machine. Ensure that there's enough network bandwidth to support the I/O required by the workload.

Unavailability of the file share hosting the [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] data files due to network connectivity issues or other failure might result in I/O delays or failures in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)]. For mission critical workloads, ensure there's redundancy built into the network and file share and that the file share supports SMB 3.0 transparent failover, also known as [continuous availability](https://techcommunity.microsoft.com/blog/filecab/smb-transparent-failover-8211-making-file-shares-continuously-available/425693).

## Related content

- [Plan a SQL Server installation](../../sql-server/install/planning-a-sql-server-installation.md)
- [Configure Windows service accounts and permissions](../configure-windows/configure-windows-service-accounts-and-permissions.md)
