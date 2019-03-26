---
title: "Modifying the Date Dimension | Microsoft Docs"
ms.date: 05/08/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: multidimensional-models
ms.topic: tutorial
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# Lesson 3-4 - Modifying the Date Dimension
[!INCLUDE[ssas-appliesto-sqlas](../includes/ssas-appliesto-sqlas.md)]

In the tasks in this topic, you create a user-defined hierarchy and change the member names that are displayed for the Date, Month, Calendar Quarter, and Calendar Semester attributes. You also define composite keys for attributes, control the sort order of dimension members, and define attribute relationships.  
  
## Adding a Named Calculation  
You can add a named calculation, which is a SQL expression that is represented as a calculated column, to a table in a data source view. The expression appears and behaves as a column in the table. Named calculations enable you to extend the relational schema of existing tables in a data source view without modifying the table in the underlying data source. For more information, see [Define Named Calculations in a Data Source View &#40;Analysis Services&#41;](../analysis-services/multidimensional-models/define-named-calculations-in-a-data-source-view-analysis-services.md)  
  
#### To add a named calculation  
  
1.  To open the **Adventure Works DW 2012** data source view, double-click it in the **Data Source Views** folder in Solution Explorer.  
  
2.  Near the bottom of the **Tables** pane, right-click **Date**, and then click **New Named Calculation**.  
  
3.  In the **Create Named Calculation** dialog box, type **SimpleDate** in the **Column name** box, and then type or copy and paste the following **DATENAME** statement in the **Expression** box:  
  
    ```  
    DATENAME(mm, FullDateAlternateKey) + ' ' +  
    DATENAME(dd, FullDateAlternateKey) + ', ' +  
    DATENAME(yy, FullDateAlternateKey)  
    ```  
  
    The **DATENAME** statement extracts the year, month, and day values from the FullDateAlternateKey column. You will use this new column as the displayed name for the FullDateAlternateKey attribute.  
  
4.  Click **OK**, and then expand **Date** in the **Tables** pane.  
  
    The **SimpleDate** named calculation appears in the list of columns in the Date table, with an icon that indicates that it is a named calculation.  
  
5.  On the **File** menu, click **Save All**.  
  
6.  In the **Tables** pane, right-click **Date**, and then click **Explore Data**.  
  
7.  Scroll to the right to review the last column in the **Explore Date Table** view.  
  
    Notice that the **SimpleDate** column appears in the data source view, correctly concatenating data from several columns from the underlying data source, without modifying the original data source.  
  
8.  Close the **Explore Date Table** view.  
  
## Using the Named Calculation for Member Names  
After you create a named calculation in the data source view, you can use the named calculation as a property of an attribute.  
  
#### To use the named calculation for member names  
  
1.  Open **Dimension Designer** for the Date dimension in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)]. To do this, double-click the **Date** dimension in the **Dimensions** node of **Solution Explorer**.  
  
2.  In the **Attributes** pane of the **Dimension Structure** tab, click the **Date Key** attribute.  
  
3.  If the Properties window is not open, open the Properties window, and then click the **Auto Hide** button on the title bar so that it stays open.  
  
4.  Click the **NameColumn** property field near the bottom of the window, and then click the ellipsis browse (**...**) button to open the **Name Column** dialog box.  
  
5.  Select **SimpleDate** at the bottom of the **Source column** list, and then click **OK**.  
  
6.  On the **File** menu, click **Save All**.  
  
## Creating a Hierarchy  
You can create a new hierarchy by dragging an attribute from the **Attributes** pane to the **Hierarchies** pane.  
  
#### To create a hierarchy  
  
1.  In **Dimension Structure** tab of the Dimension Designer for the **Date** dimension, drag the **Calendar Year** attribute from the **Attributes** pane into the **Hierarchies** pane.  
  
2.  Drag the **Calendar Semester** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **Calendar Year** level.  
  
3.  Drag the **Calendar Quarter** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **Calendar Semester** level.  
  
4.  Drag the **English Month Name** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **Calendar Quarter** level.  
  
