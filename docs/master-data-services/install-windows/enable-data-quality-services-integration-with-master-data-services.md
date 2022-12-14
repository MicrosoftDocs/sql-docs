---
title: Enable Data Quality Services Integration
description: In the Master Data Services add-in for Excel, matching functionality is provided by Data Quality Services (DQS).
ms.custom: "seo-lt-2019"
ms.date: "12/13/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: master-data-services
ms.topic: conceptual
ms.assetid: ab32938d-a80e-4106-80d4-94b2de3d67dc
author: CordeliaGrey
ms.author: jiwang6
---
# Enable Data Quality Services Integration with Master Data Services

[!INCLUDE [SQL Server Windows Only - ASDBMI ](../../includes/applies-to-version/sql-windows-only-asdbmi.md)]

  In the [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)][!INCLUDE[ssMDSXLS](../../includes/ssmdsxls-md.md)], matching functionality is provided by Data Quality Services (DQS). This functionality must be enabled to be used.  
  
## Prerequisites  
  
-   A [!INCLUDE[ssMDSshort](../../includes/ssmdsshort-md.md)] web application and database must exist.  
  
-   The Data Quality Services feature and the Data Quality Client must be installed on the same [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance that hosts the MDS database. For more information, see [Install Data Quality Services](../../data-quality-services/install-windows/install-data-quality-services.md).  
  
### To enable Data Quality Services integration  
  
1.  Open [!INCLUDE[ssMDScfgmgr](../../includes/ssmdscfgmgr-md.md)].  
  
2.  In the left pane, click **Web Configuration**.  
  
3.  On the **Web Configuration** page, select the website and web application.  
  
4.  In the **Enable DQS Integration** section, click **Enable integration with Data Quality Services**.  
  
5.  On the confirmation dialog box, click **OK**.  
  
## See Also  
 [Data Quality Matching in the MDS Add-in for Excel](../../master-data-services/microsoft-excel-add-in/data-quality-matching-in-the-mds-add-in-for-excel.md)   
 [Master Data Services Add-in for Microsoft Excel](../../master-data-services/microsoft-excel-add-in/master-data-services-add-in-for-microsoft-excel.md)   
 [Install Master Data Services](../../master-data-services/install-windows/install-master-data-services.md)  
  
  
