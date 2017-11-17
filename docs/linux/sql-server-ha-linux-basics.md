---
title: "SQL Server availability basics for Linux deployments"
ms.date: "11/17/2017"
ms.prod: "sql-server-2017"
ms.technology: 
ms.topic: "article"
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
---

# SQL Server availability basics for Linux deployments

Starting with SQL Server 2017, SQL Server is supported on both Linux and Windows Server. As with any Windows Server-based SQL Server deployment, SQL Server databases and instances need to be highly available under Linux. This paper covers the technical aspects of planning and deploying highly available Linux-based SQL Server databases and instances, as well as some of the differences from Windows Server-based installations. Because SQL Server may be new for Linux professionals, and Linux may be new for SQL Server professionals, this paper at times introduces concepts that may be familiar to some and unfamiliar to others.

## SQL Server availability options for Linux deployments
Besides backup and restore, the same three availability features are available on Linux as for Windows Server-based deployments:
-   Always On Availability Groups (AGs)
-   Always On Failover Cluster Instances (FCIs)
-   [Log Shipping](sql-server-linux-use-log-shipping.md)

On Windows Server, FCIs in all configurations always require an underlying Windows Server failover cluster (WSFC). Depending on the deployment scenario, an AG usually requires an underlying WSFC, with the exception being the new None variant in SQL Server 2017 that is discussed later in this paper. A WSFC does not exist in Linux. How clustering is implemented in Linux is discussed in the section [Pacemaker for Always On Availability Groups and Failover Cluster Instances on Linux](#pacemaker-for-always-on-availability-groups-and-failover-cluster-instances-on-linux).

## A quick Linux primer
While some Linux installations may be installed with an interface, the majority are not, meaning that nearly everything at the OS layer is done via command line. The common term for this command line in the Linux world is a *bash shell*.

In Linux, many commands need to be executed with elevated privileges, much like many things need to be done in Windows Server as an administrator. There are two main methods to execute with elevated privileges:
1. Run in the context of the proper user. To change to a different user, use the command `su`. If `su` is executed on its own without a username, as long as you know the password, you will now be in a shell as *root*.
2. The more common and security conscious way to run things is to use `sudo` before executing anything. Many of the examples in this paper will use `sudo`.

Some common commands, each of which have various switches and options that can be researched online:
-   `cd` – change the directory
-   `chmod` – change the permissions of a file or directory
-   `chown` – change the ownership of a file or directory
-   `ls` – show the contents of a directory
-   `mkdir` – create a folder (directory) on a drive
-   `mv` – move a file from one location to another
-   `ps` – show all of the working processes
-   `rm` – delete a file locally on a server
-   `rmdir` – delete a folder (directory)
-   `systemctl` – start, stop, or enable services
-   Text editor commands. On Linux, there are various text editor options, such as vi and emacs.

## Common tasks for availability configurations of SQL Server on Linux
This section covers tasks that are common to all Linux-based SQL Server deployments.

### Ensure that files can be copied
One thing that anyone using SQL Server on Linux should be able to do is copy files from one server to another. This task is very important for AG configurations, as will be shown later in this article.

Things like permission issues can exist on Linux as well as on Windows Server-based installations. However, those familiar with how to copy from server to server on Windows may not be familiar with how it is done on Linux. A common method is to use the command-line utility `scp`, which stands for secure copy. Behind the scenes, `scp` uses OpenSSH. SSH stands for secure shell. Depending on the Linux distribution, OpenSSH itself may not be installed. If it is not, OpenSSH will need to be installed first. For more information on configuring OpenSSH, see the information at the following links for each distribution:
-   [Red Hat Enterprise Linux (RHEL)](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/html/Deployment_Guide/ch-OpenSSH.html)
-   [SUSE](https://en.opensuse.org/SDB:Configure_openSSH)
-   [Ubuntu](https://help.ubuntu.com/community/SSH/OpenSSH/Configuring)

When using `scp`, you must provide the credentials of the server if it is not the source or destination. For example, using

`scp MyAGCert.cer username@servername:/folder/subfolder`

copies the file MyAGCert.cer to the folder specified on the other server. Note that you must have permissions – and possibly ownership – of the file to copy it, so `chown` may also need to be employed before copying. Similarly, on the receiving side, the right user needs access to manipulate the file. For example, to restore that certificate file the `mssql` user must be able to access it.

Samba, which is the Linux variant of server message block (SMB), can also be used to create shares accessed by UNC paths such as `\\SERVERNAME\SHARE`. For more information on configuring Samba, see the information at the following links for each distribution:
-   [RHEL](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/html/Managing_Confined_Services/chap-Managing_Confined_Services-Samba.html)
-   [SUSE](https://www.suse.com/documentation/sles11/book_sle_admin/data/cha_samba.html)
-   [Ubuntu](https://help.ubuntu.com/community/Samba)

Windows Server-based SMB shares can also be used; SMB shares do not need to be Linux-based, as long as the client portion of Samba is configured properly on the Linux server hosting SQL Server and the share has the right access. For those in a mixed environment, this would be one way to leverage existing infrastructure for Linux-based SQL Server deployments.

One thing that is important is that the version of Samba deployed should be SMB 3.0 compliant. When SMB support was added in SQL Server 2012, it required all shares support SMB 3.0. If using Samba for the share and not Windows Server, the Samba-based share should be using Samba 4.0 or later, and ideally 4.3 or later, which supports SMB 3.1.1. A good source of information on SMB and Linux is [http://events.linuxfoundation.org/sites/events/files/slides/smb3-in-samba.pr__0.pdf](http://events.linuxfoundation.org/sites/events/files/slides/smb3-in-samba.pr__0.pdf).

Finally, using a network file system (NFS) share is an option. Using NFS is not an option on Windows Server-based deployments of SQL Server and can only be used for Linux-based deployments.

### Configure the firewall
Similar to Windows Server, Linux distributions have a built-in firewall. If your company is using an external firewall to the servers, disabling the firewalls in Linux may be acceptable. However, regardless of where the firewall is enabled, ports need to be opened. The following table documents the common ports needed for highly available SQL Server deployments on Linux.

| Port Number | Type     | Description                                                                                                                 |
|-------------|----------|-----------------------------------------------------------------------------------------------------------------------------|
| 111         | TCP/UDP  | NFS – `rpcbind/sunrpc`                                                                                                    |
| 135         | TCP      | Samba (if used) – End Point Mapper                                                                                          |
| 137         | UDP      | Samba (if used) – NetBIOS Name Service                                                                                      |
| 138         | UDP      | Samba (if used) – NetBIOS Datagram                                                                                          |
| 139         | TCP      | Samba (if used) – NetBIOS Session                                                                                           |
| 445         | TCP      | Samba (if used) – SMB over TCP                                                                                              |
| 1433        | TCP      | SQL Server – default port; if desired, can change with `mssql-conf set network.tcpport <portnumber>`                       |
| 2049        | TCP, UDP | NFS (if used)                                                                                                               |
| 2224        | TCP      | Pacemaker – used by `pcsd`                                                                                                |
| 3121        | TCP      | Pacemaker – Required if there are Pacemaker Remote nodes                                                                    |
| 3260        | TCP      | iSCSI Initiator (if used) – Can be altered in `/etc/iscsi/iscsid.config` (RHEL), but should match port of iSCSI Target |
| 5022        | TCP      | SQL Server - default port used for an AG endpoint; can be changed when creating the endpoint                                |
| 5403        | TCP      | Pacemaker                                                                                                                   |
| 5404        | UDP      | Pacemaker – Required by Corosync if using multicast UDP                                                                     |
| 5405        | UDP      | Pacemaker – Required by Corosync                                                                                            |
| 21064       | TCP      | Pacemaker – Required by resources using DLM                                                                                 |
| Variable    | TCP      | AG endpoint port; default is 5022                                                                                           |
| Variable    | TCP      | NFS – port for `LOCKD_TCPPORT` (found in `/etc/sysconfig/nfs` on RHEL)                                              |
| Variable    | UDP      | NFS – port for `LOCKD_UDPPORT` (found in `/etc/sysconfig/nfs` on RHEL)                                              |
| Variable    | TCP/UDP  | NFS – port for `MOUNTD_PORT` (found in `/etc/sysconfig/nfs` on RHEL)                                                |
| Variable    | TCP/UDP  | NFS – port for `STATD_PORT` (found in `/etc/sysconfig/nfs` on RHEL)                                                 |

For additional ports that may be used by Samba, refer to [https://wiki.samba.org/index.php/Samba_port_usage](https://wiki.samba.org/index.php/Samba_port_usage).

Conversely, the name of the service under Linux can also be added as an exception instead of the port. For example, High-Availability for Pacemaker. Refer to your distribution for the names if this is the direction you wish to pursue. For example, on RHEL the command to add Pacemaker is

`sudo firewall-cmd --permanent --add-service=high-availability`

**Firewall documentation:**
-   [RHEL](https://access.redhat.com/documentation/en-us/red_hat_enterprise_linux/7/html/high_availability_add-on_reference/s1-firewalls-haar)
-   [SUSE](https://www.suse.com/documentation/sle-ha-12/singlehtml/book_sleha/book_sleha.html)

### Install SQL Server packages for availability
On a Windows Server-based SQL Server installation, some components are installed even in a basic engine install, while others are not. Under Linux, only the SQL Server Engine is installed as part of the installation process. Everything else is optional. For highly available SQL Server instances under Linux, two packages should be installed at the same time as SQL Server: SQL Server Agent (`mssql-server-agent`) and the high availability agent (`mssql-server-ha`). While SQL Server Agent is technically optional, it is SQL Server’s scheduler for jobs and is required by log shipping, so installation is recommended. On Windows Server-based installations, SQL Server agent is not optional.

For those new to SQL Server, SQL Server Agent is SQL Server’s built-in job scheduler. It is a common way for DBAs to schedule things like backups and other SQL Server maintenance. Unlike a Windows Server-based installation of SQL Server where SQL Server Agent is a completely different service, on Linux, SQL Server Agent runs in context of SQL Server itself.

When AGs or FCIs are configured on a Windows Server-based configuration, they are cluster-aware. Cluster awareness means that SQL Server has specific resource DLLs that a WSFC knows about (sqagtres.dll and sqsrvres.dll for FCIs, hadrres.dll for AGs) and are used by the WSFC to ensure that the SQL Server clustered functionality is up, running, and functioning properly. Because clustering is external not only to SQL Server but Linux itself, Microsoft had to code the equivalent of a resource DLL for Linux-based AG and FCI deployments. This is the `mssql-server-ha` package, also known as the SQL Server resource agent for Pacemaker.

Use the instructions below to install the HA package and SQL Server Agent if they are not installed already. Installing the HA package after installing SQL Server requires a restart of SQL Server for it to be able to be used by SQL Server. These instructions assume that the repositories for the Microsoft packages have already been set up, since SQL Server should be installed at this point.
> [!NOTE]
> If you will not use SQL Server Agent for log shipping or any other use, it does not have to be installed, so package `mssql-server-agent` can be skipped.

**RHEL**

```bash
sudo yum install mssql-server-ha mssql-server-agent
sudo systemctl restart mssql-server
```

**SUSE**

```bash
sudo zypper install mssql-server-ha mssql-server-agent
sudo systemctl restart mssql-server
```

**Ubuntu**

```bash
sudo apt-get install mssql-server-ha mssql-server-agent
sudo systemctl restart mssql-server
```

The other optional packages for SQL Server on Linux, SQL Server Full-Text Search (`mssql-server-fts`) and SQL Server Integration Services (`mssql-server-is`), are not required for high availability either for an FCI or an AG.

## Pacemaker for Always On Availability Groups and Failover Cluster Instances on Linux
As noted above, the only clustering mechanism currently supported by Microsoft for AGs and FCIs is Pacemaker with Corosync. This section covers the basic information to understand the solution, as well as how to plan and deploy it for SQL Server configurations.

### HA add on/extension basics
All of the currently supported distributions ship a high availability add on/extension, which is based on the Pacemaker clustering stack. This stack incorporates two key components: Pacemaker and Corosync. All of the components of the stack are:
-   Pacemaker – The core clustering component, and does things like coordinate things across the clustered machines.
-   Corosync – A framework and set of APIs that provides things such as quorum, the ability to restart failed processes, and so on.
-   libQB – Provides things such as logging.
-   Resource Agent – This is specific functionality provided so that an application can integrate with Pacemaker.
-   Fence Agent – Scripts/functionality that assist in isolating nodes and deal with them if they are having issues.
    
> [!NOTE]
> The cluster stack is commonly referred to as Pacemaker in the Linux world.

This solution in some ways is similar to, but in many ways is different from deploying clustered configurations using Windows Server. In Windows Server, the availability form of clustering called a Failover Cluster (WSFC) is built into the operating system, and the feature that enables the creation of a WSFC, Failover Clustering, is disabled by default. AGs and FCIs are built on top of a WSFC, and share tight integration because of the specific resource DLL that is provided by SQL Server. This tightly coupled solution is possible by and large because it is all from one vendor.

![](./media/sql-server-ha-linux-basics/image1.png)

On Linux, while each supported distribution has Pacemaker available, each distribution can customize and have slightly different implementations and versions. Some of the differences will be reflected in the instructions in this document. The clustering layer is open source, so even though it ships with the distributions, it is not tightly integrated in the same way a WSFC is under Windows Server. This is why Microsoft provides `mssql-server-ha`, so that SQL Server and Pacemaker stack can provide close to, but not exactly the same, experience for AGs and FCIs as under Windows Server.

For full documentation on Pacemaker, including a more in-depth explanation of what everything is with full reference information, for RHEL and SUSE:
-   [RHEL](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/7/html/High_Availability_Add-On_Reference/ch-overview-HAAR.html)
-   [SUSE](https://www.suse.com/documentation/sle_ha/book_sleha/data/book_sleha.html)

Ubuntu does not have a guide for availability.

For more information about the whole stack, also see the [official Pacemaker documentation page](http://clusterlabs.org/doc/) on the Clusterlabs site.

### Pacemaker concepts and terminology
This section documents the common concepts and terminology for a Pacemaker implementation.

#### Node
A node is a server participating in the cluster. A Pacemaker cluster natively supports up to 16 nodes. This number can be exceeded if Corosync is not running on additional nodes, but Corosync is required for SQL Server. Therefore, the maximum number of nodes a cluster can have for any SQL Server-based configuration is 16; this is the Pacemaker limit, and has nothing to do with maximum limitations for AGs or FCIs imposed by SQL Server. See the [individual documentation for AGs and FCIs] for limitations that are specific to those features.

#### Resource
Both a WSFC and a Pacemaker cluster have the concept of a resource. A resource is specific functionality that runs in context of the cluster, such as a disk or an IP address. For example, under Pacemaker there are both FCI and AG resources that can get created. This is not dissimilar to what is done in a WSFC, where you see either a SQL Server resource for an FCI or an AG resource when configuring an AG, but is not exactly the same due to the underlying Pacemaker differences in how SQL Server integrates with it.

Pacemaker has standard and clone resources. Clone resources are ones that run simultaneously on all nodes. An example would be an IP address that would run on multiple nodes for load balancing purposes. Any resource that gets created for FCIs uses a standard resource, since only one node can host a FCI at any given time.

When an AG is created, it requires a specialized form of a Clone resource called a multi-state resource. While an AG only has one primary replica, the AG itself is running across all nodes that it is configured to work on and can potentially allow things such as read-only access. Because this is a “live” use of the node, the resources have the concept of two states: Master and Slave. For more information, see [https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/s1-multistateresource-HAAR.html](https://access.redhat.com/documentation/en-US/Red_Hat_Enterprise_Linux/6/html/Configuring_the_Red_Hat_High_Availability_Add-On_with_Pacemaker/s1-multistateresource-HAAR.html).

#### Resource groups/sets
Similar to Roles in a WSFC, a Pacemaker cluster has the concept of a resource group. A resource group (called a set in SUSE) is a collection of resources that function together and can fail over from one node to another as a single unit. A resource group cannot contain resources that are configured as master/slave, thus, they cannot be used for AGs. While a resource group can be used for FCIs, it is not generally a recommended configuration.

#### Constraints
WSFCs have various parameters for resources as well as things like dependencies, which tell the WSFC of a parent/child relationship between two different resources. A dependency is just a rule telling the WSFC which resource needs to be online first.

A Pacemaker cluster does not have the concept of dependencies, but there are constraints. There are three kinds of constraints: colocation, location, and ordering.
-   A colocation constraint enforces whether or not two resources should be running on the same node.
-   A location constraint tells the Pacemaker cluster where a resource can (or cannot) run.
-   An ordering constraint tells the Pacemaker cluster the order in which the resources should start.

> [!NOTE]
> Colocation constraints are not required for resources in a resource group, since all of those are seen as a single unit.

#### Quorum, fence agents, and STONITH
Quorum under Pacemaker is somewhat similar to a WSFC in terms of concept. The whole purpose of a cluster’s quorum mechanism is to ensure that the cluster stays up and running. Both a WSFC and the HA add ons for the Linux distributions have the concept of voting, where each node counts towards quorum. You want a majority of the votes up, otherwise, in a worst case scenario, the cluster will be shut down.

Unlike a WSFC, there is no witness resource to work with quorum. Like a WSFC, the goal is to keep the number of voters odd. The individual articles for [Always On Availability Groups] and [Failover Cluster Instances] discuss quorum for those configurations, since they each have different considerations.

WSFCs monitor the status of the nodes participating and handle them when a problem occurs. Later versions of WSFCs offer such features as quarantining a node that is misbehaving or unavailable (i.e. node is not on, network communication is down, etc.). On the Linux side, this type of functionality is provided by a fence agent. The concept is sometimes referred to as fencing. However, these fence agents are generally specific to the deployment, and often provided by hardware vendors and some software vendors, such as those who provide hypervisors. For example, VMware provides a fence agent that can be used for Linux VMs virtualized using vSphere.

Quorum and fencing ties into another concept called STONITH, or Shoot the Other Node in the Head. STONITH is required to have a supported Pacemaker cluster on all Linux distributions.
For more information, see:
-   [RHEL](https://access.redhat.com/solutions/15575)

#### corosync.conf
The `corosync.conf` file contains the configuration of the cluster. It is located in `/etc/corosync`. In the course of normal day-to-day operations, this file should never have to be edited if the cluster is set up properly.

#### Cluster log location
Logs for a Pacemaker cluster are different depending on the distribution.
-   RHEL and SUSE - `/var/log/cluster/corosync.log`
-   Ubuntu – `/var/log/corosync/corosync.log`

To change the default logging location, modify `corosync.conf`.

## Planning Pacemaker clusters for SQL Server
This section discusses the important planning points for a Pacemaker cluster.

### Virtualizing Linux-based Pacemaker clusters for SQL Server
Using virtual machines to deploy Linux-based SQL Server deployments for AGs and FCIs is covered by the same rules as for their Windows Server-based counterparts. There is a base set of rules for supportability of virtualized SQL Server deployments provided by Microsoft in [Microsoft Support KB 956893](https://support.microsoft.com/en-us/help/956893/support-policy-for-microsoft-sql-server-products-that-are-running-in-a-hardware-virtualization-environment). Different hypervisors such as Microsoft’s Hyper-V and VMware’s ESXi may have different variances on top of that, due to differences in the platforms themselves.

When it comes to AGs and FCIs under virtualization, ensure that anti-affinity is set for the nodes of a given Pacemaker cluster. When configured for high availability in an AG or FCI configuration, the VMs hosting SQL Server should never be running on the same hypervisor host. For example, if a two-node FCI is deployed, there would need to be *at least* three hypervisor hosts so that there is somewhere for one of the VMs hosting a node to go in the event of a host failure, especially if using features like Live Migration or vMotion.

For more specifics, consult:
-   Hyper-V Documentation – [Using Guest Clustering for High Availability](https://technet.microsoft.com/library/dn440540(v=ws.11).aspx)
-   Whitepaper (written for Windows Server-based deployments, but most of the concepts still apply) – [Planning Highly Available, Mission Critical SQL Server Deployments with VMware vSphere](http://www.vmware.com/content/dam/digitalmarketing/vmware/en/pdf/solutions/vmware-vsphere-highly-available-mission-critical-sql-server-deployments.pdf)

>[!NOTE]
>RHEL with a Pacemaker cluster with STONITH is not yet supported by Hyper-V. Until that is supported, for more information and updates, consult [https://access.redhat.com/articles/29440#3physical_host_mixing](https://access.redhat.com/articles/29440#3physical_host_mixing).

### Networking
Unlike a WSFC, Pacemaker does not require a dedicated name or at least one dedicated IP address for the Pacemaker cluster itself. AGs and FCIs will require IP addresses (see the documentation for each for more information), but not names, since there is no network name resource. SUSE does allow the configuration of an IP address for administration purposes, but it is not required, as can be seen later in the section [Create the Pacemaker Cluster](#create).

Like a WSFC, Pacemaker would prefer redundant networking, meaning distinct network cards (NICs or pNICs for physical) having individual IP addresses. In terms of the cluster configuration, each IP address would have what is known as its own ring. However, as with WSFCs today, many implementations are virtualized or in the public cloud where there is really only a single virtualized NIC (vNIC) presented to the server. If all pNICs and vNICs are connected to the same physical or virtual switch, there is no true redundancy at the network layer, so configuring multiple NICs is a bit of an illusion to the virtual machine. Network redundancy is usually built into the hypervisor for virtualized deployments, and is definitely built into the public cloud.

One difference with multiple NICs and Pacemaker versus a WSFC is that Pacemaker allows multiple IP addresses on the same subnet, whereas a WSFC does not. For more information on multiple subnets and Linux clusters, see the topic [Configuring Multiple Subnet Always On Availability Groups and Failover Cluster Instances with Pacemaker].

### Quorum and STONITH
Quorum is discussed in the [AG] and [FCI] articles, as the configuration and what is required is related to those specific deployments of SQL Server.

STONITH is required for a supported Pacemaker cluster. Use the documentation from the distribution to configure STONITH. An example is the one at [https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_storage_protect_fencing.html](https://www.suse.com/documentation/sle_ha/book_sleha/data/sec_ha_storage_protect_fencing.html) for SUSE. There is also a STONITH agent for VMware vCenter for ESXI-based solutions. For more information, see [needs link].

> [!NOTE]
> As of the writing of this article, Hyper-V does not have a solution for STONITH. This is true for on premises deployments and also impacts Azure-based Pacemaker deployments using certain distributions such as RHEL.

### Interoperability
This section documents how a Linux-based cluster can interact with a WSFC or with other distributions of Linux.

#### WSFC
Currently, there is no direct way for a WSFC and a Pacemaker cluster to work together. This means that there is no way to create an AG or FCI that works across a WSFC and Pacemaker. However, there are two interoperability solutions, both of which are designed for AGs. The only way an FCI can participate in a cross-platform configuration is if it is participating as an instance in one of the two scenarios:
-   An AG with a cluster type of None. For more information, see the [Availability Groups documentation](../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md).
-   A distributed AG, which is a special type of availability group that allows two different AGs to be configured as their own availability group. For more information on distributed AGs, see the documentation [Distributed Availability Groups](../database-engine/availability-groups/windows/distributed-availability-groups.md).

#### Other Linux distributions

On Linux, all nodes of a Pacemaker cluster must be on the same distribution. For example, this means that a RHEL node cannot be part of a Pacemaker cluster that has a SUSE node. The main reason for this was stated above: the distributions may have different versions and functionality, so things could not work properly. Mixing distributions has the same story as mixing WSFCs and Linux: use NONE and distributed AGs.

## Deploy a Pacemaker cluster 
This section shows how to configure the underlying Pacemaker cluster used by an AG or FCI. Unlike a traditional AG or FCI on Windows Server, the cluster portion on Linux can be done before or after the installation of SQL Server, as well as the configuration of the AG. There is no set order. This is different since it is not the tightly coupled Windows Server/SQL Server stack. This is more apparent in the articles for [Always On Availability Groups] and [Failover Cluster Instances]. The integration and configuration of resources for Pacemaker portion of an AG or FCI deployment is done after the cluster is configured.
> [!IMPORTANT]
> An AG with a cluster type of None does *not* require a Pacemaker cluster, nor can it be managed by Pacemaker. See the [Always On Availability Groups] article for more information.

### Common tasks 
This section documents the tasks that must be done to configure both SQL Server and Pacemaker to be able to deploy AGs or FCIs.

#### Install the HA add on
Use the syntax below to install the packages for each distribution of Linux that make up the HA add on. On SUSE, the HA add on gets initialized when the cluster is created.

**RHEL**
1.  Register the server using the following syntax. You will be prompted for a valid username and password.
    
    `sudo subscription-manager register`
2.  List the available pools for registration.
    
    `sudo subscription-manager list --available`
3.  Run the following command to associate RHEL high availability with the subscription.
    
    `sudo subscription-manager attach --pool=*PoolID*`
    
    where *PoolId* is the pool ID for the high availability subscription from the previous step.
4.  Enable the repository to be able to use the high availability add on.
    
    `sudo subscription-manager repos --enable=rhel-ha-for-rhel-7-server-rpms`
5.  Install Pacemaker.
    
    `sudo yum install pacemaker pcs fence-agents-all resource-agents`

**Ubuntu**

`sudo apt-get install pacemaker pcs fence-agents resource-agents`

**SUSE**

Install the High Availability pattern in YaST or do it as part of the main installation of the server. This can be done with an ISO/DVD as a source or getting it from online.

#### Prepare the nodes for Pacemaker (RHEL and Ubuntu only)
Pacemaker itself uses a user created on the distribution named *hacluster*. This gets created when the HA add on is installed on RHEL and Ubuntu.
1. On each server that will serve as a node of the Pacemaker cluster, create the password for a user that will be used by the cluster. The name used in the examples will be *hacluster*, but any name can be used. The name and password must be the same on all nodes participating in the Pacemaker cluster.
   
   `sudo passwd hacluster`
2. On each node that will be part of the Pacemaker cluster, enable and start the `pcsd` service with the following commands (RHEL and Ubuntu):

   ```bash
   sudo systemctl enable pcsd
   sudo systemctl start pcsd
   ```

   Then execute
   
   ```bash
   sudo systemctl status pcsd
   ```
   
   to ensure that `pcsd` is started.
3. Enable the Pacemaker service on each possible node of the Pacemaker cluster.

   `sudo systemctl start pacemaker`

   On Ubuntu, you will see an error
   *pacemaker Default-Start contains no runlevels, aborting.*
   This is a known issue. Despite the error, enabling the pacemaker service is successful, and this is a bug that will be fixed at some point in the future.
4. Next, create and start the Pacemaker cluster. There is one difference between RHEL and Ubuntu at this step. While on both distributions, installing pcs will configure a default configuration file for the Pacemaker cluster, on RHEL, executing this command will destroy any existing configuration and create a new cluster.

<a id="create"></a>
### Create the Pacemaker cluster 
This section documents how to create the cluster for each distribution of Linux.

#### RHEL
These instructions show how to configure a Pacemaker cluster on RHEL.
1. Authorize the nodes.
   
   `sudo pcs cluster auth *Node1 Node2 … NodeN* -u hacluster`
   
   where *NodeX* is the name of the node.
2. Create the cluster
   
   `sudo pcs cluster setup --name *NameforCluster Nodelist* --start --all --enable`
   
   where *PMClusterName* is the name assigned to the Pacemaker cluster and *Nodelist* is the list of names of the nodes separated by a space.

#### Ubuntu
Configuring Ubuntu is similar to RHEL. However, there is one major difference: when the Pacemaker packages are installed, it creates a base configuration for the cluster and enables and starts pcsd. If you try to configure the Pacemaker cluster by following the RHEL instructions exactly, you will get an error. To fix this problem, perform the following steps: 
1. Remove the default Pacemaker configuration from each node.
   
   `sudo pcs cluster destroy`
   
2. Follow the steps in the RHEL section for creating the Pacemaker cluster.

#### SUSE/SLES

The process for creating a Pacemaker cluster is completely different on SUSE than it is on RHEL and Ubuntu. The steps below document how to create a cluster with SUSE.
1. Start the cluster configuration process by running sudo `ha-cluster-init` on one of the nodes. You may be prompted that NTP is not configured and that no watchdog device is found. That is fine for getting things up and running. Watchdog is related to STONITH if you use SUSE’s built-in fencing that is storage-based. NTP and watchdog can be configured later.
2. You will be prompted to configure Corosync. You will be asked for the network address to bind to, as well as the multicast address and port. The network address is the subnet that you are using. For example, 192.191.190.0. You can accept the defaults and click **Enter** at every prompt, or change if necessary.
3. Next, you will be asked if you want to configure SBD, which is the disk-based fencing. This can be done later if desired. If it is not configured, unlike on RHEL and Ubuntu, `stonith-enabled` will by default be set to false.
4. Finally, you will be asked if you want to configure an IP address for administration. This IP address is optional, but functions similar to the IP address for a WSFC in the sense that it creates an IP address in the cluster to be used for connecting to it via HA Web Konsole (HAWK). This, too, is optional.
5. Ensure that the cluster is up and running by issuing `sudo crm status`.
6. Change the hacluster password
   
   `sudo passwd hacluster`
   
8. If you configured an IP address for administration, you can test it in a browser, which also tests the password change for `hacluster`.
   ![](./media/sql-server-ha-linux-basics/image2.png)
9. On another SUSE server that will be a node of the cluster, run
   
   `sudo ha-cluster-join`
   
10. When prompted, enter the name or IP address of the server that was configured as the first node of the cluster in the previous steps. The server is added as a node to the existing cluster.
11. Verify the node was added by issuing `sudo crm status`.
12. Change the hacluster password
   
   `sudo passwd hacluster`
   
13. Repeat Steps 8-11 for all other servers to be added to the cluster.

## Next steps
[Add links]
