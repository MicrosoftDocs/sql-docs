---
title: Server-Level Firewall Rules
description: Server-level firewall rules
author: dalechen
ms.author: ninarn
ms.reviewer: vanto
ms.date: 01/14/2025
ms.service: azure-sql-database
ms.topic: include
ms.custom:
  - develop apps
keywords:
  - sql connection
  - connection string
---

1. Sign in to the [Azure portal](https://portal.azure.com/).

1. In the list on the left, select **All services**.

1. Scroll and select **SQL servers**.

   :::image type="content" source="media/sql-database-include-ip-address-22-v12portal/firewall-ip-b21-v12portal-findsvr.png" alt-text="Screenshot of Find your Azure SQL Database server in the portal." lightbox="media/sql-database-include-ip-address-22-v12portal/firewall-ip-b21-v12portal-findsvr.png":::

1. In the filter text box, start typing the name of your server. Your row is displayed.

1. Select the row for your server. A pane for your server is displayed.

1. On your server pane, select **Settings**.

1. Select **Firewall**.

    :::image type="content" source="media/sql-database-include-ip-address-22-v12portal/firewall-ip-b31-v12portal-settingsfirewall.png" alt-text="Screenshot of Select Settings > Firewall." lightbox="media/sql-database-include-ip-address-22-v12portal/firewall-ip-b31-v12portal-settingsfirewall.png":::

1. Select **Add Client IP**. Type a name for your new rule in the first text box.

1. Type in the low and high IP address values for the range you want to enable.

   - It can be handy to have the low value end with `.0` and the high value end with `.255`.

1. Select **Save**.

<!--
These includes/ files are a sequenced set, but you can pick and choose:

includes/sql-database-include-ip-address-22-v12portal.md
? includes/sql-database-include-ip-address-*.md
-->
