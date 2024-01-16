---
title: "Lesson 3: Design the parent report using the Report Wizard"
description: Learn how to design the parent report using the Report Wizard in Report Designer after you create a data connection and data table for your parent report.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/18/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Lesson 3: Design the parent report using the Report Wizard
After you create a data connection and a data table for the parent report, your next step is to design the parent report using the Report Wizard in Report Designer. For more information about Report Designer, see [Design reports with Report Designer &#40;SSRS&#41;](../reporting-services/tools/design-reporting-services-paginated-reports-with-report-designer-ssrs.md).  
  
### Design the parent report using the Report Wizard  
  
1.  Make sure that the top-level website is selected in **Solution Explorer**.  
  
2.  Right-click on the website and select **Add New Item**.  
  
3.  In the **Add New Item** dialog box, select **Report Wizard**, enter a name for the report file, and then choose **Add**.  
  
    This action launches the Report Wizard.  
  
4.  On the **Dataset Properties** page, in the **Data source** box, select the **DataSet1** you created in [Lesson 2: Define a data connection and data table for the parent report](../reporting-services/lesson-2-define-a-data-connection-and-data-table-for-parent-report.md).  
  
    The **Available datasets** box is automatically updated with the **DataTable** you previously created.  
  
5.  Select **Next**.  
  
6.  In the **Arrange Fields** page do the following:  
  
    1.  Drag **ProductID**, **Name**, **ProductNumber**, **SafetyStockLevel**, and **ReorderLevel** from **Available fields** to the **Values** box.  
  
    2.  Select the arrow next to **Sum(ProductID)**, **Sum(SafetyStockLevel)**, **Sum(ReorderLevel)**, and clear the **Sum** selection.  
  
7.  Select **Next** twice, then choose **Finish** to close the **Report Wizard**.  
  
    You created the .rdlc file. The file opens in Report Designer. The tablix you designed is now displayed in the design surface.  
  
8.  Save the .rdlc file.  
  
## Next step

You successfully designed the parent report using the Report Wizard. Next, you create a data connection and a data table for the child report. See [Lesson 4: Define a data connection and data table for the child report](../reporting-services/lesson-4-define-a-data-connection-and-data-table-for-child-report.md).  
  
  
  

