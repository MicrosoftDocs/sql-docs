---
title: mssqlctl app reference
titleSuffix: SQL Server 2019 big data clusters
description: Reference article for mssqlctl app commands.
author: rothja
ms.author: jroth
manager: craigg
ms.date: 02/27/2019
ms.topic: reference
ms.prod: sql
ms.technology: big-data-cluster
---

# mssqlctl app

The following article provides reference for the **app** commands in the **mssqlctl** tool. For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md).

## <a id="commands"></a> Commands

|||
|---|---|
| [create](#create) | Create application. |
| [delete](#delete) | Delete application. |
| [describe](#describe) | Describe application. |
| [init](#init) | Kickstart new application skeleton. |
| [list](#list) | List application(s). |
| [run](#run) | Run application. |
| [update](#update) | Update application. |
| [template](reference-mssqlctl-app-template.md) | Template commands. |

## <a id="create"></a> mssqlctl app create

Create application.

```
mssqlctl app create
   --assets
   --code
   --description
   --entrypoint
   --inputs
   --name
   --outputs
   --runtime
   --spec
   --version
   --yes
```

### Parameters

| Parameters | Description |
|---|---|
| **--assets -a** | List of additional application file assets to be included. |
| **--code -c** | Path to R or Python code file. |
| **--description -d** | Description of the application. |
| **--entrypoint** |  |
| **--inputs** | Input parameter schema. |
| **--name -n** | Application name. |
| **--outputs** | Output parameter schema. |
| **--runtime -r** | Application runtime.  Allowed values: Mleap, Python, R, SSIS. |
| **--spec -s** | Path to a directory with a YAML spec file describing the application. |
| **--version -v** | Application version. |
| **--yes -y** | Do not prompt for confirmation when creating an application from the CWD's spec.yaml file. |

### Examples

Create a new application via spec.yaml (recommended).

```
mssqlctl app create --spec /path/to/dir/with/spec/yaml
```

Create a new Python app inline using arguments.

```
mssqlctl app create --name add --version v1 --inputs x=float, y=float --outputs result=float --runtime Python --code add.py  --init init.py
```

Create a new R application inline using arguments.

```
mssqlctl app create --name add --version v1 --inputs x=numeric, y=numeric --outputs result=numeric --runtime R --code add.R  --init init.R
```

Create a new R application inline with additional file assets to be included.

```
mssqlctl app create --name add --version v1 --runtime R --code  add.R --assets file.RData,/path/to/more/files
```

## <a id="delete"></a> mssqlctl app delete

Delete application.

```
mssqlctl app delete
   --name
   --version
```

### Parameters

| Parameters | Description |
|---|---|
| **--name -n** | Application name. |
| **--version -v** | Application version. |

### Examples

Delete application by name and version.

```
mssqlctl app delete --name reduce --version v1
```

## <a id="describe"></a> mssqlctl app describe

Describe application.

```
mssqlctl app describe
   --name
   --spec
   --version
```

### Parameters

| Parameters | Description |
|---|---|
| **--name -n** | Application name. |
| **--spec -s** | Path to a directory with a YAML spec file describing the application. |
| **--version -v** | Application version. |

### Examples

Describe the application.

```
mssqlctl app describe --name reduce --version v1
```

## <a id="init"></a> mssqlctl app init

Kickstart new application skeleton.

```
mssqlctl app init
   --destination
   --name
   --spec
   --template
   --url
   --version
```

### Parameters

| Parameters | Description |
|---|---|
| **--destination -d** | Where to place the application skeleton. Default: current working directory. |
| **--name -n** | Application name. |
| **--spec -s** | Generate just an application spec.yaml. |
| **--template -t** | Template name. For a full list off supported template names run `mssqlctl app template list`. |
| **--url -u** | Specify a different template repository location. Default: https://github.com/Microsoft/sql-server-samples.git. |
| **--version -v** | Application version. |

### Examples

Scaffold a new application `spec.yaml` only.

```
mssqlctl app init --spec
```

Scaffold a new R application application skeleton based on the `r` template.

```
mssqlctl app init --name reduce --template r
```

Scaffold a new Python application application skeleton based on the `python` template.

```
mssqlctl app init --name reduce --template python
```

Scaffold a new SSIS application application skeleton based on the `ssis` template.

```
mssqlctl app init --name reduce --template ssis
```

## <a id="list"></a> mssqlctl app list

List application(s).

```
mssqlctl app list
   --name
   --version
```

### Parameters

| Parameters | Description |
|---|---|
| **--name -n** | Application name. |
| **--version -v** | Application version. |

### Examples

List application by name and version.

```
mssqlctl app list --name reduce  --version v1
```

List all application versions by name.

```
mssqlctl app list --name reduce
```

List all applications.

```
mssqlctl app list
```

## <a id="run"></a> mssqlctl app run

Run application.

```
mssqlctl app run
   --name
   --version
   --inputs
```

### Parameters

| Parameters | Description |
|---|---|
| **--name -n** | Application name. |
| **--version -v** | Application version. |
| **--inputs** | Application input parameters in a CSV `name=value` format. |

### Examples

Run application with no input parameters.

```
mssqlctl app run --name reduce --version v1
```

Run application with 1 input parameter.

```
mssqlctl app run --name reduce --version v1 --inputs x=10
```

Run application with multiple input parameters.

```
mssqlctl app run --name reduce --version v1 --inputs x=10,y5.6
```

## <a id="update"></a> mssqlctl app update

Update application.

```
mssqlctl app update
   --spec
   --yes
```

### Parameters

| Parameters | Description |
|---|---|
| **--spec -s** | Path to a directory with a YAML spec file describing the application. |
| **--yes -y** | Do not prompt for confirmation when updating an application from the CWD's spec.yaml file. |

## Next steps

For more information about other **mssqlctl** commands, see [mssqlctl reference](reference-mssqlctl.md). For more information about how to install the **mssqlctl** tool, see [Install mssqlctl to manage SQL Server 2019 big data clusters](deploy-install-mssqlctl.md).