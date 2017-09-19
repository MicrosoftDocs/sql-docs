---
title: "Connect to the SSISDB Catalog database in Azure | Microsoft Docs"
ms.date: "09/25/2017"
ms.topic: "article"
ms.prod: "sql-server-2017"
ms.technology: 
  - "integration-services"
author: "douglaslMS"
ms.author: "douglasl"
manager: "craigg"
---
# Get the server connection info for your SSISDB Catalog database in Azure

Get the connection information you need to connect to the SSISDB Catalog database hosted in Azure SQL database. You  need the following items to connect:
- fully qualified server name
- database name
- login information 

## Get the info from the Azure portal
1. Log in to the [Azure portal](https://portal.azure.com/).
2. Select **SQL Databases** from the left-hand menu, and then select the **SSISDB** database on the **SQL databases** page. 
3. On the **Overview** page for the database, review the fully-qualified server name as shown in the image below. Hover over the server name to bring up the **Click to copy** option.

   ![connection information]() 

4. If you have forgotten the login information for the SQL Database server, navigate to the SQL Database server page to view the server admin name and, if necessary, to reset the password.

## Next steps
- Deploy a package. For more info, see the following articles:
    - [Deploy from SSMS](ssis-everest-quickstart-deploy-ssms.md)
    - [Deploy with T-SQL from SSMS](ssis-everest-quickstart-deploy-tsql-ssms.md)
- Run a package. For more info, see the following articles:
    - [Run from SSMS](ssis-everest-quickstart-run-ssms.md)
    - [Run with T-SQL from SSMS](ssis-everest-quickstart-run-tsql-ssms.md)
- Schedule a package. For more info, see [Schedule page](ssis-everest-howto-schedule-package.md)
