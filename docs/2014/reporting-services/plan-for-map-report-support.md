---
title: "Plan for Map Report Support | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 5ddc97a7-7ee5-475d-bc49-3b814dce7e19
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Plan for Map Report Support
  [!INCLUDE[ssRSCurrent](../includes/ssrscurrent-md.md)] supports map reports that use spatial data sources. Spatial data can come from SQL Server databases, from ESRI Shapefiles, or from the Map Gallery that is installed with either Reporting Services or Report Builder. A map can also display a background of Bing map tiles. A report author can create a report that specifies spatial data or Bing map tiles as dynamic and retrieved at run time or as static and embedded in the report definition.  
  
## Support for Bing Maps  
 Maps can include a background layer that displays Bing map tiles. To view a published report that has a map tile layer, the report server must be configured to retrieve tiles from Bing Maps Web Services. For more information, see [RSReportServer Configuration File](report-server/rsreportserver-config-configuration-file.md).  
  
 In each report, report authors can specify whether to use a Secure Sockets Layer (SSL) connection to retrieve tiles from the tile server. To do this, in the Properties pane for the tile layer, they must set the Boolean property UseSecureConnection to `true`.  
  
> [!NOTE]  
>  For more information about the use of Bing map tiles in your report, see [Additional Terms of Use](https://go.microsoft.com/fwlink/?LinkId=151371) and [Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=151372).  
  
## Report Design Recommendations  
 Good report design for map reports requires that the report author assess the trade-offs between static and dynamic spatial data and find a balance that serves the report users. Embedded map elements can significantly increase the size of the report definition, but reduce the time that is required to view the map report. Dynamic map elements reduce the report definition size but increase the time that is required to process and view the map. The report author must find the right balance between these opposing issues.  
  
 When report definition size is an issue, as report server administrator, you can encourage report designers to reduce the amount of spatial data in a report definition.  
  
 Under the following conditions, map elements are embedded in the report definition:  
  
-   When the report is created, the spatial data source is on the local file system. This includes spatial data from the Map Gallery or ESRI Shapefiles that have been installed locally. By default, the map wizard and map layer wizard embed spatial data from data sources on the local file system.  
  
-   The report author chooses the option to embed spatial data manually in the report.  
  
 To help reduce the size of report definitions that have maps, report authors can use one or more of the following options:  
  
-   From Report Designer in [!INCLUDE[ssBIDevStudioFull](../includes/ssbidevstudiofull-md.md)], add spatial data sources that are ESRI Shapefiles to the report server project. When you deploy the project, the ESRI Shapefiles are published to the report server in addition to the report. When the report author runs the Map wizard, they can specify a spatial data source from the Report Server project, and the map elements are not embedded in the report definitions by default.  
  
-   From Report Builder, add spatial data sources that are ESRI Shapefiles by selecting Shapefiles from the report server. When the report author runs the Map wizard, they can browse to and select a spatial data source from the report server, and the map elements are not embedded in the report definitions by default.  
  
-   When map data must be embedded map data, adjust the viewport center and zoom level to include just the map data that is needed for the report.  
  
 For more information, [Maps &#40;Report Builder and SSRS&#41;](report-design/maps-report-builder-and-ssrs.md).  
  
## See Also  
 [Troubleshoot Reports: Map Reports &#40;Report Builder and SSRS&#41;](report-design/troubleshoot-reports-map-reports-report-builder-and-ssrs.md)  
  
  
