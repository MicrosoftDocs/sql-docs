---
title: "Azure Data Lake Store Source | Microsoft Docs"
ms.custom: ""
ms.date: "08/16/2018"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPADLSSRC.F1"
  - "sql14.dts.designer.afpadlssrc.f1"
ms.assetid: f9c3311f-7316-48d6-bf10-d810e70b4304
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Azure Data Lake Store Source
  The **Azure Data Lake Store Source** component enables an SSIS package to read data from an Azure Data Lake Store. The supported file formats are: Text and Avro.
  
 The **Azure Data Lake Store Source** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).  
  
> [!NOTE]
> To ensure that the Azure Data Lake Store Connection Manager and the components that use it - that is, the Azure Data Lake Store Source and the Azure Data Lake Store Destination - can connect to services, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). 
  
## Configure the Azure Data Lake Store Source
 1. To see the editor for the Azure Data Lake Store Source, drag and drop **Azure Data Lake Store Source** on the data flow designer and double-click it to open the editor).  
  
2.  For the **Azure Data Lake Store connection manager** field, specify an existing Azure Data Lake Store Connection Manager or create a new one that refers to an Azure Data Lake Store service.  
  
    1.  For the **File Path** field, specify the file path of the source file in Azure Data Lake Store.   
  
    2.  For the **File format** field, specify the file format of the source file.  
  
        If the file format is Text, you must specify the **Column delimiter character** value. Also select **Column names in the first data row** if the first row in the file contains column names.  
  
3.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.   

## Text qualifier

The **Azure Data Lake Store Source** does not support a text qualifier. If you have to specify a text qualifier to process your files correctly, consider downloading the files to your local computer and processing the files with the **Flat File Source**. The Flat File Source lets you specify a text qualifier. For more info, see [Flat File Source](flat-file-source.md).
