---
title: "Connect to data sources and file shares with Windows Authentication | Microsoft Docs"
description: Learn how to configure the SSIS Catalog in Azure SQL Database and the Azure-SSIS Integration Runtime to run packages that connect to data sources and file shares with Windows Authentication.
ms.date: "10/11/2018"
ms.topic: conceptual
ms.prod: sql
ms.prod_service: "integration-services"
ms.custom: ""
ms.technology: integration-services
author: swinarko
ms.author: sawinark
ms.reviewer: douglasl
manager: craigg
---
# Connect to data sources and file shares with Windows Authentication from SSIS packages in Azure
You can use Windows Authentication to connect to data sources and file shares in the same virtual network as your Azure SSIS Integration Runtime (IR), both on premises/Azure virtual machines and in Azure Files. There are three methods of connecting to data sources and file shares with Windows Authentication from SSIS packages running on Azure-SSIS IR:

| Connection method | Effective scope | Setup step | Access method in packages | Number of credential sets and connected resources | Type of connected resources | 
|---|---|---|---|---|---|
| Persisting credentials via `cmdkey` command | Per Azure-SSIS IR | Execute `cmdkey` command in a custom setup script (`main.cmd`) when provisioning/reconfiguring your Azure-SSIS IR, for example, `cmdkey /add:fileshareserver /user:xxx /pass:yyy`.<br/><br/> For more info, see [Customize setup for Azure-SSIS IR](https://docs.microsoft.com/azure/data-factory/how-to-configure-azure-ssis-ir-custom-setup). | Access resources directly in packages via UNC path, for example,  `\\fileshareserver\folder` | Support multiple credential sets for different connected resources | - File shares on premises/Azure VMs<br/><br/> - Azure Files, see [Use an Azure file share](https://docs.microsoft.com/azure/storage/files/storage-how-to-use-files-windows) <br/><br/> - SQL Server with Windows Authentication<br/><br/> - Other resources with Windows Authentication |
| Setting up a catalog-level execution context | Per Azure-SSIS IR | Execute SSISDB `catalog.set_execution_credential` stored procedure to set up an "execution as" context.<br/><br/> For more info, see the rest of this article below. | Access resources directly in packages | Support only one credential set for all connected resources | - File shares on premises/Azure VMs<br/><br/> - Azure Files, see [Use an Azure file share](https://docs.microsoft.com/azure/storage/files/storage-how-to-use-files-windows) <br/><br/> - SQL Server with Windows Authentication<br/><br/> - Other resources with Windows Authentication | 
| Mounting drives at package execution time (non-persistence) | Per package | Execute `net use` command in Execute Process Task that is added at the beginning of control flow in your packages, for example, `net use D: \\fileshareserver\sharename` | Access file shares via mapped drives | Support multiple drives for different file shares | - File shares on premises/Azure VMs<br/><br/> - Azure Files, see [Use an Azure file share](https://docs.microsoft.com/azure/storage/files/storage-how-to-use-files-windows) |
|||||||

> [!WARNING]
> If you do not use any of the above methods to connect to data sources and file shares with Windows Authentication, packages that depend on Windows Authentication will not be able to connect to them and will fail at run time. 

The rest of this article describes how to configure the SSIS Catalog in Azure SQL Database to run packages that use Windows Authentication to connect to data sources and file shares. 

## You can only use one set of credentials
When you use Windows authentication in an SSIS package, you can only use one set of credentials in a package. The domain credentials that you provide when you follow the steps in this article apply to all package executions - interactive or scheduled - on your Azure-SSIS IR until you change or remove the credentials. If your package has to connect to multiple data sources and file shares with different sets of credentials, you may have to consider the above alternative methods.

## Provide domain credentials for Windows Authentication
To provide domain credentials that let packages use Windows Authentication to connect to on-premises data sources/file shares, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB). For more info, see [Connect to the SSIS Catalog  (SSISDB) in Azure](ssis-azure-connect-to-catalog-database.md).

2.  With SSISDB as the current database, open a query window.

3.  Run the following stored procedure and provide appropriate domain credentials:

    ```sql
    catalog.set_execution_credential @user='<your user name>', @domain='<your domain name>', @password='<your password>'
    ```

4.  Run your SSIS packages. The packages use the credentials that you provided to connect to on-premises data sources/file shares with Windows Authentication.

### View domain credentials
To view the active domain credentials, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB).

2.  With SSISDB as the current database, open a query window.

3.  Run the following stored procedure and check the output:

    ```sql
    SELECT * 
    FROM catalog.master_properties
    WHERE property_name = 'EXECUTION_DOMAIN' OR property_name = 'EXECUTION_USER'
    ```

### Clear domain credentials
To clear and remove the credentials that you provided as described in this article, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB).

