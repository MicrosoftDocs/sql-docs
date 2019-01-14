---
title: "Use the Fail Over Availability Group Wizard (SQL Server Management Studio) | Microsoft Docs"
ms.custom: ""
ms.date: "05/17/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: high-availability
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.failoverwizard.connecttoreplicas.f1"
  - "sql13.swb.failoverwizard.progress.f1"
  - "sql13.swb.failoverwizard.selectnewprimary.f1"
  - "sql13.swb.failoverwizard.f1"
  - "sql13.swb.failoverwizard.confirmdataloss.f1"
helpviewer_keywords: 
  - "failover [SQL Server], failover"
  - "Availability Groups [SQL Server], wizards"
  - "Availability Groups [SQL Server], configuring"
ms.assetid: 4a602584-63e4-4322-aafc-5d715b82b834
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# Use the Fail Over Availability Group Wizard (SQL Server Management Studio)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  This topic describes how to perform a planned manual failover or forced manual failover (forced failover) on an Always On availability group by using [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)], [!INCLUDE[tsql](../../../includes/tsql-md.md)], or PowerShell in [!INCLUDE[ssCurrent](../../../includes/sscurrent-md.md)]. An availability group fails over at the level of an availability replica. If you fail over to a secondary replica in the SYNCHRONIZED state, the wizard performs a planned manual failover (without data loss). If you fail over to a secondary replica in the UNSYNCHRONIZED or NOT SYNCHRONIZING state, the wizard performs a forced manual failover-also known as a *forced failover* (with possible data loss). Both forms of manual failover transition the secondary replica to which you are connected to the primary role. A planned manual failover currently transitions the former primary replica to the secondary role. After a forced failover, when the former primary replica comes online, it transitions to the secondary role.  

##  <a name="BeforeYouBegin"></a> Before You Begin  
 Before your first planned manual failover, see the "Before You Begin" section in [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server.md).  
  
 Before your first forced failover, see the "Before You Begin" and "Follow Up: Essential Tasks After a Forced Failover" sections in [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md).  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   A failover command returns as soon as the target secondary replica has accepted the command. However, database recovery occurs asynchronously after the availability group has finished failing over.  
    
###  <a name="Prerequisites"></a> Prerequisites for Using the Failover Availability Group Wizard  
  
-   You must be connected to the server instance that hosts an availability replica that is currently available.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER AVAILABILITY GROUP permission on the availability group, CONTROL AVAILABILITY GROUP permission, ALTER ANY AVAILABILITY GROUP permission, or CONTROL SERVER permission.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
 **To Use the Failover Availability Group Wizard**  
  
1.  In Object Explorer, connect to the server instance that hosts a secondary replica of the availability group that needs to be failed over, and expand the server tree.  
  
2.  Expand the **Always On High Availability** node and the **Availability Groups** node.  
  
3.  To launch the Failover Availability Group Wizard, right-click the availability group that you are going to fail over, and select **Failover**.  
  
4.  The information presented by the **Introduction** page depends on whether any secondary replica is eligible for a planned failover. If this page says, "**Perform a planned failover for this availability group**", you can failover the availability group without data loss.  
  
