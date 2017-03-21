---
title: "Azure SQL DW Upload Task | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "integration-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPDWUPTASK.F1"
  - "sql14.dts.designer.afpdwuptask.f1"
ms.assetid: eef82c89-228a-4dc7-9bd0-ea00f57692f5
caps.latest.revision: 5
author: "Lingxi-Li"
ms.author: "lingxl"
manager: "jhubbard"
---
# Azure SQL DW Upload Task
The **Azure SQL DW Upload Task** enables an SSIS package to upload local data to a table in Azure SQL Data Warehouse (DW). The currently supported source data file format is delimited text in UTF8 encoding. The uploading process follows the efficient PolyBase approach as described in the article [Azure SQL Data Warehouse Loading Patterns and Strategies](https://blogs.msdn.microsoft.com/sqlcat/2016/02/06/azure-sql-data-warehouse-loading-patterns-and-strategies/). Specifically, data will first be uploaded to Azure Blob Storage, and then to Azure SQL DW. Therefore, an Azure Blob Storage account is needed to use this task.

The **Azure SQL DW Upload Task** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

To add an **Azure SQL DW Upload Task**, drag-drop it from SSIS Toolbox to the designer canvas, and double-click or right-click and click **Edit** to see the task editor dialog box.

On the **General** page, configure the following properties.

Field|Description
-----|-----------
LocalDirectory|Specifies the local directory that contains the data files to be uploaded.
Recursively|Specifies whether to recursively search sub-directories.
FileName|Specifies a name filter to select files with certain name pattern. E.g. MySheet*.xsl\* will include files such as MySheet001.xsl and MySheetABC.xslx.
RowDelimiter|Specifies the character(s) that marks the end of each row.
ColumnDelimiter|Specifies one or more characters that mark the end of each column. E.g. &#124; (pipe), \t (tab), ' (single quote), " (double quote), and 0x5c (backslash).
IsFirstRowHeader|Specifies whether the first row in each data file contains column names instead of actual data.
AzureStorageConnection|Specifies an Azure Storage connection manager.
BlobContainer|Specifies the name of blob container to which local data will be uploaded and relayed to Azure DW via PolyBase. A new container will be created if it does not exist.
BlobDirectory|Specifies the blob directory (virtual hierarchical structure) to which local data will be uploaded and relayed to Azure DW via PolyBase.
RetainFiles|Specifies whether to retain the files uploaded to Azure Storage.
CompressionType|Specifies the compression format to use upon uploading files to Azure Storage. Local source is not affected.
CompressionLevel|Specifies the compression level to use for the compression format.
AzureDwConnection|Specifies an ADO.NET connection manager for Azure SQL DW.
TableName|Specifies name of the destination table. Either choose an existing table name, or create a new one by choosing **\<New Table ...>**.
TableDistribution|Specifies the distribution method for new table. Applies if a new table name is specified for **TableName**.
HashColumnName|Specifies the column used for hash table distribution. Applies if **HASH** is specified for **TableDistribution**.

You will see a different **Mappings** page depending on whether you are uploading to a new table or to an existing one. In the former case, configure which source columns are to be mapped and their corresponding names in the to-be-created destination table. In the latter case, configure the mapping relationships between source and destination columns.

On the **Columns** page, configure the data type properties for each source column.

The **T-SQL** page shows the T-SQL used to load data from Azure Blob Storage to Azure SQL DW. The T-SQL is automatically generated from configurations on the other pages, and will be executed as part of the task execution. You may choose to manually edit the generated T-SQL to meet your particular needs by clicking the **Edit** button. You can revert to the automatically generated one later on by clicking the **Reset** button.