5.  Drag the **Date Key** attribute from the **Attributes** pane into the **<new level>** cell in the **Hierarchies** pane, underneath the **English Month Name** level.  
  
6.  In the **Hierarchies** pane, right-click the title bar of the **Hierarchy** hierarchy, click **Rename**, and then type **Calendar Date**.  
  
7.  By using the right-click context menu, in the **Calendar Date** hierarchy, rename the **English Month Name** level to **Calendar Month**, and then rename the **Date Key** level to **Date**.  
  
8.  Delete the **Full Date Alternate Key** attribute from the **Attributes** pane because you will not be using it. Click **OK** in the **Delete Objects** confirmation window.  
  
9. On the **File** menu, click **Save All**.  
  
## Defining Attribute Relationships  
If the underlying data supports it, you should define attribute relationships between attributes. Defining attribute relationships speeds up dimension, partition, and query processing.  
  
#### To define attribute relationships  
  
1.  In the **Dimension Designer** for the **Date** dimension, click the **Attribute Relationships** tab.  
  
2.  In the diagram, right-click the **English Month Name** attribute, and then click **New Attribute Relationship**.  
  
3.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **English Month Name**. Set the **Related Attribute** to **Calendar Quarter**.  
  
4.  In the **Relationship type** list, set the relationship type to **Rigid**.  
  
    The relationship type is **Rigid** because relationships between the members will not change over time.  
  
5.  Click **OK**.  
  
6.  In the diagram, right-click the **Calendar Quarter** attribute, and then click **New Attribute Relationship**.  
  
7.  In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Calendar Quarter**. Set the **Related Attribute** to **Calendar Semester**.  
  
8.  In the **Relationship type** list, set the relationship type to **Rigid**.  
  
9. Click **OK**.  
  
10. In the diagram, right-click the **Calendar Semester** attribute, and then click **New Attribute Relationship**.  
  
11. In the **Create Attribute Relationship** dialog box, the **Source Attribute** is **Calendar Semester**. Set the **Related Attribute** to **Calendar Year**.  
  
12. In the **Relationship type** list, set the relationship type to **Rigid**.  
  
13. Click **OK**.  
  
14. On the **File** menu, click **Save All**.  
  
## Providing Unique Dimension Member Names  
In this task, you will create user-friendly name columns that will be used by the **EnglishMonthName**, **CalendarQuarter**, and **CalendarSemester** attributes.  
  
#### To provide unique dimension member names  
  
1.  To switch to the **[!INCLUDE[ssSampleDBCoShort](../includes/sssampledbcoshort-md.md)] DW 2012** data source view, double-click it in the **Data Source Views** folder in Solution Explorer.  
  
2.  In the **Tables** pane, right-click **Date**, and then click **New Named Calculation**.  
  
3.  In the **Create Named Calculation** dialog box, type **MonthName** in the **Column name** box, and then type or copy and paste the following statement in the **Expression** box:  
  
    ```  
    EnglishMonthName+' '+ CONVERT(CHAR (4), CalendarYear)  
    ```  
  
    The statement concatenates the month and year for each month in the table into a new column.  
  
4.  Click **OK**.  
  
5.  In the **Tables** pane, right-click **Date**, and then click **New Named Calculation**.  
  
6.  In the **Create Named Calculation** dialog box, type **CalendarQuarterDesc** in the **Column name** box, and then type or copy and paste the following SQL script in the **Expression** box:  
  
    ```  
    'Q' + CONVERT(CHAR (1), CalendarQuarter) +' '+ 'CY ' +  
    CONVERT(CHAR (4), CalendarYear)  
    ```  
  
    This SQL script concatenates the calendar quarter and year for each quarter in the table into a new column.  
  
7.  Click **OK**.  
  
8.  In the **Tables** pane, right-click **Date**, and then click **New Named Calculation**.  
  
