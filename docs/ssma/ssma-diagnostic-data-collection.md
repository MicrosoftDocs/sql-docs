---
title: SQL Server Migration Assistant (SSMA) Usage and Diagnostic Data Collection
description: Learn about usage and diagnostic data collection in SQL Server Migration Assistant.
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/31/2025
ms.service: sql
ms.subservice: ssma
ms.topic: how-to
ms.collection:
  - sql-migration-content
---
# SSMA usage and diagnostic data collection

SQL Server Migration Assistant (SSMA) contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft.

## Collected data

SSMA might collect standard computer information and information about use and performance that might be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability. SSMA doesn't collect your name, address, or any other data related to an identified or identifiable individual. For details, see the [Microsoft Privacy Statement](https://www.microsoft.com/privacy/privacystatement), and [SQL Server privacy supplement](../sql-server/sql-server-privacy.md).

## Enable or disable usage and diagnostic data collection in SSMA

The following registry entry allows you to opt in or out of data collection:

| Configuration | Registry entry | Entry type | Values |
| --- | --- | --- | --- |
| Opt in or out of data collection | `DisableTelemetry` | `STRING` | `True` is for opting out.<br />`False` or not present is for opting in. |
| Check for new version during startup | `DisableAutoUpdate` | `STRING` | `True` is for opting out.<br />`False` or not present is for opting in. |

Depending on the migration source, the entry should be placed under one of the following registry keys:

| Registry key (Default) | Registry key (32-bit app on 64-bit) |
| --- | --- |
| **SSMA for Access** | |
| `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for Access` | `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for Access` |
| **SSMA for Db2** | |
| `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for DB2` | `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for DB2` |
| **SSMA for MySQL** | |
| `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for MySQL` | `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for MySQL` |
| **SSMA for Oracle** | |
| `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for Oracle` | `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for Oracle` |
| **SSMA for Sybase** | |
| `HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server Migration Assistant for Sybase` | `HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Microsoft SQL Server Migration Assistant for Sybase` |

## Configure SSMA feedback policy using Group Policy Objects (GPO)

SQL Server Migration Assistant (SSMA) supports centralized control of feedback collection through a setting in the Windows registry. You can add or modify this registry entry using Group Policy Management to enable or disable feedback feature usage and diagnostic data.

### Registry details

- **Registry hive**: `HKEY_CURRENT_USER`
- **Path**: `Software\Policies\Microsoft\Microsoft SQL Server Migration Assistant\Feedback`
- **Value name**: `ProductFeedbackDisabled`
- **Value type**: `REG_DWORD`

  | Value | Description |
  | --- | --- |
  | `0` | Feedback enabled |
  | `1` | Feedback disabled |

### Add the registry entry via Group Policy Management

To deploy this policy using Group Policy Management, follow these steps:

1. Open **Group Policy Management** (`gpmc.msc`).

1. Select the **Domain** or **Organizational Unit (OU)** where the policy should apply.

   Since the registry location is under `HKEY_CURRENT_USER`, ensure that the GPO is linked to an OU containing user accounts.

1. Right-click the domain or target OU, and select **Create a GPO in this domain, and Link it here...**.

1. Name the GPO. For example, `SSMA Feedback Policy`.

1. Right-click the newly created GPO and select **Edit**.

1. Navigate to **User Configuration** > **Preferences** > **Windows Settings** > **Registry**.

1. Create a **New Registry Item**.

1. Configure the settings as follows:

   | Field | Value |
   | --- | --- |
   | **Action** | Update (recommended) |
   | **Hive** | `HKEY_CURRENT_USER` |
   | **Key Path** | `Software\Policies\Microsoft\Microsoft SQL Server Migration Assistant\Feedback` |
   | **Value Name** | `ProductFeedbackDisabled` |
   | **Value Type** | `REG_DWORD` |
   | **Value Data** | `0` (enable feedback) or `1` (disable feedback) |

1. Verify the applied setting using the following PowerShell script:

   ```powershell
   Get-ItemProperty -Path "HKCU:\Software\Policies\Microsoft\Microsoft SQL Server Migration Assistant\Feedback" -Name ProductFeedbackDisabled
   ```
