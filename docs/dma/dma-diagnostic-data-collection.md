---
title: Enable and disable usage and diagnostic data collection
description: Learn how to enable and disable usage and diagnostic data collection in Database Migration Assistant.
author: ajithkr-ms
ms.author: ajithkr
ms.reviewer: randolphwest
ms.date: 06/28/2024
ms.service: sql
ms.subservice: dma
ms.topic: how-to
ms.collection:
  - sql-migration-content
---

# Enable and disable DMA usage and diagnostic data collection

[!INCLUDE [deprecation-notice](includes/deprecation-notice.md)]

Database Migration Assistant contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

This article teaches you to enable or disable DMA diagnostic data collection when using the GUI or the command-line tool.

## Collected data

DMA might collect standard computer information and information about use and performance that might be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability of DMA. DMA doesn't collect your name, address, or any other data related to an identified or identifiable individual. For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](../sql-server/sql-server-privacy.md).

## Data Migration Assistant GUI

To disable telemetry collection when using DMA GUI, you need to comment out the `AppInsightsInstrumentationKey` line in the Dma.exe.config.

To do so follow these steps:

1. Navigate to the DMA installation folder in your file system. The default location is `%ProgramFiles%\Microsoft Data Migration Assistant\`.

1. Locate the Dma.exe.config file and open it in a text editor.

1. Find the `AppInsightsInstrumentationKey` line and surround the line in a comment bracket ( `<!-- -->` ) to disable telemetry collection such as the following screenshot:

   :::image type="content" source="media/dma-diagnostic-data-collection/dmacmd-disable-telemetry.png" alt-text="Screenshot of dma.exe.config file you modify to disable telemetry." lightbox="media/dma-diagnostic-data-collection/dmacmd-disable-telemetry.png":::

This value is uncommented and thus enabled by default. Commenting out the value disables telemetry.

To re-enable telemetry, remove the comment around the line.

## Data Migration Assistant command line

In order to disable telemetry collection when using the DMA command-line tool you need to comment out the `AppInsightsInstrumentationKey` line in the `dmacmd.exe.config`.

To do so follow these steps:

1. Navigate to the DMA installation folder in your file system. The default location is `%ProgramFiles%\Microsoft Data Migration Assistant\`.

1. Locate the `dmacmd.exe.config` file and open it in a text editor.

1. Find the `AppInsightsInstrumentationKey` line and surround the line in a comment bracket ( `<!-- -->` ) to disable telemetry collection such as the following screenshot:

   :::image type="content" source="media/dma-diagnostic-data-collection/dmacmd-disable-telemetry.png" alt-text="Screenshot of dmacmd.exe.config file you modify to disable telemetry." lightbox="media/dma-diagnostic-data-collection/dmacmd-disable-telemetry.png":::

This value is uncommented and thus enabled by default. Commenting out the value disables telemetry.

To re-enable telemetry, remove the comment around the line.

## Related content

- [DMA assessment project](dma-assesssqlonprem.md)
