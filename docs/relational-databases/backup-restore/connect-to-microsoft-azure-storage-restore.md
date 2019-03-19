---
title: "Connect to Microsoft Azure Storage (Restore) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: backup-restore
ms.reviewer: ""
ms.technology: backup-restore
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.restore.connectstorage.f1"
ms.assetid: c0b7d7c8-b878-4b7f-8120-d0c6917b583f
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# Connect to Microsoft Azure Storage (Restore)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The dialog box allows you to specify the connection to Windows Azure storage account information in order to retrieve the files storage in the Windows Azure storage account. After specifying the required information, click **Connect** to establish the connection to Windows Azure storage.  
  
## Windows Azure Storage Account  
 **Storage account**  
 Select, type or paste the name of the Windows Azure storage account you want to use. The drop down box lists the accounts previously used.  
  
 **Account key**  
 Specify the Windows Azure storage account access key.  
  
 **Use secure endpoints (HTTPS)** check box  
 Select this option to make a secure connection to Windows Azure storage - recommended.  
  
 **Save account key** check box  
 Select this check box if you want SQL Server to remember the access key for this storage account.  
  
### SQL Credential  
 **Select an existing credential**  
 Select an existing SQL Credential that matches the storage account and account key information.  
  
 **Create a new Credential**  
 Select this option to create a new credential using the storage account and account key information. Specify the name of the new credential in the **Credential Name** field.  
  
  
