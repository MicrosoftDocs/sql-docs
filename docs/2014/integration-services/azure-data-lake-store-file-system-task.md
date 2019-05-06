---
title: "Azure Data Lake Store File System Task | Microsoft Docs"
ms.custom: ""
ms.date: 01/09/2019
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "integration-services"
ms.topic: conceptual
f1_keywords: 
  - "SQL12.DTS.DESIGNER.AFPADLSTASK.F1"
  - "SQL11.DTS.DESIGNER.AFPADLSTASK.F1"
ms.assetid: 02b9edd7-6ef9-463e-abbf-e1830bcae875
author: janinezhang
ms.author: janinez
manager: craigg
---
# Azure Data Lake Store File System Task

The **Azure Data Lake Store File System Task** enables users to perform various file system operations on [Azure Data Lake Store (ADLS)](https://azure.microsoft.com/services/data-lake-store/).

To add an Azure Data Lake Store File System Task to a package, drag it from SSIS Toolbox to the designer canvas. Then double-click the task, or right-click the task and select **Edit**, to open the task editor dialog box.

The **Operation** property specifies the file system operation to perform. The following operations are supported.

* **CopyToADLS:** Upload files to ADLS.
* **CopyFromADLS:** Download files from ADLS.

For any operation, you have to specify an Azure Data Lake connection manager.

Here are the descriptions of the properties specific to each operation.

## CopyToADLS

* **LocalDirectory:** Specifies the source directory which contains files to upload.
* **FileNamePattern:** Specifies a file name filter for source files. Only files whose name matches the specified pattern will be uploaded. Wildcards `*` and `?` are supported.
* **SearchRecursively:** Specifies whether to search recursively within the source directory for files to upload.
* **AzureDataLakeDirectory:** Specifies the ADLS destination directory to upload files to.
* **FileExpiry:** Specifies an expiration date and time for the files uploaded to ADLS, or leave this property blank to indicate that the files never expire.

## CopyFromADLS

* **AzureDataLakeDirectory:** Specifies the ADLS source directory which contains the files to download.
* **SearchRecursively:** Specifies whether to search recursively within the source directory for files to download.
* **LocalDirectory:** Specifies the destination directory to store downloaded files.
