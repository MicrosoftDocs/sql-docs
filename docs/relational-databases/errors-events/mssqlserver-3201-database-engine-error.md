---
description: "MSSQLSERVER_3201"
title: "MSSQLSERVER_3201 | Microsoft Docs"
ms.custom: ""
ms.date: "08/15/2022"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3201 (Database Engine error)"
author: pijocoder
ms.author: jopilov
---
# MSSQLSERVER_3201
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|3201|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|BADOPEN|  
|Message Text| Cannot open backup device '%ls'. Operating system error %ls.|  
  
## Explanation

The error occurs when SQL Server isn't able to create a new or open an existing backup device to perform a backup operation. Backup devices include files, tapes, virtual device for VDI. The message includes the operating system error raised and returned when the device access is performed. This OS error provides key information to help you understand the root cause of the failure.

The state of the error may also provide some useful information. Here are some useful states:

- State 1 and 3 - create backup file
- State 2 - open backup file
- State 5 - open tape
- State 7 - initialize virtual device
- State 6 - open virtual device
- State 8 - open and obtain ownership of a tape device

## Possible Causes

Multiple reasons may exist for not being able to access a backup device. Common examples include 
- The backup device isn't available or is invalid. Typical examples of OS errors associated with this scenario are: error 2 (The system cannot find the file specified.), error 6 (The handle is invalid.), error 3 (The system cannot find the path specified.)
- Insufficient permissions prevent SQL Server from accessing it the device- OS error 5 (Access is denied.), OS error 1117 (The request could not be performed because of an I/O device error.)
- The device is damaged or contains damaged or corrupt data - OS error 1117 (The request could not be performed because of an I/O device error.), OS error 21 (The device is not ready.), OS error 23 (Data error (cyclic redundancy check).), OS error 27 (The drive cannot find the sector requested.)


## User Action  

To address errors where the device is unavailable or invalid, ensure that you're specifying the correct device location (path, name) and ensure that the device exists and is online. For example, use Windows Explorer to navigate to a backup file and ensure it's present. For backup to URL on an Azure storage account, you can perform [ping](/windows-server/administration/windows-commands/ping) or [PsPing](/sysinternals/downloads/psping) on port 443 to check connectivity.

To address errors of insufficient permissions, ensure that the SQL Server startup account has been granted read and write access to the backup device. For backup to URL to cloud like Azure, ensure a valid shared access signature (SAS) token or Managed identities exist to access an Azure resource.

Damaged or corrupt data on devices are commonly issues with the hardware or less commonly with the OS. Investigate with your system administrator if the device is intact and if you can read or write test data to it, and repair or replace damaged device.
