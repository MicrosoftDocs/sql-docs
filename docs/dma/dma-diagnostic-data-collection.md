---
title: Enable and disable usage and diagnostic data collection
description: Learn how to enable and disable usage and diagnostic data collection in Database Migration Assistant.
author: aciortea
ms.author: "aciortea"
ms.date: "04/23/2021"
ms.service: sql
ms.subservice: dma
ms.topic: how-to
---

# Enable and disable DMA usage and diagnostic data collection

Database Migration Assistant contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

This article teaches you to enable or disable DMA diagnostic data collection when using the GUI or the command-line tool.

## Collected data

DMA may collect standard computer information and information about use and performance that may be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability of DMA. DMA doesn't collect your name, address, or any other data related to an identified or identifiable individual. For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](../sql-server/sql-server-privacy.md).

## Data Migration Assistant GUI

To disable telemetry collection when using DMA GUI, you need to comment out the `AppInsightsInstrumentionKey` line in the Dma.exe.config.

To do so follow these steps:

1. Navigate to the DMA installation folder in your file system. The default location is `%ProgramFiles%\Microsoft Data Migration Assistant\`.

2. Locate the Dma.exe.config file and open it in a text editor.

3. Find the `AppInsightsInstrumentionKey` line and surround the line in a comment bracket ( `<!-- -->` ) to disable telemetry collection such as the following screenshot:

   :::image type="content" source="media/dma-diagnostic-data-collection/dmacmd-disable-telemetry.png" alt-text="Dma.exe.config file you modify to disable telemetry":::

This value is uncommented and thus enabled by default. Commenting out the value disables telemetry.

To re-enable telemetry, remove the comment around the line.

## Data Migration Assistant Command Line

In order to disable telemetry collection when using the DMA command-line tool you need to comment out the `AppInsightsInstrumentionKey` line in the DmaCmd.exe.config.

To do so follow these steps:

1. Navigate to the DMA installation folder in your file system. The default location is `%ProgramFiles%\Microsoft Data Migration Assistant\`.

2. Locate the DmaCmd.exe.config file and open it in a text editor.

3. Find the `AppInsightsInstrumentionKey` line and surround the line in a comment bracket ( `<!-- -->` ) to disable telemetry collection such as the following screenshot:

   :::image type="content" source="media/dma-diagnostic-data-collection/dmacmd-disable-telemetry.png" alt-text="DMACMD.exe.config file you modify to disable telemetry":::

This value is uncommented and thus enabled by default. Commenting out the value disables telemetry. 

To re-enable telemetry, remove the comment around the line.

## Next steps
 
Now that you've modified your telemetry setting you can get started by creating a [DMA assessment project](dma-assesssqlonprem.md).
