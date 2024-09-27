---
title: "Create a Reporting Services mobile report"
description: With SQL Server Mobile Report Publisher, create SQL Server Reporting Services mobile reports for any screen size with flexible mobile report elements.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Create a Reporting Services mobile report

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

With SQL Server Mobile Report Publisher, you can quickly create SQL Server Reporting Services mobile reports that scale well to any screen size. They scale on to any screen on a design surface with adjustable grid rows and columns, and flexible mobile report elements.  
  
The first time you create a mobile report, you can install SQL Server Mobile Report Publisher, on your local machine from the Reporting Services web portal. Or you can install it from the [Microsoft Download Center](https://go.microsoft.com/fwlink/?LinkID=733527). After the first time, you can start it either from the web portal or locally.   
    
1. In the top bar of the Reporting Services web portal, select **New**, and then choose **Mobile Report**.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/pbi-ssmrp-newmenu.png" alt-text="Screenshot of the New menu that includes the Mobile Report option.":::
  
     
1. On the **Layout** tab in Mobile Report Publisher, select a navigator, gauge, chart, map, or datagrid and drag it to the design grid.  
  
1. Grab the lower-right corner of the element and drag it to the size you want.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-resizechart.png" alt-text="Screenshot of the element with the sizing control highlighted.":::
  
   This grid is the **Master** design grid, where you create the elements you want in your report. Later, you can [lay out the report for a tablet or phone](../../reporting-services/mobile-reports/lay-out-a-reporting-services-mobile-report-for-phone-or-tablet.md).     
     
   In **Visual Properties** under the design grid, notice the various properties you can set.  
     
1. Select the **Data** tab in the upper-left corner, and you see the chart already simulated data associated with it.   
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-simtable.png" alt-text="Screenshot of the chart with the simulated table highlighted.":::  
  
1. Select **Add Data** in the upper-right corner.  
  
1. Select **Local Excel** or **Report Server**.  
  
   > [!TIP]  
   > If you're adding data from Excel, make sure:
   >
   > * You [prepare the Excel data](../../reporting-services/mobile-reports/prepare-excel-data-for-reporting-services-mobile-reports.md) to work in your mobile report.  
   > * You close the file first.

1. Select the worksheets you want, and select **Import**.   
   You can add more than one worksheet from a workbook at a time.  
    
     :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-addexceldata.png" alt-text="Screenshot of the screen used to add Excel data.":::  
  
1. Still on the **Data** tab, in the **Data Properties** box select the table and field you want in the chart.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-dataprops.png" alt-text="Screenshot of the Gross Property property selected.":::   
  
1. Back on the **Layout** tab, in the **Visual Properties** box you can set properties like **Title**, **Time Unit**, and **Number Format**.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-chartvizprops.png" alt-text="Screenshot of the Visual Properties box.":::  
 
1. Select **Preview** in the upper left to see how your report is shaping up.  
  
1. Time to save your report. Select the Save icon in the upper left, and either **Save Locally** or **Save to Server**.  
  
   To save it to a server, you need access to a SQL Server Reporting Services report server.  
     
   ### Related content 
     
-   [Create and publish mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)  
-   [Lay out a Reporting Services mobile report for phone or tablet](../../reporting-services/mobile-reports/lay-out-a-reporting-services-mobile-report-for-phone-or-tablet.md)  
  
   
