---
title: "Add Interactive Sort to a Table or Matrix (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
f1_keywords: 
  - "10121"
  - "sql12.rtp.rptdesigner.textboxproperties.intrctvsort.f1"
ms.assetid: 05819637-729b-4cf6-82de-91a99f184ec6
author: markingmyname
ms.author: maghan
manager: kfile
---
# Add Interactive Sort to a Table or Matrix (Report Builder and SSRS)
  Add interactive sort buttons to enable users to change the sort order of rows and columns in tables and matrices. This feature is supported only in rendering formats that support user interaction, such as HTML.  
  
 When you create an interactive sort button, you must specify what to sort, what to sort by, and the scope to which to apply the sort. For example, you can sort detail rows by customer last name, subcategory group values within a category group by sales, or category and subcategory group values combined by totals.  
  
 When you view the report, columns that support interactive sorting have arrow icons that change to indicate the sort order. The first time you click an interactive sort button, items are sorted in ascending order. Subsequent clicks toggle the sort order between ascending and descending order.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
##  <a name="BackToTop"></a> In this Article  
 [Sorting Detail Rows for a Table with No Groups](#SortingDetailRows)  
  
 [Sorting a Top Level Parent Row Group for a Table or Matrix](#SortingTopLevelParent)  
  
 [Sorting Child Groups or Detail Rows for a Group](#SortingChildGroups)  
  
 [Sorting Rows Based on a Complex Group Expression](#SortingMultipleRowGroups)  
  
 [Synchronizing Sort Order for Multiple Data Regions](#SynchronizingSortOrder)  
  
##  <a name="SortingDetailRows"></a> Sorting Detail Rows for a Table with No Groups  
 Add an interactive sort button to a column header to enable a user to click the column header and sort the details rows in a table by the value displayed in that column.  
  
#### To add an interactive sort button to a column header to sort the table by value  
  
1.  In report design view, in a table with no groups, right-click the text box in the column header to which you want to add an interactive sort button, and then click **Text Box Properties**.  
  
2.  Click **Interactive Sorting**.  
  
3.  Select **Enable interactive sorting on this text box**.  
  
4.  In **Choose what to sort**, click **Detail rows**.  
  
5.  In **Sort by**, specify a sort expression. From the drop-down list, select the field that corresponds to the column for which you are defining a sort action (for example, for a column heading named "Title", choose `[Title]`). Specifying a sort expression is required.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
7.  Repeat steps 1-6 for every column to which you want to add an interactive sort button.  
  
 To verify the sort action, click **Run** to preview the report, and then click the interactive sort buttons.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#BackToTop)  
  
##  <a name="SortingTopLevelParent"></a> Sorting a Top-Level Parent Row Group for a Table or Matrix  
 Add an interactive sort button to a column header to enable a user to click the column header and sort the parent group rows in a table or matrix by the value displayed in that column. The order of child groups remains unchanged.  
  
#### To add an interactive sort button to a column header to sort groups  
  
1.  In a table or matrix in report design view, right-click the text box in the column header for the group to which you want to add an interactive sort button, and then click **Text Box Properties**.  
  
2.  Click **Interactive Sorting**.  
  
3.  Select **Enable interactive sorting on this text box**.  
  
4.  In **Choose what to sort**, click **Groups**.  
  
5.  From the drop-down list, select the name of the group that you are sorting. For groups based on simple group expressions, the **Sort by** value is populated with group expression.  
  
    > [!NOTE]  
    >  For complex group expressions, manually set the **Sort by** expression to the same value as the group expression.  
  
6.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
 To verify the sort action, click **Run** to preview the report, and then click the interactive sort buttons.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#BackToTop)  
  
##  <a name="SortingChildGroups"></a> Sorting Child Groups or Detail Rows for a Group  
 Add an interactive sort button to a group header row to enable the user to sort the values of a child group from a parent group or to sort the detail rows for the innermost child group.  
  
#### To add an interactive sort button to a text box in a group row header to sort child groups or detail rows  
  
1.  In report design view, right-click the text box in the group header row to which you want to add an interactive sort button, and then click **Text Box Properties**.  
  
2.  Click **Interactive Sorting**.  
  
3.  Select **Enable interactive sorting on this text box**.  
  
4.  In **Choose what to sort**, click one of the following options:  
  
    -   **Details** Click **Details** to sort the detail rows. From the drop-down list, select the field to sort by. For this option, you must specify the value to sort by.  
  
    -   **Groups** Click **Groups** to sort the child group values. For this option, the **Sort by** expression is automatically filled in from the group expression.  
  
5.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
 To verify the sort action, click **Run** to preview the report, and then click the interactive sort buttons.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#BackToTop)  
  
##  <a name="SortingMultipleRowGroups"></a> Sorting Rows Based on a Complex Group Expression  
 Add an interactive sort button to a column header to enable a user to click the column header and sort the combined parent and child groups. To achieve this affect, you must change the group expression to be a composite of both groups. For example, suppose a matrix displays inventory totals for a store for items grouped by both color and size. To sort the rows based on the combination of color and size, instead of having a separate group for color and a separate group for size, you can define a group based on the combination of color and size. For more information about defining group expressions, see [Group Expression Examples &#40;Report Builder and SSRS&#41;](expression-examples-report-builder-and-ssrs.md).  
  
 In the following procedure, terms specify tablix data region areas. For more information, see [Tablix Data Region Areas &#40;Report Builder and SSRS&#41;](tablix-data-region-areas-report-builder-and-ssrs.md).  
  
 Typically, when you sort rows based on multiple groups, you want to see totals for the sorted rows, regardless of column groups. In this procedure, no column groups are used. You start by adding a matrix and removing the default column group. Alternatively, you could start by adding a table and removing the details group.  
  
#### To add an interactive sort button to a column header to sort multiple groups  
  
1.  In report design view, add a matrix.  
  
2.  Drag a numeric field to the data cell to link the dataset to the matrix.  
  
     Next, you will create a group with a group expression that specifies multiple fields, and a group header to use to display the group values.  
  
3.  Verify that the matrix is selected on the design surface. The Grouping pane displays a default row and column group.  
  
4.  In the Row Groups pane, right-click the default row group, and then click **Edit Group**. The **Group Properties** dialog box opens.  
  
5.  In **Name**, replace the default name with a name that specifies the multiple groups that you want to group by.  
  
6.  In **Group expressions**, in **Group on**, click the Expression (**fx**) button to open the **Expression** dialog box.  
  
7.  Type the expression that specifies all fields that you want to group by. For example, the following group expression combines a field named Color and a field named Size: `=Fields!Color.Value & Fields!Size.Value`.  
  
8.  [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
     You have now defined the group. Next, drag the fields to display to the tablix body area of the matrix. Add the fields that you chose to group by in step 7 to the tablix body area, each in its own column.  
  
     For this scenario, the first column in the tablix row groups area is not needed. To delete the column, right-click the column header, and then click **Delete Columns**. A dialog box asks whether to delete the associated groups. Click **No**. The row group area is deleted and only the tablix body area remains.  
  
     Next, you will remove the default column group.  
  
9. In the Column Groups pane, right-click the default column group, and then click **Delete Group**. A dialog box asks whether to delete the group and related rows and columns or the group only. Click **Delete group only**. The column group is deleted, and the column group area is deleted. Only the tablix body area remains.  
  
     Next, you will add an interactive sort button to the text box that spans the matrix.  
  
10. Click in the text box in the first row and then click **Text Box Properties**.  
  
11. Click **Interactive Sorting**.  
  
12. Select **Enable interactive sorting on this text box**.  
  
13. In **Choose what to sort**, click **Groups**.  
  
14. From the drop-down list, select the name of the group you created in step 5. The group expression is automatically copied to the **Sort by** text box.  
  
15. [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
     You have added the sort button to the text box.  
  
16. (Optional) You can suppress duplicate values in the columns that display group values. On the report design surface, click the text box that displays the value for which you want to hide repeating values. In the Properties pane, scroll to **HideDuplicates**, and from the drop-down list, select the name of the dataset that is linked to this matrix.  
  
 To verify the sort action, click **Run** to preview the report, and then click the interactive sort button. The matrix sorts by the combined values of the group expression, although each individual value displays in its own column.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#BackToTop)  
  
##  <a name="SynchronizingSortOrder"></a> Synchronizing Sort Order for Multiple Data Regions  
 Add an interactive sort button that enables a user to click one sort button and sort multiple data regions. When you create an interactive sort button, you can specify whether to synchronize the sort for multiple data regions based on the same report dataset. For example, a report might include a matrix and a chart that graphically displays the data. When a user changes the sort order of the rows in the matrix, the chart automatically displays the same sort order.  
  
 To synchronize the sort order, you must use identical sort expressions for the data regions or groups to sort, and define the scope for the sort to be a mutual ancestor of both data regions. The mutual ancestor can be either the dataset to which both data regions are linked or a containing data region within which both data regions appear. For example, assume a report has both a matrix and a chart that display data from the same dataset and that are contained in a list. To synchronize the sort action, you must specify the interactive sort on a column in the matrix and set the scope to the list. When the user sorts the matrix, the chart is also sorted.  
  
#### To synchronize sort order with a chart for an interactive sort button on a matrix data region  
  
1.  In report design view, add a matrix to the report.  
  
2.  Add a numeric dataset field to the matrix data cell, for example, a field representing quantity or sales.  
  
3.  Define a row group. By default, the sort order for the group is set to the same expression as the group expression.  
  
4.  Add a chart to the report, for example, a pie chart.  
  
5.  Drag the field you chose in step 2 to the **Value** area of the **Chart Data** pane.  
  
6.  Drag the field you chose to group by to the **Category Groups** area.  
  
     The group expression for the matrix row group and the chart category group must be identical.  
  
7.  Right-click the category group, and then click **Category Group Properties**.  
  
8.  Click **Sorting**.  
  
9. Click **Add**. A new sort row is added to the sorting options grid.  
  
10. In Sort by, from the drop-down list, choose the same field that you chose in step 6 to group by.  
  
11. [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
12. In the matrix, right-click the text box in the column header to which you want to add an interactive sort button, and then click **Text Box Properties**.  
  
13. Click **Interactive Sorting**.  
  
14. Select **Enable interactive sorting on this text box**.  
  
15. In **Choose what to sort**, click **Groups**.  
  
16. From the drop-down list under **Groups**, select the name of the group that you are sorting. The group expression for this group is automatically set for the **Sort by** value.  
  
17. Select **Also apply this sort to other groups and data regions within**. In the text box, type the name of the dataset, for example, "SalesData".  
  
18. [!INCLUDE[clickOK](../../../includes/clickok-md.md)]  
  
 To verify the sort action, click **Run** to preview the report, and then click the interactive sort button. The matrix sorts by the combined values of the group expression, although each individual value displays in its own column.  
  
 ![Arrow icon used with Back to Top link](../../2014-toc/media/uparrow16x16.gif "Arrow icon used with Back to Top link") [Back to Top](#BackToTop)  
  
## See Also  
 [Filter, Group, and Sort Data &#40;Report Builder and SSRS&#41;](filter-group-and-sort-data-report-builder-and-ssrs.md)   
 [Interactive Sort &#40;Report Builder and SSRS&#41;](interactive-sort-report-builder-and-ssrs.md)   
 [Sort Data in a Data Region &#40;Report Builder and SSRS&#41;](sort-data-in-a-data-region-report-builder-and-ssrs.md)   
 [Exploring the Flexibility of a Tablix Data Region &#40;Report Builder and SSRS&#41;](exploring-the-flexibility-of-a-tablix-data-region-report-builder-and-ssrs.md)  
  
  
