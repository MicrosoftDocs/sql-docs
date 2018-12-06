---
title: "Disable Compression on a Table or Index | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql  
ms.reviewer: ""
ms.prod_service: database-engine, sql-database, sql-data-warehouse, pdw
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "data compression [SQL Server], disabling"
ms.assetid: bda1e452-397b-4757-82a4-181217361589
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || = sqlallproducts-allversions"
---
# Disable Compression on a Table or Index

[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

  This topic describes how to disable compression on a table or index in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] by using [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 **In This Topic**  
  
-   **Before you begin:**  
  
     [Limitations and Restrictions](#Restrictions)  
  
     [Security](#Security)  
  
-   **To disable compression on a table or index, using:**  
  
     [SQL Server Management Studio](#SSMSProcedure)  
  
     [Transact-SQL](#TsqlProcedure)  
  
##  <a name="BeforeYouBegin"></a> Before You Begin  
  
###  <a name="Restrictions"></a> Limitations and Restrictions  
  
-   If the table is a heap, the rebuild operation for ONLINE mode will be single threaded. Use OFFLINE mode for a multi-threaded heap rebuild operation. For a more information about data compression, see [Data Compression](../../relational-databases/data-compression/data-compression.md).  
  
-   To evaluate how changing the compression state will affect a table, an index, or a partition, use the [sp_estimate_data_compression_savings](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md) stored procedure.  
  
-   You cannot change the compression setting of a single partition if the table has nonaligned indexes.  
  
###  <a name="Security"></a> Security  
  
####  <a name="Permissions"></a> Permissions  
 Requires ALTER permission on the table or index.  
  
##  <a name="SSMSProcedure"></a> Using SQL Server Management Studio  
  
#### To disable compression on a table  
  
1.  In Object Explorer, expand the database that contains the table on which you want to disable compression and then expand the **Tables** folder.  
  
2.  Right-click the table or index on which you want to disable compression, point to **Storage** and select **Manage Compression...**.  
  
3.  To disable compression on an index, expand the table that contains the index and then expand the **Indexes** folder.  
  
4.  In the Data Compression Wizard, on the **Welcome to the Data Compression Wizard** page, click **Next**.  
  
5.  On the **Select Compression Type** page, select **None** as the compression type to apply to each partition in the index on which you want to disable compression. When finished, click **Next**.  
  
     The following options are available on the **Select Compression Type** page:  
  
     **Use the same compression type for all partitions** check box  
     Select to configure the same compression setting for all partitions. This enables the selection box and disables the **Compression Type** column in the grid. When selected, the options in the adjacent list are **None**, **Row**, and **Page**.  
  
     **Partition Number**  
     Lists each partition in the table or index. This column is read-only.  
  
     **Compression Type**  
     Select the compression option for each partition. Is not available when **Use the same compression type for all partitions** is selected. List options are **None**, **Row**, and **Page**.  
  
     **Boundary**  
     Displays the partition boundary. This column is read-only.  
  
     **Row Count**  
     Displays the number of rows in this partition. This column is read-only.  
  
     **Current Space**  
     Displays the current space this partition occupies in megabytes (MB). This column is read-only.  
  
     **Requested Compressed Space**  
     After you click **Calculate**, this column displays the estimated size of each partition after compression by using the setting specified in the **Compression Type** column. This column is read-only.  
  
     **Calculate**  
     Click to estimate the size of each partition after compression by using the setting specified in the **Compression Type** column.  
  
6.  In the **Select an Output Option** page, specify how you want to complete this task. Select **Create Script** to create a SQL script based the previous pages in the wizard. Select **Run immediately** to create the new partitioned table after completing all remaining pages in the wizard. Select **Schedule** to create the new partitioned table at a predetermined time in the future.  
  
     If you select **Create script**, the following options are available under **Script options**:  
  
     **Script to file**  
     Generates the script as a .sql file. Enter a file name and location in the **File name** box or click **Browse** to open the **Script File Location** dialog box. From **Save As**, select **Unicode text** or **ANSI text**.  
  
     **Script to Clipboard**  
     Saves the script to the Clipboard.  
  
     **Script to New Query Window**  
     Generates the script to a new Query Editor window. This is the default selection.  
  
     If you select **Schedule**, click **Change schedule**.  
  
    1.  In the **New Job Schedule** dialog box, in the **Name** box, enter the job schedule's name.  
  
    2.  On the **Schedule type** list, select the type of schedule:  
  
        -   **Start automatically when SQL Server Agent starts**  
  
        -   **Start whenever the CPUs become idle**  
  
        -   **Recurring**. Select this option if your new partitioned table updates with new information on a regular basis.  
  
        -   **One time**. This is the default selection.  
  
    3.  Select or clear the **Enabled** check box to enable or disable the schedule.  
  
    4.  If you select **Recurring**:  
  
        1.  Under **Frequency**, on the **Occurs** list, specify the frequency of occurrence:  
  
            -   If you select **Daily**, in the **Recurs every** box, enter how often the job schedule repeats in days.  
  
            -   If you select **Weekly**, in the **Recurs every** box, enter how often the job schedule repeats in weeks. Select the day or days of the week on which the job schedule is run.  
  
            -   If you select **Monthly**, select either **Day** or **The**.  
  
                -   If you select **Day**, enter both the date of the month you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the 15th day of the month every other month, select **Day** and enter "15" in the first box and "2" in the second box. Please note that the largest number allowed in the second box is "99".  
  
                -   If you select **The**, select the specific day of the week within the month that you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the last weekday of the month every other month, select **Day**, select **last** from the first list and **weekday** from the second list, and then enter "2" in the last box. You can also select **first**, **second**, **third**, or **fourth**, as well as specific weekdays (for example: Sunday or Wednesday) from the first two lists. Please note that the largest number allowed in the last box is "99".  
  
        2.  Under **Daily frequency**, specify how often the job schedule repeats on the day the job schedule runs:  
  
            -   If you select **Occurs once at**, enter the specific time of day when the job schedule should run in the **Occurs once at** box. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
            -   If you select **Occurs every**, specify how often the job schedule runs during the day chosen under **Frequency**. For example, if you want the job schedule to repeat every 2 hours during the day that the job schedule is run, select **Occurs every**, enter "2" in the first box, and then select **hour(s)** from the list. From this list you can also select **minute(s)** and **second(s)**. Please note that the largest number allowed in the first box is "100".  
  
                 In the **Starting at** box, enter the time that the job schedule should start running. In the **Ending at** box, enter the time that the job schedule should stop repeating. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
        3.  Under **Duration**, in **Start date**, enter the date that you want the job schedule to start running. Select **End date** or **No end date** to indicate when the job schedule should stop running. If you select **End date**, enter the date that you want to job schedule to stop running.  
  
    5.  If you select **One Time**, under **One-time occurrence**, in the **Date** box, enter the date that the job schedule will be run. In the **Time** box, enter the time that the job schedule will be run. Enter the hour, minute, and second of the day, as well as AM or PM.  
  
    6.  Under **Summary**, in **Description**, verify that all job schedule settings are correct.  
  
    7.  Click **OK**.  
  
     After completing this page, click **Next**.  
  
7.  On the **Review Summary** page, under **Review your selections**, expand all available options to verify that all settings are correct. If everything is as expected, click **Finish**.  
  
8.  On the **Compression Wizard Progress** page, monitor status information about the actions of the Create Partition Wizard. Depending on the options that you selected in the wizard, the progress page might contain one or more actions. The top box displays the overall status of the wizard and the number of status, error, and warning messages that the wizard has received.  
  
     The following options are available on the **Compression Wizard Progress** page:  
  
     **Details**  
     Provides the action, status, and any messages that are returned from action taken by the wizard.  
  
     **Action**  
     Specifies the type and name of each action.  
  
     **Status**  
     Indicates whether the wizard action as a whole returned the value of **Success** or **Failure**.  
  
     **Message**  
     Provides any error or warning messages that are returned from the process.  
  
     **Report**  
     Creates a report that contains the results of the Create Partition Wizard. The options are **View Report**, **Save Report to File**, **Copy Report to Clipboard**, and **Send Report as Email**.  
  
     **View Report**  
     Opens the **View Report** dialog box, which contains a text report of the progress of the Create Partition Wizard.  
  
     **Save Report to File**  
     Opens the **Save Report As** dialog box.  
  
     **Copy Report to Clipboard**  
     Copies the results of the wizard's progress report to the Clipboard.  
  
     **Send Report as Email**  
     Copies the results of the wizard's progress report into an email message.  
  
     When complete, click **Close**.  
  
##  <a name="TsqlProcedure"></a> Using Transact-SQL  
  
#### To disable compression on a table  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    ALTER TABLE Person.Person REBUILD PARTITION = ALL  
    WITH (DATA_COMPRESSION = NONE);  
    GO  
    ```  
  
#### To disable compression on an index  
  
1.  In **Object Explorer**, connect to an instance of [!INCLUDE[ssDE](../../includes/ssde-md.md)].  
  
2.  On the Standard bar, click **New Query**.  
  
3.  Copy and paste the following example into the query window and click **Execute**.  
  
    ```  
    USE AdventureWorks2012;  
    GO  
    ALTER INDEX AK_Person_rowguid ON Person.Person REBUILD PARTITION = ALL WITH (DATA_COMPRESSION = NONE);  
    GO  
    ```  
  
 For more information, see [ALTER TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-table-transact-sql.md) and [ALTER INDEX &#40;Transact-SQL&#41;](../../t-sql/statements/alter-index-transact-sql.md).  
  
  
