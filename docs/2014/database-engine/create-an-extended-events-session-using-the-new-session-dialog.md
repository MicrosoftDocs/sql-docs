---
title: "Create an Extended Events Session Using the New Session Dialog | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.SSMS.XEDISPLAY.GROUPING.F1"
  - "SQL12.SSMS.XEDISPLAY.AGGREGATION.F1"
  - "SQL12.SSMS.XEDISPLAY.NEWMERGEDCOLUMN.F1"
  - "SQL12.SSMS.XENEWEVENTSESSION.GENERAL.F1"
  - "SQL12.SSMS.XEDISPLAY.EDITCOLUMNS.F1"
  - "SQL12.SSMS.XEDISPLAY.FILTERS.F1"
helpviewer_keywords: 
  - "Extended Events Dialog Box"
ms.assetid: 6b2244bc-df6a-4b0a-990e-ddd8d42f7907
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Create an Extended Events Session Using the New Session Dialog
  The New Session dialog lets you define an Extended Events session that captures, displays, and analyzes your data. The New Session dialog exposes all Extended Events functionality.  
  
 The [New Session Wizard](../ssms/object/object-explorer.md) also lets you define an Extended Events session, supporting most Extended Events functionality.  
  
## Before you Begin  
 To open the New Session dialog, in Object Explorer, expand the **Management** node, and then expand **Extended Events**. Right-click **Sessions**, and then click **New Session**.  
  
##  <a name="BeforeYouBegin"></a> Permissions  
 To create an Extended Events session, you must have the ALTER ANY EVENT SESSION permission.  
  
## To Create an Extended Events Session using the New Session Dialog  
 You use the **General** page to select a template and schedule an event session.  
  
#### To select a template and schedule an event session  
  
1.  In Object Explorer, expand the **Management** node, and then expand **Extended Events**. Right-click **Sessions**, and then click **New Session**.  
  
2.  On the **General** page, in the **Session name** box, type a meaningful name for the event session.  
  
3.  In the **Template** box, select the template you want to use from the drop-down list.  
  
     You can select from a set of pre-configured templates designed for common problems or you can select **From File** to use a template file exported from a previous session definition. Note that you can also change the configuration of the session after you apply the template.  
  
4.  In the **Schedule** section, if you want to start the session when you start the server, select the **Start the event session at server startup** check box.  
  
     If you want to start the session after you create the session, select the **Start the event session immediately after session creation** check box.  
  
5.  To view live data for your event session, click **Watch live data on screen as it is captured**. The live data will start displaying tracing immediately after the session is created.  
  
6.  In the **Causality tracking** section, select the **Track how events are related to one another** check box to track work across multiple tasks.  
  
     For more information about causality tracking, see "Session Content and Characteristics" in the [SQL Server Extended Events Sessions](../relational-databases/extended-events/sql-server-extended-events-sessions.md) topic.  
  
     To add events to your session, in the **Select a page** section, click **Events**.  
  
    > [!NOTE]  
    >  Do not click **OK** until you have finished creating the event session.  
  
 You use the **Events** page to find and add the events you want to capture for the session.  
  
#### To add events to a session  
  
1.  In the New Session dialog, in the **Select a page** section, select **Events**.  
  
2.  On the **Events** page, click **Select** (the **Select** button is dimmed if you are already on the **Select the events you want to capture** screen).  
  
     You can search for any word in the table by entering the text you want to search for in the **Event library** box. For example, if you want to find the **lock_acquired** event, you can enter **lock** or **lock acquire**.  
  
3.  In the **Event library** section, select how you want to search for the events you want to capture from the drop-down list. For example, you can search for event names or event names and their descriptions. Enter your search criteria in the **Search** box.  
  
    > [!NOTE]  
    >  Events from the **Debug** channel are hidden by default. To display debug events, select **Debug** from the **Channel** drop-down list.  
  
     Details for the selected events appear in the Details pane under the **Event library**.  
  
     The events are sorted by name in alphabetical order. You can sort by other event details by clicking the appropriate column heading. For example, you can sort by the **Category** or **Channel** column.  
  
4.  Select the event(s) you want to capture, and then click the right arrow to move the event(s) to the **Selected Events** section.  
  
    > [!NOTE]  
    >  You can select multiple events at the same time from the **Event library** by using the CTRL or SHIFT keys.  
  
     To configure the selected events, click **Configure**.  
  
    > [!NOTE]  
    >  Do not click **OK** until you have finished creating the event session.  
  
 You use the **Data Storage** page to add targets to an event session. Targets store event data, and can perform actions such as saving events to a file for later viewing and aggregating event data for the session. For more information about Extended Events targets, see [SQL Server Extended Events Targets](../../2014/database-engine/sql-server-extended-events-targets.md).  
  
 Following are the targets that you can use for an Extended Events session:  
  
