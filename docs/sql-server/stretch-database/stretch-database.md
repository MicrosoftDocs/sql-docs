---
title: Stretch Database
description: Stretch Database migrates your cold data transparently and securely to the Microsoft Azure cloud.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: randolphwest
ms.date: 07/25/2022
ms.service: sql-server-stretch-database
ms.topic: conceptual
ms.custom: seo-dt-2019
helpviewer_keywords:
  - "Stretch Database"
---
# Stretch Database

[!INCLUDE [sqlserver2016-windows-only](../../includes/applies-to-version/sqlserver2016-windows-only.md)]

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

Stretch Database migrates your cold data transparently and securely to the Microsoft Azure cloud.

If you just want to get started with Stretch Database right away, see [Get started by running the Enable Database for Stretch Wizard](get-started-by-running-the-enable-database-for-stretch-wizard.md).

## What are the benefits of Stretch Database?

Stretch Database provides the following benefits:

- **Cost-effective availability for cold data.** Stretch warm and cold transactional data dynamically from SQL Server to Microsoft Azure with SQL Server Stretch Database. Unlike typical cold data storage, your data is always online and available to query. You can provide longer data retention timelines without breaking the bank for large tables like Customer Order History. Benefit from the low cost of Azure rather than scaling expensive, on-premises storage. You choose the pricing tier and configure settings in the Azure portal to maintain control over price and costs. Scale up or down as needed. Visit [SQL Server Stretch Database Pricing](https://azure.microsoft.com/pricing/details/sql-server-stretch-database/) for details.

- **Doesn't require changes to queries or applications.** Access your SQL Server data seamlessly regardless of whether it's on-premises or stretched to the cloud. You set the policy that determines where data is stored, and SQL Server handles the data movement in the background. The entire table is always online and queryable. And, Stretch Database doesn't require any changes to existing queries or applications - the location of the data is transparent to the application.

- **Streamlines on-premises data maintenance.** Reduce on-premises maintenance and storage for your data. Backups for your on-premises data run faster and finish within the maintenance window. Backups for the cloud portion of your data run automatically. Your on-premises storage needs are greatly reduced. Azure storage can be 80% less expensive than adding to on-premises SSD.

- **Keeps your data secure even during migration.** Enjoy peace of mind as you stretch your most important applications securely to the cloud. SQL Server's Always Encrypted provides encryption for your data in motion. Row Level Security (RLS) and other advanced SQL Server security features also work with Stretch Database to protect your data.

## What does Stretch Database do?

After you enable Stretch Database for a SQL Server instance and a database, and select at least one table, Stretch Database silently begins to migrate your cold data to Azure.

- If you store cold data in a separate table, you can migrate the entire table.

- If your table contains both hot and cold data, you can specify a filter function to select the rows to migrate.

**You don't have to change existing queries and client apps.** You continue to have seamless access to both local and remote data, even during data migration. There is a small amount of latency for remote queries, but you only encounter this latency when you query the cold data.

**Stretch Database ensures that no data is lost** if a failure occurs during migration. It also has retry logic to handle connection issues that may occur during migration. A dynamic management view provides the status of migration.

**You can pause data migration** to troubleshoot problems on the local server or to maximize the available network bandwidth.

:::image type="content" source="media/stretch-database/stretch-overview.png" alt-text="Diagram showing the Stretch Database overview.":::

## Is Stretch Database for you?

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

If you can make the following statements, Stretch Database may help to meet your requirements and solve your problems.

|If you're a decision maker|If you're a DBA|
| --- | --- |
|I have to keep transactional data for a long time.|The size of my tables is getting out of control.|
|Sometimes I have to query the cold data.|My users say that they want access to cold data, but they only rarely use it.|
|I have apps, including older apps, that I don't want to update.|I have to keep buying and adding more storage.|
|I want to find a way to save money on storage.|I can't back up or restore such large tables within the SLA.|

## What kind of databases and tables are candidates for Stretch Database?

Stretch Database targets transactional databases with large amounts of cold data, typically stored in a few tables. These tables may contain more than a billion rows.

If you use the temporal table feature of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], use Stretch Database to migrate all or part of the associated history table to cost-effective storage in Azure. For more info, see [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md).

To learn more about potential blocking issues, see [Limitations for Stretch Database](limitations-for-stretch-database.md).

## Test drive Stretch Database

> [!IMPORTANT]  
> Stretch Database is deprecated in [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)]. [!INCLUDE [ssNoteDepFutureAvoid-md](../../includes/ssnotedepfutureavoid-md.md)]

**Test drive Stretch Database with the AdventureWorks sample database.** To get the AdventureWorks sample database, download at least the database file and the samples and scripts file from [here](https://github.com/microsoft/sql-server-samples/releases/tag/adventureworks). After you restore the sample database to an instance of SQL Server 2016, unzip the samples file and open the Stretch DB Samples file from the Stretch DB folder. Run the scripts in this file to check the space used by your data before and after you enable Stretch Database,  to track the progress of data migration, and to confirm that you can continue to query existing data and insert new data both during and after data migration.

## Next steps

- [Disable Stretch Database and bring back remote data](disable-stretch-database-and-bring-back-remote-data.md)
