---
title: "Preload the Cache (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "cache [Reporting Services]"
  - "preloading cache"
ms.assetid: 152a1051-8aa5-4c01-bc85-f8be8971b0cd
author: markingmyname
ms.author: maghan
manager: craigg
---
# Preload the Cache (Report Manager)
  You can preload the cache for a shared dataset by creating a cache refresh plan for the shared dataset.  
  
 You can preload the cache for a report in two ways:  
  
1.  Create a cache refresh plan for the report. This is the preferred method.  
  
2.  Use a data-driven subscription to preload the cache with instances of parameterized reports. This was the only way to preload the cache in versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] earlier than [!INCLUDE[ssKilimanjaro](../../includes/sskilimanjaro-md.md)]. For more information, see [Caching Reports &#40;SSRS&#41;](caching-reports-ssrs.md).  
  
 The following conditions must be met before you can cache a report or a shared dataset:  
  
-   The shared dataset or the report must have caching enabled.  
  
-   The shared data sources for the shared dataset or the report must be configured to use stored credentials or no credentials.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service must be running.  
  
### To preload the cache by creating a cache refresh plan  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page, and then navigate to the item that you want to cache.  
  
3.  Hover over the item, click the drop-down list, and then click **Manage**.  
  
4.  Click the **Cache Refresh Options** tab.  
  
5.  On the toolbar, click **New Cache Refresh Plan**.  
  
    > [!NOTE]  
    >  If the item does not have caching enabled, you will be prompted to enable caching. To enable caching, click **OK**.  
  
     The Cache Refresh Plan page opens.  
  
6.  Optionally type a description for the refresh plan.  
  
7.  For a shared schedule, click **Shared schedule**, and then select the name of the schedule to use.  
  
     For a custom schedule, click **Item-specific schedule**, and then click **Configure**.  
  
8.  Configure the schedule  
  
9. [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
### To preload the cache with a user-specific report by using a data-driven subscription  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page, and then navigate to the report you want to create a subscription for.  
  
3.  Click the report, click the **Subscriptions** tab, and then click **New Data-Driven Subscription**.  
  
4.  Optionally type a description for the subscription.  
  
5.  From the **Specify how recipients are notified** list, select **Null Delivery Provider**.  
  
6.  Specify a data source type and then click **Next** to configure the data source.  
  
7.  Specify the connection type, connection string, and credentials for accessing the data source that contains subscriber data. The following example illustrates a connection string used to connect to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database called Subscribers:  
  
    ```  
    data source=<servername>; initial catalog=Subscribers  
    ```  
  
8.  Click **Next**.  
  
9. Specify the query or command that retrieves subscriber data. Optionally increase the time-out period for queries that take a long time to process. For example:  
  
    ```  
    Select * from UserInfo  
    ```  
  
10. Click **Validate**. The query must be validated before you continue. When the **Query validated successfully** message appears, click **Next**.  
  
11. Because you cannot configure delivery extension settings for the null delivery provider, click **Next**.  
  
12. Specify report parameter values for the subscription, and click **Next**.  
  
13. Specify when the subscription is processed. Do not choose **When the report data is updated on the report server**. That setting is for snapshots only. If want to use a pre-existing schedule, select **On a shared schedule**.  
  
     Or, to create a custom schedule, click **On a schedule created for this subscription** and then click **Next**. Configure the schedule and then click **Finish**.  
  
    > [!NOTE]  
    >  In order for the subscribers to receive the newest report, the schedule that you configure should be consistent with the report delivery schedule that you have defined for the subscribers. For more information, see [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
14. Configure the Execution options for the report as follows. On the report page, click the **Properties** tab.  
  
15. In the left frame, click the **Execution** tab.  
  
16. On the page, select **Render this report with the most recent data**.  
  
17. Choose one of the following two cache options and configure the expiration as follows:  
  
    -   To make the cached copy expire after a particular time period, click **Cache a temporary copy of the report. Expire copy of report after a number of minutes.** Type the number of minutes for report expiration.  
  
    -   To make the cached copy expire on a schedule, click **Cache a temporary copy of the report. Expire copy of report on the following schedule.** Click **Configure**, or select a shared schedule to set a schedule for report expiration.  
  
18. Click **Apply**.  
  
## See Also  
 [Data-Driven Subscriptions](../subscriptions/data-driven-subscriptions.md)   
 [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](../create-a-data-driven-subscription-ssrs-tutorial.md)   
 [Performance, Snapshots, Caching &#40;Reporting Services&#41;](performance-snapshots-caching-reporting-services.md)   
 [Set Report Processing Properties](set-report-processing-properties.md)   
 [Caching Reports &#40;SSRS&#41;](caching-reports-ssrs.md)  
  
  
