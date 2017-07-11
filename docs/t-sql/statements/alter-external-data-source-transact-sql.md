---
title: "ALTER EXTERNAL DATA SOURCE (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "language-reference"
f1_keywords: 
  - "ALTER EXTERNAL DATA SOURCE"
  - "ALTER_EXTERNAL_DATA_SOURCE"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "polybase, alter external data source statement"
  - "ALTER EXTERNAL DATA SOURCE statement"
ms.assetid: a34b9e90-199d-46d0-817a-a7e69387bf5f
caps.latest.revision: 8
author: "JennieHubbard"
ms.author: "jhubbard"
manager: "jhubbard"
---
# ALTER EXTERNAL DATA SOURCE (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Modifies an external data source used to create an external table. The external data source can be Hadoop or Azure blob storage (WASB).  
  
## Syntax  
  
```  
--Modify an external data source   
ALTER EXTERNAL DATA SOURCE data_source_name SET  
    {   
        LOCATION = 'server_name_or_IP' [,] |  
        RESOURCE_MANAGER_LOCATION = <'IP address;Port'> [,] |  
        CREDENTIAL = credential_name  
    }  
    [;]  

-- Modify an external data source pointing to Azure Blob storage
ALTER EXTERNAL DATA SOURCE data_source_name  
    WITH (   
        TYPE = BLOB_STORAGE,  
        LOCATION = 'https://storage_account_name.blob.core.windows.net'
        [, CREDENTIAL = credential_name ]
    )  
```  
  
## Arguments  
 data_source_name  
 Specifies the user-defined name for the data source. The name must be unique.  
  
 LOCATION = ‘server_name_or_IP’  
 Specifies the name of the server or an IP address.  
  
 RESOURCE_MANAGER_LOCATION = ‘\<IP address;Port>’  
 Specifies the Hadoop resource manager location. When specified, the query optimizer might choose to pre-process data for a PolyBase query by using Hadoop’s computation capabilities. This is a cost-based decision. Called predicate pushdown, this can significantly reduce the volume of data transferred between Hadoop and SQL, and therefore improve query performance.  
  
 CREDENTIAL = Credential_Name  
 Specifies the named credential. See  [CREATE DATABASE SCOPED CREDENTIAL &#40;Transact-SQL&#41;](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).  

TYPE = BLOB_STORAGE   
**Applies to:** [!INCLUDE[ssSQLv14_md](../../includes/sssqlv14-md.md)].   
For bulk operations only, `LOCATION` must be valid the URL to Azure Blob storage. Do not put **/**, file name, or shared access signature parameters at the end of the `LOCATION` URL.   
The credential used, must be created using `SHARED ACCESS SIGNATURE` as the identity. For more information on shared access signatures, see [Using Shared Access Signatures (SAS)](https://docs.microsoft.com/azure/storage/storage-dotnet-shared-access-signature-part-1).

  
  
## Remarks  
 Only single source can be modified at a time. Concurrent requests to modify the same source cause one statement to wait. However, different sources can be modified at the same time. Also, this this statement can run concurrently with other statements.  
  
## Permissions  
 Requires ALTER ANY EXTERNAL DATA SOURCE permission.  
 > [!IMPORTANT]  
 >  The ALTER ANY EXTERNAL DATA SOURCE  permission grants any principal the ability to create and modify any external data source object, and therefore, it also grants the ability to access all database scoped credentials on the database. This permission must be considered as highly privileged, and therefore must be granted only to trusted principals in the system.

  
## Examples  
 The following example alters the location and resource manager location of an existing data source.  
  
```  
ALTER EXTERNAL DATA SOURCE hadoop_eds SET  
     LOCATION = 'hdfs://10.10.10.10:8020',  
     RESOURCE_MANAGER_LOCATION = '10.10.10.10:8032'  
    ;  
  
```  
  
 The following example alters the credential to connect to an existing data source.  
  
```  
ALTER EXTERNAL DATA SOURCE hadoop_eds SET  
   CREDENTIAL = new_hadoop_user  
    ;  
```  
  
  
