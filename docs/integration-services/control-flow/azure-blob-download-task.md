---
title: "Azure Blob Download Task | Microsoft Docs"
ms.custom: ""
ms.date: "07/25/2016"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.afpblobdltask.f1"
  - "sql14.dts.designer.afpblobdltask.f1"
ms.assetid: 8a63bf44-71be-456d-9a5c-be7c31aff065
author: janinezhang
ms.author: janinez
manager: craigg
---
# Azure Blob Download Task
The Azure Blob Download Task enables an SSIS package to download files from an Azure blob storage.

To add an **Azure Blob Download Task**, drag-drop it to the SSIS Designer, and double-click or right-click and click **Edit** to see the following **Azure Blob Download Task Editor** dialog box.  
  
 The **Azure Blob Download Task** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).  
  
 The following table provides description for the fields in this dialog box.  
  
|||  
|-|-|  
|**Field**|**Description**|  
|AzureStorageConnection|Specify an existing Azure Storage Connection Manager or create a new one that refers to an Azure Storage Account, which points to where the blob files are hosted.|  
|BlobContainer|Specifies the name of the blob container that contains the blob files to be downloaded.|  
|BlobDirectory|Specifies the blob directory that contains the blob files to be downloaded. The blob directory is a virtual hierarchical structure.|  
|LocalDirectory|Specifies the local directory where the downloaded blob files are stored.|  
|FileName|Specifies a name filter to select files with the specified name pattern. For example, `MySheet*.xls\*` includes files such as `MySheet001.xls` and `MySheetABC.xlsx`.|  
|TimeRangeFrom/TimeRangeTo|Specifies a time range filter. Files modified after **TimeRangeFrom** and before **TimeRangeTo** are included.|  
  
  
