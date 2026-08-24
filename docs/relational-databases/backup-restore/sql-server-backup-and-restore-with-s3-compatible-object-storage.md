---
title: "Back Up and Restore SQL Server With S3-Compatible Object Storage"
description: Learn about SQL Server backup to and restore from S3-compatible object storage, including the benefits of using S3-compatible object storage to store SQL Server backups.
author: MashaMSFT
ms.author: mathoma
ms.reviewer: randolphwest
ms.date: 01/07/2025
ms.service: sql
ms.subservice: backup-restore
ms.topic: concept-article
monikerRange: ">=sql-server-ver16 || >=sql-server-linux-ver16"
---

# Back up and restore SQL Server with S3-compatible object storage

[!INCLUDE [SQL Server 2022 and later](../../includes/applies-to-version/sqlserver2022-and-later.md)]

This article introduces the concepts, requirements, and components necessary to use S3-compatible object storage as a backup destination.

## Overview

[!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] introduces object storage integration to the data platform, enabling you to integrate SQL Server with S3-compatible object storage in addition to Azure Storage. To provide this integration, SQL Server provides an S3 connector, which uses the S3 REST API to connect to any provider of S3-compatible object storage. [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] extends the existing `BACKUP TO URL` and `RESTORE FROM URL` syntax by adding support for the S3 connector using the REST API. For information on supported platforms, see [providers of S3-compatible object storage](#providers-of-s3-compatible-object-storage).

This article contains information on using Backup to URL for S3-compatible object storage. To learn more about using Backup to URL for S3-compatible object storage, see [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md).

### Back up and restore to S3-compatible storage

The `BACKUP TO URL` and `RESTORE FROM URL` syntax support the S3 connector. For more information on Backup to URL functionality, see:

- [SQL Server backup to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md).

- [BACKUP (Transact-SQL)](../../t-sql/statements/backup-transact-sql.md)

- [SQL Server back up to URL for Microsoft Azure Blob Storage best practices and troubleshooting](sql-server-backup-to-url-best-practices-and-troubleshooting.md)

- [Blog: SQL Server Backup to URL - a cheat sheet](https://techcommunity.microsoft.com/blog/azuresqlblog/sql-server-backup-to-url-%E2%80%93-a-cheat-sheet/346358)

## Providers of S3-compatible object storage

There are many providers of S3-compatible object storage in the market today. Object storage is either provided as software-defined, as hardware appliances, or as a combination for hybrid cloud scenarios.

The following table provides a nonexhaustive summary of object storage providers offering an S3 endpoint as part of their solution.

| Vendor (alphabetical) | Offering |
| --- | --- |
| [AWS](https://aws.amazon.com/s3) | Amazon Simple Storage Service (S3) |
| [Ceph](https://ceph.com/en/) | Ceph |
| [Cloudian](https://cloudian.com/products/hyperstore/) | HyperStore |
| [Dell Technologies](https://www.dell.com/en-us/shop/scc/sc/storage-products?pid=ModernDataCenterPage-022916) | ECS Enterprise Object Storage |
| [Hitachi Vantara](https://docs.hitachivantara.com/) | Hitachi Content Platform for Cloud Scale |
| [HPE](https://www.hpe.com/us/en/hpe-ezmeral-data-fabric.html) | HPE Ezmeral Data Fabric |
| [MinIO](https://min.io) | Multicloud Object Storage |
| [NetApp](https://www.netapp.com/data-storage/storagegrid/) | StorageGRID, ONTAP |
| [Nutanix](https://www.nutanix.com/products/objects) | Nutanix Object Storage |
| [Pure Storage](https://www.purestorage.com/products/unstructured-data-storage/flashblade-s.html) | Pure FlashBlade |
| [Red Hat](https://www.redhat.com/technologies/cloud-computing/openshift) | OpenShift Container Storage |
| [Scality](https://www.artesca.scality.com/) | Scality Artesca |
| [Weka](https://www.weka.io) | Weka S3 |

## Prerequisites for the S3 endpoint

The S3 endpoint must be configured as follows:

- TLS must be configured. The S3 endpoint must use a TLS certificate trusted by SQL Server (Linux) or its host operating system environment (Windows). Connections are assumed to be securely transmitted over HTTPS, not HTTP.

- A user (Access Key ID) must be configured and the secret (Secret Key ID) for that user is known to you. You need both to authenticate against the S3 endpoint.

- At least one bucket must be configured. Buckets can't be created or configured inside [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)].

## Performance best practices

Check with your S3-compatible object storage provider for guidance on performance best practices optimization, initial setup, and configuration. Due to a wide variety of solutions and setups, the recommended values for backup and restore parameters and throughput can change.

By using S3 parts in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and later versions, you can stripe your backup set to support files size up to 12.8 TB.

## Limitations

Because of the current limitation of S3 Standard REST API, the temporary uncommitted data files aren't removed if there are failures. They can be created in the S3-compatible object store due to an ongoing multipart upload operation while the `BACKUP` Transact-SQL command is running.

These uncommitted data blocks persist in the S3-compatible object storage in the case the `BACKUP` command fails or is canceled. If the backup succeeds, the object store automatically removes these temporary files to form the final backup file. Some S3-providers handle temporary file cleanup through their garbage collector system.

## Related content

- [SQL Server back up to URL for S3-compatible object storage](sql-server-backup-to-url-s3-compatible-object-storage.md)
- [SQL Server back up to URL for S3-compatible object storage best practices and troubleshooting](sql-server-backup-to-url-s3-compatible-object-storage-best-practices-and-troubleshooting.md)
- [SQL Server backup to URL for Microsoft Azure Blob Storage best practices and troubleshooting](sql-server-backup-to-url-best-practices-and-troubleshooting.md)
- [SQL Server backup to URL for Azure Blob Storage](sql-server-backup-to-url.md)
- [Back up and restore: System databases (SQL Server)](back-up-and-restore-of-system-databases-sql-server.md)
- [Tutorial: Use Azure Blob Storage with SQL Server](../tutorial-use-azure-blob-storage-service-with-sql-server.md)
