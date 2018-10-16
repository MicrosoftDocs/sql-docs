---
title: "Upload a File or Report (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "publishing reports [Reporting Services], uploading files"
  - "reports [Reporting Services], publishing"
  - "uploading reports [Reporting Services]"
  - "uploading files [Reporting Services]"
  - "files [Reporting Services], uploading"
ms.assetid: 79027e11-f4ba-4bfd-bd8c-d334e3da02a1
author: markingmyname
ms.author: maghan
manager: craigg
---
# Upload a File or Report (Report Manager)
  Report Manager provides an upload feature so that you can add reports, models, and other files to a report server without having to publish those items from a client application. Files that you upload from the file system are stored as items on the report server. The type of file you upload determines how it is stored:  
  
-   .rdl files are stored as reports.  
  
-   .smdl files are stored as report models.  
  
-   All other files, including shared data source (.rds) files, are uploaded as resources. Resources are not processed by a report server, but can be viewed in Report Manager if the report server supports the MIME type of the file.  
  
### To upload a file or report  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to the **Contents** page. Navigate to the folder to which you want to add an item.  
  
3.  Click **Upload File**.  
  
4.  Click **Browse** to select a file to upload. You can upload a report definition file, an image, a document, or any file that you want to make available on the report server.  
  
5.  Type a name for the new item. An item name can include spaces, but cannot include the reserved characters: ; ? : \@ & = + , $ / * \< > |.  
  
6.  If you want to replace an existing item with the new item, select **Overwrite item if it exists**.  
  
7.  [!INCLUDE[clickOK](../../includes/clickok-md.md)]  
  
## See Also  
 [Create, Delete, or Modify a Shared Data Source &#40;Report Manager&#41;](../create-delete-or-modify-a-shared-data-source-report-manager.md)   
 [Contents Page &#40;Report Manager&#41;](../contents-page-report-manager.md)   
 [Upload File Page &#40;Report Manager&#41;](../upload-file-page-report-manager.md)   
 [Upload Files to a Folder](../report-server/upload-files-to-a-folder.md)  
  
  
