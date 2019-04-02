---
title: SQL Server availability basics for Linux deployments | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 11/27/2017
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
---
 
# SQL Server availability basics for Linux deployments

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

Starting with [!INCLUDE[sssql17-md](../includes/sssql17-md.md)], [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] is supported on both Linux and Windows. Like Windows-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] databases and instances need to be highly available under Linux. This article covers the technical aspects of planning and deploying highly available Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] databases and instances, as well as some of the differences from Windows-based installations. Because [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] may be new for Linux professionals, and Linux may be new for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] professionals, the article at times introduces concepts that may be familiar to some and unfamiliar to others.

## [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] availability options for Linux deployments
Besides backup and restore, the same three availability features are available on Linux as for Windows-based deployments:
-   Always On Availability Groups (AGs)
-   Always On Failover Cluster Instances (FCIs)
-   [Log Shipping](sql-server-linux-use-log-shipping.md)

On Windows, FCIs always require an underlying Windows Server failover cluster (WSFC). Depending on the deployment scenario, an AG usually requires an underlying WSFC, with the exception being the new None variant in [!INCLUDE[sssql17-md](../includes/sssql17-md.md)]. A WSFC does not exist in Linux. Clustering implementation in Linux is discussed in the section [Pacemaker for Always On Availability Groups and failover cluster instances on Linux](#pacemaker-for-always-on-availability-groups-and-failover-cluster-instances-on-linux).

## A quick Linux primer
While some Linux installations may be installed with an interface, most are not, meaning that nearly everything at the operating system layer is done via command line. The common term for this command line in the Linux world is a *bash shell*.

In Linux, many commands need to be executed with elevated privileges, much like many things need to be done in Windows Server as an administrator. There are two main methods to execute with elevated privileges:
1. Run in the context of the proper user. To change to a different user, use the command `su`. If `su` is executed on its own without a username, as long as you know the password, you will now be in a shell as *root*.
   
2. The more common and security conscious way to run things is to use `sudo` before executing anything. Many of the examples in this article use `sudo`.

Some common commands, each of which have various switches and options that can be researched online:
-   `cd` - change the directory
-   `chmod` - change the permissions of a file or directory
-   `chown` - change the ownership of a file or directory
-   `ls` - show the contents of a directory
-   `mkdir` - create a folder (directory) on a drive
-   `mv` - move a file from one location to another
-   `ps` - show all of the working processes
-   `rm` - delete a file locally on a server
-   `rmdir` - delete a folder (directory)
-   `systemctl` - start, stop, or enable services
-   Text editor commands. On Linux, there are various text editor options, such as vi and emacs.

## Common tasks for availability configurations of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] on Linux
This section covers tasks that are common to all Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments.

### Ensure that files can be copied
Copying files from one server to another is a task that anyone using [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] on Linux should be able to do. This task is very important for AG configurations.

Things like permission issues can exist on Linux as well as on Windows-based installations. However, those familiar with how to copy from server to server on Windows may not be familiar with how it is done on Linux. A common method is to use the command-line utility `scp`, which stands for secure copy. Behind the scenes, `scp` uses OpenSSH. SSH stands for secure shell. Depending on the Linux distribution, OpenSSH itself may not be installed. If it is not, OpenSSH needs to be installed first. For more information on configuring OpenSSH, see the information at the following links for each distribution:
-   [Red Hat Enterprise Linux (RHEL)](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/6/html/deployment_guide/ch-openssh)
-   [SUSE Linux Enterprise Server (SLES)](https://en.opensuse.org/SDB:Configure_openSSH)
-   [Ubuntu](https://help.ubuntu.com/community/SSH/OpenSSH/Configuring)

When using `scp`, you must provide the credentials of the server if it is not the source or destination. For example, using

```bash
scp MyAGCert.cer username@servername:/folder/subfolder
```

copies the file MyAGCert.cer to the folder specified on the other server. Note that you must have permissions - and possibly ownership - of the file to copy it, so `chown` may also need to be employed before copying. Similarly, on the receiving side, the right user needs access to manipulate the file. For example, to restore that certificate file, the `mssql` user must be able to access it.

Samba, which is the Linux variant of server message block (SMB), can also be used to create shares accessed by UNC paths such as `\\SERVERNAME\SHARE`. For more information on configuring Samba, see the information at the following links for each distribution:
-   [RHEL](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/html/Managing_Confined_Services/chap-Managing_Confined_Services-Samba.html)
-   [SLES](https://www.suse.com/documentation/sles11/book_sle_admin/data/cha_samba.html)
-   [Ubuntu](https://help.ubuntu.com/community/Samba)

Windows-based SMB shares can also be used; SMB shares do not need to be Linux-based, as long as the client portion of Samba is configured properly on the Linux server hosting [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] and the share has the right access. For those in a mixed environment, this would be one way to leverage existing infrastructure for Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments.

One thing that is important is that the version of Samba deployed should be SMB 3.0 compliant. When SMB support was added in [!INCLUDE[sssql11-md](../includes/sssql11-md.md)], it required all shares to support SMB 3.0. If using Samba for the share and not Windows Server, the Samba-based share should be using Samba 4.0 or later, and ideally 4.3 or later, which supports SMB 3.1.1. A good source of information on SMB and Linux is [SMB3 in Samba](https://events.linuxfoundation.org/sites/events/files/slides/smb3-in-samba.pr__0.pdf).

Finally, using a network file system (NFS) share is an option. Using NFS is not an option on Windows-based deployments of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)], and can only be used for Linux-based deployments.

### Configure the firewall
Similar to Windows, Linux distributions have a built-in firewall. If your company is using an external firewall to the servers, disabling the firewalls in Linux may be acceptable. However, regardless of where the firewall is enabled, ports need to be opened. The following table documents the common ports needed for highly available [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments on Linux.

| Port Number | Type     | Description                                                                                                                 |
|-------------|----------|-----------------------------------------------------------------------------------------------------------------------------|
| 111         | TCP/UDP  | NFS - `rpcbind/sunrpc`                                                                                                    |
| 135         | TCP      | Samba (if used) - End Point Mapper                                                                                          |
| 137         | UDP      | Samba (if used) - NetBIOS Name Service                                                                                      |
| 138         | UDP      | Samba (if used) - NetBIOS Datagram                                                                                          |
| 139         | TCP      | Samba (if used) - NetBIOS Session                                                                                           |
| 445         | TCP      | Samba (if used) - SMB over TCP                                                                                              |
| 1433        | TCP      | [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] - default port; if desired, can change with `mssql-conf set network.tcpport <portnumber>`                       |
| 2049        | TCP, UDP | NFS (if used)                                                                                                               |
| 2224        | TCP      | Pacemaker - used by `pcsd`                                                                                                |
| 3121        | TCP      | Pacemaker - Required if there are Pacemaker Remote nodes                                                                    |
| 3260        | TCP      | iSCSI Initiator (if used) - Can be altered in `/etc/iscsi/iscsid.config` (RHEL), but should match port of iSCSI Target |
| 5022        | TCP      | [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] - default port used for an AG endpoint; can be changed when creating the endpoint                                |
| 5403        | TCP      | Pacemaker                                                                                                                   |
| 5404        | UDP      | Pacemaker - Required by Corosync if using multicast UDP                                                                     |
| 5405        | UDP      | Pacemaker - Required by Corosync                                                                                            |
| 21064       | TCP      | Pacemaker - Required by resources using DLM                                                                                 |
| Variable    | TCP      | AG endpoint port; default is 5022                                                                                           |
| Variable    | TCP      | NFS - port for `LOCKD_TCPPORT` (found in `/etc/sysconfig/nfs` on RHEL)                                              |
| Variable    | UDP      | NFS - port for `LOCKD_UDPPORT` (found in `/etc/sysconfig/nfs` on RHEL)                                              |
| Variable    | TCP/UDP  | NFS - port for `MOUNTD_PORT` (found in `/etc/sysconfig/nfs` on RHEL)                                                |
| Variable    | TCP/UDP  | NFS - port for `STATD_PORT` (found in `/etc/sysconfig/nfs` on RHEL)                                                 |

For additional ports that may be used by Samba,see [Samba Port Usage](https://wiki.samba.org/index.php/Samba_Port_Usage).

Conversely, the name of the service under Linux can also be added as an exception instead of the port; for example, `high-availability` for Pacemaker. Refer to your distribution for the names if this is the direction you wish to pursue. For example, on RHEL the command to add in Pacemaker is

```bash
sudo firewall-cmd --permanent --add-service=high-availability
```

**Firewall documentation:**
-   [RHEL](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/s1-firewalls-haar)
-   [SLES](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html)

### Install [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] packages for availability
On a Windows-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] installation, some components are installed even in a basic engine install, while others are not. Under Linux, only the [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] engine is installed as part of the installation process. Everything else is optional. For highly available [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] instances under Linux, two packages should be installed with [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]: [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent (*mssql-server-agent*) and the high availability (HA) package (*mssql-server-ha*). While [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is technically optional, it is [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]'s scheduler for jobs and is required by log shipping, so installation is recommended. On Windows-based installations, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is not optional.

>[!NOTE]
>For those new to [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)], [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]'s built-in job scheduler. It is a common way for DBAs to schedule things like backups and other [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] maintenance. Unlike a Windows-based installation of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] where [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent is a completely different service, on Linux, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Agent runs in context of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] itself.

When AGs or FCIs are configured on a Windows-based configuration, they are cluster-aware. Cluster awareness means that [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] has specific resource DLLs that a WSFC knows about (sqagtres.dll and sqsrvres.dll for FCIs, hadrres.dll for AGs) and are used by the WSFC to ensure that the [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] clustered functionality is up, running, and functioning properly. Because clustering is external not only to [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] but Linux itself, Microsoft had to code the equivalent of a resource DLL for Linux-based AG and FCI deployments. This is the *mssql-server-ha* package, also known as the [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] resource agent for Pacemaker. To install the *mssql-server-ha* package, see [Install the HA and SQL Server Agent packages](sql-server-linux-deploy-pacemaker-cluster.md#install-the-sql-server-ha-and-sql-server-agent-packages).

The other optional packages for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] on Linux, [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Full-Text Search (*mssql-server-fts*) and [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] Integration Services (*mssql-server-is*), are not required for high availability, either for an FCI or an AG.

## Pacemaker for Always On Availability Groups and failover cluster instances on Linux
As previous noted, the only clustering mechanism currently supported by Microsoft for AGs and FCIs is Pacemaker with Corosync. This section covers the basic information to understand the solution, as well as how to plan and deploy it for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] configurations.

### HA add-on/extension basics
All of the currently supported distributions ship a high availability add-on/extension, which is based on the Pacemaker clustering stack. This stack incorporates two key components: Pacemaker and Corosync. All the components of the stack are:
-   Pacemaker - The core clustering component, that does things like coordinate across the clustered machines.
-   Corosync - A framework and set of APIs that provides things like quorum, the ability to restart failed processes, and so on.
-   libQB - Provides things like logging.
-   Resource agent - Specific functionality provided so that an application can integrate with Pacemaker.
-   Fence agent - Scripts/functionality that assist in isolating nodes and deal with them if they are having issues.
    
> [!NOTE]
> The cluster stack is commonly referred to as Pacemaker in the Linux world.

This solution is in some ways similar to, but in many ways different from deploying clustered configurations using Windows. In Windows, the availability form of clustering, called a Windows Server failover cluster (WSFC), is built into the operating system, and the feature that enables the creation of a WSFC, failover clustering, is disabled by default. In Windows, AGs and FCIs are built on top of a WSFC, and share tight integration because of the specific resource DLL that is provided by [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]. This tightly coupled solution is possible by and large because it is all from one vendor.

![](./media/sql-server-linux-ha-basics/image1.png)

On Linux, while each supported distribution has Pacemaker available, each distribution can customize and have slightly different implementations and versions. Some of the differences will be reflected in the instructions in this article. The clustering layer is open source, so even though it ships with the distributions, it is not tightly integrated in the same way a WSFC is under Windows. This is why Microsoft provides *mssql-server-ha*, so that [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] and the Pacemaker stack can provide close to, but not exactly the same, experience for AGs and FCIs as under Windows.

For full documentation on Pacemaker, including a more in-depth explanation of what everything is with full reference information, for RHEL and SLES:
-   [RHEL](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/ch-overview-HAAR.html)
-   [SLES](https://www.suse.com/documentation/sle_ha/book_sleha/data/book_sleha.html)

Ubuntu does not have a guide for availability.

For more information about the whole stack, also see the official [Pacemaker documentation page](https://clusterlabs.org/doc/) on the Clusterlabs site.

### Pacemaker concepts and terminology
This section documents the common concepts and terminology for a Pacemaker implementation.

#### Node
A node is a server participating in the cluster. A Pacemaker cluster natively supports up to 16 nodes. This number can be exceeded if Corosync is not running on additional nodes, but Corosync is required for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]. Therefore, the maximum number of nodes a cluster can have for any [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]-based configuration is 16; this is the Pacemaker limit, and has nothing to do with maximum limitations for AGs or FCIs imposed by [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]. 

#### Resource
Both a WSFC and a Pacemaker cluster have the concept of a resource. A resource is specific functionality that runs in context of the cluster, such as a disk or an IP address. For example, under Pacemaker both FCI and AG resources can get created. This is not dissimilar to what is done in a WSFC, where you see a [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] resource for either an FCI or an AG resource when configuring an AG, but is not exactly the same due to the underlying  differences in how [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] integrates with Pacemaker.

Pacemaker has standard and clone resources. Clone resources are ones that run simultaneously on all nodes. An example would be an IP address that runs on multiple nodes for load balancing purposes. Any resource that gets created for FCIs uses a standard resource, since only one node can host an FCI at any given time.

When an AG is created, it requires a specialized form of a clone resource called a multi-state resource. While an AG only has one primary replica, the AG itself is running across all nodes that it is configured to work on, and can potentially allow things such as read-only access. Because this is a "live" use of the node, the resources have the concept of two states: master and slave. For more information, see [Multi-state resources: Resources that have multiple modes](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/s1-multistateresource-HAAR.html).

#### Resource groups/sets
Similar to roles in a WSFC, a Pacemaker cluster has the concept of a resource group. A resource group (called a set in SLES) is a collection of resources that function together and can fail over from one node to another as a single unit. Resource groups cannot contain resources that are configured as master/slave; thus, they cannot be used for AGs. While a resource group can be used for FCIs, it is not generally a recommended configuration.

#### Constraints
WSFCs have various parameters for resources as well as things like dependencies, which tell the WSFC of a parent/child relationship between two different resources. A dependency is just a rule telling the WSFC which resource needs to be online first.

A Pacemaker cluster does not have the concept of dependencies, but there are constraints. There are three kinds of constraints: colocation, location, and ordering.
-   A colocation constraint enforces whether or not two resources should be running on the same node.
-   A location constraint tells the Pacemaker cluster where a resource can (or cannot) run.
-   An ordering constraint tells the Pacemaker cluster the order in which the resources should start.

> [!NOTE]
> Colocation constraints are not required for resources in a resource group, since all of those are seen as a single unit.

#### Quorum, fence agents, and STONITH
Quorum under Pacemaker is somewhat similar to a WSFC in concept. The whole purpose of a cluster's quorum mechanism is to ensure that the cluster stays up and running. Both a WSFC and the HA add-ons for the Linux distributions have the concept of voting, where each node counts towards quorum. You want a majority of the votes up, otherwise, in a worst case scenario, the cluster will be shut down.

Unlike a WSFC, there is no witness resource to work with quorum. Like a WSFC, the goal is to keep the number of voters odd. Quorum configuration has different considerations for AGs than FCIs.

WSFCs monitor the status of the nodes participating and handle them when a problem occurs. Later versions of WSFCs offer such features as quarantining a node that is misbehaving or unavailable (node is not on, network communication is down, etc.). On the Linux side, this type of functionality is provided by a fence agent. The concept is sometimes referred to as fencing. However, these fence agents are generally specific to the deployment, and often provided by hardware vendors and some software vendors, such as those who provide hypervisors. For example, VMware provides a fence agent that can be used for Linux VMs virtualized using vSphere.

Quorum and fencing ties into another concept called STONITH, or Shoot the Other Node in the Head. STONITH is required to have a supported Pacemaker cluster on all Linux distributions. For more information, see [Fencing in a Red Hat High Availability Cluster](https://access.redhat.com/solutions/15575) (RHEL).

#### corosync.conf
The `corosync.conf` file contains the configuration of the cluster. It is located in `/etc/corosync`. In the course of normal day-to-day operations, this file should never have to be edited if the cluster is set up properly.

#### Cluster log location
Log locations for Pacemaker clusters differ depending on the distribution.
-   RHEL and SLES - `/var/log/cluster/corosync.log`
-   Ubuntu - `/var/log/corosync/corosync.log`

To change the default logging location, modify `corosync.conf`.

## Plan Pacemaker clusters for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]
This section discusses the important planning points for a Pacemaker cluster.

### Virtualizing Linux-based Pacemaker clusters for [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)]
Using virtual machines to deploy Linux-based [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments for AGs and FCIs is covered by the same rules as for their Windows-based counterparts. There is a base set of rules for supportability of virtualized [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] deployments provided by Microsoft in [Microsoft Support KB 956893](https://support.microsoft.com/help/956893/support-policy-for-microsoft-sql-server-products-that-are-running-in-a-hardware-virtualization-environment). Different hypervisors such as Microsoft's Hyper-V and VMware's ESXi may have different variances on top of that, due to differences in the platforms themselves.

When it comes to AGs and FCIs under virtualization, ensure that anti-affinity is set for the nodes of a given Pacemaker cluster. When configured for high availability in an AG or FCI configuration, the VMs hosting [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)] should never be running on the same hypervisor host. For example, if a two-node FCI is deployed, there would need to be *at least* three hypervisor hosts so that there is somewhere for one of the VMs hosting a node to go in the event of a host failure, especially if using features like Live Migration or vMotion.

For more specifics, consult:
-   Hyper-V Documentation - [Using Guest Clustering for High Availability](https://technet.microsoft.com/library/dn440540(v=ws.11).aspx)
-   Whitepaper (written for Windows-based deployments, but most of the concepts still apply) - [Planning Highly Available, Mission Critical SQL Server Deployments with VMware vSphere](https://www.vmware.com/content/dam/digitalmarketing/vmware/en/pdf/solutions/vmware-vsphere-highly-available-mission-critical-sql-server-deployments.pdf)

>[!NOTE]
>RHEL with a Pacemaker cluster with STONITH is not yet supported by Hyper-V. Until that is supported, for more information and updates, consult [Support Policies for RHEL High Availability Clusters](https://access.redhat.com/articles/29440#3physical_host_mixing).

### Networking
Unlike a WSFC, Pacemaker does not require a dedicated name or at least one dedicated IP address for the Pacemaker cluster itself. AGs and FCIs will require IP addresses (see the documentation for each for more information), but not names, since there is no network name resource. SLES does allow the configuration of an IP address for administration purposes, but it is not required, as can be seen in [Create the Pacemaker cluster](sql-server-linux-deploy-pacemaker-cluster.md#create).

Like a WSFC, Pacemaker would prefer redundant networking, meaning distinct network cards (NICs or pNICs for physical) having individual IP addresses. In terms of the cluster configuration, each IP address would have what is known as its own ring. However, as with WSFCs today, many implementations are virtualized or in the public cloud where there is really only a single virtualized NIC (vNIC) presented to the server. If all pNICs and vNICs are connected to the same physical or virtual switch, there is no true redundancy at the network layer, so configuring multiple NICs is a bit of an illusion to the virtual machine. Network redundancy is usually built into the hypervisor for virtualized deployments, and is definitely built into the public cloud.

One difference with multiple NICs and Pacemaker versus a WSFC is that Pacemaker allows multiple IP addresses on the same subnet, whereas a WSFC does not. For more information on multiple subnets and Linux clusters, see the article [Configure multiple subnets](sql-server-linux-configure-multiple-subnet.md).

### Quorum and STONITH
Quorum configuration and requirements are related to AG or FCI-specific deployments of [!INCLUDE[ssnoversion-md](../includes/ssnoversion-md.md)].

STONITH is required for a supported Pacemaker cluster. Use the documentation from the distribution to configure STONITH. An example is at [Storage-based Fencing](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_storage_protect_fencing.html) for SLES. There is also a STONITH agent for VMware vCenter for ESXI-based solutions. For more information, see [Stonith Plugin Agent for VMWare VM VCenter SOAP Fencing (Unofficial)](https://github.com/olafrv/fence_vmware_soap).

> [!NOTE]
> As of the writing of this article, Hyper-V does not have a solution for STONITH. This is true for on premises deployments and also impacts Azure-based Pacemaker deployments using certain distributions such as RHEL.

### Interoperability
This section documents how a Linux-based cluster can interact with a WSFC or with other distributions of Linux.

#### WSFC
Currently, there is no direct way for a WSFC and a Pacemaker cluster to work together. This means that there is no way to create an AG or FCI that works across a WSFC and Pacemaker. However, there are two interoperability solutions, both of which are designed for AGs. The only way an FCI can participate in a cross-platform configuration is if it is participating as an instance in one of these two scenarios:
-   An AG with a cluster type of None. For more information, see the Windows [availability groups documentation](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).
-   A distributed AG, which is a special type of availability group that allows two different AGs to be configured as their own availability group. For more information on distributed AGs, see the documentation [Distributed Availability Groups](../database-engine/availability-groups/windows/distributed-availability-groups.md).

#### Other Linux distributions
On Linux, all nodes of a Pacemaker cluster must be on the same distribution. For example, this means that a RHEL node cannot be part of a Pacemaker cluster that has a SLES node. The main reason for this was previously stated: the distributions may have different versions and functionality, so things could not work properly. Mixing distributions has the same story as mixing WSFCs and Linux: use None or distributed AGs.

## Next steps
[Deploy a Pacemaker cluster for SQL Server on Linux](sql-server-linux-deploy-pacemaker-cluster.md)