-   **etw_classic_sync_target**. Use to correlate SQL Server events with Windows operating system or application event data.  
  
-   **event_counter**. Counts all specified events that occur after an Extended Event session is started. Use to obtain information about workload characteristics without adding the overhead of full event collection.  
  
-   **event_file**. Use to write event session output from complete memory buffers to disk.  
  
-   **histogram**. Use to count the number of times that a specified event occurs, based on a specified event column or action.  
  
-   **pair_matching**. Use to determine when a specified paired event does not occur in a matched set.  
  
-   **ring_buffer**. Use to hold the event data in memory on a first-in first-out (FIFO) basis, or on a per-event FIFO basis.  
  
#### To configure global fields for a session  
  
1.  On the **Events** page, after you select the event or events you want to add to the event session, click **Configure**.  
  
     After you click **Configure**, the **Event library** section collapses, and the **Selected events** section slides to the left of the page. The **Event configuration options** section that you use to configure the events appears on the right side of the page. You use the tabs on this page to configure actions for your event session.  
  
2.  On the **Global Fields** tab, select the fields you want to apply to the selected events.  
  
     You can select multiple fields for each event.  
  
    > [!NOTE]  
    >  If two events that are already selected have different configured actions, Extended Events will display the partially checked actions. To quickly find which actions you have enabled, you can sort by the enabled/disabled status by clicking the column heading above the check boxes.  
  
3.  On the  **Filters** tab, apply the filters (also called predicates) to limit the events that you want to capture.  
  
     If a filter is applied to the selected event, a check appears in the column.  
  
    > [!NOTE]  
    >  You can select multiple events that you want to apply a filter to by using the CTRL or SHIFT keys. However, only common event fields will appear for configuration. If two selected events already have different filters configured for them, the filter will not appear. Reconfiguring filters will overwrite existing filter values.  
  
    > [!NOTE]  
    >  When you configure a group clause for your filter, redundant parentheses are removed from the filter after the result is saved. For example, if you create a filter grouping **Clause 1** and **Clause 2**, parentheses will appear around the clauses. After you save the filter, the redundant parentheses are removed. The removal of the parentheses does not affect the filter logic.  
  
4.  On the **Event Fields** tab, you select the event fields you want to apply to a selected event.  
  
     Events contain a number of fields that are always collected, which are displayed on the **Event Fields** tab without check boxes. An event can also have optional fields that are not collected by default (for example, expensive optional fields are not selected). Optional fields are also listed on the **Event Fields** tab with check boxes. To collect an optional field, select the check box in front of the optional field.  
  
    > [!NOTE]  
    >  The event field options will display fields that will be captured and viewed in the trace results. (Extended Events only displays data types of the event fields. For example, read-only event fields are not displayed.) If you select two or more events, the New Session dialog will display a blank on the **Event Fields** tab.  
  
5.  To add targets to an event session, in the **Select a page** section, select **Data Storage**.  
  
    > [!NOTE]  
    >  Do not click **OK** until you have finished creating the event session.  
  
#### To add targets to an event session  
  
1.  In the New Session dialog, in the **Select a page** section, select **Data Storage**.  
  
2.  On the **Data Storage** page, select the target type from the drop-down list.  
  
     After you select the target type, the target description is displayed. You can add a target only once. If you have already added the target to the session, it will not appear in the drop-down list.  
  
3.  Click **Add** to add a target for the event session. If you want to remove a target, click **Remove**.  
  
     Target properties appear below the **Targets** section, depending on the target you select.  
  
