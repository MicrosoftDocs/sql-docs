---
title: "Azure Data Lake Store Source | Microsoft Docs"
ms.custom: ""
ms.date: "06/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.DTS.DESIGNER.AFPADLSSRC.F1"
  - "SQL11.DTS.DESIGNER.AFPADLSSRC.F1"
ms.assetid: 7d9e8457-62aa-4aea-98ee-7d1121dfae4f
author: yualan
ms.author: douglasl
manager: craigg
---
# Azure Data Lake Store Source
  The **Azure Data Lake Store Source** component enables an SSIS package to read data from an Azure Data Lake Store. The supported file formats are: Text and Avro.
  
## Configure the Azure Data Lake Store Source 
  
1.  To see the editor for the Azure Data Lake Store Source, drag and drop **Azure Data Lake Store Source** on the data flow designer and double-click it to open the editor).  
  
2.  For the **Azure Data Lake Store connection manager** field, specify an existing Azure Data Lake Store Connection Manager or create a new one that refers to an Azure Data Lake Store service.  
  
    1.  For the **File Path** field, specify the file path of the source file in Azure Data Lake Store.   
  
    2.  For the **File format** field, specify the file format of the source file.  
  
        If the file format is Text, you must specify the **Column delimiter character** value. Also select **Column names in the first data row** if the first row in the file contains column names.  
  
3.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.  
