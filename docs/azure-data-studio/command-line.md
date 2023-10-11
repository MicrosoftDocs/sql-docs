---
title: Command line interface in Azure Data Studio
description: Learn more about the command line interface within Azure Data Studio and how to use it.
author: erinstellato-ms
ms.author: erinstellato
ms.reviewer: maghan, randolphwest
ms.date: 08/21/2023
ms.service: azure-data-studio
ms.topic: conceptual
---
# The Azure Data Studio command line interface

Azure Data Studio includes a built-in command line interface that lets you control how you launch the application. You can open files, install extensions, change the display language, and output diagnostics through command line options (switches).

:::image type="content" source="media/command-line/cli-introduction.png" alt-text="Screenshot of command line output for Azure Data Studio.":::

For examples of how to run command line tools inside Azure Data Studio, see [Integrated terminal](integrated-terminal.md).

## Command line help

To view an overview of the Azure Data Studio command line interface, open a terminal within Azure Data Studio (**View > Terminal**) or a command prompt and type `azuredatastudio --help`. The output contains the version, usage example, and list of command line options, as shown in the truncated example:

```bash
C:\>azuredatastudio --help
Azure Data Studio 1.45.1

Usage: azuredatastudio.exe [options][paths...]

To read output from another program, append '-' (e.g. 'echo Hello World | azuredatastudio.exe -')

Options

  <vscode options>

  -h --help                                  Print usage.
  --aad                                      Use Azure Active Directory authentication, this option is
                                             deprecated - use 'authenticationType' instead.
  -A --applicationName                       Supports providing applicationName
                                             that will be used for connection profile app name.
  -T --authenticationType                    Provide authentication mode to be
                                             used. Accepted values: AzureMFA, SqlLogin, Integrated, etc.
  -c --command <command-name>                Name of command to run, accepted
                                             values: connect, openConnectionDialog
  -Z --connectionProperties                  Supports providing advanced connection properties that
                                             providers support. Value must be a json object containing
                                             key-value pairs in format: '{"key1":"value1"}'
  -D --database <database>                   Name of database
  -E --integrated                            Use Integrated authentication,
                                             this option is deprecated - use 'authenticationType' instead.
  -P --provider                              Connection provider to use, e.g. MSSQL, PGSQL, etc.
  -S --server <server>                       Name of target server or host name.
  --showDashboard                            Whether or not to show dashboard on connection, false by default.
  -U --user <user-name>                      User name/email address
```

## Launch from the command line

You can launch Azure Data Studio from the command line to quickly open a file, folder, or project. Typically, you open Azure Data Studio within the context of a folder. From an open terminal or command prompt, navigate to your project folder and type `azuredatastudio`.

If you have the Azure Data Studio Insiders build installed and want to open it from a command line, use `azuredatastudio-insiders`.

## Launch with Query Editor

Sometimes you may want to open a script in the Query Editor when opening Azure Data Studio. You can launch Azure Data Studio with an existing script by providing the path of the file:

```bash
azuredatastudio .\samplescript.sql
```

## Launch using URI

You can launch Azure Data Studio from browser using URL format to quickly establish connection on launch, or open a connection dialog, optionally opening a script in the Query Editor. The supported format for launching with URI is:

```bash
azuredatastudio://{command}?{option1}={value1}&{option2}={value2}...
```

### Supported commands

The following commands are supported when launching Azure Data Studio from a command line:

- `connect`: Connects to the target server using the provided options
- `openConnectionDialog`: Opens the connection dialog using the provided options

### Supported options

The following options can be provided in the URL:

- `provider`: Connection provider to use, for example, MSSQL, PGSQL, and so on.
- `server`: Name of target server or host name
- `database`: Name of database
- `user`: Name of user
- `authenticationType`: Authentication mode to be used, accepted values: `AzureMFA`, `SqlLogin`, `Integrated`, and so on.
- `applicationName`: Provide an application name that is used in the connection profile
- `connectionProperties`: Advanced connection properties that a provider supports. Value must be a json object containing key-value pairs in format: `{"key1":"value1"}`.

## Examples

#### Command prompt: Integrated authentication

```bash
azuredatastudio --server localhost --provider mssql --authenticationType Integrated
```

#### Command prompt: Launch the Insider build with a saved script

```bash
azuredatastudio-insider --server localhost --provider mssql --user sa .\samplescript.sql
```

#### Browser

```bash
azuredatastudio://connect?server=*****&user=*****&authenticationType=*****&connectionProperties={"key1":"value1"}
```

## Next steps

- [Integrated terminal](integrated-terminal.md)
- [Extend the functionality of Azure Data Studio](extensions/add-extensions.md)
- [Modify user and workspace settings](settings.md)
