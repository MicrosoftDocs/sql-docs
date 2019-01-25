---
title: "SQL Server Profiler dialog boxes | Microsoft Docs"
ms.custom: ""
ms.date: "07/07/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: profiler
ms.topic: "reference"
f1_keywords: 
  - "sql13.pro.traceproperties.general.f1;"
  - "sql13.pro.traceproperties.eventsselection.f1;"
  - "sql13.pro.traceproperties.eventsselection.f1"
  - "sql13.pro.traceproperties.general.f1"
  - "sql13.pro.tracetemplateproperties"
  - "sql13.pro.edittracetemplateproperties.general.f1"
  - "sql13.pro.edittracetemplateproperties.eventsselection.f1"
  - "sql13.pro.tracefileproperties.general.f1"
  - "sql13.pro.tracefileproperties.eventsselection.f1"
  - "sql13.pro.performancecounterlimit.f1"
  - "sql13.pro.replay.tools.generaloptions.f1"
  - "sql13.pro.replay.tools.sourcetable.f1"
  - "sql13.pro.replay.tools.destinationtable.f1"
  - "sql13.pro.replay.generaloptions.f1"
  - "sql13.pro.replay.generaloptions.advanced.f1"
  - "sql13.pro.find.f1"
  - "sql13.pro.organize.columns.f1"
  - "sql13.pro.editfilter.f1"
helpviewer_keywords: 
  - "Profiler [SQL Server Profiler], help"
  - "SQL Server Profiler, help"
  - "Trace Properties dialog box"
  - "Trace Template Properties dialog box"
  - "Trace Files Properties dialog box"
  - "Performance Counters List dialog box"
  - "General Options dialog box"
  - "Select Workload Table dialog box"
  - "Destination Table dialog box"
  - "Replay Configuration dialog box"
  - "Find dialog box"
ms.assetid: e57b9160-4b78-4353-abb2-bfdbdf523d7a
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# SQL Server Profiler dialog boxes
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
Microsoft [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] is a tool that captures [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] events from a server. The events are saved in a trace file that can later be analyzed or used to replay a specific series of steps when trying to diagnose a problem. The following are the commands and settings available in the dialog boxes of [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)].  
## Trace properties
### General tab
Use the **General** tab of the **Trace Properties** dialog box to view or specify properties of a trace.  

|Item|Description
|---|---
|**Trace name** |Specify the name of the trace.  
|**Trace provider name**|Shows the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that will be traced. This field is populated automatically with the name of the server that you specified when you connected. To change the name of the trace provider, click **Cancel** to close the dialog box, and start a new trace.  
|**Trace provider type**|Shows the server type that is providing the trace. The trace definition file populates the **Trace provider type** field automatically. You cannot modify this field.  
|**version**|Shows the version of the server that is providing the trace. The trace definition file populates the **Version** field automatically. You cannot modify this field.  
|**Use the template**|Select a template from the template directory. The directory is populated with the default templates and any user-defined templates created for the current trace provider type.  
|**Save to file**|Capture the trace data to a .trc file. Saving trace data is useful for later review and analysis.  
|**Set maximum file size (MB)**|If you choose to save the trace data to a file, you must specify the maximum size of the trace file. The default is 5 megabytes (MB). The maximum size is limited only by the file system (NTFS, FAT) where the file is saved.  
|**Save As**|After you have selected to save, you can select this icon to change the file name.  
|**Enable file rollover**|Select to enable the creation of additional files to accept the trace data when the maximum file size is reached. Each new file name consists of the original .trc file name, numbered sequentially. For example, once it reaches maximum file size, **NewTrace.trc** closes, and a new file, **NewTrace_1.trc**, opens, followed by **NewTrace_2.trc**, and so on. File rollover is enabled by default when you save a trace to a file.  
|**Server processes trace data**|Specify that the server running the trace should process the trace data. Using this option reduces the performance overhead incurred by tracing. If selected, no events are skipped even under stress conditions. If this check box is cleared, processing is performed by SQL Server Profiler, and there is a possibility that some events are not traced under stress conditions.  
|**Save to table**|Capture the trace data to a database table. Saving trace data is useful for later review and analysis. However, saving trace data to a table can incur significant overhead on the server where the trace is being saved. If possible, do not save the trace table on the same server that is being traced.  
|**Destination Table**|After you have selected to save the trace data to a database table, you can select this icon to change the table name.  
| **Set maximum rows (in thousands)**|Specify the largest number of rows in which to save data. The default is 1000 rows. 
|**Enable trace stop time**|Set the date and time for the trace to end and close itself. 

