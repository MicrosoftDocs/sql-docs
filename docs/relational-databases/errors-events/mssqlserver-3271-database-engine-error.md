---
title: "MSSQLSERVER_3271 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
helpviewer_keywords: 
  - "3271 (Database Engine error)"
ms.assetid: 21b8de4b-6624-4163-9561-1a6cc8fe3d51
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_3271
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|3271|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DMPIO_IO_ERROR|  
|Message Text|A nonrecoverable I/O error occurred on file "%ls:" %ls.|  
  
## Explanation  
This is a general error that occurs when the operating system raises an error while performing I/O during a backup or restore operation. In most situations the cause is simply that the backup medium is full.  
  
The error may include additional text from the operating system indicating that the disk is full. When performing a backup or restore operation with third-party backup software an additional message may occur indicating that the backup failed. The message may look similar to the following text:  
  
```  
"2005-08-02 16:05:16.04 spid55 Error: 18210, Severity: 16, State: 1.  
 2005-08-02 16:05:16.04 spid55 BackupVirtualDeviceFile  
::RequestDurableMedia: Flush failure on backup device 'VDINULL'.   
Operating system error 995(The I/O operation has been aborted because   
of either a thread exit or an application request.)."  
```  
  
This is an indication that the backup software requested a termination of the backup or restore operation.  
  
## User Action  
Perform the following tasks as appropriate:  
  
-   Review the underlying system error messages and SQL Server error messages preceding this one to identify the cause of the failure.  
  
-   Ensure that the backup and restore medium has sufficient space.  
  
-   Correct any errors raised by third-party backup and restore software.  
  
