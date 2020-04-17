---
title: Add SQL Server help content with Help Viewer
ms.prod: sql
ms.technology: 
ms.reviewer: ""
ms.topic: conceptual
ms.assetid: 51f8a08c-51d0-41d8-8bc5-1cb4d42622fb
author: markingmyname
ms.author: maghan
ms.custom: ""
ms.date: 04/20/2020
---

# Add different versions of SQL Server help documentation

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

The documentation for several older versions of Microsoft SQL Server has been archived. Yet the archived documentation is still available.

This article describes how to download and view offline SQL Server content in Help Viewer with [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md).

Internet access is required to download the Help Viewer content. You can then migrate the content to a computer that doesn't have internet access.

Offline content is available for these SQL Server versions:

- SQL Server 2019
- SQL Server 2017
- SQL Server 2016
- SQL Server 2014
- SQL Server 2012

## How to download and configure offline content

You can view the SQL Server help content by using the **HELP** menu in the latest version of Help Viewer in [SQL Server Management Studio (SSMS)](../ssms/download-sql-server-management-studio-ssms.md).

Below are steps on how to load offline content for different versions of SQL Server.

### Configure SQL Server 2019 offline content

For this approach, you use the **Online** Installation source.

1. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![Help Viewer Add Remove Content](../sql-server/media/sql-server-help-installation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

2. To find the latest help content for SQL Server 2019, under the **Manage Content** tab choose **Online** under the Installation source and then type in *sql server 2019* in the search bar.

   ![SQL Server 2019 documentation search in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2019-search.png)

   > [!Note]
   > The Local store path on the Manage Content tab shows where on the local computer the content is installed. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**. If the help installation fails after changing the Local store path, close and reopen Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

3. To install the latest help content package for SQL Server 2019, select **Add** next to each content package (book) that you want to install and then select **Update** in the lower right.

   ![SQL Server 2019 books add and update in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2019-add-update.png)

   > [!NOTE]
   > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

4. You can verify that the SQL Server 2019 content is loaded by searching under the content pane on the left for *sql server 2019*.

   ![SQL Server 2019 books automatically updated](../sql-server/media/sql-server-help-installation/sql-2019-content.png)

### Configure SQL Server 2017 offline content

For this approach, you use the **Online** Installation source.

1. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![Help Viewer Add Remove Content](../sql-server/media/sql-server-help-installation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

2. To find the latest help content for SQL Server 2017, under the **Manage Content** tab choose **Online** under the Installation source and then type in *sql server 2017* in the search bar.

   ![SQL Server 2017 books search in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2017-search.png)

   > [!Note]
   > The Local store path on the Manage Content tab shows where on the local computer the content is installed. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**. If the help installation fails after changing the Local store path, close and reopen Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

3. To install the latest help content package for SQL Server 2017, select **Add** next to each content package (book) that you want to install and then select **Update** in the lower right.

   ![SQL Server 2017 books add and update in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2017-add-update.png)

   > [!NOTE]
   > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

4. You can verify that the SQL Server 2017 content is loaded by searching under the content pane on the left for *sql server 2017*.

   ![SQL Server 2017 books automatically updated](../sql-server/media/sql-server-help-installation/sql-2017-content.png)

### Configure SQL Server 2016 offline content

For this approach, you use the **Online** Installation source.

1. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![Help Viewer Add Remove Content](../sql-server/media/sql-server-help-installation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

2. To find the latest help content for SQL Server 2016, under the **Manage Content** tab choose **Online** under the Installation source and then type in *sql server 2016* in the search bar.

   ![SQL Server 2016 books search in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2016-search.png)

   > [!Note]
   > The Local store path on the Manage Content tab shows where on the local computer the content is installed. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**. If the help installation fails after changing the Local store path, close and reopen Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

3. To install the latest help content package for SQL Server 2016, select **Add** next to each content package (book) that you want to install and then select **Update** in the lower right.

   ![SQL Server 2016 books add and update in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2016-add-update.png)

   > [!NOTE]
   > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

4. You can verify that the SQL Server 2016 content is loaded by searching under the content pane on the left for *sql server 2016*.

   ![SQL Server 2016 books automatically updated](../sql-server/media/sql-server-help-installation/sql-2016-content.png)

### Configuring SQL Server 2014 offline content

For this approach, you use the **Disk** Installation source.

1. Download the [Product Documentation for Microsoft SQL Server 2014 for firewall and proxy restricted environments](https://www.microsoft.com/en-us/download/details.aspx?id=42557) content from the download center and save it to a folder.

2. Unzip the file to view the.msha file.

   ![SQL Server 2014 Help documentation setup file](../sql-server/media/sql-server-help-installation/sql-2014-help-content-setup-msha.png)

3. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![HelpViewer Add Remove Content](../sql-server/media/sql-server-help-installation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

4. To install the latest help content package, choose **Disk** under Installation source and then the ellipses (...).

   ![Help Viewer Manage Content Disk Source](../sql-server/media/sql-server-help-installation/install-source-disk.png)

   > [!NOTE]
   > The Local store path on the Manage Content tab shows where on the local computer the content is located. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**.
   If the help installation fails after changing the Local store path, close and reopen the Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

5. Locate the folder where you unzipped the content. Select the **HelpContentSetup.msha** file in the folder then select **Open**.

   ![Open the SQL Server 2014 Help Content Setup.msha file](../sql-server/media/sql-server-help-installation/sql-2014-open-msha.png)

6. Type in *sql server 2014* in the search bar. Once you see the 2014 content available, select **Add** next to each content package (book) that you want to install to Help Viewer and then select **Update**.

   ![SQL Server 2014 books search in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2014-search.png)

   ![SQL Server 2014 books add and update in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2014-add-update.png)

    > [!NOTE]
    > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

7. You can verify that the SQL Server 2014 content is loaded by searching under the content pane on the left for *sql server 2014*.

   ![SQL Server 2014 books automatically updated](../sql-server/media/sql-server-help-installation/sql-2014-content.png)

### Configure SQL Server 2012 offline content

For this approach, you use the **Disk** Installation source.

1. Download the [Product Documentation for Microsoft SQL Server 2014 for firewall and proxy restricted environments](https://www.microsoft.com/en-us/download/details.aspx?id=42557) content from the download center and save it to a folder.

2. Unzip the file to view the.msha file.

   ![SQL Server 2014 Help content setup file](../sql-server/media/sql-server-help-installation/sql-2012-help-content-setup-msha.png)

3. In SSMS, select **Add and Remove Help Content** on the Help menu.

   ![HelpViewer Add Remove Content](../sql-server/media/sql-server-help-installation/add-remove-content.png)

   The Help Viewer opens to the Manage Content tab.

4. To install the latest help content package, choose **Disk** under Installation source and then the ellipses (...).

   ![Help Viewer Manage Content Disk Source](../sql-server/media/sql-server-help-installation/install-source-disk.png)

   > [!NOTE]
   > The Local store path on the Manage Content tab shows where on the local computer the content is located. To change the location, select **Move**, enter a different folder path in the **To** field, and then select **OK**.
   If the help installation fails after changing the Local store path, close and reopen the Help Viewer. Ensure the new location appears in the Local store path and then try the installation again.

5. Locate the folder where you unzipped the content. Select the **HelpContentSetup.msha** file in the folder then select **Open**.

   ![Open the SQL Server 2012 Help Content Setup.msha file](../sql-server/media/sql-server-help-installation/sql-2012-open-msha.png)

6. Type in *sql server 2014* in the search bar. Once you see the 2014 content available, select **Add** next to each content package (book) that you want to install to Help Viewer and then select **Update**.

   ![SQL Server 2012 books search in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2012-search.png)

   ![SQL Server 2012 books add and update in Help Viewer](../sql-server/media/sql-server-help-installation/sql-2012-add-update.png)

    > [!NOTE]
    > If the Help Viewer freezes (hangs) while adding content, change the Cache LastRefreshed="\<mm/dd/yyyy> 00:00:00" line in the %LOCALAPPDATA%\Microsoft\HelpViewer2.x\HlpViewer_SSMSx_en-US.settings or HlpViewer_VisualStudiox_en-US.settings file to some date in the future. For more information about this issue, see [Visual Studio Help Viewer freezes](/visualstudio/welcome-to-visual-studio).

7. You can verify that the SQL Server 2014 content is loaded by searching under the content pane on the left for *sql server 2014*.

   ![SQL Server 2012 documentation automatically updated](../sql-server/media/sql-server-help-installation/sql-2012-content.png)

### View offline help content

#### To view offline help content in SSMS with Help Viewer

To view the installed help in SSMS, press CTRL + ALT + F1, or choose **Add or Remove Content** from the Help menu, to launch the Help Viewer.

   ![HelpViewer2 Add Remove Content](../sql-server/media/sql-server-help-installation/add-remove-content.png)  

The Help Viewer opens to the Manage Content tab, with the installed help table of contents in the left pane. select articles in the table of contents to display them in the right pane.
> [!TIP]
> If the contents pane is not visible, select Contents on the left margin. select the pushpin icon to keep the contents pane open.  

   ![Help Viewer v2.x with content](../sql-server/media/sql-server-help-installation/helpviewer2-withcontentinstalled.png)

#### To view offline help content in VS with Help Viewer

To view the installed help in Visual Studio:

1. Point to **Set Help Preference** on the Help menu and choose **Launch in Help Viewer**.

   ![VS Help View Set Preference Help Viewer](../sql-server/media/sql-server-help-installation/launchviewer.png)

2. select **View Help** in the Help menu to display the content in the Help Viewer.

   ![View Help](../sql-server/media/sql-server-help-installation/viewhelp.png)

   The help table of contents shows on the left, and the selected help article on the right.

## View online help

Online help always shows the most up-to-date content.

### To view SQL Server online help in SSMS

- Select **View Help** in the **Help** menu. The latest SQL Server 2016/2017 documentation from [https://docs.microsoft.com/sql/sql-server/](https://docs.microsoft.com/sql/sql-server/) displays in a browser.

   ![View Help](../sql-server/media/sql-server-help-installation/viewhelp.png)

### View Visual Studio Online help in Visual Studio

1. Point to **Set Help Preference** on the Help menu and choose either **Launch in Browser** or **Launch in Help Viewer**.

2. Select **View Help** in the Help menu. The latest Visual Studio help displays in the chosen environment.

## View F1 help

When you press F1 or select **Help** or **?** icon in a dialog box in SSMS or VS, a context-sensitive online help article appears in the browser or Help Viewer.

### To View F1 help

1. In the Help menu, select **Set Help Preference**, and choose either **Launch in Browser** or **Launch in Help Viewer**.

2. Press F1, or select **Help**, or **?** in dialog boxes where they're available, to see context-sensitive online articles in the chosen environment.

> [!NOTE]
> F1 help only works when you are online. There are no offline sources for F1 help.

## Systems without internet access

After you download offline books on a system that has internet access, you can use the following steps to migrate the content to a system that doesn't have internet access.

  >[!NOTE]
  >Software that supports the Help Viewer, such as SQL Server Management Studio, must be installed on the offline system.

1. Open Help Viewer (Ctrl + Alt + F1).

2. Select the documentation you're interested in. For example, filter by SQL and select the SQL Server Technical Documentation.

3. Identify the physical path of the files on disk, which can be found under **Local store path**.

4. Navigate to this location using your file system explorer.
   1. The default location is: `C:\Program Files (x86)\Microsoft SQL Server\140\Tools\Binn\ManagementStudio\Extensions\Application`

5. Select the three folders, **ContentStore**, **Incoming**, **IndexStore**, and copy them to the same location on your offline system. You may need to use an interim media device such as a USB or CD.

6. Once these files have been moved, launch Help Viewer on the offline system and you can see the SQL Server technical documentation.

![physical-location-of-offline-content.png](media/sql-server-help-installation/physical-location-of-offline-content.png)

### Help Viewer

You can use the Microsoft Help Viewer to download and install SQL Server help packages from online sources or local disk. You can then view the content offline. The Help Viewer is installed with several different tools.

- [Latest version of SQL Server Management Studio](../ssms/download-sql-server-management-studio-ssms.md)
- [Visual Studio](https://docs.microsoft.com/visualstudio/help-viewer/installation?view=vs-2019)

To install the Help Viewer with Visual Studio 2019 or 2017, on the **Individual Components** tab in the Visual Studio Installer, select **Code Tools** \> **Help Viewer** \> **Install**.

For additional details about the Help Viewer tool, see [SQL Server offline help and Help Viewer](../sql-server/sql-server-help-installation.md?view=sql-server-2016).

## Life cycle policy

Review the Microsoft Product Lifecycle for information about how a specific product, service, or technology is supported:

- [Microsoft Lifecycle Policy](https://support.microsoft.com/lifecycle/selectindex)

[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]

## Next steps

To learn more about archived content and about Help viewer please reference the links below.

- [A direct link to previous versions of SQL Server documentation](https://docs.microsoft.com/previous-versions/sql/)
- [Microsoft Help Viewer - Visual Studio](https://docs.microsoft.com/visualstudio/help-viewer/overview)
- [SQL Server Documentation, start](../sql-server/index.yml?view=sql-server-2016)
- [Versioning system for SQL documentation](../sql-server/versioning-system-monikers-ui-sql-server.md?view=sql-server-2016)