### Events Selection tab
Use the **Events Selection** tab of the **Trace Properties** dialog box to view or specify traced events and data columns.  

|Item|Description
|---|---
|**Events** column|Specify traced events by selecting or clearing the check box in the event column. **Events** are organized by event category. Event classes specified in the template are automatically selected. For more information, see [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md).  
|Data columns|Specify traced data columns by checking the box that corresponds with the event and the data column you need. All relevant event columns are checked by default for each event included in the trace.  
|Filters|Specify filters by clicking the data column heading and entering the filter criteria. Filtered data columns are indicated by a filter icon to the left of the column label in the **Edit Filter** dialog box. For more information, see [SQL Server Profiler - Edit Filter](https://msdn.microsoft.com/library/a589eff5-6ec6-4f6e-94b8-831658257f14).  
|**Show all events**|Show all available events. By default, only rows in the **Events Selection** grid that are selected display. Uncheck this box to hide all unselected events in the **Events Selection** grid.  
|**Show all columns**|Show all available data columns. By default, only data columns that are selected display. Uncheck this box to hide all unselected data columns in the **Events Selection** grid.  
|**Column Filters**|Launches the **Edit Filter** dialog box. You can use this dialog to edit data column filters.  
|**Organize Columns**|Changes the order of columns in the trace and groups results by one or more columns.  

## Trace template properties 
### New (General tab)
Use the **General** tab of the **Trace Template Properties** dialog box to create new trace templates by using the following options. To access this dialog box, on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu, point to **Templates**, and then click **New**.

|Item|Description
|---|---
|**Select server type**|Specify the type of server against which this template will be used.  
|**New template name**|Provide a descriptive name for the template.  
|**Base new template on existing one**|Use a template from the list as a basis for this template. All selected events, data columns and filters initially match those in the existing template, and can then be modified as needed.  
|**Use as a default template for selected server type**|Use this template by default, for traces created for this server type.  

### Edit (General tab)
 Use the **General** tab of the **Trace Template Properties** dialog box to view or edit existing trace templates by using the following options. To access this dialog box, on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu, point to **Templates**, and then click **Edit Template**.  

|Item|Description
|---|---
|**Select server type**|Specify the type of server against which this template will be used.  
|**Select template name**|Select the template that you want to edit.  
|**Use as a default template for selected server type**|Use this template by default, for traces created for this server type.  

### Events Selection tab
Use the **Events Selection** tab of the **Trace Template Properties** dialog box to view, edit, or specify event classes and data columns to include in a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace template.  

|Item|Description
|---|---
|**Events** column|Specify events that should be traced by selecting or clearing the check box in the event column. Events are organized by event category. If you selected **Base new template on existing one** on the **General** tab, events are automatically selected according to the specified template. For more information about event classes, see [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md).  
|Data columns|Specify data columns that should be traced by checking the box that corresponds with the event and the data column you need. All relevant event columns are checked by default for each event included in the trace, if the checkbox corresponding to the event is checked. If you checked **Base new template on existing one** on the **General** tab, data columns and filters are automatically selected according to the specified template.  
|Filters|Specify filters by clicking the data column heading and entering the filter criteria. Filtered data columns are indicated by a filter icon to the left of the column label in the **Edit Filter** dialog box.  
|**Show all events**|Show all available events. This option is checked by default if you are creating a new template that is not based on an existing template. Uncheck to hide all unselected events in the **Events Selection** grid.  
|**Show all columns**|Show all available data columns. This option is checked by default if you are creating a new template that is not based on an existing template. Uncheck to hide all unselected data columns in the **Events Selection** grid.  
|**Column Filters**|Launches the **Edit Filter** dialog box, which displays a filter icon to the left of the data column label. Use the **Edit Filter** dialog box to edit data column filters.  
|**Organize Columns**|Changes the order of columns in the trace and groups results by one or more columns. 

## Trace file properties 
### General tab
Use the **General** tab of the **Trace File Properties** dialog box to view the properties of a trace file.  
To view this window, open a trace file. Then on the **File** menu, click **Properties**.  

|Item|Description
|---|---
|**File name**|The path and name of the trace file displayed.  
|**Trace provider name**|Shows the name of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that was traced.  
|**Trace provider type**|Shows the server type that provided the trace.  
|**version**|Shows the version of the server that provided the trace.  
|**File size (KB)**|The size of the trace file in kilobytes (KB).  
|**Created**|The date and time the trace file was created.  
|**Modified** |The date and time the trace file was modified.  

### Events Selection tab
Use the **Events Selection** tab of the **Trace File Template Properties** dialog box to view the column properties of the trace or remove data columns from the trace.  
To view this window, open a trace file. Then, on the **File** menu, click **Properties**, and then click the **Events Selection** tab.  

|Item|Description
|---|---
|**Events** column|View traced events which are organized by event category. Initially, all events in the trace are selected. Events can be selected by checking the box or by checking a data column for an event. If the event box is checked, all data columns available for that event are selected. If the data column for an event is checked, the event is checked and any other required column is also automatically checked. If you are viewing a trace file or table, clearing check boxes for events or data columns reduces the amount of visible data in the trace window for easier analysis. You can also change column filters to reduce the amount of visible data in the trace window. For more information about event classes, see [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md).  
|Data Columns|View traced data columns. All relevant data columns in the trace are checked by default for each event included in the trace.  
|Filters|Specify filters by clicking the data column heading and entering the filter criteria. Filtered data columns are indicated by a filter icon to the left of the column label in the **Edit Filter** dialog box.  
|**Show all events**|Show all available events. By default, only rows in the **Events Selection** grid that are selected display. Uncheck this box to hide all unselected events in the **Events Selection** grid. If **Show all events** is checked and you are viewing a trace file or table, all events that were recorded in the trace display in the trace window.  
|**Show all columns**|Show all available data columns. By default, only data columns that are selected display. Uncheck this box to hide all unselected data columns in the **Events Selection** grid.  
|**Column Filters**|Launches the **Edit Filter** dialog box, which displays a filter icon to the left of the column label for filtered data columns. Use the **Edit Filter** dialog box to edit data column filters.  
|**Organize Columns**|After selecting **Events** and data columns to trace, click **Organize Columns** to force the grid to reorder the column in the trace results window.  

## Trace table properties
### Events Selection tab
Use the **Events Selection** tab of the **Trace Table Properties** dialog box to view the events and data column properties of the trace or to remove events or columns from the trace.  
To view this window, use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to open a trace table. Then on the **File** menu, click **Properties**, and then click the **Events Selection** tab.  

|Item|Description
|---|---
|**Events** column|View traced events which are organized by event category. Events can be selected by checking the box or by checking a data column for an event. If the event box is checked, all data columns available for that event are selected. If the data column for an event is checked, the event is checked and any other required column is also automatically checked. If you are viewing a trace file or table, clearing check boxes for events or data columns reduces the amount of visible data in the trace window for easier analysis. You can also change column filters to reduce the amount of visible data in the trace window. For more information about event classes, see [SQL Server Event Class Reference](../../relational-databases/event-classes/sql-server-event-class-reference.md).  
|Other data columns|View traced data columns. All relevant data columns in the trace are checked by default for each event included in the trace.  
|Filters|Specify filters by clicking the data column heading and entering the filter criteria. Filtered data columns are indicated by a filter icon to the left of the column label in the **Edit Filter** dialog box.  
|**Show all events**|Show all available events. By default, only rows in the **Events Selection** grid that are selected display. Uncheck this box to hide all unselected events in the **Events Selection** grid. If **Show all events** is checked and you are viewing a trace file or table, all events that were recorded in the trace display in the trace window.  
|**Show all columns**|Show all available data columns. By default, only data columns that are selected display. Uncheck this box to hide all unselected data columns in the **Events Selection** grid.  
|**Column Filters**|Launches the **Edit Filter** dialog box, which displays a filter icon to the left of the column label. You can use this dialog box to edit data column filters.  
|**Organize Columns** |After selecting **Events** and data columns to trace, click **Organize Columns** to force the grid to reorder the column in the trace results window.  

## Performance counters limit
Use the Performance Counters Limit dialog box to limit the information from a System Monitor performance log file when correlating it with a [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace. You can use this dialog box to select counters that should be displayed and used for correlation.  
The **Performance Counters Limit** dialog box is populated with the performance objects and counters that the performance log file contains.  
### To select performance objects and counters to correlate with a trace  
1.  Expand a performance object to see which counters are included in the performance log file.  
2.  Check the counters that you want to correlate with the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace file.  

If you want to select all counters for a performance object, check the box that is adjacent to the performance object. Checking the topmost node, which indicates the computer, selects all performance objects and counters contained in the performance log file. 
## Tools/options (General options page)
Use the **General Options** dialog box to view or specify the following options.  
### Display options  

|Item|Description
|---|---
|**Font name**|Displays the name of the font used in the trace results grid during traces.  
|**Font size**|Displays the size of the font used in the trace results grid during traces.  
|**Choose Font**|Opens a dialog to change the font settings.  
|**Use regional settings to display date and time values**|Displays date and time values in regional settings configured for your computer. If you do not select this option, the date and time values are displayed in the fixed format used by Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], which includes milliseconds. Note that toggling this checkbox changes the time columns display format such as **StartTime** and **EndTime**. However, it does not change the **DateTime** value parameters inside the language events or remote procedure calls (RPCs).  
|**Show values in Duration column in microseconds**|Displays the values in microseconds in the **Duration** data column of traces. By default, the **Duration** column displays values in milliseconds.  

### Tracing options  

|Item|Description
|---|---
|**Start tracing immediately after making connection**|Begin a trace using the default template as soon as a connection is made.  
|**Update trace definition when provider version changes**|Apply the most current trace definition to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] when the provider is updated. This item is not checked by default. This forces [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to query the server for the trace definition and re-create, if one exists, the file on disk.  

### File rollover options  

|Item|Description
|---|---
|**Load all rollover files in sequence without prompting**|Load rollover files automatically when a trace file is opened. If more than one file was created while tracing, selecting this option automatically loads all rollover files.  
|**Prompt before loading rollover files**|Have [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] prompt you before adding a rollover file when a trace file is opened.  
|**Never load subsequent rollover files**|[!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] never loads subsequent rollover files when a trace file is opened.  

### Replay options  

|Item|Description
|---|---
|**Default number of replay threads**|Specify the number of replay threads to use concurrently. A higher number consumes more resources during replay, but increases replay concurrency.  
|**Default health monitor wait interval (sec)**|Specify the wait interval to replay in seconds. Default is 3600 seconds (1 hour). This setting affects the amount of time a thread is allowed to run before being terminated by the health monitor.  
|**Default health monitor poll interval (sec)**|Specify the health monitor poll interval during replay in seconds. Default is 60 seconds. This value allows the user to configure how often the health monitor polls for candidates for termination.

## Source table (Database Engine Tuning Advisor Select Workload table)
Microsoft SQL Server Profiler and Tuning Advisor use this dialog box to select tables.  
- In Profiler, use the **Source Table** dialog box to specify a source table for a trace table. This is a table from which a trace is loaded, and the contents of which are viewed or used for replaying the trace.  
- In Tuning Advisor, use the **Select Workload Table** dialog box to select a database table that contains profiler trace information to use as a tuning workload, or to preview the table contents before starting tuning analysis.  

|Item|Description
|---|---
|**SQL Server**|Specifies the instance of SQL Server currently connected. This field is populated automatically and cannot be updated.  
|**Database**|Specify the database where the trace table is located.  
|**Owner**|Specifies the owner of the trace table. This field is populated automatically as **dbo**.  
|**Table**|Specify the name of the trace table from which the trace should be read.  

## Destination table
Use the **Destination Table** dialog box to specify a table where you wish to store the trace.  

|Item|Description
|---|---
|**SQL Server**|Specifies the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] currently connected. This field is populated automatically and cannot be updated. To change the server, click **Cancel** and connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you want to store the trace table.  
|**Database**|Specify the database where you want the trace table to be stored.  
|**Owner**|Specifies the owner of the trace table. This field is populated automatically as **dbo**.  
|**Table**|Specify the name of the table where you want to store the trace.  

