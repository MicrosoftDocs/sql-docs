---
title: "Access external data: S3-compatible object storage - PolyBase"
description: The article explains how to use PolyBase on a SQL Server instance to query external data in S3-compatible object storage. Create external tables to reference the external data.
ms.date: 11/10/2022
ms.metadata: seo-lt-2019
ms.service: sql
ms.subservice: polybase
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

This article explains how to use PolyBase to query external data in S3-compatible object storage.

[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] introduces the ability to connect to any S3-compatible object storage, there are two available options for authentication: basic authentication or pass-through authorization (also known as STS authorization).

Basic Authentication, also known as static credentials, requires the user to store the `access key id` and `secret key id` in SQL Server, it is up to the user to explicitly revoke and rotate the credentials whenever needed. Fine-grain access control would require the administrator to set up static credentials for each login, this approach can be challenging when dealing with dozens or hundreds of unique credentials.

Pass-through (STS) authorization offers a solution for these problems by enabling the use of SQL Server own user's identities to access the S3-compatible object storage. S3-compatible object storage has the ability of assigning a temporary credential through the use of Secure Token Service (STS). These credentials are short termed and dynamically generated.

This article includes instructions for both Basic Authentication and pass-through authorization (STS) authorization.

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
* TLS must have been configured. It is assumed that all connections will be securely transmitted over HTTPS not HTTP. The endpoint will be validated by a certificate installed on the SQL Server OS Host. For more information on TLS and certificates, see [Enable encrypted connections to the Database Engine](../../database-engine/configure-windows/enable-encrypted-connections-to-the-database-engine.md).

### Permissions

In order for the proxy user to read the content of an S3 bucket, the user will need to be allowed to perform the following actions against the S3 endpoint:

* *ListBucket*;
* *ReadOnly*;

### Pre-configuration

1. Enable PolyBase in `sp_configure`:

```sql
exec sp_configure @configname = 'polybase enabled', @configvalue = 1;
GO
RECONFIGURE
GO
exec sp_configure @configname = 'polybase enabled';
```

