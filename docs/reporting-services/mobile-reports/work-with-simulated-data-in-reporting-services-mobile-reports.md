---
title: "Work with simulated data in Reporting Services mobile reports | Microsoft Docs"
ms.date: 02/08/2016
ms.prod: reporting-services
ms.prod_service: "reporting-services-native"
ms.technology: mobile-reports

ms.topic: conceptual
ms.assetid: 6baabc36-58fb-4a98-bb9c-c42bafb16d0f
author: maggiesMSFT
ms.author: maggies
---
# Work with simulated data in Reporting Services mobile reports
When you place a gallery element on the design surface, [!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)] immediately generates simulated data for that element. This data serves a number of useful purposes when creating mobile reports.   
  
![SS_MRP_SimDataTreeMapProps](../../reporting-services/mobile-reports/media/ss-mrp-simdatatreemapprops.png)  
  
Simulated data helps when taking a design-based approach to mobile report creation. Initially populating elements with simulated data allows you to create mobile report prototypes quickly without having to address specific data requirements. These mobile reports can then be evaluated for the overall aesthetics and effectiveness.  
  
## Create an Excel file with simulated data as a template  
  
Simulated data also provides a template that accurately represents the data requirements of a particular mobile report design.   
  
-  Click **Export All Data** in the upper-right corner of the Data View.   
  
[!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)] generates an Excel document containing the simulated data, allowing for quick substitution of actual data, ready for import.   
  
## How simulated data behaves  
  
The simulated data generated is tailored specifically to the mobile report you're creating. As you place more elements on the design surface, the associated simulated data grows and changes to provide the best possible experience short of real data. This evolution ensures that extra fields and filter possibilities are available in case you add extra series to chart visualizations or expand the scope of one or more mobile report elements another way.  
  
As mentioned previously, you can export simulated data to an Excel file, creating a perfect data template for the associated mobile report. You can  swap in your own real data into the Excel file and import it back into the [!INCLUDE[PRODUCT_NAME](../../includes/ss-mobilereptpub-short.md)].   
  
After all controls are bound to real data, simulated tables that are no longer in use are automatically removed from the mobile report. You can't remove simulated tables still referenced by elements on the design surface.  
  
>**Note**: Simulated data does not add to the overall mobile report footprint because it's not serialized with the mobile report, but generated on-the-fly at runtime.  
  
### See also  
- [Create and publish mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)  
-  View [SQL Server mobile reports and KPIs in the iPad app](https://pbiwebprod-docs.azurewebsites.net/documentation/powerbi-mobile-ipad-kpis-mobile-reports)  (Power BI for iOS)  
-  View [SQL Server mobile reports and KPIs in the iPhone app](https://pbiwebprod-docs.azurewebsites.net/documentation/powerbi-mobile-iphone-kpis-mobile-reports) (Power BI for iOS)  
  
  
  
  
  

