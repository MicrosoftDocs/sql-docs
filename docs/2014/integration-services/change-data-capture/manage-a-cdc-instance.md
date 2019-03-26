---
title: "Manage a CDC Instance | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "manIns"
ms.assetid: cfed22c8-c666-40ca-9e73-24d93e85ba92
author: janinezhang
ms.author: janinez
manager: craigg
---
# Manage a CDC Instance
  You can use the CDC Designer Console to view information about the instances that you create and to manage the operation of the instances.  
  
 Click on the name of an instance in the left pane to view the information about the instance.  
  
> [!NOTE]  
>  If you select a service in the left pane, the list of available instances is also displayed in the center of the CDC Designer Console. If you select one of the instances in this section, you can carry out the tasks in the right pane; however, you will not be able to view the information in the property tabs.  
  
## What you can do when you display the CDC Instance information  
 The following actions are carried out from the right pane:  
  
 **Start**  
 Click **Start** to start the capturing changes for the selected CDC instance.  
  
 **Stop**  
 Click **Stop** to stop capturing changes for the selected CDC instance. When you stop the CDC instance, the changes that were captured to that point are not lost and are delivered when the CDC instance is resumed.  
  
 **Reset**  
 Click **Reset** to reset the CDC instance to its initial (empty) state. This option is available when the CDC instance is stopped. All changes in the change tables and the CDC instance internal state are deleted. When the CDC instance is started later, the change capture starts from that point in time and only includes transactions that started after the CDC instance started.  
  
 Click **OK** in the confirmation dialog box to confirm that you want to reset the CDC instance and delete the changes written to the change tables.  
  
 **Delete**  
 Click **Delete** to delete the CDC instance permanently. This option is available only when the CDC instance is stopped.  
  
 Click **OK** in the confirmation dialog box to confirm that you want to delete the CDC instance.  
  
 **Oracle Logging Script**  
 Click this link to display the Oracle Logging script dialog box with the Oracle supplemental-logging script. For information on what you can do in this dialog box, see [Oracle Supplemental Logging Script](oracle-supplemental-logging-script.md).  
  
> [!NOTE]  
>  When you run the supplemental logging scripts, the Oracle Credentials for Running Script dialog box opens where you provide a valid Oracle user name and password. For information on how to provide the proper Oracle credentials, see [Oracle Credentials for Running Script](oracle-credentials-for-running-script.md).  
  
 **CDC Instance Deployment Script**  
 Click this link to display the CDC Instance Deployment Script dialog box that displays the CDC instance deployment script. For information about this dialog box, see [CDC Instance Deployment Script](cdc-instance-deployment-script.md).  
  
 **Properties**  
 Click this link to open the property editor. You edit the CDC instance configuration using the property editor. For more information about editing the properties for a CDC instance, see [Edit Instance Properties](edit-instance-properties.md).  
  
 **Viewer Tabs**  
  
 The following Viewer tabs are available when you view information for the CDC instance. The information in these tabs is read only.  
  
 **Status**  
 This tab provides information and statistics about the CDC instance current status. It contains the following information.  
  
-   **Status**: An icon that indicates the current status for the CDC instance. The following describes the statuses.  
  
    |||  
    |-|-|  
    |![Error](../media/error.gif "Error")|**Error**. The Oracle CDC Instance is not running because a non-retryable error occurred. The following sub-statuses are available:<br /><br /> **Misconfigured**: A configuration error occurred that requires manual intervention.<br /><br /> **Password Required**: No password was set for the Oracle CDC Instance or the password is not valid.<br /><br /> **Unexpected**. All other non-recoverable errors.|  
    |![Okay](../media/okay.gif "Okay")|**Running**: The CDC Instance is running and is processing change records. The following sub-statuses are available.<br /><br /> **Idle**: All change records have been processed and stored in the target change tables. There are no more active transactions.<br /><br /> **Processing**: There are change records being process that are not yet written to the change tables.|  
    |![Stop](../media/stop.gif "Stop")|**Stopped**: The CDC instance is not running. The stopped status indicates that the CDC instance was stopped in a normal manner.|  
    |![Paused](../media/paused.gif "Paused")|**Paused**: The CDC instance is running but processing is suspended because of a retryable error. The following sub-statuses are available:<br /><br /> **Disconnected**: The connection to the source Oracle database cannot be established. Processing resumes when the connection is restored.<br /><br /> **Storage**: The storage is full. Processing resumes when additional storage becomes available.<br /><br /> **Logger**: The logger is connected to Oracle but cannot read the Oracle transaction logs due to a temporary problem, for example, a required transaction log is not available.|  
  
-   **Detailed Status**: The current substatus.  
  
-   **Status Message**: More information about the current status.  
  
-   **Timestamp**: The UTC time for when the CDC state was last read from the state table.  
  
-   **Currently Processing**: You monitor the following information in this section.  
  
    -   **Last transaction timestamp**: The local time of the last transaction written to the change tables.  
  
    -   **Last change timestamp**: The local time of the most recent change seen by the Oracle CDC Instance in the source Oracle database transaction logs. This provides information about the current latency of the CDC instance in reading the Oracle transaction log.  
  
    -   **Transaction log head CN**: The most recent change number (CN) that was read from the Oracle transaction log.  
  
    -   **Transaction log tail CN**: The change number for recovery or restarting the CDC instance. The Oracle CDC instance will reposition to this location in the event of a re-start or any other type of failure (including cluster failover).  
  
    -   **Current CN**: The last change number (SCN) seen in the source Oracle database (not the transaction log).  
  
    -   **Active transactions**: The current number of source Oracle transactions that are being processed by the Oracle CDC Instance and are not yet decided (commit/rollback).  
  
    -   **Staged transactions**: The current number source Oracle transactions that are staged to the [cdc.xdbcdc_staged_transactions](the-oracle-cdc-databases.md#BKMK_cdcxdbcdc_staged_transactions) table.  
  
-   **Counters**: You monitor the following information in this section.  
  
    -   **Completed transactions**: The number of transactions completed since the CDC instance was last reset. This does not include transactions that do not contain tables of interest.  
  
    -   **Written changes**: The number of changes written to the SQL Server change tables.  
  
 **Oracle**  
 Displays information about the CDC instance and its connection to the Oracle database. This tab is read only. To edit these properties, right-click the instance in the left pane and select **Properties** or click **Properties** in the right pane to open the \<instance> Properties dialog box.  
  
 For information about these properties and how to edit them, see [Edit the Oracle Database Properties](edit-the-oracle-database-properties.md).  
  
 **Tables**  
 Displays information about the tables included in the CDC instance. Column information is also available here. This tab is read only. To edit these properties, right-click the instance in the left pane and select **Properties** or click **Properties** in the right pane to open the \<instance> Properties dialog box.  
  
 For information about these properties and how to edit them, see [Edit Tables](edit-tables.md).  
  
 **Advanced**  
 Displays the advanced properties for the CDC instance and the property values. This tab is read only. To edit these properties, right-click the instance in the left pane and select **Properties** or click **Properties** in the right pane to open the \<instance> Properties dialog box.  
  
 For information about these properties and how to edit them, see [Edit the Advanced Properties](edit-the-advanced-properties.md).  
  
## See Also  
 [How to Create the SQL Server Change Database Instance](how-to-create-the-sql-server-change-database-instance.md)   
 [How to View the CDC Instance Properties](how-to-view-the-cdc-instance-properties.md)   
 [How to Edit the CDC Instance Properties](how-to-edit-the-cdc-instance-properties.md)   
 [Use the New Instance Wizard](use-the-new-instance-wizard.md)  
  
  
