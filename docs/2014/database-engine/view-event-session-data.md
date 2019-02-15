---
title: "View Event Session Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: ac742a01-2a95-42c7-b65e-ad565020dc49
author: mashamsft
ms.author: mathoma
manager: craigg
---
# View Event Session Data
  This topic describes using the display user interface to see and analyze extended event data:  
  
-   View Target Data  
  
-   Working With Data  
  
## View Target Data  
 You can display the data collected into the specified target within [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
### View Target Data  
 To view target data:  
  
1.  In Object Explorer, expand **Management**, **Extended Events**, **Sessions**, and then a session.  
  
2.  Right-click the target name, and then click **View Target Data** to display the target data.  
  
     The target data window appears in default view and displays the target data.  
  
 Notes on viewing target data:  
  
-   Target data is not available for the ETW target.  
  
-   To view the ring_buffer data in xml format, in the target data window click the **ring_buffer target data** link. The ring_buffer.xml file appears in the xml editor.  
  
-   For an event_file target, view the file target data (.XEL file) using one of the following methods:  
  
    -   Use File -> Open in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
    -   Drag and Drop the file into [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
    -   Double click the .XEL file.  
  
    -   In [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)], right click on a running Extended Events session and select View Target Data.  
  
    -   [fn_xe_file_target_read_file](/sql/relational-databases/system-functions/sys-fn-xe-file-target-read-file-transact-sql)  
  
    -   You can view more than one .XEL file by selecting **Merge Extended Event Files** from the File -> Open menu.  
  
### Watching Live Data  
 You can watch live data as it is being captured.  
  
-   In Object Explorer, expand the **Management**, **Extended Events**, and then **Sessions** nodes.  
  
-   Right-click the session name and then click **Watch Live Data** to start displaying the tracing data.  
  
     The default display columns are **Event Name** and **TimeStamp**.  
  
     To add additional columns to the trace window, click the **Choose Columns** button on the Extended Events toolbar. The **Details** tab shows all of the event details for the selected event.  
  
     Events are usually displayed in approximately 30 seconds. If you want to change the latency period, you can change the **Maximum dispatch latency** on the **Advanced** page of the of the **New Session** dialog.  
  
### To Refresh Target Data  
 Refreshing target data is not supported for event_files targets:  
  
1.  To refresh the target data automatically, right click the target data, select **Refresh Interval**, and then select the refresh interval from the interval list.  
  
2.  To pause and resume the automatic refresh, right-click the target data and then select **Pause** or **Resume**.  
  
3.  To refresh the target data manually, right click the target data, and then select **Refresh**.  
  
## Working With Data  
 You can use the analysis capabilities of the Extended Events user interface to identify problems.  
  
### Details Pane  
 The **Details** pane shows all the columns for the selected event, including fields and actions. You can add a column to the target data table by right-clicking a row in the **Details** pane and selecting **Show Column in Table**.  
  
### Create, Modify, or Delete Merged Columns  
 A merged column allows you to combine a set of fields to be displayed in a single column. The merged column will show the data from the first non-NULL field based on the order they are added to the field list. This is similar to what you see in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Profiler, where a specific column may show different data depending on the event (the most common example of this is the TextData field in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] Profiler). For an example, you could merge the statement and batch_text fields from the sql_statement_completed and sql_batch_completed events, respectively, into a field named myStatement. When you display the myStatement column in the table it will show the appropriate data for the associated event.  
  
 You can create, modify, or delete merged columns:  
  
1.  Open a .XEL file to view the trace results. (You can also right click the session name, and then select **Watch Live Data**.)  
  
2.  In the trace results window, right-click the column header, and then click **Choose Columns**.  
  
 To create a merged column, click **New** in the **Choose Columns** dialog box.  In the **New Merged Column** dialog, name the merged column and select the original columns to be included in the merged column.  
  
 To edit a merged column, select a merged column in the **Choose Columns** dialog and click **Edit**. In the **Edit Merged Column** dialog, rename the merged column or modify the original columns to be included in the merged column.  
  
 To delete a merged column, select a merged column in the **Choose Columns** dialog and click **Delete**.  
  
### Filter Results  
 You can view trace results, and then apply filters to narrow down the trace results that are displayed in the trace window. The display filter includes a time filter and an advanced filter. You use the time filter to filter the trace results by event timestamp, and you use the advanced filter to construct filter conditions using the event fields and actions. There is an "and" relationship between the time and advanced filters.  
  
 To create a filter:  
  
1.  Open a .XEL file to view the trace results. (You can also right click the session name, and then select **Watch Live Data**.)  
  
2.  In the trace results window, select the results you want to filter, and then on the **Extended Events** toolbar, click **Filters**.  
  
3.  In the **Filters** dialog box, select **Set time filter** to set the time filter by dragging the slider bars or modifying the time in the edit box.  
  
4.  In the **Additional filters** section, apply your filter criteria, and then click **Apply**.  
  
### Sort Results  
 To sort results either in ascending or descending order:  
  
1.  Open a .XEL file to view the trace results. (You can also right click the session name, select **Watch Live Data**, and then click the **Stop Data Feed** button on the toolbar.)  
  
2.  In the trace results window, right-click the column heading you want to sort and click **Sort Ascending** or **Sort Descending**.  
  
 You can also click the column header to reverse the sort order.  
  
 If you have grouped columns, sorting the column will only sort data within the group.  
  
