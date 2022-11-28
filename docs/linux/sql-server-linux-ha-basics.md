---
title: SQL Server high availability for Linux deployments
description: Learn about the high availability options for SQL Server on Linux, such as availability groups, failover cluster instances (FCI), and log shipping.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 10/19/2022
ms.service: sql
ms.subservice: linux
ms.topic: conceptual
ms.custom: seo-lt-2019
---
# SQL Server availability basics for Linux deployments

[!INCLUDE [SQL Server - Linux](../includes/applies-to-version/sql-linux.md)]

Starting with [!INCLUDE[sssql17-md](../includes/sssql17-md.md)], [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] is supported on both Linux and Windows. Like Windows-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] databases and instances need to be highly available under Linux. This article covers the technical aspects of planning and deploying highly available Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] databases and instances, as well as some of the differences from Windows-based installations. Because [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] may be new for Linux professionals, and Linux may be new for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] professionals, the article at times introduces concepts that may be familiar to some and unfamiliar to others.

## [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] availability options for Linux deployments

Besides backup and restore, the same three availability features are available on Linux as for Windows-based deployments:

- [Availability groups - SQL Server on Linux](sql-server-linux-availability-group-overview.md)
- [Failover Cluster Instances - SQL Server on Linux](sql-server-linux-shared-disk-cluster-concepts.md)
- [Log shipping - SQL Server on Linux](sql-server-linux-use-log-shipping.md)

On Windows, FCIs always require an underlying Windows Server failover cluster (WSFC). Depending on the deployment scenario, an AG usually requires an underlying WSFC, with the exception being the new None variant in [!INCLUDE[sssql17-md](../includes/sssql17-md.md)]. A WSFC doesn't exist in Linux. Clustering implementation in Linux is discussed in [Pacemaker for availability groups and failover cluster instances on Linux](sql-server-linux-pacemaker-basics.md).

## A quick Linux primer

While some Linux installations may be installed with an interface, most aren't, meaning that nearly everything at the operating system layer is done via command line. The common term for this command line in the Linux world is a *bash shell*.

In Linux, many commands need to be executed with elevated privileges, much like many things need to be done in Windows Server as an administrator. There are two main methods to execute with elevated privileges:

1. Run in the context of the proper user. To change to a different user, use the command `su`. If `su` is executed on its own without a username, as long as you know the password, you will now be in a shell as *root*.

1. The more common and security conscious way to run things is to use `sudo` before executing anything. Many of the examples in this article use `sudo`.

Some common commands, each of which have various switches and options that can be researched online:

- `cd` - change the directory
- `chmod` - change the permissions of a file or directory
- `chown` - change the ownership of a file or directory
- `ls` - show the contents of a directory
- `mkdir` - create a folder (directory) on a drive
- `mv` - move a file from one location to another
- `ps` - show all of the working processes
- `rm` - delete a file locally on a server
- `rmdir` - delete a folder (directory)
- `systemctl` - start, stop, or enable services
- Text editor commands. On Linux, there are various text editor options, such as vi and emacs.

## Common tasks for availability configurations of SQL Server on Linux

This section covers tasks that are common to all Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments.

### Ensure that files can be copied

Copying files from one server to another is a task that anyone using [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] on Linux should be able to do. This task is very important for AG configurations.

Things like permission issues can exist on Linux and on Windows-based installations. However, those familiar with how to copy from server to server on Windows may not be familiar with how it is done on Linux. A common method is to use the command-line utility `scp`, which stands for secure copy. Behind the scenes, `scp` uses OpenSSH. SSH stands for secure shell. Depending on the Linux distribution, OpenSSH itself may not be installed. If it isn't, OpenSSH needs to be installed first. For more information on configuring OpenSSH, see the information at the following links for each distribution:

