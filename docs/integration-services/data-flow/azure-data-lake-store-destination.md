---
title: "Azure Data Lake Store Destination | Microsoft Docs"
ms.custom: ""
ms.date: 01/09/2019
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPADLSDEST.F1"
  - "sql14.dts.designer.afpadlsdest.f1"
ms.assetid: 4c4f504f-dd2b-42c5-8a20-1a8ad9a5d632
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# Azure Data Lake Store Destination
  The **Azure Data Lake Store Destination** component enables an SSIS package to write data to an Azure Data Lake Store. The supported file formats are: Text, Avro and ORC. 
  
 The **Azure Data Lake Store Destination** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).
 
> [!NOTE]
> To ensure that the Azure Data Lake Store Connection Manager and the components that use it - that is, the Azure Data Lake Store Source and the Azure Data Lake Store Destination - can connect to services, make sure you download the latest version of the Azure Feature Pack [here](https://www.microsoft.com/download/details.aspx?id=49492). 

## Configure the Azure Data Lake Store Destination  
1. Drag-drop **Azure Data Lake Store Destination** to the data flow designer and double-click it to see the editor.  

2.  For the **Azure Data Lake Store connection manager** field, specify an existing Azure Data Lake Store Connection Manager or create a new one that refers to an Azure Data Lake Store service.  
  
    1.  For the **File Path** field, specify the file name you want to write data to. If this file already exist, its content will be overwritten.  
  
    2.  For the **File format** field, specify the file format you want to use.  
  
       If the file format is Text, you must specify the **Column delimiter character** value. Also  select **Column names in the first data row** if the first row in the file contains column names.  

       If the file format is ORC, you need to install the Java Runtime Environment (JRE) for the appropriate platform.
  
3.  After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.  

## Prerequisite for ORC format

Before you can use the ORC file format, you have to install the Java Runtime Environment with version 1.7.51  or higher for the appropriate platform.

Both the Zulu and the Oracle JRE are supported.
-   [Zulu JRE](https://www.azul.com/downloads/zulu/zulu-windows/)
-   [Oracle JRE](https://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html)

### Set up the Zulu JRE

1. Download and extract the Zulu OpenJDK installation zip package.

2.	From the Command Prompt, run `sysdm.cpl`.

3. On the **Advanced** tab, select **Environment Variables**.

4. Under the **System variables** section, select **New**.

5. Enter `JAVA_HOME` for the **Variable name**.

6. Select **Browse Directory**, navigate to the Zulu OpenJDK installation folder, and select the `jre` subfolder. Then select OK. The variable value is populated automatically.

7. Select **OK** to close the **New System Variable** dialog box.

8. Select **OK** to close the Environment Variables dialog box.

### Set up the Oracle JRE

1. Download and run the Oracle JRE exe installer.

1. Follow the installer instructions to complete setup.
