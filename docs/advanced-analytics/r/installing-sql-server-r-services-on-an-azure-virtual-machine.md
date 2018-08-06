---
title: Install SQL Server machine learning features on an Azure virtual machine | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Install SQL Server machine learning features on an Azure virtual machine
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]
 
We recommend using the [Data Science virtual machine](https://docs.microsoft.com/azure/machine-learning/data-science-virtual-machine/provision-vm), but if you want a VM that has just SQL Server 2017 Machine Learning Services or SQL Server 2016 R Services, this article guides you through the steps.

## Create a virtual machine on Azure

1. In the Azure portal in the left-side list, click **Virtual machines** and then click **Add**.
2. Search for SQL Server 2017 Enterprise Edition or SQL Server 2016 Enterprise Edition.
3. Configure the server name and account permissions, and select a pricing plan.
4. In **SQL Server Settings** (Step 4 in the VM setup wizard), locate **Machine Learning Services (Advanced Analytics)** (or **R Services** for SQL Server 2016) and click **Enable**.
5. Review the summary presented for validation and click **OK**.
6. When the virtual machine is ready, connect to it, and open SQL Server Management Studio, which is pre-installed. Machine learning is ready to run.
7. To verify this, you can open a new query window and run a simple statement such as this one, which uses R to generate a sequence of numbers from 1 to 10.

    ```
    EXEC sp_execute_external_script
    @language = N'R'
    , @script = N' OutputDataSet <- as.data.frame(seq(1, 10, ));'
    , @input_data_1 = N'   ;'
    WITH RESULT SETS (([Sequence] int NOT NULL));
    ```

6. If you plan to connect to the instance from a remote data science client, complete [additional steps](#additional-steps) as necessary.

### Disable machine learning features on a SQL Server VM

You can also enable or disable the feature on an existing SQL Server virtual machine at any time.

1. Open the virtual machine blade.
2. Click **Settings**, and select **SQL Server configuration**.
3. Make changes to features and apply.

### <a name="existing"></a>Add R Services to an existing SQL Server 2016 Enterprise VM

If you created an Azure virtual machine that included SQL Server without machine learning, you can add the feature by following these steps:

1. Re-run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup and add the feature on the **Server Configuration** page of the wizard.
2. Enable execution of external scripts and restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md).
3. (Optional) Configure database access for R worker accounts, if needed for remote script execution.
4. (Optional) Modify a firewall rule on the Azure virtual machine, if you intend to allow R script execution from remote data science clients. For more information, see [Unblock firewall](#firewall).
5. Install or enable required network libraries. For more information, see [Add network protocols](#network).

## Additional steps

Some additional steps are required if you expect remote clients to access the server as a remote SQL Server compute context.

### <a name="firewall"></a>Unblock the firewall

By default, the firewall on the Azure virtual machine includes a rule that blocks network access for local user accounts.

You must disable this rule to ensure that you can access the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance from a remote data science client.  Otherwise, your machine learning code cannot execute in compute contexts that use the virtual machine's workspace.

To enable access from remote data science clients:

1. On the virtual machine, open Windows Firewall with Advanced Security.
2. Select **Outbound Rules**
3. Disable the following rule:
  
     `Block network access for R local user accounts in SQL Server instance MSSQLSERVER`
  
### Enable ODBC callbacks for remote clients

If you expect that clients calling the server will need to issue ODBC queries as part of their machine learning solutions, you must ensure that the Launchpad can make ODBC calls on behalf of the remote client. To do this, you must allow the SQL worker accounts that are used by Launchpad to log into the instance.
   For more information, see [Install SQL Server 2016 R Services](../install/sql-r-services-windows-install.md).

### <a name="network"></a>Add network protocols

+ Enable Named Pipes
  
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] uses the Named Pipes protocol for connections between the client and server computers, and for some internal connections. If Named Pipes is not enabled, you must install and enable it on both the Azure virtual machine, and on any data science clients that connect to the server.
  
+ Enable TCP/IP

  TCP/IP is required for loopback connections. If you get the following error, enable TCP/IP on the virtual machine that supports the instance:

  "DBNETLIB; SQL Server does not exist or access denied"
