---
description: "Flexible File Source"
title: "Flexible File Source | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "sql13.dts.designer.afpextfilesrc.f1"
  - "sql14.dts.designer.afpextfilesrc.f1"
author: chugugrace
ms.author: chugu
---
# Flexible File Source

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

The **Flexible File Source** component enables an SSIS package to read data from various supported storage services.
Currently supported storage services are

- [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs/)
- [Azure Data Lake Storage Gen2](/azure/storage/blobs/data-lake-storage-introduction)
  
To see the editor for the Flexible File Source, drag and drop **Flexible File Source** on the data flow designer and double-click it to open the editor.
  
The **Flexible File Source** is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).  
  
Following properties are available on the **Flexible File Source Editor**.

- **File Connection Manager Type:** Specifies the source connection manager type. Then choose an existing one of the specified type or create a new one.
- **Folder Path:** Specifies the source folder path.
- **File Name:** Specifies the source file name.
- **File Format:** Specifies the source file format. Supported formats are **Text**, **Avro**, **ORC**, **Parquet**. Java is required for ORC/Parquet. See [here](../../integration-services/azure-feature-pack-for-integration-services-ssis.md#dependency-on-java) for details.
- **Column delimiter character:** Specifies the character used as column delimiter (multi-character delimiters are not supported).
- **First row as the column name:** Specifies whether to treat first row as column names.
- **Decompress the file:** Specifies whether to decompress the source file.
- **Compression Type:** Specifies the source file compression format. Supported formats are **GZIP**, **DEFLATE**, **BZIP2**.
  
Following properties are available on the **Advanced Editor**.

- **rowDelimiter:** The character used to separate rows in a file. Only one character is allowed. The **default** value is \r\n.
- **escapeChar:** The special character used to escape a column delimiter in the content of input file. You cannot specify both escapeChar and quoteChar for a table. Only one character is allowed. No default value.
- **quoteChar:** The character used to quote a string value. The column and row delimiters inside the quote characters would be treated as part of the string value. This property is applicable to both input and output datasets. You cannot specify both escapeChar and quoteChar for a table. Only one character is allowed. No default value.
- **nullValue:** One or more characters used to represent a null value. The **default** value is \N.
- **encodingName:** Specify the encoding name. See [Encoding.EncodingName](/dotnet/api/system.text.encoding) Property.
- **skipLineCount:**  Indicates the number of non-empty rows to skip when reading data from input files. If both skipLineCount and firstRowAsHeader are specified, the lines are skipped first and then the header information is read from the input file.
- **treatEmptyAsNull:** Specifies whether to treat null or empty string as a null value when reading data from an input file. The **default** value is True.

After you specify the connection information, switch to the **Columns** page to map source columns to destination columns for the SSIS data flow.

**Notes on Service Principal Permission Configuration**

For **Test Connection** to work (either blob storage or Data Lake Storage Gen2), the service principal should be assigned at least **Storage Blob Data Reader** role to the storage account.
This is done with [RBAC](/azure/storage/common/storage-auth-aad-rbac-portal#assign-rbac-roles-using-the-azure-portal).

For blob storage, read permission is granted by assigning at least **Storage Blob Data Reader** role.

For Data Lake Storage Gen2, permission is determined by both RBAC and [ACLs](/azure/storage/blobs/data-lake-storage-how-to-set-permissions-storage-explorer).
Pay attention that ACLs are configured using the Object ID (OID) of the service principal for the app registration as detailed [here](/azure/storage/blobs/data-lake-storage-access-control#how-do-i-set-acls-correctly-for-a-service-principal).
This is different from the Application (client) ID that is used with RBAC configuration.
When a security principal is granted RBAC data permissions through a built-in role, or through a custom role, these permissions are evaluated first upon authorization of a request.
If the requested operation is authorized by the security principal's RBAC assignments, then authorization is immediately resolved and no additional ACL checks are performed.
Alternatively, if the security principal does not have an RBAC assignment, or the request's operation does not match the assigned permission, then ACL checks are performed to determine if the security principal is authorized to perform the requested operation.
For read permission, grant at least **Execute** permission starting from the source file system, along with **Read** permission for the files to read.
Alternatively, grant at least the **Storage Blob Data Reader** role with RBAC.
See [this](/azure/storage/blobs/data-lake-storage-access-control) article for details.