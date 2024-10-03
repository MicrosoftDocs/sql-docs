---
title: Install extensions in SQL Server Management Studio (SSMS)
description: Learn how to install extensions in SQL Server Management Studio (SSMS).
author: dzsquared
ms.author: drskwier
ms.reviewer: maghan, randolphwest
ms.date: 08/19/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
ms.custom:
  - intro-installation
keywords:
  - "extensions"
  - "vsix"
  - "install extension"
  - "install vsix"
---

# Install extensions in SQL Server Management Studio (SSMS)

[!INCLUDE [SQL Server ASDB, ASDBMI, ASDW](../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

SQL Server Management Studio (SSMS) extensions are created using the Visual Studio Software Development Kit (SDK). SSMS 18.x and later versions are built on the Visual Studio 2017 Isolated Shell (IsoShell), and are subject to the limitations of that environment.

Extension installation for SSMS is managed by an independent managed package installer.

> [!NOTE]  
> SQL Server Management Studio extensions can't be installed via VSIXInstaller for SSMS 18.x and later versions.

## Manual installation of an extension for SSMS

To install an extension manually, you must copy the associated extension files (`.vsix`) into the default SSMS extensions folder. SSMS automatically checks this folder for extensions at launch.

1. Locate your SSMS installation and extensions folder. With default SSMS installation settings, the folder location for SSMS 20 is `C:\Program Files (x86)\Microsoft SQL Server Management Studio 20\Common7\IDE\Extensions\`.

1. Copy entire extension folder structure to the Extensions folder.

1. Close SSMS and restart to use the extension.