2.  With SSISDB as the current database, open a query window.

3.  Run the following stored procedure:

    ```sql
    catalog.set_execution_credential @user='', @domain='', @password=''
    ```

## Connect to an on-premises SQL Server
To check whether you can connect to an on-premises SQL Server, do the following things:

1.  To run this test, find a non-domain-joined computer.

2.  On the non-domain-joined computer, run the following command to start SQL Server Management Studio (SSMS) with the domain credentials that you want to use:

    ```cmd
    runas.exe /netonly /user:<domain>\<username> SSMS.exe
    ```

3.  From SSMS, check whether you can connect to the on-premises SQL Server that you want to use.

### Prerequisites
To connect to an on-premises SQL Server from a package running on Azure, you have to enable the following prerequisites:

1.  In SQL Server Configuration Manager, enable the TCP/IP protocol.
2.  Allow access through the Windows firewall. For more info, see [Configure the Windows Firewall to Allow SQL Server Access](https://docs.microsoft.com/sql/sql-server/install/configure-the-windows-firewall-to-allow-sql-server-access).
3.  To connect with Windows Authentication, make sure that your Azure-SSIS IR belongs to a virtual network that also includes the on-premises SQL Server.  For more info, see [Join an Azure-SSIS integration runtime to a virtual network](https://docs.microsoft.com/azure/data-factory/join-azure-ssis-integration-runtime-virtual-network). Then use `catalog.set_execution_credential` to provide credentials as described in this article.

## Connect to an on-premises file share
To test whether you can connect to an on-premises file share, do the following things:

1.  To run this test, find a non-domain-joined computer.

2.  On the non-domain-joined computer, run the following command. This command opens a command prompt window with the domain credentials that you want to use, and then tests connectivity to the file share by getting a directory listing.

    ```cmd
    runas.exe /netonly /user:<domain>\<username> cmd.exe
    dir \\fileshare
    ```

3.  Check whether the directory listing is returned for the on-premises file share that you want to use.

## Connect to a file share on an Azure VM
To connect to a file share on an Azure virtual machine, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB).

2.  With SSISDB as the current database, open a query window.

3.  Run the `catalog.set_execution_credential` stored procedure as described in the following options:

    ```sql
    catalog.set_execution_credential @domain = N'.', @user = N'username of local account on Azure virtual machine', @password = N'password'
    ```

## Connect to a file share in Azure Files
For more info about Azure Files, see [Azure Files](https://azure.microsoft.com/services/storage/files/).

To connect to a file share in Azure Files, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB).

2.  With SSISDB as the current database, open a query window.

3.  Run the `catalog.set_execution_credential` stored procedure as described in the following options:

    ```sql
    catalog.set_execution_credential @domain = N'Azure', @user = N'<storage-account-name>', @password = N'<storage-account-key>'
    ```

## Next steps
- Deploy a package. For more info, see [Deploy an SSIS project with SQL Server Management Studio (SSMS)](../ssis-quickstart-deploy-ssms.md).
- Run a package. For more info, see [Run an SSIS package with SQL Server Management Studio (SSMS)](../ssis-quickstart-run-ssms.md).
- Schedule a package. For more info, see [Schedule SSIS packages in Azure](ssis-azure-schedule-packages.md).
