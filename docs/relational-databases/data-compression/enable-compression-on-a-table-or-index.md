---
title: "Enable compression on a table or index"
description: Learn how to enable compression on a table or index in SQL Server by using SQL Server Management Studio or Transact-SQL.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 08/21/2023
ms.service: sql
ms.subservice: performance
ms.topic: conceptual
f1_keywords:
  - "sql13.swb.compwiz.compressiontype.f1"
  - "sql13.swb.compwiz.outputoptions.f1"
  - "sql13.swb.compwiz.summary.f1"
  - "sql13.swb.compwiz.scriptfileoption.f1"
  - "sql13.swb.compwiz.progress.f1"
  - "sql13.swb.compwiz.welcome.f1"
  - "sql13.swb.compwiz.createjob.f1"
  - "sql13.swb.compwiz.selectaction.f1"
helpviewer_keywords:
  - "data compression wizard"
  - "compression [SQL Server], enable"
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016"
---
# Enable compression on a table or index

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to enable [data compression](data-compression.md) on an existing table or index in [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] by using [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] or [!INCLUDE [tsql](../../includes/tsql-md.md)]. To enable data compression when creating a table or index, see the [Create a compressed index](../../t-sql/statements/create-index-transact-sql.md#l-create-a-compressed-index) and [Creating a table that uses row compression](../../t-sql/statements/create-table-transact-sql.md#n-creating-a-table-that-uses-row-compression) examples.

## Limitations and restrictions

- System tables can't be enabled for compression.

- If the table is a heap, the rebuild operation for `ONLINE` mode is single threaded. Use `OFFLINE` mode for a multi-threaded heap rebuild operation. Rebuild operations are `OFFLINE` unless you specify the `ONLINE` option. For complete information on performing an `ONLINE` rebuild, see [Perform Index Operations Online](../indexes/perform-index-operations-online.md).

- You can't change the compression setting of a single partition if the table has nonaligned indexes.

- Several data types aren't affected by data compression. For more detail, see [How row compression affects storage](row-compression-implementation.md#how-row-compression-affects-storage).

## Permissions

Requires `ALTER` permission on the table or index.

## <a id="SSMSProcedure"></a> Use SQL Server Management Studio

1. In Object Explorer, expand the database that contains the table that you want to compress and then expand the **Tables** folder.

1. To compress an index, expand the table that contains the index that you want to compress and then expand the **Indexes** folder.

1. Right-click the table or index to compress, point to **Storage** and select **Manage Compression...**.

1. In the Data Compression Wizard, on the **Welcome to the Data Compression Wizard** page, select **Next**.

1. On the **Select Compression Type** page, select the compression type to apply to each partition in the table or index you want to compress. When finished, select **Next**.

   The following options are available on the **Select Compression Type** page:

   - **Use the same compression type for all partitions** check box

     Select to configure the same compression setting for all partitions. This enables the selection box and disables the **Compression Type** column in the grid. When selected, the options in the adjacent list are **None**, **Row**, and **Page**.

   - **Partition Number**

     Lists each partition in the table or index. This column is read-only.

   - **Compression Type**

     Select the compression option for each partition. Isn't available when **Use the same compression type for all partitions** is selected. List options are **None**, **Row**, and **Page**.

   - **Boundary**

     Displays the partition boundary. This column is read-only.

   - **Row Count**

     Displays the number of rows in this partition. This column is read-only.

   - **Current Space**

     Displays the current space this partition occupies in megabytes (MB). This column is read-only.

   - **Requested Compressed Space**

     After you select **Calculate**, this column displays the estimated size of each partition after compression by using the setting specified in the **Compression Type** column. This column is read-only.

   - **Calculate**

     Select to estimate the size of each partition after compression by using the setting specified in the **Compression Type** column.

1. In the **Select an Output Option** page, specify how you want to complete your compression. Select **Create Script** to create a SQL script based the previous pages in the wizard. Select **Run immediately** to create the new partitioned table after completing all remaining pages in the wizard. Select **Schedule** to create the new partitioned table at a predetermined time in the future.

   If you select **Create script**, the following options are available under **Script options**:

   - **Script to file**  
     Generates the script as a `.sql` file. Enter a file name and location in the **File name** box or select **Browse** to open the **Script File Location** dialog box. From **Save As**, select **Unicode text** or **ANSI text**.

   - **Script to Clipboard**  
     Saves the script to the Clipboard.

   - **Script to New Query Window**  
     Generates the script to a new Query Editor window. This is the default selection.

   - If you select **Schedule**, select **Change schedule**.

   1. In the **New Job Schedule** dialog box, in the **Name** box, enter the job schedule's name.

   1. On the **Schedule type** list, select the type of schedule:

      - **Start automatically when SQL Server Agent starts**

      - **Start whenever the CPUs become idle**

      - **Recurring**. Select this option if your new partitioned table updates with new information regularly.

      - **One time**. This option is the default selection.

   1. Select or clear the **Enabled** check box to enable or disable the schedule.

   1. If you select **Recurring**:

      1. Under **Frequency**, on the **Occurs** list, specify the frequency of occurrence:

         - If you select **Daily**, in the **Recurs every** box, enter how often the job schedule repeats in days.

         - If you select **Weekly**, in the **Recurs every** box, enter how often the job schedule repeats in weeks. Select the day or days of the week on which the job schedule is run.

         - If you select **Monthly**, select either **Day** or **The**.

           - If you select **Day**, enter both the date of the month you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the 15th day of the month every other month, select **Day** and enter "15" in the first box and "2" in the second box. The largest number allowed in the second box is "99".

           - If you select **The**, select the specific day of the week within the month that you want the job schedule to run and how often the job schedule repeats in months. For example, if you want the job schedule to run on the last weekday of the month every other month, select **Day**, select **last** from the first list and **weekday** from the second list, and then enter "2" in the last box. You can also select **first**, **second**, **third**, or **fourth**, as well as specific weekdays (for example: Sunday or Wednesday) from the first two lists. The largest number allowed in the last box is "99".

      1. Under **Daily frequency**, specify how often the job schedule repeats on the day the job schedule runs:

         - If you select **Occurs once at**, enter the specific time of day when the job schedule should run in the **Occurs once at** box. Enter the hour, minute, and second of the day, as well as AM or PM.

         - If you select **Occurs every**, specify how often the job schedule runs during the day chosen under **Frequency**. For example, if you want the job schedule to repeat every 2 hours during the day that the job schedule is run, select **Occurs every**, enter "2" in the first box, and then select **hour(s)** from the list. From this list you can also select **minute(s)** and **second(s)**. The largest number allowed in the first box is "100".

           In the **Starting at** box, enter the time that the job schedule should start running. In the **Ending at** box, enter the time that the job schedule should stop repeating. Enter the hour, minute, and second of the day, as well as AM or PM.

      1. Under **Duration**, in **Start date**, enter the date that you want the job schedule to start running. Select **End date** or **No end date** to indicate when the job schedule should stop running. If you select **End date**, enter the date that you want to job schedule to stop running.

   1. If you select **One Time**, under **One-time occurrence**, in the **Date** box, enter the date that the job schedule will be run. In the **Time** box, enter the time that the job schedule will be run. Enter the hour, minute, and second of the day, as well as AM or PM.

   1. Under **Summary**, in **Description**, verify that all job schedule settings are correct.

   1. Select **OK**.

   After completing this page, select **Next**.

1. On the **Review Summary** page, under **Review your selections**, expand all available options to verify that all compression settings are correct. If everything is as expected, select **Finish**.

1. On the **Compression Wizard Progress** page, monitor status information about the actions of the Create Partition Wizard. Depending on the options that you selected in the wizard, the progress page might contain one or more actions. The top box displays the overall status of the wizard and the number of status, error, and warning messages that the wizard has received.

   The following options are available on the **Compression Wizard Progress** page:

   - **Details**

     Provides the action, status, and any messages that are returned from action taken by the wizard.

   - **Action**

     Specifies the type and name of each action.

   - **Status**

     Indicates whether the wizard action as a whole returned the value of **Success** or **Failure**.

   - **Message**

     Provides any error or warning messages that are returned from the process.

   - **Report**

     Creates a report that contains the results of the Create Partition Wizard. The options are **View Report**, **Save Report to File**, **Copy Report to Clipboard**, and **Send Report as Email**.

   - **View Report**

     Opens the **View Report** dialog box, which contains a text report of the progress of the Create Partition Wizard.

   - **Save Report to File**

     Opens the **Save Report As** dialog box.

   - **Copy Report to Clipboard**

     Copies the results of the wizard's progress report to the Clipboard.

   - **Send Report as Email**

     Copies the results of the wizard's progress report into an email message.

   When complete, select **Close**.

## <a id="TsqlProcedure"></a> Use Transact-SQL

### SQL Server and Azure SQL Database

In [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)], run `sp_estimate_data_compression_savings` and then enable compression on the table or index. See the following sections.