- [Red Hat Enterprise Linux (RHEL)](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/6/html/deployment_guide/ch-openssh)
- [SUSE Linux Enterprise Server (SLES)](https://en.opensuse.org/SDB:Configure_openSSH)
- [Ubuntu](https://help.ubuntu.com/community/SSH/OpenSSH/Configuring)

When using `scp`, you must provide the credentials of the server if it isn't the source or destination. For example, using

```bash
scp MyAGCert.cer username@servername:/folder/subfolder
```

copies the file MyAGCert.cer to the folder specified on the other server. You must have permissions - and possibly ownership - of the file to copy it, so `chown` may also need to be employed before copying. Similarly, on the receiving side, the right user needs access to manipulate the file. For example, to restore that certificate file, the `mssql` user must be able to access it.

Samba, which is the Linux variant of server message block (SMB), can also be used to create shares accessed by UNC paths such as `\\SERVERNAME\SHARE`. For more information on configuring Samba, see the information at the following links for each distribution:

- [RHEL](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/selinux_users_and_administrators_guide/chap-managing_confined_services-samba)
- [SLES](https://www.suse.com/documentation/sles11/book_sle_admin/data/cha_samba.html)
- [Ubuntu](https://help.ubuntu.com/community/Samba)

Windows-based SMB shares can also be used; SMB shares don't need to be Linux-based, as long as the client portion of Samba is configured properly on the Linux server hosting [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] and the share has the right access. For those in a mixed environment, this would be one way to use existing infrastructure for Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments.

One thing that is important is that the version of Samba deployed should be SMB 3.0 compliant. When SMB support was added in [!INCLUDE[sssql11-md](../includes/sssql11-md.md)], it required all shares to support SMB 3.0. If using Samba for the share and not Windows Server, the Samba-based share should be using Samba 4.0 or later, and ideally 4.3 or later, which supports SMB 3.1.1. A good source of information on SMB and Linux is [SMB3 in Samba](https://events.static.linuxfound.org/sites/events/files/slides/smb3-in-samba.pr__0.pdf).

Finally, using a network file system (NFS) share is an option. Using NFS isn't an option on Windows-based deployments of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)], and can only be used for Linux-based deployments.

### Configure the firewall

Similar to Windows, Linux distributions have a built-in firewall. If your company is using an external firewall to the servers, disabling the firewalls in Linux may be acceptable. However, regardless of where the firewall is enabled, ports need to be opened. The following table documents the common ports needed for highly available [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments on Linux.

| Port Number | Type | Description |
| --- | --- | --- |
| 111 | TCP/UDP | NFS - `rpcbind/sunrpc` |
| 135 | TCP | Samba (if used) - End Point Mapper |
| 137 | UDP | Samba (if used) - NetBIOS Name Service |
| 138 | UDP | Samba (if used) - NetBIOS Datagram |
| 139 | TCP | Samba (if used) - NetBIOS Session |
| 445 | TCP | Samba (if used) - SMB over TCP |
| 1433 | TCP | [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] - default port; if desired, can change with `mssql-conf set network.tcpport <portnumber>` |
| 2049 | TCP, UDP | NFS (if used) |
| 2224 | TCP | Pacemaker - used by `pcsd` |
| 3121 | TCP | Pacemaker - Required if there are Pacemaker Remote nodes |
| 3260 | TCP | iSCSI Initiator (if used) - Can be altered in `/etc/iscsi/iscsid.config` (RHEL), but should match port of iSCSI Target |
| 5022 | TCP | [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] - default port used for an AG endpoint; can be changed when creating the endpoint |
| 5403 | TCP | Pacemaker |
| 5404 | UDP | Pacemaker - Required by Corosync if using multicast UDP |
| 5405 | UDP | Pacemaker - Required by Corosync |
| 21064 | TCP | Pacemaker - Required by resources using DLM |
| Variable | TCP | AG endpoint port; default is 5022 |
| Variable | TCP | NFS - port for `LOCKD_TCPPORT` (found in `/etc/sysconfig/nfs` on RHEL) |
| Variable | UDP | NFS - port for `LOCKD_UDPPORT` (found in `/etc/sysconfig/nfs` on RHEL) |
| Variable | TCP/UDP | NFS - port for `MOUNTD_PORT` (found in `/etc/sysconfig/nfs` on RHEL) |
| Variable | TCP/UDP | NFS - port for `STATD_PORT` (found in `/etc/sysconfig/nfs` on RHEL) |

For additional ports that may be used by Samba,see [Samba Port Usage](https://wiki.samba.org/index.php/Samba_Port_Usage).

Conversely, the name of the service under Linux can also be added as an exception instead of the port; for example, `high-availability` for Pacemaker. Refer to your distribution for the names if this is the direction you wish to pursue. For example, on RHEL the command to add in Pacemaker is

```bash
sudo firewall-cmd --permanent --add-service=high-availability
```

#### Firewall documentation

- [RHEL](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/s1-firewalls-haar)
- [SLES](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html)

### Install SQL Server packages for availability

On a Windows-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] installation, some components are installed even in a basic engine install, while others aren't. Under Linux, only the [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] engine is installed as part of the installation process. Everything else is optional. For highly available [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] instances under Linux, two packages should be installed with [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]:

- [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent (`mssql-server-agent`)
- the high availability (HA) package (`mssql-server-ha`)

While [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is technically optional, it is the [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] scheduler for jobs and is required by log shipping, so installation is recommended.

On [!INCLUDE [sssql17-md](../includes/sssql17-md.md)] with CU 4 and later versions, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is included in the Database Engine package, but you still need to [enable it](./sql-server-linux-setup-sql-agent.md). On Windows-based installations, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent isn't optional.

> [!NOTE]  
>For those new to [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)], [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]'s built-in job scheduler. It is a common way for DBAs to schedule things like backups and other [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] maintenance. Unlike a Windows-based installation of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] where [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is a completely different service, on Linux, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent runs in context of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] itself.

When AGs or FCIs are configured on a Windows-based configuration, they are cluster-aware. Cluster awareness means that [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] has specific resource DLLs that a WSFC knows about (`sqagtres.dll` and `sqsrvres.dll` for FCIs, `hadrres.dll` for AGs) and are used by the WSFC to ensure that the [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] clustered functionality is up, running, and functioning properly. Because clustering is external not only to [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] but Linux itself, Microsoft had to code the equivalent of a resource DLL for Linux-based AG and FCI deployments. This is the `mssql-server-ha` package, also known as the [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] resource agent for Pacemaker. To install the `mssql-server-ha` package, see [Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md).

The other optional packages for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Full-Text Search (`mssql-server-fts`) and [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Integration Services (`mssql-server-is`), aren't required for high availability, either for an FCI or an AG.

## SQL Server high availability and disaster recovery partners

To provide high availability and disaster recovery for your [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] services, choose from a wide variety of industry-leading tools. This section highlights [!INCLUDE [msconame-md](../includes/msconame-md.md)] partner companies with high availability and disaster recovery solutions supporting [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)].

| Partner | Description |
| --- | --- |
| **[DH2i](https://dh2i.com)** | DxEnterprise is Smart Availability software for Windows, Linux & Docker that helps you achieve the nearest-to-zero planned and unplanned downtime, unlocks huge cost savings, drastically simplifies management, and gets you both physical and logical consolidation.<br /><br />- [Deploy availability group with DH2i for SQL Server containers on AKS](tutorial-sql-server-containers-kubernetes-dh2i.md)<br />-  [Tutorial: Set up a three node Always On availability group with DH2i DxEnterprise](/azure/azure-sql/virtual-machines/linux/dh2i-high-availability-tutorial) |
| **[HPE Serviceguard](https://www.hpe.com/us/en/product-catalog/detail/pip.376220.html)** | HPE SGLX offers context-sensitive monitoring and recovery options for Failover Cluster Instance and Always On Availability Groups. Maximize uptime with HPE SGLX without compromising data integrity and performance.<br /><br />- [Tutorial: Set up a three node Always On availability group with HPE Serviceguard for Linux](sql-server-availability-group-ha-hpe.md). |
| **[Pacemaker](https://www.clusterlabs.org/pacemaker/)** | Pacemaker is an open source high-availability cluster resource manager. With Corosync, an open source group communication system, Pacemaker can detect component failures and orchestrate necessary failover procedures to minimize interruptions to applications.<br /><br />- [Pacemaker for AGs and FCIs on Linux](sql-server-linux-pacemaker-basics.md)<br />- [Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md) |

## Next steps

- [Pacemaker for availability groups and failover cluster instances on Linux](sql-server-linux-pacemaker-basics.md)
- [Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md)
- [Configure a Pacemaker cluster for SQL Server availability groups](sql-server-linux-availability-group-cluster-pacemaker.md)
