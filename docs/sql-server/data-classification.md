---
title: "SQL Server Data Classification Matrix | Microsoft Docs"
ms.date: "2/14/2018"
ms.prod: "sql-non-specified"
ms.prod_service: "sql-non-specified"
ms.service: ""
ms.component: "sql-non-specified"
ms.reviewer: ""
ms.suite: "sql"
ms.custom: ""
ms.technology: ""
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
helpviewer_keywords: 
author: "craigg-msft"
ms.author: "craigg"
manager: "jhubbard"
ms.workload: "Active"
---
# SQL Server Documentation
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]


|Data Category  |Definition  |Examples  |Details  |
|---------|---------|---------|---------|
|Access Control     |Credential-related information used to secure logins, users, or accounts within a SQL Server installation         |- Passwords<br>- certificates         |         |
|Row2     |         |         |         |
|Row3     |         |         |         |
|Row4     |         |         |         |
|Row5     |         |         |         |
|Row6     |         |         |         |
|Row7     |         |         |         |
|Row8     |         |         |         |

## Access Control
Credential-related information used to secure logins, users, or accounts within a SQL Server installation.
### Examples
- Passwords
- Certificates
 
### Permitted Usage Scenarios

|Scenario  |Access Restrictions  |Retention Requirements |
|---------|---------|---------|
|These credentials never leave the user machine via Usage Feedback.     |NA         |NA         |
|Crash Dumps may contain Access Control Data.     |NA         |Crash Dumps: Maximum 30 days.         |
|These credentials never leave the user machine via User Feedback unless Customer injects it manually    |Limit to MSFT internal with no third party access.         |User Feedback: Max 1 year         |
 |


  
[!INCLUDE[get-help-options](../includes/paragraph-content/get-help-options.md)]
