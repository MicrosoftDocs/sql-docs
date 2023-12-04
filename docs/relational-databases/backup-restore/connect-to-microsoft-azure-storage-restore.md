---
title: "Connect to Microsoft Azure Storage (Restore) in SQL Server Management Studio (SSMS)"
description: In SQL Server Management Studio (SSMS) the Azure Storage Account dialog box lets you specify a connection to Azure storage account information to get files storage in an Azure account.
author: MashaMSFT
ms.author: mathoma
ms.date: 12/04/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.restore.connectstorage.f1"
---
# Connect to Microsoft Azure Storage (Restore from URL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

The dialog box in SQL Server Management Studio (SSMS) allows you to specify the connection to Azure storage account information in order to retrieve the files storage in the Azure storage account. After specifying the required information, select **Connect** to establish the connection to Azure storage.  
  
 This dialog box appears when using the **Restore Database** task from SQL Server Management Studio and the operation involves a URL device.

## Azure Storage Account

 **Storage account**  
 Select, type or paste the name of the Azure storage account you want to use. The dropdown list box lists the accounts previously used.  
  
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
  
## Related content

- [SQL Server backup to URL for Microsoft Azure Blob Storage](sql-server-backup-to-url.md)
