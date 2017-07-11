---
title: "Installing SQL Server R Services on an Azure Virtual Machine | Microsoft Docs"
ms.custom: ""
ms.date: "07/10/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: c3c223b8-75c4-412e-a319-d57ecf6533af
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Installing SQL Server R Services on an Azure Virtual Machine
 
If you deploy an Azure virtual machine that includes [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], you can now select machine learning as a feature to be added to the instance when the VM is created.

+ [Create a new VM with SQL Server 2016 and R Services](#new)
+ [Add R Services to an existing virtual machine with SQL Server 2016](#existing)

## <a name="new"></a>Create a new SQL Server 2016 Enterprise Virtual Machine with R Services Enabled

1. In the Azure portal, click VIRTUAL MACHINES and then click NEW.
2. Select SQL Server 2016 Enterprise Edition.
3. Configure the server name and account permissions, and select a pricing plan.
4. On Step 4 in the VM setup wizard, in **SQL Server Settings**, locate **R Services (Advanced Analytics)** and click **Enable**.
5. Review the summary presented for validation and click **OK**.
6. When the virtual machine is ready, connect to it, and open SQL Server Management Studio, which is pre-installed. R Services is ready to run.
7. To verify this, you can open a new query window and run a simple statement such as this one, which uses R to generate a sequence of numbers from 1 to 10.
   ```
    execute sp_execute_external_script
    @language = N'R'
    , @script = N' OutputDataSet <- as.data.frame(seq(1, 10, ));'
    , @input_data_1 = N'   ;'
    WITH RESULT SETS (([Sequence] int NOT NULL));
   ```
6. If you will be connecting to the instance from a remote data science client, complete [additional steps](#additional-steps) as necessary.


## Additional steps  

Some additional steps are required if you expect remote clients to access the server as a remote SQL Server compute context.

### <a name="firewall"></a>Unblock the firewall

By default, the firewall on the Azure virtual machine includes a rule that blocks network access for local R user accounts.

You must disable this rule to ensure that you can access the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance from a remote data science client.  Otherwise, your R code cannot execute in compute contexts that use the virtual machine's workspace, even if other R code uses the SQL Server compute context without problems.

To enable access to R Services from remote data science clients:

1. On the virtual machine, open Windows Firewall with Advanced Security.
2. Select **Outbound Rules**
3. Disable the following rule:
  
     `Block network access for R local user accounts in SQL Server instance MSSQLSERVER`
  
### Enable ODBC callbacks for remote clients

If you expect that R clients calling the server will need to issue ODBC queries as part of their R solutions, you must ensure that the Launchpad can make ODBC calls on behalf of the remote client. To do this, you must allow the SQL worker accounts that are used by Launchpad to log into the instance.
   For more information, see [Set Up SQL Server R Services](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md).

### <a name="network"></a>Add network protocols

+ Enable Named Pipes
  
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] uses the Named Pipes protocol for connections between the client and server computers, and for some internal connections. If Named Pipes is not enabled, you must install and enable it on both the Azure virtual machine, and on any data science clients that connect to the server.
  
+ Enable TCP/IP

  TCP/IP is required for loopback connections to SQL Server R Services. If you get the following error, enable TCP/IP on the virtual machine that supports the instance:

  "DBNETLIB; SQL Server does not exist or access denied"

## How to disable R Services on an instance

You can also enable or disable the feature on an existing virtual machine at any time.

1. Open the virtual machine blade
2. Click **Settings**, and select **SQL Server configuration**.

## <a name="existing"></a>Add SQL Server R Services to an existing SQL Server 2016 Enterprise virtual machine

If you created an Azure virtual machine that did not include R Services, you can add the feature by following these steps:

1. Re-run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup and add the feature on the **Server Configuration** page of the wizard.
2. Enable execution of external scripts and restart the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. For more information, see [Set Up SQL Server R Services](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md).
3. (Optional) Configure database access for R worker accounts, if needed for remote script execution.
   For more information, see [Set Up SQL Server R Services](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md).
3. (Optional) Modify a firewall rule on the Azure virtual machine, if you intend to allow R script execution from remote data science clients. For more information, see [Unblock firewall](#firewall).
4. Install or enable required network libraries. For more information, see [Add network protocols](#network).

## Related resources

[Set Up Sql Server R Services](../../advanced-analytics/r/set-up-sql-server-r-services-in-database.md)
