---
title: "Access external data: S3-compatible object storage - PolyBase"
description: The article explains how to use PolyBase on a SQL Server instance to query external data in S3-compatible object storage. Create external tables to reference the external data.
ms.date: 03/05/2021
ms.metadata: seo-lt-2019
ms.prod: sql
ms.technology: polybase
ms.topic: conceptual
ms.custom:
- event-tier1-build-2022
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: hudequei
monikerRange: ">= sql-server-linux-ver16 || >= sql-server-ver16"
---
# Configure PolyBase to access external data in S3-compatible object storage

 [!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

This article explains how to use PolyBase to query external data in an S3-compatible object storage.

## Prerequisites

To use the S3-compatible object storage integration features, you will need the following tools and resources:

* Install the PolyBase feature for SQL Server.
* Install [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md).
* S3-compatible storage.
* An S3 bucket created. Buckets cannot be created or configured from SQL Server.
* A user (`Access Key ID`) has been configured and the secret (`Secret Key ID`) and that user is known to you. You will need both to authenticate against the S3 object storage endpoint.
* *ListBucket* permission on S3 user for browse privileges.
* *ReadOnly* permission on S3 user for read privileges.
* *WriteOnly* permission on S3 user for write privileges.
* TLS must have been configured. It is assumed that all connections will be securely transmitted over HTTPS not HTTP. The endpoint will be validated by a certificate installed on the SQL Server OS Host.

### Permission

In order for the proxy user to read the content of an S3 bucket, the user will need to be allowed to perform the following actions against the S3 endpoint:

* *ListBucket*;
* *ReadOnly*;

### Pre-configuration

1. Enable PolyBase in `sp_configure`:

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1
;
RECONFIGURE
;
exec sp_configure @configname = 'polybase enabled'
;
```

1. Before you create a database scoped credential, the user database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md). 

### Create a database scoped credential

The following sample script creates a database scoped credential `s3-dc` in the source user database in SQL Server. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md).

```sql
USE [database_name];
GO
IF NOT EXISTS(SELECT * FROM sys.credentials WHERE name = 's3_dc')
BEGIN
 CREATE DATABASE SCOPED CREDENTIAL s3_dc
 WITH IDENTITY = 'S3 Access Key',
 SECRET = '<AccessKeyID>:<SecretKeyID>' ;
END
GO
```

Verify the new database-scoped credential with [sys.database_scoped_credentials (Transact-SQL)](../system-catalog-views/sys-database-scoped-credentials-transact-sql.md):

```sql
SELECT * FROM sys.database_scoped_credentials;
```

### Create an external data source

The following sample script creates an external data source `s3_ds` in the source user database in SQL Server. The external data source references the `s3_dc` database scoped credential. For more information, see [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md).

```sql
CREATE EXTERNAL DATA SOURCE s3_ds
WITH
(   LOCATION = 's3://<ip_address>:<port>/'
,   CREDENTIAL = s3_dc
);
GO
```

Verify the new external data source with [sys.external_data_sources](../system-catalog-views/sys-external-data-sources-transact-sql.md).

```sql
SELECT * FROM sys.external_data_sources;
```

## Limitations

1. SQL Server queries on an external table backed by S3-compliant object storage are limited to 1000 objects per prefix. This is because S3-compliant object listing is limited to 1000 object keys per prefix.
2. For S3-compliant object storage, customers are not allowed to create their access key ID with a `:` character in it.
3. The total URL length is limited to 259 characters. This means `s3://<hostname>/<objectkey>` shouldn't exceed 259 characters. The `s3://` counts towards this limit, so the path length cannot exceed 259-5 = 254 characters.
4. The SQL credential name is limited by 128 characters in UTF-16 format.
5. The credential name created must contain the bucket name unless this credential is for a new external data source.
6. Access Key ID and Secret Key ID must only contain alphanumeric values.

## Next steps

 - To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md)
 - [Virtualize parquet file in a S3-compatible object storage with PolyBase](polybase-virtualize-parquet-file.md)
 - For more tutorials on creating external data sources and external tables to a variety of data sources, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md).
