---
title: "Prerequisites for Migrating from Log Shipping to AlwaysOn Availability Groups (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/14/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
helpviewer_keywords: 
  - "log shipping [SQL Server], AlwaysOn Availability Groups"
  - "Availability Groups [SQL Server], interoperability"
ms.assetid: 2738ce65-205e-4682-92d8-dc7e37c58b2b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Prerequisites for Migrating from Log Shipping to AlwaysOn Availability Groups (SQL Server)
  This topic describes the prerequisites for converting a log shipping primary database along with one or more of its secondary databases to an AlwaysOn primary database and secondary database(s).  
  
> [!NOTE]  
>  You can configure any primary or secondary database (possibly readable) in an availability group as a log shipping primary database.  
  
 **In This Topic:**  
  
-   [Availability Group Prerequisites](#AGPrereqsRealAddress)  
  
-   [Log Shipping Prerequisites](#LogShipPrereqs)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="AGPrereqsRealAddress"></a> Availability Group Prerequisites  
 To allow backup jobs to run on the primary replica of the availability group, use the following AlwaysOn Availability Groups backup settings:  
  
|Property|Setting|  
|--------------|-------------|  
|Automated backup preference of availability group|Only on the primary replica|  
|Backup priority of the primary replica.|>0|  
  
 **For more information:**  
  
 [View Availability Group Properties &#40;SQL Server&#41;](view-availability-group-properties-sql-server.md)  
  
 [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md)  
  
##  <a name="LogShipPrereqs"></a> Log Shipping Prerequisites  
  
-   The log shipping primary database must reside on the instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] that hosts the initial/current primary replica of the availability group.  
  
-   For a given log shipping secondary database to be converted to an AlwaysOn secondary database, it must:  
  
    -   Use the same name as the primary database.  
  
    -   Reside on a server instance that hosts a secondary replica for the availability group.  
  
 Once the backup job has run on the primary database, disable the backup job, and once the restore job has run on a given secondary database, disable the restore job.  
  
 After you have created all the secondary databases for the availability group, if you want to perform backups on secondary replicas, you need to re-configure the automated backup preference of the availability group.  
  
 **For more information:**  
  
 [Converting a logshipping configuration to Availability Group](https://blogs.msdn.com/b/sqlalwayson/archive/2012/01/09/converting-a-logshipping-configuration-to-availability-group.aspx) (a SQL Server blog)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **Log shipping**  
  
-   [Upgrade Log Shipping to SQL Server 2014 &#40;Transact-SQL&#41;](../../log-shipping/upgrading-log-shipping-to-sql-server-2016-transact-sql.md)  
  
-   [Remove Log Shipping &#40;SQL Server&#41;](../../log-shipping/remove-log-shipping-sql-server.md)  
  
 **AlwaysOn Availability Groups**  
  
-   [Use the Availability Group Wizard &#40;SQL Server Management Studio&#41;](use-the-availability-group-wizard-sql-server-management-studio.md)  
  
-   [Use the New Availability Group Dialog Box &#40;SQL Server Management Studio&#41;](use-the-new-availability-group-dialog-box-sql-server-management-studio.md)  
  
-   [Create an Availability Group &#40;Transact-SQL&#41;](create-an-availability-group-transact-sql.md)  
  
-   [Create an Availability Group &#40;SQL Server PowerShell&#41;](../../../powershell/sql-server-powershell.md)  
  
-   [Join a Secondary Database to an Availability Group &#40;SQL Server&#41;](join-a-secondary-database-to-an-availability-group-sql-server.md)  
  
-   [Configure Backup on Availability Replicas &#40;SQL Server&#41;](configure-backup-on-availability-replicas-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
  
-   **Blogs:**  
  
     [Converting a logshipping configuration to Availability Group](https://blogs.msdn.com/b/sqlalwayson/archive/2012/01/09/converting-a-logshipping-configuration-to-availability-group.aspx)  
  
     [Add a Log Shipping Primary Database and Secondary Database(s) to an Existing Availability Group](https://blogs.msdn.com/b/sqlalwayson/archive/2012/02/01/use-log-shipping-to-prepare-secondary-databases-for-an-existing-availability-group.aspx)  
  
     [SQL Server AlwaysOn Team Blogs: The official SQL Server AlwaysOn Team Blog](https://blogs.msdn.com/b/sqlalwayson/)  
  
     [CSS SQL Server Engineers Blogs](https://blogs.msdn.com/b/psssql/)  
  
-   **Whitepapers:**  
  
     [Migration Guide: Migrating to AlwaysOn Availability Groups from Prior Deployments Combining Database Mirroring and Log Shipping](https://msdn.microsoft.com/library/jj635217)  
  
     [Microsoft White Papers for SQL Server 2012](https://msdn.microsoft.com/library/hh403491.aspx)  
  
     [SQL Server Customer Advisory Team Whitepapers](http://sqlcat.com/)  
  
## See Also  
 [About Log Shipping &#40;SQL Server&#41;](../../log-shipping/about-log-shipping-sql-server.md)   
 [Overview of AlwaysOn Availability Groups &#40;SQL Server&#41;](overview-of-always-on-availability-groups-sql-server.md)   
 [Monitoring of Availability Groups &#40;SQL Server&#41;](monitoring-of-availability-groups-sql-server.md)  
  
  
