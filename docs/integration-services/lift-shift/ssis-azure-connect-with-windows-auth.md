---
title: "Connect to on-premises data sources and Azure file shares with Windows Authentication | Microsoft Docs"
ms.date: "11/27/2017"
ms.topic: "article"
ms.prod: "sql-non-specified"
ms.prod_service: "integration-services"
ms.service: ""
ms.component: "lift-shift"
ms.suite: "sql"
ms.custom: ""
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
ms.workload: "Inactive"
---
# Connect to on-premises data sources and Azure file shares with Windows Authentication
This article describes how to configure the SSIS Catalog on Azure SQL Database to run packages that use Windows Authentication to connect to on-premises data sources and Azure file shares. You can use Windows authentication to connect to data sources in the same virtual network as the Azure SSIS Integration Runtime, both on premises and on Azure virtual machines and in Azure Files.

The domain credentials that you provide when you follow the steps in this article apply to all package executions on the SQL Database instance until you change or remove the credentials.

## Provide domain credentials for Windows Authentication
To provide domain credentials that let packages use Windows Authentication to connect to on-premises data sources, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB). For more info, see [Connect to the SSISDB Catalog database on Azure](ssis-azure-connect-to-catalog-database.md).

2.  With SSISDB as the current database, open a query window.

3.  Run the following stored procedure and provide appropriate domain credentials:

    ```sql
    catalog.set_execution_credential @user='<your user name>', @domain='<your domain name>', @password='<your password>'
    ```

4.  Run your SSIS packages. The packages use the credentials that you provided to connect to on-premises data sources with Windows Authentication.

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
3.  To connect with Windows Authentication, make sure that the Azure-SSIS Integration Runtime belongs to a virtual network (VNet) that also includes the on-premises SQL Server.  For more info, see [Join an Azure-SSIS integration runtime to a virtual network](https://docs.microsoft.com/azure/data-factory/join-azure-ssis-integration-runtime-virtual-network). Then use `catalog.set_execution_credential` to provide credentials as described in this article.

## Connect to an on-premises file share
To check whether you can connect to an on-premises file share, do the following things:

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

To connect to a file share on an Azure file share, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB).

2.  With SSISDB as the current database, open a query window.

3.  Run the `catalog.set_execution_credential` stored procedure as described in the following options:

    ```sql
    catalog.set_execution_credential @domain = N'Azure', @user = N'<storage-account-name>', @password = N'<storage-account-key>'
    ```

## Next steps
- Deploy a package. For more info, see [Deploy an SSIS project with SQL Server Management Studio (SSMS)](../ssis-quickstart-deploy-ssms.md).
- Run a package. For more info, see [Run an SSIS package with SQL Server Management Studio (SSMS)](../ssis-quickstart-run-ssms.md).
- Schedule a package. For more info, see [Schedule SSIS package execution on Azure](ssis-azure-schedule-packages.md).
