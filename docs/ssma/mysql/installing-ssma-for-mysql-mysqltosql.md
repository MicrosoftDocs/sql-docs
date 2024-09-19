---
title: "Installing SSMA for MySQL (MySQLToSQL)"
description: Use these articles to install, upgrade, and uninstall SQL Server Migration Assistant (SSMA) for MySQL, which includes a client application and extension pack.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 07/10/2023
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
ms.custom:
  - intro-installation
helpviewer_keywords:
  - "Installing SSMA 2008, Upgrading"
---
# Install SSMA for MySQL (MySQLToSQL)

SQL Server Migration Assistant (SSMA) for MySQL consists of a client application that you use to perform a migration from MySQL to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or SQL Azure. It also contains an extension pack that supports data migration and the use of MySQL system functions in your migrated databases.

Install the client application on the computer from which you will perform the migration steps. You must install the extension pack files on the computer where the migrated databases will be hosted. That computer must be running SQL Server.

## Upgrade SSMA for MySQL

If you want to upgrade to a later version of SSMA for MySQL, you must first uninstall the client and the server extension pack, and then install the newer version.

## Contents

| Article | Description |
| --- | --- |
| [Installing SSMA for MySQL Client (MySQLToSQL)](installing-ssma-for-mysql-client-mysqltosql.md) | Provides information about and instructions for installing the SSMA client. |
| [Installing SSMA Components on SQL Server (MySQL to SQL)](installing-ssma-components-on-sql-server-mysqltosql.md) | Provides information about and instructions for installing the extension pack on instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Removing the SSMA for MySQL Components (MySQLToSQL)](removing-the-ssma-for-mysql-components-mysqltosql.md) | Provides instructions for uninstalling the client program. |

## See also

- [Migrating MySQL Databases to SQL Server - Azure SQL Database (MySQLToSQL)](migrating-mysql-databases-to-sql-server-azure-sql-db-mysqltosql.md)
