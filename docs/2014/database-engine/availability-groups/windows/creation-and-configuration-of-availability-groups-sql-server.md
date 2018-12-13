---
title: "Creation and Configuration of Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "Availability Groups [SQL Server], deploying"
  - "Availability Groups [SQL Server], configuring"
  - "Availability Groups [SQL Server], creating"
ms.assetid: 7f89fab8-6ee2-4273-9de0-e594bfb9407f
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Creation and Configuration of Availability Groups (SQL Server)
  The topics in this section explain how to deploy a [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)] implementation on instances of [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] that reside on different Windows Server Failover Clustering (WSFC) nodes within a single WSFC failover cluster.  
  
 Before you create your first availability group, we strongly recommend that you familiarize yourself with the information in the following topics:  
  
 [Prerequisites, Restrictions, and Recommendations for AlwaysOn Availability Groups &#40;SQL Server&#41;](prereqs-restrictions-recommendations-always-on-availability.md)  
 This topic describes the prerequisites, restrictions, and recommendations for computers; WSFC nodes; instances of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)]; availability groups, replicas, and databases. This topic also contains information about security considerations.  
  
 [Getting Started with AlwaysOn Availability Groups &#40;SQL Server&#41;](getting-started-with-always-on-availability-groups-sql-server.md)  
 Contains information about the steps for configuring a server instance, creating an availability group, configuring the availability group for client connections, managing availability groups, and monitoring availability groups.  
  
 
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To configure a server instance for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]**  
  
-   [Enable and Disable AlwaysOn Availability Groups &#40;SQL Server&#41;](enable-and-disable-always-on-availability-groups-sql-server.md)  
  
-   [Create a Database Mirroring Endpoint for AlwaysOn Availability Groups &#40;SQL Server PowerShell&#41;](database-mirroring-always-on-availability-groups-powershell.md)  
  
-   [Create a Database Mirroring Endpoint for Windows Authentication &#40;Transact-SQL&#41;](../../database-mirroring/create-a-database-mirroring-endpoint-for-windows-authentication-transact-sql.md)  
  
-   [Allow a Database Mirroring Endpoint to Use Certificates for Outbound Connections &#40;Transact-SQL&#41;](../../database-mirroring/database-mirroring-use-certificates-for-outbound-connections.md)  
  
 **To get started with configuring AlwaysOn Availability Groups**  
  
-   [Getting Started with AlwaysOn Availability Groups &#40;SQL Server&#41;](getting-started-with-always-on-availability-groups-sql-server.md)  
  
 **To create and configure a new availability group**  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](create-an-availability-group-transact-sql.md)  
  
-   [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../powershell/sql-server-powershell.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Specify the Endpoint URL When Adding or Modifying an Availability Replica &#40;SQL Server&#41;](specify-endpoint-url-adding-or-modifying-availability-replica.md)  
  
-   [Create or Configure an Availability Group Listener &#40;SQL Server&#41;](create-or-configure-an-availability-group-listener-sql-server.md)  
  
-   [Configure the Flexible Failover Policy to Control Conditions for Automatic Failover (AlwaysOn Availability Groups)](configure-flexible-automatic-failover-policy.md)  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md)  
  
-   [Configure Read-Only Access on an Availability Replica &#40;SQL Server&#41;](configure-read-only-access-on-an-availability-replica-sql-server.md)  
  
-   [Configure Read-Only Routing for an Availability Group &#40;SQL Server&#41;](configure-read-only-routing-for-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Replica to an Availability Group &#40;SQL Server&#41;](join-a-secondary-replica-to-an-availability-group-sql-server.md)  
  
-   [Start Data Movement on an AlwaysOn Secondary Database &#40;SQL Server&#41;](start-data-movement-on-an-always-on-secondary-database-sql-server.md)  
  
-   [Manually Prepare a Secondary Database for an Availability Group &#40;SQL Server&#41;](manually-prepare-a-secondary-database-for-an-availability-group-sql-server.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Management of Logins and Jobs for the Databases of an Availability Group &#40;SQL Server&#41;](../../logins-and-jobs-for-availability-group-databases.md)  
  
 **To troubleshoot**  
  
-   [Troubleshoot AlwaysOn Availability Groups Configuration (SQL Server)deleted](troubleshoot-always-on-availability-groups-configuration-sql-server.md)  
  
-   [Troubleshoot a Failed Add-File Operation &#40;AlwaysOn Availability Groups&#41;](troubleshoot-a-failed-add-file-operation-always-on-availability-groups.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [AlwaysON - HADRON Learning Series: Worker Pool Usage for HADRON Enabled Databases](https://blogs.msdn.com/b/psssql/archive/2012/05/17/alwayson-hadron-learning-series-worker-pool-usage-for-hadron-enabled-databases.aspx)  
  
     [SQL Server AlwaysOn Team Blogs: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Videos:**  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 1: Introducing the Next Generation High Availability Solution](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI302)  
  
     [Microsoft SQL Server Code-Named "Denali" AlwaysOn Series,Part 2: Building a Mission-Critical High Availability Solution Using AlwaysOn](http://channel9.msdn.com/Events/TechEd/NorthAmerica/2011/DBI404)  
  
-   **Whitepapers:**  
  
     [Microsoft SQL Server AlwaysOn Solutions Guide for High Availability and Disaster Recovery](https://go.microsoft.com/fwlink/?LinkId=227600)  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](http://sqlcat.com/)  
  
## See Also  
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Administration of an Availability Group &#40;SQL Server&#41;](administration-of-an-availability-group-sql-server.md)   
 [AlwaysOn Policies for Operational Issues with AlwaysOn Availability Groups (SQL Server)](always-on-policies-for-operational-issues-always-on-availability.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](monitoring-of-availability-groups-sql-server.md)   
 [AlwaysOn Availability Groups: Interoperability (SQL Server)](always-on-availability-groups-interoperability-sql-server.md)  
  
  
