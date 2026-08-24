---
title: Uninstall Reporting Services
description: This article describes how to uninstall Reporting Services, which doesn't remove content you created or configuration you have modified.
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: maghan
ms.date: 06/26/2026
ms.service: reporting-services
ms.topic: how-to
---

# Uninstall Reporting Services

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

Uninstalling [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] doesn't remove the content you created or the configuration you modified. However, if you need the content after the uninstall, make copies of it before you begin the uninstallation process.

## Uninstall SharePoint Mode

When you uninstall [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] SharePoint mode, the following components are removed:

- [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] service and service proxy.

- Files used for the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] installation.

The uninstall process doesn't remove the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications. If you no longer want the service applications, delete them by using Windows PowerShell or SharePoint Central Administration.

The uninstall process doesn't remove the report items and related metadata. This information is contained in the content and configuration databases related to the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] service applications. The databases aren't removed and you can manually migrate the databases to another installation of [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] in SharePoint mode. If you no longer want the information, delete the databases. For more information, see [Upgrade and migrate Reporting Services](../../reporting-services/install-windows/upgrade-and-migrate-reporting-services.md).

The following are example names of the three [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] databases that aren't removed:

- **Report server database:** ReportingService_7f616e2d253040e8ab5653b3c09a065e

- **Report server temp database:** ReportingService_7f616e2d253040e8ab5653b3c09a065eTempDB

- **Report server alerting database:** ReportingService_7f616e2d253040e8ab5653b3c09a065e_Alerting

### Uninstall the Add-in for SharePoint Products

When you uninstall the add-in from a computer, you can choose to only uninstall the files or to also remove the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] feature from the farm. For information on uninstalling the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] add-in for SharePoint products, see [Install or uninstall the Reporting Services add-in for SharePoint (SSRS)](../../reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint.md).

## Uninstall native mode

When you uninstall [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode, the uninstaller leaves in place anything that you **created** or **modified** after the installation. For example, the uninstaller leaves in place database files, log files, [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] configuration files, and content items such as reports and datasource files.

[!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] is an instance feature and therefore doesn't appear in Windows Control Panel, **Programs and Features**. To uninstall [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] native mode:

1. In Windows Control Panel, select **Programs and Features**.

1. In **Programs and Features**, select **Microsoft SQL Server 2016**.

1. In the uninstall wizard, select the instance that includes the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] instance feature **RS**.

   :::image type="content" source="media/uninstall-reporting-services/rs-nativemode-uninstall-selectinstance.gif" alt-text="Screenshot of rs_nativemode_uninstall_selectinstance." lightbox="media/uninstall-reporting-services/rs-nativemode-uninstall-selectinstance.gif":::

1. After you select the instance, select the [!INCLUDE [ssRSnoversion](../../includes/ssrsnoversion-md.md)] feature.

   :::image type="content" source="media/uninstall-reporting-services/rs-nativemode-uninstall-selectfeatures.gif" alt-text="Screenshot of rs_nativemode_uninstall_selectfeatures." lightbox="media/uninstall-reporting-services/rs-nativemode-uninstall-selectfeatures.gif":::

1. Complete the wizard.

## Related content

- [Uninstall an existing instance of SQL Server (Setup)](uninstall-an-existing-instance-of-sql-server-setup.md)
- [Install or Uninstall the Power Pivot for SharePoint Add-in (SharePoint 2013)](/analysis-services/instances/install-windows/install-or-uninstall-the-power-pivot-for-sharepoint-add-in-sharepoint-2013)
- [Install or uninstall the Reporting Services add-in for SharePoint (SSRS)](/previous-versions/sql/reporting-services/install-windows/install-or-uninstall-the-reporting-services-add-in-for-sharepoint)
