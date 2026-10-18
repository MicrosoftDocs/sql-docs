---
title: Obtain the Connection String from the Azure Portal
description: Use the Azure portal to obtain the connection string that's necessary for your client program to interact with Azure SQL Database.
author: dalechen
ms.author: ninarn
ms.reviewer: vanto
ms.date: 01/14/2025
ms.service: azure-sql-database
ms.topic: include
keywords:
  - sql connection
  - connection string
ms.custom:
  - develop apps
  - sfi-ropc-nochange
---

### Obtain the connection string from the Azure portal

Use the [Azure portal](https://portal.azure.com/) to obtain the connection string that's necessary for your client program to interact with Azure SQL Database.

1. Select **All services** > **SQL databases**.

1. Enter the name of your database into the filter text box near the upper left of the **SQL databases** pane.

1. Select the row for your database.

1. After the pane appears for your database, for visual convenience select the **Minimize** buttons to collapse the blades you used for browsing and database filtering.

1. On the pane for your database, select **Show database connection strings**.

1. Copy the appropriate connection string. For example, if you intend to use the ADO.NET connection library, copy the appropriate string from the **ADO.NET** tab.

   :::image type="content" source="media/sql-database-include-connection-string-20-portalshots/connqry-connstr-b.png" alt-text="Screenshot showing how to copy the ADO connection string for your database." lightbox="media/sql-database-include-connection-string-20-portalshots/connqry-connstr-b.png":::

1. Edit the connection string as needed. In this example, insert your password into the connection string, or remove `<server-name>` from the username if the username or server name are too long.

1. In one format or another, paste the connection string information into your client program code.

For more information, see [Connection strings and configuration files](/dotnet/framework/data/adonet/connection-strings-and-configuration-files).

<!--
These three includes/ files are a sequenced set, but you can pick and choose:

includes/sql-database-include-connection-string-20-portalshots.md
includes/sql-database-include-connection-string-30-compare.md
includes/sql-database-include-connection-string-40-config.md
-->
