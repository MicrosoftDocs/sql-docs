---
title: "Publishing Reports to a Report Server"
description: Learn how to use the deployment features in SQL Server Data Tools (SSDT) to publish multiple reports or a report server project to a report server.
author: maggiesMSFT
ms.author: maggies
ms.date: 05/31/2024
ms.service: reporting-services
ms.subservice: reports
ms.topic: conceptual
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
# Publishing Reports to a Report Server

After you design and test a report or set of reports, you can use the deployment features in [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] to publish the reports to a report server. You can publish individual reports or a report server project, which can include multiple reports and data sources. Publishing a report server project is the easiest way to publish multiple reports. [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] uses the term *deploy*, instead of the term *publish*. The two terms are interchangeable.

[!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] provides project configurations for managing report publication. The configuration specifies the location of the report server and the version of SQL Server Reporting Services (SSRS) installed on the report server. The configuration specifies whether the data sources are published to the report server or are overwritten. For example, the "Debug" configuration can publish to a different server than the "release" configuration. In addition to using the configurations that [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] provides, you can create more configurations.

## Requirements to publish

Your report server administrator defines role-based security that determines permission. Publishing operations are typically granted through the **Publisher role**.

## Project configurations

 Your reporting environment might have multiple report servers and different versions of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] installed. You can create multiple configurations and then use a different one depending on the deployment scenario. Project configurations include properties for building reports, such as the folder in which to temporarily store the built reports, and how to handle build issues. The configurations also have properties that you use to specify the location and version of the report server, the folders on the report server.

 By default, [!INCLUDE[ssBIDevStudioFull](../../includes/ssbidevstudiofull-md.md)] provides three project configurations: **DebugLocal**, **Debug**, and **Release**. The default configuration is DebugLocal. You typically use the DebugLocal configuration to view reports in a local preview window, the Debug configuration to publish reports to a test server, and the Release configuration to publish reports to a production server. The solution configurations drop-down list on the Standard toolbar shows the active configuration. To use a different configuration, select it from the list.

:::image type="content" source="../../reporting-services/reports/media/ssrs-project-properties.png" alt-text="Screenshot of the tutorial Property Pages, highlighting TargetServerURL.":::

 For more information, see:

- [Project property pages dialog box](../../reporting-services/tools/project-property-pages-dialog-box.md)
- [Deployment and version support in SQL Server Data Tools](../../reporting-services/tools/deployment-and-version-support-in-sql-server-data-tools-ssrs.md)
- [Set deployment properties for Reporting Services projects in SSDT](../../reporting-services/tools/set-deployment-properties-reporting-services.md)

## To publish all reports in a project

On the **Build** menu, select **Deploy Solution**. Alternatively, in Solution Explorer, right-click the report project and then select **Deploy Solution**. You can view the status of the publishing process in the Output window.

When you deploy a report server project, the shared data sources in the report project are also deployed. All reports are deployed by using the same project configuration: to the same report server, the same folder on the server, and so on. To publish reports to different servers, either publish them one by one or include only reports you want to in the report server project. A solution can include multiple report server projects. I multiple projects might make it easier to manage the deployment of reports because you can use a different configuration to deploy different projects. You can more easily manage the deployment of reports by using multiple projects because you can use a different configuration to deploy different projects.

## To publish a single report

In Solution Explorer, right-click the report and then select **Deploy Solution**. You can view the status of the publishing process in the Output window.

When you publish a report, you must also deploy the shared data sources that the report uses.
If you don't want to publish all reports in a project, you can choose to publish only a single report. Select a configuration that deploys the report (for example, the Release configuration), right-click the report, and then select **Deploy Solution**.

 If a report uses a shared data source, you need to also deploy the shared data source or the deployed report won't run. Right-click the shared data source and then select **Deploy Solution**.

 The target server URL of the report server must be specified and you might want to change the default folders to which reports and shared data sources deploy.

## Related content

 [Project property pages dialog box](../../reporting-services/tools/project-property-pages-dialog-box.md)
 [Report Server content management &#40;SSRS Native Mode&#41;](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)
 [Upgrade reports](../../reporting-services/install-windows/upgrade-reports.md)
