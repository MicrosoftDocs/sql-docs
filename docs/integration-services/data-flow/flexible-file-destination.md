---
title: "Flexible File Destination | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2019"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.afpextfiledest.f1"
  - "sql14.dts.designer.afpextfiledest.f1"
author: janinezhang
ms.author: janinez
manager: craigg
---
# Flexible File Destination

[!INCLUDE[ssis-appliesto](../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]

The **Flexible File Destination** component enables an SSIS package to write data to various supported storage services.

Currently supported storage services are

- [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs/)
- [Azure Data Lake Storage Gen2](https://docs.microsoft.com/azure/storage/blobs/data-lake-storage-introduction)
   
Drag-drop **Flexible File Destination** to the data flow designer and double-click it to see the editor.
  
The **Flexible File Destination** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).  

Following properties are available.

- **File Connection Manager Type:** Specifies the source connection manager type. Then choose an existing one of the specified type or create a new one.
- **Folder Path:** Specifies the destination folder path.
- **File Name:** Specifies the destination file name.
- **File Format:** Specifies the destination file format. Supported formats are **Text**, **Avro**, **ORC**, **Parquet**.
- **Column delimiter character:** Specifies the character to use as column delimiter (multi-character delimiters are not supported).
- **First row as the column name:** Specifies whether to write column names to first row.
- **Compress the file:** Specifies whether to compress the file.
- **Compression Type:** Specifies the compression format to use. Supported formats are **GZIP**, **DEFLATE**, **BZIP2**.
- **Compression Level:** Specifies the compression level to use.
  
After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.

**Prerequisite for ORC/Parquet File Format**

Java is required to use ORC/Parquet file format.
Architecture (32/64-bit) of the Java build should match that of the SSIS runtime to use.
The following Java builds have been tested.

- [Zulu's OpenJDK 8u192](https://www.azul.com/downloads/zulu/zulu-windows/)
- [Oracle's Java SE Runtime Environment 8u192](https://www.oracle.com/technetwork/java/javase/downloads/java-archive-javase8-2177648.html)

**Set Up Zulu's OpenJDK**

1. Download and extract the installation zip package.
2. From the Command Prompt, run `sysdm.cpl`.
3. On the **Advanced** tab, select **Environment Variables**.
4. Under the **System variables** section, select **New**.
5. Enter `JAVA_HOME` for the **Variable name**.
6. Select **Browse Directory**, navigate to the extracted folder, and select the `jre` subfolder.
   Then select **OK**, and the **Variable value** is populated automatically.
7. Select **OK** to close the **New System Variable** dialog box.
8. Select **OK** to close the **Environment Variables** dialog box.
9. Select **OK** to close the **System Properties** dialog box.

**Set Up Oracle's Java SE Runtime Environment**

1. Download and run the exe installer.
2. Follow the installer instructions to complete setup.
