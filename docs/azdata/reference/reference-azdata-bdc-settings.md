---
title: azdata bdc settings reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc settings commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc settings

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc settings set](#azdata-bdc-settings-set) | Set cluster-scope settings.
[azdata bdc settings apply](#azdata-bdc-settings-apply) | Apply the pending settings changes to the BDC.
[azdata bdc settings cancel-apply](#azdata-bdc-settings-cancel-apply) | Cancel the BDC settings apply.
[azdata bdc settings show](#azdata-bdc-settings-show) | Show cluster-scope settings or all cluster settings using --recursive.
[azdata bdc settings revert](#azdata-bdc-settings-revert) | Reverts pending settings changes in the BDC at all scopes.
## azdata bdc settings set
Set a cluster-scope setting. Specify the full name of the setting and the value. Run apply to apply the setting and update the BDC settings.
```bash
azdata bdc settings set --settings -s 
                        
```
### Examples
Set the cluster default for "bdc.telemetry.customerFeedback".
```bash
azdata bdc settings set --settings bdc.telemetry.customerFeedback=false
```
### Required Parameters
#### `--settings -s`
Sets the configured value for the provided settings. Multiple settings can be set using a comma separated list.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc settings apply
Apply the pending settings changes to the BDC.
```bash
azdata bdc settings apply [--force -f] 
                          
```
### Examples
Apply the pending settings changes to the BDC.
```bash
azdata bdc settings apply
```
Force apply, the user will not be prompted for any confirmation and all issues will be printed as part of stderr.
```bash
azdata bdc settings apply --force
```
### Optional Parameters
#### `--force -f`
Force apply, the user will not be prompted for any confirmation and all issues will be printed as part of stderr.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc settings cancel-apply
In the event of a failure during settings application, return the Big Data Cluster to its last running state. This command will be a no-op if applied to a running cluster. Previously pending settings changes will still be pending after the cancellation.
```bash
azdata bdc settings cancel-apply [--force -f] 
                                 
```
### Examples
Cancel the BDC settings apply.
```bash
azdata bdc settings cancel-apply
```
Force cancel-apply, the user will not be prompted for any confirmation and all issues will be printed as part of stderr.
```bash
azdata bdc settings cancel-apply --force
```
### Optional Parameters
#### `--force -f`
Force cancel-apply, the user will not be prompted for any confirmation and all issues will be printed as part of stderr.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc settings show
Shows the BDC's cluster-level settings. By default, this command shows user-configured cluster-scope settings. Other filters are available to show all settings (system-managed & user-configurable & inherited), all configurable settings, or pending settings. To see a specific cluster-scope setting, you can provide the setting name. If you want to see settings at all scopes (cluster, service, and resource), you can specify "recursive".
```bash
azdata bdc settings show [--settings -s] 
                         [--filter-option -f]  
                         
[--recursive -rec]  
                         
[--include-details -i]  
                         
[--description -d]
```
### Examples
Show whether BDC telemetry collection is enabled.
```bash
azdata bdc settings show --settings bdc.telemetry.customerFeedback
```
Show user configured settings in the BDC at all scopes.
```bash
azdata bdc settings show --recursive
```
Show all pending settings in the BDC at all scopes.
```bash
azdata bdc settings show –filter-option=pending --recursive
```
### Optional Parameters
#### `--settings -s`
Shows information for the specified setting name(s).
#### `--filter-option -f`
Filter which cluster-scope settings are shown, rather than only 'User Configured' settings. Filters are available to show all settings (system-managed & user-configurable), all configurable settings, or pending settings.
`userConfigured`
#### `--recursive -rec`
Shows the setting information for the cluster scope and all lower-scoped components (services, resources).
#### `--include-details -i`
Includes additional details for the settings chosen to be shown.
#### `--description -d`
Includes a description of the setting.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata bdc settings revert
Reverts pending settings changes in the BDC at all scopes.
```bash
azdata bdc settings revert [--force -f] 
                           
```
### Examples
Revert the BDC's pending settings changes at all scopes.
```bash
azdata bdc settings revert
```
Force revert, the user will not be prompted for any confirmation and all issues will be printed as part of stderr.
```bash
azdata bdc settings revert --force
```
### Optional Parameters
#### `--force -f`
Force revert, the user will not be prompted for any confirmation and all issues will be printed as part of stderr.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

For more information about how to install the **azdata** tool, see [Install azdata](..\install\deploy-install-azdata.md).