9. In the **Create Named Calculation** dialog box, type **CalendarSemesterDesc** in the **Column name** box, and then type or copy and paste the following SQL script in the **Expression** box:  
  
    ```  
    CASE  
    WHEN CalendarSemester = 1 THEN 'H1' + ' ' + 'CY' + ' '   
           + CONVERT(CHAR(4), CalendarYear)  
    ELSE  
    'H2' + ' ' + 'CY' + ' ' + CONVERT(CHAR(4), CalendarYear)  
    END  
    ```  
  
    This SQL script concatenates the calendar semester and year for each semester in the table into a new column.  
  
10. Click **OK.**  
  
11. On the **File** menu, click **Save All**.  
  
## Defining Composite KeyColumns and Setting the Name Column  
The **KeyColumns** property contains the column or columns that represent the key for the attribute. In this task, you will define composite **KeyColumns**.  
  
#### To define composite KeyColumns for the English Month Name attribute  
  
1.  Open the **Dimension Structure** tab for the Date dimension.  
  
2.  In the **Attributes** pane, click the **English Month Name** attribute.  
  
3.  In the **Properties** window, click the **KeyColumns** field, and then click the browse (**...**) button.  
  
4.  In the **Key Columns** dialog box, in the **Available Columns** list, select the column **CalendarYear**, and then click the **>** button.  
  
5.  The **EnglishMonthName** and **CalendarYear** columns are now displayed in the **Key Columns** list.  
  
6.  Click **OK**.  
  
7.  To set the **NameColumn** property of the **EnglishMonthName** attribute, click the **NameColumn** field in the Properties window, and then click the browse (**...**) button.  
  
8.  In the **Name Column** dialog box, in the **Source Column** list, select **MonthName**, and then click **OK**.  
  
9. On the **File** menu, click **Save All**.  
  
#### To define composite KeyColumns for the Calendar Quarter attribute  
  
1.  In the **Attributes** pane, click the **Calendar Quarter** attribute.  
  
2.  In the **Properties** window, click the **KeyColumns** field, and then click the browse (**...**) button.  
  
3.  In the **Key Columns** dialog box, in the **Available Columns** list, select the column **CalendarYear**, and then click the **>** button.  
  
    The **CalendarQuarter** and **CalendarYear** columns are now displayed in the **Key Columns** list.  
  
4.  Click **OK**.  
  
5.  To set the **NameColumn** property of the **Calendar Quarter** attribute, click the **NameColumn** field in the Properties window, and then click the browse (**...**) button.  
  
6.  In the **Name Column** dialog box, in the **Source Column** list, select **CalendarQuarterDesc**, and then click **OK**.  
  
7.  On the **File** menu, click **Save All**.  
  
#### To define composite KeyColumns for the Calendar Semester attribute  
  
1.  In the **Attributes** pane, click the **Calendar Semester** attribute.  
  
2.  In the **Properties** window, click the **KeyColumns** field, and then click the browse (**...**) button.  
  
3.  In the **Key Columns** dialog box, in the **Available Columns** list, select the column, **CalendarYear**, and then click the **>** button.  
  
    The **CalendarSemester** and **CalendarYear** columns are now displayed in the **Key Columns** list.  
  
4.  Click **OK**.  
  
5.  To set the **NameColumn** property of the **Calendar Semester** attribute, click the **NameColumn** field in the property window, and then click the browse (**...**) button.  
  
6.  In the **Name Column** dialog box, in the **Source Column** list, select **CalendarSemesterDesc**, and then click **OK**.  
  
7.  On the **File** menu, click **Save All**.  
  
## Deploying and Viewing the Changes  
After you have changed attributes and hierarchies, you must deploy the changes and reprocess the related objects before you can view the changes.  
  
#### To deploy and view the changes  
  
