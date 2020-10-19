---
title: Encryption at Rest
titleSuffix: SQL Server Big Data Clusters
description: Learn all about encryption at rest on a SQL Server 2019 Big Data Cluster.
author: dacoelho
ms.author: dacoelho
ms.reviewer: mikeray
ms.metadata: seo-lt-2019
ms.date: 10/19/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Encryption at Rest Concepts and Configuration Guide

Starting from SQL Server Big Data Clusters CU8, a comprehensive Encryption at Rest feature set is available to provide application level encryption to all data stored in the platform. This guide documents the concepts, architecture and configuration for the Encryption at Rest feature set for SQL Server Big Data Clusters.

SQL Server Big data clusters stores data in the following two locations:

* __SQL Server__
* __HDFS__ used by Storage pool and Spark.

To be able to transparently encrypt data in SQL Server Big Data Clusters there are two possible approaches:

* __Volume encryption__. This is supported by the Kubernetes platform and is expected as a best practice for Big Data Clusters deployments. This guide does not cover volume encryption. Please consult your kubenetes platform or appliance documentation for guides on how to properly encrypt volumes that will be used for SQL Server Big Data Clusters.
* __Application level encryption__. This architecture refers to the encryption of data by the application handling the data before it is written to disk. In case the volumes are exposed, an attacker wouldn’t be able to restore data artifacts elsewhere, unless the destination system also has been configured with the same encryption keys. 

The Encryption at Rest feature set of SQL Server Big Data Clusters supports the core scenario of application level encryption for the SQL Server and HDFS components.

The following capabilities are provided:

* __System managed encryption at rest__. This is available in CU8.
* __User managed encryption at rest (BYOK)__, with both service managed and external key provider integrations. This will be available in a future release.

## Key Definitions

### SQL Server Big Data Clusters Key Management Service - BDC KMS

A Controller hosted service responsible for managing keys and certificates for the Encryption at Rest feature set for the SQL Server BDC cluster. It’s a service that supports the following features:

* Secure management and storage of keys and certificates used for encryption at rest.
* Hadoop KMS compatibility. It acts as the key management service for HDFS component on BDC.
* SQL Server TDE certificate management.
* *Keys Versioning support*. This will be available in a future release.

We will reference this service as __BDC KMS__ throughout the rest of this document. Also the term __BDC__ is used to refer to the __SQL Server Big Data Clusters__ computing platform.

### System Generated and Managed Keys and Certificates

BDC KMS service will generate and manage all keys and certificates for SQL Server and HDFS.

### User Provided Keys and Certificates

User provides keys and certificates to be managed by BDC KMS, commonly known as BYOK. This feature will provided in a future release.

### External Providers

External key solutions compatible with BDC KMS for external delegation. This feature will provided in a future release.

## Encryption at Rest on SQL Server Big Data Clusters CU8

SQL Server Big Data Clusters CU8 is the initial release of the Encryption at Rest feature set. Please read this document carefully to completely assess your scenario.

The feature set introduces the __BDC KMS controller service__ to provide __system generated and system managed keys and certificates for encryption on both SQL Server and HDFS__. Those keys and certificates are service managed and this documentation will provide proper operational guidance on how to interact with the service.

