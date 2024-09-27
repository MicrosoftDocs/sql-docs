---
title: "Convert the CRI dialog (Report Builder)"
description: Report Builder prompts you to convert some custom report items, which have unsupported features, to the new report definition format.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-builder
ms.topic: reference
ms.custom:
  - updatefrequency5
f1_keywords:
  - "10008"
helpviewer_keywords:
  - "CRI"
  - "custom report items"
---
# Convert the CRI dialog (Report Builder)

  This report contains custom report items (CRIs) with unsupported features. CRIs are extensions to the Report Definition Language (RDL) that support custom objects that display data in a report. CRIs include design-time and run-time components that are supplied by non-Microsoft software vendors.

> [!NOTE]  
> Choosing to support custom report items on a report server is a decision made by the system administrator. To view CRIs in a report, the CRI components must be installed on the report authoring client to preview a report and on the report server to view a published or uploaded report.

Some CRIs can be converted to report items in the new report definition format. When you open the report, you're prompted whether to upgrade. Use the following information to decide whether to convert the CRIs in this report:

- **Yes**: Choose **Yes** to convert all the CRIs in the report, where possible. Unsupported features in the CRIs can't be upgraded and are removed from the report definition file. For the list of unsupported features, see [Upgrade reports](../../reporting-services/install-windows/upgrade-reports.md). When you view the report, you might see differences in the way the CRI displays in the report.

- **No**: Choose **No** when you don't want to convert the CRIs in the report. The report processor can't display these CRIs in their current version. If your system administrator is planning to install a new version of the CRI from the non-Microsoft software vendor that is compatible with the new report definition format, you should choose **No**. Until new versions are available, the CRIs display in the report as an empty text box with a red **X**.

In either case, the report is upgraded to the new report definition format and a backup copy of the original report is saved as `<Report Name>-Backup.rdl`. If you save the report in your report authoring tool, you're saving the upgraded report in the new report definition format. If you publish the report, the report is first saved on your computer, and then published to the report server. You're publishing the upgraded version of the report to the report server.

If you don't save the report, the original report remains unchanged. However, you can't edit this report in a [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] later version of [!INCLUDE[ssBIDevStudio](../../includes/ssbidevstudio-md.md)] or a report authoring environment that uses this report definition format. You can continue to run the original version of the report by uploading it to a [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] or later [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report server by using Report Manager. For more information, see [Upload a file or report (Report Manager)](../../reporting-services/reports/upload-a-file-or-report-report-manager.md).

For reports that you upload instead of publish to a report server, the report processor determines whether the report can be upgraded on first use. Reports that can't be upgraded are processed in backward-compatibility mode, and continue to display as they did in the earlier version of [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]. For more information, see [Upgrade reports](../../reporting-services/install-windows/upgrade-reports.md).

To identify the current report definition format for a report, for a report server, or for your report authoring environment, see [Find the report definition schema version (SSRS)](../../reporting-services/reports/find-the-report-definition-schema-version-ssrs.md).
