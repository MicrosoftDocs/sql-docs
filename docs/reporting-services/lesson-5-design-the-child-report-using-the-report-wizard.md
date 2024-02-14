---
title: "Lesson 5: Design the child report by using the Report Wizard"
description: Learn how to design the child report using the Report Wizard in Report Designer after you create a data connection and data table for your child report.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/18/2016
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Lesson 5: Design the child report by using the Report Wizard
After you create a data connection and data table for the child report, your next step is to design the child report using the Report Wizard in Report Designer. For more information about Report Designer, see [Design Reports with Report Designer &#40;SSRS&#41;](../reporting-services/tools/design-reporting-services-paginated-reports-with-report-designer-ssrs.md).  
  
### Design the child report by using the Report Wizard  
  
1.  Make sure that the top-level website is selected in **Solution Explorer**.  
  
2.  Right-click on the website and select **Add New Item**.  
  
3.  In the **Add New Item** dialog box, select **Report Wizard**, enter a name for the report file, and then choose **Add**.  
  
    This action launches the Report Wizard.  
  
4.  In the **Dataset Properties** page, in the **Data source** box, select **DataSet2**.  
  
    The **Available datasets** box is automatically updated with the DataTable you created.  
  
5.  Select **Next**.  
  
6.  In the **Arrange Fields** page do the following:  
  
    1.  Drag **ProductID**, **PurchaseOrderID**, **PurchaseOrderDetailID**, **OrderQty**, **ReceivedQty**, **RejectedQty**, and **StockedQty** from **Available Fields** to the **Values** box.  
  
    2.  Select the arrow next to **Sum(ProductID)**, **Sum(PurchaseOrderID)**, **Sum(PurchaseOrderDetailID)**, **Sum(OrderQty)**, **Sum(ReceivedQty)**, **Sum(RejectedQty)**, and **Sum(StockedQty)** and clear the **Sum** selection.  
  
7.  Select **Next** twice, then choose **Finish** to close the **Report Wizard**.  
  
    You created the .rdlc file. The file opens in Report Designer. The tablix you designed is now displayed in the design surface.  
  
8.  With the .rdlc file open, add a parameter by doing the following steps:  
  
    1.  Right-click **Parameters** in the **Report Data** pane, and then select **Add Parameters**.  
  
    2.  Enter **productid** in the **Name** box.  
  
    3.  Confirm that **Integer** is selected in the **Data Type** list box.  
  
    4.  Select **OK**.  
  
9. Save the .rdlc file.  
  
## Next step

You successfully designed the child report by using the Report Wizard. Next, you add a ReportViewer control to the website application. See [Lesson 6: Add a ReportViewer control to the application](../reporting-services/lesson-6-add-a-reportviewer-control-to-the-application.md).  
  
  
  

