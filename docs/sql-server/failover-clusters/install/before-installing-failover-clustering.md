---
title: "Before Installing Failover Clustering"
description: This article describes planning considerations preparing to install a SQL Server failover cluster, including hardware, operating system, and configuration.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: ""
ms.date: 10/17/2022
ms.service: sql
ms.subservice: failover-cluster-instance
ms.topic: how-to
helpviewer_keywords:
  - "clusters [SQL Server], preinstallation checklist"
  - "installing failover clusters"
  - "failover clustering [SQL Server], preinstallation checklist"
---

# Before Installing Failover Clustering

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sqlserver.md)]

Before you install a SQL Server failover cluster, you must select the hardware and the operating system on which SQL Server will run. You must also configure Windows Server Failover Clustering (WSFC) and review network, security, and considerations for other software that will run on your failover cluster.

 If a Windows cluster has a local disk drive and the same drive letter is used on one or more cluster nodes as a shared drive, you can't install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on that drive. This restriction applies to both SQL Server failover cluster instances and standalone instances on a server that is part of a Windows Failover Cluster Instance.

 You may also want to review the following topics to learn more about [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clustering concepts, features and tasks.

|Topic Description|Topic|
|-----------------------|-----------|
|Describes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clustering concepts, and provides links to associated content and tasks.|[Always On Failover Cluster Instances (SQL Server)](../../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md)|
|Describes [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover policy concepts, and provides links to configuring the failover policy to suit your organizational requirements.|[Failover Policy for Failover Cluster Instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md)|
|Describes how to maintain and your existing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster.|[Failover Cluster Instance Administration and Maintenance](../../../sql-server/failover-clusters/windows/failover-cluster-instance-administration-and-maintenance.md)|
|Explains how to install [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)] on a Windows Server Failover Cluster (WSFC).|[How to Cluster SQL Server Analysis Services](/previous-versions/sql/sql-server-2012/dn736073(v=msdn.10))|

## <a id="BestPractices"></a> Best Practices

- Review [[!INCLUDE[SQL Server 2019](../../../includes/sssql19-md.md)] release notes](../../sql-server-version-15-release-notes.md).

- Install prerequisite software. Before running Setup to install or upgrade, install the following prerequisites to reduce installation time. You can install prerequisite software on each failover cluster node and then restart nodes once before running Setup.

  - Windows PowerShell is no longer installed by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup. Windows PowerShell is a prerequisite for installing [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)][!INCLUDE[ssDE](../../../includes/ssde-md.md)] components and [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)]. If Windows PowerShell isn't present on your computer, you can enable it by following the instructions on the [Windows Management Framework](/powershell/scripting/windows-powershell/wmf/overview) page.

  - .NET Framework 3.5 SP1 is no longer installed by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup, but may be required while installing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on older Windows operating systems. For more information, see [SQL Server 2019: Hardware and software requirements](../../install/hardware-and-software-requirements-for-installing-sql-server-2019.md).

  - **[!INCLUDE[msCoName](../../../includes/msconame-md.md)] Update package:** To avoid computer restart due to .NET Framework 4 installation during setup, [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] setup requires a [!INCLUDE[msCoName](../../../includes/msconame-md.md)] update to be installed on the computer.  If you're installing [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] on Windows 7 SP1 or [!INCLUDE[winserver2008](../../../includes/winserver2008-md.md)] SP2 this update is included. If you're installing on an older Windows operating system, download it from [Microsoft Update for .NET Framework 4.0 on Windows Vista and Windows Server 2008](https://go.microsoft.com/fwlink/?LinkId=198093).

  - .NET Framework 4: Setup installs .NET Framework 4 on a clustered operating system. To reduce installation time, you may consider installing .NET Framework 4 before you run Setup.

  - [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Set up support files. You can install these files by running SqlSupport.msi located on your installation media.

- Verify that antivirus software isn't installed on your WSFC cluster. For more information, see the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Knowledge Base article, [Antivirus software may cause problems with cluster services](/troubleshoot/windows-server/high-availability/not-cluster-aware-antivirus-software-cause-issue).

- When naming a cluster group for your failover cluster installation, you must not use any of the following characters in the cluster group name:

  - Less than operator (\<)

  - Greater than operator (>)

  - Double quote (")

  - Single quote (')

  - Ampersand (&)

     Also verify that existing cluster group names don't contain unsupported characters.

- Ensure that all cluster nodes are configured identically, including COM+, disk drive letters, and users in the administrators group.

- Verify that you've cleared the system logs in all nodes and viewed the system logs again. Ensure that the logs are free of any error messages before continuing.

- Before you install or update a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster, disable all applications and services that might use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] components during installation, but leave the disk resources online.

- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup automatically sets dependencies between the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] cluster group and the disks that will be in the failover cluster. Don't set dependencies for disks before Setup.

  - During [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Failover Cluster installation, computer object (Active Directory computer accounts) for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Network Resource Name is created. In a [!INCLUDE[winserver2008](../../../includes/winserver2008-md.md)] cluster, the cluster name account (computer account of the cluster itself) needs to have permissions to create computer objects. For more information, see [Configuring Accounts in Active Directory](https://technet.microsoft.com/library/cc731002\(WS.10\).aspx).

  - If you're using SMB File share as a storage option, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup account must have SeSecurityPrivilege on the file server. To do this, using the Local Security Policy console on the file server, add the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup account to **Manage auditing and security log** rights.

## <a id="Hardware"></a> Verify Your Hardware Solution

- If the cluster solution includes geographically dispersed cluster nodes, additional items like network latency and shared disk support must be verified.

  - For more information about [!INCLUDE[winserver2008](../../../includes/winserver2008-md.md)] and [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)], see [Validating Hardware for a failover cluster](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc732035(v=ws.10)) and [Support Policy for Windows Failover Clusters](https://go.microsoft.com/fwlink/?LinkId=196818).

- Verify that the disk where [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will be installed isn't compressed or encrypted. If you attempt to install [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to a compressed drive or an encrypted drive, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup fails.

- SAN configurations are also supported on [!INCLUDE[winserver2008](../../../includes/winserver2008-md.md)] and [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)] Advanced Server and Datacenter Server editions. The Windows Catalog and Hardware Compatibility List category "Cluster/Multi-cluster Device" list the set of SAN-capable storage devices that have been tested and are supported as SAN storage units with multiple WSFC clusters attached. Run cluster validation after finding the certified components.

- SMB File Share is also supported for installing data files. For more information, see [Storage Types for Data Files](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#StorageTypes).

    > [!WARNING]  
    >  If you are using Windows File Server as a SMB File Share storage, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup account must have SeSecurityPrivilege on the file server. To do this, using the Local Security Policy console on the file server, add the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] setup account to **Manage auditing and security log** rights.  
    >
    >  If you are using SMB file share storage other than Windows File server, please consult the storage vendor for an equivalent setting on the file server side.

- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports mount points.

     A mounted volume, or mount point, allows you to use a single drive letter to refer to many disks or volumes. If you have a drive letter D: that refers to a regular disk or volume, you can connect or "mount" additional disks or volumes as directories under drive letter D: without the additional disks or volumes requiring drive letters of their own.

     Additional mount point considerations for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clustering:

  - [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup requires that the base drive of a mounted drive has an associated drive letter. For failover cluster installations, this base drive must be a clustered drive. Volume GUIDs aren't supported in this release.

  - The base drive, the one with the drive letter, can't be shared among failover cluster instances. This is a normal restriction for failover clusters, but isn't a restriction on stand-alone, multi-instance servers.

  - The clustered installations of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] are limited to the number of available drive letters. Assuming that you use only one drive letter for the operating system, and all other drive letters are available as normal cluster drives or cluster drives hosting mount points, you're limited to a maximum of 25 instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] per failover cluster.

      > [!TIP]  
      >  The 25 instance limit can be overcome by using SMB file share option. If you use SMB file share as the storage option, you can install up to 50 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instances.

  - Formatting a drive after mounting additional drives isn't supported.

- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster installation supports Local Disk only for installing the `tempdb` files. Ensure that the path specified for the `tempdb` data and log files is valid on all the cluster nodes. During failover, if the `tempdb` directories aren't available on the failover target node, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource will fail to come online. For more information, see [Storage Types for Data Files](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md#StorageTypes) and [Database Engine Configuration - Data Directories](../../../database-engine/install-windows/install-sql-server.md).

- If you deploy a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster on iSCSI technology components, we recommend that you use appropriate caution. For more information, see [Support for SQL Server on iSCSI technology components](/troubleshoot/sql/admin/support-iscsi-technology-components).

- For more information, see [SQL Server support policy for Microsoft Clustering](/troubleshoot/sql/failover-clusters/support-policy-clustered-configurations).

- For more information about proper quorum drive configuration, see [Quorum Drive Configuration Information](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc770620(v=ws.10)).

- To install a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster when the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] source installation files and the cluster exist on different domains, copy the installation files to the current domain available to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster.

## <a id="Security"></a> Review Security Considerations

- To use encryption, install the server certificate with the fully qualified DNS name of the WSFC cluster on all nodes in the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster. For example, if you have a two-node cluster, with nodes named "Test1.DomainName.com" and "Test2.DomainName.com" and a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance named "Virtsql", you must get a certificate for "Virtsql.DomainName.com" and install the certificate on the test1 and test2 nodes. Then you can select the **Force protocol encryption** check box on the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager to configure your failover cluster for encryption.

    > [!IMPORTANT]  
    >  Do not select the **Force protocol encryption** check box until you have certificates installed on all participating nodes in your failover cluster instance.

- For [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installations in side-by-side configurations with previous versions, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services must use accounts found only in the global domains group. Additionally, accounts used by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] services must not appear in the local Administrators group. Failure to comply with this guideline will result in unexpected security behavior.

- To create a failover cluster, you must be a local administrator with permissions to log in as a service, and to act as part of the operating system on all nodes of the failover cluster instance.

- On [!INCLUDE[winserver2008](../../../includes/winserver2008-md.md)], service SIDs are generated automatically for use with [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] services. For [!INCLUDE[ssnoversion](../../../includes/ssnoversion-md.md)] failover cluster instances upgraded from previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], existing domain groups and ACL configurations will be preserved.

- Domain groups must be within the same domain as the machine accounts. For example, if the machine where [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will be installed is in the SQLSVR domain, which is a child of MYDOMAIN. You must specify a group in the SQLSVR domain. The SQLSVR domain may contain user accounts from MYDOMAIN.

- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clustering can't be installed where cluster nodes are domain controllers.

- Review content in [Security Considerations for a SQL Server Installation](../../../sql-server/install/security-considerations-for-a-sql-server-installation.md).

- To enable Kerberos authentication with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], see [How to use Kerberos authentication in SQL Server](https://www.betaarchive.com/wiki/index.php?title=Microsoft_KB_Archive/319723) in the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Knowledge Base.

- SQL Server failover cluster instance (FCI) requires the cluster nodes to be domain joined. The following configurations are **not supported**:
  - SQL FCI on workgroup clusters.
  - SQL FCI on Multi-Domain cluster.
  - SQL FCI on Domain + Workgroup Clusters.

## <a id="Network"></a> Review Network, Port, and Firewall Considerations

- Verify that you have disabled NetBIOS for all private network cards before beginning [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup.

- The network name and IP address of your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] shouldn't be used for any other purpose, such as file sharing. If you want to create a file share resource, use a different, unique network name and IP address for the resource.

    > [!IMPORTANT]  
    >  We recommend that you do not use file shares on data drives, because they can affect [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] behavior and performance.

- Even though [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] supports both Named Pipes and TCP/IP Sockets over TCP/IP within a cluster. We recommend that you use TCP/IP Sockets in a clustered configuration.

- ISA server isn't supported on Windows Clustering and is also not supported on [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clusters.

- The Remote Registry service must be up and running.

- Remote Administration must be enabled.

- For SQL Server instances using a non-default port, use the network configuration of the SQL Server Configuration Manager to determine the port used by the SQL Server instance you want to unblock. Enable the TCP port for IPALL in the firewall if you want to connect to your SQL Server instance using the [SQL Server Browser Service](../../../tools/configuration-manager/sql-server-browser-service.md), which uses a different IP address than the clustered instance, and UDP port 1434.

- Failover cluster Setup operations include a rule that checks network binding order. Although binding orders might seem correct, you might have disabled or "ghosted" NIC configurations on the system. "Ghosted" NIC configurations can affect the binding order and cause the binding order rule to issue a warning. To avoid this situation, use the following steps to identify and remove disabled network adapters:

    1. At a command prompt, type: set devmgr_Show_Nonpersistent_Devices=1.

    1. Type and run: start Devmgmt.msc.

    1. Expand the list of network adapters. Only the physical adapters should be in the list. If you have a disabled network adapter, Setup will report a failure for the network binding order rule. Control Panel/Network Connections will also show that adapter was disabled. Confirm that Network Settings in the Control Panel show the same list of enabled physical adapters that devmgmt.msc shows.

    1. Remove disabled network adapters before you run SQL Server Setup.

    1. After the Setup finishes, return to Network Connections in Control Panel and disable any network adapters that aren't currently in use.

## <a id="OS_Support"></a> Verify Your Operating System

 Make sure that your operating system is installed properly and is designed to support failover clustering. The following table is a list of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] editions and the operating systems that support them.

|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] edition|Windows Server 2022 Datacenter|Windows Server 2022 Datacenter: Azure Edition|Windows Server 2022 Standard|
|---------------------------------------|------------------------------------------------|-------------------------------------------------------|----------------------------------------------|
|[!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] Enterprise (64-bit) x64*|No|No|No|
|[!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] Enterprise (32-bit)|No|No|No|
|[!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)] Enterprise |No|No|No|
|[!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)] Standard |No|No|No|
|[!INCLUDE[sssql17-md](../../../includes/sssql17-md.md)] Enterprise |Yes|Yes|Yes|
|[!INCLUDE[sssql17-md](../../../includes/sssql17-md.md)] Standard |Yes|Yes|Yes|
|[!INCLUDE[sssql19-md](../../../includes/sssql19-md.md)] Enterprise |Yes|Yes|Yes|
|[!INCLUDE[sssql19-md](../../../includes/sssql19-md.md)] Standard |Yes|Yes|Yes|

