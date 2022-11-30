---
title: MySQL extension
description: Learn how to install the MySQL extension for Azure Data Studio. It enables you to connect to, query, and develop for MySQL databases hosted on-premises, on VMs, on other clouds or on Azure Database for MySQL - Flexible Server. 
author: shreyaaithal
ms.author: shaithal
ms.reviewer: erinstellato
ms.date: 10/12/2022
ms.service: azure-data-studio
ms.topic: conceptual
ms.custom: ignite-2022
---

# MySQL extension (Preview)

With the MySQL extension (preview) for Azure Data Studio, you can now connect to, query and manage MySQL databases along with your other databases, taking advantage of the modern editor experience and capabilities in Azure Data Studio, such as IntelliSense, code snippets, source control integration, native Jupyter Notebooks, an integrated terminal, and more.

The Azure Data Studio functionality available for MySQL includes:

- Connection manager, allowing you to connect to any MySQL server hosted on-premises, on virtual machines, on managed MySQL in other clouds, and on Azure Database for MySQL – Flexible Server.
- Searchable object explorer view for database objects, with auto-completion
- Query authoring and editing with Intellisense, syntax highlighting and code snippets
- Ability to query results and save to csv, JSON, xml, or Excel
- [Integrated terminal for Bash, PowerShell, and cmd.exe](../integrated-terminal.md)
- [Source control integration with Git](../source-control.md)
- [Customizable dashboards and insight widgets](../insight-widgets.md)
- [Customizable keyboard shortcuts](../keyboard-shortcuts.md), multi-tab support, color theme options, etc.
- [Server groups](../server-groups.md) for organizing connections

## Install the MySQL extension (preview)

If you don't already have Azure Data Studio installed, see its [install instructions](../download-azure-data-studio.md).

1. Select the extensions icon from the sidebar in Azure Data Studio.

    :::image type="content" source="media/mysql-extension/extensions-icon.png" alt-text="Screenshot of the extension's icon.":::

2. Search for the **MySQL** and select it.

3. Select **Install**. Once installed, select **Reload** to activate the extension in Azure Data Studio.

## Next steps

Learn how to [connect and query MySQL using Azure Data Studio](../quickstart-mysql.md).
