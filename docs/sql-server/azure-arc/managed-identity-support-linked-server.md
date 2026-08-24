---
title: Configure Managed Identity for Linked Servers
description: Learn how to configure managed identity authentication for linked servers on SQL Server 2025 Azure Virtual Machines and SQL Server enabled by Azure Arc.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest
ms.date: 12/15/2025
ms.service: sql
ms.subservice: security
ms.topic: how-to
ai-usage: ai-assisted
monikerRange: ">=sql-server-ver17"
---

# Configure managed identity for linked servers

**Applies to**: [!INCLUDE [sqlserver2025](../../includes/applies-to-version/sqlserver2025.md)]

SQL Server 2025 introduces managed identity support for linked servers, enabling secure, credential-free authentication between SQL Server instances. This capability is available for both SQL Server on Azure Virtual Machines and SQL Server enabled by Azure Arc. With managed identity authentication, you can establish linked server connections without managing passwords or storing credentials, improving your security posture and simplifying credential management.

This article shows you how to set up linked server connections using managed identity authentication. You'll configure the source server to initiate connections and the destination server to accept managed identity-based authentication.

## Prerequisites

Before you begin, ensure you have the following:

- SQL Server 2025 running on either:
  - Azure Virtual Machine with SQL Server IaaS Agent extension installed, or
  - On-premises or virtual machine with Azure Arc enabled
- Microsoft Entra authentication configured on both source and destination servers
- Network connectivity between source and destination servers with appropriate firewall rules
- Appropriate permissions to create logins and configure linked servers on both instances
- For Azure Virtual Machines: Both *SqlIaasExtension* and *AADLogin* extensions enabled
- For Azure Arc-enabled instances: Azure Arc agent installed and configured

### Azure Virtual Machine requirements

When using SQL Server on Azure Virtual Machines:

- Verify that *SqlIaasExtension* and *AADLogin* extensions are installed. These extensions are included by default when deploying from the Azure Marketplace SQL Server template.
- Configure Microsoft Entra authentication following the guidance in [Enable Microsoft Entra authentication for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm?view=azuresql&tabs=azure-portal&preserve-view=true).
- Ensure both virtual machines allow inbound and outbound network traffic for SQL Server communication.
- Configure firewall rules on each virtual machine to permit SQL Server traffic.

### Azure Arc-enabled requirements

When using SQL Server enabled by Azure Arc:

1. Install and configure Azure Arc on your SQL Server 2025 instances following the [prerequisites for SQL Server enabled by Azure Arc](prerequisites.md).
1. Set up Microsoft Entra authentication using the guidance in [Set up Microsoft Entra authentication with app registration](entra-authentication-setup-tutorial.md).
1. Verify network connectivity between the source and destination servers.

## Create login on destination server

The destination server must have a login that matches the name of the source server. When the source server connects using managed identity, it authenticates using the machine's managed identity. The destination server validates this identity by matching it to a login created from an external provider.

1. Connect to the destination SQL Server instance.

1. Create a login using the source server's name:

   ```sql
   USE [master];
   GO

   CREATE LOGIN [AzureSQLVMSource]
   FROM EXTERNAL PROVIDER
   WITH DEFAULT_DATABASE = [master],
        DEFAULT_LANGUAGE = [us_english];
   GO
   ```

1. Grant the login appropriate server-level permissions. For example, to grant `sysadmin` role:

   ```sql
   ALTER SERVER ROLE [sysadmin] ADD MEMBER [AzureSQLVMSource];
   GO
   ```

   > [!TIP]  
   > Apply the principle of least privilege by granting only the permissions required for your specific use case rather than using `sysadmin`.

## Configure linked server on source server

After creating the login on the destination server, configure the linked server connection on the source server. This configuration uses the `MSOLEDBSQL` provider with managed identity authentication.

1. Connect to the source SQL Server instance.

1. Create the linked server using `sp_addlinkedserver`:

   ```sql
   USE [master];
   GO

   EXEC master.dbo.sp_addlinkedserver
       @server = N'AzureSQLVMDestination',
       @srvproduct = N'',
       @provider = N'MSOLEDBSQL',
       @datasrc = N'AzureSQLVMDestination',
       @provstr = N'Authentication=ActiveDirectoryMSI;';
   GO
   ```

   The `@provstr` parameter specifies `ActiveDirectoryMSI` authentication, which instructs the linked server to use managed identity.

1. Configure the linked server login mapping:

   ```sql
   EXEC master.dbo.sp_addlinkedsrvlogin
       @rmtsrvname = N'AzureSQLVMDestination',
       @useself = N'False',
       @locallogin = NULL,
       @rmtuser = NULL,
       @rmtpassword = NULL;
   GO
   ```

   This configuration sets up the linked server to use the managed identity without requiring explicit user credentials.

## Test the linked server connection

After configuration, verify that the linked server connection works correctly.

1. Test the connection using `sp_testlinkedserver`:

   ```sql
   EXECUTE master.dbo.sp_testlinkedserver AzureSQLVMDestination;
   GO
   ```

1. If the test succeeds, you can query the remote server. For example:

   ```sql
   SELECT * FROM [AzureSQLVMDestination].[master].[sys].[databases];
   GO
   ```

If the test fails, verify:

- Microsoft Entra authentication is properly configured on both servers
- The login on the destination server matches the source server name exactly
- Network connectivity exists between the servers
- Firewall rules permit SQL Server traffic
- The required extensions (*SqlIaasExtension* and *AADLogin*) are enabled for Azure VMs

## Related content

- [Linked servers (Database Engine)](../../relational-databases/linked-servers/linked-servers-database-engine.md)
- [Create linked servers (SQL Server Database Engine)](../../relational-databases/linked-servers/create-linked-servers-sql-server-database-engine.md)
- [sys.sp_addlinkedserver (Transact-SQL)](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md)
- [Enable Microsoft Entra authentication for SQL Server on Azure VMs](/azure/azure-sql/virtual-machines/windows/configure-azure-ad-authentication-for-sql-vm?view=azuresql&tabs=azure-portal&preserve-view=true)
- [Tutorial: Set up Microsoft Entra authentication for SQL Server enabled by Azure Arc](entra-authentication-setup-tutorial.md)
- [Prerequisites - SQL Server enabled by Azure Arc](prerequisites.md)
