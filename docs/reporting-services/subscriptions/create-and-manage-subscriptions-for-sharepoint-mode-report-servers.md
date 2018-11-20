---
title: "Create and Manage Subscriptions for SharePoint Mode Report Servers | Microsoft Docs"
ms.date: 03/07/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: subscriptions


ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [Reporting Services], creating"
  - "subscriptions [Reporting Services], deleting"
  - "subscriptions [Reporting Services], managing"
ms.assetid: 44be7ee2-33ce-46e4-9d1a-a20aaf43a227
author: markingmyname
ms.author: maghan
---
# Create and Manage Subscriptions for SharePoint Mode Report Servers
  You can create [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions to deliver reports from a SharePoint Web application that is integrated with a SharePoint mode report server. Subscriptions can deliver reports to a document library, file folder, or as e-mail. This topic summarizes the requirements and steps for creating a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscription.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**<br /><br /> [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode &#124; SharePoint 2010 and SharePoint 2013|  
  
 When you create a subscription, there are three ways to specify its delivery:  
  
-   **Document library**: You can create a subscription that delivers a document based on the original report to a library within the same SharePoint site as the original report. You cannot deliver the document to a library on another server or another site within the same site collection. To deliver the document, you must have Add Items permission on the library to which the report is delivered.  
  
-   **File folder:** You can deliver a document based on the original report to a shared folder on the file system. You must select an existing folder that is accessible over a network connection.  
  
-   **E-mail:** If the report server is configured to use the Report Server E-mail delivery extension, you can create a subscription that sends a report or an exported report file (saved in an output format) to your in-box. To receive just the notification without the report or report URL, clear the **Include a link to this report** and the **Show report inside message** checkboxes.  
  
 **In this topic:**  
  
-   [General Requirements for Subscriptions](#bkmk_subscription_requirements)  
  
-   [To create a subscription to deliver a report to a SharePoint library](#bkmk_tosharepoint_library)  
  
-   [To create a subscription to deliver a report to a SharePoint library](#bkmk_tosharepoint_library)  
  
-   [To create a subscription for report server e-mail delivery](#bkmk_subscription_for_email)  
  
-   [To view or modify a subscription](#bkmk_to_modify_subscription)  
  
-   [To delete a subscription](#bkmk_to_delete_subscription)  
  
##  <a name="bkmk_subscription_requirements"></a> General Requirements for Subscriptions  
 To create a subscription, the report must use stored credentials and you must have permission to view the report and create alerts.  
  
 When you create a subscription, you can select an output file format. Not every report works well in every format. Before you select a format in a subscription, open the report and export it to different formats to verify that it appears as expected.  
  
 Users require **Edit Items** list permission in SharePoint if they want to be able to create [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] subscriptions. For more information, see [SharePoint Site and List Permission Reference for Report Server Items](../../reporting-services/security/sharepoint-site-and-list-permission-reference-for-report-server-items.md)  
  
> [!IMPORTANT]  
>  A subscription that delivers a report to a library or to a shared folder creates a new, static file that is based on the original report, but it is not a true report definition that runs in the Report Viewer Web Part. If the original report has interactive features (such as drillthrough links) or dynamic content, those features will not be available in the static file that is delivered to the target location. If you select a "Web Page" you can preserve some interactivity, but because the document is not an .rdl file that runs in the Report Viewer, clicking through a report creates new pages in the browser session that you must scroll through to return to the site.  
  
 You cannot rename the file name extension of an exported report to .rdl and have it run in the Report Viewer Web Part. If you want to create a subscription that provides an actual report, use the Report Server E-mail delivery extension and set options to include a link to the report.  
  
 Version settings on the library that contains the delivered document determine whether a new version of the document is created with each delivery. By default, version settings are enabled for each library. Unless you specifically choose **No versioning**, a new major version of the document will be created upon delivery. Only major versions of the document are created; minor versions are never created as a result of subscription delivery, even if you select a versioning option that allows minor versions. If you limit the number of major versions that are retained, older deliveries will be replaced by newer ones when the maximum limit is reached.  
  
 Output formats that you select for a subscription are based on rendering extensions that are installed on the report server. You can only select output formats that are supported by the rendering extensions on the report server.  
  
###  <a name="bkmk_tosharepoint_library"></a> To create a subscription to deliver a report to a SharePoint library  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Click the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Click **Add Subscription**.  
  
4.  In **Delivery Extension**, select **SharePoint Document Library**.  
  
5.  In **Document Library**, select a library within the same site.  
  
6.  In **File Options**, specify the file name and title for the document that will be created by the subscription.  
  
7.  In **Output Format**, select the application format.  
  
     Web archive (MHTML) is the default because it produces a self-contained HTML file, but it will not preserve interactive report features that might be in the original report.  
  
8.  In **Overwrite Options**, specify an option that determines whether subsequent deliveries overwrite a file. If you want to preserve previous deliveries, you can select **Create a file with a unique name**. A number will be appended to new files to create a unique file name.  
  
9. In **Delivery Event**, specify a schedule or event that causes the subscription to run. You can create a custom schedule, select a shared schedule if one is available, or run the subscription whenever the data is refreshed for a report that runs with snapshot data. For more information about schedules and data processing, see [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md).  
  
10. In **Parameters**, if you are creating a subscription to a parameterized report, specify the values that you want to use with the report when the subscription is processed. The parameters section is not visible on this page if the report you select does not contain parameters. For more information about parameters, see [Set Parameters on a Published Report &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
###  <a name="bkmk_subscription_for_sharedfolder"></a> To create a subscription for shared folder delivery  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Click the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Click **Add Subscription**.  
  
4.  In **Delivery Extension**, select **Windows File Share**.  
  
5.  In **File Name**, enter the name of the file that will be created in the shared folder.  
  
6.  In **Path**, enter a folder path in Uniform Naming Convention (UNC) format that includes the computer's network name. Do not include trailing backslashes in the folder path. An example path might be `\\ComputerName01\Public\MyReports`, where Public and MyReports are shared folders.  
  
7.  In **Render Format**, select the application format for the report.  
  
8.  In **Write Mode**, choose between **None**, **Autoincrement**, or **Overwrite**. These options determine whether subsequent deliveries overwrite a file. If you want to preserve previous deliveries, you can choose **Autoincrement**. A number will be appended to new files to create a unique file name. If you choose **None**, no delivery will occur if a file of the same name already exists in the target location.  
  
9. In **File Extension**, choose **True** to add a file name extension that corresponds to the application file format, or False to create the file without an extension.  
  
10. In **User Name** and **Password**, enter credentials that have write permissions on the shared folder.  
  
11. In **Delivery Event**, specify a schedule or event that causes the subscription to run. You can create a custom schedule, select a shared schedule if one is available, or run the subscription whenever the data is refreshed for a report that runs with snapshot data. For more information about schedules and data processing, see [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md).  
  
12. In **Parameters**, if you are creating a subscription to a parameterized report, specify the values that you want to use with the report when the subscription is processed. For more information about parameters, see [Set Parameters on a Published Report &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
###  <a name="bkmk_subscription_for_email"></a> To create a subscription for report server e-mail delivery  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Click the down arrow next to the report and then select **Manage Subscriptions**.  
  
3.  Click **Add Subscription**.  
  
4.  In **Delivery Extension**, select **E-mail**.  
  
5.  In **Delivery options**, specify an e-mail address to send the report to.  
  
6.  Optionally, you can modify the Subject line. The Subject line uses built-in parameters that capture the report name and time when it was processed. These are the only built-in parameters that can be used. The parameters are placeholders that customize the text that appears in the Subject line, but you can replace it with static text.  
  
7.  Choose **Include a link to this report** if you want to embed a report URL in the body of the message.  
  
8.  In **Report Contents**, specify whether you want to embed the actual report in the body of the message.  
  
     The rendering format and browser determine whether the report is embedded or attached. If your browser supports HTML 4.0 and MHTML, and you select the Web archive rendering format, the report is embedded as part of the message. All other rendering formats (CSV, PDF, and so on) deliver reports as attachments. [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] does not check the size of the attachment or message before sending the report. If the attachment or message exceeds the maximum limit allowed by your mail server, the report will not be delivered. Choose one of the other delivery options (such as URL or notification) for large reports.  
  
9. In **Delivery Event**, specify a schedule or event that causes the subscription to run. You can create a custom schedule, select a shared schedule if one is available, or run the subscription whenever the data is refreshed for a report that runs with snapshot data. For more information about schedules and data processing, see [Set Processing Options &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-server-sharepoint/set-processing-options-reporting-services-in-sharepoint-integrated-mode.md).  
  
10. In **Parameters**, if you are creating a subscription to a parameterized report, specify the values that you want to use with the report when the subscription is processed. For more information about parameters, see [Set Parameters on a Published Report &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-design/set-parameters-on-a-published-report-sharepoint-integrated-mode.md).  
  
###  <a name="bkmk_to_modify_subscription"></a> To view or modify a subscription  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Click the down arrow next to the report and then click **Manage Subscriptions**.  
  
3.  Each subscription is identified by the type of delivery. Click the subscription type to view and change the existing properties.  
  
###  <a name="bkmk_to_delete_subscription"></a> To delete a subscription  
  
1.  Browse to a SharePoint library that contains your report.  
  
2.  Click the down arrow next to the report and then click **Manage Subscriptions**.  
  
3.  Click the checkbox next to the subscription, and click **Delete**.  
  
## See Also  
 [Subscriptions and Delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)   
 [E-Mail Delivery in Reporting Services](../../reporting-services/subscriptions/e-mail-delivery-in-reporting-services.md)   
 [File Share Delivery in Reporting Services](../../reporting-services/subscriptions/file-share-delivery-in-reporting-services.md)   
 [SharePoint Library Delivery in Reporting Services](../../reporting-services/subscriptions/sharepoint-library-delivery-in-reporting-services.md)   
 [Configure a Report Server for E-Mail Delivery (SSRS Configuration Manager)](https://msdn.microsoft.com/b838f970-d11a-4239-b164-8d11f4581d83)  
  
  
