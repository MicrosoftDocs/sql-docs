---
title: "Local Language Versions in SQL Server"
description: English-language versions of supported operating systems support localized versions of SQL Server by using Windows Multilingual User Interface Pack settings.
author: adbadram
ms.author: adbadram
ms.date: 09/07/2025
ms.service: sql
ms.subservice: install
ms.topic: concept-article
---
# Local language versions in SQL Server

[!INCLUDE [SQL Server -Windows Only](../../includes/applies-to-version/sql-windows-only.md)]

[!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] supports all languages that are supported by Windows operating systems.

## Cross-language support

The English-language version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is supported on all localized versions of operating systems.

Localized versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are supported on localized operating systems with the corresponding language or on English-language versions of supported operating systems by using the Windows Multilingual User Interface Pack (MUI) settings. For more information, see [Configure Operating System to Support Localized Versions](../../sql-server/install/local-language-versions-in-sql-server.md#BK_ConfigureOS).

Localized versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can only be upgraded to localized versions of the same language, and can't be upgraded to the English-language version.

Localized versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] can also be installed side by side with English-language instances of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

<a id="BK_ConfigureOS"></a>

## Configure operating system to support localized versions

Localized versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] are supported on English-language versions of supported operating systems Windows through using Multilingual User Interface Pack (MUI) settings.

However, you must verify certain operating system settings before installing a localized version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a server that is running an English-language operating system with a non-English MUI setting. You need to verify that the following operating system settings match the language of the localized [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to be installed:

- The operating system user interface setting
- The operating system user locale setting
- The system locale setting

If the settings don't match the language of the localized [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] to be installed, then use the following procedures to correctly set these operating system settings.

> [!CAUTION]  
> Installations of different language versions of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instances on the same computer aren't supported.

### Change the operating system user interface setting

1. If not already installed, install the operating system MUI that matches your localized version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. In Control Panel, open **Regional and Language Options**.

1. On the **Languages** tab, for **Language used in menus and dialogs**, select a value from the list.

   This setting affects the user interface language of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], so it must match your localized version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. Select **Apply** to confirm the change, and **OK** to close the window.

### Change the operating system user locale setting

1. If not already installed, install the operating system MUI that matches your localized version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. In Control Panel, open **Regional and Language Options**.

1. On the **Regional Options** tab, for **Select an item to match its preferences**, select a value from the list.

   This setting affects culture-specific data formatting.

1. Select **Apply** to confirm the change, and **OK** to close the window.

### Change the system locale setting

1. If not already installed, install the operating system MUI that matches your localized version of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

1. In Control Panel, open **Regional and Language Options**.

1. On the **Advanced** tab, for **Select a language to match the language version of the non-Unicode programs you want to use**, select a value from the list.

   This setting allows [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Setup to choose the best default collation for your [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installation.

1. Select **Apply** to confirm the change, and **OK** to close the window.

## Related content

- [Hardware and software requirements for SQL Server 2022](hardware-and-software-requirements-for-installing-sql-server-2022.md)
- [SQL Server installation guide](../../database-engine/install-windows/install-sql-server.md)
