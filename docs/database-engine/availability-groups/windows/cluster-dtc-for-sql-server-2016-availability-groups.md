---
title: "Cluster DTC service for an availability group"
description: "Describes the requirements and steps for clustering the Microsoft Distributed Transaction Coordinator (DTC) service for an Always On availability group. "
author: MashaMSFT
ms.author: mathoma
ms.date: "08/30/2016"
ms.service: sql
ms.subservice: availability-groups
ms.topic: how-to
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2016"
---
# How to cluster the DTC service for an Always On availability group

[!INCLUDE[sql windows only](../../../includes/applies-to-version/sql-windows-only.md)]

This topic describes the requirements and steps for clustering the Microsoft Distributed Transaction Coordinator (DTC) service for [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)]. For additional information regarding distributed transactions and [!INCLUDE[ssHADR](../../../includes/sshadr-md.md)], see [Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring (SQL Server)](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md).

 ## Checklist: Preliminary Requirements

|Task|Reference|  
|-----------------|----------|  
|Ensure all nodes, services and the Availability Group have been configured correctly.|[Prerequisites, Restrictions, and Recommendations for Always On Availability Groups (SQL Server)](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md)|
|Ensure Availability Group DTC requirements have been met.|[Cross-Database Transactions and Distributed Transactions for Always On Availability Groups and Database Mirroring (SQL Server)](../../../database-engine/availability-groups/windows/transactions-always-on-availability-and-database-mirroring.md)

## Checklist: Clustered DTC Resource Dependencies

|Task|Reference|  
|-----------------|----------|  
|A shared-storage drive.|[Configuring the Shared-Storage Drive](https://msdn.microsoft.com/library/cc982358(v=bts.10).aspx). Consider using drive letter **M**.|
|A unique DTC Network Name resource.  The name will be registered as a cluster computer object in Active Directory.<br /><br />Make sure that either of the following is true:<br /><br />•	The user who creates the DTC Network Name resource has the Create Computer objects permission to the OU or the container where the DTC Network Name resource will reside.<br /><br />•	If the user does not have the Create Computer objects permission, ask a domain administrator to prestage a cluster computer object for the DTC Network Name resource.|[Prestage Cluster Computer Objects in Active Directory Domain Services](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/dn466519(v=ws.11))|
|A valid available static IP address and the appropriate subnet mask for that IP address.||

## Cluster the DTC Resource
Once you have created your Availability Group resource, create a clustered DTC resource and add it to the Availability Group.  A sample script can be seen at [Create Clustered DTC for an Always On Availability Group](../../../database-engine/availability-groups/windows/create-clustered-dtc-for-an-always-on-availability-group.md).


## Checklist: Post Clustered DTC Resource Configurations

|Task|Reference|  
|-----------------|----------|  
|Enable network access securely for the clustered DTC resource.|[Enable Network Access Securely for MS DTC](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc753620(v=ws.10))|
|Stop and disable local DTC service.|[Configure How a Service Is Started](/previous-versions/windows/it-pro/windows-server-2008-R2-and-2008/cc755249(v=ws.11))|
|Cycle the SQL Server service for each instance in the Availability Group.  Failover the Availability Group as needed.|[Perform a Planned Manual Failover of an Availability Group (SQL Server)](../../../database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server.md)<br /><br />[Start, Stop, Pause, Resume, Restart the Database Engine, SQL Server Agent, or SQL Server Browser Service](../../../database-engine/configure-windows/start-stop-pause-resume-restart-sql-server-services.md)|

- If the server is Windows Server 2012 R2 the operating system must have [KB 3030373](https://support.microsoft.com/kb/3090973) applied.

- Prepare the servers for Availability Groups according to the checklists at [Prerequisites, Restrictions, and Recommendations for Always On Availability Groups](../../../database-engine/availability-groups/windows/prereqs-restrictions-recommendations-always-on-availability.md).

- Configure the server instances for [**Always On Availability Groups**](../../../database-engine/availability-groups/windows/configuration-of-a-server-instance-for-always-on-availability-groups-sql-server.md).

### RESOURCES


[More Information on Testing DTC on Availability Groups:](/archive/blogs/dataplatform/sql-server-2016-dtc-support-in-availability-groups)

[Monitoring Always on Availability groups system views](monitor-availability-groups-transact-sql.md)

[Create Availability Group Step by Step](create-an-availability-group-transact-sql.md)


[SQL Server 2016 DTC Support in Availability Groups](/archive/blogs/dataplatform/sql-server-2016-dtc-support-in-availability-groups) 

[External link: Configure DTC for a clustered instance of SQL Server with Windows Server 2008 R2](https://sqlha.com/2013/03/12/how-to-properly-configure-dtc-for-clustered-instances-of-sql-server-with-windows-server-2008-r2/)