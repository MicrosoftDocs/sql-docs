---
title: "Preload the cache (SSRS)"
description: Learn how to preload the cache for a shared dataset by creating a cache refresh plan for the shared dataset in a Reporting Services report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "cache [Reporting Services]"
  - "preloading cache"
---
# Preload the cache  
  You can preload the cache for a shared dataset by creating a cache refresh plan for the shared dataset.  
  
 You can preload the cache for a report in two ways:  
  
1.  Create a cache refresh plan for the report. This method is the preferred method.  
  
1.  Use a data-driven subscription to preload the cache with instances of parameterized reports. This subscription was the only way to preload the cache in versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] earlier than [!INCLUDE[sql2008r2](../../includes/sql2008r2-md.md)]. For more information, see [Caching Reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md).  
  
 The following conditions must be met before you can cache a report or a shared dataset:  
  
-   The shared dataset or the report must have caching enabled.  
  
-   The shared data sources for the shared dataset or the report must be configured to use stored credentials or no credentials.  
  
-   The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Agent service must be running.  
  
## Preload the cache by creating a cache refresh plan  
  
1. Start the [web portal of a report server](../../reporting-services/web-portal-ssrs-native-mode.md).  
  
1. Select **Browse** from the **Home** screen and navigate the folder hierarchy to locate the item that you want to cache.  
  
1. Select the ellipsis in the top-right corner of the item, then select **Manage** from the menu.  
  
1. Select the **Caching** tab in the vertical menu on the left.  
  
1. To activate caching for a dataset, select the **Cache copies of this dataset and use them when available** option. The  **Cache expiration** section then appears beneath it. Select one of the following options:

    - **Cache expires after x minutes**: Enter the desired number of minutes for `x`.
    - **Cache expires on a schedule**: Reporting Services provides shared schedules and report-specific schedules to help you control processing, consistent content, and the performance of report distribution. For more information, see [Create, modify, and delete schedules](../../reporting-services/subscriptions/create-modify-and-delete-schedules.md). You have several options on how to create a schedule. In the following case, cache expiration is the example:
    Select one of the two scheduling choices:  
      - **Shared schedule** option, then select a schedule from the **Select a shared schedule** box. For more information, see [Schedules](../../reporting-services/subscriptions/schedules.md "Schedules").  
      - **Report-specific schedule** option, then select the **Edit schedule** link, if necessary, to display the **Schedule details** page.  

         :::image type="content" source="../../reporting-services/report-server/media/preload-the-cache/web-portal-dataset-cache-schedule-details.png" alt-text="Screenshot of the web portal cache expiration schedule details page for datasets.":::


          On the **Schedule details** page, you can select:
         - The type of schedule:
           - **Hour**: Run the schedule every: specify hours and minutes and the start time.
           - **Day**: Select one of the three choices:  
              - **On the following days**: (Sun, Mon, Tue, Wed, Thu, Fri, Sat).
              - **Every weekday**
              - **Repeat after this number of days**: Specify a number.  
           - **Week**: Specify both of the following two items:
              - **Repeat after this number of weeks**: Specify a number.  
              - **On days**: Pick the days of the week to run it.  
           - **Month**: Which month(s), with a choice of:
              - **On week of the month**: Select (1st, 2nd, 3rd, 4th or Last) from the list.  
                 - **On day of the week**: Select one or more of the days of the week to run the report (Sun, Mon, Tue, Wed, Thu, Fri, Sat).  
                 - **On calendar day(s)**: Enter the actual day number of the month separated  by commas, or a range of days separated by a dash, or any combination of both, for example, 1,3-5.  
           - **Once**: Indicates a single occurrence.  
         - **Start time**: The time of the day for the schedule to start.  
         - **Start and end dates**: Specify the start date and optionally the end date of the schedule.
         - Select **Apply** to save the schedule.  
           > [!NOTE]
           > If the item doesn't have caching enabled, you are prompted to enable caching. To enable caching, select **OK**.  

         - Select **Create cache refresh plan** to create/save the cache plan. The **Cache Refresh Plans** page opens on the screen. From here you can:
           - Add a new cache refresh plan.
           - Create a new cache refresh plan from an existing plan.
           - Refresh the cache refresh plans page.
           - Delete a plan.
           - Search for a plan by name.

         If no cache refresh plans are saved yet, the list is empty, and the **Add** option is the only available option. Select **+ New cache refresh plan** to add a new plan, and the **New Cache Refresh Plan** page is displayed.  
           - Enter a **Description** in the first box to name the refresh plan.  
           - Select one of the following options in the **Refresh the cache on the following schedule**:
             - **Shared schedule**: Select a shared schedule from the adjacent menu. 
             - **Report-specific schedule**: Edit the schedule by selecting the **Edit schedule** link if desired to display the **Schedule details** page. 
             - Select **Create cache refresh plan** to save the plan if you're adding, or **Apply** if you're editing the plan.  
      You're returned to the updated **Cache Refresh Plans** page.
  
