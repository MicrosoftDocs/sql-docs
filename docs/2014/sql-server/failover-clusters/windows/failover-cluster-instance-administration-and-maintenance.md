---
title: "Failover Cluster Instance Administration and Maintenance | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "user accounts [SQL Server], failover clustering"
  - "clusters [SQL Server], maintaining"
  - "nodes [Faillover Clustering]"
  - "failover clustering [SQL Server], maintaining"
  - "adding nodes"
  - "virtual servers [SQL Server], removing nodes"
  - "clustered instance of SQL Server"
  - "nodes [Faillover Clustering], removing"
  - "nodes [Faillover Clustering], adding"
  - "service accounts [SQL Server]"
  - "removing nodes"
  - "virtual servers [SQL Server], adding nodes"
ms.assetid: 2d5c63e9-8061-45c3-94db-8dd3100b8a91
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Failover Cluster Instance Administration and Maintenance
  Maintenance tasks like adding or removing nodes from an existing AlwaysOn Failover Cluster Instance (FCI) are accomplished using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program. Other administration tasks like changing the IP address resource, recovering from certain FCI scenarios are accomplished using the Failover Cluster Manager snap-in, which is the management snap-in for the Windows Server Failover Clustering (WSFC) service.  
  
## Maintaining a Failover Cluster Instance  
 After you have installed an FCI, you can change or repair it using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup program. For example, you can add additional nodes to an FCI, run an FCI as a stand-alone instance, or remove a node from a FCI configuration.  
  
### Adding a Node to an Existing Failover Cluster Instance  
 [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup gives you the option of maintaining an existing FCI. If you choose this option, you can add other nodes to your FCI by running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup on the computer that you want to add to the FCI. For more information, see [Create a New SQL Server Failover Cluster &#40;Setup&#41;](../install/create-a-new-sql-server-failover-cluster-setup.md) and [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
### Removing a Node from an Existing Failover Cluster Instance  
 You can remove a node from an FCI by running [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Setup on the computer that you want to remove from the FCI. Each node in an FCI is considered a peer without dependencies on other nodes on the FCI, and you can remove any node. A damaged node does not have to be available to be removed, and the removal process does not uninstall the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] binaries from the unavailable node. A removed node can be added back to a FCI at any time. For more information, see [Add or Remove Nodes in a SQL Server Failover Cluster &#40;Setup&#41;](../install/add-or-remove-nodes-in-a-sql-server-failover-cluster-setup.md).  
  
### Changing Service Accounts  
 You should not change passwords for any of the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service accounts when an FCI node is down or offline. If you must do this, you must reset the password again by using [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Configuration Manager when all nodes are back online.  
  
 If the service account for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is not an administrator in your cluster, the administrative shares cannot be deleted on any nodes of the cluster. The administrative shares must be available in a cluster for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] to function.  
  
> [!IMPORTANT]  
>  Do not use the same account for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service account and the WSFC service account. If the password changes for the WSFC service account, your [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] installation will fail.  
  
 On [!INCLUDE[nextref_longhorn](../../../includes/nextref-longhorn-md.md)], service SIDs are used for [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service accounts. For more information, see [Configure Windows Service Accounts and Permissions](../../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).  
  
## Administering a Failover Cluster Instance  
  
|Task Description|Topic Link|  
|----------------------|----------------|  
|Describes how to add dependencies to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] resource.|[Add Dependencies to a SQL Server Resource](add-dependencies-to-a-sql-server-resource.md)|  
|Kerberos is a network authentication protocol designed to provide strong authentication for client/server applications. Kerberos provides a foundation for interoperability and helps to enhance the security of enterprise-wide network authentication. You can use Kerberos authentication with [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] stand-alone instances or with AlwaysOn FCIs.|[Register a Service Principal Name for Kerberos Connections](../../../database-engine/configure-windows/register-a-service-principal-name-for-kerberos-connections.md).|  
|Provides links to content that describes how to enable Kerberos authentication||  
|Describes the procedure used to recover from a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster failure.|[Recover from Failover Cluster Instance Failure](recover-from-failover-cluster-instance-failure.md)|  
|Describe the procedure used to change the IP address resource for a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] failover cluster instance.|[Change the IP Address of a Failover Cluster Instance](change-the-ip-address-of-a-failover-cluster-instance.md)|  
  
## See Also  
 [Configure HealthCheckTimeout Property Settings](configure-healthchecktimeout-property-settings.md)   
 [Configure FailureConditionLevel Property Settings](configure-failureconditionlevel-property-settings.md)   
 [View and Read Failover Cluster Instance Diagnostics Log](view-and-read-failover-cluster-instance-diagnostics-log.md)  
  
  
