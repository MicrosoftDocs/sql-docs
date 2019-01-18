---
title: "Install non-English language versions of SQL Server Management Studio (SSMS) | Microsoft Docs"
description: "Install non-English language versions of SQL Server Management Studio (SSMS)"
ms.custom: ""
ms.date: "12/08/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: ssms
ms.topic: conceptual
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# Install non-English language versions of SQL Server Management Studio (SSMS) 

[SSMS is available in several languages](download-sql-server-management-studio-ssms.md#available-languages-ssms-1791), but the SSMS installer blocks installation on computers when their system locale doesn't match the SSMS language. 

The following directions differ depending on your version of Windows. The following are for Windows 10.

## Install non-English SSMS on a computer running an English operating system (OS)

1. Install the Windows language pack for the language you want SSMS to use: 
   - **Settings** > **Time & language** > **Region & language** > **Add a language** 
2. Now set the system locale to use the language pack installed in the previous step by clicking the language just installed, then select **Set as default**. (After installing SSMS, you can set the system locale back to English.)
3. Once your operating system is running in the desired language, [SSMS is available in several languages](download-sql-server-management-studio-ssms.md#available-languages-ssms-1791). The first time you install a new SSMS language, use the full package. You can use the upgrade package for subsequent installs.
4. Run SSMS, and it should display as the language you installed in the previous step.
5. Set your computer's system locale back to English.

## Install SSMS in a language other than the language of the installed OS

1. Install the Windows language pack for the language you want SSMS to use: 
   - **Settings** > **Time & language** > **Region & language** > **Add a language** 
2. Now set the system locale to use the language pack installed in the previous step by clicking the language just installed, then select **Set as default**. 
3. Once your operating system is running in the desired language, [SSMS is available in several languages](download-sql-server-management-studio-ssms.md#available-languages-ssms-1791). The first time you install a new SSMS language, use the full package. You can use the upgrade package for subsequent installs.
4. For each language you want to install that does not match the language of the first version of SSMS you installed, install the corresponding Visual Studio 2015 Shell (Isolated) Language Pack:
   - Browse to [https://connect.microsoft.com/VisualStudio/ExtendVS](https://connect.microsoft.com/VisualStudio/ExtendVS) (you may need to sign in and complete the *Connect Registration* process).
   - Download the desired Visual Studio 2015 Shell (Isolated) Language Pack and install it.

   > [!IMPORTANT]
   > Use the previous steps to install the Visual Studio 2015 Isolated Shell Language Pack, do not use the **Get additional languages** link on **Tools** | **Options** | **International Settings**. 

5. Run SSMS and select the language you want to use in:
   - **Tools** | **Options** | **International Settings**
1. Close and restart SSMS.

## Next steps

- [Tutorial: SQL Server Management Studio](https://docs.microsoft.com/sql/ssms/tutorials/tutorial-sql-server-management-studio)
