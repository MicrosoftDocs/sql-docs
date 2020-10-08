---
title: azdata arc dc reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata arc dc commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 09/22/2020
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata arc dc

Applies to `azdata`

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata arc dc create](#azdata-arc-dc-create) | Create data controller.
[azdata arc dc delete](#azdata-arc-dc-delete) | Delete data controller.
[azdata arc dc endpoint](reference-azdata-arc-dc-endpoint.md) | Endpoint commands.
[azdata arc dc status](reference-azdata-arc-dc-status.md) | Status commands.
[azdata arc dc config](reference-azdata-arc-dc-config.md) | Configuration commands.
[azdata arc dc debug](reference-azdata-arc-dc-debug.md) | Debug commands.
[azdata arc dc export](#azdata-arc-dc-export) | Export metrics, logs or usage.
[azdata arc dc upload](#azdata-arc-dc-upload) | Upload exported data file.
## azdata arc dc create
Create data controller - kube config is required on your system along with the following environment variables ['AZDATA_USERNAME', 'AZDATA_PASSWORD'].
```bash
azdata arc dc create --namespace -ns 
                     --name -n  
                     
--connectivity-mode  
                     
--resource-group -g  
                     
--location -l  
                     
--subscription -s  
                     
[--profile-name]  
                     
[--path -p]  
                     
[--storage-class -sc]
```
### Examples
Data controller deployment.
```bash
azdata arc dc create
```
### Required Parameters
#### `--namespace -ns`
The Kubernetes namespace to deploy the data controller into. If it exists already it will be used. If it does not exist, an attempt will be made to create it first.
#### `--name -n`
The name for the data controller.
#### `--connectivity-mode`
The connectivity to Azure - indirect or direct - which the data controller should operate in.
#### `--resource-group -g`
The Azure resource group in which the data controller resource should be added.
#### `--location -l`
The Azure location in which the data controller metadata will be stored (e.g. eastus).
#### `--subscription -s`
The Azure subscription ID in which the data controller resource should be added.
### Optional Parameters
#### `--profile-name`
The name of an existing configuration profile. Run `azdata arc dc config list` to see available options. One of the following: ['azure-arc-aks-premium-storage', 'azure-arc-ake', 'azure-arc-openshift', 'azure-arc-gke', 'azure-arc-aks-default-storage', 'azure-arc-kubeadm', 'azure-arc-eks', 'azure-arc-azure-openshift', 'azure-arc-aks-hci'].
#### `--path -p`
The path to a directory containing a custom configuration profile to use. Run `azdata arc dc config init` to create a custom configuration profile.
#### `--storage-class -sc`
The storage class to be use for all data and logs persistent volumes for all data controller pods that require them.
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
## azdata arc dc delete
Delete data controller - kube config is required on your system.
```bash
azdata arc dc delete --name -n 
                     --namespace -ns  
                     
[--force -f]
```
### Examples
Data controller deployment.
```bash
azdata arc dc delete
```
### Required Parameters
#### `--name -n`
Data controller name.
#### `--namespace -ns`
The Kubernetes namespace in which the data controller exists.
### Optional Parameters
#### `--force -f`
Force delete data controller.
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
## azdata arc dc export
Export metrics, logs or usage to a file.
```bash
azdata arc dc export --type -t 
                     --path -p  
                     
[--force -f]
```
### Required Parameters
#### `--type -t`
The type of data to be exported. Options: logs, metrics, and usage.
#### `--path -p`
The full or relative path including the file name of the file to be exported.
### Optional Parameters
#### `--force -f`
Force create output file. Overwrites any existing file at the same path.
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
## azdata arc dc upload
Upload data file exported from a data controller to Azure.
```bash
azdata arc dc upload --path -p 
                     
```
### Required Parameters
#### `--path -p`
The full or relative path including the file name of the file to be uploaded.
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

