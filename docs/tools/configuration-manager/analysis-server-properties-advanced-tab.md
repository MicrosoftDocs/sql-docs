---
title: "Analysis Server Properties (Advanced Tab)"
description: Learn about the options on the Advanced tab in the Analysis Server Properties dialog box, such as the dump directory and the instance ID.
author: rwestMSFT
ms.author: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: tools-other
ms.topic: ui-reference
ms.collection:
  - data-tools
monikerRange: ">=sql-server-2017"
---
# Analysis Server Properties (Advanced tab)

[!INCLUDE [SQL Server Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

This service is the Microsoft [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)]. If custom properties are defined, they appear on this tab, with their values.

For more information, see [Configure usage and diagnostic data collection for SQL Server tools (CEIP)](../../sql-server/usage-and-diagnostic-data-configuration-for-sql-server-tools.md).

## Options

#### Clustered

Indicates whether this service is installed as a resource of a clustered server.

#### Customer Feedback Reporting

Indicates whether Service Quality Monitoring has been enabled on this service.

#### Dump Directory

Displays the location where memory dumps are placed in case of an error.

#### Error Reporting

When set to **Yes**, the Dr. Watson program sends information to either [!INCLUDE [msCoName](../../includes/msconame-md.md)] or your error server if a serious failure occurs.

#### Instance ID

Indicates the instance that uses this service.
