---
title: Usage and Diagnostic Data
description: "Local audit for SSMS usage and diagnostic data collection"
ms.service: sql
ms.subservice: ssms
ms.topic: conceptual
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: ""
ms.custom: seo-lt-2019
ms.date: 05/03/2021
---

# Local audit for SSMS usage and diagnostic data collection

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

SQL Server Management Studio (SSMS) contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft. 

SQL Server Management Studio (SSMS) may collect standard computer, use, and performance information that may be transmitted to Microsoft and analyzed to improve the quality, security, and reliability of SSMS.

SQL Server Management Studio (SSMS) doesn't collect your name, address, or other data related to an identified or identifiable individual.

For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](../sql-server/sql-server-privacy.md).

## Audit feature usage and diagnostic data

To see feature usage data that is collected by SSMS, follow the steps below:

1. Launch SSMS.
2. Select **View**, then Select **Output** in the main menu to show the **Output** window. 
3. When the **Output** window is visible, choose **Telemetry** in the **Show output from:** menu.

While you use SSMS to interact with your databases, the **Output** window shows the data that is collected.

## Enable or disable usage and diagnostic data collection in SSMS

To opt in or out of usage data collection for SSMS:

- For SQL Server Management Studio (17 and later):

  `Subkey = HKEY_CURRENT_USER\Software\Microsoft\SQL Server Management Studio\14.0`

  RegEntry name = `UserFeedbackOptIn`

  Entry type `DWORD`: `0` is opt out; `1` is opt in

  Additionally, SSMS is based on the Visual Studio shell, and the Visual Studio installation enables customer feedback by default.  

  To configure Visual Studio to disable customer feedback on individual computers, change the value of the following registry subkey to string `0`: `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\VisualStudio\SQM OptIn`

  For example, change the subkey:  
  `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\VisualStudio\SQM OptIn `=` 0`

  Registry-based Group Policy on these registry subkeys is honored by SQL Server 2017 usage and diagnostic data collection.

- For SQL Server Management Studio 18:

  `Subkey = HKEY_CURRENT_USER\Software\Microsoft\SQL Server Management Studio\18.0_IsoShell`

  RegEntry name = `UserFeedbackOptIn`

  Entry type `DWORD`: `0` is opt out; `1` is opt in

## See also

- [Configure usage and diagnostic data collection for SQL Server](../sql-server/usage-and-diagnostic-data-configuration-for-sql-server.md)
- [Local audit for SQL Server usage and diagnostic data collection](../sql-server/usage-and-diagnostic-data-in-local-audit.md)