To continue, choose [Basic Authentication](#basic-authentication) or [pass-through (STS) authorization](#pass-through-sts-authorization).

## Basic Authentication

1. Before you create a database scoped credential, the user database must have a master key to protect the credential. For more information, see [CREATE MASTER KEY](../../t-sql/statements/create-master-key-transact-sql.md). 

### Create a database scoped credential with Basic Authentication

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

### Create an external data source with Basic Authentication

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

### Limitations of Basic Authentication

1. For S3-compatible object storage, customers are not allowed to create their access key ID with a `:` character in it.
2. The total URL length is limited to 259 characters. This means `s3://<hostname>/<objectkey>` shouldn't exceed 259 characters. The `s3://` counts towards this limit, so the path length cannot exceed 259-5 = 254 characters.
3. The SQL credential name is limited by 128 characters in UTF-16 format.
4. The credential name created must contain the bucket name unless this credential is for a new external data source.
5. Access Key ID and Secret Key ID must only contain alphanumeric values.

## Pass-through (STS) authorization

S3-compatible object storage has the ability of assigning a temporary credential through the use of Secure Token Service (STS). These credentials are short termed and dynamically generated.

Pass-through authorization relies on Active Directory Federation Service (ADFS) acting as OpenID Connect (OIDC) identity provider, it is up to the ADFS to communicate with the S3-compatible object storage STS, request the STS and provide it back to SQL Server.

### Use pass-through (STS) authorization on SQL Server

1. TLS must be configured with certificates between the SQL Server and the S3-compatible host server. It is assumed that all connections will be securely transmitted over HTTPS, not HTTP. The endpoint will be validated by a certificate installed on the SQL Server OS Host. Public or self-signed certificates are supported.

2. Create a database scoped credential to which will be used to pass the identity to the S3-compatible object storage. For more information, see [CREATE DATABASE SCOPED CREDENTIAL (Transact-SQL)](../../t-sql/statements/create-database-scoped-credential-transact-sql.md). As the following example:

```sql
CREATE DATABASE SCOPED CREDENTIAL CredName
WITH IDENTITY = 'User Identity'
```

3. Create an external data source to access the S3-compatible object storage. Use `CONNECTION_OPTIONS`, as JSON format, to inform the required information for both the ADFS and STS. For more information, see [CREATE EXTERNAL DATA SOURCE](../../t-sql/statements/create-external-data-source-transact-sql.md). As the following example:

```sql
CREATE EXTERNAL DATA SOURCE EdsName 
WITH  
{ 
    LOCATION = 's3://<hostname>:<port>/<bucket_name>',
   CREDENTIAL= <CredName>    
   [, CONNECTION_OPTIONS= '{ 
       [, "authorization": 
      {
           "adfs":
          {
              "endpoint": "http[s]://hostname:port/servicepath", 
              "relying_party": "SQL Server Relying Party Identifier"
          },
          "sts":
           {
                "endpoint": "http[s]://hostname:port/stspath", 
                "role_arn": "Role Arn"
               [, "role_session_name": "AD user login"]  -- default value if not provided
               [, "duration_seconds": 3600]              -- default value if not provided
               [, "version": "2011-06-15"]               -- default value if not provided
               [, "request_parameters": "In request query string format"]
          }
      }]
      [, "s3": 
      { 
           "url_style": "Path" 
       }] 
   }'] 
}
```

- `ADFS` options specify Windows transport endpoint and `relying_party` identifier of SQL Server in ADFS.
- `STS` options specify S3-compatible object storage STS endpoint and parameters for `AssumeRoleWithWebIdentity` request. The `AssumeRoleWithWebIdentity` is the method used to acquire the temporary security credential used to authenticate. For the complete list of parameters, including optional ones, and information about default values please refer to [STS API Reference](https://docs.aws.amazon.com/STS/latest/APIReference/API_AssumeRoleWithWebIdentity.html).

### Use pass-through (STS) authorization with Active Directory

* Mark SQL Server user accounts properties in AD as non-sensitive to allow pass-through to S3-compatible storage.
* Allow Kerberos constrained delegation to ADFS services for the user related to SQL server SPN (Service Principal Names).

### Use pass-through (STS) authorization with Active Directory Federation Service

* Enable the SQL Server to be a [claims provider trust](/windows-server/identity/ad-fs/operations/create-a-claims-provider-trust) in Active Directory.
* Allow intranet windows authentication as authentication methods for ADFS.
* Enable windows transport service endpoint in your intranet.
* Enable OIDC (OpenID Connect) endpoints.
* Register SQL Server as a [relying party trust](/windows-server/identity/ad-fs/operations/create-a-relying-party-trust).
    * Provide a unique identifier.
    * Set claims rules for JWT (JSON Web Token).
<!--    * Sub – SQL Server login name.-->
* Custom claims - These claims can be added by customers if these are needed to determine access policy on the storage side.
* For more vendor-specific information, check with your S3-compatible platform provider.

### Use pass-through (STS) authorization on S3-compatible object storage

* Follow documentation provided by S3-compatible storage provider to set up external OIDC identity provider. To set up identity provider mostly following values are commonly needed.

    * Configuration end point of OIDC provider.
    * Thumbprint of OIDC provider.
    * Pass-through authorization to S3-compatible object storage

### Limitations of pass-through (STS) authorization

* Pass-through to S3-compatible object storage will be supported for SQL Server logins with Windows authentication.
* STS tokens cannot be used for [BACKUP to URL for S3-compatible object storage](../backup-restore/sql-server-backup-to-url-s3-compatible-object-storage.md).
* ADFS and SQL Server must be in the same domain. ADFS Windows transport endpoint should be disabled from the extranet.
* ADFS should have the same AD (Active directory) as SQL Server as claim trust provider.
* S3-compatible storage should have STS endpoint service that enables clients to request temporary credentials using JWT of external identities.
* OPENROWSET and CETAS (Create External Table as Select) queries will be supported for parquet and CSV format.
* By default, Kerberos ticket renewal time is seven days and lifetime are 10 hours on Windows and 2 hours on Linux. SQL Server renews Kerberos token of the user up to seven days. After seven days the user's ticket expires, therefore pass-through to S3-compatible storage will fail. In this case SQL Server will need to reauthenticate the user to get a new Kerberos ticket.
* ADFS 2019 with Windows Server 2019 will be supported.
* S3 REST API calls will use AWS signature version 4.
<!-- * Supported STS version will be 2011-06-15.-->

## Next steps

 - To learn more about PolyBase, see [Overview of SQL Server PolyBase](polybase-guide.md)
 - [Configure PolyBase to access external data in S3-compatible object storage](polybase-configure-s3-compatible.md)
 - [Virtualize parquet file in a S3-compatible object storage with PolyBase](polybase-virtualize-parquet-file.md)
 - For more tutorials on creating external data sources and external tables to a variety of data sources, see [PolyBase Transact-SQL reference](polybase-t-sql-objects.md).