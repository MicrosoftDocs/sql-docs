---
description: "Flexible File Task"
title: "Flexible File Task | Microsoft Docs"
ms.custom: ""
ms.date: "05/22/2019"
ms.service: sql
ms.reviewer: ""
ms.subservice: integration-services
ms.topic: conceptual
f1_keywords: 
  - "SQL13.DTS.DESIGNER.AFPEXTFILETASK.F1"
  - "SQL14.DTS.DESIGNER.AFPEXTFILETASK.F1"
author: chugugrace
ms.author: chugu
---
# Flexible File Task

[!INCLUDE[sqlserver-ssis](../../includes/applies-to-version/sqlserver-ssis.md)]

The Flexible File Task enables users to perform file operations on various supported storage services.
Currently supported storage services are

- Local File System
- [Azure Blob Storage](https://azure.microsoft.com/services/storage/blobs/)
- [Azure Data Lake Storage Gen2](/azure/storage/blobs/data-lake-storage-introduction)

The Flexible File Task is a component of the [SQL Server Integration Services (SSIS) Feature Pack for Azure](../../integration-services/azure-feature-pack-for-integration-services-ssis.md).

To add a Flexible File Task to a package, drag it from SSIS Toolbox to the designer canvas. Then double-click the task, or right-click the task and select **Edit**, to open the **Flexible File Task Editor** dialog box.

The **Operation** property specifies the file operation to perform.
Currently supported operations are:
- **Copy** Operation
- **Delete** Operation

For **Copy** operation, following properties are available.

- **SourceConnectionType:** Specifies the source connection manager type.
- **SourceConnection:** Specifies the source connection manager.
- **SourceFolderPath:** Specifies the source folder path.
- **SourceFileName:** Specifies the source file name. If left blank, the source folder will be copied. Following wildcards are allowed in source file name: `*` (matches zero or more characters), `?` (matches zero or single character) and `^` (escape character).
- **SearchRecursively:** Specifies whether to recursively copy subfolders.
- **DestinationConnectionType:** Specifies the destination connection manager type.
- **DestinationConnection:** Specifies the destination connection manager.
- **DestinationFolderPath:** Specifies the destination folder path.
- **DestinationFileName:** Specifies the destination file name. If left blank, the source file names will be used.

For **Delete** operation, following properties are available.
- **ConnectionType:** Specifies the connection manager type.
- **Connection:** Specifies the connection manager.
- **FolderPath:** Specifies the folder path.
- **FileName:** Specifies the file name. If left blank, the folder will be deleted. For Azure Blob Storage, delete folder is not supported. Following wildcards are allowed in file name: `*` (matches zero or more characters), `?` (matches zero or single character) and `^` (escape character).
- **DeleteRecursively:** Specifies whether to recusively delete files.

***Notes on Service Principal Permission Configuration***

For **Test Connection** to work (either blob storage or Data Lake Storage Gen2), the service principal should be assigned at least **Storage Blob Data Reader** role to the storage account.
This is done with [RBAC](/azure/storage/common/storage-auth-aad-rbac-portal#assign-rbac-roles-using-the-azure-portal).

For blob storage, read and write permissions are granted by assigning at least **Storage Blob Data Reader** and **Storage Blob Data Contributor** roles, respectively.

For Data Lake Storage Gen2, permission is determined by both RBAC and [ACLs](/azure/storage/blobs/data-lake-storage-how-to-set-permissions-storage-explorer).
Pay attention that ACLs are configured using the Object ID (OID) of the service principal for the app registration as detailed [here](/azure/storage/blobs/data-lake-storage-access-control#how-do-i-set-acls-correctly-for-a-service-principal).
This is different from the Application (client) ID that is used with RBAC configuration.
When a security principal is granted RBAC data permissions through a built-in role, or through a custom role, these permissions are evaluated first upon authorization of a request.
If the requested operation is authorized by the security principal's RBAC assignments, then authorization is immediately resolved and no additional ACL checks are performed.
Alternatively, if the security principal does not have an RBAC assignment, or the request's operation does not match the assigned permission, then ACL checks are performed to determine if the security principal is authorized to perform the requested operation.

- For read permission, grant at least **Execute** permission starting from the source file system, along with **Read** permission for the files to copy. Alternatively, grant at least the **Storage Blob Data Reader** role with RBAC.
- For write permission, grant at least **Execute** permission starting from the sink file system, along with **Write** permission for the sink folder. Alternatively, grant at least the **Storage Blob Data Contributor** role with RBAC.

See [this](/azure/storage/blobs/data-lake-storage-access-control) article for details.