5.  On the **Select New Primary Replica** page, you can view the status of the current primary replica and of the WSFC quorum, before you choose the secondary replica that will become the new primary replica (the *failover target*). For a planned manual failover, be sure to select a secondary replica whose **Failover Readiness** value is "**No data loss**". For a forced failover, for all the possible failover targets, this value will be "**Data loss, Warnings(**_#_**)**", where *#* indicates the number of warnings that exist for a given secondary replica. To view the warnings for a given failover target, click its "Failover Readiness" value.  
  
     For more information, see [Select New Primary Replica page](#SelectNewPrimaryReplica), later in this topic.  
  
6.  On the **Connect to Replica** page,  connect to the failover target. For more information, see [Connect to Replica page](#ConnectToReplica), later in this topic.  
  
7.  If you are performing a forced failover, the wizard displays the **Confirm Potential Data Loss** page. To proceed with the failover, you must select **Click here to confirm failover with potential data loss**. For more information, see .[Confirm Potential Data Loss page](#ConfirmPotentialDataLoss), later in this topic.  
  
8.  On the **Summary** page, review the implications of failing over to the selected secondary replica.  
  
     If you are satisfied with your selections, optionally click **Script** to create a script of the steps the wizard will execute. Then, to failover the availability group to the selected secondary replica, click **Finish**.  
  
9. The **Progress** page displays the progress of failing over the availability group.  
  
10. When the failover operation finishes, the **Results** page displays the result. When the wizard completes, click **Close** to exit.  
  
     For more information, see [Results Page &#40;Always On Availability Group Wizards&#41;](../../../database-engine/availability-groups/windows/results-page-always-on-availability-group-wizards.md).  
  
11. After a forced failover, see the "Follow Up: After a Forced Failover" section in the [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md).  
  
## Help for Pages that are Exclusive to This Wizard  
 This section describes the pages that are unique to the [!INCLUDE[ssAoFoAgWiz](../../../includes/ssaofoagwiz-md.md)].  
  
 **In This Section**  
  
-   [Select New Primary Replica page](#SelectNewPrimaryReplica)  
  
-   [Connect to Replica page](#ConnectToReplica)  
  
-   [Confirm Potential Data Loss page](#ConfirmPotentialDataLoss)  
  
 The other pages of this wizard share help with one or more of the other Always On Availability Groups wizards and are documented in separate F1 help topics.  
  
###  <a name="SelectNewPrimaryReplica"></a> Select New Primary Replica Page  
 This section describes the options of the **Select New Primary Replica** page. Use this page to select the secondary replica (failover target) to which the availability group will fail over. This replica will become the new primary replica.  
  
#### Page Options  
 **Current Primary Replica**  
 Displays the name of the current primary replica, if it is online.  
  
 **Primary Replica Status**  
 Displays the status of the current primary replica, if it is online.  
  
 **Quorum Status**  
 For cluster type WSFC, displays the quorum status for the availability replica, one of:  
  
   |Value|Description|  
   |-----------|-----------------|  
   |**Normal quorum**|The cluster has started with normal quorum.|  
   |**Forced quorum**|The cluster has started with forced quorum.|  
   |**Unknown quorum**|The cluster quorum status is unavailable.|  
   |**Not applicable**|The node that hosts the availability replica has no quorum.|  
  
 For more information, see [WSFC Quorum Modes and Voting Configuration &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-quorum-modes-and-voting-configuration-sql-server.md).  

 For cluster type NONE, the quorum status does not apply.

 For cluster type EXTERNAL, the quorum status is managed by the cluster manager, and not visible to SQL Server.
  
 **Choose a new primary replica**  
 Use this grid to select a secondary replica to become the new primary replica. The columns in this grid are as follows:  
  
 **Server Instance**  
 Displays the name of a server instance that hosts a secondary replica.  
  
 **Availability Mode**  
 Displays the availability mode of the server instance, one of:  
  
|Value|Description|  
|-----------|-----------------|  
|**Synchronous commit**|Under synchronous-commit mode, before committing transactions, a synchronous-commit primary replica waits for a synchronous-commit secondary replica to acknowledge that it has finished hardening the log. Synchronous-commit mode ensures that once a given secondary database is synchronized with the primary database, committed transactions are fully protected.|  
|**Asynchronous commit**|Under asynchronous-commit mode, the primary replica commits transactions without waiting for acknowledgement that an asynchronous-commit secondary replica has hardened the log. Asynchronous-commit mode minimizes transaction latency on the secondary databases but allows them to lag behind the primary databases, making some data loss possible.|  
  
 For more information, see [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md).  
  
 **Failover Mode**  
 Displays the failover mode of the server instance, one of:  
  
|Value|Description|  
|-----------|-----------------|  
|**Automatic**|A secondary replica that is configured for automatic failover also supports planned manual failover whenever the secondary replica is synchronized with the primary replica.|  
|**Manual**|Two types of manual failover exist: planned (without data loss) and forced (with possible data loss). For a given secondary replica, only one of these is supported, depending on the availability mode and, for synchronous-commit mode, the synchronization state of the secondary replica. To determine which form of manual failover is currently supported by a given secondary replica, see the **Failover Readiness** column of this grid.|  
  
 For more information, see [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md).  
  
 **Failover Readiness**  
 Displays failover readiness of the secondary replica, one of:  
  
|Value|Description|  
|-----------|-----------------|  
|**No data loss**|This secondary replica currently supports planned failover. This value occurs only when a synchronous-commit mode secondary replica is currently synchronized with the primary replica.|  
|**Data loss, Warnings(** _#_ **)**|This secondary replica currently supports forced failover (with possible data loss). This value occurs whenever the secondary replica is not synchronized with the primary replica. Click the data-loss warnings link for information about the possible data loss.|  
  
 **Refresh**  
 Click to update the grid.  
  
 **Cancel**  
 Click to cancel the wizard. On the **Select New Primary Replica** page, cancelling the wizard cause it to exit without performing any actions.  
  
###  <a name="ConfirmPotentialDataLoss"></a> Confirm Potential Data Loss Page  
 This section describes the options of the **Confirm Potential Data Loss** page, which is displayed only if you are performing a forced failover. This topic is used only by the [!INCLUDE[ssAoFoAgWiz](../../../includes/ssaofoagwiz-md.md)]. Use this page to indicate whether you are willing to risk possible data loss in order to force the availability group to fail over.  
  
#### Confirm Potential Data Loss Options  
 If the selected secondary replica is not synchronized with the primary replica, the wizard displays a warning that failing over to this secondary replica could cause data loss on one or more databases.  
  
 **Click here to confirm failover with potential data loss.**  
 If you are willing to risk data loss in order to make the databases in this availability group available to users, click this checkbox. If you are not willing to risk data loss, you can either click **Previous** to return to the **Select New Primary Replica** page, or click **Cancel** to exit the wizard without failing over the availability group.  
  
 **Cancel**  
 Click to cancel the wizard. On the **Confirm Potential Data Loss** page, cancelling the wizard cause it to exit without performing any actions.  
  
###  <a name="ConnectToReplica"></a> Connect to Replica Page  
 This section describes the options of the **Connect to Replica** page of the [!INCLUDE[ssAoFoAgWiz](../../../includes/ssaofoagwiz-md.md)]. This page is displayed only if you are not connected to the target secondary replica. Use this page to connect to the secondary replica that you have selected as the new primary replica.  
  
#### Page Options  
 **Grid columns:**  
 **Server Instance**  
 Displays the name of the server instance that will host the availability replica.  
  
 **Connected As**  
 Displays the account that is connected to the server instance, once the connection has been established. If this column displays "**Not Connected**" for a given server instance, you will need to click the **Connect** button.  
  
 **Connect**  
 Click if this server instance is running under a different account than other server instances to which you need to connect.  
  
 **Cancel**  
 Click to cancel the wizard. On the **Connect to Replica** page, cancelling the wizard cause it to exit without performing any actions.  
  
## See Also  
 [Overview of Always On Availability Groups &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md)   
 [Availability Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/availability-modes-always-on-availability-groups.md)   
 [Failover and Failover Modes &#40;Always On Availability Groups&#41;](../../../database-engine/availability-groups/windows/failover-and-failover-modes-always-on-availability-groups.md)   
 [Perform a Planned Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-planned-manual-failover-of-an-availability-group-sql-server.md)   
 [Perform a Forced Manual Failover of an Availability Group &#40;SQL Server&#41;](../../../database-engine/availability-groups/windows/perform-a-forced-manual-failover-of-an-availability-group-sql-server.md)   
 [WSFC Disaster Recovery through Forced Quorum &#40;SQL Server&#41;](../../../sql-server/failover-clusters/windows/wsfc-disaster-recovery-through-forced-quorum-sql-server.md)  
  
