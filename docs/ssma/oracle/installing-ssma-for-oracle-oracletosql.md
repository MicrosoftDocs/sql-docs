---
title: "Installing SSMA  for Oracle (OracleToSQL)"
description: Use these articles to install, upgrade, and uninstall SQL Server Migration Assistant (SSMA) for Oracle, which includes a client application and an extension pack.
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
---
# Install SSMA for Oracle (OracleToSQL)

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for Oracle consists of a client application that you use to perform a migration from Oracle to [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and Azure SQL Database. It also contains an extension pack that supports data migration and the use of Oracle system functions in your migrated databases.

You install the client application on the computer from which you perform the migration steps. You must install the extension pack files on the computer where the migrated databases will be hosted. That computer must be running [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Upgrade SSMA for Oracle

If you want to upgrade to a later version of SSMA for Oracle, you must first uninstall the client and the server extension pack, and then install the newer version.

## Contents

| Article | Description |
| --- | --- |
| [Installing SSMA for Oracle Client (OracleToSQL)](installing-ssma-for-oracle-client-oracletosql.md) | Provides information about and instructions for installing the SSMA client. |
| [Installing SSMA Components on SQL Server (OracleToSQL)](installing-ssma-components-on-sql-server-oracletosql.md) | Provides information about and instructions for installing the extension pack on instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. |
| [Removing SSMA for Oracle Components (OracleToSQL)](removing-ssma-for-oracle-components-oracletosql.md) | Provides instructions for uninstalling the client program and extension pack. |

## See also

- [Migrating Oracle Databases to SQL Server (OracleToSQL)](migrating-oracle-databases-to-sql-server-oracletosql.md)
