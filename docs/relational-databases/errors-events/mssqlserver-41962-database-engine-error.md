---
title: "MSSQLSERVER_41962"
description: "MSSQLSERVER_41962"
author: djordje-jeremic
ms.author: djjeremi
ms.date: 12/06/2024
ms.service: sql
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords:
  - "41962 (Database Engine error)"
---
# MSSQLSERVER_41962

 [!INCLUDE [SQL MI](../../includes/applies-to-version/asmi.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|41962|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Message Text|Operation was aborted as replication to Azure SQL Managed Instance did not start within %u minutes since it was initiated. Please verify the network connectivity and firewall rules are configured according to the guidelines described at https://aka.ms/mi-link-troubleshooting, and retry the operation.|  
  
## Explanation  

The operation to create the link was aborted because replication between your SQL Server instance and Azure SQL Managed Instance didn't start within 5 minutes of the operation being initiated. This could be due to network connectivity issues or incorrectly configured firewall rules.
  
## User action  

Verify that network connectivity was successfully established between SQL Server and Azure SQL Managed Instance, and that firewall rules are correctly configured, as described in [Configure network connectivity](/azure/azure-sql/managed-instance/managed-instance-link-preparation#configure-network-connectivity). Retry the operation after connection has been verified successfully. 

## Related content

- [Managed Instance link overview](/azure/azure-sql/managed-instance/managed-instance-link-feature-overview)
- [Prepare environment for the link](/azure/azure-sql/managed-instance/managed-instance-link-preparation)
- [Configure link with SSMS](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-ssms)
- [Configure link with scripts](/azure/azure-sql/managed-instance/managed-instance-link-configure-how-to-scripts)
- [Troubleshoot issues with the link](/azure/azure-sql/managed-instance/managed-instance-link-troubleshoot-how-to)
