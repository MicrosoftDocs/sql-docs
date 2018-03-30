---
title: "Connections (MDS Add-in for Excel) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "master-data-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 2f2b2f9d-7744-460e-83cd-56d34ea70ff0
caps.latest.revision: 10
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Connections (MDS Add-in for Excel)
  To download data in to the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], you must first create a connection. A connection is how the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] web service knows which MDS database to connect to.  
  
 The connection string is usually the URL of the [!INCLUDE[ssMDSmdm](../../includes/ssmdsmdm-md.md)] web application, for example http://contoso/mds.  
  
 Each time you start Excel, you must connect to an MDS repository. The only exception is when the active spreadsheet already contains MDS-managed data. In this case, a connection is automatically made each time you refresh or publish data in the sheet.  
  
 You can create multiple connections. The most recently-accessed connection is considered the default.  
  
 Multiple users can be connected at the same time. However, conflicts can arise when multiple users attempt to publish the same data. For more information, see [Publishing Data &#40;MDS Add-in for Excel&#41;](../../2014/master-data-services/publishing-data-mds-add-in-for-excel.md).  
  
## Connect Automatically and Load Frequently-Used Data  
 If you want to always connect to the same server and load the same set of data, you can create shortcut query files, which contain connection and filter information. For more information about query files, see [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](../../2014/master-data-services/shortcut-query-files-mds-add-in-for-excel.md).  
  
## Data Quality Services  
 The [!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)] has Data Quality Services functionality to help you match data before publishing it to the MDS repository. When you make a connection, if a DQS database is installed on the same instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] as the MDS database, you will be able to view DQS buttons on the ribbon. If the DQS_Main database does not exist on the instance, these buttons are not displayed and data quality functionality is not available.  
  
## Troubleshooting Connections  
 When you connect to MDS, if you encounter any issues see [http://social.technet.microsoft.com/wiki/contents/articles/4520.aspx](http://social.technet.microsoft.com/wiki/contents/articles/4520.aspx) for troubleshooting tips.  
  
## Related Tasks  
  
|Task Description|Topic|  
|----------------------|-----------|  
|Create a connection to a [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] database.|[Connect to an MDS Repository &#40;MDS Add-in for Excel&#41;](../../2014/master-data-services/connect-to-an-mds-repository-mds-add-in-for-excel.md)|  
|Load MDS data into Excel.|[Load Data from MDS into Excel](../../2014/master-data-services/load-data-from-mds-into-excel.md)|  
|Filter MDS data before you load it into Excel.|[Filter Data before Loading &#40;MDS Add-in for Excel&#41;](../../2014/master-data-services/filter-data-before-loading-mds-add-in-for-excel.md)|  
  
## Related Content  
  
-   [Loading Data &#40;MDS Add-in for Excel&#41;](../../2014/master-data-services/loading-data-mds-add-in-for-excel.md)  
  
-   [Shortcut Query Files &#40;MDS Add-in for Excel&#41;](../../2014/master-data-services/shortcut-query-files-mds-add-in-for-excel.md)  
  
-   [Master Data Services Add-in for Microsoft Excel](../../2014/master-data-services/master-data-services-add-in-for-microsoft-excel.md)  
  
  