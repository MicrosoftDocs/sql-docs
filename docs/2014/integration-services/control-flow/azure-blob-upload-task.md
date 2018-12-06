---
title: "Azure Blob Upload Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.afpblobuptask.f1"
  - "sql11.dts.designer.afpblobuptask.f1"
ms.assetid: 6ea068b0-4cd8-45b5-b89d-09b8f25040c0
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Azure Blob Upload Task
  The Azure Blob Upload Task enables an SSIS package to upload files to an Azure blob storage.   
To add an **Azure Blob Upload Task**, drag-drop it to the SSIS Designer, and double-click or right-click and click **Edit** to see the following **Azure Blob Upload Task Editor** dialog box.  
  
 The following table provides descriptions for the fields in this dialog box.  
  
|||  
|-|-|  
|**Field**|**Description**|  
|AzureStorageConnection|Specify an existing Azure Storage Connection Manager or create a new one that refers to an Azure Storage Account, which points to where the blob files are hosted.|  
|BlobContainer|Specifies the name of the blob container that will hold the uploaded files as blobs.|  
|BlobDirectory|Specifies the blob directory where the uploaded file will be stored as a block blob. The blob directory is a virtual hierarchical structure. If the blob already exists, it will be replaced.|  
|LocalDirectory|Specify the local directory that contains the files to be uploaded.|  
|FileName|Specifies a name filter to select files with the specified name pattern. E.g. MySheet*.xls\* includes files such as MySheet001.xls and MySheetABC.xlsx.|  
|TimeRangeFrom/TimeRangeTo|Specifies a time range filter. Files modified after **TimeRangeFrom** and before **TimeRangeTo** will be included.|  
  
  
