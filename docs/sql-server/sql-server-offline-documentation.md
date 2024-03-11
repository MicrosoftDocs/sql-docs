---
title: Install SQL Server documentation to view offline
description: Learn how to install offline documentation for SQL Server. Use SQL Server Management Studio (SSMS) to view the offline content.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 03/11/2024
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.custom:
  - intro-installation
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017"
---

# Install SQL Server documentation to view offline in SSMS

[!INCLUDE [sqlserver](../includes/applies-to-version/sqlserver.md)]

This article describes how to download and view offline SQL Server content in [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md). Offline content enables you to access the documentation without an internet connection (although an internet connection is initially required to download it).

## Overview

Offline documentation is available for versions of [!INCLUDE [sssql11-md](../includes/sssql11-md.md)] and later versions. Although you can view content for [previous versions online](/previous-versions/sql/), an offline option provides a convenient way to access the older content.

- [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later versions
- [!INCLUDE [sssql14-md](../includes/sssql14-md.md)]
- [!INCLUDE [sssql11-md](../includes/sssql11-md.md)]

If your system doesn't have internet access and you want to install the offline content, first download the content on a system that has internet access, and then move the package over to the offline system. Use SSMS to locate the installation file path and load the files.

## Offline content for SQL Server

The following steps explain how to load offline content for [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)], by using SQL Server Management Studio (SSMS) that has access to the internet.

### [SQL Server 2016 and later versions](#tab/sqlserver2016)