## Replay configuration
### Basic replay options
In the **Replay Configuration** dialog box, use the **Basic Replay Options** page to specify how to replay a trace file or table.  
To view this window, use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to open a trace file or table that contains the appropriate events for replay. For more information, see [Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md). While the trace file or table is open, on the **Replay** menu, click **Start**, and then connect to the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] where you want to replay the trace.  

|Item|Description
|---|---
|**Replay server**|Displays the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to connect to for the replay.  
|**Change...**|Launches the **Connect to Server** dialog box to connect to another server.  
|**Save to file** |Save the replay results to a file. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the standard file dialog, where you can specify the location to save the file.  
|**Save to table**|Save the replay results to a table. [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] displays the table selection dialog, where you can specify the location to save the table.  
|**Number of replay threads**|Specify the number of replay threads to use concurrently. A higher number consumes more resources during replay, but replay is faster and more concurrent.  
|**Replay events in the order they were traced**|Replay events sequentially. Use this option if you are replaying a trace for debugging.  
|**Replay events using multiple threads** |Replay events concurrently. This option is faster than replaying events sequentially, but disables debugging. The events are ordered within their system process identifiers (SPID).  
|**Display replay results**|Display replay results in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)]. 

### Advanced replay options
In the **Replay Configuration** dialog box, use the **Advanced Replay Options** tab to specify how to replay a trace file.  
To view this window, use [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] to open a trace file or table that contains the appropriate events for replay. For more information, see [Replay Requirements](../../tools/sql-server-profiler/replay-requirements.md). While the trace file or table is open, on the **Replay** menu, click **Start**, connect to the instance of SQL Server where you want to replay the trace, and then click the **Advanced Replay Options** tab.  

