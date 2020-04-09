---
title: Troubleshoot Active Directory mode deployment
titleSuffix: SQL Server Big Data Cluster
description: Troubleshoot deployment of a SQL Server Big Data Cluster in an Active Directory domain.
author: rl-msft
ms.author: rafidl
ms.reviewer: mikeray
ms.date: 03/12/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Troubleshoot SQL Server Big Data Cluster Active Directory mode deployment

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article explains how to troubleshoot deployment of a SQL Server Big Data Cluster in Active Directory mode.

## Check deployment progress

Deployment can take several minutes. If the cluster is not ready after 15 minutes, check controller logs for more details.

While the cluster is deploying, check the pods.

```console
kubectl get pods -n mssql-cluster
```

Verify that the list of pods returned includes:

- `compute-`$
- `data-`
- `storage-`

If the compute, data, and storage pods are not created, check the logs to identify why.

## Check logs

To identify why deployment quit without creating compute, data, or storage pods, check the following logs: 

- Check `controller.log` (`<folderOfDebugCopyLog>\debuglogs-mssql-cluster-20200219-093941\mssql-cluster\control-<suffix>\controller\controller\<date>\controller.log`). Look for the following entry:

  `WARN | StatefulSet master is not ready with 0 ready pods and 3 unready pods `

- Check `master-0` `provisioner.log` (`<folderOfDebugCopyLog>\debuglogs-mssql-cluster-20200219-093941\mssql-cluster\master-0\mssql-server\provisioner\provisioner.log`)

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

## Check security-support container 

Review the security-support container logs.

The following command collects the security-support logs in a cluster at namespace `mssql-cluster`.

```console
azdata bdc debug copy-logs -n mssql-cluster -c security-support
```

Extract the logs and locate `\mssql-cluster\control-<identifier>\controller\control-rts5t-controller-stdout.log`.

Look for the following entries in the log:

```
ERROR    | Failed to create AD user account 'cntrl-controller'. Error code: 53. Message: Failed to create user object: Failed to add object 'CN=cntrl-controller,OU=bdc, DC=CONTOSO, DC=com' to '  <domain>.<top-level-domain>  ': Server is unwilling to perform. 
ERROR | Failed to create AD user account 'ldap-user'. Error code: 53. Message: Failed to create user object: Failed to add object 'CN=ldap-user,OU=bdc, DC=CONTOSO, DC=com' to '  <domain>.<top-level-domain>  ': Server is unwilling to perform. 
ERROR | Failed to create AD user account 'nginx-mgmtproxy'. Error code: 53. Message: Failed to create user object: Failed to add object 'CN=nginx-mgmtproxy,OU=bdc, DC=CONTOSO, DC=com' to '  <domain>.<top-level-domain>  ': Server is unwilling to perform.
```

These entries can happen when the domain controller DNS server is missing reverse DNS entry (PTR record).

## Verify reverse lookup (PTR record)
    
Run the following PowerShell script to confirm if you have reverse DNS entry (PTR record) configured.

```powershell
#Domain Controller FQDN 'DCserver01.contoso.local'
$Domain_controller_FQDN = 'DCserver01.contoso.local'

#Performing Domain Controller DNS record, reverse PTR Checks...
$DcControllerDnsPtr_Result = New-Object System.Collections.ArrayList
try {
    $Domain_controller_DNS_Record = Resolve-DnsName $Domain_controller_FQDN -Type A -Server $Domain_DNS_IP_address -ErrorAction Stop
    foreach ($ip in $Domain_controller_DNS_Record.IPAddress) {
        #resolving hostname by IP address to make sure we have reverse PTR record 
        if ((Resolve-DnsName $ip).NameHost -eq $Domain_controller_FQDN) {
            [void]$DcControllerDnsPtr_Result.add("OK - $Domain_controller_FQDN has an A record with an IP $ip, Reverse PTR record is in place") 
        }
        else {
            [void]$DcControllerDnsPtr_Result.add("Missing - $Domain_controller_FQDN has an A record with an IP $ip, But no reverse PTR record was found for the host")
        }
    }
}
catch {
    [void]$DcControllerDnsPtr_Result.add("Error - " + $_.exception.message)
}

#show the results 
$DcControllerDnsPtr_Result
```

[Verify reverse DNS entry (PTR record) for domain controller](deploy-active-directory.md#verify-reverse-dns-entry-for-domain-controller).