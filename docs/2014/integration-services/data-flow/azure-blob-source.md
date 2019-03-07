---
title: "Azure Blob Source | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql12.dts.designer.afpblobsrc.f1"
  - "sql11.dts.designer.afpblobsrc.f1"
ms.assetid: 80645c5c-88c8-4fb0-8607-de1bb7bffcbb
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Azure Blob Source
 The **Azure Blob Source** component enables an SSIS package to read data from an Azure blob. The supported file formats are: CSV and AVRO. To see the editor for the Azure Blob Source, drag and drop **Azure Blob Source** on the data flow designer and double-click it to open the editor).  
  
1.  For the **Azure storage connection manager** field, specify an existing Azure Storage Connection Manager or create a new one that refers to an Azure Storage Account.  
  
2.  For the **Blob container name** field, specify the name of the blob container that contains source files.  
  
3.  For the **Blob name** field, specify the path for the blob.  
  
4.  For the **Blob file format** field, specify the blob format you want to use.  
  
5.  If the file format is CSV, you must specify the **Column delimiter character** value. Also select **Column names in the first data row** if the first row in the file contains column names.  
  
6.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.  
  
  