|Item|Description
|---|---
|**Replay system SPIDs**|Specifies whether [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] replays system process identifiers (SPIDs).  
|**Replay one SPID only**|Replays only the activity in the source trace file that is related to the selected SPID.  
|**SPID to replay**|Specify which SPID to replay.  
|**Limit replay by date and time**|Check to replay only a portion of the source trace file.  
|**Start time**|Date and time in the source trace file where the replay should start.  
|**End time**|Date and time in the source trace file where the replay should stop.  
|**Health monitor wait interval (sec)**|Specify the wait interval to replay in seconds. Default is 3600 seconds (1 hour). This setting affects the amount of time a process is allowed to run before being terminated by the health monitor.  
|**Health monitor poll interval (sec)**|Specify the health monitor poll interval during replay in seconds. Default is 60 seconds. This value allows the user to configure how often the health monitor polls for candidates for termination.  
|**Enable SQL Server blocked processes monitor**|Enables a process that searches for blocked or blocking processes.  
|**Blocked processes monitor wait interval (sec)**|Configures how often the blocked processes monitor searches for blocked or blocking processes.  

## Find dialog box
Use the **Find** dialog box to search a trace for specific characters or words. To cancel a search in progress, press ESC.  
 To open this dialog box in [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)], on the **Edit** menu, click **Find**.  

