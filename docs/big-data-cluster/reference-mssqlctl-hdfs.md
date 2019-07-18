---
title: azdata hdfs reference
titleSuffix: SQL Server big data clusters
description: Reference article for azdata hdfs commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 06/26/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# azdata hdfs

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **hdfs** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[azdata hdfs shell](#azdata-hdfs-shell) | The HDFS shell is a simple interactive command shell for HDFS file system.
[azdata hdfs ls](#azdata-hdfs-ls) | List the status of the given file or directory.
[azdata hdfs exists](#azdata-hdfs-exists) | Determine if a file or directory exists.  Returns True if exists and False otherwise.
[azdata hdfs mkdir](#azdata-hdfs-mkdir) | Create a directory at the specified path.
[azdata hdfs mv](#azdata-hdfs-mv) | Move the specified file or path to the specified location.
[azdata hdfs create](#azdata-hdfs-create) | Create the text file at the specified location.  Simple text content can be added via data parameter.
[azdata hdfs read](#azdata-hdfs-read) | Read content of a file.  Offset and length in bytes are optional parameters.
[azdata hdfs rm](#azdata-hdfs-rm) | Remove a file or directory.
[azdata hdfs rmr](#azdata-hdfs-rmr) | Recursively remove a file or directory.
[azdata hdfs chmod](#azdata-hdfs-chmod) | Change the permission on the specified file or directory.
[azdata hdfs chown](#azdata-hdfs-chown) | Change the owner or group of the specified file.
## azdata hdfs shell
The HDFS shell is a simple interactive command shell for HDFS file system.
```bash
azdata hdfs shell 
```
### Examples
Launch the shell.
```bash
azdata hdfs shell
```
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs ls
List the status of the given file or directory.
```bash
azdata hdfs ls --path -p 
                 
```
### Examples
List Status
```bash
azdata hdfs ls --path '/tmp'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs exists
Determine if a file or directory exists.  Returns True if exists and False otherwise.
```bash
azdata hdfs exists --path -p 
                     
```
### Examples
Check for file or directory existance.
```bash
azdata hdfs exists --path '/tmp'
```
### Required Parameters
#### `--path -p`
Path to check for existance.
### Global Arguments
#### `--debug`
Increase logging verbosity to show all debug logs.
#### `--help -h`
Show this help message and exit.
#### `--output -o`
Output format.  Allowed values: json, jsonc, table, tsv.  Default: json.
#### `--query -q`
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs mkdir
Create a directory at the specified path.
```bash
azdata hdfs mkdir --path -p 
                    
```
### Examples
Make directory.
```bash
azdata hdfs mkdir --path '/tmp'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs mv
Move the specified file or path to the specified location.
```bash
azdata hdfs mv --source-path -s 
                 --target-path -t
```
### Examples
Move file or directory.
```bash
azdata hdfs mv --source-path '/tmp' --target-path '/dest'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs create
Create the text file at the specified location.  Simple text content can be added via data parameter.
```bash
azdata hdfs create --path -p 
                     --data -d
```
### Examples
Create file.
```bash
azdata hdfs create --path '/tmp/test.txt' --data "This is a test."
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs read
Read content of a file.  Offset and length in bytes are optional parameters.
```bash
azdata hdfs read --path -p 
                   --offset  
                   --length -l
```
### Examples
Read file.
```bash
azdata hdfs read --path '/tmp/test.txt'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs rm
Remove a file or directory.
```bash
azdata hdfs rm --path -p 
                 
```
### Examples
Remove a file or directory.
```bash
azdata hdfs rm --path '/tmp'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs rmr
Recursively remove a file or directory.
```bash
azdata hdfs rmr --path -p 
                  
```
### Examples
Recursive remove directory.
```bash
azdata hdfs rmr --path '/tmp'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs chmod
Change the permission on the specified file or directory.
```bash
azdata hdfs chmod --path -p 
                    --permission
```
### Examples
Change file or directory permission.
```bash
azdata hdfs chmod --permission 775 --path '/tmp/test.txt'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.
## azdata hdfs chown
Change the owner or group of the specified file.
```bash
azdata hdfs chown --path -p 
                    --owner  
                    --group -g
```
### Examples
Change the owner and group.
```bash
azdata hdfs chown --owner hdfs --group superusergroup --path '/tmp/test.txt'
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
JMESPath query string. See [http://jmespath.org/](http://jmespath.org/]) for more information and examples.
#### `--verbose`
Increase logging verbosity. Use --debug for full debug logs.

## Next steps

For more information about how to install the **azdata** tool, see [Install azdata to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).