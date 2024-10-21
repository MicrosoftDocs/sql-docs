---
title: "Appendix - 1 (Db2ToSQL)"
description: View the command line options for SQL Server Migration Assistant Console for Db2.
author: cpichuka
ms.author: cpichuka
ms.reviewer: randolphwest
ms.date: 09/24/2024
ms.service: sql
ms.subservice: ssma
ms.topic: conceptual
ms.collection:
  - sql-migration-content
---
# Appendix 1: Console command line options (Db2ToSQL)

Quick view of the SQL Server Migration Assistant (SSMA) Console command line options:

| Slot number | Switch | Required | Switch argument | Permitted values |
| --- | --- | --- | --- | --- |
| 1 | `-s`&#124;`script` | Yes | `scriptfile` | Valid XML file name.<br /><br />Console Script definition file. |
| 2 | `-v`&#124;`variable` | No | `variablevaluefile` | Valid XML file name.<br /><br />If variables are used in script file, then this file must be specified. |
| 3 | `-c`&#124;`serverconnection` | No | `serverconnectionfile` | Valid XML file name.<br /><br />This file contains server connection information. |
| 4 | `-x`&#124;`xmloutput` | No | `xmloutputfile` | This option indicates console output in the XML format. If this option isn't specified, the default output is in TEXT format.<br /><br />If xmloutputfile isn't specified, XML output is directed to STDOUT.<br /><br />Xmloutputfile is the name of the file to which the console output is written in the XML format. |
| 5 | `-l`&#124;`log` | No | `logfile` | Valid file name. |
| 6 | `-e`&#124;`projectenvironment` | No | `projectenvironmentfolder` | Valid folder name containing SSMA project environment files. |
| 7 | `-p`&#124;`securepassword` | No | `-a`&#124;`add { <server_id> [ , ...n ] `&#124;` all } ` `-c`&#124;`serverconnection <server-connection-file> [ -v`&#124;`variable <variable-value-file> ] [ -o`&#124;`overwrite ] `<br /><br />or<br /><br />`-a`&#124;`add { <server_id> [ ,...n ] `&#124;` all } ` `-s`&#124;`script <script-file> [ -v`&#124;`variable <variable-value-file> ] [ -o`&#124;`overwrite ] `<br /><br />`-r`&#124;`remove { <server_id> [ , ...n ] `&#124;` all } <br /><br />-l`&#124;`list`<br /><br />`-e`&#124;`export { <server-id> [ , ...n ] `&#124;` all } <encrypted-password-file>`<br /><br />`-i`&#124;`import { <server-id> [ , ...n ] `&#124;` all } <encrypted-password-file>` | If specified, this option must not be combined with any other options.<br /><br />`server-id`: A unique ID provided for a server ` { string } `<br /><br />`server-connection-file`: server definition file (`serverconnectionfile` or `scriptfile`).<br /><br />`variable-value-file`: A variable definition file used in `server-connection-file`.<br /><br />`encrypted-password-file`: A server password file encrypted using a user-specified passphrase. |
| 8 | `-?` | No | Not applicable | Not applicable |

## Related content

- [Execute the SSMA console (Db2ToSQL)](executing-the-ssma-console-db2tosql.md)
