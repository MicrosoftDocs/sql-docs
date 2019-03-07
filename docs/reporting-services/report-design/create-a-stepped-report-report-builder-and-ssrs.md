---
title: "Create a Stepped Report (Report Builder and SSRS) | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-design


ms.topic: conceptual
ms.assetid: 5933c4f0-c713-4ecb-b521-ff46c9c63fff
author: markingmyname
ms.author: maghan
---
# Create a Stepped Report (Report Builder and SSRS)
A stepped report is a type of  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] paginated report that shows detail rows or child groups indented under a parent group in the same column, as shown in the example below:  
  
 ![Rendered stepped report](../../reporting-services/report-design/media/steppedreportrendered.gif "Rendered stepped report")  
  
 Traditional table reports place the parent group in an adjacent column on the report. The new tablix data region enables you to add a group and detail rows or child groups to the same column. To differentiate the group rows from the detail or child group rows, you can apply formatting such as font color, or you can indent the detail rows.  
  
 The procedures in this topic show you how to manually create a stepped report, but you can also use the New Table and Matrix Wizard. It provides the layout for stepped reports, making it easy to create them. After you complete the wizard, you can further enhance the report.  
  
> [!NOTE]  
>  The wizard is available only in Report Builder.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## To create a stepped report  
  
1.  Create a table report. For example, insert a tablix data region and add fields to the Data row.  
  
2.  Add a parent group to your report.  
  
    1.  Click anywhere in the table to select it. The Grouping pane displays the Details group in the Row Groups pane.  
  
    2.  In the Grouping Pane, right-click the Details Group, point to **Add Group**, and then click **Parent Group**.  
  
    3.  In the **Tablix Group** dialog box, provide a name for the group and type or select a group expression from the drop-down list. The drop-down list displays the simple field expressions that are available in the Report Data pane. For example, [PostalCode] is a simple field expression for the PostalCode field in a dataset.  
  
    4.  Select **Add group header**. This option adds a static row above the group for the group label and group totals. Likewise, you can select **Add group footer** to add a static row below the group. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
     You now have a basic tabular report. When it is rendered, you see one column with the group instance value, and one or more columns with grouped detail data. The following figure shows what the data region might look like on the design surface.  
  
     ![Table data region with group](../../reporting-services/report-design/media/tabledataregionwithgroup.gif "Table data region with group")  
  
     The following figure shows how the rendered data region might look when you view the report.  
  
     ![Rendered grouped report](../../reporting-services/report-design/media/tablereportrendered.gif "Rendered grouped report")  
  
3.  For a stepped report, you do not need the first column that shows the group instance. Instead, copy the value in the group header cell, delete the group column, and paste in the first text box in the group header row. To remove the group column, right-click the group column or cell, and click **Delete Columns**. The following figure shows what the data region might look like on the design surface.  
  
     ![Data region with group header row](../../reporting-services/report-design/media/tabledataregiongroupheader.gif "Data region with group header row")  
  
4.  To indent the detail rows under the group header row in the same column, change the padding of the detail data cell.  
  
    1.  Select the cell with the detail field that you want to indent. The text box properties for that cell appear in the Properties pane.  
  
    2.  In the Properties pane, under **Alignment**, expand the properties for **Padding**.  
  
    3.  For **Left**, type a new padding value, such as **.5in**. Padding indents the text in the cell by the value you specify. The default padding is 2 points. Valid values for the Padding properties are zero or a positive number, followed by a size designator.  
  
         Size designators are:  
  
        |||  
        |-|-|  
        |**in**|Inches (1 inch = 2.54 centimeters)|  
        |**cm**|Centimeters|  
        |**mm**|Millimeters|  
        |**pt**|Points (1 point = 1/72 inch)|  
        |**pc**|Picas (1 pica = 12 points)|  
  
     Your data region will look similar to the following example.  
  
     ![Data region for stepped report](../../reporting-services/report-design/media/steppedreportdataregion.gif "Data region for stepped report")  
  
     **Data Region for Stepped Report Layout**  
  
     On the **Home** tab Click **Run**. The report displays the group with indented levels for the child group values.  
  
## To create a stepped report with multiple groups  
  
1.  Create a report as described in the previous procedure.  
  
2.  Add additional groups to your report.  
  
    1.  In the Row Groups pane, right-click the group, click **Add Group**, and then choose the type of group you want to add.  
  
        > [!NOTE]  
        >  There are several ways to add groups to a data region. For more information, see [Add or Delete a Group in a Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-or-delete-a-group-in-a-data-region-report-builder-and-ssrs.md).  
  
    2.  In the **Tablix Group** dialog box, type a name.  
  
    3.  In **Group expression**, type an expression or select a dataset field to group on. To create an expression, click the expression (**fx**) button to open the **Expression** dialog box.  
  
    4.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
3.  Change the padding for the cell that displays the group data.  
  
## See Also  
 [Page Headers and Footers &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/page-headers-and-footers-report-builder-and-ssrs.md)   
 [Formatting Report Items &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/formatting-report-items-report-builder-and-ssrs.md)   
 [Tablix Data Region &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tablix-data-region-report-builder-and-ssrs.md)   
 [Tables &#40;Report Builder  and SSRS&#41;](../../reporting-services/report-design/tables-report-builder-and-ssrs.md)   
 [Matrices &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-a-matrix-report-builder-and-ssrs.md)   
 [Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/create-invoices-and-forms-with-lists-report-builder-and-ssrs.md)   
 [Tables, Matrices, and Lists &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/tables-matrices-and-lists-report-builder-and-ssrs.md)  
  
  
