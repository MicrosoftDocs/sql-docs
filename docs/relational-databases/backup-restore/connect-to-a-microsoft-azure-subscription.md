---
title: "Connect to a Microsoft Azure Subscription"
description: Register an Azure Blob Storage container with your instance of SQL Server, which creates a shared access signature, stored access policy, and SQL Server Credential.
author: MashaMSFT
ms.author: mathoma
ms.date: 12/04/2023
ms.service: sql
ms.subservice: backup-restore
ms.topic: ui-reference
---
# Connect to a Microsoft Azure Subscription (Backup TO URL)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Use the **Connect to a Microsoft Subscription** dialogue box in SQL Server Management Studio (SSMS) to register an existing Azure Blob Storage container with your instance of SQL Server. The dialog box will create a shared access signature and stored access policy on an Azure Blob Storage container and then create a SQL Server Credential. 

This dialog box appears when using the **Back Up Database** or **Restore Database** task from SQL Server Management Studio and the operation involves a URL device.

## Limitation

**Connect to a Microsoft Subscription** will only work with an Azure Storage Account created through the Service Management (Classic) deployment model.  For more information regarding Azure deployment models, see [Azure Resource Manager vs. classic deployment](/azure/azure-resource-manager/management/deployment-models).

## Options
**Sign in**     
Sign in with the appropriate Azure account.

**Select a subscription to use**      
Select desired subscription from the dropdown list.

**Select Storage Account**  
Select desired storage account from the dropdown list.

**Select Blob Container**   
Select desired blob container from the dropdown list.

**Shared Access Policy Expiration**   
The shared access policy will expire on the given date.  The default expiration date is one year from creation.  Modify as desired.

**Shared Access Signature Generated**   
The rich text box will display the generated shared access signature.

**Create Credential**   
Button will generate a stored access policy and shared access signature and then create a SQL Server credential.
  
## Related content

- [SQL Server backup to URL for Microsoft Azure Blob Storage](sql-server-backup-to-url.md)
