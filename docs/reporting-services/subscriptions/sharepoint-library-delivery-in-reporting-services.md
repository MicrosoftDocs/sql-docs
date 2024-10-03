---
title: "SharePoint library delivery in Reporting Services"
description: Learn how to use the SharePoint library delivery extension in Reporting Services by using a subscription from an application page on a SharePoint site.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: subscriptions
ms.topic: conceptual
ms.custom:
  - updatefrequency5
helpviewer_keywords:
  - "SharePoint integration [Reporting Services], report delivery"
  - "delivering reports [Reporting Services]"
  - "subscriptions [Reporting Services], SharePoint library delivery"
---
# SharePoint library delivery in Reporting Services
  A report server that is configured for SharePoint integration includes a delivery extension that you can use to send a report to a SharePoint library.  
  
 To use the SharePoint delivery extension, you must create a subscription from an application page on a SharePoint site, and then select **SharePoint document library** as the delivery type. You can't use the SharePoint delivery extension for subscriptions that you create in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] or Report Manager.  
  
> [!NOTE]  
>  The delivery extension does not support the delivery of reports to a SharePoint site if the report server is running in native mode. If you attempt to call the delivery extension programmatically for a native mode report server, the server will return the **rsDeliveryExtensionNotFound** error and log the **rsOperationNotSupportedSharePointMode** error in the report server log files.  
  
## Requirements  
 Requirements for delivering rendered reports to a library include:  
  
-   The report server must be configured for SharePoint integration mode.  
  
-   The report server must have the SharePoint delivery extension installed and configured.  
  
-   The report must be a report definition (.rdl) file. You can't deliver other report server content types, such as models or resources, through a subscription. You can't subscribe to reports that use models as a data source.  
  
-   The report must use stored credentials. This requirement is a prerequisite for creating any subscription on a report, regardless of the delivery type.  
  
-   The destination must be a SharePoint library. When choosing a target library, you must choose one that is on the same SharePoint site. You can't deliver a report to a library on another server or another site within the same site collection.  
  
 Properties and metadata aren't part of report delivery. When the report is delivered for the first time, it inherits the security settings of the folder or list that contains it. If you later modify security settings or set report properties, those settings are retained. The subscription just refreshes the report that is stored at the specified location.  
  
## SharePoint permissions  
 To create the subscription, you must have View Items permission on the report. To deliver the report, you must have Add Items permission on the library to which the report is delivered.  
  
## How to create, modify and delete subscriptions  
  
1.  Go to the SharePoint site from which you access the report.  
  
2.  Choose the report, select the down arrow next to the report, and choose **Manage Subscriptions**.  
  
3.  Select **Create**, **Edit**, or **Delete**.  
  
 A Status message on the **Manage Subscriptions** list displays current information about the subscription, including whether it succeeded and the date and time the subscription last ran.  
  
## Set delivery options  
 You can set the following delivery options on a subscription that delivers a report to a SharePoint library.  
  
 Render output format  
 Specify the application format in which you want the report delivered. The report is rendered in this format before delivery. The output format you select determines the default file extension.  
  
 The list of output formats you can select from is the set of rendering extensions that are installed on the report server.  
  
 You can't specify output formats that are for internal use only, or that aren't supported for report servers that run in SharePoint integrated mode. These formats include Null, RGDI and HTMLOWC.  
  
 File name and extension  
 Specify the file name and extension of the report as you want it to appear in the target library. If you don't specify a file extension, the report server creates one based on the report output format. This value is required. The file name must not include the following characters: `: \ / * ? " < > | # { } %`.  
  
 Title  
 Specifies an optional **Title** property for the report in the target library. This property is a standard property for all items stored in a library. Users can specify whether to show or hide this property when viewing library contents on a SharePoint site.  
  
 Path  
 Specifies a fully qualified URL to the SharePoint library, including the SharePoint Web application and site. For example: `https://mySharePointWeb/MySite/MyDocLib`; where `https://mySharePointWeb` indicates the Web application, "MySite" is the SharePoint site, and "MyDocLib" is the SharePoint library where the report is delivered.  
  
 You can't specify a page, site, or list. The target container must be a library in the same site or farm.  
  
 Overwrite options  
 Specifies whether the subscription processing replaces a file with the same name and extension with a newer version. Choose **Overwrite** if you want to replace an existing file with a newer version. Choose **None** if you don't want the subscription to replace a file. In this case, no delivery occurs if a file exists with the target name and extension. Choose **Autoincrement** if you want to add successive versions of the same file by appending a number at the end of the file name.  
  
 Autocopy  
 If you use the Autocopy feature to automatically copy the latest version of a file to multiple locations, the file is copied if **Overwrite** is enabled. If you used **Autoincrement** or **None**, the delivery fails and the **rsDeliveryError** error occurs.  
  
## Related content

- [Create and manage subscriptions for SharePoint mode report servers](../../reporting-services/subscriptions/create-and-manage-subscriptions-for-sharepoint-mode-report-servers.md)
- [Subscriptions and delivery &#40;Reporting Services&#41;](../../reporting-services/subscriptions/subscriptions-and-delivery-reporting-services.md)
- [Specify credential and connection information for report data sources](../../reporting-services/report-data/specify-credential-and-connection-information-for-report-data-sources.md)
