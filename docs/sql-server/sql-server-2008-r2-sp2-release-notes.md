---
title: "SQL Server 2008 R2 SP2 Release Notes | Microsoft Docs"
description: This Release Notes document describes known issues that you should read about before you install or troubleshoot Microsoft SQL Server 2008 R2 Service Pack 2.
ms.service: sql
ms.subservice: release-landing
ms.custom: ""
ms.date: 07/22/2020
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL Server 2008 R2 SP2"
  - "Release Notes, SQL Server 2008 R2 SP2"
ms.assetid: e2bd3de7-674c-4ea7-8d53-bb40bba86fae
author: MikeRayMSFT
ms.author: mikeray
monikerRange: "= sql-server-2016"
---
# SQL Server 2008 R2 SP2 Release Notes
[!INCLUDE[sqlserver](../includes/applies-to-version/sqlserver.md)]
This Release Notes document describes known issues that you should read about before you install or troubleshoot Microsoft SQL Server 2008 R2 Service Pack 2. This Release Notes document applies to all editions of SQL Server 2008 R2 SP2 and is available online only. It is updated periodically.  
  
## 1.0 What's New in Service Pack 2  
Added the dynamic management view (DMV) **sys.dm_db_stats_properties**. You can use this DMV to return statistics properties for a specified table or indexed view in the current database. For example, this DMV returns the number of rows that were sampled and the number of steps in the histogram.  
  
## 2.0 Before You Install  
For information about how to install [!INCLUDE[ssKilimanjaro](../includes/sskilimanjaro-md.md)] updates, see [SQL Server 2008 R2 Servicing Documentation](/previous-versions/sql/sql-server-2008-r2/dd638062(v=sql.105)).  
  
For general information about how to get started and install SQL Server 2008 R2, see the SQL Server 2008 R2 Readme. The Readme document is available on the installation media.
  
### 2.1 Choose the Correct File to Download and Install  
Use the following table to determine which file to download and install. Verify that you have the correct system requirements before installing the service pack. The system requirements are provided on the download pages that are linked to in the table.  
  
