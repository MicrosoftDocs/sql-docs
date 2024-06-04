---
title: "Tutorial: How to locate and start Reporting Services tools"
description: Learn tools used to configure a report server, manage report server content & operations, and create and publish paginated & mobile Reporting Services reports.
author: maggiesMSFT
ms.author: maggies
ms.date: 12/09/2019
ms.service: reporting-services
ms.subservice: tools
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "Reporting Services, tutorials"
  - "Reporting Services, tools"
  - "Reporting Services Configuration tool"
  - "Business Intelligence Development Studio, locating and starting tool"
  - "Model Designer [Reporting Services], locating and starting tool"
  - "Report Designer [Reporting Services], tutorials"
  - "tools [Reporting Services]"
  - "tutorials [Reporting Services]"
  - "Report Builder"
---

# Tutorial: How to locate and start Reporting Services tools (SSRS)

This tutorial introduces the tools used to configure a report server, manage report server content and operations, and create and publish paginated and mobile [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports. If you're already familiar with the tools, you can move on to other tutorials to learn how to use [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For other tutorials, see [Reporting Services Tutorials &#40;SSRS&#41;](../../reporting-services/reporting-services-tutorials-ssrs.md).

## <a name="bkmk_configuration_manager"></a> Report Server Configuration Manager (native mode)
Use the Native mode configuration manager to complete the following tasks:

- Specify the service account.
- Create or upgrade the report server database.
- Modify the connection properties.
- Specify URLs.
- Manage encryption keys.
- Configure unattended report processing and e-mail report delivery.

**Installation:** [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Configuration Manager is installed when you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode. For more information, see [Install Reporting Services native mode report server](../../reporting-services/install-windows/install-reporting-services-native-mode-report-server.md).

### Start the Report Server Configuration Manager

1. In the Windows start menu, enter **reporting** and in the **Apps** search results, select **Report Server Configuration Manager**.

    :::image type="content" source="../../reporting-services/tools/media/bi-ssrs-configmanager-win8-startscreen.gif" alt-text="Screenshot that shows the Report Server Configuration Manager button in the Start menu.":::

    **Or**

    Select **Start**, then choose **Programs**, then [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], then choose **Configuration Tools**, and then select **Report Server Configuration Manager**.

    The **Report Server Installation Instance Selection** dialog box appears so that you can select the report server instance you want to configure.

2. In **Server Name**, specify the name of the computer on which the report server instance is installed. The name of the local computer is specified by default, but you can also enter the name of a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

    If you specify a remote computer, select **Find** to establish a connection. The report server must be configured for remote administration in advance. For more information, see [Configure a report server for remote administration](../../reporting-services/report-server/configure-a-report-server-for-remote-administration.md).

3. In **Instance Name**, choose the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance that you want to configure. Only SQL Server 2008 and later report server instances appear in the list. You can't configure earlier versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].

4. Select **Connect**.

5. To verify that you launched the tool, compare your results to the following image:

    :::image type="content" source="../../reporting-services/tools/media/rs-ui-reportserverconfigkatmai.png" alt-text="Screenshot that shows the Report Services Subscription Settings tool.":::


 **Next Steps:** [Configure and administer a report server &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/configure-and-administer-a-report-server-ssrs-native-mode.md) and [Report Server Configuration Manager &#40;native mode&#41;](../../reporting-services/install-windows/reporting-services-configuration-manager-native-mode.md).

## Web portal (native mode)

Use [Web portal (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md) to set permissions, manage subscriptions and schedules, and work with reports. You can also use the Web Portal to view reports.

**Installation:** The Web Portal is installed when you install [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] Native mode: [Install Reporting Services native mode report server](../../reporting-services/install-windows/install-reporting-services-native-mode-report-server.md)

Before you can open the Web Portal, you must have sufficient permissions (initially, only members of the local Administrators group have permissions that provide access to the Web Portal features). The Web Portal provides different pages and options depending on the role assignments of the current user. Users who have no permissions get an empty page. Users with permissions to view reports get links that they can select to open the reports. To learn more about permissions, see [Roles and permissions &#40;Reporting Services&#41;](../../reporting-services/security/roles-and-permissions-reporting-services.md).

### Start the web portal

1. Open your browser. For information on supported browsers and browser versions, see [Browser support for Reporting Services](../../reporting-services/browser-support-for-reporting-services-and-power-view.md).

2. In the address bar of the Web browser, enter the Web Portal URL. By default, the URL is `https://<serverName>/reports`. You can use the Reporting Services Configuration tool to confirm the server name and URL. For more information about URLs used in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)], see [Configure report server URLs &#40;Report Server Configuration Manager&#41;](../../reporting-services/install-windows/configure-report-server-urls-ssrs-configuration-manager.md).

3. The Web Portal opens in the browser window. The startup page is the Home folder. Depending on permissions, you might see other folders, hyperlinks to reports, and resource files within the startup page. You might also see other buttons and commands on the toolbar.

4. If you run the Web Portal on the local report server, see [Configure a native mode report server for local administration &#40;SSRS&#41;](../../reporting-services/report-server/configure-a-native-mode-report-server-for-local-administration-ssrs.md).

## <a name="bkmk_managements_studio"></a> Management Studio

Report server administrators can use [!INCLUDE[ssManStudio](../../includes/ssmanstudio-md.md)] to manage a report server alongside other [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] component servers. For more information, see the [SQL Server Management Studio](../../ssms/quickstarts/ssms-connect-query-sql-server.md) tutorial.

### Start SQL Server Management Studio

1. From the Windows start menu, enter **sql server** and in the **Apps** search results, select **SQL Server Management Studio**.

    :::image type="content" source="../../reporting-services/tools/media/bi-ssms-win8-startscreen.gif" alt-text="Screenshot that shows the SQL Server Management Studio from the Windows button in the Start menu.":::


    **Or**

    Select **Start**, then choose **All Programs**, then [!INCLUDE[ssCurrentUI](../../includes/sscurrentui-md.md)], and then select **SQL Server Management Studio**. The **Connect to Server** dialog box appears.

2. If the **Connect to Server** dialog box doesn't appear, in **Object Explorer**, select **Connect** and then choose **Reporting Services**.

3. In the **Server type** list, select **Reporting Services**. If [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] isn't on the list, it isn't installed.

4. In the **Server name** list, select a report server instance. Local instances appear in the list. You can also enter the name of a remote [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

5. Select **Connect**. You can expand the root node to set server properties, modify role definitions, or turn off report server features.

## <a name="bkmk_ssdt"></a> SQL Server Data Tools with Report Designer and Report Wizard

You have a choice of two different tools for creating [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] paginated reports: Report Designer and [Report Builder](#bkmk_report_builder).

Report Designer is available in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] - Visual Studio. The Report Designer design surface includes tabbed windows, wizards, and menus used to access report authoring features. The report designer tool becomes available when you choose a Report Server Project or a Report Server Wizard template in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)]. To learn more, see [Reporting Services in SQL Server Data Tools &#40;SSDT&#41;](../../reporting-services/tools/reporting-services-in-sql-server-data-tools-ssdt.md).

Download [SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md).

### Start Report Designer

1. Open **SQL Server Data Tools**.

2. On the **File** menu, point to **New**, and then select **Project**.

3. In the **Project Types** list, select **Business Intelligence Projects**.

4. In the **Templates** list, select **Report Server Project**. The following diagram shows how the project templates appear in the dialog box:

    :::image type="content" source="../../reporting-services/tools/media/rs-ui-newrsproject.gif" alt-text="Screenshot that shows the New Project template dialog box.":::

5. Enter a name and location for the project, or select **Browse** and choose a location.

6. Select **OK**. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] opens with the [!INCLUDE[vsprvs](../../includes/vsprvs-md.md)] start page. Solution Explorer provides categories for creating reports and data sources. You can use these categories to create new reports and data sources. Tabbed windows appear when you create a report definition. The tabbed windows are Data, Layout, and Preview.

To get started on your first report, see [Create a basic table report &#40;SSRS tutorial&#41;](../../reporting-services/create-a-basic-table-report-ssrs-tutorial.md). To learn more about query designers you can use within Report Designer, see [Query Design Tools &#40;SSRS&#41;](../../reporting-services/report-data/query-design-tools-ssrs.md).

## <a name="bkmk_report_builder"></a> [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]

[Report Builder in SQL Server](../../reporting-services/report-builder/report-builder-in-sql-server-2016.md) is a stand-alone application you can use to create paginated reports outside [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. You can customize and update all existing reports, regardless of whether they were created in Report Designer or in previous versions of [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]. Install it from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal or from the Microsoft Download Center.

When your paginated report is ready, publish it to a report server or [save it to the Power BI service](/power-bi/paginated-reports-save-to-power-bi-service).
[Download Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138) from the Microsoft Download Center.

### Start [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]

1. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal, on the **New** menu, select **Paginated Report**.

    :::image type="content" source="../../reporting-services/mobile-reports/media/pbi-ssmrp-newmenu.png" alt-text="Screenshot that shows the New menu options.":::

2. If [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] isn't installed on this computer yet, select **Get [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)]**.

    Or

    [Download Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138) from the Microsoft Download Center.

3. [!INCLUDE[ssRBnoversion](../../includes/ssrbnoversion.md)] opens and you can create or open a paginated report.

## <a name="bkmk_mobile_report_pub"></a> [!INCLUDE[SS_MobileReptPub_Long](../../includes/ss-mobilereptpub-long.md)]

[!INCLUDE [ssrs-mobile-report-deprecated](../../includes/ssrs-mobile-report-deprecated.md)]

Use [SQL Server Mobile Report Publisher](../mobile-reports/create-mobile-reports-with-sql-server-mobile-report-publisher.md) to create mobile reports you can view in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal and in mobile devices such as iPads and iPhones.\

You can install it from the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal or from the Microsoft Download Center.

[Download SQL Server Mobile Report Publisher](https://go.microsoft.com/fwlink/?LinkID=733527) from the Microsoft Download Center.

### Start [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)]

1. In the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] web portal, on the **New** menu, select **Mobile Report**.

    :::image type="content" source="../../reporting-services/mobile-reports/media/pbi-ssmrp-newmenu.png" alt-text="Screenshot that shows the New menu options.":::

2. If [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)] isn't installed on this computer yet, select **Get [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)]**.

    Or

    [Download SQL Server Mobile Report Publisher](https://go.microsoft.com/fwlink/?LinkID=733527) from the Microsoft Download Center.

3. [!INCLUDE[SS_MobileReptPub_Short](../../includes/ss-mobilereptpub-short.md)] opens and you can create or open a mobile report.

## Related content

[Download SQL Server Mobile Report Publisher](https://go.microsoft.com/fwlink/?LinkID=733527)  
[Download Report Builder](https://go.microsoft.com/fwlink/?LinkID=219138)  
[Download SQL Server Data Tools](../../ssdt/download-sql-server-data-tools-ssdt.md)  
[Install Reporting Services SharePoint mode](../../reporting-services/install-windows/install-reporting-services-sharepoint-mode.md)  
[Reporting Services report server](../../reporting-services/report-server-sharepoint/reporting-services-report-server.md)  
[Query design tools](../../reporting-services/report-data/query-design-tools-ssrs.md)  
[Reporting Services tutorials](../../reporting-services/reporting-services-tutorials-ssrs.md)  

More questions? [Try asking the Reporting Services forum](/answers/search.html?c=&f=&includeChildren=&q=ssrs+OR+reporting+services&redirect=search%2fsearch&sort=relevance&type=question+OR+idea+OR+kbentry+OR+answer+OR+topic+OR+user)
