---
title: "Lesson 3: Design the Parent Report using the Report Wizard | Microsoft Docs"
ms.date: 05/18/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: reporting-services

ms.topic: conceptual
ms.assetid: 2f69dcd3-cd6d-45a9-a62a-ba6f5f3179d8
author: markingmyname
ms.author: maghan
---
# Lesson 3: Design the Parent Report using the Report Wizard
After you create a data connection and a data table for the parent report, your next step is to design the parent report using the Report Wizard in Report Designer. For more information about Report Designer, see [Design Reports with Report Designer &#40;SSRS&#41;](../reporting-services/tools/design-reporting-services-paginated-reports-with-report-designer-ssrs.md).  
  
### To design the parent report using the Report Wizard  
  
1.  Make sure that the top-level website is selected in **Solution Explorer**.  
  
2.  Right-click on the website and select **Add New Item**.  
  
3.  In the **Add New Item** dialog box, select **Report Wizard**, enter a name for the report file, and then select **Add**.  
  
    This launches the Report Wizard.  
  
4.  On the **Dataset Properties** page, in the **Data source** box, select the **DataSet1** you created in [Lesson 2: Define a Data Connection and Data Table for Parent Report](../reporting-services/lesson-2-define-a-data-connection-and-data-table-for-parent-report.md).  
  
    The **Available datasets** box is automatically updated with the **DataTable** you created above.  
  
5.  Select **Next**.  
  
6.  In the **Arrange Fields** page do the following:  
  
    1.  Drag **ProductID**, **Name**, **ProductNumber**, **SafetyStockLevel**, and **ReorderLevel** from **Available fields** to the **Values** box.  
  
    2.  Select the arrow next to **Sum(ProductID)**, **Sum(SafetyStockLevel)**, **Sum(ReorderLevel)** and clear the **Sum** selection.  
  
7.  Select **Next** twice, then select **Finish** to close the **Report Wizard**.  
  
    You've now created the .rdlc file. The file opens in Report Designer. The tablix you designed is now displayed in the design surface.  
  
8.  Save the .rdlc file.  
  
## Next Task  
You have successfully designed the parent report using the Report Wizard. Next, you will create a data connection and a data table for the child report. See [Lesson 4: Define a Data Connection and Data Table for Child Report](../reporting-services/lesson-4-define-a-data-connection-and-data-table-for-child-report.md).  
  
  
  

