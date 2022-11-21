---
title: azdata bdc hdfs reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata bdc hdfs commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata bdc hdfs

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata bdc hdfs key](reference-azdata-bdc-hdfs-key.md) | Manage Hadoop encryption zone keys.
[azdata bdc hdfs status](reference-azdata-bdc-hdfs-status.md) | Hdfs service status commands.
[azdata bdc hdfs shell](#azdata-bdc-hdfs-shell) | The HDFS shell is a simple interactive command shell for HDFS file system.
[azdata bdc hdfs ls](#azdata-bdc-hdfs-ls) | List the status of the given file or directory.
[azdata bdc hdfs exists](#azdata-bdc-hdfs-exists) | Determine if a file or directory exists.  Returns True if exists and False otherwise.
[azdata bdc hdfs mkdir](#azdata-bdc-hdfs-mkdir) | Create a directory at the specified path.
[azdata bdc hdfs mv](#azdata-bdc-hdfs-mv) | Move the specified file or path to the specified location.
[azdata bdc hdfs create](#azdata-bdc-hdfs-create) | Create the text file at the specified location.  Simple text content can be added via data parameter.
[azdata bdc hdfs cat](#azdata-bdc-hdfs-cat) | Read content of a file.  Offset and length in bytes are optional parameters.
[azdata bdc hdfs rm](#azdata-bdc-hdfs-rm) | Remove a file or directory.
[azdata bdc hdfs rmr](#azdata-bdc-hdfs-rmr) | Recursively remove a file or directory.
[azdata bdc hdfs chmod](#azdata-bdc-hdfs-chmod) | Change the permission on the specified file or directory.
[azdata bdc hdfs chown](#azdata-bdc-hdfs-chown) | Change the owner or group of the specified file.
[azdata bdc hdfs cp](#azdata-bdc-hdfs-cp) | Copy a file or directory between the local machine and HDFS.
[azdata bdc hdfs mount](reference-azdata-bdc-hdfs-mount.md) | Manage mounting of remote stores in HDFS.
[azdata bdc hdfs settings](reference-azdata-bdc-hdfs-settings.md) | BDC settings commands for hdfs service.
[azdata bdc hdfs encryption-zone](reference-azdata-bdc-hdfs-encryption-zone.md) | Manage encryption zones in HDFS.
## azdata bdc hdfs shell
The HDFS shell is a simple interactive command shell for HDFS file system.
```bash
azdata bdc hdfs shell 
```
### Examples
Launch the shell.
```bash
azdata bdc hdfs shell
```
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
## azdata bdc hdfs ls
List the status of the given file or directory.
```bash
azdata bdc hdfs ls --path -p 
                   
```
### Examples
List Status
```bash
azdata bdc hdfs ls --path "tmp/"
```
### Required Parameters
#### `--path -p`
The path to list status.
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
## azdata bdc hdfs exists
Determine if a file or directory exists.  Returns True if exists and False otherwise.
```bash
azdata bdc hdfs exists --path -p 
                       
```
### Examples
Check for file or directory existance.
```bash
azdata bdc hdfs exists --path "tmp/"
```
### Required Parameters
#### `--path -p`
Path to check for existence.
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
## azdata bdc hdfs mkdir
Create a directory at the specified path.
```bash
azdata bdc hdfs mkdir --path -p 
                      
```
### Examples
Make directory.
```bash
azdata bdc hdfs mkdir --path "tmp/"
```
### Required Parameters
#### `--path -p`
Name of the directory to create.
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
## azdata bdc hdfs mv
Move the specified file or path to the specified location.
```bash
azdata bdc hdfs mv --source-path -s 
                   --target-path -t
```
### Examples
Move file or directory.
```bash
azdata bdc hdfs mv --source-path "tmp/" --target-path "dest/"
```
### Required Parameters
#### `--source-path -s`
The directory to move.
#### `--target-path -t`
The location to move to.
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
## azdata bdc hdfs create
Create the text file at the specified location.  Simple text content can be added via data parameter.
```bash
azdata bdc hdfs create --path -p 
                       --data -d
```
### Examples
Create file.
```bash
azdata bdc hdfs create --path "tmp/test.txt" --data "This is a test."
```
### Required Parameters
#### `--path -p`
Name of the file to create.
#### `--data -d`
Content of the file.  Meant for simple text content.
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
## azdata bdc hdfs cat
Read content of a file.  Offset and length in bytes are optional parameters.
```bash
azdata bdc hdfs cat --path -p 
                    --offset  
                    
--length -l
```
### Examples
Read file.
```bash
azdata bdc hdfs cat --path "tmp/test.txt"
```
### Required Parameters
#### `--path -p`
Name of the file to read.
#### `--offset`
Number of bytes offset within the file to read.
#### `--length -l`
Length of the data to read.
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
## azdata bdc hdfs rm
Remove a file or directory.
```bash
azdata bdc hdfs rm --path -p 
                   
```
### Examples
Remove a file or directory.
```bash
azdata bdc hdfs rm --path "tmp/"
```
### Required Parameters
#### `--path -p`
Name of the file to remove.
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
## azdata bdc hdfs rmr
Recursively remove a file or directory.
```bash
azdata bdc hdfs rmr --path -p 
                    
```
### Examples
Recursive remove directory.
```bash
azdata bdc hdfs rmr --path "tmp/"
```
### Required Parameters
#### `--path -p`
Name of the file to remove recursively.
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
## azdata bdc hdfs chmod
Change the permission on the specified file or directory.
```bash
azdata bdc hdfs chmod --path -p 
                      --permission
```
### Examples
Change file or directory permission.
```bash
azdata bdc hdfs chmod --permission 775 --path "tmp/test.txt"
```
### Required Parameters
#### `--path -p`
Name of the file or directory to set permissions on.
#### `--permission`
Permission octets to set.  Example "775".
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
## azdata bdc hdfs chown
Change the owner or group of the specified file.
```bash
azdata bdc hdfs chown --path -p 
                      --owner  
                      
--group -g
```
### Examples
Change the owner and group.
```bash
azdata bdc hdfs chown --owner hdfs --group superusergroup --path "tmp/test.txt"
```
### Required Parameters
#### `--path -p`
Name of the file or directory to change owner of.
#### `--owner`
The owner name to set to.
#### `--group -g`
Group name to set to.
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
## azdata bdc hdfs cp
Copy a file or directory between the local machine and HDFS.  If the input is a directory then the whole directory tree is copied.  If the target file or directory exists the command will fail.  To specify the remote HDFS directory prefix the path with "hdfs:"
```bash
azdata bdc hdfs cp --from-path -f 
                   --to-path -t
```
### Examples
Copy file or directory between the local machine and HDFS.
```bash
azdata bdc hdfs cp --from-path "tmp/test.txt" --to-path "hdfs:/user/me/test.txt"
```
### Required Parameters
#### `--from-path -f`
Name of the path to copy from.  Prefix the path with "hdfs:" to indicate an HDFS path.
#### `--to-path -t`
Name of the path to copy to.  Prefix the path with "hdfs:" to indicate an HDFS path.
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