---
title: "Create SQL Trace collection set with Profiler"
description: Use SQL Server Profiler to create a SQL Trace collection set.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 12/28/2023
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Trace collector set"
---
# Use SQL Server Profiler to create a SQL Trace collection set

[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

In [!INCLUDE [ssnoversion](../../includes/ssnoversion-md.md)] you can exploit the server-side trace capabilities of [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to export a trace definition that you can use to create a collection set that uses the Generic SQL Trace collector type. There are two parts to this process:

1. Create and export a [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace.
1. Script a new collection set based on an exported trace.

The scenario for the following procedures involves collecting data about any stored procedure that requires 80 milliseconds or longer to complete. In order to complete these procedures you should be able to:

- Use [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to create and configure a trace.
- Use [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] to open, edit, and execute a query.

### Create and export a SQL Server Profiler trace

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], open [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. (On the **Tools** menu, select **SQL Server Profiler**.)

1. In the **Connect to Server** dialog box, select **Cancel**.

1. For this scenario, ensure that duration values are configured to display in milliseconds (the default). To do this, follow these steps:

    1. On the **Tools** menu, select **Options**.

    1. In the **Display Options** area, ensure that the Show values in Duration column in microseconds check box is cleared.

    1. Select **OK** to close the **General Options** dialog box.

1. On the **File** menu, select **New Trace**.

1. In the **Connect to Server** dialog box, select the server that you want to connect to, and then select **Connect**.

     The **Trace Properties** dialog box appears.

1. On the **General** tab, do the following:

    1. In the **Trace name** box, type the name that you want to use for the trace. For this example, the trace name is `SPgt140`.

    1. In the **Use the template list**, select the template to use for the trace. For this example, select **TSQL_SPs**.

1. On the **Events Selection** tab, do the following:

    1. Identify the events to use for the trace. For this example, clear all check boxes in the **Events** column, except for **ExistingConnection** and **SP:Completed**.

    1. In the lower-right corner, select the **Show all columns** check box.

    1. Select the **SP:Completed** row.

    1. Scroll across the row to the **Duration** column, and then select the **Duration** check box.

1. In the lower-right corner, select **Column Filters** to open the **Edit Filter** dialog box. In the **Edit Filter** dialog box, do the following:

    1. In the filter list, select **Duration**.

    1. In the Boolean operator window, expand the **Greater than or equal** node, type `80` as the value, and then select **OK**.

1. Select **Run** to start the trace.

1. On the toolbar, select **Stop Selected Trace** or **Pause Selected Trace**.

1. On the **File** menu, point to **Export**, point to **Script Trace Definition**, and then select **For SQL Trace Collection Set**.

1. In the **Save As** dialog box, type the name that you want to use for the trace definition in the **File name** box, and then save it in the location that you want. For this example, the file name is the same as the trace name (`SPgt140`).

1. Select **OK** when you receive a message that the file was successfully saved, and then close [!INCLUDE [ssSqlProfiler](../../includes/sssqlprofiler-md.md)].

### Script a new collection set from a SQL Server Profiler trace

1. In [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)], on the **File** menu, point to **Open,** and then select **File**.

1. In the **Open File** dialog box, locate and then open the file that you created in the previous procedure (`SPgt140`).

     The trace information that you saved is opened in a Query window and merged into a script that you can run to create the new collection set.

1. Scroll through the script and make the following replacements, which are noted in the script comment text:

    -   Replace **SQLTrace Collection Set Name Here** with the name that you want to use for the collection set. For this example, name the collection set **SPROC_CollectionSet**.

    -   Replace **SQLTrace Collection Item Name Here** with the name that you want to use for the collection item. For this example, name the collection item **SPROC_Collection_Item**.

1. Select **Execute** to run the query and to create the collection set.

1. In Object Explorer, verify that the collection set was created. To do this, follow these steps:

    1. Right-click **Management**, and then select **Refresh**.

    1. Expand **Management**, and then expand **Data Collection**.

     The **SPROC_CollectionSet** collection set appears at the same level as the **System Data Collection Sets** node. By default, the collection set is disabled.

1. Use Object Explorer to edit the properties of SPROC_CollectionSet, such as the collection mode and upload schedule. Follow the same procedures that you would for the System Data collection sets that are provided with the data collector.

## Examples

The following code sample is the final script resulting from the steps documented in the preceding procedures.

```sql
/*************************************************************/
-- SQL Trace collection set generated from SQL Server Profiler
-- Date: 11/19/2022  12:55:31 AM
/*************************************************************/
USE msdb;
GO

BEGIN TRANSACTION

BEGIN TRY
    -- Define collection set
    -- ***
    -- *** Replace 'SqlTrace Collection Set Name Here' in the
    -- *** following script with the name you want
    -- *** to use for the collection set.
    -- ***
    DECLARE @collection_set_id INT;

    EXEC [dbo].[sp_syscollector_create_collection_set] @name = N'SPROC_CollectionSet',
        @schedule_name = N'CollectorSchedule_Every_15min',
        @collection_mode = 0, -- cached mode needed for Trace collections
        @logging_level = 0, -- minimum logging
        @days_until_expiration = 5,
        @description = N'Collection set generated by SQL Server Profiler',
        @collection_set_id = @collection_set_id OUTPUT;

    SELECT @collection_set_id;

    -- Define input and output variables for the collection item.
    DECLARE @trace_definition XML;
    DECLARE @collection_item_id INT;

    -- Define the trace parameters as an XML variable
    SELECT @trace_definition = convert(XML, N'<ns:SqlTraceCollector xmlns:ns"DataCollectorType" use_default="0">
<Events>
  <EventType name="Sessions">
    <Event id="17" name="ExistingConnection" columnslist="1,2,14,26,3,35,12" />
  </EventType>
  <EventType name="Stored Procedures">
    <Event id="43" name="SP:Completed" columnslist="1,2,26,34,3,35,12,13,14,22" />
  </EventType>
</Events>
<Filters>
  <Filter columnid="13" columnname="Duration" logical_operator="AND" comparison_operator="GE" value="80000L" />
</Filters>
</ns:SqlTraceCollector>
');

    -- Retrieve the collector type GUID for the trace collector type.
    DECLARE @collector_type_GUID UNIQUEIDENTIFIER;

    SELECT @collector_type_GUID = collector_type_uid
    FROM [dbo].[syscollector_collector_types]
    WHERE name = N'Generic SQL Trace Collector Type';

    -- Create the trace collection item.
    -- ***
    -- *** Replace 'SqlTrace Collection Item Name Here' in
    -- *** the following script with the name you want to
    -- *** use for the collection item.
    -- ***
    EXEC [dbo].[sp_syscollector_create_collection_item] @collection_set_id = @collection_set_id,
        @collector_type_uid = @collector_type_GUID,
        @name = N'SPROC_Collection_Item',
        @frequency = 900, -- specified the frequency for checking to see if trace is still running
        @parameters = @trace_definition,
        @collection_item_id = @collection_item_id OUTPUT;

    SELECT @collection_item_id;

    COMMIT TRANSACTION;
END TRY

BEGIN CATCH
    ROLLBACK TRANSACTION;

    DECLARE @ErrorMessage NVARCHAR(4000);
    DECLARE @ErrorSeverity INT;
    DECLARE @ErrorState INT;
    DECLARE @ErrorNumber INT;
    DECLARE @ErrorLine INT;
    DECLARE @ErrorProcedure NVARCHAR(200);

    SELECT @ErrorLine = ERROR_LINE(),
        @ErrorSeverity = ERROR_SEVERITY(),
        @ErrorState = ERROR_STATE(),
        @ErrorNumber = ERROR_NUMBER(),
        @ErrorMessage = ERROR_MESSAGE(),
        @ErrorProcedure = ISNULL(ERROR_PROCEDURE(), '-');

    RAISERROR (
            14684,
            @ErrorSeverity,
            1,
            @ErrorNumber,
            @ErrorSeverity,
            @ErrorState,
            @ErrorProcedure,
            @ErrorLine,
            @ErrorMessage
            );
END CATCH;
GO
```
