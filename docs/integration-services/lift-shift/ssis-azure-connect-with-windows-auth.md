---
title: "Connect to on-premises data sources with Windows Authentication | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Connect to on-premises data sources with Windows Authentication
This article describes how to configure the SSIS Catalog on Azure SQL Database to run packages that use Windows Authentication to connect to on-premises data sources.

The domain credentials that you provide when you follow the steps in this article apply to all package executions on the SQL Database instance until you change or remove the credentials.

## Prerequisite
Before you set up domain credentials for Windows Authentication, check whether a non-domain-joined computer can connect to your on-premises data sources in `runas` mode. For example, to check whether you can connect to an on-premises SQL Server, do the following things:

1.  Find a non-domain-joined computer to run this test.

2.  On the non-domain-joined computer, run the following command to start SQL Server Management Studio (SSMS) with the domain credentials that you want to use:

   ```cmd
   runas.exe /netonly /user:<domain>\<username> SSMS.exe
   ```

3.  From SSMS, check whether you can connect to the on-premises SQL Server that you want to use.

## Provide domain credentials
To provide domain credentials that let packages use Windows Authentication to connect to on-premises data sources, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB). For more info, see [Connect to the SSISDB Catalog database on Azure](ssis-azure-connect-to-catalog-database.md).

2.  With SSISDB as the current database, open a query window.

3.  Run the following stored procedure and provide appropriate domain credentials:

    ```sql
    catalog.set_execution_credential @user='<your user name>', @domain='<your domain name>', @password='<your password>'
    ```
4.  Run your SSIS packages. The packages use the credentials that you provided to connect to on-premises data sources with Windows Authentication.

## View domain credentials
To view the active domain credentials, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB).

2.  With SSISDB as the current database, open a query window.

3.  Run the following stored procedure and check the output:

    ```sql
    SELECT * 
    FROM catalog.master_properties
    WHERE property_name = 'EXECUTION_DOMAIN' OR property_name = 'EXECUTION_USER'
    ```

## Clear domain credentials
To clear and remove the credentials that you provided as described in this article, do the following things:

1.  With SQL Server Management Studio (SSMS) or another tool, connect to the SQL Database that hosts the SSIS Catalog database (SSISDB).

2.  With SSISDB as the current database, open a query window.

3.  Run the following stored procedure:

    ```sql
    catalog.set_execution_credential @user='', @domain='', @password=''
    ```

## Next steps
- Deploy a package. For more info, see [Deploy an SSIS project with SQL Server Management Studio (SSMS)](ssis-everest-quickstart-deploy-ssms.md).
- Run a package. For more info, see [Run an SSIS package with SQL Server Management Studio (SSMS)](ssis-everest-quickstart-run-ssms.md).
- Schedule a package. For more info, see [Schedule SSIS package execution on Azure](ssis-azure-schedule-packages.md)
