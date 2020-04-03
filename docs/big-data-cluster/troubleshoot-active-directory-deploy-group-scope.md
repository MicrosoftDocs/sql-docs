---
title: Troubleshoot deployment - active directory group - DomainLocal scope
titleSuffix: SQL Server Big Data Cluster
description: Troubleshoot deployment of a SQL Server Big Data cluster with active directory group configured as DomainLocal scope.
author: rl-msft
ms.author: rafidl
ms.reviewer: mikeray
ms.date: 04/02/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Troubleshoot SQL Server Big Data Cluster Active Directory mode deployment

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

## Symptom

You started deploying Big Data Cluster in Active Director mode. The deployment is stuck and not moving forward.

The following example text may appear on the console:

```
The privacy statement can be viewed at:
https://go.microsoft.com/fwlink/?LinkId=853010

The license terms for SQL Server Big Data Cluster can be viewed at:
Enterprise: https://go.microsoft.com/fwlink/?linkid=2104292
Standard: https://go.microsoft.com/fwlink/?linkid=2104294
Developer: https://go.microsoft.com/fwlink/?linkid=2104079


Cluster deployment documentation can be viewed at:
https://aka.ms/bdc-deploy



NOTE: Cluster creation can take a significant amount of time depending on
configuration, network speed, and the number of nodes in the cluster.

Starting cluster deployment.
Cluster controller endpoint is available at bdc-control.contoso.com:30080, 193.168.5.14:30080.
Cluster control plane is ready.
Data pool is ready.
Storage pool is ready.
Compute pool is ready.
Cluster is not ready after 15 minutes. Check controller logs for more details.
```

## Check deployment progress

To check the deployment progress, see what pods have been created.

```bash
kubectl get pods -n mssql-cluster
```

```bash
NAME              READY   STATUS    RESTARTS   AGE
appproxy-6q4rm    2/2     Running   0          32m
compute-0-0       3/3     Running   0          32m
control-n8jqh     3/3     Running   0          35m
controldb-0       2/2     Running   0          35m
controlwd-fgpj8   1/1     Running   0          34m
data-0-0          3/3     Running   0          32m
data-0-1          3/3     Running   0          32m
dns-fjp7n         2/2     Running   0          34m
gateway-0         2/2     Running   0          32m
logsdb-0          1/1     Running   0          34m
logsui-d26c5      1/1     Running   0          34m
master-0          3/4     Running   0          32m
master-1          3/4     Running   0          32m
master-2          3/4     Running   0          32m
metricsdb-0       1/1     Running   0          34m
metricsdc-c2kbh   1/1     Running   0          34m
metricsdc-lmqzx   1/1     Running   0          34m
metricsdc-r6499   1/1     Running   0          34m
metricsdc-tj99w   1/1     Running   0          34m
metricsui-dg8rz   1/1     Running   0          34m
mgmtproxy-dvzpc   2/2     Running   0          34m
nmnode-0-0        2/2     Running   0          32m
nmnode-0-1        2/2     Running   0          32m
operator-27gt9    1/1     Running   0          32m
sparkhead-0       4/4     Running   0          31m
sparkhead-1       4/4     Running   0          31m
storage-0-0       4/4     Running   0          31m
storage-0-1       4/4     Running   0          31m
storage-0-2       4/4     Running   0          31m
zookeeper-0       2/2     Running   0          32m
zookeeper-1       2/2     Running   0          32m
zookeeper-2       2/2     Running   0          32m
```

Only pods that belong to the controller have been deployed. No compute, data, or storage pool pods are created.

Verify that the list of pods returned includes:

- `compute-`$
- `data-`
- `storage-`

If the compute, data, and storage pods are not created, check the logs to identify why.

## Check logs

To identify why deployment quit without creating compute, data, or storage pods, check the following logs: 

- Check `controller.log` (`<folderOfDebugCopyLog>\debuglogs-mssql-cluster-YYYYMMDD-HHMMSS\mssql-cluster\control-<suffix>\controller\controller\<date>\controller.log`). Look for the following entry:

  `WARN | StatefulSet master is not ready with 0 ready pods and 3 unready pods `

