---
title: Backup Server Capacity Planning
description: Backup server capacity planning for Parallel Data Warehouse starts here. Use this worksheet to size storage, network, I/O, CPU, and RAM before you buy.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: concept-article
---

# Backup server capacity planning worksheet - Parallel Data Warehouse
This capacity planning worksheet helps you determine the requirements for a backup server to perform SQL Server PDW database backup and restore operations. Use this worksheet to create your plan for purchasing new or provisioning existing backup servers.

This worksheet supplements the instructions in [Acquire and Configure a Backup Server](acquire-and-configure-backup-server.md).

## Capacity planning worksheet for backup servers

### Notes

1.  This worksheet applies to servers that perform backup and restore operations for PDW databases.

1.  The only way to back up and restore PDW databases is to use the BACKUP DATABASE and RESTORE DATABASE SQL commands. However, after the backup data is on your backup server, it exists as a set of Windows files. You can archive the backup files from your server to another storage location by using traditional Windows file-based backup methods.

### ![Clipboard icon](media/clipboard-icon.png "Clipboard icon") Capacity planning worksheet 

Print this worksheet and fill it in with your own requirements.

|Component|Requirement|Fill in this column with your own requirements|Recommendations|
|-------------|---------------|--------------------------------------------------|-------------------|
|Storage|Maximum bytes you plan to store on the backup server at any given period of time.|![Pencil icon](media/pencil-icon.png "Pencil icon")|To determine storage requirements, figure out how much data you plan to store on the backup server at any given period of time.<br /><br />The backup data is stored on the backup server in a compressed format. Data compression rates depend on the characteristics of your data.<br /><br />For example: As a rough estimate, estimate a 7:1 compression ratio relative to the size of your uncompressed data. This ratio assumes at least 80% of the data is stored in clustered columnstore indexes. For example, if you have 700 GB of uncompressed data in a database and it's stored in clustered columnstore indexes, then you could estimate the database backup needs about 100 GB.<br /><br />If you plan to have multiple copies of database backups on the backup server, account for them.<br /><br />For example: If you plan to back up 10 databases that each contain 5 TB of uncompressed data, the databases have a combined size of 50 TB. If you plan to back up these 10 databases every day for 5 days in a row, the total uncompressed size is 250 TB. Factoring in a 7:1 compression ratio, you need 250 / 7 = 35.7 TB of storage on your backup server. Be conservative and get about 30% additional capacity to account for variances and growth. In this example, 46.6 TB would be better.|
|Network|Network connection type.|![Pencil icon](media/pencil-icon.png "Pencil icon")|Determine the best network connection type that can meet your load rate requirements.<br /><br />For example: InfiniBand or 10Gbit Ethernet provide the optimal loading rates. 1Gbit Ethernet limits load rates to 360 GB per hour or less.|
|I/O|Bytes per hour for writes.|![Pencil icon](media/pencil-icon.png "Pencil icon")|For writing backups to disk, 4 TB per hour write speeds are optimal.<br /><br />For example: For drives that can write 50 MB/sec, you want at least 24 drives, plus more for mirroring or parity.<br /><br />For I/O capacity, take into account all of the I/O happening on the loading server. If the loading server has other I/O traffic in addition to data loads, such as receiving data files from an ETL server, the I/O requirements increase.|
|CPU|Number of sockets.|![Pencil icon](media/pencil-icon.png "Pencil icon")|Receiving and storing backup files isn't a CPU-intensive application.  As a minimum requirement, use a recently-manufactured 2-socket server.|
|RAM|GB of memory that allows Windows to cache files during loads.|![Pencil icon](media/pencil-icon.png "Pencil icon")|Receiving and storing backup files requires very little RAM on the loading server.<br /><br />To determine the RAM requirements, refer to your Windows Server installation and any third-party application requirements. Use a minimum of 32 GB if you don't have requirements from other sources.|

When you finish determining your capacity requirements, return to the [Acquire and Configure a Loading Server](acquire-and-configure-loading-server.md) article to plan your purchase.

## Related content

- [Backup and loading hardware overview - Parallel Data Warehouse](backup-and-loading-hardware.md)
