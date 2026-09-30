---
title: High Availability Basics
titleSuffix: SQL Server on Linux
description: Learn about the high availability options for SQL Server on Linux, such as availability groups, failover cluster instances (FCI), and log shipping.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: amitkh, atsingh
ms.date: 09/14/2026
ms.service: sql
ms.subservice: linux
ms.topic: concept-article
ms.custom:
  - linux-related-content
---
# SQL Server availability basics for Linux deployments

[!INCLUDE [SQL Server - Linux](../../includes/applies-to-version/sql-linux.md)]

Starting with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] is supported on both Linux and Windows. Like Windows-based [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployments, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] databases and instances need to be highly available under Linux.

This article covers the technical aspects of planning and deploying highly available Linux-based [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances and databases, and highlights key differences from Windows-based installations. Because either [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] or Linux might be new to you, this article discusses concepts that might already be familiar to you.

## SQL Server availability options for Linux deployments

Besides backup and restore, the same three availability features are available on Linux as for Windows-based deployments:

- [Availability groups for SQL Server on Linux](availability-groups/overview.md)
- [Failover cluster instances on Linux](failover-cluster-instance/shared-disk-cluster-concepts.md)
- [Get started with log shipping on Linux](use-log-shipping.md)

On Windows, FCIs always require an underlying Windows Server failover cluster (WSFC). Depending on the deployment scenario, an AG usually requires an underlying WSFC, with the exception being the new None variant in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)]. A WSFC doesn't exist in Linux. Clustering implementation in Linux is discussed in [Pacemaker for availability groups and failover cluster instances on Linux](availability-groups/pacemaker-basics.md).

## A quick Linux primer

While some Linux installations include an interface, most don't. You use the command line for nearly everything at the operating system layer. The common term for this command line in the Linux world is a *shell*, the most common being `bash`.

In Linux, you need elevated privileges to run many commands, similar to needing administrator privileges in Windows Server. You can run commands with elevated privileges in two ways:

1. Run the command as the proper user. To change to a different user, use the `su` command. If you run `su` without a username, you enter a shell as `root` if you know the password.

1. Use `sudo` before the command. This method is more common and more secure. Many examples in this article use `sudo`.

Here are some common commands. Each command has various switches and options that you can research online:

- `cd` - change the directory
- `chmod` - change the permissions of a file or directory
- `chown` - change the ownership of a file or directory
- `ls` - show the contents of a directory
- `mkdir` - create a folder (directory) on a drive
- `mv` - move a file from one location to another
- `ps` - show all working processes
- `rm` - delete a file locally on a server
- `rmdir` - delete a folder (directory)
- `systemctl` - start, stop, or enable services
- Text editor commands. On Linux, there are various text editor options, such as vi and emacs.

## Common tasks for availability configurations of SQL Server on Linux

