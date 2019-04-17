---
title: "Modify the Trace Results View | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 860a80dc-bac0-4ef0-bf7f-7a9b430d7aa3
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Modify the Trace Results View
  This topic describes how to modify the trace results view of an Extended Events session in [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] by performing the following tasks.  
  
1.  [Add or Remove Columns](#AddRemoveColumns)  
  
2.  [Create, Edit, or Delete Merged Columns](#ChangeColumns)  
  
3.  [Sort the Results](#SortResults)  
  
4.  [Group the Results](#GroupResults)  
  
5.  [Aggregate the Results](#AggregateResults)  
  
6.  [Filter the Results](#Filter)  
  
7.  [Search for Text in Columns](#Search)  
  
8.  [Change the Display Settings](#ChangeDisplay)  
  
##  <a name="AddRemoveColumns"></a> Add or Remove Columns  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, and then select **Watch Live Data**.  
  
2.  In the trace results window, right-click the column header, and then select **Choose Columns**.  
  
3.  In the **Choose Columns** dialog box, in the **Available columns** section, select the column names you want to add, and then click the right arrow.  
  
    > [!NOTE]  
    >  By default, the columns are arranged by name. To display the columns by event, click **Arrange by event**.  
  
     To remove columns, in the **Selected columns** section, select the columns you want to remove, and click the left arrow.  
  
4.  In the **Selected columns** section, to change the column order display, click **Move Up** or **Move Down** respectively. You cannot move multiple rows.  
  
5.  Click **OK**.  
  
##  <a name="ChangeColumns"></a> Create, Edit, or Delete Merged Columns  
  
#### To create merged columns  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, and then select **Watch Live Data**.  
  
2.  In the trace results window, right-click the column header, and then click **Choose Columns**.  
  
3.  In the **Choose Columns** dialog box, click **New**.  
  
4.  In the **New Merged Column** dialog box, in the **Merged column name** box, enter a name for the merged columns.  
  
5.  In the **Original columns to merge** box, select two or more columns to merge from the drop-down list.  
  
    > [!NOTE]  
    >  Extended Events only supports merging up to five columns.  
  
6.  Click **OK**.  
  
#### To edit merged columns  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, and then select **Watch Live Data**.  
  
2.  In the trace results window, right-click the column header, and then click **Choose Columns**.  
  
3.  In the **Choose Columns** dialog box, click **Edit**.  
  
4.  To change the name of the merged column, in the **New Merged Column** dialog box, in the **Merged column name** box, enter the new name.  
  
     To change the columns you want to merge, in the **Original columns to merge** box, select the columns you want to merge from the drop-down list, and then click **OK**.  
  
#### To delete merged columns  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, and then select **Watch Live Data**.  
  
2.  In the trace results window, right-click the column header, and then click **Choose Columns**.  
  
3.  In the **Choose Columns** dialog box, select the name of the merged column you want to delete, and then click **Delete**.  
  
##  <a name="SortResults"></a> Sort the Results  
  
#### To sort the results in ascending or descending order  
  
-   Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, select **Watch Live Data**, and then click the **Stop Data Feed** button on the toolbar.  
  
-   In the trace results window, right-click the column heading you want to sort. Click **Sort Ascending** or **Sort Descending** to sort the column in ascending or descending order respectively.  
  
     If you have grouped columns, sorting the column will only sort data within the group.  
  
##  <a name="GroupResults"></a> Group Results  
  
#### To group the results by a single column  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, select **Watch Live Data**, and then click the **Stop Data Feed** button on the Extended Events toolbar.  
  
2.  In the trace results window, right-click the column header you want to group, and then click **Group By This Column**.  
  
#### To group the results by multiple columns  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, select **Watch Live Data**, and then click the **Stop Data Feed** button on the toolbar.  
  
2.  Click the **Grouping** button on the Extended Events toolbar.  
  
3.  In the **Grouping** dialog box, in the **Available columns** box, select the columns you want to group, and then click the right arrow.  
  
     To change the grouping order, in the **Columns grouped on** section, click the up or down arrows.  
  
     To remove columns from the grouping, in the **Columns grouped on** box, select the columns you want to remove and then click the left arrow.  
  
4.  Click **OK**.  
  
##  <a name="AggregateResults"></a> Aggregate Results  
 Extended Events supports five aggregation functions:  
  
-   Sum  
  
-   Min  
  
-   Max  
  
-   Average  
  
-   Count  
  
 Sum, Min,  Max, and Average can only be used with available numeric columns. Count is the number of non-null values that exist for the selected column in the group.  
  
#### To aggregate results  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, select **Watch Live Data**, and then click the **Stop Data Feed** button on the toolbar.  
  
    > [!NOTE]  
    >  Aggregation is run against a group, so you must group the results before you can perform the aggregation.  
  
2.  On the Extended Events toolbar, click the **Aggregate** button.  
  
     The **Aggregation** dialog box appears displaying the columns available for aggregation.  
  
3.  Under **Aggregation Type**, select how you want to aggregate the corresponding column from the drop-down list.  
  
4.  In the **Sort aggregation by** box, select the column you want to sort by from the drop-down list.  
  
5.  Select the **In ascending order** option to sort the aggregation result in ascending order.  
  
6.  Select the **In descending order** option to sort the aggregation result in descending order.  
  
7.  Click **OK**.  
  
##  <a name="Filter"></a> Filter Results  
 You can apply filters to narrow down the trace results that are displayed in the trace window. The display filter includes a time filter and an advanced filter. You use the time filter to filter the trace results by event timestamp, and you use the advanced filter to construct filter conditions using the event fields and actions. There is an logical AND relationship between the Time and Advanced Filters.  
  
#### To create a filter  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, and then select **Watch Live Data**.  
  
2.  In the trace results window, select the results you want to filter, and then on the Extended Events toolbar, click the **Filters** button.  
  
3.  In the **Filters** dialog box, select **Set time filter** to set the time filter by dragging the slider bars to set the timeline. Note that when you move the slider bars, the time box displays the time value accordingly. You can also enter the time in the time boxes, or you select it from the drop-down list. Note that when you enter the time, the left time slider will move accordingly.  
  
4.  In the **Additional Filters** section, apply your filter criteria, and then click **Apply**. When your finished created the filter, click **OK**.  
  
 A special situation is when an event field has the same name as an action. An example of this would be session_id. There are several events that include a session_id field and you could also add the session_id action. Both pieces of information are collected, but the Extended Events profiler display grid uses the following logic.  
  
-   Only one copy of the column (session_id in this case) is shown in the grid display.  
  
-   If both fields and action exist in the data, the field value is shown.  
  
-   If only field or action is exists in the data, the field or action is displayed.  
  
-   If neither action nor field exists, display NULL.  
  
##  <a name="Search"></a> Search for Text in Columns  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, and then select **Watch Live Data**.  
  
2.  On the Extended Events toolbar, click the **Find** button.  
  
3.  In the **Find in Extended Events** dialog box, in the **Find what** box, enter the text you want to search for.  
  
     You can select one of your last 20 search strings from the drop-down list.  
  
4.  In the **Look in** box, select the location in which to search for the specified text from the drop-down list. Use the following options for searching:  
  
    -   **Table Columns**. Use this option to search all visible columns in the trace window.  
  
    -   **Details**. Use this option to search all columns (promoted and non-promoted) in the trace window that were selected before opening the **Find in Extended Events** dialog box.  
  
    -   **\<Event column name>**. Use this option to search in a specific event column from the drop-down list.  
  
5.  Use the following options to specify how you want to define the search:  
  
    1.  **Match case**. Use this option to display the search results for the text you entered in the **Find what** box that are matched both by content and by case.  
  
    2.  **Match whole word**. Use this option to display only the search results for the text you entered that match complete words.  
  
    3.  **Search up**. Use this option to search from your cursor location to the beginning of the results.  
  
    4.  **Use**. Use this option to interpret the special characters and the regular expressions you entered in the **Find what** box. Special characters include the wildcard characters (*) and (?) to represent one or more characters. Regular expressions are special notations used to define patterns of search text.  
  
6.  Click **Find Next** to search for the next text that you entered in the **Find what** box.  
  
##  <a name="ChangeDisplay"></a> Change the Display Settings  
 You can save column information (column order, merge column, and column width) and filter information of a trace result into an Extended Events display setting file (.viewsetting file). After saving the file, you can apply it to your trace results to change the view.  
  
#### To change the display settings  
  
1.  Open a .XEL file to view the trace results.  
  
    > [!NOTE]  
    >  You can also right click the session name, and then select **Watch Live Data**.  
  
2.  In the trace results window, on the Extended Events toolbar or menu, select **Display Settings**.  
  
3.  From the drop-down list, select one of the following options:  
  
    -   **Save as**. Save the columns and filter information of a trace result to a .viewsetting file.  
  
    -   **Open**. Open an existing .viewsetting file.  
  
    -   **Open recent**. Open a recently saved .viewsetting file.  
  
  
