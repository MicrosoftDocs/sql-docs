---
title: "Cache a Shared Dataset | Microsoft Docs"
description: Learn how to schedule the expiration of a cached shared dataset in Report Manager. Caching shared datasets improves performance.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-server


ms.topic: conceptual
ms.assetid: c2d8c81a-da1e-4a8a-9845-fff9a0903d24
author: maggiesMSFT
ms.author: maggies
---
# Cache a Shared Dataset
  One way to improve performance is to configure caching properties for a shared dataset. When a shared dataset is cached, a copy of the query results is saved for a specified period of time. The first user who requests a report that uses the shared dataset must wait for the query results and all processing to complete before viewing the report. Subsequent users who request the report within the caching period will experience improved performance because the query and processing has already occurred. You can also specify a cache refresh plan to run the query and cache the results until the specified cache expiration.  
  
 Users running reports based on a shared dataset or cache refresh plans create the query cache and in both cases, the cache is available based on the cache expiration options.  
  
 There are restrictions on the types of shared datasets that you can cache. For example, the query results cannot be cached if the data varies based on the user identity or if data is retrieved using the security token of the user who requests the report. For more information, see [Cache Shared Datasets &#40;SSRS&#41;](../../reporting-services/report-server/cache-shared-datasets-ssrs.md) and [Caching Reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md).  
  
### To schedule the expiration of a cached report  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../web-portal-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the shared dataset for which you want to set caching properties, hover over the item, and click the drop-down arrow.  
  
3.  In the drop-down menu, click **Manage**.  
  
4.  In the left frame, click **Caching**.  
  
    > [!NOTE]  
    >  If you see the error **Credentials used to run the shared dataset are not stored**, the cache shared dataset option will be disabled. You need modify the data source to store credentials or modify the shared dataset to use a different data source that does store credentials.  
  
5.  Select **Cache share dataset**.  
  
6.  Select the option to expire the cache after 30 minutes. You can also choose for the cache to expire on a specified schedule.  
  
7.  Click **Apply**.  
  
## See Also  
 [Manage Shared Datasets](../../reporting-services/report-data/manage-shared-datasets.md)  
  