### Group Results  
 Grouped results are equivalent to the functionality of the `GROUP BY` clause in [!INCLUDE[tsql](../includes/tsql-md.md)]. The target data table will show the data grouped together, allowing you to expand and collapse the data.  
  
 You must group data before you can aggregate it. For example, you can group on the query_hash value, sort descending by duration, get the average duration for each group, and then sort descending on the aggregation.  This will produce a list that shows the list of unique statements from longest to shortest average duration. When you expand the top group you will see the individual executions of that specific query sorted from longest to shortest.  
  
 You can group results by a single column or by multiple columns.  
  
 Open a .XEL file to view the trace results. (You can also right click the session name, select **Watch Live Data**, and then click the **Stop Data Feed** button on the toolbar.)  
  
 To group results by a single column, right-click the column heading in the trace results window, and click **Group by this Column**. To undo the grouping, select one of the rows and click **Remove All Groupings**.  
  
 To group results by a multiple columns, click the **Grouping** button on the **Extended Events** toolbar. In the **Available columns** box of the **Grouping** dialog, select the columns you want to group, and move them into the **Columns grouped on** box. To change the order in the **Columns grouped on** box, click the up or down arrows.  
  
### Aggregate Results  
 You can view the trace results, and then further analyze your event data by aggregating columns in your results. Extended Events supports five aggregation functions:  
  
-   sum  
  
-   min  
  
-   max  
  
-   average  
  
-   count  
  
 Sum, min, max, and average can only be used with numeric columns. Count is the number of non-null values that exist for the selected column in the group.  
  
 Aggregation is performed on a group, so you must group the results before you can perform the aggregation. To aggregate results:  
  
1.  Open a .XEL file to view the trace results. (You can also right click the session name, select **Watch Live Data**, and then click the **Stop Data Feed** button on the toolbar.)  
  
2.  On the **Extended Events** toolbar, click the **Aggregation** button. The Aggregation dialog box will display the columns available for aggregation.  
  
3.  In the **Aggregation Type** column, select the aggregation type.  
  
4.  In the **Sort aggregation by** box, select the sort column. Then select ascending or descending order.  
  
### Search for Text in Columns  
 You can search for text in columns:  
  
1.  Open a .XEL file to view the trace results. (You can also right click the session name, select **Watch Live Data**.  
  
2.  Click **Find** on the **Extended Events** toolbar.  
  
3.  In the **Find what** box of the **Find in Extended Events** dialog box, enter the search text. You can select one of your last 20 search strings from the drop-down list.  
  
4.  In the **Look in** box, select the location to search for the specified text. Use the following options for searching:  
  
    -   Table Columns. Use this option to search all visible columns in the trace window.  
  
    -   Details. Use this option to search all columns (promoted and non-promoted) in the trace window that were selected before opening the **Find in Extended Events** dialog box.  
  
    -   *Event_column_name*. Use this option to search in a specific event column from the drop-down list.  
  
5.  Use the following options to specify how you want to define the search:  
  
    -   Match case. Use this option to display the search results for the text you entered in the Find what box that are matched both by content and by case.  
  
    -   Match whole word. Use this option to display only the search results for the text you entered that match complete words.  
  
    -   Search up. Use this option to search from your cursor location to the beginning of the results.  
  
    -   Use. Use this option to interpret the special characters and the regular expressions you entered in the Find what box. Special characters include the wildcard characters (*) and (?) to represent one or more characters. Regular expressions are special notations used to define patterns of search text.  
  
    -   Click **Find Next** to search for the next text that you entered in the **Find what** box.  
  
### Bookmarks  
 To make it easier to return to a row, you can bookmark one or more row(s) in the target data. Right click on a row to change the bookmark. Use the previous and next buttons on the **Extended Events** toolbar to navigate to bookmarked rows.  
  
### Change Display Settings  
 You can save column information (column order, merge column, and column width) and filter information of a trace result into an Extended Events display setting file (.viewsetting file). After saving the file, you can apply it to your trace results to change the view.  
  
 To change the display settings:  
  
1.  Open a .XEL file to view the trace results. (You can also right click the session name, select **Watch Live Data**.  
  
2.  On the **Extended Events** toolbar, select **Display Settings**. From the drop-down list, select one of the following options:  
  
    -   Save as. Save the columns and filter information of a trace result to a .viewsetting file.  
  
    -   Open. Open an existing .viewsetting file.  
  
    -   Open recent. Open a recently saved .viewsetting file.  
  
### Copy or Export Trace Results  
 You can copy cells, rows, and details to selected rows from your trace results. You can also export your trace results to the following:  
  
-   .XEL file  
  
-   table  
  
-   .CSV file  
  
 To copy trace results, select a cell, row, or rows, right click, select **Copy** and then **Cell**, **Row**, or **Details**. Extended Events supports copying up to a maximum of 1000 rows.  
  
 You can export trace results to a .XEL file, table, or .CSV file by selecting **Export to** from the **Extended Events** menu option in [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)].  
  
### View a Deadlock Graph and Query Plans  
 You can view the deadlock graph for **xml_deadlock_report** in the Details pane to help you troubleshoot deadlocks. You can also view query plan graphs for the following events:  
  
-   query_post_compilation_showplan  
  
-   query_pre_execution_showplan  
  
-   query_post_execution_showplan  
  
 To view the deadlock graph:  
  
-   In Object Explorer, expand the **Management**, **Extended Events**, and then **Sessions** nodes.  
  
-   Right-click the session that contains the configured deadlock event that you want to view, and select **Watch Live Data**.  
  
-   Select the deadlock event and view the graph on the **Deadlock** tab in the Details pane.  
  
 To view query plan graphs:  
  
1.  In Object Explorer, expand the **Management**, **Extended Events**, and then **Sessions** nodes.  
  
2.  Right-click the session that contains the query plan graph that you want to view (for example, query_post_compilation_showplan), and then select **Watch Live Data**.  
  
3.  Select the query plan graph event (for example, query_post_compilation_showplan) and view the graph on the **Query plan** tab in the Details pane.  
  
  
