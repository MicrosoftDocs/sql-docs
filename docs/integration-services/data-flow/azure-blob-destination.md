---
title: "Azure Blob Destination | Microsoft Docs"
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
  - "sql13.dts.designer.afpblobdest.f1"
  - "sql14.dts.designer.afpblobdest.f1"
ms.assetid: 820a1e7a-7182-4c7b-ab56-5b4097a7e042
caps.latest.revision: 12
author: "douglaslMS"
ms.author: "douglasl"
manager: "jhubbard"
---
# Azure Blob Destination
  The **Azure Blob Destination** component enables an SSIS package to write data to an Azure blob. The supported file formats are: CSV and AVRO. 
  
>   [!NOTE]
> To ensure that the Azure Storage Connection Manager and the components that use it - that is, the Blob Source, the Blob Destination, the Blob Upload Task, and the Blob Download Task - can connect to both general-purpose storage accounts and to blob storage accounts, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). For more info about these two types of storage accounts, see [Introduction to Microsoft Azure Storage](https://azure.microsoft.com/en-us/documentation/articles/storage-introduction/#general-purpose-storage-accounts).
  
 Drag-drop **Azure Blob Destination** to the data flow designer and double-click it to see the editor).  
  
 The **Azure Blob Destination** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).  
  
1.  For the **Azure storage connection manager** field, specify an existing Azure Storage Connection Manager or create a new one that refers to an Azure Storage Account.  
  
2.  For the **Blob container name** field, specify the name of the blob container that contains source files.  
  
3.  For the **Blob name** field, specify the path for the blob.  
  
4.  For the **Blob file format** field, specify the blob format you want to use.  
  
5.  If the file format is CSV, you must specify the **Column delimiter character** value. Also  select **Column names in the first data row** if the first row in the file contains column names.  
  
6.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.  
  
  
