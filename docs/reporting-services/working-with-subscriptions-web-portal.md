---
title: "Work with subscriptions (web portal)"
description: Learn how to use the Subscriptions page to list all of the subscriptions for the current report in Reporting Services.
author: maggiesMSFT
ms.author: maggies
ms.date: 01/24/2022
ms.service: reporting-services
ms.subservice: reporting-services
ms.topic: conceptual
ms.custom: updatefrequency5
---

# Work with subscriptions (web portal)

[!INCLUDE[ssrs-appliesto](../includes/ssrs-appliesto.md)] [!INCLUDE[ssrs-appliesto-2016-and-later](../includes/ssrs-appliesto-2016-and-later.md)] [!INCLUDE[ssrs-appliesto-pbirsi](../includes/ssrs-appliesto-pbirs.md)]

Use the Subscriptions page to list all of the subscriptions for the current report. If you have sufficient permission (as conveyed by the "Manage all subscriptions" task), you can view the subscriptions of all users. Otherwise, this page shows only the subscriptions that you own.  

> [!NOTE]
> This page is not supported in the mobile layout.
  
Before you can create a new subscription, you must verify that the report data source uses stored credentials. Use the Data Sources properties page to store credentials.  
  
> [!NOTE]
> The SQL Server Agent service needs to be started.

:::image type="content" source="../reporting-services/media/working-with-subscriptions-web-portal/ssrs-manage-subscriptions.png" alt-text="Screenshot of the Subscriptions page that shows the Employee Sales Summary subscription." lightbox="../reporting-services/media/working-with-subscriptions-web-portal/ssrs-manage-subscriptions.png":::

You get to the Subscriptions page by selecting the **ellipsis (...)** of a report, choosing **Manage** and selecting **Subscriptions**.  
  
From the Subscriptions page, you can create new subscriptions by selecting **+ New Subscription**. You can also edit existing subscriptions, or delete subscriptions that you select.  
  
This page also provides the result status of subscription runs on the **Result** column. If an error occurred for a subscription, you want to check the result column first to see what the message was. 

You can also run a subscription whenever you want by selecting **Run now** on the Subscriptions page.
  
## Create or edit a subscription  
Use the New Subscription or Edit Subscription page to create a new subscription or modify an existing subscription to a report. The options on this page vary depending on your role assignment. Users with advanced permissions can work with more options.  
  
Subscriptions are supported for reports that can run unattended. At a minimum, the report must use stored or no credentials. If the report uses parameters, a default value must be specified. Subscriptions might become inactive if you change report execution settings or remove the default values used by parameter properties. For more information, see [Create and manage subscriptions for Native Mode report servers](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md).
  
## Type of subscription  
You can select between a **Standard subscription** and a **Data-driven subscription**.  
  
:::image type="content" source="../reporting-services/media/working-with-subscriptions-web-portal/ssrswebportal-subscriptions3.png" alt-text="Screenshot showing the Type of Subscription section.":::

A data-driven subscription is one that queries a subscriber database for subscription information each time the subscription runs. Data-driven subscriptions use query results to determine the recipients of the subscription, delivery settings, and report parameter values. At run time, the report server runs a query to get values used for subscription settings.   
  
To create a data-driven subscription, you must know how to write a query or command that gets the data for the subscription. You must also have a data store that contains the subscriber data (for example, subscriber names and email addresses) to use for the subscription.  
  
This option is available to users with advanced permissions. If you use default security, data-driven subscriptions can't be used for reports located in a My Reports folder.  
  
## Destination  
Select the delivery extension to use to distribute the report.   
  
The availability of a delivery extension depends on whether you install and configure it on the report server. Report Server email is the default delivery extension, but it must be configured before you can use it. File Share delivery doesn't require configuration, but you must define a shared folder before you can use it.  

:::image type="content" source="../reporting-services/media/working-with-subscriptions-web-portal/ssrswebportal-subscriptions2.png" alt-text="Screenshot showing the Destination and Delivery Options (Windows File Share) sections.":::
  
Depending on the delivery extension you select, the following settings appear:  
  
-   Email subscriptions provide fields that are familiar to email users (for example, To, Subject, and Priority fields). Specify **Include Report** to embed or attach the report, or **Include Link** to include a URL to the report. Specify **Render Format** to choose a presentation format for the attached or embedded report. For more information, see [Create an email subscription](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md#bkmk_create_email_subscription). 
  
-   File share subscriptions provide fields that allow you to specify a target location. You can deliver any report to a file share. However, reports that support interactive features (including matrix reports that support drill-down to supporting rows and columns) are rendered as static files. You can't view drill-down rows and columns in a static file. The file share name must be specified in Uniform Naming Convention (UNC) format (for example, \mycomputer\public\myreportfiles). Don't include a trailing backslash in the path name. The report file is delivered in a file format that is based on the render format. For example, if you choose Excel, the report is delivered as an .xlsx file. For more information, see [Create a file share subscription](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md#bkmk_create_fileshare_subscription) for details.
  
## Data-driven subscription dataset  
For a data-driven subscription, you need to define the dataset used for the subscription. Select **Edit Dataset** to supply that information.  

:::image type="content" source="../reporting-services/media/working-with-subscriptions-web-portal/ssrswebportal-subscriptions4.png" alt-text="Screenshot that shows the Dataset section.":::
  
You need to first provide a **data source** to use for the query. This source can be either a shared data source, or you can supply a custom data source.  
  
You need to then supply a **query** that lists the different options needed for the subscription to run. The screen provides the fields that need to be returned. These fields vary depending on your delivery method and the parameters of the report.  
  
For best result, run the query in SQL Server Management Studio first, before using it in the data-driven subscription. You can then examine the results to verify that it contains the information you require. Important points to recognize about the query results are:  
  
-   Columns in the result set determine the values that you can specify for delivery options and report parameters. For example, if you're creating a data-driven subscription for email delivery, you should have a column of email addresses.  
  
-   Rows in the result set determine the number of report deliveries that are generated. If you have 10,000 rows, the report server generates 10,000 notifications and deliveries.  

:::image type="content" source="../reporting-services/media/working-with-subscriptions-web-portal/ssrswebportal-subscriptions5.png" alt-text="Screenshot that shows the Query section.":::
  
You can then validate the query. You can also define a **query timeout**.  
  
After you create the query, you can then assign values to the required fields. You can either enter your manual data, or select a field from the dataset you created. 

## Related content

- [Create and manage subscriptions for Native Mode report servers](subscriptions/create-and-manage-subscriptions-for-native-mode-report-servers.md)
- [The web portal of a report server (SSRS Native Mode)](../reporting-services/web-portal-ssrs-native-mode.md)  
- [Work with paginated reports (web portal)](working-with-paginated-reports-web-portal.md)  
- [Work with shared datasets - web portal](../reporting-services/work-with-shared-datasets-web-portal.md)

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user).