This section covers tasks that are common to all Linux-based [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployments.

### Ensure that you can copy files

Anyone administering [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] on Linux should be able to copy files from one server to another. This task is essential for AG configurations.

Permission problems can exist on both Linux and Windows-based installations. However, Windows users familiar with how to copy files from server to server, might not be familiar with how it's done on Linux. A common method is to use the command-line utility `scp`, which stands for *secure copy*. Behind the scenes, `scp` uses OpenSSH. SSH stands for *secure shell*. Depending on the Linux distribution, OpenSSH itself might not be installed. If it isn't, you need to install OpenSSH.

For more information on configuring OpenSSH for your Linux distribution, see:

- [Red Hat Enterprise Linux (RHEL)](https://docs.redhat.com/documentation/red_hat_enterprise_linux/9/html/configuring_basic_system_settings/assembly_using-secure-communications-between-two-systems-with-openssh_configuring-basic-system-settings)
- [SUSE Linux Enterprise Server (SLES)](https://en.opensuse.org/SDB:Configure_openSSH)
- [Ubuntu](https://help.ubuntu.com/community/SSH/OpenSSH/Configuring)

[!INCLUDE [sles-deprecated](../includes/sles-deprecated.md)]

When you use `scp`, you must provide the credentials of the server if it isn't the source or destination. For example, the following command copies the file `MyAGCert.cer` to the folder specified on the other server:

```bash
scp MyAGCert.cer username@servername:/folder/subfolder
```

You must have permissions, and possibly ownership, of the file to copy it, so you might need to use `chown` before copying. Similarly, on the receiving side, the right user needs access to manipulate the file. For example, to restore that certificate file, the `mssql` user must be able to access it.

Samba, which is the Linux variant of server message block (SMB), can also be used to create shares accessed by UNC paths such as `\\SERVERNAME\SHARE`. For more information on configuring Samba, see the information at the following links for each distribution:

- [RHEL](https://docs.redhat.com/documentation/red_hat_enterprise_linux/9/html/configuring_and_using_network_file_services/assembly_using-samba-as-a-server_configuring-and-using-network-file-services)
- [SLES](https://documentation.suse.com/sles/15-SP5/html/SLES-all/cha-samba.html)
- [Ubuntu](https://help.ubuntu.com/community/Samba)

[!INCLUDE [sles-deprecated](../includes/sles-deprecated.md)]

You can also use Windows-based SMB shares. SMB shares don't need to be Linux-based, as long as the client portion of Samba is configured properly on the Linux server hosting [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and the share has the right access. For customers in a mixed environment, this approach lets you use existing infrastructure for Linux-based [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployments.

The version of Samba you deploy should be SMB 3.0 compliant. When SMB support was added in [!INCLUDE [sssql11-md](../../includes/sssql11-md.md)], it required all shares to support SMB 3.0. If you use Samba for the share and not Windows Server, the Samba-based share should use Samba 4.0 or later, and ideally 4.3 or later, which supports SMB 3.1.1. A good source of information on SMB and Linux is [SMB3 in Samba](https://events.static.linuxfound.org/sites/events/files/slides/smb3-in-samba.pr__0.pdf).

Finally, using a network file system (NFS) share is an option. You can't use NFS on Windows-based deployments of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)], and it can only be used for Linux-based deployments.

### Configure the firewall

Similar to Windows, Linux distributions have a built-in firewall. If your organization uses an external firewall for the servers, you might be able to disable the firewalls in Linux. However, regardless of where you enable the firewall, you need to open ports. The following table lists the common ports needed for highly available [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] deployments on Linux.

| Port Number | Type | Description |
| --- | --- | --- |
| `111` | TCP/UDP | NFS - `rpcbind/sunrpc` |
| `135` | TCP | Samba (if used) - Endpoint Mapper |
| `137` | UDP | Samba (if used) - NetBIOS Name Service |
| `138` | UDP | Samba (if used) - NetBIOS Datagram |
| `139` | TCP | Samba (if used) - NetBIOS Session |
| `445` | TCP | Samba (if used) - SMB over TCP |
| `1433` | TCP | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] - default port; if desired, can change with `mssql-conf set network.tcpport <portnumber>` |
| `2049` | TCP, UDP | NFS (if used) |
| `2224` | TCP | Pacemaker - used by `pcsd` |
| `3121` | TCP | Pacemaker - Required if there are Pacemaker Remote nodes |
| `3260` | TCP | iSCSI Initiator (if used) - Can be altered in `/etc/iscsi/iscsid.config` (RHEL), but should match port of iSCSI Target |
| `5022` | TCP | [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] - default port used for an AG endpoint; can be changed when creating the endpoint |
| `5403` | TCP | Pacemaker |
| `5404` | UDP | Pacemaker - Required by Corosync if using multicast UDP |
| `5405` | UDP | Pacemaker - Required by Corosync |
| `21064` | TCP | Pacemaker - Required by resources using DLM |
| Variable | TCP | AG endpoint port; default is 5022 |
| Variable | TCP | NFS - port for `LOCKD_TCPPORT` (found in `/etc/sysconfig/nfs` on RHEL) |
| Variable | UDP | NFS - port for `LOCKD_UDPPORT` (found in `/etc/sysconfig/nfs` on RHEL) |
| Variable | TCP/UDP | NFS - port for `MOUNTD_PORT` (found in `/etc/sysconfig/nfs` on RHEL) |
| Variable | TCP/UDP | NFS - port for `STATD_PORT` (found in `/etc/sysconfig/nfs` on RHEL) |

For other ports that Samba uses, see [Samba Port Usage](https://wiki.samba.org/index.php/Samba_Port_Usage).

Conversely, you can add the name of the service under Linux as an exception instead of the port. For example, use `high-availability` for Pacemaker. Refer to your distribution for the appropriate names. On RHEL, for example, the command to add in Pacemaker is:

```bash
sudo firewall-cmd --permanent --add-service=high-availability
```

#### Firewall documentation

- [RHEL](https://docs.redhat.com/documentation/red_hat_enterprise_linux/9/html/configuring_firewalls_and_packet_filters/index)
- [SLES](https://documentation.suse.com/sles/15-SP5/html/SLES-all/cha-security-firewall.html)
- [Ubuntu](https://help.ubuntu.com/community/Firewall)

[!INCLUDE [sles-deprecated](../includes/sles-deprecated.md)]

### Install SQL Server packages for availability

On a Windows-based [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] installation, some components are installed even in a basic engine install, while others aren't. Under Linux, only the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] engine is installed as part of the installation process. Everything else is optional. For highly available [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instances under Linux, two packages should be installed with [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]:

- [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent (`mssql-server-agent`)
- the high availability (HA) package (`mssql-server-ha`)

While [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent is technically optional, it's the default scheduler for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] jobs, and is required by log shipping, so installation is recommended.

On [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] with CU 4 and later versions, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent is included in the Database Engine package, but you still need to [enable it](../install-upgrade/setup-sql-agent.md). On Windows-based installations, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent isn't optional.

> [!NOTE]  
> [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent is the built-in job scheduler for [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)]. It's used to schedule tasks such as backups and routine maintenance. On Windows, [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Agent runs as a separate service. On Linux, it runs in the context of [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] itself.

When you configure AGs or FCIs on a Windows-based configuration, they're cluster-aware. Cluster awareness means that [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] has specific resource DLLs that a WSFC knows about (`sqagtres.dll` and `sqsrvres.dll` for FCIs, `hadrres.dll` for AGs) and are used by the WSFC to ensure that the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] clustered functionality is up, running, and functioning properly.

Because clustering is external not only to [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] but Linux itself, Microsoft had to code the equivalent of a resource DLL for Linux-based AG and FCI deployments. This resource is the `mssql-server-ha` package, also known as the [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] resource agent for Pacemaker. To install the `mssql-server-ha` package, see [Deploy a Pacemaker cluster for SQL Server on Linux](failover-cluster-instance/deploy-pacemaker-cluster.md).

[!INCLUDE [ss-linux-cluster-pacemaker-ha-agent-v2](../includes/cluster-pacemaker-ha-agent-v2.md)]

On Linux, Full-Text Search (`mssql-server-fts`) and Integration Services (`mssql-server-is`) are optional [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] packages, and aren't required for an FCI or AG.

## SQL Server high availability and disaster recovery partners

To provide high availability and disaster recovery for your [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] services, choose from a wide variety of industry-leading tools. This section highlights [!INCLUDE [msconame-md](../../includes/msconame-md.md)] partner companies with high availability and disaster recovery solutions supporting [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].

| Partner | Description |
| --- | --- |
| **[DH2i](https://dh2i.com)** | DxEnterprise is an availability management solution for Windows, Linux, and container environments. It supports high availability, reduces planned and unplanned downtime, and simplifies the management of physical and logical resources.<br /><br />- [Deploy availability groups with DH2i DxEnterprise on Kubernetes](containers/tutorial-kubernetes-dh2i.md)<br />- [Tutorial: Set up a three node Always On availability group with DH2i DxEnterprise](/azure/azure-sql/virtual-machines/linux/dh2i-high-availability-tutorial) |
| **[HPE Serviceguard](https://buy.hpe.com/us/en/software/high-availability-disaster-recovery-software/serviceguard-software/hpe-serviceguard-for-linux/p/376220)** | HPE SGLX offers context-sensitive monitoring and recovery options for Failover Cluster Instance and Always On Availability Groups. Maximize uptime with HPE SGLX without compromising data integrity and performance.<br /><br />- [Tutorial: Set up a three node Always On availability group with HPE Serviceguard for Linux](availability-groups/high-availability-hpe.md). |
| **[Pacemaker](https://www.clusterlabs.org/projects/pacemaker)** | Pacemaker is an open source high-availability cluster resource manager. With Corosync, an open source group communication system, Pacemaker can detect component failures and orchestrate necessary failover procedures to minimize interruptions to applications.<br /><br />- [Pacemaker for availability groups and failover cluster instances on Linux](availability-groups/pacemaker-basics.md)<br />- [Deploy a Pacemaker cluster for SQL Server on Linux](failover-cluster-instance/deploy-pacemaker-cluster.md) |

## Related content

- [Pacemaker for availability groups and failover cluster instances on Linux](availability-groups/pacemaker-basics.md)
- [Deploy a Pacemaker cluster for SQL Server on Linux](failover-cluster-instance/deploy-pacemaker-cluster.md)
- [Configure a Pacemaker cluster for SQL Server availability groups](availability-groups/cluster-pacemaker.md)
