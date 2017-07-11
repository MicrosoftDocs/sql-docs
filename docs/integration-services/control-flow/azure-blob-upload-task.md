---
title: "Azure Blob Upload Task | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "07/25/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.dts.designer.afpblobuptask.f1"
  - "sql14.dts.designer.afpblobuptask.f1"
ms.assetid: 6ea068b0-4cd8-45b5-b89d-09b8f25040c0
caps.latest.revision: 14
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Azure Blob Upload Task
  The **Azure Blob Upload Task** enables an SSIS package to upload files to an Azure blob storage.
  
>   [!NOTE]
> To ensure that the Azure Storage Connection Manager and the components that use it - that is, the Blob Source, the Blob Destination, the Blob Upload Task, and the Blob Download Task - can connect to both general-purpose storage accounts and to blob storage accounts, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). For more info about these two types of storage accounts, see [Introduction to Microsoft Azure Storage](https://azure.microsoft.com/en-us/documentation/articles/storage-introduction/#general-purpose-storage-accounts). 
    
To add an **Azure Blob Upload Task**, drag-drop it to the SSIS Designer, and double-click or right-click and click **Edit** to see the following **Azure Blob Upload Task Editor** dialog box.  
  
 The **Azure Blob Upload Task** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).
  
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
  
  
