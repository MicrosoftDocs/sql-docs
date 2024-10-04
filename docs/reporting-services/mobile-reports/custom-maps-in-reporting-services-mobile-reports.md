---
title: "Custom maps in Reporting Services mobile reports"
description: Learn about geographic maps in SQL Server Mobile Report Publisher, defined in a format known as ESRI shapefiles.
author: maggiesMSFT
ms.author: maggies
ms.reviewer: maghan
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: mobile-reports
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---

# Custom maps in Reporting Services mobile reports

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Geographic maps in SQL Server Mobile Report Publisher are defined in a format known as *ESRI shapefiles*.

Designed by a private company, this format is now a widespread semi-open format used in a large portion of GIS applications. In accordance with this format, Mobile Report Publisher requires two files to be provided when defining a map:

- A .SHP file for shape geometries
- A .DBF file for metadata

The base files names must match, for example, `canada.shp` and `canada.dbf`. The metadata must include the field **NAME** with the value of the corresponding shape's name, or key, to be used when populating the map with data.

The two map files together, the SHP and the DBF, can be no bigger than 512 KB. If your map files are too large, use a tool like [https://mapshaper.org/](https://mapshaper.org/) to reduce their size.

See how to [add custom maps to mobile reports](add-a-custom-map-to-a-reporting-services-mobile-report.md).

## Technical information

- The official specification: [https://www.esri.com/library/whitepapers/pdfs/shapefile.pdf](https://www.esri.com/content/dam/esrisites/sitecore-archive/Files/Pdfs/library/whitepapers/pdfs/shapefile.pdf)
- The Wikipedia shapefile article: [https://en.wikipedia.org/wiki/Shapefile](https://en.wikipedia.org/wiki/Shapefile)

## Create and edit map geometry

Creating and editing shapefiles is a complex process that is beyond the scope of this document. Here are some resources and applications to help you get started:

- ArcGIS: [https://www.arcgis.com/](https://www.arcgis.com/index.html)
- MAPublisher plug-in for Adobe Illustrator: [https://www.avenza.com/mapublisher](https://www.avenza.com/mapublisher)
- QuantumGIS (free): [https://www.qgis.org/](https://www.qgis.org/en/site)

## Related content

- [Maps in Reporting Services mobile reports](maps-in-reporting-services-mobile-reports.md)
- [Create and publish mobile reports with SQL Server Mobile Report Publisher](create-mobile-reports-with-sql-server-mobile-report-publisher.md)
