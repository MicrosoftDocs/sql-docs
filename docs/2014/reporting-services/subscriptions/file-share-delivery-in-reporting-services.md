---
title: "File Share Delivery in Reporting Services | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "subscriptions [Reporting Services], file share delivery"
  - "file share delivery [Reporting Services]"
ms.assetid: 9f338dd3-f68a-4355-b9d7-9b25dacf3b5e
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# File Share Delivery in Reporting Services
  SQL Server [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] includes a file share delivery extension so that you can deliver a report to a folder. The file share delivery extension is available by default and requires no additional configuration. In order for file delivery to succeed, you must set write access permissions on the shared folder. In addition, users who require access to the reports must have read permissions on the shared folder.  
  
 To distribute a report to a file share, you define either a standard subscription or a data-driven subscription. You can subscribe to and request delivery for only one report at a time. To learn how to use file share delivery in a data-driven subscription, see [Create a Data-Driven Subscription &#40;SSRS Tutorial&#41;](../create-a-data-driven-subscription-ssrs-tutorial.md). Additionally, the account that runs remote file share subscriptions requires rights to log on locally on the [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] computer.  
  
||  
|-|  
|**[!INCLUDE[applies](../../includes/applies-md.md)]**  [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode &#124; [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode|  
  
 In this topic:  
  
-   [Characteristics of a Report That is Delivered to a Shared Folder](#bkmk_Characteristics)  
  
-   [Target Folders](#bkmk_target_folders)  
  
-   [File Formats](#bkmk_file_formats)  
  
-   [File Options](#bkmk_file_options)  
  
##  <a name="bkmk_Characteristics"></a> Characteristics of a Report That is Delivered to a Shared Folder  
 Unlike reports that are hosted and managed by a report server, reports that are delivered to a shared folder are static files. Interactive features that are defined for the report do not work for reports that are stored as files on the file system. Interaction features are represented as static elements. For example, if you deliver a matrix report, the resulting file shows the top-level view of the report; you cannot expand rows and columns to view supporting data. If the report includes charts, the default presentation is used. If the report links through to another report, the link is rendered as static text. If you want to retain interactive features in a delivered report, use e-mail delivery instead. For more information, see [E-Mail Delivery in Reporting Services](e-mail-delivery-in-reporting-services.md).  
  
##  <a name="bkmk_target_folders"></a> Target Folders  
 When defining a subscription that uses file share delivery, you must specify an existing folder as the target folder. The report server does not create folders on the file system. The folder that you specify must be accessible over a network connection.  
  
 Verify that users who will view the reports in the shared folder have Read permission.  
  
 When specifying the target folder in a subscription, use Uniform Naming Convention (UNC) format that includes the computer's network name. Do not include trailing backslashes in the folder path. The following example illustrates a UNC path:  
  
```  
\\<servername>\reportarchive\operations\2003  
```  
  
 When you create the folder, consider the connection limits you require. The report server requires two connections, but you must include enough connections to accommodate additional users who want to open reports on the shared folder.  
  
##  <a name="bkmk_file_formats"></a> File Formats  
 Reports can be rendered in a variety of file formats, such as HTML or Excel. To save the report in a specific file format, select that rendering format when creating your subscription. For example, choosing **Excel** saves the report as a [!INCLUDE[ofprexcel](../../includes/ofprexcel-md.md)] file. Although you can choose from any supported rendering format, some formats work better than others when rendering to a file.  
  
 For file share delivery, choose a format that delivers the report in a single file, where all images and related content are included in the report. Suitable formats include Web archive, PDF, TIFF, and Excel. Avoid HTML4.0. If your report includes images, the HTML 4.0 formats will not include them in the file.  
  
##  <a name="bkmk_file_options"></a> File Options  
 When you create a subscription, you can choose options that determine how the file name is created and whether it is replaced by newer versions over time. A fully qualified file name has three parts: a name, an extension, and text or a number that is appended to the file to create a unique file name. Overwrite options determine whether the text or number is added to the file name.  
  
 The file name is based on the report name, but you can provide a custom name in the subscription. The extension is optional, but if you specify it, the report server will create an extension that corresponds to the rendering format.  
  
 You can specify overwrite options to reuse the same file name for each report delivery or to create a new file. To overwrite the file, you must use the same file name and extension.  
  
 An alternative approach to creating unique files for every delivery is to include a timestamp in the file name. To do this, add the `@timestamp` variable to the file name (for example, *CompanySales@timestamp*). With this approach, the file name is unique by definition, so it will never be overwritten.  
  
## See Also  
 [Create, Modify, and Delete Standard Subscriptions &#40;Reporting Services in Native Mode&#41;](create-and-manage-subscriptions-for-native-mode-report-servers.md)  
  
  
