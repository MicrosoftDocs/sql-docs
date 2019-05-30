---
title: "Flexible File Task | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPEXTFILETASK.F1"
  - "SQL14.DTS.DESIGNER.AFPEXTFILETASK.F1"
author: janinezhang
ms.author: janinez
manager: craigg
---
# Flexible File Task

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]

The Flexible File Task enables users to perform file operations on various supported storage services.
Currently supported storage services are

- Local File System
- [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs/)
- [Azure Data Lake Storage Gen2](https://docs.microsoft.com/azure/storage/blobs/data-lake-storage-introduction)

The Flexible File Task is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

To add a Flexible File Task to a package, drag it from SSIS Toolbox to the designer canvas. Then double-click the task, or right-click the task and select **Edit**, to open the **Flexible File Task Editor** dialog box.

The **Operation** property specifies the file operation to perform.
Only **Copy** operation is currently supported.

For **Copy** operation, following properties are available.

- **SourceConnectionType:** Specifies the source connection manager type.
- **SourceConnection:** Specifies the source connection manager.
- **SourceFolderPath:** Specifies the source folder path.
- **SourceFileName:** Specifies the source file name. If left blank, the source folder will be copied.
- **SearchRecursively:** Specifies whether to recursively copy sub-folders.
- **DestinationConnectionType:** Specifies the destination connection manager type.
- **DestinationConnection:** Specifies the destination connection manager.
- **DestinationFolderPath:** Specifies the destination folder path.
- **DestinationFileName:** Specifies the destination file name.
