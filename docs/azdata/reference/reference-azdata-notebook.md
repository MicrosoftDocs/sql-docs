---
title: azdata notebook reference
titleSuffix: SQL Server Big Data Clusters
description: Reference article for azdata notebook commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: seanw
ms.date: 10/05/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: reference
---

# azdata notebook

Applies to [!INCLUDE [azure-data-cli-azdata](../../includes/azure-data-cli-azdata.md)]

The following article provides reference for the **sql** commands in the **azdata** tool. For more information about other **azdata** commands, see [azdata reference](reference-azdata.md)

## Commands

|Command|Description|
| --- | --- |
[azdata notebook view](#azdata-notebook-view) | View a notebook.  Option to stop at first cell execution error.
[azdata notebook run](#azdata-notebook-run) | Run a notebook.  Execution stops at the first error.
## azdata notebook view
This command can take a notebook file and convert the markdown, code, and output to color terminal format.
```bash
azdata notebook view --path -p 
                     [--continue-on-error -c]
```
### Examples
View notebook.  This shows all cells.
```bash
azdata notebook view --path "/home/me/notebooks/demo_notebook.ipynb"
```
View notebook.  This shows all cells unless a cell with error in it's output is encountered.  In that case the output stops.
```bash
azdata notebook view --path "/home/me/notebooks/demo_notebook.ipynb" --stop-on-error
```
### Required Parameters
#### `--path -p`
The path to the notebook to view.
### Optional Parameters
#### `--continue-on-error -c`
Continue displaying additional cells ignoring any cell errors found in the notebook output. The default behavior is to stop on error.  Stopping makes seeing the first cell that encountered an error easier.
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
## azdata notebook run
This command creates a temporary directory and executes the given notebook within it as the working directory.
```bash
azdata notebook run --path -p 
                    [--output-path]  
                    
[--output-html]  
                    
[--arguments -a]  
                    
[--interactive -i]  
                    
[--clear -c]  
                    
[--timeout -t]  
                    
[--env -e]
```
### Examples
Run notebook.
```bash
azdata notebook run --path "/home/me/notebooks/demo_notebook.ipynb"
```
### Required Parameters
#### `--path -p`
The file path to the notebook to run.
### Optional Parameters
#### `--output-path`
Directory path to use for notebook output.  Notebook with output data and any notebook generated files are generated relative to this directory.
#### `--output-html`
Optional flag indicatingg whether to additionally convert the output notebook to HTML format.  Creates a second output file.
#### `--arguments -a`
Optional list of notebook arguments to inject into the notebook execution.  Encoded as a JSON dictionary.  Example: '{"name":"value", "name2":"value2"}'
#### `--interactive -i`
Run a notebook in an interactive mode.
#### `--clear -c`
In interactive mode clear the console before rendering a cell.
#### `--timeout -t`
Seconds to wait for the execution to complete. The value -1 indicates wait forever.
`600`
#### `--env -e`
Name of environment.
`base`
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