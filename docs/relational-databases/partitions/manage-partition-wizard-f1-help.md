---
title: "Manage Partition Wizard F1 Help | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: 
ms.topic: "reference"
f1_keywords: 
  - "sql13.swb.managepartition.createjob.f1"
  - "sql13.swb.managepartition.progress.f1"
  - "sql13.swb.managepartition.getstart.f1"
  - "sql13.swb.managepartition.selectswitchtables.f1"
  - "sql13.swb.managepartition.stagingtable.f1"
  - "sql13.swb.managepartition.switchin.f1"
  - "sql13.swb.managepartition.switchout.f1"
  - "sql13.swb.managepartition.partitionaction.f1"
  - "sql13.swb.managepartition.summary.f1"
  - "sql13.swb.managepartition.selectoutput.f1"
helpviewer_keywords: 
  - "wizards [SQL Server Management Studio] See Manage Partition Wizard"
ms.assetid: e2478d26-dea4-428d-98c5-aad2d2a30da8
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# Manage Partition Wizard F1 Help
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Use the **Manage Partition Wizard** to manage and modify existing partitioned tables through partition switching or the implementation of a sliding window scenario. This wizard can ease the management of your partitions and simplify the regular migration of data in and out of your tables.  
  
### To start the Manage Partition Wizard  
  
-   In [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], select the database, right-click the table on which you want to create partitions, point to **Storage**, and then click **Manage Partition**.  
  
     **Note** If **Manage Partition** is unavailable, you may have selected a table that does not contain partitions. Click **Create Partition** on the **Storage** submenu and use the **Create Partition Wizard** to create partitions in your table.  
  
 For general information about partitions and indexes, see [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md).  
  
 This section provides the information that is required to manage, modify, and implement partitions using the **Manage Partition Wizard**.  
  
