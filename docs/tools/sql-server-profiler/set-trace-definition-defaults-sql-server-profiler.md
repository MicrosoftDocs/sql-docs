---
title: Set Trace Definition Defaults
titleSuffix: SQL Server Profiler
description: Discover how to use SQL Server Profiler to set up the templates that SQL Server and Analysis Services use by default for each provider or server.
author: cwebb-msft
ms.author: chweb
ms.date: 06/05/2025
ms.service: sql
ms.subservice: profiler
ms.topic: how-to
ms.collection:
  - data-tools
---

# Set trace definition defaults (SQL Server Profiler)

[!INCLUDE [SQL Server Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdbmi.md)]

The trace definition default is the default trace template that is used for each provider or server. You can set default trace templates for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] or for [!INCLUDE [ssASnoversion](../../includes/ssasnoversion-md.md)].

## Set trace definition defaults

1. On the **File** menu, select **Templates**, and then select **Edit Template**.

1. In the **Trace Template Properties** dialog box, on the **General** tab, select a server type from the **Select server type** list.

1. In the **Select template name** list, select the name of the template that you want to use as the trace definition default.

1. Select **Use as a default template for selected server type**.

1. If necessary, select the **Events Selection** tab to modify the template.

1. Select **Save**.

## Related content

- [SQL Server Profiler Templates](sql-server-profiler-templates.md)
- [SQL Server Profiler](sql-server-profiler.md)
