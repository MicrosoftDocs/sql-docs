---
title: Install SQL Server documentation to view offline
description: Learn how to install offline documentation for SQL Server 2019, 2017, 2016, 2014, and 2012. Use SQL Server Management Studio (SSMS) to view the offline content.
ms.service: sql
ms.subservice: install
ms.topic: conceptual
ms.assetid: 51f8a08c-51d0-41d8-8bc5-1cb4d42622fb
author: markingmyname
ms.author: maghan
ms.date: 11/15/2022
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017"
ms.custom:
  - intro-installation
---

# Install SQL Server documentation to view offline in SSMS

[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]

This article describes how to download and view offline SQL Server content in [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md). Offline content enables you to access the documentation without an internet connection (although an internet connection is initially required to download it).

## Overview

Offline documentation is available for versions of SQL Server 2012 and later. Although you can view content for [previous versions online](/previous-versions/sql/), an offline option provides a convenient way to access the older content.

- [SQL Server 2016 and later](#sql-server-2016-and-later-offline-content)
- [SQL Server 2014](#sql-server-2014-offline-content)
- [SQL Server 2012](#sql-server-2012-offline-content)

If your system doesn't have internet access and you want to install the offline content, first downlad the content on a system that has internet, and then move the package over to the offline system, using SSMS to locate the installation file path and load the files. 

## SQL Server 2016 and later offline content

The following steps explain how to load offline content for SQL Server 2016 and later by using SQL Server Management Studio (SSMS) that has access to the internet. 

1. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![Add and Remove Help Content](../sql-server/media/sql-server-offline-documentation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

2. To find the latest help content for SQL Server 2016 and later, under the **Manage Content** tab choose **Online** under the Installation source and then type in *sql server* in the search bar.

   ![SQL Server books search](../sql-server/media/sql-server-offline-documentation/sql-online-search.png)

   > [!Note]
   > The Local store path on the Manage Content tab shows where on the local computer the content is installed. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**. If the help installation fails after changing the Local store path, close and reopen Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

3. To install the latest help content for SQL Server 2016 and later, select **Add** next to each content package (book) that you want to install and then select **Update** in the lower right.

   ![SQL Server online books add and update](../sql-server/media/sql-server-offline-documentation/sql-add-update.png)

   > [!NOTE]
   > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

4. You can verify that the SQL Server 2016 and later content loaded by searching under the left content pane for *sql server 2016*.

   ![SQL Server 2016 books automatically updated](../sql-server/media/sql-server-offline-documentation/sql-2016-content.png)
   
5. (Optional) To move the content to an offline system, go to the **Local store path** where the files were installed, and move them to the offline system. Next, use the SSMS UI to install the content again, but this time choose **Disk** as the **Installation source** and provide the location where you saved the files after moving them. 

## SQL Server 2014 offline content

> [!IMPORTANT]
> SQL 2014 Transact-SQL content is only available offline.

The following steps explain how to load offline content for SQL Server 2014.

1. Download the [Product Documentation for Microsoft SQL Server 2014 for firewall and proxy restricted environments](https://www.microsoft.com/download/details.aspx?id=42557) content from the download center and save it to a folder.

2. Unzip the file to view the *.msha* file.

   ![SQL Server 2014 Help documentation setup file](../sql-server/media/sql-server-offline-documentation/sql-2014-help-content-setup-msha.png)

3. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![HelpViewer Add Remove Content](../sql-server/media/sql-server-offline-documentation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

4. To install the latest help content, choose **Disk** under Installation source and then the ellipses (...).

   ![Help Viewer Manage Content Disk Source](../sql-server/media/sql-server-offline-documentation/install-source-disk.png)

   > [!NOTE]
   > The Local store path on the Manage Content tab shows where on the local computer the content is located. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**.
   If the help installation fails after changing the Local store path, close and reopen the Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

5. Locate the folder where you unzipped the content. Select the **HelpContentSetup.msha** file in the folder then select **Open**.

   ![Open the SQL Server 2014 Help Content Setup.msha file](../sql-server/media/sql-server-offline-documentation/sql-2014-open-msha.png)

6. Type in *sql server 2014* in the search bar. Once you see the 2014 content available, select **Add** next to each content package (book) that you want to install to Help Viewer and then select **Update**.

   ![SQL Server 2014 books search in Help Viewer](../sql-server/media/sql-server-offline-documentation/sql-2014-search.png)

   ![SQL Server 2014 books add and update in Help Viewer](../sql-server/media/sql-server-offline-documentation/sql-2014-add-update.png)

    > [!NOTE]
    > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

7. You can verify that the SQL Server 2014 content installed by searching under the content pane on the left for *sql server 2014*.

   ![SQL Server 2014 books automatically updated](../sql-server/media/sql-server-offline-documentation/sql-2014-content.png)

## SQL Server 2012 offline content

The following steps explain how to load offline content for SQL Server 2012

1. Download the [Product Documentation for Microsoft SQL Server 2012 for firewall and proxy restricted environments](https://www.microsoft.com/download/details.aspx?id=35750) content from the download center and save it to a folder.

2. Unzip the file to view the *.msha* file.

   ![SQL Server 2012 Help content setup file](../sql-server/media/sql-server-offline-documentation/sql-2012-help-content-setup-msha.png)

3. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![HelpViewer Add Remove Content](../sql-server/media/sql-server-offline-documentation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

4. To install the latest help content, choose **Disk** under Installation source and then the ellipses (...).

   ![Help Viewer Manage Content Disk Source](../sql-server/media/sql-server-offline-documentation/install-source-disk.png)

   > [!NOTE]
   > The Local store path on the Manage Content tab shows where on the local computer the content is located. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**.
   If the help installation fails after changing the Local store path, close and reopen the Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

5. Locate the folder where you unzipped the content. Select the **HelpContentSetup.msha** file in the folder then select **Open**.

   ![Open the SQL Server 2012 Help Content Setup.msha file](../sql-server/media/sql-server-offline-documentation/sql-2012-open-msha.png)

6. Type in *sql server 2012* in the search bar. Once you see the 2012 content available, select **Add** next to each content package (book) that you want to install to Help Viewer and then select **Update**.

   ![SQL Server 2012 books search in Help Viewer](../sql-server/media/sql-server-offline-documentation/sql-2012-search.png)

   ![SQL Server 2012 books add and update in Help Viewer](../sql-server/media/sql-server-offline-documentation/sql-2012-add-update.png)

    > [!NOTE]
    > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

7. You can verify that the SQL Server 2012 content is loaded by searching under the content pane on the left for *sql server 2012*.

   ![SQL Server 2012 documentation automatically updated](../sql-server/media/sql-server-offline-documentation/sql-2012-content.png)

## View offline documentation

You can view SQL Server help content using the **HELP** menu in the latest version of [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md).

### View offline help content in SSMS

To view the installed help in SSMS, select **Launch in Help Viewer** from the Help menu, to launch the Help Viewer.

   ![Launch in Help Viewer](../sql-server/media/sql-server-offline-documentation/helpviewer-view-offline.png)  

Help Viewer opens to the Manage Content tab, with the installed help table of contents in the left pane. select articles in the table of contents to display them in the right pane.

> [!Important]
> If the contents pane is not visible, select Contents on the left margin. select the pushpin icon to keep the contents pane open.  

   ![Help Viewer with content](../sql-server/media/sql-server-offline-documentation/view-offline-all.png)

## Life-cycle policy

Review the Microsoft Product Lifecycle for information about how a specific product, service, or technology is supported:

- [Microsoft Lifecycle Policy](https://support.microsoft.com/lifecycle/selectindex)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

## Next steps

To learn more about archived content and Help viewer, reference the links below.

- [SQL Server online documentation](../sql-server/index.yml?view=sql-server-2016&preserve-view=true)
- [SQL Server 2014 online documentation](/previous-versions/sql/2014)
- [Previous versions of SQL Server online documentation](previous-versions-sql-server.md)
- [Versioning system for SQL documentation](../sql-server/versioning-system-monikers-ui-sql-server.md?view=sql-server-2016&preserve-view=true)
