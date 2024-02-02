---
title: "Azure Blob Destination"
description: "Azure Blob Destination"
author: chugugrace
ms.author: chugu
ms.date: "07/25/2016"
ms.service: sql
ms.subservice: integration-services
ms.topic: how-to
f1_keywords:
  - "sql13.dts.designer.afpblobdest.f1"
  - "sql14.dts.designer.afpblobdest.f1"
---
# Azure Blob Destination

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]


 The **Azure Blob Destination** component enables an SSIS package to write data to an Azure blob. The supported file formats are: CSV and AVRO. 
   
 Drag-drop **Azure Blob Destination** to the data flow designer and double-click it to see the editor).  
  
 The **Azure Blob Destination** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).  
  
1.  For the **Azure storage connection manager** field, specify an existing Azure Storage Connection Manager or create a new one that refers to an Azure Storage Account.  
  
2.  For the **Blob container name** field, specify the name of the blob container that contains source files.  
  
3.  For the **Blob name** field, specify the path for the blob.  
  
4.  For the **Blob file format** field, specify the blob format you want to use.  
  
5.  If the file format is CSV, you must specify the **Column delimiter character** value. Also  select **Column names in the first data row** if the first row in the file contains column names.  
  
6.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.  
  
  
