---
title: "Add a custom map to a Reporting Services mobile report"
description: You can add a custom map to a Reporting Services mobile report. This article describes how to load and connect data to a custom map.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/21/2022
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add a custom map to a Reporting Services mobile report

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Custom maps require two files:  
* An .SHP file for shape geometries  
* A .DBF file for metadata  
  
For more information about custom maps, see [Custom maps in Reporting Services mobile reports](../../reporting-services/mobile-reports/custom-maps-in-reporting-services-mobile-reports.md).  
  
Store the two files in the same folder. The file names of the two must match, for example, `canada.shp` and `canada.dbf`. The first column of the metadata in the DBF file is used to match with the key value of the corresponding shape's name, or key. This value is to be used when populating the map with data.
  
## Load a custom map  
  
1. On the **Layout** tab, select a map type: **Gradient Heat Map**, **Range Stop Heat Map**, or **Bubble Map**, drag it to the design surface, and make it the size you want.  
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-mapsgallery.png" alt-text="Screenshot of the Maps gallery with the Bubble Map highlighted.":::
  
1. In **Layout** view, select the **Visual Properties** panel, choose **Map**, and then select **Custom Map From File**.   
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-selectcustommap.png" alt-text="Screenshot of the option to select a custom map highlighted.":::  
  
1. In the **Open** dialog, browse to the location of the SHP and DBF files and select both of them.   
  
   :::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-selectdbfandshp.png" alt-text="Screenshot of the selected SHP and DBF files.":::   
  
## Connect data to a custom map  
When you first add the custom map to your report, [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)] populates it with simulated geography data.  
  
:::image type="content" source="../../reporting-services/mobile-reports/media/ssmrp-mapsdata.png" alt-text="Screenshot of the simulated geography table.":::  
  
Displaying real data in your custom map is the same as displaying data in the built-in maps. Follow the steps in [Maps in Reporting Services mobile reports](../../reporting-services/mobile-reports/maps-in-reporting-services-mobile-reports.md) to display your data.  
  
### Related content
- [Custom maps in Reporting Services mobile reports](../../reporting-services/mobile-reports/custom-maps-in-reporting-services-mobile-reports.md)  
- [Maps in Reporting Services mobile reports](../../reporting-services/mobile-reports/maps-in-reporting-services-mobile-reports.md)  
- [Create and publish mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)   
  
  
  
  