|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] edition|Windows Server 2019 Datacenter|Windows Server 2019 Standard|Windows Server 2016 Datacenter|Windows Server 2016 Standard |
|---------------------------------------|------------------------------------------------|-------------------------------------------------------|----------------------------------------------|-----------------------------------------------------|
|[!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] Enterprise (64-bit) x64*|Yes|Yes|Yes|Yes|
|[!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)] Enterprise (32-bit)|Yes|Yes|||
|[!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)] Enterprise |Yes|Yes|Yes|Yes|
|[!INCLUDE[sssql16-md](../../../includes/sssql16-md.md)] Standard |Yes|Yes|Yes|Yes|
|[!INCLUDE[sssql17-md](../../../includes/sssql17-md.md)] Enterprise |Yes|Yes|Yes|Yes|
|[!INCLUDE[sssql17-md](../../../includes/sssql17-md.md)] Standard |Yes|Yes|Yes|Yes|
|[!INCLUDE[sssql19-md](../../../includes/sssql19-md.md)] Enterprise |Yes|Yes|Yes|Yes|
|[!INCLUDE[sssql19-md](../../../includes/sssql19-md.md)] Standard |Yes|Yes|Yes|Yes|

 *[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] clusters aren't supported in WOW mode. That includes upgrades from previous versions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover clusters that were originally installed in WOW. For those the only upgrade option is to install the new version side by side and migrate.