|Item|Description
|---|---
|**Find what**|Enter the text that you want to search for. The search matches any string containing the specified string. For example, searching for "Completed" matches "SQL:BatchCompleted." Wild card characters (*, ?, etc.) are not supported.  
|**Search in column**|Click a data column to search, or click **\<All columns>** to search all the data columns in the trace.  
|**Match case**|Finds text that has the same case as the **Find what** box. Clear this check box to find examples in the trace that are in both uppercase and lowercase text characters.  
|**Match whole word**|Restricts the search to entire words. Clear the **Match whole word** check box to search for characters within a word.  
|**Find Next**|Finds the next example of the characters in the **Find what** box.  
|**Find Previous**|Searches backwards in the trace, to find the previous example of the characters in the **Find what** box.  

 ## Organize columns
Use the **Organize Columns** dialog box to select data columns for grouping or aggregating events that are displayed in a trace, which makes large trace files or tables easier to view and analyze.  
- Aggregating moves and collapses all events in the trace under its respective event class type. A plus sign (**+**) appears to the left of the event class name. Clicking the plus sign expands the event class so you can view all events of that type.  
- Grouping organizes all event classes of a specific type together in the trace window display. However, the events are not collapsed under the event class type.  

When you group or aggregate events in a trace window display, the columns selected for grouping or aggregating remain fixed in the display window, but you can scroll to the right or left to view all other data columns.  
To access this dialog box, open an existing trace file or table, and click **Properties** on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu. In the **Trace Properties** dialog box, click the **Events Selection** tab, and then click **Organize Columns**. You can also click **Organize Columns** on the **Events Selection** tab when you are creating a new trace.  
Move data column names under **Groups** to group or aggregate event classes in the trace window.
- To aggregate events, move one data column into **Groups**. This causes all events of a specific type to be collapsed under event class type name in the trace window display. A plus sign (**+**) appears to the left of the event class name. Click the plus sign to expand the event class type and view all events. You can set aggregation and grouping on and off by clicking **Aggregated View** or **Grouped View** on the **View** menu.
- To group events, move more than one data column into **Groups**. This causes all events of a specific type to be grouped together in the trace window display, but does not collapse the events under each event class type name. You can switch back and forth between a grouped view and an ungrouped view by clicking **Grouped View** on the View menu. When more than one data column is moved into **Groups**, the option to switch to **Aggregated View** is not available.