4.  Following are the properties you can specify, subject to the targets you select:  
  
    |Target|Target Properties|  
    |------------|-----------------------|  
    |**etw_classic_sync_target**|**Session log file name on server**. Enter the log file name and directory on the server, or click **Browse** to find and select the log file.<br /><br /> **Maximum log file size**. Enter the maximum log file size for the Event Tracing for Windows (ETW) event. The default is 20 megabytes (MB). You can select a different unit of storage from the drop-down list.<br /><br /> **Buffer size**. Enter the in-memory buffer size for the event session. The default is 128 kilobytes (KB). You can select a different unit of storage from the drop-down list.<br /><br /> **Session name**. Enter a meaningful ETW session name.<br /><br /> **Retry on error writing to ETW**. Select this check box to retry publishing the event to the ETW subsystem.<br /><br /> **Maximum number of retries**. Enter the maximum number of times you want to retry publishing the event to the ETW subsystem before dropping the event. The default number of retries is zero (0). For this target property, zero (0) means no retries.|  
    |**event_counter**|There are no target properties for the event counter.|  
    |**event_file**|**File name on server**. Enter the directory and target file name on the server, or click **Browse** to find and select the target file.<br /><br /> **Maximum file size**. Specify the maximum file size for the file target. If you do not specify the maximum file size, the file will grow until the disk is full. The default file size is 1 gigabyte (GB). You can select a different unit of storage from the drop-down list.<br /><br /> **Enable file rollover**. Select this check box to enable file rollover for the file target.<br /><br /> **Maximum number of files**. Enter the maximum number of files you want to retain in the file system.|  
    |**histogram**|**Event to filter on**. Select the event you want to filter on from the drop-down list. You can filter on any event that exists in the event session. You can also select **\<None>** from the drop-down list to include all events and the base buckets on the action.<br /><br /> **Base buckets on: Action**. Select this option to base the buckets on the action name that is used as the data source, and then select the action from the drop-down list.<br /><br /> **Base buckets on: Field**. Select this option to base the buckets on the event field that is used as the data source, and then select the field from the drop-down list.<br /><br /> **Maximum number of buckets**. Enter the maximum number of buckets you want to retain. When this value is reached, the event session will ignore any new events that do not belong to the existing buckets.|  
    |**pair_matching**|**Events: Begin with**. Select the event name from the drop-down list that specifies the beginning event in a paired sequence.<br /><br /> **Events: End with**. Select the event name from the drop-down list that specifies the ending event in a paired sequence.<br /><br /> **Fields and actions: Begin with**. Select the beginning field and/or action in a paired sequence from the drop-down list.<br /><br /> **Fields and actions: End with**. Select the ending field and/or action in a paired sequence from the drop-down list.<br /><br /> **Discard new unpaired events if memory pressure occurs**. Select this check box to stop collecting events to the pair_matching target when the computer is under memory pressure. When there is no longer memory pressure, the collection of events will resume.<br /><br /> **Maximum orphan events**. Specify the maximum number of orphan events you want to keep in memory.|  
    |**ring_buffer**|**Number of events to keep**. Use the up and down arrows to specify the number of events you want to keep. The default is 1000.<br /><br /> **Maximum buffer memory size**. Enter the maximum amount of memory to use. Existing events are dropped when this value is reached. The default memory size is 0 megabytes (MB), which means unlimited. You can select a different unit of storage from the drop-down list.<br /><br /> **Keep a specified number of events (per type) when the buffer is full**. Select this option to keep a specified number of events of each type in the buffer.<br /><br /> **Number of events to keep (per type)**. Enter the preferred number of events of each type to keep in the buffer.|  
  
5.  If you want to set advanced configuration properties, select **Advanced** in the **Select a page** section.  
  
    > [!NOTE]  
    >  Do not click **OK** until you have finished creating the event session.  
  
#### To set advanced configurations  
  
1.  In the New Session dialog, in the **Select a page** section, select **Advanced**.  
  
2.  On the **Advanced** page, to specify the **Event retention mode** options for the event session, do the following:  
  
    1.  **Single event loss**. Select this option to allow loss of a single event.  
  
    2.  **Multiple event loss**. Select this option to allow loss of multiple events.  
  
    3.  **No event loss**. Select this option if you want to prevent event loss. We do not recommend selecting this option.  
  
        > [!NOTE]  
        >  Some events such as the **sqlos.wait_info** event are not compatible with the **No event loss** event retention mode.  
  
3.  To specify the **Maximum dispatch latency** options for the event session, do the following:  
  
    1.  **In seconds**. Select this option to prolong or shorten the maximum dispatch latency. Use the up and down arrows to specify the maximum dispatch latency in seconds.  
  
    2.  **Unlimited**. Select this option if you want the events dispatched only when the buffer is full.  
  
4.  In the **Maximum memory size** box, enter the maximum memory size. Existing events are dropped when this value is reached. You can select a different unit of storage from the drop-down list.  
  
5.  In the **Max event size** box, enter the maximum event size for events that are too large to be contained in the **Maximum memory size**. If you are not collecting very large events you do not need to configure this option. You can select a different unit of storage from the drop-down list.  
  
6.  To specify the **Memory partition mode** options for the event session, do the following:  
  
    1.  **None**. Select this option if you do not want a memory partition mode.  
  
    2.  **Per node**. Select this option if you want to partition memory per node.  
  
    3.  **Per CPU**. Select this option if you want to partition memory per CPU.  
  
 To restore the configuration default values for the preceding session properties, click **Restore Defaults**.  
  
## See Also  
 [Create an Extended Events Session Using Query Editor](../../2014/database-engine/create-an-extended-events-session-using-query-editor.md)   
 [Create an Extended Events Session Using the Wizard &#40;Object Explorer&#41;](../ssms/object/object-explorer.md)   
 [Script an Extended Event Session](../../2014/database-engine/script-an-extended-event-session.md)  
  
  
