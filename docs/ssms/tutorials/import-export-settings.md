---
title: Import and Export SSMS Settings"
description: Learn how to import and export settings for SQL Server Management Studio (SSMS).
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 07/12/2024
ms.service: sql
ms.subservice: ssms
ms.topic: tutorial
keywords:
  - SQL Server
  - SSMS
  - SQL Server Management Studio
---

# Import and export settings for SQL Server Management Studio(SSMS)

This tutorial describes how to import settings from another installation of SQL Server Management Studio (SSMS), and how to export settings from SSMS. In this article, you learn how to:

> [!div class="checklist"]

> * Import settings
> * Export settings
> * Reset all settings

### Import user settings

The first time you open SSMS after installing a major release you are prompted to import user settings:

:::image type="content" source="media/import-export-settings/import-user-settings.png" alt-text="Import User Settings dialog":::

The **Import User Settings** dialog only appears on the first launch of a newly installed version. For example, if you have SSMS 19.3 installed on your machine and install SSMS 20.2, the dialog appears. If you have SSMS 20.1 installed and you install SSMS 20.2, the import dialog doesn't appear.

For a new installation of SSMS 20, you can import settings from SSMS 18 or SSMS 19, or not import any settings.

Choosing to import settings in this dialog only imports connection settings. After the import is complete, open the connection dialog. Within the **Server name** drop-down, you see the list of servers that exists in the version of SSMS from which you imported settings. The connection settings for each server, such as **Authentication** and **User name**, are also imported.

### Import environment settings

1. To import environment settings from another installation of SSMS, select **Tools** > **Import and Export Settings...**.
1. In the welcome dialog, select **Import selected environment settings**, then select **Next>**.
1. If there are changes to environment settings that you want to save, select **Yes, save my current settings**, and enter a filename and location for the settings file to be saved. Otherwise, select **No, just import new settings, overwriting my current settings**. Select **Next>**.
1. Choose the settings file that you want to import, and select **Next>**.
1. Choose the settings to import, then select **Finish**.

### Export environment settings

1. To export environment settings from an existing installation of SSMS, select **Tools** > **Import and Export Settings...**.
1. In the welcome dialog, select **Export selected environment settings**, then select **Next>**.
1. Choose the settings to export, then select **Next>**.
1. Entire a filename and location for the settings file to be saved, then select **Finish**.

### Reset settings to the default

1. To restore default settings, select **Tools** > **Import and Export Settings...**.
1. In the welcome dialog, select **Reset all settings**, then **Next>**.
1. If there are changes to environment settings that you want to save, select **Yes, save my current settings** and enter a filename and location for the settings file to be saved. Otherwise, select **No, just import new settings, overwriting my current settings**. Select **Next>**.
1. Choose collection of settings to reset to, then select **Finish**.

## Next steps

The best way to get acquainted with SSMS is through hands-on practice. These *tutorial* and *how-to* articles help you with various features available within SSMS.  These articles teach you how to manage the components of SSMS and how to find the features that you use regularly.

* [Connect to and query an instance](../quickstarts/ssms-connect-query-sql-server.md)
* [SSMS components and configuration](ssms-configuration.md)
* [Scripting](scripting-ssms.md)
* [Using Templates in SSMS](../template/templates-ssms.md)
* [Tips and tricks for SSMS](ssms-tricks.md)