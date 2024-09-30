---
title: "Get data from shared datasets in Reporting Services mobile reports"
description: SQL Server Mobile Report Publisher can access data from almost any source by using a shared data source, configured on a Reporting Services web portal.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Get data from shared datasets in Reporting Services mobile reports

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Besides [loading data from Excel files](../../reporting-services/mobile-reports/prepare-excel-data-for-reporting-services-mobile-reports.md), SQL Server Mobile Report Publisher can also access data from almost any source. Accessing data requires a shared data source, configured on a Reporting Services web portal. Read more about [creating shared data sources](../../reporting-services/report-data/create-modify-and-delete-shared-data-sources-ssrs.md) and [creating shared datasets](../../reporting-services/report-data/manage-shared-datasets.md).  
  
After shared data sources and shared datasets are configured on the Reporting Services server, you can use them in mobile reports you create in [!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)].   
  
After you connect to a [!INCLUDE[PRODUCT_NAME](../../includes/ssrsnoversion-md.md)] server from the [!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)], connecting a mobile report to a shared dataset is straightforward.   
  
1. On the **Data** tab, select **Add Data**.  
  
1. Select **Report Server**.   
  
1.  If you're connecting to the server for the first time, fill in the server name and your name and password. Put the server name in the Server address box in this format:  
  
    `<servername>/reports/`  
  
    As in this example:  
       
    :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-connecttoserver.png" alt-text="Screenshot of the screen used to connect to the server.":::
  
  
1. When you select the [!INCLUDE[PRODUCT_NAME](../../includes/ssrsnoversion-md.md)] server, you see the available datasets in folders. Select a dataset to import the data into [!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)].  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ss-mrp-serverdata.png" alt-text="Screenshot of the available datasets you can import.":::

  
After you import the dataset, you can design your mobile report just as you would with simulated data, or local data from an Excel file.  
  
By default, the shared dataset is always up to date with the latest data. The dataset is up to date because every time someone views a mobile report based on that dataset. SQL Server runs the underlying query and returns the latest data. Clearly, if lots of people view your mobile report this result might not be ideal, so you can set up caching to run the query periodically and cache the resulting dataset. This blog post explains [how caching and data refresh works in the web portal](https://christopherfinlan.com/2016/02/10/so-refreshinghow-data-refresh-works-with-mobile-reports-and-kpis-in-reporting-services/).  
  
## Add, edit, or remove a report server  
  
If you're already connected to a report server, when you select **Add Data** on the Data tab, you don't see an option to add another report server. Follow these steps instead.  
  
1. In the upper-left corner, select **Connections**.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-addconnectionicon.png" alt-text="Screenshot of the Connections button to open the Server Connection pane.":::
  
   The Server Connection pane opens.  
     
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-serverconnectnpane.png" alt-text="Screenshot of the Server Connection pane.":::

1. Add a new server connection, or edit or remove existing connections.  
  
### Related content 
- [Create and publish mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)  
-  [Web portal (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md)  
  