This section describes how to load offline content for [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later versions.

1. In SSMS, select **Add and Remove Help Content** on the Help menu.

   :::image type="content" source="media/sql-server-offline-documentation/add-remove-content.png" alt-text="Screenshot of Add and Remove Help Content.":::

   The Help Viewer opens to the Manage Content tab.

1. To find the latest help content for [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later versions, under the **Manage Content** tab choose **Online** under the Installation source and then type in *sql server* in the search bar.

   :::image type="content" source="media/sql-server-offline-documentation/sql-online-search.png" alt-text="Screenshot of SQL Server books search." lightbox="media/sql-server-offline-documentation/sql-online-search.png":::

   > [!NOTE]  
   > The Local store path on the Manage Content tab shows where on the local computer the content is installed. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**. If the help installation fails after changing the Local store path, close and reopen Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

1. To install the latest help content for [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later versions, select **Add** next to each content package (book) that you want to install and then select **Update** in the lower right.

   :::image type="content" source="media/sql-server-offline-documentation/sql-add-update.png" alt-text="Screenshot of SQL Server online books add and update." lightbox="media/sql-server-offline-documentation/sql-add-update.png":::

   > [!NOTE]  
   > If the Help Viewer freezes (hangs) while adding content, change the `Cache LastRefreshed="<mm/dd/yyyy> 00:00:00"` line in the `%LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings` or `HlpViewer_VisualStudiox_en-US.settings` file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

1. You can verify that the [!INCLUDE [sssql16-md](../includes/sssql16-md.md)] and later version content loaded, by searching under the left content pane for `sql server <nnnn>`, where `<nnnn>` is the version you installed.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2016-content.png" alt-text="Screenshot of SQL Server 2016 documentation automatically updated." lightbox="media/sql-server-offline-documentation/sql-2016-content.png":::

1. (Optional) To move the content to an offline system, go to the **Local store path** (mentioned in step 2) where the files were installed. Copy the ContentStore and IndexStore folders to **new** folder in another location. After copying, zip up the folders and their contents, then copy them to the offline system.

1. On the offline system, open SSMS, select **Add and Remove Help Content** on the Help menu. Go to the **Manage Content** tab and note the location for the Local store path. Close SSMS.

1. Extract the contents of the zip file. Copy the contents of the ContentStore folder into the ContentStore folder within the Local store path.

   > [!NOTE]  
   > The installedBooks.*.xml file should be larger than 1 KB. If there are two files with the same names, rename the smaller file to `.old` by changing the file extension.

1. Copy the contents of the IndexStore folder into the IndexStore folder within the local store path.

1. Open SSMS, select **Add and Remove Help Content** on the Help menu to view the documentation.

### [SQL Server 2014](#tab/sqlserver2014)

This section describes how to load offline content for [!INCLUDE [sssql14-md](../includes/sssql14-md.md)].

> [!IMPORTANT]  
> [!INCLUDE [sssql14-md](../includes/sssql14-md.md)] Transact-SQL content is only available offline.

1. Download the [Product Documentation for Microsoft SQL Server 2014 for firewall and proxy restricted environments](https://www.microsoft.com/download/details.aspx?id=42557) content from the download center and save it to a folder.

1. Unzip the file to view the `*.msha*` file.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2014-help-content-setup-msha.png" alt-text="Screenshot of SQL Server 2014 Help documentation setup file." lightbox="media/sql-server-offline-documentation/sql-2014-help-content-setup-msha.png":::

1. In SSMS, select **Add and Remove Help Content** on the Help menu.

   :::image type="content" source="media/sql-server-offline-documentation/add-remove-content.png" alt-text="Screenshot of HelpViewer Add Remove Content.":::

   The Help Viewer opens to the Manage Content tab.

1. To install the latest help content, choose **Disk** under Installation source and then the ellipses (...).

   :::image type="content" source="media/sql-server-offline-documentation/install-source-disk.png" alt-text="Screenshot of Help Viewer Manage Content Disk Source." lightbox="media/sql-server-offline-documentation/install-source-disk.png":::

   > [!NOTE]  
   > The Local store path on the Manage Content tab shows where on the local computer the content is located. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**.
   If the help installation fails after changing the Local store path, close and reopen the Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

1. Locate the folder where you unzipped the content. Select the `HelpContentSetup.msha` file in the folder then select **Open**.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2014-open-msha.png" alt-text="Screenshot of Open the SQL Server 2014 Help Content Setup.msha file." lightbox="media/sql-server-offline-documentation/sql-2014-open-msha.png":::

1. Type in *sql server 2014* in the search bar. Once you see the 2014 content available, select **Add** next to each content package (book) that you want to install to Help Viewer and then select **Update**.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2014-search.png" alt-text="Screenshot of SQL Server 2014 books search in Help Viewer." lightbox="media/sql-server-offline-documentation/sql-2014-search.png":::

   :::image type="content" source="media/sql-server-offline-documentation/sql-2014-add-update.png" alt-text="Screenshot of SQL Server 2014 books add and update in Help Viewer." lightbox="media/sql-server-offline-documentation/sql-2014-add-update.png":::

    > [!NOTE]  
    > If the Help Viewer freezes (hangs) while adding content, change the `Cache LastRefreshed="<mm/dd/yyyy> 00:00:00"` line in the `%LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings` or `HlpViewer_VisualStudiox_en-US.settings` file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

1. You can verify that the SQL Server 2014 content installed by searching under the content pane on the left for *sql server 2014*.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2014-content.png" alt-text="Screenshot of SQL Server 2014 books automatically updated." lightbox="media/sql-server-offline-documentation/sql-2014-content.png":::

### [SQL Server 2012](#tab/sqlserver2012)

This section describes how to load offline content for [!INCLUDE [sssql11-md](../includes/sssql11-md.md)].

1. Download the [Product Documentation for Microsoft SQL Server 2012 for firewall and proxy restricted environments](https://www.microsoft.com/download/details.aspx?id=35750) content from the download center and save it to a folder.

1. Unzip the file to view the `*.msha*` file.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2012-help-content-setup-msha.png" alt-text="Screenshot of SQL Server 2012 Help content setup file.":::

1. In SSMS, select **Add and Remove Help Content** on the Help menu.

   :::image type="content" source="media/sql-server-offline-documentation/add-remove-content.png" alt-text="Screenshot of HelpViewer Add Remove Content.":::

   The Help Viewer opens to the Manage Content tab.

1. To install the latest help content, choose **Disk** under Installation source and then the ellipses (...).

   :::image type="content" source="media/sql-server-offline-documentation/install-source-disk.png" alt-text="Screenshot of Help Viewer Manage Content Disk Source." lightbox="media/sql-server-offline-documentation/install-source-disk.png":::

   > [!NOTE]  
   > The Local store path on the Manage Content tab shows where on the local computer the content is located. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**.
   If the help installation fails after changing the Local store path, close and reopen the Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

1. Locate the folder where you unzipped the content. Select the `HelpContentSetup.msha` file in the folder then select **Open**.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2012-open-msha.png" alt-text="Screenshot of Open the SQL Server 2012 Help Content Setup.msha file." lightbox="media/sql-server-offline-documentation/sql-2012-open-msha.png":::

1. Type in *sql server 2012* in the search bar. Once you see the 2012 content available, select **Add** next to each content package (book) that you want to install to Help Viewer and then select **Update**.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2012-search.png" alt-text="Screenshot of SQL Server 2012 documentation search in Help Viewer." lightbox="media/sql-server-offline-documentation/sql-2012-search.png":::

   :::image type="content" source="media/sql-server-offline-documentation/sql-2012-add-update.png" alt-text="Screenshot of SQL Server 2012 documentation add and update in Help Viewer." lightbox="media/sql-server-offline-documentation/sql-2012-add-update.png":::

   > [!NOTE]  
   > If the Help Viewer freezes (hangs) while adding content, change the `Cache LastRefreshed="<mm/dd/yyyy> 00:00:00"` line in the `%LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings` or `HlpViewer_VisualStudiox_en-US.settings` file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

1. You can verify that the [!INCLUDE [sssql11-md](../includes/sssql11-md.md)] content is loaded by searching under the content pane on the left for *sql server 2012*.

   :::image type="content" source="media/sql-server-offline-documentation/sql-2012-content.png" alt-text="Screenshot of SQL Server 2012 documentation automatically updated." lightbox="media/sql-server-offline-documentation/sql-2012-content.png":::

---

## View offline documentation

You can view SQL Server help content using the **Help** menu in the latest version of [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md).

### View offline help content in SSMS

To view the installed help in SSMS, select **Launch in Help Viewer** from the Help menu, to launch the Help Viewer.

:::image type="content" source="media/sql-server-offline-documentation/helpviewer-view-offline.png" alt-text="Screenshot of Launch in Help Viewer." lightbox="media/sql-server-offline-documentation/helpviewer-view-offline.png":::

Help Viewer opens to the Manage Content tab, with the installed help table of contents in the left pane. Select **Articles** in the table of contents to display them in the contents pane.

> [!IMPORTANT]  
> If the contents pane isn't visible, select **Contents** on the left margin. Select the pushpin icon to keep the contents pane open.

:::image type="content" source="media/sql-server-offline-documentation/view-offline-all.png" alt-text="Screenshot of Help Viewer with content." lightbox="media/sql-server-offline-documentation/view-offline-all.png":::

## Lifecycle policy

Review the Microsoft Product Lifecycle for information about how a specific product, service, or technology is supported:

- [Microsoft Lifecycle Policy](/lifecycle/products)

[!INCLUDE [get-help-options](../includes/paragraph-content/get-help-options.md)]

## Related content

- [Online documentation for SQL Server 2016 and later versions](index.yml)
- [SQL Server 2014 online documentation](/previous-versions/sql/2014)
- [Previous versions of SQL Server documentation](previous-versions-sql-server.md)
- [Versioning system for SQL documentation](versioning-system-monikers-ui-sql-server.md)
