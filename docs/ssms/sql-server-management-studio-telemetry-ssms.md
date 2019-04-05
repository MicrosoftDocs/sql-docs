---
title: "SQL Server Management Studio - Telemetry (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "02/20/2017"
ms.prod: sql
ms.prod_service: "sql-tools"
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
ms.assetid: c28ffa44-7b8b-4efa-b755-c7a3b1c11ce4
author: "stevestein"
ms.author: "sstein"
manager: craigg
---

# Local audit for SSMS usage and diagnostic data collection
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../includes/appliesto-ss-asdb-asdw-pdw-md.md)]

SQL Server Management Studio (SSMS) contains Internet-enabled features that can collect and send anonymous feature usage and diagnostic data to Microsoft. SSMS may collect standard computer information and information about use and performance that may be transmitted to Microsoft and analyzed for purposes of improving the quality, security, and reliability of SSMS. We do not collect your name, address or other contact information. For details, see the [Microsoft Privacy Statement](https://privacy.microsoft.com/privacystatement), and [SQL Server Privacy supplement](https://go.microsoft.com/fwlink/?LinkID=868444).

## Audit feature usage data

To see feature usage data that is collected by SSMS, do the following:
1.	Launch SSMS.
2.	Click **View**, then click **Output** in the main menu to show the **Output** window. 
3.	When the **Output** window is visible, choose **Telemetry** in the **Show output from:** menu.

While you use SSMS to interact with your databases, the **Output** window shows the data that is collected.

## Enable or disable usage feedback collection in SSMS

To opt in or out of usage data collection for SSMS:

- For SQL Server Management Studio 17:

  `Subkey = HKEY_CURRENT_USER\Software\Microsoft\SQL Server Management Studio\14.0`

  RegEntry name = `UserFeedbackOptIn`

  Entry type `DWORD`: `0` is opt out; `1` is opt in

  Additionally, SSMS 17.x is based on the Visual Studio 2015 shell, and the Visual Studio installation enables customer feedback by default.  

  To configure Visual Studio to disable customer feedback on individual computers, change the value of the following registry subkey to string `0`: `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\VisualStudio\SQM OptIn`

  For example, change the subkey to the following:  
  `HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\VisualStudio\SQM OptIn `=` 0`

  Registry-based Group Policy on these registry subkeys is honored by SQL Server 2017 usage and diagnostic data collection.

- For SQL Server Management Studio 18:

  `Subkey = HKEY_CURRENT_USER\Software\Microsoft\SQL Server Management Studio\18.0_IsoShell`

  RegEntry name = `UserFeedbackOptIn`

  Entry type `DWORD`: `0` is opt out; `1` is opt in

## See also

[Local Audit for SQL Server Usage Feedback Collection](https://msdn.microsoft.com/library/mt743085.aspx)