## <a id="MultiSubnet"></a> Additional Considerations for Multi-Subnet Configurations

 The sections below describe the requirements to keep in mind when installing a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster. A multi-subnet configuration involves clustering across multiple subnets, therefore involves using multiple IP addresses and changes to IP address resource dependencies.

### [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Edition and Operating System Considerations

- For information about the editions of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that support a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster, see [Features Supported by the Editions of SQL Server 2016](~/sql-server/editions-and-supported-features-for-sql-server-2016.md).

- To create a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster, you must first create the [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)] multi-site failover cluster on multiple subnets.

- [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster depends on the Windows Server failover cluster to make sure that the IP dependency conditions are valid if there's a failover.

- [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)] requires that all the cluster servers must be in the same Active Directory domain. Therefore, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] multi-subnet failover cluster requires that all the cluster nodes be in the same Active Directory domain even if they are in different subnets.

#### IP Address and IP Address Resource Dependencies

1. The IP Address resource dependency is set to OR in a multi-subnet configuration. For more information, see [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../../../sql-server/failover-clusters/install/create-a-new-sql-server-failover-cluster-setup.md)

1. Mixed AND-OR IP address dependencies aren't supported. For example, \<IP1> AND \<IP2> OR \<IP3> isn't supported.

1. More than one IP address per subnet isn't supported.

     If you decide to use more than one IP address configured for the same subnet, you may experience client connection failures during [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] startup.

