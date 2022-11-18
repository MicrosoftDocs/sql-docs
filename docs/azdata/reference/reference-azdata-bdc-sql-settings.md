---
title: azdata bdc sql settings reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc sql settings commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc sql settings

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc sql settings set](#azdata-bdc-sql-settings-set) | Set sql service-scope settings.
[azdata bdc sql settings show](#azdata-bdc-sql-settings-show) | Show sql service-scope settings and optionally sql settings for specified resources
## azdata bdc sql settings set
Provides the ability to set a service-scoped or resource-scoped setting. Specify the full name of the setting and the value. Does not apply the setting to the running BDC. Run apply to do so.
```bash
azdata bdc sql settings set [--resources -r] 
                            [--settings -s]
```
### Examples
Enable SQL agent in the SQL Server Master instance.
```bash
azdata bdc sql settings set --settings mssql.sqlagent.enabled=true --resources master
```
Set the number of CPUs for SQL Server to 10 in the Data Pool.
```bash
azdata bdc sql settings set --settings mssql.numberOfCpus=10 --resources data-0 
```
### Optional Parameters
#### `--resources -r`
Sets the given setting(s) for the provided resource(s). Resources can be specified as a comma separated list.
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
## azdata bdc sql settings show
Shows the BDC's sql service-scope (optionally resource-scope) settings. By default, this command shows user-configured service-scope settings. Filters are available to show all settings (system-managed and configurable), configurable settings, or pending settings. To see a specific service-scope or resource-scope setting, you can provide the setting name. Use "recursive" to show settings for all resources as part of the service.
```bash
azdata bdc sql settings show [--resources -r] 
                             [--settings -s]  
                             
[--filter-option -f]  
                             
[--recursive -rec]  
                             
[--include-details -i]  
                             
[--description -d]
```
### Examples
Show the user configured SQL service-scope settings.
```bash
azdata bdc sql settings show
```
Show the max server memory SQL configuration in the Data Pool.
```bash
azdata bdc sql settings show --settings mssql.maxServerMemory --resources data-0
```
Show the pending settings changes for the SQL service-scope and resource-scope settings.
```bash
azdata bdc sql settings show --filter-option=pending --recursive
```
### Optional Parameters
#### `--resources -r`
Shows the setting(s) information for the given resource(s). Resources can be specified as a comma separated list.
#### `--settings -s`
Shows information for the specified setting name(s).
#### `--filter-option -f`
Filter which cluster-scope settings are shown, rather than only 'User Configured' settings. Filters are available to show all settings (system-managed & user-configurable), all configurable settings, or pending settings.
`userConfigured`
#### `--recursive -rec`
Shows the setting information for the given scope (service or service-resource) and all lower-scoped components (resources).
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

## Next steps

For more information about other **azdata** commands, see [azdata reference](reference-azdata.md). 

For more information about how to install the **azdata** tool, see [Install azdata](..\install\deploy-install-azdata.md).
