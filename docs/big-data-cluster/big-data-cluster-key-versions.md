---
title: Key Versions
titleSuffix: SQL Server Big Data Clusters
description: This article provides details of how key versions are used for SQL BDC key management and key rotation for HDFS and SQL Server keys.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 06/14/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: conceptual
---

# Key Versions in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]

[!INCLUDE[SQL Server 2019](../includes/applies-to-version/sqlserver2019.md)]

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

This article provides details of how key versions are used in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] for key management and key rotation for HDFS and SQL Server keys. The article includes details about how the versions can [incorporate customer's keys](#customer-provided-key). 

For general information on securing [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see [Security concepts for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](concept-security.md).

For information on configuring and using the encryption at rest feature see the following guides:
* [Encryption at rest concepts and configuration guide](encryption-at-rest-concepts-and-configuration.md)
* [SQL Server Big Data Clusters HDFS Encryption Zones usage guide](encryption-at-rest-hdfs-encryption-zones.md)
* [SQL Server Big Data Clusters transparent data encryption (TDE) at rest usage guide](encryption-at-rest-sql-server-tde.md)

## HDFS keys

For using encryption at rest in HDFS, the following concepts are involved:

* Encryption zones (EZ): The encryption zone is a folder in HDFS that is associated with a key. All files in this folder are encrypted. Default provisioned EZ in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] is called "securelake".
* Encryption Zone keys (EZ Key): A named symmetric key. The default system-managed provisioned in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] is "securelakekey". The encryption zone keys are managed using Hadoop KMS (Key Management Server) running inside the name node pods of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]. The EZ keys are further protected by a main encryption key stored in controldb (discussed in sections below). The EZ keys are encrypted in Hadoop KMS by fetching the public part of main encryption key and the decryption requests are sent to the control plane.
* Encrypted Data Encryption Key: Every file in Encryption zone is encrypted by a Data Encryption Key (DEK) generated for the file. Once the DEK is created, it is persisted with the data. To persist the DEK, it is first Encrypted by the Encryption Zone Key and then persisted with data. The DEK is randomly generated per file and the strength of the symmetric DEK is the same as the strength of the EZ Key.

The below graphic explains how files are protected by the DEK and how the DEK is protected by the EZ key securelakekey.

   :::image type="content" source="media/big-data-cluster-key-versions/securelakekey.png" alt-text="Shows how files are protected by the DEK and how the DEK is protected by the EZ key securelakekey":::  
   

## SQL Server keys

The system managed main encryption key and the HDFS EZ keys are stored inside the controldb, which will be named controldb-<#>, for example `controldb-0`. For more information, see [Resources deployed with Big Data Cluster](concept-architecture-pods.md).

SQL Server databases are encrypted by a symmetric key, also known as a Database encryption key (DEK). The DEK is persisted with the database in an encrypted format. The DEK protector can be a *certificate* or *asymmetric key*. To change the DEK protector use [ALTER DATABSE ENCRYPTION KEY](../t-sql/statements/alter-database-encryption-key-transact-sql.md) statement. The asymmetric key in SQL Server has metadata containing a URL link to the key inside the control plane. Hence all the encryption and decryption operations of the Database Encryption Key (DEK) are done inside the controller. SQL Server stores the public key, but only to identify the asymmetric key and doesn't encrypt using the public key.

:::image type="content" source="media/big-data-cluster-key-versions/sqlkey.png" alt-text="SQL Server Keys":::

## [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] main encryption key

In [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] control plane, to protect the Encryption zone keys and to provision asymmetric keys in SQL Server, there is a concept of the main encryption key. There are two main encryption keys, one for SQL Server and one for HDFS. This concept allows [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] control plane to allow the main encryption key to reside outside the cluster as well. The properties of main encryption key are:

1. The main encryption keys are asymmetric RSA keys.
2. A main encryption key is created for SQL Server master instance and another for HDFS.
3. The public key corresponding to the main encryption key is always stored in the controller database or controldb. The private key is stored in the controller database for system managed main encryption key. For encryption keys from a Hardware Security Module (HSM) or any other external provider, the private keys are not stored inside the controller database (unless the application for external keys brings the private key with it). However, the private key is not needed inside the controldb and [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] will not require the private key material.
4. Encryption operations using the public key of the main encryption key can be performed inside the controller itself, or, the controller can distribute the public key to Hadoop KMS so that Hadoop KMS can perform encryption locally. The decryption operations are expected to be handled by the external key provider, like HSM. This design allows us to keep the sensitive part of the asymmetric key outside the cluster and in the customer's protection. This makes sure that the root of encryption to decrypt all the data is never available in the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] ecosystem for customer configured keys.

