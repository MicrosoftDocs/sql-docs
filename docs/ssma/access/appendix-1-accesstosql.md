---
title: "Appendix - 1 (AccessToSQL)"
description: "Appendix - 1 (AccessToSQL)"
author: nilabjaball
ms.author: niball
ms.reviewer: vanto
ms.date: 12/30/2025
ms.service: sql
ms.subservice: ssma
ms.topic: reference
ai-usage: ai-assisted
---
# Appendix - 1 (AccessToSQL)

This article provides an overview of the SQL Server Migration Assistant (SSMA) Console command-line options.

## Arguments

#### `-s` or `script`

- **Required**: Yes

- **Switch argument**: *scriptfile*

- **Permitted values**: Valid XML file name. Console script definition file.

#### `-v` or `variable`

- **Required**: No

- **Switch argument**: *variablevaluefile*

- **Permitted values**: Valid XML file name. If the script file uses variables, you must specify this file.

#### `-c` or `serverconnection`

- **Required**: No

- **Switch argument**: *serverconnectionfile*

- **Permitted values**: Valid XML file name. This file contains server connection information.

#### `-x` or `xmloutput`

- **Required**: No

- **Switch argument**: *xmloutputfile*

- **Permitted values**: Indicates console output in XML format. If you don't specify this option, the default output is in text format. If you don't specify *xmloutputfile*, XML output goes to `stdout`. *xmloutputfile* is the name of the file to which the console output is written in the XML format.

#### `-l` or `log`

- **Required**: No

- **Switch argument**: *logfile*

- **Permitted values**: Valid file name.

#### `-e` or `projectenvironment`

- **Required**: No

- **Switch argument**: *projectenvironmentfolder*

- **Permitted values**: Valid folder name containing SSMA project environment files.

#### `-p` or `securepassword`

- **Required**: No

- **Switch arguments and forms**:

  - `-a|add {<server_id> [,...n] | all} -c|serverconnection <server-connection-file> [-v|variable <variable-value-file>] [-o|overwrite]`

  - `-a|add {<server_id> [,...n] | all} -s|script <script-file> [-v|variable <variable-value-file>] [-o|overwrite]`

  - `-r|remove {<server_id> [, ...n] | all}`

  - `-l|list`

  - `-e|export {<server-id> [, ...n] | all} <encrypted-password-file>`

  - `-i|import {<server-id> [, ...n] | all} <encrypted-password-file>`

- **Notes**:

  - Don't combine this option with other options.
  - `server-id`: A unique ID you provide for a server (`string`).
  - `server-connection-file`: Server definition file (*serverconnectionfile* or *scriptfile*).
  - `variable-value-file`: A variable definition file used in `server-connection-file`.
  - `encrypted-password-file`: A server passwords file encrypted with a user-specified passphrase.

#### `-?`

- **Required**: No

- **Switch argument**: Not applicable

- **Permitted values**: Not applicable

## Related content

- [Execute the SSMA Console](executing-the-ssma-console-accesstosql.md)
