---
title: "Publish reports to a report server"
description: Learn how to use the deployment features in SQL Server Data Tools (SSDT) to publish multiple reports or a report server project to a report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/14/2024
ms.service: reporting-services
ms.subservice: reports
ms.topic: how-to
ms.custom: updatefrequency5
helpviewer_keywords:
  - "production environments [Reporting Services]"
  - "report projects [Reporting Services]"
  - "Debug configuration [Reporting Services]"
  - "report publishing [Reporting Services]"
  - "publishing reports [Reporting Services]"
  - "report properties [Reporting Services]"
  - "Report Designer [Reporting Services], deploying reports"
  - "Production configuration [Reporting Services]"
  - "publishing reports [Reporting Services], production environments"
  - "DebugLocal configuration [Reporting Services]"
  - "deploying [Reporting Services], reports"
  - "Report Designer [Reporting Services], publishing reports"
#customer intent: As a SQL server user, I want to use the deployment features in SQL Server Data Tools (SSDT) so that I can publish reports to a report server.
---
# Publish reports to a report server

After you design and test a report or set of reports, use the deployment features in SQL Server Data Tools (SSDT) to publish the reports to a report server. You can publish individual reports or a report server project, which can include multiple reports and data sources. Publishing a report server project is the easiest way to publish multiple reports. SSDT uses the term *deploy*, instead of the term *publish*. The two terms are interchangeable.

SSDT provides project configurations for managing report publications. Configurations specify:

- The location of the report server and the version of SQL Server Reporting Services (SSRS) installed on the report server.
- Whether the data sources are published to the report server or are overwritten. For example, the "Debug" configuration can publish to a different server than the "release" configuration. In addition, you can create more configurations.

## Prerequisites

Your report server administrator defines role-based security that determines permission. Publishing operations are typically granted through the **Publisher role**.

### Project configurations

Your reporting environment might have multiple report servers and different versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installed. You can create multiple configurations and then choose one specific to your deployment scenario. Project configurations include properties for building reports, such as the folder in which to temporarily store the built reports, and how to handle build issues. The configurations also have properties that you use to specify the report server's location, the version of the report server, and the folders on the report server.

By default, SSDT provides three project configurations:

|Project configuration|Description|
|---|---|
| **DebugLocal**| View reports in a local preview window.|
| **Debug**| Publish reports to a test server.|
|**Release**| Publish reports to a production server.|

**The Solution Configurations drop-down list** on the Standard toolbar shows the active configuration. To use a different configuration, select it from the list.

:::image type="content" source="../../reporting-services/reports/media/ssrs-project-properties.png" alt-text="Screenshot of the tutorial Property Pages window, highlighting the Configuration drop-down list.":::

For more information, see:

- [Project property pages dialog box](../../reporting-services/tools/project-property-pages-dialog-box.md)
- [Deployment and version support in SQL Server Data Tools](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md)
- [Set deployment properties for Reporting Services projects in SSDT](../../reporting-services/tools/set-deployment-properties-reporting-services.md)

## Publish all reports in a project

You can view the publishing process in the output window by using one of the following methods:

- On the SSDT **Build** menu, select **Deploy Solution**. OR,
- In Solution Explorer, right-click the report project and then select **Deploy Solution**.

When you deploy a report server project, the shared data sources in the report project are also deployed. All reports are deployed from the same project configuration: to the same report server, the same folder on the server, and so on. To publish reports to different servers, either publish them one at a time or include only the reports you want to in the report server project. A solution can include multiple report server projects. Multiple projects might make it easier to manage the deployment of reports because you can use a different configuration to deploy different projects.

## Publish a single report

In Solution Explorer, right-click the report and then select **Deploy Solution**. View the status of the publishing process in the output window.

When you publish a report, you must also deploy the shared data sources that the report uses.

- If you don't want to publish all reports in a project, you can publish only a single report. Select a configuration that deploys the report (for example, the Release configuration), right-click the report, and then select **Deploy Solution**.
- If a report uses a shared data source, you need to also deploy the shared data source or the deployed report doesn't run. Right-click the shared data source and then select **Deploy Solution**.

After the target server URL of the report server is specified, you can change the default folders to specific reports, and then shared data sources deploy.

## Related content

- [Project property pages dialog box](../../reporting-services/tools/project-property-pages-dialog-box.md)
- [Report Server content management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)
- [Upgrade reports](../../reporting-services/install-windows/upgrade-reports.md)