1.  On the **Build** menu of [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click **Deploy Analysis Services Tutorial**.  
  
2.  After you have received the **Deployment Completed Successfully** message, click the **Browser** tab of **Dimension Designer** for the **Date** dimension, and then click the Reconnect button on the toolbar of the designer.  
  
3.  Select **Calendar Quarter** from the **Hierarchy** list. Review the members in the **Calendar Quarter** attribute hierarchy.  
  
    Notice that the names of the members of the **Calendar Quarter** attribute hierarchy are clearer and easier to use because you created a named calculation to use as the name. Members now exist in the **Calendar Quarter** attribute hierarchy for each quarter in each year. The members are not sorted in chronological order. Instead they are sorted by quarter and then by year. In the next task in this topic, you will modify this behavior to sort the members of this attribute hierarchy by year and then by quarter.  
  
4.  Review the members of the **English Month Name** and **Calendar Semester** attribute hierarchies.  
  
    Notice that the members of these hierarchies are also not sorted in chronological order. Instead, they are sorted by month or semester, respectively, and then by year. In the next task in this topic, you will modify this behavior to change this sort order.  
  
## Changing the Sort Order by Modifying Composite Key Member Order  
In this task, you will change the sort order by changing the order of the keys that make up the composite key.  
  
#### To modify the composite key member order  
  
1.  Open the **Dimension Structure** tab of Dimension Designer for the **Date** dimension, and then select **Calendar Semester** in the **Attributes** pane.  
  
2.  In the Properties window, review the value for the **OrderBy** property. It is set to **Key**.  
  
    The members of the **Calendar Semester** attribute hierarchy are sorted by their key value. With a composite key, the ordering of the member keys is based first on the value of the first member key, and then on the value of the second member key. In other words, the members of the **Calendar Semester** attribute hierarchy are sorted first by semester and then by year.  
  
3.  In the Properties window, click the ellipsis browse button (**...**) to change the **KeyColumns** property value.  
  
4.  In the **Key Columns** list of the **Key Columns** dialog box, verify that **CalendarSemester** is selected, and then click the down arrow to reverse the order of the members of this composite key. Click **OK**.  
  
    The members of the attribute hierarchy are now sorted first by year and then by semester.  
  
5.  Select **Calendar Quarter** in the **Attributes** pane, and then click the ellipsis browse button (**...**) for the **KeyColumns** property in the Properties window.  
  
6.  In the **Key Columns** list of the **Key Columns** dialog box, verify that **CalendarQuarter** is selected, and then click the down arrow to reverse the order of the members of this composite key. Click **OK**.  
  
    The members of the attribute hierarchy are now sorted first by year and then by quarter.  
  
7.  Select **English Month Name** in the **Attributes** pane, and then click the ellipsis button (**...**) for the **KeyColumns** property in the Properties window.  
  
8.  In the **Key Columns** list of the **Key Columns** dialog box, verify that **EnglishMonthName** is selected, and then click the down arrow to reverse the order of the members of this composite key. Click **OK**.  
  
    The members of the attribute hierarchy are now sorted first by year and then by month.  
  
9. On the **Build** menu of [!INCLUDE[ssBIDevStudio](../includes/ssbidevstudio-md.md)], click **Deploy Analysis Services Tutorial**. When deployment has successfully completed, click the **Browser** tab in Dimension Designer for the **Date** dimension.  
  
10. On the toolbar of the **Browser** tab, click the Reconnect button.  
  
11. Review the members of the **Calendar Quarter** and **Calendar Semester** attribute hierarchies.  
  
    Notice that the members of these hierarchies are now sorted in chronological order, by year and then by quarter or semester, respectively.  
  
12. Review the members of the **English Month Name** attribute hierarchy.  
  
    Notice that the members of the hierarchy are now sorted first by year and then alphabetically by month. This is because the data type for the EnglishCalendarMonth column in the data source view is a string column, based on the nvarchar data type in the underlying relational database. For information about how to enable the months to be sorted chronologically within each year, see [Sorting Attribute Members Based on a Secondary Attribute](../analysis-services/lesson-4-5-sorting-attribute-members-based-on-a-secondary-attribute.md).  
  
## Next Task in Lesson  
[Browsing the Deployed Cube](../analysis-services/lesson-3-5-browsing-the-deployed-cube.md)  
  
## See Also  
[Dimensions in Multidimensional Models](../analysis-services/multidimensional-models/dimensions-in-multidimensional-models.md)  
  
