---
title: "Azure Data Lake Store Destination | Microsoft Docs"
ms.custom: ""
ms.date: "06/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.DTS.DESIGNER.AFPADLSDEST.F1"
  - "SQL11.DTS.DESIGNER.AFPADLSDEST.F1"
ms.assetid: d0e86032-2a6b-48b2-898f-e94328474fde
author: yualan
ms.author: douglasl
manager: craigg
---
# Azure Data Lake Store Destination
  The **Azure Data Lake Store Destination** component enables an SSIS package to write data to an Azure Data Lake Store. The supported file formats are: Text, Avro and ORC. 
  
## Configure the Azure Data Lake Store Destination 

1. Drag-drop **Azure Data Lake Store Destination** to the data flow designer and double-click it to see the editor.  

2.  For the **Azure Data Lake Store connection manager** field, specify an existing Azure Data Lake Store Connection Manager or create a new one that refers to an Azure Data Lake Store service.  
  
    1.  For the **File Path** field, specify the file name you want to write data to. If this file already exist, its content will be overwritten.  
  
    2.  For the **File format** field, specify the file format you want to use.  
  
        If the file format is Text, you must specify the **Column delimiter character** value. Also  select **Column names in the first data row** if the first row in the file contains column names.  

        If the file format is ORC, you need to install JRE of corresponding platform. 
  
3.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.  
