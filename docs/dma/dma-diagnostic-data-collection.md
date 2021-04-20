---
title: "DMA usage and diagnostic data collection"
description: Learn about usage and diagnostic data collection in Database Migration Assistant.
author: aciortea

ms.prod: sql
ms.custom: ""
ms.date: "04/21/2021"
ms.reviewer: ""
ms.technology: dma
ms.topic: conceptual
ms.author: "aciortea"
---

# DMA usage and diagnostic data collection

Database Migration Assistant contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

## Collected data

DMA may collect standard computer information and information about use and performance that may be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability of DMA. We do not collect your name, address, or other contact information. For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](../sql-server/sql-server-privacy.md).

## Enable or disable usage and diagnostic data collection in DMA

To be updated!!!!!!!

Following registry entry allows you to opt in or out of data collection:

RegEntry name = `DisableTelemetry`  
Entry type `STRING`: `True` is opt out; `False` or not present is opt in

Additionally, automatic checks for newer tool versions during start-up can be disabled by adding the following entry:

RegEntry name = `DisableAutoUpdate`  
Entry type `STRING`: `True` is opt out; `False` or not present is opt in


