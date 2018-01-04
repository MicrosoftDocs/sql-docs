---
title: "Installing SQL Server machine learning features on an Azure virtual machine | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c3c223b8-75c4-412e-a319-d57ecf6533af
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
ms.workload: "Inactive"
---
# Installing SQL Server machine learning features on an Azure virtual machine
 
If you deploy an Azure virtual machine that includes [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can now select machine learning as a feature to be added to the instance when the VM is created.

+ [Create a new VM that includes SQL Server 2016 and R Services](#new)
+ [Add machine learning features to an existing virtual machine with SQL Server 2016](#existing)

> [!NOTE]
> Virtual machines are now available for SQL Server 2017! See [this announcement](https://azure.microsoft.com/blog/announcing-new-azure-vm-images-sql-server-2017-on-linux-and-windows/) for details.
> 
> R is also available as a preview feature in Azure SQL Database. For more information, see [Using R in Azure SQL Database](../r/using-r-in-azure-sql-database.md).

## Create a new SQL Server 2017 virtual machine

To use R or Python in SQL Server 2017, be sure to get a Windows-based virtual machine. [!INCLUDE[sscurrentlong-md](../../includes/sscurrentlong-md.md)] on Linux supports fast [native scoring](../sql-native-scoring.md) using the T-SQL PREDICT function, but other machine learning features are not available yet in this edition.

For a list of SQL Server VM offerings, see this article: [Overview of SQL Server on Azure Virtual Machines (Windows)](https://docs.microsoft.com/azure/virtual-machines/windows/sql/virtual-machines-windows-sql-server-iaas-overview).

### <a name="new"></a>Create a new SQL Server Enterprise VM with machine learning

1. In the Azure portal, click VIRTUAL MACHINES and then click NEW.
2. Select SQL Server 2017 Enterprise Edition.
3. Configure the server name and account permissions, and select a pricing plan.
4. In **SQL Server Settings** (Step 4 in the VM setup wizard), locate **Machine Learning Services (Advanced Analytics)** and click **Enable**.
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
2. Enable execution of external scripts and restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [Set up SQL Server R Services](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md).
3. (Optional) Configure database access for R worker accounts, if needed for remote script execution.
   For more information, see [Set Up SQL Server R Services](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md).
3. (Optional) Modify a firewall rule on the Azure virtual machine, if you intend to allow R script execution from remote data science clients. For more information, see [Unblock firewall](#firewall).
4. Install or enable required network libraries. For more information, see [Add network protocols](#network).

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
   For more information, see [Set Up SQL Server R Services](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md).

### <a name="network"></a>Add network protocols

+ Enable Named Pipes
  
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] uses the Named Pipes protocol for connections between the client and server computers, and for some internal connections. If Named Pipes is not enabled, you must install and enable it on both the Azure virtual machine, and on any data science clients that connect to the server.
  
+ Enable TCP/IP

  TCP/IP is required for loopback connections. If you get the following error, enable TCP/IP on the virtual machine that supports the instance:

  "DBNETLIB; SQL Server does not exist or access denied"

## Related resources

[Using R in Azure SQL Database](../r/using-r-in-azure-sql-database.md)