#### Enable compression on a table

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. The example first executes the stored procedure `sp_estimate_data_compression_savings` to return the estimated size of the object if it were to use the `ROW` compression setting. The example then enables `ROW` compression on all partitions in the specified table.

   ```sql
   USE AdventureWorks2022;
   GO
   EXEC sp_estimate_data_compression_savings 'Production', 'TransactionHistory', NULL, NULL, 'ROW';

   ALTER TABLE Production.TransactionHistory REBUILD PARTITION = ALL
   WITH (DATA_COMPRESSION = ROW);
   GO
   ```

#### Enable compression on an index

1. In **Object Explorer**, connect to an instance of [!INCLUDE [ssDE](../../includes/ssde-md.md)].

1. On the Standard bar, select **New Query**.

1. Copy and paste the following example into the query window and select **Execute**. The example first queries the `sys.indexes` catalog view to return the name and `index_id` for each index on the `Production.TransactionHistory` table. It then executes the stored procedure `sp_estimate_data_compression_savings` to return the estimated size of the specified index ID if it were to use the `PAGE` compression setting. Finally, the example rebuilds index ID 2 (`IX_TransactionHistory_ProductID`), specifying `PAGE` compression.

   ```sql
   USE AdventureWorks2022;
   GO
   SELECT name, index_id
   FROM sys.indexes
   WHERE OBJECT_NAME (object_id) = N'TransactionHistory';

   EXEC sp_estimate_data_compression_savings
       @schema_name = 'Production',
       @object_name = 'TransactionHistory',
       @index_id = 2,
       @partition_number = NULL,
       @data_compression = 'PAGE';

   ALTER INDEX IX_TransactionHistory_ProductID ON Production.TransactionHistory REBUILD PARTITION = ALL WITH (DATA_COMPRESSION = PAGE);
   GO
   ```

For more information, see [ALTER TABLE (Transact-SQL)](../../t-sql/statements/alter-table-transact-sql.md) and [ALTER INDEX (Transact-SQL)](../../t-sql/statements/alter-index-transact-sql.md).

## See also

- [Data compression](data-compression.md)
- [Row compression implementation](row-compression-implementation.md)
- [Page compression implementation](page-compression-implementation.md)
- [sp_estimate_data_compression_savings (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-estimate-data-compression-savings-transact-sql.md)
- [Create a compressed index](../../t-sql/statements/create-index-transact-sql.md#l-create-a-compressed-index)
- [Create a table that uses row compression](../../t-sql/statements/create-table-transact-sql.md#n-creating-a-table-that-uses-row-compression)
