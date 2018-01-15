---
title: "Store and retrieve files on file shares on premises and in Azure | Microsoft Docs"
description: "This article describes how to use the file system and file shares, both on premises and in Azure, with SSIS"
ms.date: "11/27/2017"
ms.topic: "article"
ms.prod: sql-non-specified
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
ms.workload: "Inactive"
---
# Store and retrieve files on file shares on premises and in Azure with SSIS
This article describes how to update your SQL Server Integration Services (SSIS) packages when you lift and shift packages that use local file systems into SSIS in Azure.

> [!IMPORTANT]
> Currently, the SSIS Catalog database (SSISDB) only supports a single set of access credentials. Therefore the Azure-SSIS Integration Runtime (IR) can't use different credentials to connect to multiple on-premises file shares and Azure Files shares.

## Store temporary files
If you need to store and process temporary files during a single package execution, packages can use the current working directory (`.`) or temporary folder (`%TEMP%`) of your Azure-SSIS Integration Runtime nodes.

## Store files across multiple package executions
If you need to store and process permanent files and persist them across multiple package executions, you can use either on-premises file shares or Azure Files

### Use on-premises file shares
To continue to use **on-premises file shares** when you lift and shift packages that use local file systems into SSIS in Azure, do the following things:
1.	Transfer files from local file systems to on-premises file shares.
2.	Join the on-premises file shares to an Azure virtual network (VNet).
3.	Join your Azure-SSIS IR to the same VNet. For more info, see [Join an Azure-SSIS integration runtime to a virtual network](https://docs.microsoft.com/azure/data-factory/join-azure-ssis-integration-runtime-virtual-network).
4.	Connect your Azure-SSIS IR to the on-premises file shares inside the same VNet by setting up access credentials that use Windows authentication. For more info, see [Connect to on-premises data sources and Azure file shares with Windows Authentication](ssis-azure-connect-with-windows-auth.md).
5.	Update local file paths in your packages to UNC paths pointing to on-premises file shares. For example, update `C:\abc.txt` to `\\<on-prem-server-name>\<share-name>\abc.txt`.

### Use Azure file shares
To use **Azure Files** when you lift and shift packages that use local file systems into SSIS in Azure, do the following things:
1.	Transfer files from local file systems to Azure Files. For more info, see [Azure Files](https://azure.microsoft.com/services/storage/files/).
2.	Connect your Azure-SSIS IR to Azure Files by setting up access credentials that use Windows authentication. For more info, see [Connect to on-premises data sources and Azure file shares with Windows Authentication](ssis-azure-connect-with-windows-auth.md).
3.	Update local file paths in your packages to UNC paths pointing to Azure Files. For example, update `C:\abc.txt` to `\\<storage-account-name>.file.core.windows.net\<share-name>\abc.txt`.
