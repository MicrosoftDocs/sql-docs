---
title: "VSCode and Azure Data Studio extensions for the Azure SQL Database local development experience"
description: Learn about the VSCode and Azure Data Studio extensions that enable the Azure SQL Database local development experience.
services: sql-database
ms.service: sql-database
author: scoriani
ms.author: scoriani
ms.reviewer: mathoma
ms.topic: conceptual 
ms.date: 05/05/2022
---

# VSCode and Azure Data Studio extensions for the Azure SQL Database local development experience

The Azure SQL Database local development experience allows application developers and database professionals to design, edit, build/validate, publish, and run database schemas for databases directly on their workstation using an Azure SQL Database containerized environment. The SQL Database local development experience utilizes extensions that are available for [VSCode](https://code.visualstudio.com/Docs) and [Azure Data Studio](/sql/azure-data-studio). 


## VSCode users

If you are using [VSCode](https://code.visualstudio.com/Docs), install the [mssql extension for VSCode](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql). 

This extension enables you to connect and run queries and testing scripts against a database. The database may be running in the Azure SQL Database emulator locally, or it may be a database in the global Azure SQL Database service. | Install the mssql extension.

The mssql extension for VSCode will automatically install the SQL Database Projects extension. The SQL Database Projects extension is bundled with the mssql extension so that it will automatically receive updates when you update the mssql extension.

To install the extension:

1. In VSCode, select **View** > **Command Palette**, or press **Ctrl**+**Shift**+**P**, or press **F1** to open the **Command Palette**.

2. In the **Command Palette**, select **Extensions: Install Extensions** from the dropdown.

3. In the **Extensions** pane, type **mssql**.

4. Select the **SQL Server (mssql)** extension, and then select **Install**.

5. After the installation completes, select **Reload** to enable the extension.

## Azure Data Studio users

If you are using [Azure Data Studio](/sql/azure-data-studio/download-azure-data-studio), the functionality from the mssql extension is provided natively by Azure Data Studio: there is no need to install it.

You need only install the [SQL Database Projects extension (Preview)](https://marketplace.visualstudio.com/items?itemName=ms-mssql.sql-database-projects-vscode). The SQL Database Projects extension enables you to capture an existing database schema and/or design new database objects using a declarative database design model. You can commit database schema to version control. You can also publish database schema to a database running in the Azure SQL Database emulator, or to a database running in the global Azure SQL Database service. You may publish an entire database, or incremental changes to a database.

To install the extension:

1. In Azure Data Studio, select **View** > **Command Palette**, or press **Ctrl**+**Shift**+**P**, or press **F1** to open the **Command Palette**.

2. In the **Command Palette**, select **Extensions: Install Extensions** from the dropdown.

3. In the **Extensions** pane, type **SQL Database Projects**.

4. Select the **SQL Database Projects** extension, and then select **Install**.

5. After the installation completes, select **Reload** to enable the extension.

## Next steps

Learn more about the local development experience for Azure SQL Database:

- [What is the local development experience for Azure SQL Database?](local-dev-experience-overview.md)
- [Create a database project for a local Azure SQL Database development environment](local-dev-experience-create-database-project.md)
- [Publish a database project for Azure SQL Database to the local emulator](local-dev-experience-publish-emulator.md)
- [Quickstart: Create a local development environment for Azure SQL Database](local-dev-experience-quickstart.md)
- [Introducing the Azure SQL Database emulator](local-dev-experience-azure-sql-database-emulator.md)