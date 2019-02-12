---
title: "Configure compatible SQL Server features with Stretch Database | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology:
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: c8121ede-1aec-459b-b7b0-1408bb3e62fb
author: rothja
ms.author: jroth
manager: craigg
---
# Configure compatible SQL Server features with Stretch Database
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md-winonly.md)]


Take simple steps to configure the following SQL Server features to work with Stretch Database.
-   Always On
-   Always Encrypted
-   Transparent Data Encryption (TDE)
-   Temporal tables

## Configure Always On with Stretch Database
If you're using Always On with Stretch Database, you have to make sure that the database master key is available on the secondary replicas. Stretch Database uses the database master key to secure the credentials that it uses to connect to the remote Azure database.

After you set up the Always On availability group, run the stored procedure **sp_control_dbmasterkey_password** on each secondary replica and provide the password for the Stretch-enabled database. For more info and examples, see [sp_control_dbmasterkey_password](../../relational-databases/system-stored-procedures/sp-control-dbmasterkey-password-transact-sql.md). 

## Configure Always Encrypted with Stretch Database
If you want to use Always Encrypted and Stretch Database together, you have to configure encryption on the selected columns before you enable Stretch Database on the table.

If you have already enabled Stretch Database on the table, and you want to use Always Encrypted columns, you have to do the following things.
1.   Disable Stretch Database on the table and bring the remote data back from Azure. For more info, see [Disable Stretch Database and bring back remote data](../../sql-server/stretch-database/disable-stretch-database-and-bring-back-remote-data.md).
2.   Configure Always Encrypted on the selected columns.
3. Re-enable Stretch Database on the table. For more info, see [Enable Stretch Database for a database](../../sql-server/stretch-database/enable-stretch-database-for-a-table.md).

## Configure Transparent Data Encryption (TDE) with Stretch Database

If TDE is enabled on your local database, it will not be automatically enabled on the Stretch database remote endpoint. You must remember to enable TDE on the remote endpoint after enabling Stretch on your database.

## Configure temporal tables with Stretch Database
If you're using temporal tables, you can enable Stretch Database on the history table, but not on the current table.
-   For guidance about using temporal tables with Stretch Database, see [Manage Retention of Historical Data in System-Versioned Temporal Tables](../../relational-databases/tables/manage-retention-of-historical-data-in-system-versioned-temporal-tables.md).
-   To filter rows to migrate from the history table by using a sliding window, see [Select rows to migrate by using a filter function](../../sql-server/stretch-database/select-rows-to-migrate-by-using-a-filter-function-stretch-database.md).
-   You can't enable Stretch Database on the temporal history table if the table is memory-optimized. Memory-optimized tables are not supported.
