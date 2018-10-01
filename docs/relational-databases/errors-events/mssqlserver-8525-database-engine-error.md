---
title: "MSSQLSERVER_8525 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: supportability
ms.topic: "language-reference"
f1_keywords: 
  - "8525"
helpviewer_keywords: 
  - "8525 (Database Engine error)"
ms.assetid: 297867c1-691e-4d6b-a3be-a7575015ecfa
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_8525
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|8525|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name||  
|Message Text|Distributed transaction completed. Either enlist this session in a new transaction or the NULL transaction.|  
  
## Explanation  
The programming model for using the Distributed Transaction Coordinator with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] requires applications to explicitly enlist to and defect from a distributed transaction.  
  
This error occurs when the following four conditions are met:  
  
-   The application has enlisted into a distributed transaction.  
  
-   The transaction has ended, either committed or rolled back, for any reason.  
  
-   The user application has not explicitly defected from a distributed transaction or explicitly enlisted into a new distributed transaction.  
  
-   The application tries to do any transactional operation other than defecting from existing distributed transaction or enlisting to a new distributed transaction, such as issuing a query or starting a local transaction.  
  
Error state 1 is used when the application performs an operation that creates local transactions, and state 2 is used when application tries to enlist to a bound session.  
  
## User Action  
After an application has enlisted into a distributed transaction, the application must explicitly defect from the distributed transaction or enlist to another distributed transaction. This will implicitly defect from a previous enlisted transaction. For the exact syntax to defect from or enlist to a distributed transaction, see the programming interface manual for the application.  
  
