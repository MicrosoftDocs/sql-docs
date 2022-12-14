---
title: Configure parallel or concurrent R and Python script execution
description: Configure parallel or concurrent R and Python script execution in a user account pool to scale SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/25/2019
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: "=sql-server-2016||=sql-server-2017"
---
# Scale concurrent execution of external scripts in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

Learn about worker accounts for SQL Server Machine Learning Services and how to change the default configuration to scale the number of concurrent execution of external scripts.

As part of the installation process for Machine Learning Services, a new Windows *user account pool* is created to support execution of tasks by the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. The purpose of these worker accounts is to isolate concurrent execution of external scripts by different SQL Server users.

> [!Note]
> In SQL Server 2019, **SQLRUserGroup** only has one member which is now the single SQL Server Launchpad service account instead of multiple worker accounts. This article describes the worker accounts for SQL Server 2016 and 2017.

## Worker account group

A Windows account group is created by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup for each instance on which machine learning is installed and enabled.

- In a default instance, the group name is **SQLRUserGroup**. The name is the same whether you use Python or R or both.
- In a named instance, the default group name is suffixed with the instance name: for example, **SQLRUserGroupMyInstanceName**.

By default, the user account pool contains 20 user accounts. In most cases, 20 is more than adequate to support machine learning tasks, but you can change the number of accounts. The maximum number of accounts is 100.

- In a default instance, the individual accounts are named **MSSQLSERVER01** through **MSSQLSERVER20**.
- For a named instance, the individual accounts are named after the instance name: for example, **MyInstanceName01** through **MyInstanceName20**.

If more than one instance uses machine learning, the computer will have multiple user groups. A group cannot be shared across instances.

<a name = "HowToChangeGroup"> </a>

## Number of worker accounts

To modify the number of users in the account pool, you must edit the properties of the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service as described below.

Passwords associated with each user account are generated at random, but you can change them later, after the accounts have been created.

1. Open SQL Server Configuration Manager and select **SQL Server Services**.
2. Double-click the SQL Server Launchpad service and stop the service if it is running.
3.  On the **Service** tab, make sure that the Start Mode is set to Automatic. External scripts cannot start when the Launchpad is not running.
4.  Click the **Advanced** tab and edit the value of **External Users Count** if necessary. This setting controls how many different SQL users can run external script sessions concurrently. The default is 20 accounts. The maximum number of users is 100.
5. Optionally, you can set the option **Reset External Users Password** to _Yes_ if your organization has a policy that requires changing passwords on a regular basis. Doing this will regenerate the encrypted passwords that Launchpad maintains for the user accounts. For more information, see [Enforcing Password Policy](../security/sql-server-launchpad-service-account.md#bkmk_EnforcePolicy).
6.  Restart the Launchpad service.

## Managing workloads

The number of accounts in this pool determines how many external script sessions can be active simultaneously.  By default, 20 accounts are created, meaning that 20 different users can have active Python or R sessions at one time. You can increase the number of worker accounts, if you expect to run more than 20 concurrent scripts.

When the same user executes multiple external scripts concurrently, all the sessions run by that user use the same worker account. For example, a single user might have 100 different Python or R scripts running concurrently, as long as resources permit, but all scripts would run using a single worker account.

The number of worker accounts that you can support, and the number of concurrent sessions that any single user can run, is limited only by server resources. Typically, memory is the first bottleneck that you will encounter when using the Python or R runtime.

The resources that can be used by Python or R scripts are governed by SQL Server. We recommend that you monitor resource usage using SQL Server DMVs, or look at performance counters on the associated Windows job object, and adjust server memory use accordingly. If you have SQL Server Enterprise Edition, you can allocate resources used for running external scripts by configuring an [external resource pool](create-external-resource-pool.md).

## Next steps

- [Monitor Python and R script execution using custom reports in SQL Server Management Studio](../../machine-learning/administration/monitor-sql-server-machine-learning-services-using-custom-reports-management-studio.md)
- [Monitor SQL Server Machine Learning Services using dynamic management views (DMVs)](../../machine-learning/administration/monitor-sql-server-machine-learning-services-using-dynamic-management-views.md)