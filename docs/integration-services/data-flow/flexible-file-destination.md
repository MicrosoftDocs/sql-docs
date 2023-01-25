---
description: "Flexible File Destination"
title: "Flexible File Destination | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.afpextfiledest.f1"
  - "sql14.dts.designer.afpextfiledest.f1"
author: chugugrace
ms.author: chugu
---
# Flexible File Destination

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

The **Flexible File Destination** component enables an SSIS package to write data to various supported storage services.

Currently supported storage services are

- [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs/)
- [Azure Data Lake Storage Gen2](/azure/storage/blobs/data-lake-storage-introduction)
   
Drag-drop **Flexible File Destination** to the data flow designer and double-click it to see the editor.
  
The **Flexible File Destination** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).  

Following properties are available on the **Flexible File Destination Editor**.

- **File Connection Manager Type:** Specifies the source connection manager type. Then choose an existing one of the specified type or create a new one.
- **Folder Path:** Specifies the destination folder path.
- **File Name:** Specifies the destination file name.
- **File Format:** Specifies the destination file format. Supported formats are **Text**, **Avro**, **ORC**, **Parquet**. Java is required for ORC/Parquet. See [here](../../integration-services/azure-feature-pack-for-integration-services-ssis.md#dependency-on-java) for details.
- **Column delimiter character:** Specifies the character to use as column delimiter (multi-character delimiters are not supported).
- **First row as the column name:** Specifies whether to write column names to first row.
- **Compress the file:** Specifies whether to compress the file.
- **Compression Type:** Specifies the compression format to use. Supported formats are **GZIP**, **DEFLATE**, **BZIP2**.
- **Compression Level:** Specifies the compression level to use.

Following properties are available on the **Advanced Editor**.

- **rowDelimiter:** The character used to separate rows in a file. Only one character is allowed. The **default** value is \r\n.
- **escapeChar:** The special character used to escape a column delimiter in the content of input file. You cannot specify both escapeChar and quoteChar for a table. Only one character is allowed. No default value.
- **quoteChar:** The character used to quote a string value. The column and row delimiters inside the quote characters would be treated as part of the string value. This property is applicable to both input and output datasets. You cannot specify both escapeChar and quoteChar for a table. Only one character is allowed. No default value.
- **nullValue:** One or more characters used to represent a null value. The **default** value is \N.
- **encodingName:** Specify the encoding name. See [Encoding.EncodingName](/dotnet/api/system.text.encoding) Property.
- **skipLineCount:**  Indicates the number of non-empty rows to skip when reading data from input files. If both skipLineCount and firstRowAsHeader are specified, the lines are skipped first and then the header information is read from the input file.
- **treatEmptyAsNull:** Specifies whether to treat null or empty string as a null value when reading data from an input file. The **default** value is True.

After specifying the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.

**Notes on Service Principal Permission Configuration**

For **Test Connection** to work (either blob storage or Data Lake Storage Gen2), the service principal should be assigned at least **Storage Blob Data Reader** role to the storage account.
This is done with [RBAC](/azure/storage/common/storage-auth-aad-rbac-portal#assign-rbac-roles-using-the-azure-portal).

For blob storage, write permission is granted by assigning at least **Storage Blob Data Contributor** role.

For Data Lake Storage Gen2, permission is determined by both RBAC and [ACLs](/azure/storage/blobs/data-lake-storage-how-to-set-permissions-storage-explorer).
Pay attention that ACLs are configured using the Object ID (OID) of the service principal for the app registration as detailed [here](/azure/storage/blobs/data-lake-storage-access-control#how-do-i-set-acls-correctly-for-a-service-principal).
This is different from the Application (client) ID that is used with RBAC configuration.
When a security principal is granted RBAC data permissions through a built-in role, or through a custom role, these permissions are evaluated first upon authorization of a request.
If the requested operation is authorized by the security principal's RBAC assignments, then authorization is immediately resolved and no additional ACL checks are performed.
Alternatively, if the security principal does not have an RBAC assignment, or the request's operation does not match the assigned permission, then ACL checks are performed to determine if the security principal is authorized to perform the requested operation.
For write permission, grant at least **Execute** permission starting from the sink file system, along with **Write** permission for the sink folder.
Alternatively, grant at least the **Storage Blob Data Contributor** role with RBAC.
See [this](/azure/storage/blobs/data-lake-storage-access-control) article for details.