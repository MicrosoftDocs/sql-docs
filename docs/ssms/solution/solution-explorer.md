---
title: "Solution Explorer"
description: "Solution Explorer"
author: "markingmyname"
ms.author: "maghan"
ms.date: 08/01/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
helpviewer_keywords:
  - "SQL Server Management Studio [SQL Server], solutions"
  - "projects [SQL Server Management Studio], about projects"
  - "SQL Server Management Studio [SQL Server], projects"
  - "solutions [SQL Server Management Studio], about solutions"
  - "SQL Server Management Studio [SQL Server], items"
  - "items [SQL Server]"
---

# Solution Explorer

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

The Solution Explorer pane in [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [ssManStudioFull](../../includes/ssmanstudiofull-md.md)] provides containers called projects for managing items such as database scripts, queries, data connections, and files. One or more projects that are related to each other can be combined in a container called a solution.

A solution includes one or more projects, plus files and metadata that help define the solution as a whole. A project is a set of files, plus related metadata such as connection information. Solutions and projects contain items that represent the scripts, queries, connection information and files that you need to create your database solution.

## Benefits of Using Solutions

Use these containers to:

- Implement source control on queries and scripts.

- Manage settings for your solution as a whole or for individual projects.

- Handle the details of file management while you focus on items that make up your database solution.

- Add items that are useful to multiple projects in the solution or to the solution without referencing the item in each project.

- Work on miscellaneous files that are independent from solutions or projects.

The items contained in projects depend on the project type and whether you are using SQL Server Management Studio.

## Related Tasks

Use the following topics to get started with SQL Server Solutions:

| Description | Topic |
| - | - |
| Describes how to collect one or more projects in a solution. | [Solutions (SQL Server Management Studio)](../../ssms/solution/solutions-sql-server-management-studio.md) |
| Describes how to create a project and add items like scripts and connections. | [Projects (SQL Server Management Studio)](../../ssms/solution/projects-sql-server-management-studio.md) |
| Provides information about the files used by SQL Server Management Studio to manage solutions and files. | [Files That Manage Solutions and Projects](../../ssms/solution/files-that-manage-solutions-and-projects.md) |
