---
title: "Install non-English language versions of SQL Server Management Studio (SSMS) | Microsoft Docs"
ms.custom: ""
ms.date: "11/16/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "stevestein"
ms.author: "sstein"
manager: "craigg"
ms.workload: "Inactive"
---
# Install non-English language versions of SQL Server Management Studio (SSMS)

[SSMS is available in several languages](download-sql-server-management-studio-ssms.md#available-languages), but the SSMS installer blocks installation on computers when their system locale doesn't match the SSMS language. 

## Install non-English SSMS on a computer that has its system locale set to English

The following steps differ depending on your version of Windows. The following steps are for Windows 10:

1. Install the Windows language pack for the language you want SSMS to use. 
   - **Settings** > **Time & language** > **Region & language** > **Add a language** 

1. Set the system locale to use the language pack installed in the previous step. (After installing SSMS, you can set the system locale back to English.)
   
  1. **Settings** > **Time & language** > **Region & language** > **Additional date, time, & regional settings**.
  2. Now select **Change location**, then click the **Administrative** tab, and select **Change system locale**.
  3. Set **Current system locale** to use the language pack you installed in the previous steps.

1. After your operating system is running in the desired language, [install SSMS version of the same language](download-sql-server-management-studio-ssms.md#available-languages).
2. Run SSMS, and it should display as the language you installed in the previous step.
3. Set your computer's sytem locale back to English.



