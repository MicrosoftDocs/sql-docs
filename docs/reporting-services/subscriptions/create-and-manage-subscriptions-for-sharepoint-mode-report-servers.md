---
title: "Create and manage subscriptions for SharePoint mode report servers"
description: Learn to create a Reporting Services subscription to deliver reports from a SharePoint web app you integrated with a SharePoint mode report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "subscriptions [Reporting Services], creating"
  - "subscriptions [Reporting Services], deleting"
  - "subscriptions [Reporting Services], managing"
---
# Create and manage subscriptions for SharePoint mode report servers
  You can create [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions to deliver reports from a SharePoint Web application that is integrated with a SharePoint mode report server. Subscriptions can deliver reports to a document library, file folder, or as e-mail. This article summarizes the requirements and steps for creating a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscription.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**<br /><br /> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode &#124; SharePoint 2010 and SharePoint 2013|  
  
 When you create a subscription, there are three ways to specify its delivery:  
  
-   **Document library**: You can create a subscription that delivers a document based on the original report to a library within the same SharePoint site as the original report. You can't deliver the document to a library on another server or another site within the same site collection. To deliver the document, you must have Add Items permission on the library to which the report is delivered.  
  
-   **File folder:** You can deliver a document based on the original report to a shared folder on the file system. You must select an existing folder that is accessible over a network connection.  
  
-   **E-mail:** If the report server is configured to use the Report Server E-mail delivery extension, you can create a subscription that sends a report or an exported report file (saved in an output format) to your in-box. To receive just the notification without the report or report URL, clear the **Include a link to this report** and the **Show report inside message** checkboxes.  
  
 **In this topic:**  
  
-   [General requirements for subscriptions](#bkmk_subscription_requirements)  
  
-   [Create a subscription to deliver a report to a SharePoint library](#bkmk_tosharepoint_library)  
  
-   [Create a subscription to deliver a report to a SharePoint library](#bkmk_tosharepoint_library)  
  
-   [Create a subscription for report server e-mail delivery](#bkmk_subscription_for_email)  
  
-   [View or modify a subscription](#bkmk_to_modify_subscription)  
  
-   [Delete a subscription](#bkmk_to_delete_subscription)  
  
##  <a name="bkmk_subscription_requirements"></a> General requirements for subscriptions  
 To create a subscription, the report must use stored credentials and you must have permission to view the report and create alerts.  
  
 When you create a subscription, you can select an output file format. Not every report works well in every format. Before you select a format in a subscription, open the report and export it to different formats to verify that it appears as expected.  
  
 Users require **Edit Items** list permission in SharePoint if they want to be able to create [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions. For more information, see [SharePoint site and list permission reference for report server items](../../reporting-services/security/sharepoint-site-and-list-permission-reference-for-report-server-items.md)  
  
> [!IMPORTANT]  
>  A subscription that delivers a report to a library or to a shared folder creates a new, static file that is based on the original report, but it is not a true report definition that runs in the Report Viewer Web Part. If the original report has interactive features (such as drillthrough links) or dynamic content, those features will not be available in the static file that is delivered to the target location. If you select a "Web Page" you can preserve some interactivity, but because the document is not an .rdl file that runs in the Report Viewer, navigating through a report creates new pages in the browser session that you must scroll through to return to the site.  
  
 You can't rename the file name extension of an exported report to .rdl and have it run in the Report Viewer Web Part. If you want to create a subscription that provides an actual report, use the Report Server E-mail delivery extension and set options to include a link to the report.  
  
 Version settings on the library that contains the delivered document determine whether a new version of the document is created with each delivery. By default, version settings are enabled for each library. Unless you specifically choose **No versioning**, a new major version of the document is created upon delivery. Only major versions of the document are created; minor versions are never created as a result of subscription delivery, even if you select a versioning option that allows minor versions. You replace older deliveries with newer ones when the maximum limit is reached if you limit the number of major versions that are retained.  
  
 Output formats that you select for a subscription are based on rendering extensions that are installed on the report server. You can only select output formats that the rendering extensions support on the report server.  
  
###  <a name="bkmk_tosharepoint_library"></a> Create a subscription to deliver a report to a SharePoint library  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Select the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Select **Add Subscription**.  
  
4.  In **Delivery Extension**, select **SharePoint Document Library**.  
  
5.  In **Document Library**, select a library within the same site.  
  
6.  In **File Options**, specify the file name and title for the document that you want the subscription to create.  
  
7.  In **Output Format**, select the application format.  
  
     Web archive (MHTML) is the default because it produces a self-contained HTML file, but it doesn't preserve interactive report features that might be in the original report.  
  
8.  In **Overwrite Options**, specify an option that determines whether subsequent deliveries overwrite a file. If you want to preserve previous deliveries, you can select **Create a file with a unique name**. A number is appended to new files to create a unique file name.  
  
9. In **Delivery Event**, specify a schedule or event that causes the subscription to run. You can create a custom schedule or select a shared schedule if one is available. You can also run the subscription whenever the data is refreshed for a report that runs with snapshot data. For more information about schedules and data processing, see [Set processing options &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md).  
  
10. In **Parameters**, if you're creating a subscription to a parameterized report, specify the values that you want to use with the report when the subscription is processed. The parameters section isn't visible on this page if the report you select doesn't contain parameters. For more information about parameters, see [Set parameters on a published report &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
###  <a name="bkmk_subscription_for_sharedfolder"></a> Create a subscription for shared folder delivery  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Select the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Select **Add Subscription**.  
  
4.  In **Delivery Extension**, select **Windows File Share**.  
  
5.  In **File Name**, enter the name of the file that you want to create in the shared folder.  
  
6.  In **Path**, enter a folder path in Uniform Naming Convention (UNC) format that includes the computer's network name. Don't include trailing backslashes in the folder path. An example path might be `\\ComputerName01\Public\MyReports`, where Public and MyReports are shared folders.  
  
7.  In **Render Format**, select the application format for the report.  
  
8.  In **Write Mode**, choose between **None**, **Autoincrement**, or **Overwrite**. These options determine whether subsequent deliveries overwrite a file. If you want to preserve previous deliveries, you can choose **Autoincrement**. A number is appended to new files to create a unique file name. If you choose **None**, no delivery occurs if a file of the same name already exists in the target location.  
  
9. In **File Extension**, choose **True** to add a file name extension that corresponds to the application file format, or False to create the file without an extension.  
  
10. In **User Name** and **Password**, enter credentials that have write permissions on the shared folder.  
  
11. In **Delivery Event**, specify a schedule or event that causes the subscription to run. You can create a custom schedule or select a shared schedule if one is available. You can also run the subscription whenever the data is refreshed for a report that runs with snapshot data. For more information about schedules and data processing, see [Set processing options &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md).  
  
12. In **Parameters**, if you're creating a subscription to a parameterized report, specify the values that you want to use with the report when the subscription is processed. For more information about parameters, see [Set parameters on a published report &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
###  <a name="bkmk_subscription_for_email"></a> Create a subscription for report server e-mail delivery  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Select the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Select **Add Subscription**.  
  
4.  In **Delivery Extension**, select **E-mail**.  
  
5.  In **Delivery options**, specify an e-mail address to send the report to.  
  
6.  Optionally, you can modify the Subject line. The Subject line uses built-in parameters that capture the report name and time when it was processed. These parameters are the only built-in parameters that can be used. The parameters are placeholders that customize the text that appears in the Subject line, but you can replace it with static text.  
  
7.  Choose **Include a link to this report** if you want to embed a report URL in the body of the message.  
  
8.  In **Report Contents**, specify whether you want to embed the actual report in the body of the message.  
  
     The rendering format and browser determine whether the report is embedded or attached. If your browser supports HTML 4.0 and MHTML, and you select the Web archive rendering format, the report is embedded as part of the message. All other rendering formats (CSV, PDF, and so on) deliver reports as attachments. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't check the size of the attachment or message before sending the report. If the attachment or message exceeds the maximum limit allowed by your mail server, the report isn't delivered. Choose one of the other delivery options (such as URL or notification) for large reports.  
  
9. In **Delivery Event**, specify a schedule or event that causes the subscription to run. You can create a custom schedule or select a shared schedule if one is available. You can also run the subscription whenever the data is refreshed for a report that runs with snapshot data. For more information about schedules and data processing, see [Set processing options &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md).  
  
10. In **Parameters**, if you're creating a subscription to a parameterized report, specify the values that you want to use with the report when the subscription is processed. For more information about parameters, see [Set parameters on a published report &#40;Reporting Services in SharePoint integrated mode&#41;](../../reporting-services/report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
###  <a name="bkmk_to_modify_subscription"></a> View or modify a subscription  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Select the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Identify each subscription by the type of delivery. Select the subscription type to view and change the existing properties.  
  
###  <a name="bkmk_to_delete_subscription"></a> Delete a subscription  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Select the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Select the checkbox next to the subscription, and select **Delete**.  
  
## Related content
  
 [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [E-Mail delivery in Reporting Services](../../reporting-services/subscriptions/e-mail-delivery-in-reporting-services.md)   
 [File share delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md)   
 [SharePoint library delivery in Reporting Services](../../reporting-services/subscriptions/sharepoint-library-delivery-in-reporting-services.md)   
 [Configure a report server for e-Mail delivery (Report Server Configuration Manager)](../install-windows/configure-the-report-server-service-account-ssrs-configuration-manager.md)  
  
  
