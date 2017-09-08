---
title: "Custom maps in Reporting Services mobile reports | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 59a4ebad-587a-4770-afcd-c69216b8afd9
caps.latest.revision: 9
author: "maggiesMSFT"
ms.author: "maggies"
manager: "erikre"
---
# Custom maps in Reporting Services mobile reports
Geographic maps in SQL Server Mobile Report Publisher are defined in a format known as *ESRI shapefiles*.  
  
Initially designed by a private company, this is now a widespread semi-open format used in a large portion of GIS applications. In accordance with this format, Mobile Report Publisher requires two files to be provided when defining a map:  
  
- A .SHP file for shape geometries  
- A .DBF file for metadata  
  
The base files names must match (e.g. *canada.shp* and *canada.dbf*). The metadata must include the field *NAME* with the value of the corresponding shape's name (key), to be used when populating the map with data.  
  
> **Note**: The two map files together, the SHP and the DBF, can be no bigger than 512 KB. If your map files are too big, use a tool like [http://mapshaper.org/](http://mapshaper.org/) to reduce their size.  
  
See how to [add custom maps to mobile reports](../../reporting-services/mobile-reports/add-a-custom-map-to-a-reporting-services-mobile-report.md).  
  
## Technical information  
  
- The official specification: [http://www.esri.com/library/whitepapers/pdfs/shapefile.pdf](http://www.esri.com/library/whitepapers/pdfs/shapefile.pdf)  
- The Wikipedia shapefile article: [http://en.wikipedia.org/wiki/Shapefile](http://en.wikipedia.org/wiki/Shapefile)  
  
## Creating & editing map geometry  
  
Creating and editing shapefiles is a complex process that is beyond the scope of this document. Here are some resources and applications to help you get started:  
  
- ArcGIS: [http://www.arcgis.com/](http://www.arcgis.com/)  
- MAPublisher plug-in for Adobe Illustrator: [http://www.avenza.com/mapublisher](http://www.avenza.com/mapublisher)  
- QuantumGIS (free): [http://www.qgis.org/](http://www.qgis.org/)  
- Manco ShapeFile Editor: [http://www.mancosoftware.com/ShapeFileEditor](http://www.mancosoftware.com/ShapeFileEditor)  
  
## Existing shapefiles  
  
Many existing shapefiles can be downloaded from the Web, from sites like these:  
  
- Diva-GIS: [http://www.diva-gis.org/Data](http://www.diva-gis.org/Data)  
- OpenStreetMap: [http://openstreetmapdata.com/data](http://openstreetmapdata.com/data)  
  
### See also  
- [Maps in Reporting Services mobile reports](../../reporting-services/mobile-reports/maps-in-reporting-services-mobile-reports.md)  
- [Create and publish mobile reports with SQL Server Mobile Report Publisher](../../reporting-services/mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md)   
  
  
  
