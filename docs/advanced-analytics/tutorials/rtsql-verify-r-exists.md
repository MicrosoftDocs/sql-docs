---
title: Quickstart for verifying R exists in SQL Server
description: Quickstart for verifying that R and Machine Learning Services exist in SQL Server. 
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/14/2018  
ms.topic: quickstart
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Quickstart: Verify R exists in SQL Server 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server includes R language support for data science analytics on resident SQL Server data. Your R script can consist of open-source R functions, third-party R libraries, or built-in Microsoft R libraries such as [RevoScaleR](../r/revoscaler-overview.md) for predictive analytics at scale.

Script execution is through stored procedures, using either of the following approaches:

+ Built-in [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) stored procedure, passing R script in as an input parameter.
+ Wrap R script in a [custom stored procedure](sqldev-in-database-r-for-sql-developers.md) that you create.

In this quickstart, you will verify that [SQL Server 2017 Machine Learning Services](../what-is-sql-server-machine-learning.md) or [SQL Server 2016 R Services](../r/sql-server-r-services.md) is installed and configured.

## Prerequisites

This exercise requires access to an instance of SQL Server with one of the following already installed:

+ [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md), with the R language installed
+ [SQL Server 2016 R Services](../install/sql-r-services-windows-install.md)

  Your SQL Server instance can be in an Azure virtual machine or on-premises. Just be aware that the external scripting feature is disabled by default, so you might need to [enable external scripting](../install/sql-machine-learning-services-windows-install.md#bkmk_enableFeature) and verify that **SQL Server Launchpad service** is running before you start.

+ A tool for running SQL queries. You can connect to the SQL Database and run the R scripts any database management or query tool, as long as it can connect to a SQL Database, and run a T-SQL query or stored procedure. This quickstart uses [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms).


## Verify R exists

You can confirm that Machine Learning Services (with R) is enabled for your SQL Server instance. Follow the steps below.

1. Open SQL Server Management Studio and connect to your SQL Server instance.

1. Run the code below. 

    ```sql
    EXECUTE sp_execute_external_script
    @language =N'R',
    @script=N'print(31 + 11)';
    GO
    ```
    If all is well, you should see a result message like this one.

    ```text
    STDOUT message(s) from external script: 
    42
    ```

If you get any errors from this query, rule out any installation issues. Post-install configuration is required to enable use of external code libraries. See [Install SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) or [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md).Likewise, make sure that the Launchpad service is running. 

Depending on your environment, you might need to enable the R worker accounts to connect to SQL Server, install additional network libraries, enable remote code execution, or restart the instance after everything is configured. For more information, see
[R Services Installation and Upgrade FAQ](../r/upgrade-and-installation-faq-sql-server-r-services.md)

## Next steps

Now that you have confirmed your instance is ready to work with R, take a closer look at a basic R interaction.

> [!div class="nextstepaction"]
> [Quickstart: "Hello world" R script in SQL Server ](rtsql-using-r-code-in-transact-sql-quickstart.md)
