---
title: "Connect to Microsoft Azure Storage (Restore) | Microsoft Docs"
description: In SQL Server, the Azure Storage Account dialog box lets you specify a connection to Azure storage account information to get files storage in an Azure account.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.restore.connectstorage.f1"
ms.assetid: c0b7d7c8-b878-4b7f-8120-d0c6917b583f
author: MashaMSFT
ms.author: mathoma
---
# Connect to Microsoft Azure Storage (Restore)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  The dialog box allows you to specify the connection to Azure storage account information in order to retrieve the files storage in the Azure storage account. After specifying the required information, click **Connect** to establish the connection to Azure storage.  
  
## Azure Storage Account  
 **Storage account**  
 Select, type or paste the name of the Azure storage account you want to use. The drop down box lists the accounts previously used.  
  
 **Account key**  
 Specify the Azure storage account access key.  
  
 **Use secure endpoints (HTTPS)** check box  
 Select this option to make a secure connection to Azure storage - recommended.  
  
 **Save account key** check box  
 Select this check box if you want SQL Server to remember the access key for this storage account.  
  
### SQL Credential  
 **Select an existing credential**  
 Select an existing SQL Credential that matches the storage account and account key information.  
  
 **Create a new Credential**  
 Select this option to create a new credential using the storage account and account key information. Specify the name of the new credential in the **Credential Name** field.  
  
  