* __SQL Server__ instances leverages the established [__Transparent Data Encryption (TDE)__](https://docs.microsoft.com/sql/relational-databases/security/encryption/transparent-data-encryption?view=sql-server-ver15) functionality.
* __HDFS__ uses native Hadoop KMS within each pod to interact with BDC KMS on the controller. This enables HDFS Encryption Zones, which provide secure paths on HDFS.

### SQL Server instances

* A system generated certificate will be installed on SQL Server pods to be used with TDE encryption commands. The system managed certificate naming standard is ‘TDECertificate+timestamp’. Example:  *TDECertificate2020_09_15_22_46_27*
* Master instance BDC provisioned databases and user databases won’t be encrypted automatically. DBA’s may use the installed certificate to encrypt any database.
* Compute Pool and Storage Pool will be automatically encrypted using the system generated certificate.
* Data Pool encryption, albeit technically possible using T-SQL’s ‘EXECUTE AT’ commands, is discouraged and unsupported at this time. Using this techniques to encrypt Data Pool databases might not be effective and encryption may not be happening at the desired state. It will also create an incompatible upgrade path towards next releases.
* There is no certificate rotation, this will come in future releases with the appropriate azdata command set. It is supported to decrypt and then encrypt with a new certificate if not on HA deployments.
* Encryption monitoring happens through existing standard SQL Server DMVs for TDE.
* It is supported to backup and restore a TDE enabled database into the cluster.
* HA is supported. If a database on the primary instance of SQL Server is encrypted, then all secondary replica of the database will be encrypted as well.

### HDFS Encryption Zones

* [Active Directory integration](active-directory-prerequisites.md) is required to enable the encryption zones feature for HDFS.
* A system generated key will be provisioned in Hadoop KMS. The key name is __‘securelakekey’__. On CU8 the default key is 256 bit and we support 256 bit AES encryption.
* A default encryption zone will be provisioned using the above system generated key on a path named __```/securelake```__.
* Users can create additional keys and encryption zones using specific instructions provided in this guide. Users will be able to choose the key size of 128, 192, or 256 during key creation.
* In place key rotation for HDFS is not possible in CU8. As an alternative, the data can be moved from one encryption zone to another using distcp.
* Its not supported to perform HDFS Tiering mounting on top of an encryption zone.

## Configuration Guide

SQL Server Big Data Clusters Encryption at Rest is a service managed feature and, depending on your deployment path, may require additional steps.

During __new deployments of SQL Server Big Data Clusters__, CU8 onwards, __Encryption at Rest will be enabled and configured by default__. That means:

* BDC KMS component will be deployed in the controller and will generate a default set of keys and certificates.
* SQL Server will be deployed with TDE turned on and certificates will get installed by the controller.
* Hadoop KMS (for HDFS) will be configured to interact with BDC KMS for encryption operations. HDFS Encryption Zones are configured and ready to use.

Requirements and default behaviors described in the previous section apply.

If __upgrading your cluster to CU8__, __read carefully the next section__.

### Upgrading to CU8

   > [!CAUTION]
   > Before upgrading to SQL Server Big Data Clusters CU8 perform a complete backup of your data.

On existing clusters, the upgrade process __won't enforce encryption on user data__ and __won't configure HDFS Encryption Zones__. This is by design and the following needs to be considered per component:

* __SQL Server__

    1. __Master instance databases__. The upgrade process won’t touch any master instance databases and installed TDE certificates, but it is highly encouraged to backup your databases and your manually installed TDE certificates before the upgrade process. It is also advised to store those artifacts outside the SQL Server BDC cluster.
    1. __Compute and Storage Pool__. Those databases are system managed, volatile and will be recreated and automatically encrypted on cluster upgrade.
    1. __Data Pools__. The upgrade process won’t touch data pool tables.

* __HDFS__

    1. __HDFS__. The upgrade process won't touch HDFS files and folders outside encryption zones.
    1. __Encryption Zones won't be configured__. The Hadoop KMS component won't be configured to use BDC KMS. In order to configure and enable HDFS Encryption Zones feature after upgrade follow the next section.

### Enabling HDFS Encryption Zones after upgrade

Execute the following actions if you upgraded your cluster to CU8 (azdata upgrade) and want to enable HDFS Encryption Zones.

Requirements:
- [Active Directory](active-directory-prerequisites.md) integrated cluster.
- [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md) installed and connected to the cluster.
- From the [SQL Server BDC Operation Notebooks](cluster-manage-notebooks.md) execute the __```sop128-enable-encryption-zones```__ notebook to perform the configuration and validation of HDFS Encryption Zones support.

## Next steps

To learn more about how to effectively use Encryption at Rest SQL Server Big Data Clusters see the following:

- [Encryption at Rest - SQL Server TDE](encryption-at-rest-sql-server-tde.md)
- [Encryption at Rest - HDFS Encryption Zones](encryption-at-rest-hdfs-encryption-zones.md)
- [Encryption at Rest - Key Management](encryption-at-rest-key-management.md)

To learn more about the [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)], see the following overview:

- [What are [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]?](big-data-cluster-overview.md)
