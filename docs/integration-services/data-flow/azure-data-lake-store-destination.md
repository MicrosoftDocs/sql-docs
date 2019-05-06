---
title: "Azure Data Lake Store Destination | Microsoft Docs"
ms.custom: ""
ms.date: 01/10/2019
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPADLSDEST.F1"
  - "sql14.dts.designer.afpadlsdest.f1"
ms.assetid: 4c4f504f-dd2b-42c5-8a20-1a8ad9a5d632
author: janinezhang
ms.author: janinez
manager: craigg
---
# Azure Data Lake Store Destination
  The **Azure Data Lake Store Destination** component enables an SSIS package to write data to an Azure Data Lake Store. The supported file formats are: Text, Avro, and ORC. 
  
 The **Azure Data Lake Store Destination** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).
 
> [!NOTE]
> To ensure that the Azure Data Lake Store Connection Manager and the components that use it - that is, the Azure Data Lake Store Source and the Azure Data Lake Store Destination - can connect to services, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). 

## Configure the Azure Data Lake Store Destination  
1. Drag-drop **Azure Data Lake Store Destination** to the data flow designer and double-click it to see the editor.  

2.  For the **Azure Data Lake Store connection manager** field, specify an existing Azure Data Lake Store Connection Manager or create a new one that refers to an Azure Data Lake Store service.  
  
    1.  For the **File Path** field, specify the file name you want to write data to. If this file already exists, its contents are overwritten.  
  
    2.  For the **File format** field, specify the file format you want to use.  
  
       If the file format is Text, you must specify the **Column delimiter character** value. Also  select **Column names in the first data row** if the first row in the file contains column names.  

       If the file format is ORC, you need to install the Java Runtime Environment (JRE) for the appropriate platform.
  
3.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.  
