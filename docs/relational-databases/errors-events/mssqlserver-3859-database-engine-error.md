---
description: "MSSQLSERVER_3859"
title: MSSQLSERVER_3859
ms.custom: ""
ms.date: 08/20/2020
ms.service: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3859 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni
---
# MSSQLSERVER_3859
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|3859|
|Event Source|MSSQLSERVER|
|Component|SQLEngine|
|Symbolic Name|DBCC_CHECKCAT_DIRECT_UPDATE|
|Message Text|Warning: The system catalog was updated directly in database ID \%d, most recently at %S_DATE|

## Explanation

This error indicates a user initiated changes to system tables. Manually updating system tables is not supported. The system tables should only be updated by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine. When [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] detects user initiated changes to the system tables, error 3859 is raised in the following two scenarios:

- Scenario 1

    An event that resembles the following is logged in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Error Log or in the Application log in Event Viewer when you start a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database that contains a system table that was manually updated:

    > Log Name: Application  
    Source: MSSQLSERVER
    Event ID: 3859  
    Task Category: Server  
    Level: Information  
    Description: Warning: The system catalog was updated directly in database ID \%d, most recently at **date_time**  

- Scenario 2  

    The following warning message is returned when you execute the `DBCC_CHECKDB` command after a system table is manually updated:

    > DBCC results for '**database_name**'.  
    Msg 8992, Level 16, State 1, Line 1  
    Check Catalog Msg 3859, State 1: Warning: The system catalog was updated directly in database ID \%d, most recently at **date_time**.  
    CHECKDB found 0 allocation errors and 0 consistency errors in database '**db_name**'.  
    DBCC execution completed. If DBCC printed error messages, contact your system administrator.

## User action

To resolve this issue, use one of the following methods.

- Method 1

    If you have a clean backup of the database, restore the database from the backup.  
    > [!NOTE]
    > This method works only if the backup does not have inconsistencies in the metadata.  

- Method 2  

    If you cannot restore the database from a backup, export the data and the objects to a new database. Then, transfer the contents of the manually-updated database into the new database. Note You cannot repair inconsistencies in the system catalogs by using the REPAIR options in the DBCC CHECKDB commands. Therefore, because the command cannot repair metadata corruption, the command does not provide any recommended repair level.

    > [!NOTE]
    > You can view the data in the system tables through the system catalog views.

## More information

For more information you can, see: [System Base Tables](../system-tables/system-base-tables.md).
