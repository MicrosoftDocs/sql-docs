---
description: "MSSQLSERVER_17659"
title: MSSQLSERVER_17659
ms.custom: ""
ms.date: 08/20/2020
ms.prod: sql
ms.reviewer: ramakoni1, pijocoder, suresh-kandoth, Masha
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "17659 (Database Engine error)"
ms.assetid: 
author: suresh-kandoth
ms.author: ramakoni,sureshka
---
# MSSQLSERVER_17659

 [!INCLUDE [SQL Server](../../includes/ssnoversion-md.md)]
 [!INCLUDE [SQL Server 2019](../../includes/sssqlv15-md.md)]
 [!INCLUDE [SQL Server 2017](../../includes/sssql17-md.md)]
 [!INCLUDE [SQL Server 2016](../../includes/sssql15-md.md)]
 [!INCLUDE [SQL Server 2014](../../includes/sssql14-md.md)]
 [!INCLUDE [SQL Server 2012](../../includes/sssql11-md.md)]
 [!INCLUDE [SQL Server 2008](../../includes/sskatmai-md.md)]
 [!INCLUDE [Azure SQL DB](../../includes/sssdsfull-md.md)]

## Details

|Attribute|Value|
|---|---|
|Product Name|SQL Server|
|Event ID|17659|
|Event Source|MSSQLSERVER|
|Component|SQL SQLEngine|
|Symbolic Name|DEMO_SYSCATUPDATE|
|Message Text|System table ID \%d has been updated directly in database ID \%d and cache coherence may not have been maintained. <br/> SQL Server should be restarted.|
||

## Explanation

This error indicates that a system object was updated directly. Manually updating system tables is not supported. The system tables should only be updated by the SQL Server database engine. When SQL Server detects user initiated changes to the system tables, error 17659 is raised. An event that resembles the following is logged in the SQL Server Error Log or in the Application log in Event Viewer in this scenario.

> Log Name: Application  
Source: MSSQLServer  
Event ID: 17659  
Task Category: Server  
Level: Information  
Description: Warning: System table ID \%d has been updated directly in database ID %d and cache coherence may not have been maintained. SQL Server should be restarted.

## User action

To resolve this issue, use one of the following methods.

- Method 1  
    If you have a clean backup of the database, restore the database from the backup.  
    > [!NOTE] This method works only if the backup does not have inconsistencies in the metadata.  

- Method 2  
    If you cannot restore the database from a backup, export the data and the objects to a new database. Then, transfer the contents of the manually-updated database into the new database. Note You cannot repair inconsistencies in the system catalogs by using the REPAIR options in the DBCC CHECKDB commands. Therefore, because the command cannot repair metadata corruption, the command does not provide any recommended repair leve

> [!NOTE]
> You can view the data in the system tables through the system catalog views.

## More information

For more information you can, see: [System Base Tables](/sql/relational-databases/system-tables/system-base-tables).
