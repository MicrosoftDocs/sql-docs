---
title: "Project Settings (Migration) (Db2ToSQL)"
description: Use the Migration page to customize how SSMA migrates data from Db2 to SQL Server.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-migration
f1_keywords:
  - "ssma.db2.projectsettingsmigration.f1"
---
# Project Settings (Migration) (Db2ToSQL)

The Migration page of the **Project Settings** dialog box contains settings that customize how SQL Server Migration Assistant (SSMA) migrates data from Db2 to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

The Migration pane is available in both the **Project Settings** and **Default Project Settings** dialog boxes.

- To specify settings for all SSMA projects, navigate to **Tools** > **Default Project Settings**, select the migration project type for which settings are required to be viewed or changed. From the **Migration Target Version** dropdown list, select **General** at the bottom of the left pane, and then select **Migration**.

- To specify settings for the current project, navigate to **Tools** > **Project Settings**, select **General** at the bottom of the left pane, and then select **Migration**.

## Migration engine

| Term | Definition |
| --- | --- |
| **Migration Engine** | Specifies database engine used during Data Migration. Client side data migration refers to SSMA client retrieving the data from the source and bulk inserting that data into SQL Server. Server side data migration refers to SSMA data migration engine (bulk copy program) running on the SQL Server box as a SQL Agent job retrieving data from the source and inserting directly into SQL Server thus avoiding an extra client-hop (better performance).<br /><br />**Default Mode**: Client Side Data Migration Engine<br />**Optimistic Mode**: Client Side Data Migration Engine<br />**Full Mode**: Client Side Data Migration Engine |

> [!IMPORTANT]  
> When the **Migration Engine** option is set to **Server Side Data Migration Engine**, a new Project setting option **Use 32-Bit Server Side Data Migration Engine** is displayed. It specifies whether 32 bit or 64 bit Bulk Copy Program (BCP) utility is used to migrate data.

## Miscellaneous options

| Term | Definition |
| --- | --- |
| **Batch Size** | Specifies the batch size used during data migration.<br /><br />**Default Mode**: 10000<br />**Optimistic Mode**: 10000<br />**Full Mode**: 10000 |
| **Check constraints** | Specifies whether SSMA should check constraints when it inserts data into SQL Server tables.<br /><br />**Default Mode**: False<br />**Optimistic Mode**: False<br />**Full Mode**: False |
| **Data Migration Timeout** | Specifies the timeout used during data migration<br /><br />**Default Mode**: 15<br />**Optimistic Mode**: 15<br />**Full Mode**: 15 |
| **Extended Data Migration Options** | Shows extra data migration options for each table in separate detail tab.<br /><br />**Default Mode**: Hide<br />**Optimistic Mode**: Hide<br />**Full Mode**: Hide |
| **Fire triggers** | Specifies whether SSMA should fire insertion triggers when it adds data to SQL Server tables.<br /><br />**Default Mode**: False<br />**Optimistic Mode**: False<br />**Full Mode**: False |
| **Keep identity** | Specifies whether SSMA preserves null values in the source data when it adds data to SQL Server, regardless of the default values that are specified in SQL Server.<br /><br />**Default Mode**: True<br />**Optimistic Mode**: True<br />**Full Mode**: False |
| **Keep nulls** | Specifies whether SSMA preserves null values in the source data when it adds data to SQL Server, regardless of the default values that are specified in SQL Server.<br /><br />**Default Mode**: True<br />**Optimistic Mode**: True<br />**Full Mode**: True |
| **Mark string Trim operation with error** | If the target column size is less than the source string length, the value is trimmed and marked as an error.<br /><br />**Default Mode**: Yes<br />**Optimistic Mode**: Yes<br />**Full Mode**: Yes |
| **On Error** | Stops Data migration when an error occurs. It has three options:<br /><br />**Stop migration:** Stops data migration operation<br /><br />**Proceed to next table:** Stops data migration to the current table and proceeds to the next one<br /><br />**Proceed to next batch:** Stops data migration to the current batch and proceeds to the next one<br /><br />**Default Mode**: Proceed to the next batch<br />**Optimistic Mode**: Proceed to the next batch<br />**Full Mode**: Proceed to the next batch |
| **Replace unsupported dates** | Specifies whether SSMA should correct dates that are earlier than the earliest [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] **datetime** date (1 January 1753).<br /><br />To keep the current date values, select **Do nothing**. [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] doesn't accept dates before 1 January 1753 in a **datetime** column. If you use older dates, you must convert the datetime values to character values.<br /><br />To convert dates before 1 January 1753 to `NULL`, select **Replace with NULL**.<br /><br />To replace dates before 1 January 1753 with a supported date, select **Replace with nearest supported date**.<br /><br />**Default Mode**: Do nothing<br />**Optimistic Mode**: Do nothing<br />**Full Mode**: Replace with nearest supported date |
| **Table lock** | Specifies whether SSMA locks tables when it adds data to tables during data migration. Obtains a bulk update lock during the bulk copy operation. If the value is False, a lock is set at the row level.<br /><br />**Default Mode**: True<br />**Optimistic Mode**: True<br />**Full Mode**: True |

## Parallel data migration

| Term | Definition |
| --- | --- |
| **Parallel Data Migration Mode** | Specifies the mode used to fork threads to enable parallel data migration. In Auto mode, SSMA chooses the number of threads (10 by default) forked to migrate data. In Custom mode, user can specify the number of threads forked to migrate data (minimum is 1 and maximum is 100). Currently, only client side data migration engine supports parallel data migration.<br /><br />**Default Mode**: Auto<br />**Optimistic Mode**: Auto<br />**Full Mode**: Auto |

> [!IMPORTANT]  
> When the **Parallel Data Migration Mode** option is set to **Custom**, a new Project setting option **Thread Count** is displayed. It specifies number of threads used for data migration.
