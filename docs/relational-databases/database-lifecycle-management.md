---
title: "Database Lifecycle Management"
description: Learn how to use database lifecycle management in SQL Server to manage databases and data assets for performance, protection, availability, and cost.
author: MashaMSFT
ms.author: mathoma
ms.date: 09/23/2024
ms.service: sql
ms.subservice: supportability
ms.topic: conceptual
helpviewer_keywords:
  - "Data sync"
  - "SQL Database"
  - "Azure Training Kit"
  - "Database development"
  - "Database backup"
  - "Database connection management"
  - "Database community"
  - "Backup and restore"
  - "Database import and export"
  - "Azure Service Dashboard"
  - "SQL Server Management Studio"
  - "Database management"
  - "Database export"
  - "SQL Server Data Tools"
  - "SSMS"
  - "SSDT"
  - "Database migration"
  - "Database connectivity"
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database Lifecycle Management
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Database lifecycle management (DLM) is a policy-based approach to managing databases and data assets. DLM isn't a product but a comprehensive approach to managing the database schema, data, and metadata for a database application. A thoughtful and proactive approach to DLM enables an organization to manage data resources according to appropriate levels of performance, protection, availability, and cost.  
  
 DLM begins with discussion of project design and intent, continues with database develop, test, build, deploy, maintain, monitor, and backup activities, and ends with data archive. This article provides an overview of the stages of DLM that begin with database development and progress through build, deploy, and monitor actions. Also included are data management activities, and data portability operations like import/export, backup, migrate, and sync.  
  
 For tools, tutorials, and more information, see the following articles:

**Database projects**

- [Install SQL Server Data Tools (SSDT) for Visual Studio](../ssdt/download-sql-server-data-tools-ssdt.md)
- [Azure SQL Database Projects extension](/azure-data-studio/extensions/sql-database-project-extension)
- [Getting started with the SQL Database Projects extension](/azure-data-studio/extensions/sql-database-project-extension-getting-started)

**SqlPackage**

- [SqlPackage](../tools/sqlpackage/sqlpackage.md)
- [SqlPackage Export parameters and properties](../tools/sqlpackage/sqlpackage-export.md)
- [SqlPackage Import parameters and properties](../tools/sqlpackage/sqlpackage-import.md)
- [DaxFx on GitHub](https://github.com/microsoft/DacFx)

**BACPAC**

- [Export to a BACPAC file - Azure SQL Database and Azure SQL Managed Instance](/azure/azure-sql/database/database-export)
- [Import a BACPAC File to Create a New User Database](data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database.md)
- [Tutorial: Import SQL BACPAC files with ARM templates](/azure/azure-resource-manager/templates/template-tutorial-deploy-sql-extensions-bacpac)

**DACPAC**

- [Data-tier applications (DAC)](data-tier-applications/data-tier-applications.md)
- [What are SQL database projects?](../tools/sql-database-projects/sql-database-projects.md)
- [Extract, Publish, and Register .dacpac Files](../ssdt/extract-publish-and-register-dacpac-files.md)
- [Import a BACPAC File to Create a New User Database](data-tier-applications/import-a-bacpac-file-to-create-a-new-user-database.md)
- [Deploy a Database By Using a DAC](data-tier-applications/deploy-a-database-by-using-a-dac.md)

## Related content

- [Microsoft Modern Lifecycle Policy](/lifecycle/policies/modern)