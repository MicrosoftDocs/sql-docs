---
title: SQL Server Agent extension
description: Learn how to install and use the SQL Server Agent extension for Azure Data Studio—an extension for managing SQL Agent jobs and configurations.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan
ms.date: 09/24/2018
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom: seodec18
---

# SQL Server Agent extension (Preview)

The SQL Server Agent extension (preview) is an extension for managing and troubleshooting SQL Agent jobs and configuration. This extension is currently in preview.

Key actions include:

- List SQL Server Agent Jobs Configured on a SQL Server
- View Job History with job execution results
- Basic Job Control to start and stop jobs

## Install the SQL Server Agent extension

1. To open the extensions manager and access the available extensions, select the extensions icon, or select **Extensions** in the **View** menu.
2. Select an available extension to view its details.

   ![Install agent](media/sql-server-agent-extension/install-sql-agent.png)

3. Select the extension you want and **Install** it.
4. Select **Reload** to enable the extension (only required the first time you install an extension).
5. Navigate to your management dashboard by right-clicking your server or database and selecting **Manage**.
6. Installed extensions appear as tabs on your management dashboard:

   ![View agent](media/sql-server-agent-extension/view-sql-agent.png)

## View jobs

When you connect to the SQL Server Agent extension, the first thing you see is a list of all your Agent jobs.

   ![View jobs](media/sql-server-agent-extension/job-view.png)

## Next steps

To learn more about SQL Server Agent, [check our documentation.](../../ssms/agent/sql-server-agent.md)
