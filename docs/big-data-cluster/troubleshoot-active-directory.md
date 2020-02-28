---
title: Troubleshoot Active Directory mode deployment
titleSuffix: SQL Server Big Data Cluster
description: Troubleshoot deployment of a SQL Server Big Data Cluster in an Active Directory domain.
author: mihaelablendea
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 02/13/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Troubleshoot SQL Server Big Data Cluster Active Directory mode deployment

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article explains how to troubleshoot deployment of a SQL Server Big Data Cluster in Active Directory mode.

## Places to check

Deployment can take several minutes. If the cluster is not ready after 15 minutes, check controller logs for more details.

While the cluster is deploying, check the pods.

```console
kubectl get pods -n mssql-cluster
```

Verify that the list of pods returned include:

- `compute-`$
- `data-`
- `storage-`

Check `controller.log` (`<folderOfDebugCopyLog>\debuglogs-mssql-cluster-20200219-093941\mssql-cluster\control-<suffix>\controller\controller\<date>\controller.log`). Look for the following entry:

`WARN | StatefulSet master is not ready with 0 ready pods and 3 unready pods `

Check `master-0` `provisioner.log` (`<folderOfDebugCopyLog>\debuglogs-mssql-cluster-20200219-093941\mssql-cluster\master-0\mssql-server\provisioner\provisioner.log`)

```
ERROR | Failed to create sql login for domain user [contoso.com\<domain-group>]
	Traceback (most recent call last):
	  File "/opt/provisioner/bin/scripts/provisioningpool.py", line 214, in executeNonQueries
	    connection.execute_non_query(command)
	  File "src/_mssql.pyx", line 1033, in _mssql.MSSQLConnection.execute_non_query
	  File "src/_mssql.pyx", line 1061, in _mssql.MSSQLConnection.execute_non_query
	  File "src/_mssql.pyx", line 1634, in _mssql.check_and_raise
	  File "src/_mssql.pyx", line 1683, in _mssql.maybe_raise_MSSQLDatabaseException
	_mssql.MSSQLDatabaseException: (15401, b"Windows NT user or group 'contoso.com\\<domain-group>' not found. Check the name again.DB-Lib error message 20018, severity 16:\nGeneral SQL Server error: Check messages from the SQL Server\n")
WARNING | [3/3] Provisioning exception occurred during provisioning step: ProvisioningMasterPool.
WARNING | Failed to create sql login for domain user [contoso.com\<domain-group>]
WARNING | Retrying.
```

- Check the scope of the domain group (<`domain-group`>). Use [get-adgroup](/powershell/module/addsadministration/get-adgroup/).

If the `<domain-group>` group scope is domain local (`DomainLocal`) deployment fails. 

[Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](deploy-active-directory.md) explains AD group scope requirements.

## Check the scope of domain groups.

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
