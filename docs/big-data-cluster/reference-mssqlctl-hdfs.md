---
title: mssqlctl hdfs reference
titleSuffix: SQL Server big data clusters
description: Reference article for mssqlctl hdfs commands.
author: rothja
ms.author: jroth
manager: jroth
ms.date: 06/26/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl hdfs

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the **hdfs** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## Commands
|     |     |
| --- | --- |
[mssqlctl hdfs shell](#mssqlctl-hdfs-shell) | The HDFS shell is a simple interactive command shell for HDFS file system.
[mssqlctl hdfs ls](#mssqlctl-hdfs-ls) | List the status of the given file or directory.
[mssqlctl hdfs exists](#mssqlctl-hdfs-exists) | Determine if a file or directory exists.  Returns True if exists and False otherwise.
[mssqlctl hdfs mkdir](#mssqlctl-hdfs-mkdir) | Create a directory at the specified path.
[mssqlctl hdfs mv](#mssqlctl-hdfs-mv) | Move the specified file or path to the specified location.
[mssqlctl hdfs create](#mssqlctl-hdfs-create) | Create the text file at the specified location.  Simple text content can be added via data parameter.
[mssqlctl hdfs read](#mssqlctl-hdfs-read) | Read content of a file.  Offset and length in bytes are optional parameters.
[mssqlctl hdfs rm](#mssqlctl-hdfs-rm) | Remove a file or directory.
[mssqlctl hdfs rmr](#mssqlctl-hdfs-rmr) | Recursively remove a file or directory.
[mssqlctl hdfs chmod](#mssqlctl-hdfs-chmod) | Change the permission on the specified file or directory.
[mssqlctl hdfs chown](#mssqlctl-hdfs-chown) | Change the owner or group of the specified file.
## mssqlctl hdfs shell
The HDFS shell is a simple interactive command shell for HDFS file system.
```bash
mssqlctl hdfs shell 
```
### Examples
Launch the shell.
```bash
mssqlctl hdfs shell
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
## mssqlctl hdfs ls
List the status of the given file or directory.
```bash
mssqlctl hdfs ls --path -p 
                 
```
### Examples
List Status
```bash
mssqlctl hdfs ls --path '/tmp'
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
## mssqlctl hdfs exists
Determine if a file or directory exists.  Returns True if exists and False otherwise.
```bash
mssqlctl hdfs exists --path -p 
                     
```
### Examples
Check for file or directory existance.
```bash
mssqlctl hdfs exists --path '/tmp'
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
## mssqlctl hdfs mkdir
Create a directory at the specified path.
```bash
mssqlctl hdfs mkdir --path -p 
                    
```
### Examples
Make directory.
```bash
mssqlctl hdfs mkdir --path '/tmp'
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
## mssqlctl hdfs mv
Move the specified file or path to the specified location.
```bash
mssqlctl hdfs mv --source-path -s 
                 --target-path -t
```
### Examples
Move file or directory.
```bash
mssqlctl hdfs mv --source-path '/tmp' --target-path '/dest'
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
## mssqlctl hdfs create
Create the text file at the specified location.  Simple text content can be added via data parameter.
```bash
mssqlctl hdfs create --path -p 
                     --data -d
```
### Examples
Create file.
```bash
mssqlctl hdfs create --path '/tmp/test.txt' --data "This is a test."
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
## mssqlctl hdfs read
Read content of a file.  Offset and length in bytes are optional parameters.
```bash
mssqlctl hdfs read --path -p 
                   --offset  
                   --length -l
```
### Examples
Read file.
```bash
mssqlctl hdfs read --path '/tmp/test.txt'
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
## mssqlctl hdfs rm
Remove a file or directory.
```bash
mssqlctl hdfs rm --path -p 
                 
```
### Examples
Remove a file or directory.
```bash
mssqlctl hdfs rm --path '/tmp'
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
## mssqlctl hdfs rmr
Recursively remove a file or directory.
```bash
mssqlctl hdfs rmr --path -p 
                  
```
### Examples
Recursive remove directory.
```bash
mssqlctl hdfs rmr --path '/tmp'
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
## mssqlctl hdfs chmod
Change the permission on the specified file or directory.
```bash
mssqlctl hdfs chmod --path -p 
                    --permission
```
### Examples
Change file or directory permission.
```bash
mssqlctl hdfs chmod --permission 775 --path '/tmp/test.txt'
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
## mssqlctl hdfs chown
Change the owner or group of the specified file.
```bash
mssqlctl hdfs chown --path -p 
                    --owner  
                    --group -g
```
### Examples
Change the owner and group.
```bash
mssqlctl hdfs chown --owner hdfs --group superusergroup --path '/tmp/test.txt'
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

For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).