## Preload the cache with a user-specific report by using a data-driven subscription

1. Start the [web portal of a report server](../../reporting-services/web-portal-ssrs-native-mode.md).  
1. Select **Browse** from the **Home** screen and navigate the folder hierarchy to locate the report you want to subscribe to.  
1. Right-click the report, select **Subscribe** from the menu. The **New Subscriptions** page is displayed.  
1. Enter a description for the subscription in the **Description** box.  
1. Under **Type of subscription** select one of the two options:  
   - **Standard subscription**: Select this option to generate and deliver one report.
   - **Data-driven subscription**: Select this option to generate and deliver one report for each row in a dataset. Select this option to preload the cache.
1. In the **Schedule** section, select one of the following options:
   - **Shared schedule**: Select a shared schedule from the list.  
   - **Report-specific schedule**: Edit the schedule by selecting the **Edit schedule** link if desired to display the **Schedule details** page.  
1. The **Destination** section displays the following choices in a list:
    - **Windows File Share**
    - **E-Mail**
    - **Null Delivery Provider**: For this task, select **Null delivery provider**.  
1. In the **Dataset** section, edit or create a dataset for this report subscription by selecting **Edit dataset**.  
1. On the **Edit Dataset** page in the **data source** section, choose the data source that contains the report parameter values and delivery options. Your choices are:  
   - **A shared data source**: Select the ellipsis and select a shared data source from the **Shared Data Source** folder.
   - **A custom data source**: Choose this option unless you or someone else completed the following steps to create it as a shared data source.  
     - Specify the connection type, connection string, and credentials for accessing the data source that contains subscriber data. The following example illustrates a connection string used to connect to a SQL Server database named `Subscribers`.  
  
   ```T-SQL
   data source=<servername>;initial catalog=Subscribers  
   ```
  
1. In the **Query** section, specify the query that retrieves the desired subscriber data.  For example:  
  
    ```T-SQL  
    Select * from RptSubscribers  
    ```
  
    Optionally, increase the time-out period for queries that take a long time to process.  
  
1. Select **Validate**. The query must be validated before you continue. When the **Validation successful** message appears, a list of dataset fields is displayed by the **Validate** button. Select **Apply** to create the custom data source.  
  
1. You're returned to the **New Subscription** page.  In the **Report Parameters** section,  specify report parameter values for the report parameters displayed, if any.  

1. Select **Create subscription**.  
  
1. The **Subscriptions** page is displayed showing your new Data-driven subscription. From this page, you can enable the subscription when you're ready by selecting the checkbox to the left of it, and selecting **Enable**.

1. Specify when the subscription is processed. Don't choose **When the report data is updated on the report server**. That setting is for snapshots only. If you want to use a pre-existing schedule, select **On a shared schedule**.  
  
     Or, to create a custom schedule, select **On a schedule created for this subscription**, and then select **Next**. Configure the schedule, and then select **Finish**.  
  
    > [!NOTE]  
    > In order for the subscribers to receive the newest report, configure the schedule to be consistent with the report delivery schedule that you have defined for the subscribers. For more information, see the [web portal of a report server (SSRS Native Mode)](../web-portal-ssrs-native-mode.md).
  
1. Configure the Execution options for the report as follows. On the **Report** page, select the **Properties** tab.  
  
1. In the left frame, select the **Execution** tab.  
  
1. On the page, select **Render this report with the most recent data**.  
  
1. Choose one of the following two cache options and configure the expiration as follows:  
  
    - To make the cached copy expire after a particular time period, select **Cache a temporary copy of the report. Expire copy of report after a number of minutes.** Enter the number of minutes for report expiration.  
  
    - To make the cached copy expire on a schedule, select **Cache a temporary copy of the report. Expire copy of report on the following schedule.** Select **Configure**, or select a shared schedule to set a schedule for report expiration.  
  
1. Select **Apply**.
  
## Related content

- [Data-driven subscriptions](../../reporting-services/subscriptions/data-driven-subscriptions.md)
- [Create a data-driven subscription &#40;SSRS tutorial&#41;](../../reporting-services/create-a-data-driven-subscription-ssrs-tutorial.md)
- [Performance, snapshots, caching &#40;Reporting Services&#41;](../../reporting-services/report-server/performance-snapshots-caching-reporting-services.md)
- [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md)
- [Cache reports &#40;SSRS&#41;](../../reporting-services/report-server/caching-reports-ssrs.md)
- [Work with shared datasets](../../reporting-services/work-with-shared-datasets-web-portal.md)
