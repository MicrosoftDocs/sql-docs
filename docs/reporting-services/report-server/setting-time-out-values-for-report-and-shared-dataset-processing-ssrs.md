---
title: Set time-out values for report and shared dataset processing in Reporting Services
description: See how you can specify time-out values to set limits on how system resources are used in your report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/04/2024
ms.service: reporting-services
ms.subservice: report-server
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "time-outs [Reporting Services]"
  - "query time-outs [Reporting Services]"
  - "report processing [Reporting Services], time-outs"
  - "report execution time-outs [Reporting Services]"

#customer intent: As a Reporting Services user, I want to see how to time-out values on my report server so that I can set limits on how much system resources are used.
---
# Set time-out values for report and shared dataset processing in Reporting Services

In [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], you can specify time-out values to set limits on how system resources are used. Report servers support two time-out values:  
  
- An embedded dataset query time-out value is the number of seconds that the report server waits for a response from the database. This value is defined in a report.  
  
- A shared dataset query time-out value is the number of seconds that the report server waits for a response from the database. This value is part of the shared dataset definition and can be changed when you manage the shared dataset on the report server.  
  
- A report execution time-out value is the maximum number of seconds that report processing can continue before processing is stopped. This value is defined at the system level. You can vary this setting for individual reports.  
  
Most time-out errors occur during query processing. If you encounter time-out errors, try increasing the query time-out value. Make sure to adjust the report execution time-out value so that the value is larger than the query time-out. The time period should be large enough to complete both query and report processing.  
  
## Set a query time-out for an embedded dataset in a report  

Query time-out values are specified during report authoring when you define an embedded dataset. For more information, see [Report embedded datasets and shared datasets &#40;Report Builder and SSRS&#41;](../../reporting-services/report-data/report-embedded-datasets-and-shared-datasets-report-builder-and-ssrs.md).

To set the query time-out value in Report Builder:

1. Right-click your database in the **Report Data** pane.
1. Select **Dataset Properties**.
1. On the Query tab in the Dataset Properties dialog box, enter the timeout value in the **Time out** field.

:::image type="content" source="media/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs/embedded-dataset-timeout.png" alt-text="Screenshot of the Dataset Properties dialog box highlighting the Time out field.":::

> [!NOTE]  
> For Paginated Reports in Power BI, the default value is set to 600 seconds.

## Set a query time-out for a data-driven subscription

The query time-out value for a data-driven subscription is specified on the **Edit Subscription** page. The value you specify determines how long the report server waits for query processing to complete when retrieving data from the subscriber data source.  
  
## Set a query time-out for a shared dataset  

Query time-out values are specified in seconds on the report server when you create or manage a shared dataset. By default, this value is set to 0 seconds, which is the equivalent of no time-out value. For more information, see [Manage shared datasets](../../reporting-services/report-data/manage-shared-datasets.md).

To set the query time-out value in the web portal:

1. On the **Browse** page, select **More Info** > **Manage**.
1. On the **Properties Page**, set the value in the **Query time-out in seconds** field.

:::image type="content" source="media/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs/shared-dataset-timeout.png" alt-text="Screenshot of the dataset Properties page highlighting the Query time-out in seconds field.":::
  
## Set a report execution time-out  

You can set the report execution time-out value to limit the amount of time that a report server uses to process a report. Report execution time-out values can be specified in the web portal. You can set a default value for all reports on the **Site Settings** page, and then override that value in the **Properties** page for a specific report. By default, the value is set to 1800 seconds. For more information, see [Set report processing properties](../../reporting-services/report-server/set-report-processing-properties.md).

To set the report time-out value in the web portal for a specific report:

1. On the **Browse** page, select **More Info** > **Manage**.
1. On the **Properties Page**, select the default, custom, or indefinite timeout option in **Advanced** > **Report time-out**.

:::image type="content" source="media/setting-time-out-values-for-report-and-shared-dataset-processing-ssrs/report-timeout.png" alt-text="Screenshot of the report Properties page highlighting the Report time out options.":::
  
## How report execution time-out values are evaluated  

The report server evaluates running jobs at 60-second intervals. At each 60-second interval, the report server compares actual process time against the report execution time-out value. If the processing time for a report exceeds the report execution time-out value, report processing stops.  
  
If you specify a time-out value smaller than 60 seconds, the report might still run. Processing would start and complete during the quiet part of the cycle when the report server isn't evaluating running jobs. For example, if you set a time-out value of 10 seconds for a report that takes 20 seconds to run, the report processes in full if report execution starts early in the 60-second cycle.  
  
> [!NOTE]  
> You can set the **RunningRequestsDbCycle** setting in the `RSReportServer.config` file to change the frequency of how often running jobs are evaluated.  
  
## Related content

- [Set processing options &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md)
- [Reporting Services report server &#40;native mode&#41;](../../reporting-services/report-server/reporting-services-report-server-native-mode.md)
- [Manage a running process](../../reporting-services/subscriptions/manage-a-running-process.md)
  
