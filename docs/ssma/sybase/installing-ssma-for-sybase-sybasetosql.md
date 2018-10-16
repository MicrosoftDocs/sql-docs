---
title: "Installing SSMA for SAP ASE (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "11/29/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssma
ms.topic: conceptual
ms.assetid: 8d5a4ce6-b747-46e3-9184-645d56e8b35c
author: "Shamikg"
ms.author: "Shamikg"
manager: craigg
---
# Installing SSMA for SAP ASE (SybaseToSQL)
[!INCLUDE[msCoName](../../includes/msconame_md.md)][!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Migration Assistant (SSMA) for SAP Adaptive Server Enterprise (ASE) consists of a client application that you use to perform a migration from SAP ASE  to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or Azure SQL Database. It also contains an extension pack that supports data migration and the use of ASE system functions in your migrated databases.  
  
Install the client application on the computer from which you plan to perform the migration steps. Install the extension pack files on the computer that is running [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on which the migrated databases are to be hosted.  
  
## Upgrading SSMA for SAP ASE  
If you want to upgrade to a later version of SSMA for SAP ASE, you must first uninstall the client and the server extension pack. Then install the newer version.  
  
If you open a project from an earlier version of SSMA for SAP ASE, SSMA asks if you want to convert the project to the newer version. Click **Yes** to work with the project in the newer version of SSMA.  
  
## Contents  
  
|Article|Description|  
|---------|---------------|  
|[Installing the SSMA for SAP ASE Client &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-for-sybase-client-sybasetosql.md)|Provides information about and instructions for installing the SSMA for SAP ASE client.|  
|[Installing SSMA Components on SQL Server &#40;SybaseToSQL&#41;](../../ssma/sybase/installing-ssma-components-on-sql-server-sybasetosql.md)|Provides information about and instructions for installing the extension pack on instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|[Removing SSMA for SAP ASE Components &#40;SybaseToSQL&#41;](../../ssma/sybase/removing-ssma-for-sybase-components-sybasetosql.md)|Provides instructions for uninstalling the client program and extension pack.|  
  
## See also  
[Migrating SAP ASE databases to SQL Server - Azure SQL Database &#40;SybaseToSQL&#41;](../../ssma/sybase/migrating-sybase-ase-databases-to-sql-server-azure-sql-db-sybasetosql.md)  
