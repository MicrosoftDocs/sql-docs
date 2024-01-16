---
title: Connect to Microsoft Azure Storage
description: Connect to Microsoft Azure Storage.
author: markingmyname
ms.author: maghan
ms.reviewer: randolphwest
ms.date: 11/16/2023
ms.service: sql
ms.subservice: ssms
ms.topic: ui-reference
f1_keywords:
  - "sql13.swb.windowsazurestorage.connect.f1"
---

# Connect to Microsoft Azure Storage

[!INCLUDE [SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Use the **Azure Storage Connection** dialog to specify a storage account and validate your connection to Azure.

## Options

Specify the following information about your Azure account, and then select **Next** to continue.

1. **Storage Account** - Specify the storage account name.

   You can only connect to [General-purpose Storage Accounts](/azure/storage/common/storage-introduction#azure-storage-services). Connecting to other types of storage accounts can result in an error similar to the following example:

   > The value for one of the HTTP headers is not in the correct format. (Microsoft.SqlServer.StorageClient).
   >  
   > The remote server returned an error: (400) Bad Request. (System)

1. **Account Key** - Specify the account key for the specified storage account.

1. **Use secure endpoints (HTTPS)** - This option utilizes encrypted communication and secure identification of a network web server.

1. **Save account key** - This option saves your password in an encrypted file.
