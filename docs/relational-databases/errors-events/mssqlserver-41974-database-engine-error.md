---
title: "MSSQLSERVER_41974"
description: "MSSQLSERVER_41974"
author: djordje-jeremic
ms.author: djjeremi
ms.date: 12/06/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "41974 (Database Engine error)"
---
# MSSQLSERVER_41974

 [!INCLUDE [SQL MI](../../includes/applies-to-version/asmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41974|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Message Text|The link cannot be established because the endpoint certificate from SQL Azure SQL Managed Instance has not been imported to SQL Server. Please download the endpoint certificate from Managed Instance and import it to SQL Server, and retry the link creation again. Please see online documentation for Managed Instance link for more information.|  
  
## Explanation  

The endpoint certificate from Azure SQL Managed Instance hasn't been imported to SQL Server, so the link can't be established.
  
## User action  

[Import the Azure SQL Managed Instance public key into SQL Server](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-scripts#get-the-certificate-public-key-from-sql-managed-instance-and-import-it-to-sql-server) and then try creating the link again.

## Related content

- [Managed Instance link overview](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview)
- [Prepare environment for the link](/azure/azure-sql/managed-instance/managed-instance-link-preparation)
- [Configure link with SSMS](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-ssms)
- [Configure link with scripts](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-scripts)
- [Troubleshoot issues with the link](/azure/azure-sql/managed-instance/managed-instance-link-troubleshoot-how-to)