##  <a name="Top"></a> In This Section  
 The following sections provide help on the pages of the **Manage Partition Wizard**.  
  
 [Manage Partition Wizard (Select Partition Action Page)](#SelectPartitionAction)  
  
 [Manage Partition Wizard (Switch In Page)](#SwitchIn)  
  
 [Manage Partition Wizard (Switch Out Page)](#SwitchOut)  
  
 [Manage Partition Wizard (Select Staging Table Options Page)](#StagingTableOptions)  
  
 [Manage Partition Wizard (Select Output Option Page)](#OutputOption)  
  
 [Manage Partition Wizard (New Job Schedule Page)](#NewJob)  
  
 [Manage Partition Wizard (Summary Page)](#Summary)  
  
 [Manage Partition Wizard (Progress Page)](#Progress)  
  
##  <a name="SelectPartitionAction"></a> Select Partition Action Page  
 Use the **Select Partition Action** page to choose the action you want to perform on your partition.  
  
### Create a Staging Table  
 Partition switching is a common partitioning task if you have a partitioned table that you migrate data into and out of on a regular basis; for example, you have a partitioned table that stores current quarterly data, and you must move in new data and archive older data at the end of each quarter.  
  
 The wizard designs the staging table with the same partitioning column, table and column structure, and indexes, and stores the new table in the filegroup where your source partition is located.  
  
 To create a staging table to switch in or switch out partition data, select **Create a staging table for partition switching**.  
  
### Sliding Window Scenario  
 To manage your partitions in a sliding-window scenario, select **Manage partitioned data in a sliding window scenario**.  
  
## UIElement List  
 **Create a staging table for partition switching**  
 Creates a staging table for the data you are switching in or switching out of the existing partitioned table.  
  
 **Switch out partition**  
 Provides options when removing a partition from the table.  
  
 **Switch in partition**  
 Provides options when adding a partition to the table.  
  
 **Manage partitioned data in a sliding window scenario**  
 Appends an empty partition to the existing table that can be used for switching in data. The wizard currently supports switching into the last partition and switching out the first partition.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
##  <a name="SwitchIn"></a> Select Partition Switching-In Options Page  
 Use the **Select Partition Switching-In options** page to select the staging table you are switching into the partitioned table.  
  
## UIElement List  
 **Show All Partitions**  
 Select to show all partitions, including the partitions currently in the partitioned table.  
  
 **Partition grid**  
 Displays the partition name, **Left boundary**, **Right boundary**, **Filegroup**, and **Row count** of the partitions you selected.  
  
 **Switch in table**  
 Select the staging table that contains the partition that you want to add to your partitioned table. You must create this staging table before you switch-in partitions with the **Manage PartitionsWizard**.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
##  <a name="SwitchOut"></a> Select Partition Switching-Out Options Page  
 Use the **Select Partition Switching-Out options** page to select the partition and the staging table to hold the partitioned data that you are switching out of the partitioned table.  
  
## UIElement List  
 **Partition grid**  
 Displays the partition name, **Left boundary**, **Right boundary**, **Filegroup**, and **Row count** of the partitions you selected.  
  
 **Switch out table**  
 Choose a new table or an existing table to switch-out your data to.  
  
 **New**  
 Enter a new name for the staging table you want to use for the partition to switch out of the current source table.  
  
 **Existing**  
 Select an existing staging table you want to use for the partition you want to switch out of the current source table. If the existing table contains data, this data will be overwritten with the data you are switching out.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
##  <a name="StagingTableOptions"></a> Select the Staging Table Options Page  
 Use the **Select the Staging Table Options** page to create the staging table you want to use for switching your partitioned data.  
  
 Staging tables must reside in the same filegroup of the selected partition where the source table is located. The staging table must mirror the design of both the source table and the destination table.  
  
 You can also create the same indexes in the staging table that exist in the source partition. The staging table automatically contains a constraint based on the elements of the source partition. This constraint is typically generated from the boundary value of the source partition.  
  
## UIElement List  
 **Staging table name**  
 Create a name for the staging table or accept the default name displayed in the edit box.  
  
 **Switch partition**  
 Select the source partition that you want to switch out of the current table.  
  
 **New boundary value**  
 Select or enter the boundary value you want for the partition in the staging table.  
  
 **Filegroup**  
 Select a filegroup for the new table.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
##  <a name="OutputOption"></a> Select Output Option Page  
 Use the **Select Output Option** page to specify how you want to complete the modifications to your partitions.  
  
### Create Script  
 When the wizard finishes, it creates a script in Query Editor to modify partitions in the table. Select **Create Script** if you want to review the script, and then execute the script manually.  
  
 **Script to file**  
 Generate the script to a .sql file. Specify either **Unicode** or **ANSI text**. To specify a name and location for the file, click **Browse**.  
  
 **Script to Clipboard**  
 Save the script to the Clipboard.  
  
 **Script to New Query Window**  
 Generate the script to a Query Editor window. If no editor window is open, a new editor window opens as the target for the script.  
  
### Run Immediately  
 **Run immediately**  
 Have the wizard finish modifications to the partitions when you click **Next** or **Finish**.  
  
### Schedule  
 Select to modify the table partitions at a scheduled date and time.  
  
 **Change schedule**  
 Opens the **New Job Schedule** dialog box, where you can select, change, or view the properties of the scheduled job.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
##  <a name="NewJob"></a> New Job Schedule Page  
 Use the **New Job Schedule** page to view and change the properties of the schedule.  
  
### Options  
 Select the type of schedule you want for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent job.  
  
 **Name**  
 Type a new name for the schedule.  
  
 **Jobs in schedule**  
 View the existing jobs that use the schedule.  
  
 **Schedule type**  
 Select the type of schedule.  
  
 **Enabled**  
 Enable or disable the schedule.  
  
### Recurring Schedule Types Options  
 Select the frequency of the scheduled job.  
  
 **Occurs**  
 Select the interval at which the schedule recurs.  
  
 **Recurs every**  
 Select the number of days or weeks between recurrences of the schedule. This option is not available for monthly schedules.  
  
 **Monday**  
 Set the job to occur on a Monday. Only available for weekly schedules.  
  
 **Tuesday**  
 Set the job to occur on a Tuesday. Only available for weekly schedules.  
  
 **Wednesday**  
 Set the job to occur on a Wednesday. Only available for weekly schedules.  
  
 **Thursday**  
 Set the job to occur on a Thursday. Only available for weekly schedules.  
  
 **Friday**  
 Set the job to occur on a Friday. Only available for weekly schedules.  
  
 **Saturday**  
 Set the job to occur on a Saturday. Only available for weekly schedules.  
  
 **Sunday**  
 Set the job to occur on a Sunday. Only available for weekly schedules.  
  
 **Day**  
 Select the day of the month the schedule occurs. Only available for monthly schedules.  
  
 **of every**  
 Select the number of months between occurrences of the schedule. Only available for monthly schedules.  
  
 **The**  
 Specify a schedule for a specific day of the week on a specific week within the month. Only available for monthly schedules.  
  
 **Occurs once at**  
 Set the time for a job to occur daily.  
  
 **Occurs every**  
 Set the number of hours or minutes between occurrences.  
  
 **Start date**  
 Set the date when this schedule will become effective.  
  
 **End date**  
 Set the date when the schedule will no longer be effective.  
  
 **No end date**  
 Specify that the schedule will remain effective indefinitely.  
  
### One Time Schedule Types Options  
 If you schedule a job to run once, you must select a date and time in the future.  
  
 **Date**  
 Select the date for the job to run.  
  
 **Time**  
 Select the time for the job to run.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
##  <a name="Summary"></a> Summary Page  
 Use the **Summary** page to review the options that you have selected on the previous pages.  
  
## UIElement List  
 **Review your selections**  
 Displays the selections you have made for each page of the wizard. Click a node to expand and view your previously selected options.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
##  <a name="Progress"></a> Progress Page  
 Use the **Progress** page to monitor status information about the actions of the **Manage Partition Wizard**. Depending on the options that you selected in the wizard, the **Progress** page might contain one or more actions. The top box displays the overall status of the wizard and the number of status, error, and warning messages that the wizard has received.  
  
### Options  
 **Details**  
 Provides the action, status, and any messages that are returned from action taken by the wizard.  
  
 **Action**  
 Specifies the type and name of each action.  
  
 **Status**  
 Indicates whether the wizard action as a whole returned the value of **Success** or **Failure**.  
  
 **Message**  
 Provides any error or warning messages that are returned from the process.  
  
 **Stop**  
 Stop the action of the wizard.  
  
 **Report**  
 Create a report that contains the results of the **Manage Partition Wizard**. The options are:  
  
-   **View Report**  
  
-   **Save Report to File**  
  
-   **Copy Report to Clipboard**  
  
-   **Send Report as Email**  
  
 **View Report**  
 Open the **View Report** dialog box. This dialog box contains a text report of the progress of the **Manage Partition Wizard**.  
  
 **Close**  
 Close the wizard.  
  
 ![Arrow icon used with Back to Top link](../../analysis-services/instances/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [In This Section](#Top)  
  
## See Also  
 [Partitioned Tables and Indexes](../../relational-databases/partitions/partitioned-tables-and-indexes.md)  
  
  
