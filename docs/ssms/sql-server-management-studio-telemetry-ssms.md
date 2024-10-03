---
title: Usage and Diagnostic Data
description: "Local audit for SSMS usage and diagnostic data collection"
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 09/12/2024
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
---

# Local audit for SSMS usage and diagnostic data collection

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL Server Management Studio (SSMS) contains Internet-enabled features that collect and send anonymous feature usage and diagnostic data to Microsoft.

SSMS might collect standard computer usage and performance information, which could be transmitted to Microsoft and analyzed to improve its quality, security, and reliability.

However, SSMS doesn't collect your name, address, or other data that could identify you as an individual.

For more information, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement) and the [SQL Server privacy supplement](../sql-server/sql-server-privacy.md).

## Audit feature usage and diagnostic data

To view the feature usage data collected by SSMS, follow these steps:

1. Launch SSMS.
1. Select **View** from the main menu, then select **Output** to display the **Output** window.
1. In the **Output** window, choose **Telemetry** from the **Show output from:** dropdown list menu.

As you interact with SSMS, the **Output** window displays the collected data.

## Enable or disable usage and diagnostic data collection in SSMS

To opt in or out of SSMS usage data collection, use the following methods:

- **Registry Subkey:** `HKEY_CURRENT_USER\Software\Microsoft\SQL Server Management Studio\20.0_IsoShell`
- **Registry Entry Name:** `UserFeedbackOptIn`
- **Entry Type:** `DWORD`
- **Values:** `0` to opt out, `1` to opt in

Additionally, SSMS is based on the Visual Studio shell, which enables customer feedback by default. For more information and opt-out options, see [Visual Studio Customer Experience Improvement Program](/visualstudio/ide/visual-studio-experience-improvement-program).

Registry-based Group Policy on these registry subkeys is honored by [!INCLUDE [ssnoversion-md](../includes/ssnoversion-md.md)] usage and diagnostic data collection.

## Related content

- [Configure usage and diagnostic data collection for SQL Server (CEIP)](../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md)
- [Local audit for SQL Server usage and diagnostic data collection (CEIP)](../sql-server/usage-and-diagnostic-data-in-local-audit.md)