## Storage protection of different keys

The DEK for HDFS files and for SQL Server are stored along with the data that DEK protects. DEK is protected by the HDFS EZ key or the asymmetric key in SQL Server respectively. 

Asymmetric keys in SQL Server have metadata include the key's URL in the control plane. SQL Server only stores the public key of the asymmetric key, for correlation with the key in the control plane. 

The storage of keys in controldb, is protected by the column encryption key in controldb. The controldb stores sensitive information about the cluster and all the sensitive information is protected by a Column Encryption Key of SQL Server in controldb, which is further protected by a password stored in Kubernetes Secrets. For more information, see [Secrets in Kubernetes Documentation](https://kubernetes.io/docs/concepts/configuration/secret/).

The protection is summarized in the diagrams below. First, storage protection of HDFS EZ keys:

:::image type="content" source="media/big-data-cluster-key-versions/storage-protection-ez.png" alt-text="Storage protection of HDFS EZ keys ":::  

Storage protection of the main encryption key:

:::image type="content" source="media/big-data-cluster-key-versions/storage-protection-main.png" alt-text="Storage protection of the main encryption key":::  


## Key rotation

### HDFS encryption zone keys

When [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] is deployed with Active Directory, HDFS is provisioned with a default encryption zone called securelake, protected by *securelakekey*. We will be using the defaults in examples, however new zones and keys can be provisioned using `azdata`. The *securelakekey* is protected by a main encryption key for HDFS, which is stored in the control plane. Starting with SQL 2019 CU9, `azdata` can be used to rotate the EZ keys for HDFS. The rotation of EZ keys causes new key material to be generated, with the same name as "*securelakekey*", but with a new version of the key pointing to the key material. Any new encryption operation in HDFS (for example, file writes), EZ always uses the latest version of the key associated with EZ. For files in an EZ, protected by older version of keys, `azdata bdc hdfs encryption-zone reencrypt` command can be used, so that all the files can be protected by the latest version of the EZ key.

Consider a file called file2 placed in /securelake. The following depicts the protection chain.

:::image type="content" source="media/big-data-cluster-key-versions/protection-chain.png" alt-text="Protection chain":::

The securelakekey can be rotated to a new version using `azdata bdc hdfs key roll -n securelakekey`. The rotation doesn't cause re-encryption of DEKs protecting the file. Hadoop key rotation causes a new key material to be generated, and protected by the latest version of main-encryption-key. The following diagram shows the state of the system after rotation of securelakekey.

:::image type="content" source="media/big-data-cluster-key-versions/protection-chain-rotation.png" alt-text="Protection chain after rotation":::

To make sure that the files in the encryption zones are protected by the rotated securelakekey, `azdata bdc hdfs encryption-zone -a start -p /securelake`.

The following diagram depicts the state of system after re-encryption of encryption zone.

:::image type="content" source="media/big-data-cluster-key-versions/protection-chain-reencryption.png" alt-text="Protection chain after reencryption":::

### SQL Server

The key protecting the SQL Database is the DEK, which can be regenerated. The process of regeneration would cause the whole database to be re-encrypted.

### Main encryption key rotation

> [!NOTE]
> Prior to SQL Server 2019 CU10 there was no way to rotate the main encryption key. Starting with SQL Server 2019 CU10, `azdata` command is be exposed to allow the rotation of the main encryption key.

The main encryption key is an RSA 2048-bit key. The rotation of the main encryption key would do one of the following depending on the source of the key:

1. Create new key in case the request has been made to rotate the main key to a system-managed key. A system-managed key is an asymmetric key, generated and stored inside the controller.
2. Create a reference to an externally provided key, where the private key of the asymmetric will be managed by the customer application. The customer application need not have the private key, but it should know how to retrieve the private key based on the configuration of the application provided.

Rotation of the main key does not re-encrypt anything. SQL Server and HDFS keys must then be rotated:

- After the main key has been rotated, the SQL Server database DEK protector will need to be rotated to the new version of the main key created. 
- Similar concepts apply to HDFS. When the HDFS key is rotated, new material is encrypted by the latest version of the main key. Older versions of the EZ keys will remain untouched. After the HDFS EZ key is rotated then the encryption zones associated with the EZ key need to be re-encrypted so that they are re-encrypted by the latest EZ key version.

### Main key rotation for HDFS

The following diagrams illustrate the process of rotating the main encryption key for HDFS. First, the initial state of HDFS:

:::image type="content" source="media/big-data-cluster-key-versions/hdfs-initial.png" alt-text="Initial state of HDFS":::  

The main encryption key will be rotated using `azdata bdc kms set –key-provider SystemManaged`. (Note the command causes rotation of main encryption key for both SQL and HDFS, even though they are different keys inside the control plane.) After using the `azdata bdc kms` command:

:::image type="content" source="media/big-data-cluster-key-versions/hdfs-rotated.png" alt-text="After using the azdata bdc kms command":::  

To use the new version of the HDFS main encryption key, the `azdata bdc hdfs key roll` command can be used, which then takes the system to the following state. After rotating the securelakekey:

:::image type="content" source="media/big-data-cluster-key-versions/hdfs-securelakekey.png" alt-text="After rotating the securelakekey":::  

Rotating the securelakekey would cause a new version of securelakekey to be created, and protected by main encryption key. To make sure that the files in securelake are encrypted by securelakekey@2, the securelake encryption zone needs to be re-encrypted. After re-encrypting encryption zone:

:::image type="content" source="media/big-data-cluster-key-versions/hdfs-reencryption.png" alt-text="After re-encrypting encryption zone":::  

### Main key rotation for SQL Server

The SQL Server main encryption key is installed in the master database of SQL Server master instance.

The following diagrams illustrate the process of rotating main encryption key for SQL Server. 

On a fresh install of [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], there will be no asymmetric key provisioned in SQL Server. In the initial State of SQL Server, the databases may be encrypted using certificates, however the SQL Server master instance admin controls this aspect.

:::image type="content" source="media/big-data-cluster-key-versions/sql-initial.png" alt-text="Initial State of SQL Server":::  

The main encryption key will be rotated using `azdata bdc kms set –key-provider SystemManaged`. (Note the command causes rotation [or creates if not exists] of main encryption key for SQL and HDFS both, even though they are different keys inside the control plane.) 

:::image type="content" source="media/big-data-cluster-key-versions/install-sql-key.png" alt-text="The SQL Server main encryption key is installed in the master DB of SQL Server master instance":::  

The asymmetric key can be seen using the following T-SQL query, with the `sys.asymmetric_keys` system catalog view.

```tsql
USE master;
select * from sys.asymmetric_keys;
```

The asymmetric key will appear with the naming convention "tde_asymmetric_key_\<version\>". The SQL Server administrator can then change the protector of the DEK to the asymmetric key using [ALTER DATABASE ENCRYPTION KEY](../t-sql/statements/alter-database-encryption-key-transact-sql.md). For example, use the following T-SQL command:

```tsql
USE db1;
ALTER DATABASE ENCRYPTION KEY ENCRYPTION BY SERVER ASYMMETRIC KEY tde_asymmetric_key_0;
```

Now, the DEK protector is changed to use the asymmetric key:

:::image type="content" source="media/big-data-cluster-key-versions/sql-asymmetric.png" alt-text="After the DEK protector is changed to use the asymmetric key":::  

If `azdata bdc kms set` command is re-executed, then the asymmetric keys in SQL Server would show another entry in `sys.asymmetric_keys` with the format "tde_asymmetric_key_\<version\>". This `azdata` command can be used to again change the DEK protector of a SQL Server database.

## Customer provided key

With the capability to bring in external keys in [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], the main encryption key fetches the public key using the Application that the customer deploys. When HDFS keys are rotated and used, the calls to decrypt the HDFS keys will be sent to the control plane and then redirected to the application using the key identifier provided by the customer. For SQL Server, the requests to encrypt are sent and fulfilled by the control plane, since it has the public key. The requests to decrypt DEK from SQL, are sent to control plane as well, and then are redirected to the KMS application.

:::image type="content" source="media/big-data-cluster-key-versions/sql-customerkey.png" alt-text="After customer key is installed":::

The following diagram explains the interactions while configuring external keys in control plane:

:::image type="content" source="media/big-data-cluster-key-versions/external-key-control-pane-interactions.png" alt-text="Interactions while configuring external keys in control plane"  lightbox="media/big-data-cluster-key-versions/external-key-control-pane-interactions-LG.png":::  

After the key is installed, the encryption and decryption of different payloads are protected by main encryption key. This protection is similar to system-managed keys, except that the decryption calls routed to control plane, are then routed to the KMS plugin app. The KMS plugin app routes the request to appropriate location, such as the HSM or another product.

## See also

* [Security concepts for [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)]](concept-security.md)  
* [Configure a SQL Server Big Data Cluster](configure-bdc-overview.md)
* [Big Data Clusters FAQ](big-data-cluster-faq.yml)