#### Related Content

 For more information about [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)] multi-site failover, see [Windows Server 2008 R2 Failover Clustering Site](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/ff182338(v=ws.10)) and [Design for a Clustered Service or Application in a Multi-Site Failover Cluster](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/dd197430(v=ws.10)).

## <a id="WSFC"></a> Configure Windows Server Failover Cluster

- [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Cluster Service (WSFC) must be configured on at least one node of your server cluster. You must also run [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Enterprise, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Business Intelligence, or [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Standard with WSFC. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Enterprise support failover clusters with up to 16 nodes. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Business Intelligence and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Standard supports two-node failover clusters.

- The resource DLL for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service exports two functions used by WSFC Cluster Manager to check for availability of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource. For more information, see [Failover Policy for Failover Cluster Instances](../../../sql-server/failover-clusters/windows/failover-policy-for-failover-cluster-instances.md).

- WSFC must be able to verify that the failover clustered instance is running by using the IsAlive check. This requires connecting to the server by using a trusted connection. By default, the account that runs the cluster service isn't configured as an administrator on nodes in the cluster, and the BUILTIN\Administrators group doesn't have permission to log into [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]. These settings change only if you change permissions on the cluster nodes.

- Configure Domain Name Service (DNS) or Windows Internet Name Service (WINS). A DNS server or WINS server must be running in the environment where your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster will be installed. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup requires dynamic domain name service registration of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] IP interface virtual reference. DNS server configuration should allow cluster nodes to dynamically register an online IP address map to Network Name. If the dynamic registration can't be completed, the Setup fails, and the installation is rolled back. For more information, see [this knowledgebase article](https://mskb.pkisolutions.com/kb/947048)

## <a id="MSDTC"></a> Install [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Distributed Transaction Coordinator

 Before installing [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] on a failover cluster, determine whether the [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Distributed Transaction Coordinator (MSDTC) cluster resource must be created. If you're installing only the [!INCLUDE[ssDE](../../../includes/ssde-md.md)], the MSDTC cluster resource isn't required. If you're installing the [!INCLUDE[ssDE](../../../includes/ssde-md.md)] and SSIS, Workstation Components, or if you'll use distributed transactions, you must install MSDTC. MSDTC isn't required for [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)]-only instances.

 On [!INCLUDE[winserver2008](../../../includes/winserver2008-md.md)] and [!INCLUDE[winserver2008r2](../../../includes/winserver2008r2-md.md)], you can install multiple instances of MSDTC on a single failover cluster. The first instance of MSDTC that is installed will be the cluster default instance of MSDTC. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] will take advantage of an instance of MSDTC installed to the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] local cluster resource group by automatically using the instance of MSDTC. However, individual applications can be mapped to any instance of MSDTC on the cluster.

 The following rules are applied for an instance of MSDTC to be chosen by [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]:

- Use MSDTC installed to the local group, else

- Use the mapped instance of MSDTC, else

- Use the cluster's default instance of MSDTC, else

- Use the local machine's installed instance of MSDTC

> [!IMPORTANT]  
> If the MSDTC instance that is installed to the local cluster group of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] fails, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] does not automatically attempt to use the default cluster instance or the local machine instance of MSDTC. You would need to completely remove the failed instance of MSDTC from the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] group to use another instance of MSDTC. Likewise, if you create a mapping for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] and the mapped instance of MSDTC fails, your distributed transactions will also fail. If you want [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to use a different instance of MSDTC, you must either add an instance of MSDTC to the local cluster group of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] or delete the mapping.

### Configure [!INCLUDE[msCoName](../../../includes/msconame-md.md)] Distributed Transaction Coordinator

 After you install the operating system and configure your cluster, you must configure MSDTC to work in a cluster by using the Cluster Administrator. Failure to cluster MSDTC won't block [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup, but [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] application functionality may be affected if MSDTC isn't properly configured.

## See also

- [Hardware and Software Requirements for Installing SQL Server 2016](../../../sql-server/install/hardware-and-software-requirements-for-installing-sql-server.md)
- [Check Parameters for the System Configuration Checker](../../../database-engine/install-windows/check-parameters-for-the-system-configuration-checker.md)
- [Failover Cluster Instance Administration and Maintenance](../../../sql-server/failover-clusters/windows/failover-cluster-instance-administration-and-maintenance.md)