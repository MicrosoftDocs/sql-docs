---
description: "SQL Server backup and restore with S3-compatible object storage preview"
title: "Backup & restore with S3-compatible object storage"
storage: Learn about SQL Server backup to and restore from S3-compatible object storage, including the benefits of using S3-compatible object storage to store SQL Server backups.
ms.custom:
- event-tier1-build-2022
ms.date: 05/24/2022
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
---
# SQL Server backup and restore with S3-compatible object storage preview

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

This article introduces the concepts, requirements and components necessary to use S3-compatible object storage as a backup destination. 

> [!NOTE]
> SQL Server backup and restore with S3-compatible object storage is in preview as a feature of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].
  
## Overview

[!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] introduces object storage integration to the data platform, enabling you to integrate SQL Server with S3-compatible object storage in addition to Azure Storage. To provide this integration SQL Server has been enhanced with a new S3 connector, which uses the S3 REST API to connect to any provider of S3-compatible object storage. [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)] extends the existing BACKUP/RESTORE TO/FROM URL syntax by adding support for the new S3 connector using the REST API. For information on supported platforms, see [providers of S3-compatible object storage](#providers-of-s3-compatible-object-storage).

This article contains information on using Backup to URL for S3-compatible object storage. To learn more about using Backup to URL for S3-compatible object storage, see [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md). 

### Backup and Restore to S3-compatible storage

The `BACKUP TO URL` and `RESTORE FROM URL` syntax has been extended to support the S3 connector. For more information on Backup to URL functionality, see: 

- [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md).
- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)
- [SQL Server back up to URL best practices and troubleshooting](sql-server-backup-to-url-best-practices-and-troubleshooting.md)
- [Blog: SQL Server Backup to URL – a cheat sheet](https://techcommunity.microsoft.com/t5/datacat/sql-server-backup-to-url-a-cheat-sheet/ba-p/346358)

## Providers of S3-compatible object storage

There are many providers of S3-compatible object storage in the market today. Object storage is either provided as software-defined, as hardware appliances, or as a combination for hybrid cloud scenarios.

The following table provides a non-exhaustive summary of object storage providers offering an S3 endpoint as part of their solution. Not all solutions have been validated against the current version of [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

| **Vendor (alphabetical)**       | **Offering**                  | 
|---------------------------------|-------------------------------|
| [AWS][aws_webs]                 | AWS Simple Cloud Storage (S3) |
| [Ceph][ceph_webs]               | Ceph                          |
| [Cloudian][cloudian_webs]       | HyperStore                    |
| [Dell Technologies][dell_webs]  | ECS Enterprise Object Storage |
| [Hitachi Vantara][hitachi_webs] | Hitachi Content Platform      |
| [HPE][hpe_webs]                 | HPE Ezmeral Data Fabric       |
| [MinIO][minio_webs]             | Multi-Cloud Object Storage    |
| [NetApp][netapp_webs]           | StorageGRID<br>ONTAP          |
| [Nutanix][nutanix_webs]         | Nutanix Object Storage        |
| [Pure Storage][pure_webs]       | Pure FlashBlade               |
| [Red Hat][redhat_webs]          | OpenShift Container Storage   |
| [Scality][scality_webs]         | Scality Artesca               |
| [Weka][weka_webs]               | Weka S3                       |

## Prerequisites for the S3 endpoint

The S3 endpoint must have been configured as follows:

- TLS has been configured. It is assumed that all connections will be securely transmitted over HTTPS not HTTP. SQL Server will require the certificate for this scenario.
- A user (Access Key ID) has been configured and the secret (Secret Key ID) for that user is known to you. You will need both to authenticate against the S3 endpoint.
- At least one bucket has been configured. Buckets can't be created or configured inside [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)].

## Performance best practices

It's recommended to check with your S3-compatible object storage provider for guidance on performance best practices optimization as well as initial setup and configuration. Due to a wide variety of solutions and setups, the recommended values for backup and restore parameters and throughput can change.

By using S3 parts in [!INCLUDE[sssql22-md](../../includes/sssql22-md.md)], you can stripe your backup set to support files size up to 12.8 TB.

## Known issues and limitations

Due to the current limitation of S3 Standard REST API, the temporary uncommitted data files are not removed in case of failures. They may be created in the S3-compatible object store due to an ongoing multipart upload operation while the BACKUP T-SQL command is running. These uncommitted data blocks will continue to persist in the S3-compatible object storage in the case the BACKUP T-SQL command fails or is canceled. If the backup succeeds, these temporary files are removed automatically by the object store to form the final backup file. Some S3-providers will handle temporary file cleanup through their garbage collector system.

## Next steps

 - [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md). 
 - [SQL Server back up to URL for S3-compatible object storage best practices and troubleshooting](sql-server-backup-to-url-s3-compatible-object-storage-best-practices-and-troubleshooting.md)
 - [SQL Server Backup to URL Best Practices and Troubleshooting](../../relational-databases/backup-restore/sql-server-backup-to-url-best-practices-and-troubleshooting.md)
 - [SQL Server Backup to URL for Microsoft Azure Blob Storage](../../relational-databases/backup-restore/sql-server-backup-to-url.md)
 - [Back Up and Restore of System Databases &#40;SQL Server&#41;](../../relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md)   
 - [Tutorial: Use Azure Blob Storage with SQL Server 2016 - SQL Server](../tutorial-use-azure-blob-storage-service-with-sql-server-2016.md)

<!-- Table links -->
[aws_docs]:  https://docs.aws.amazon.com/AmazonS3/latest/API/Welcome.html
<!-- [aws_sheet]: -->
[aws_webs]:  https://www.aws.amazon.com/s3

[ceph_docs]: https://docs.ceph.com/en/pacific/
<!-- [ceph_sheet]: -->
[ceph_webs]: https://ceph.com/en/

<!-- [cloudian_docs]: -->
[cloudian_sheet]: https://data.cloudian.com/l/677273/2019-06-24/h6pn/677273/20197/Cloudian_HyperStore_Xtreme_Datasheet.pdf
[cloudian_webs]: https://cloudian.com/products/hyperstore/

[dell_docs]:  http://doc.isilon.com/ECS/3.6/DataAccessGuide/GUID-8725EEF9-EE9C-4423-A9DD-58B6877B8486.html
[dell_sheet]: https://www.delltechnologies.com/asset/products/storage/briefs-summaries/dell_emc_ecs_solution_overview.pdf
[dell_webs]:  https://www.delltechnologies.com/storage/ecs/index.htm

[hitachi_docs]:  https://knowledge.hitachivantara.com/Documents/Storage/HCP_for_Cloud_Scale/2.3.x/Administration/01_Getting_started
[hitachi_sheet]: https://www.hitachivantara.com/pdf/white-paper/content-platform-architecture-fundamentals-whitepaper.pdf
[hitachi_webs]:  https://www.hitachivantara.com/en-us/products/storage/object-storage/content-platform.html

[hpe_docs]:  https://docs.datafabric.hpe.com/62/MapRObjectStore/s3-gateway.html
[hpe_sheet]: https://www.hpe.com/us/en/collaterals/collateral.a50001592enw.html
[hpe_webs]:  https://www.hpe.com/us/en/software/ezmeral-data-fabric.html

[minio_docs]: https://docs.min.io/
[minio_sheet]: https://min.io/resources/docs/MinIO-high-performance-object-storage.pdf
[minio_webs]:  https://www.min.io

[netapp_docs]:  https://docs.netapp.com/sgws-115/index.jsp
[netapp_sheet]: https://www.netapp.com/pdf.html?item=/media/7931-ds-3613.pdf
[netapp_webs]:  https://www.netapp.com/data-storage/storagegrid/

[nutanix_docs]: https://portal.nutanix.com/page/documents/details?targetId=Objects-v3_3:Objects-v3_3
[nutanix_sheet]: https://www.nutanix.com/viewer?type=pdf&path=/content/dam/nutanix/resources/datasheets/ds-objects.pdf&icid=107JORGDJNAA3
[nutanix_webs]: https://www.nutanix.com/products/objects

[pure_docs]:  https://support.purestorage.com/FlashBlade/Purity_FB/PurityFB_REST_API/S3_Object_Store_REST_API/FlashBlade_S3_Object_Store_Documentation
[pure_sheet]: https://www.purestorage.com/content/dam/pdf/en/technical-briefs/tb-pure-flashblade-uffo.pdf
[pure_webs]:  https://www.purestorage.com/products/file-and-object/flashblade.html

[redhat_docs]:  https://access.redhat.com/documentation/en-us/red_hat_openshift_container_storage/4.8
[redhat_sheet]: https://www.redhat.com/rhdc/managed-files/cl-ocs3-datasheet-f19840wg-201911-en.pdf
[redhat_webs]:  https://access.redhat.com/products/red-hat-openshift-container-storage

[scality_docs]: https://docs.scality.com
[scality_sheet]: https://go.scality.com/l/893901/2021-08-17/23c52m/893901/1629719058hjLdj34t/Artesca_Datasheet_Letter_Web_210818.pdf
[scality_webs]:  https://www.scality.com/products/artesca

[weka_docs]:  https://docs.weka.io/additional-protocols/s3
[weka_sheet]: https://www.weka.io/wp-content/uploads/files/2020/03/WekaFS-DS-W01R14DS201808.pdf
[weka_webs]:  https://weka.io

[Azure block blobs]: /rest/api/storageservices/understanding-block-blobs--append-blobs--and-page-blobs
