---
title: azdata bdc notebook run
titleSuffix: SQL Server big data clusters
description: Reference article for azdata bdc notebook run commands.
author: MikeRayMSFT
ms.author: mikeray
ms.reviewer: mihaelab
ms.date: 08/21/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# `azdata bdc notebook run`

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

The following article provides reference for the `bdc notebook run` commands in the `azdata` tool. For more information about other `azdata` commands, see [azdata reference](reference-azdata.md).

## Commands
|     |     |
| --- | --- |
[`azdata bdc notebook run`](#azdata-bdc-status-show) | Run a notebook.

## Example

```bash
azdata notebook run --path '/home/me/notebooks/demo_notebook.ipynb'
```

### Arguments

#### `--path`

The file path to the notebook to run. Default: `/.`

#### `--help -h`

Show this help message and exit.

#### `--arguments -a`

Optional list of notebook arguments to inject into the notebook execution. Encoded as a JSON dictionary.

Example:
    ```bash
    '{"name":"value",
        "name2":"value2"}'
    ```

#### `--clear -c`

In interactive mode clear the console before rendering a cell.

#### `--interactive -i`

Run a notebook in an interactive mode.

#### `--output-html`

Optional flag indicating whether to additionally convert the output notebook to HTML format. Creates a second output file.

#### `--output-path`

Directory path to use for notebook output. Notebook with output data and any notebook generated files are generated relative to this directory.

### Global arguments

#### `--debug`

Increase logging verbosity to show all debug logs.

#### `--help -h`

Show this help message and exit.

#### `--output -o`

Output format. Allowed values: `json`, `jsonc`, `table`, `tsv`. Default: `json`.

#### `--query`

`JMESPath` query string. See [jmespath.org](http://jmespath.org/) for more information and examples.

#### `--verbose`

Increase logging verbosity. Use `--debug` for full debug logs.

## Next steps

For more information about other `azdata` commands, see [azdata reference](reference-azdata.md). For more information about how to install the `azdata` tool, see [Install azdata to manage [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ver15.md)]](deploy-install-azdata.md).

