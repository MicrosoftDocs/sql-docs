---
title: "Connect to Microsoft Azure Storage | Microsoft Docs"
ms.custom: ""
ms.date: "07/12/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "tools-ssms"
ms.tgt_pltfrm: ""
ms.topic: "article"
f1_keywords: 
  - "sql13.swb.windowsazurestorage.connect.f1"
  - "SQL13.SWB.WINDOWSAZURESTORAGE.CONNECT.F1"
ms.assetid:
caps.latest.revision: 4
author: "stevestein"
ms.author: "sstein"
manager: "jhubbard"
---
# Connect to Microsoft Azure Storage
Use the **Windows Azure Storage Connection** dialog to specify a storage account and validate your connection to Windows Azure.  
  
## Options  
Specify the following information about your Windows Azure account, and then click **Next** to continue.  
  
1.  **Storage Account** - Specify the storage account name.

   >[!NOTE]
   > You can only connect to [General-purpose Storage Accounts](https://docs.microsoft.com/en-us/azure/storage/storage-introduction#introducing-the-azure-storage-services). Connecting to other types of storage accounts can result in an error similar to the following:
   >
   >  The value for one of the HTTP headers is not in the correct format. (Microsoft.SqlServer.StorageClient).
   >
   >  The remote server returned an error: (400) Bad Request. (System)

2.  **Account Key** - Specify the account key for the specified storage account.  
  
3.  **Use secure endpoints (HTTPS)** – This option utilizes encrypted communication and secure identification of a network web server.  
  
4.  **Save account key** - This option saves your password in an encrypted file.  
  