- Check `master-0` `provisioner.log` (`<folderOfDebugCopyLog>\debuglogs-mssql-cluster-YYYYMMDD-HHMMSS\mssql-cluster\master-0\mssql-server\provisioner\provisioner.log`)

  ```
  ERROR | Failed to create sql login for domain user [<domain>.<top-level-domain>\<domain-group>]
    Traceback (most recent call last):
      File "/opt/provisioner/bin/scripts/provisioningpool.py", line 214, in executeNonQueries
        connection.execute_non_query(command)
      File "src/_mssql.pyx", line 1033, in _mssql.MSSQLConnection.execute_non_query
      File "src/_mssql.pyx", line 1061, in _mssql.MSSQLConnection.execute_non_query
      File "src/_mssql.pyx", line 1634, in _mssql.check_and_raise
      File "src/_mssql.pyx", line 1683, in _mssql.maybe_raise_MSSQLDatabaseException
    _mssql.MSSQLDatabaseException: (15401, b"Windows NT user or group '<domain>.<top-level-domain>\\<domain-group>' not found. Check the name again.DB-Lib error message 20018, severity 16:\nGeneral SQL Server error: Check messages from the SQL Server\n")
  WARNING | [3/3] Provisioning exception occurred during provisioning step: ProvisioningMasterPool.
  WARNING | Failed to create sql login for domain user [<domain>.<top-level-domain>\<domain-group>]
  WARNING | Retrying.
  ```

  In the example above, the deployment fails to create a login for the domain user because the domain group is scoped as domain local. Use domain global or domain universal scoped groups. [Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](deploy-active-directory.md) explains AD group scope requirements.

## Check the scope of domain groups.

Check the scope of the domain group (<`domain-group`>). Use [get-adgroup](/powershell/module/addsadministration/get-adgroup/).

If the `<domain-group>` group scope is domain local (`DomainLocal`) deployment fails. 

The following PowerShell script checks the scope of two AD groups named `bdcadmins` and `bdcusers`. Replace the names with the names for your groups. 

```powershell
#Administrators and users AD groups
$Cluster_admins_group='bdcadmins'
$Cluster_users_group='bdcusers'

#Performing AD Group Checks...

#AD admin group Check
$ClusterAdminGroupScope_Result = New-Object System.Collections.ArrayList
try {
    $GroupScope = Get-ADgroup -Identity $Cluster_admins_group | Select-Object -ExpandProperty GroupScope
    
    if ($GroupScope -eq 'DomainLocal') {
        [void]$ClusterAdminGroupScope_Result.Add("Misconfiguration - $Cluster_admins_group Group scope is $GroupScope, this scope is not supported, Please change group scope to either Global or Univesal") 
    }
    else {
        [void]$ClusterAdminGroupScope_Result.Add("OK - $Cluster_admins_group Group scope is $GroupScope")
    }
}
catch {
    [void]$ClusterAdminGroupScope_Result.Add("Error - " + $_.exception.message)
}
#Ad users group check
$ClusterUsersGroupScope_Result = New-Object System.Collections.ArrayList
$GroupScope = ''
try {
    $GroupScope = Get-ADgroup -Identity $Cluster_users_group | Select-Object -ExpandProperty GroupScope
    
    if ($GroupScope -eq 'DomainLocal') {
        [void]$ClusterUsersGroupScope_Result.Add("Misconfiguration - $Cluster_users_group Group scope is $GroupScope, this scope is not supported, Please change group scope to either Global or Univesal")
    } 
    else 
    { [void]$ClusterUsersGroupScope_Result.Add("OK - $Cluster_users_group Group scope is $GroupScope") }
}
catch {
    [void]$ClusterUsersGroupScope_Result.Add("Error - " + $_.exception.message)
}

#Display the results
$ClusterUsersGroupScope_Result
```