|Item|Description
|---|---
|**Columns**|List of data columns available to move into **Groups**. Click the plus sign (**+**) to the left of **Columns** to expand the list.  
|**Up**|After selecting a data column, click **Up** to move data columns up into **Groups**. You can also click **Up** to rearrange the display of columns in the trace window display.  
|**Down**|After selecting a data column, click **Down** to move data columns out of **Groups**. You can also click **Down** to rearrange the display of columns in the trace window display.  

## Edit filter
Use the **Edit Filter** dialog box to create and modify data column filters in a trace. Click a data column name in the list and the filter criteria that is available for that data column displays in the adjacent pane. Enter the filter criteria and click **OK** to apply it to the selected data column. If a filter icon appears to the left of the data column name in the list, that column already has a filter configured for it.  
 >[!NOTE]
 >For string type data columns, the filter criteria will show as a LIKE or NOT LIKE string value.  

## Select template name
Use the **Select Template Name** dialog box to select an existing [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] trace template to export to a file on the operating system. You can also use this dialog box to select or enter a different name to save a trace template as when editing an existing trace template. To access this dialog box when exporting a template, on the [!INCLUDE[ssSqlProfiler](../../includes/sssqlprofiler-md.md)] **File** menu, point to **Templates**, and then click **Export Template**. To access this dialog box when changing the name of a template, on the **File** menu, point to **Templates**, point to **Edit Template**, and then click **Save As**.  

|Item|Description
|---|---
|**Server type**|Select the type of server from which you want to choose a template. This option is only available when you are exporting a template.  
|**Template name**|Type a new template name, or select a template name from the list. If you are exporting a template, you can only select a template name from the list. 

## See also 
[SQL Server Profiler](../../tools/sql-server-profiler/sql-server-profiler.md)   
[Server Performance and Activity Monitoring](../../relational-databases/performance/server-performance-and-activity-monitoring.md)  
  
  