|If your current installed version is...|And you want to...|Download and install...|  
|-------------------------------------------|----------------------|---------------------------|  
|A 32-bit version of any edition of SQL Server 2008 R2 or SQL Server 2008 R2 SP1|Upgrade to the 32-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x86-ENU from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 32-bit version of SQL Server 2008 R2 RTM Express or SQL Server 2008 R2 SP1 Express|Upgrade to the 32-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x86-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 32-bit version of only the client and manageability tools for SQL Server 2008 R2 or SQL Server 2008 R2 SP1 (including SQL Server 2008 R2 Management Studio)|Upgrade the client and manageability tools to the 32-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x86-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 32-bit version of SQL Server 2008 R2 Management Studio Express or SQL Server 2008 R2 SP1 Management Studio Express|Upgrade to the 32-bit version of SQL Server 2008 R2 SP2 Management Studio Express|SQLManagementStudio_x86_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251791)|  
|A 32-bit version of any edition of SQL Server 2008 R2 or SQL Server 2008 R2 SP1 **and** a 32-bit version of the client and manageability tools (including SQL Server 2008 R2 RTM Management Studio)|Upgrade all products to the 32-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x86-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 32-bit version of one or more tools from the [Microsoft SQL Server 2008 R2 RTM Feature Pack](https://www.microsoft.com/download/details.aspx?id=44272)|Upgrade the tools to the 32-bit version of Microsoft SQL Server 2008 R2 SP2 Feature Pack|One or more files from [Microsoft SQL Server 2008 R2 SP2 Feature Pack](https://www.microsoft.com/download/details.aspx?id=30438)|  
|No 32-bit installation of SQL Server 2008 R2|Install Server 2008 R2 including SP2|Go to [SQL Server 2008 R2 SP2 - Express Edition](https://go.microsoft.com/fwlink/?LinkId=251791) and follow the instructions.|  
|No 32-bit installation of SQL Server 2008 R2 Management Studio|Install SQL Server 2008 R2 Management Studio including SP2|SQLManagementStudio_x86_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251791) to install the free SQL Server 2008 R2 SP2 Management Studio Express Edition.|  
|A 64-bit version of any edition of SQL Server 2008 R2 or SQL Server 2008 R2 SP1|Upgrade to the 64-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x64-ENU or SQLServer2008R2SP2-KB2630455-IA64-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 64-bit version of SQL Server 2008 R2 RTM Express or SQL Server 2008 R2 SP1 Express|Upgrade to the 64-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x64-ENU.exe or SQLServer2008R2SP2-KB2630455-IA64-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 64-bit version of only the client and manageability tools for SQL Server 2008 R2 or SQL Server 2008 R2 SP1 (including SQL Server 2008 R2 Management Studio)|Upgrade the client and manageability tools to the 64-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x64-ENU.exe or SQLServer2008R2SP2-KB2630455-IA64-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 64-bit version of SQL Server 2008 R2 Management Studio Express or SQL Server 2008 R2 SP1 Management Studio Express|Upgrade to the 64-bit version of SQL Server 2008 R2 SP2 Management Studio Express|SQLManagementStudio_x64_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251791)|  
|A 64-bit version of any edition of SQL Server 2008 R2 or SQL Server 2008 R2 SP1 **and** a 64-bit version of the client and manageability tools (including SQL Server 2008 R2 RTM Management Studio)|Upgrade all products to the 64-bit version of SQL Server 2008 R2 SP2|SQLServer2008R2SP2-KB2630458-x64-ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251790)|  
|A 64-bit version of one or more tools from the [Microsoft SQL Server 2008 R2 RTM Feature Pack](https://www.microsoft.com/download/details.aspx?id=44272)|Upgrade the tools to the 64-bit version of Microsoft SQL Server 2008 R2 SP2 Feature Pack|One or more files from [Microsoft SQL Server 2008 R2 SP2 Feature Pack](https://www.microsoft.com/download/details.aspx?id=30438)|  
|No 64-bit installation of SQL Server 2008 R2|Install Server 2008 R2 including SP2|Go to [SQL Server 2008 R2 SP2 - Express Edition](https://go.microsoft.com/fwlink/?LinkId=251791) and follow the instructions.|  
|No 64-bit installation of SQL Server 2008 R2 Management Studio|Install SQL Server 2008 R2 Management Studio including SP2|SQLManagementStudio_x64_ENU.exe from [here](https://go.microsoft.com/fwlink/p/?LinkId=251791) to install the free SQL Server 2008 R2 SP2 Management Studio Express Edition.|  
  
### 2.2 Setup Might Fail if SQAGTRES.dll Is Locked by Another Process  
**Issue**: A SQL Server setup operation might fail with this error: `Upgrading of cluster resource C:\Program Files\Microsoft SQL Server\MSSQL10_50.<Instance name>\MSSQL\Binn\SQAGTRES.DLL on machine <Computer name> failed with Win32Exception. Please look at inner exception for details.` The root cause is that C:\Windows\system32\SQAGTRES.DLL is locked by another process and Setup was not able to update it.  
  
**Workaround**: Rename C:\Windows\system32\SQAGTRES.DLL to a temporary name such as C:\Windows\system32\SQAGTRES_old.DLL, and then select the Retry option on the setup error message. That will allow Setup to continue. After a reboot, you can delete the temporary file C:\Windows\system32\SQAGTRES_old.DLL.  
  
## 3.0 Known Issues Fixed in this Service Pack  
For a complete list of bugs and known issues fixed in this service pack, see this [master KB article](https://support.microsoft.com/kb/2630455).  
  
## See Also  
[How to determine the version and edition of SQL Server](https://support.microsoft.com/kb/321185)  
