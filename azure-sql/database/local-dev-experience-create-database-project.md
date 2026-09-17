---
title: Create a Project for a Local Development Environment
description: Learn how to create a database project as part of the local development experience for Azure SQL Database.
author: croblesm
ms.author: roblescarlos
ms.reviewer: mathoma, wiassaf, randolphwest
ms.date: 09/16/2026
ms.service: azure-sql-database
ms.topic: how-to
ms.custom:
  - template-how-to
  - build-2023
  - build-2023-dataai
monikerRange: "=azuresql || =azuresql-db"
---

# Create a project for a local Azure SQL Database development environment

[!INCLUDE [appliesto-sqldb](../includes/appliesto-sqldb.md)]

Use the Azure SQL Database [local development experience](local-dev-experience-overview.md) to design, edit, build, validate, publish, and run database schemas on your own workstation, against a local database engine that runs in a container. The schema lives in a SQL Database project. Use the SQL Database Projects extension to create an empty project, create a project from an existing database, or open a project you created previously.

## Prerequisites

Before creating or opening a SQL Database project, follow the steps in [Quickstart: Set up a development environment with Dev Container Templates for Azure SQL Database](local-dev-experience-dev-containers-quickstart.md) to configure your environment.

## Create a new project

In the **Database Projects** view, select the **New Project** button and enter a project name in the text input that appears. In the **Select a Folder** dialog that appears, choose a directory for the project's folder, `.sqlproj` file, and other contents.

The empty project opens in the **Database Projects** view for editing.

## Create a project from Azure SQL Database

In the **Project** view, select the **Import Project from Database** button and connect to a database in Azure SQL Database. After you connect, select a database from the list of available databases and name the project.

Finally, select a target structure of the extraction. The new project opens and contains SQL scripts for the contents of the selected database.

## Open an existing project

In the **Database Projects** view, select **Open Project** and open an existing `.sqlproj` file from the file picker that appears. Existing projects can originate from Visual Studio Code or [SQL Server Data Tools](/sql/ssdt/sql-server-data-tools).

The existing project opens and its contents are visible in the **Database Projects** view for editing.

## Related content

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)
- [Dev Container Templates for Azure SQL Database overview](local-dev-experience-dev-containers.md)
- [Quickstart: Set up a development environment with Dev Container Templates for Azure SQL Database](local-dev-experience-dev-containers-quickstart.md)
