---
title: Create a project for a local Azure SQL Database development environment
description: Learn how to create a database project as part of the local development experience for Azure SQL Database.
author: scoriani
ms.author: scoriani
ms.reviewer: mathoma
ms.date: 05/24/2022
ms.service: sql-database
ms.topic: how-to
ms.custom: template-how-to
---

# Create a project for a local Azure SQL Database development environment
[!INCLUDE[appliesto-sqldb](../includes/appliesto-sqldb.md)]

The Azure SQL Database [local development experience](local-dev-experience-overview.md) empowers application developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases directly on their workstation using an Azure SQL Database containerized environment. As part of this workflow, you will create a SQL Database Project. The SQL Database Project extension allows you to create a new blank project, create a new project from a database, and open previously created projects. 

## Prerequisites

Before creating or opening a SQL Database project, follow the steps in [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md) to configure your environment.

## Create a new project

In the **Projects** view select the **New Project** button and enter a project name in the text input that appears.  In the **Select a Folder** dialog that appears, choose a directory for the project's folder, .sqlproj file, and other contents to reside in.

The empty project is opened and visible in the **Projects** view for editing.

## Create a project from Azure SQL Database

In the **Project** view, select the **Import Project from Database** button and connect to a database in Azure SQL Database.  Once connected, select a database from the list of available databases and set the name of the project.

Finally, select a target structure of the extraction. The new project opens and contains SQL scripts for the contents of the selected database.

## Open an existing project

In the **Projects** view, select **Open Project** and open an existing *.sqlproj* file from the file picker that appears. Existing projects can originate from Azure Data Studio, Visual Studio Code or [Visual Studio SQL Server Data Tools](/sql/ssdt/sql-server-data-tools).

The existing project opens and its contents are visible in the **Projects** view for editing.

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)
- [Set up a local development environment for Azure SQL Database](local-dev-experience-set-up-dev-environment.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
- [Publish a database project for Azure SQL Database to the local emulator](local-dev-experience-publish-emulator.md)
- [Introducing the Azure SQL Database emulator](local-dev-experience-sql-database-emulator.md)
