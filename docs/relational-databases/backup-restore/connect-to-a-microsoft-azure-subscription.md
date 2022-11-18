---
title: "Connect to a Microsoft Azure Subscription | Microsoft Docs"
description: Register an Azure Blob Storage container with your instance of SQL Server, which creates a shared access signature, stored access policy, and SQL Server Credential.
ms.custom: ""
ms.date: "03/01/2017"
ms.service: sql
ms.subservice: backup-restore
ms.topic: conceptual
ms.assetid: cca5a270-643f-4677-8802-98464f19f82a
author: MashaMSFT
ms.author: mathoma
---
# Connect to a Microsoft Azure Subscription
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
Use **Connect to a Microsoft Subscription** to register an existing Azure Blob Storage container with your instance of SQL Server.  The dialog box will create a shared access signature and stored access policy on an Azure Blob Storage container and then create a SQL Server Credential.  This dialog box appears when using the Back Up or Restore task from SQL Server Management Studio and the operation involves a URL device.

## Limitation
**Connect to a Microsoft Subscription** will only work with an Azure Storage Account created through the Service Management (Classic) deployment model.  For more information regarding Azure deployment models, see [Azure Resource Manager vs. classic deployment](/azure/azure-resource-manager/management/deployment-models).

## Options
**Sign in**     
Sign in with the appropriate Azure account.

**Select a subscription to use**      
Select desired subscription from the drop-down list.

**Select Storage Account**  
Select desired storage account from the drop-down list.

**Select Blob Container**   
Select desired blob container from the drop-down list.

**Shared Access Policy Expiration**   
The shared access policy will expire on the given date.  The default expiration date is one year from creation.  Modify as desired.

**Shared Access Signature Generated**   
The rich text box will display the generated shared access signature.

**Create Credential**   
Button will generate a stored access policy and shared access signature and then create a SQL Server credential.